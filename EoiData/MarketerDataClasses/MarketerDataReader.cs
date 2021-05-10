using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace EoiData.MarketerDataClasses
{
    internal static class MarketerDataReader
    {
        private static string _marketerApi = "https://api.evemarketer.com/ec/marketstat";

        internal static List<MarketerDataRequest> PendingRequests { get; private set; } = new List<MarketerDataRequest>();

        internal static List<MarketerDataRequest> CachedRequests { get; private set; } = new List<MarketerDataRequest>();

        internal static int RequestCapacity { get; set; }
        internal static TimeSpan RequestResetTimer { get; set; }

        internal static void Request(string region, string solarSystem)
        {
            using (var client = new HttpClient())
            {
                var values = new Dictionary<string, string>();
                
                var typeIds = string.Empty;
                var requests = PendingRequests.Where(x => x.Region == region && x.SolarSystem == solarSystem);
                foreach (var value in requests)
                {
                    if (typeIds != string.Empty)
                        typeIds += ",";

                    typeIds += value.Id.ToString();
                }
                values.Add("typeid", typeIds);
                
                if (region != null && !string.IsNullOrWhiteSpace(region))
                    values.Add("regionlimit", region);

                if (solarSystem != null && !string.IsNullOrWhiteSpace(solarSystem))
                    values.Add("usesystem", solarSystem);

                var content = new FormUrlEncodedContent(values);

                var result = client.PostAsync(_marketerApi, content).Result;

                if (result.IsSuccessStatusCode)
                {
                    var remainingRequests = result.Headers.FirstOrDefault(x => x.Key == "X-Ratelimit-Remaining");
                    RequestCapacity = int.Parse(remainingRequests.Value.First());

                    var remainingTime = result.Headers.FirstOrDefault(x => x.Key == "X-Ratelimit-Reset");
                    var resetDateTime = new DateTime(1970, 1, 1).AddSeconds(Int32.Parse(remainingTime.Value.First()));
                    RequestResetTimer = resetDateTime.Subtract((DateTime.UtcNow));


                    string resultContent = result.Content.ReadAsStringAsync().Result;

                    TextReader input = new StringReader(resultContent);

                    XmlSerializer serializer = new XmlSerializer(typeof(RawExec_api), new XmlRootAttribute("exec_api"));

                    var marketResponse = (RawExec_api)serializer.Deserialize(input);

                    foreach (var type in marketResponse.marketstat.type)
                    {
                        var pendingRequests = PendingRequests.Where(x => x.Region == region && x.SolarSystem == solarSystem && x.Id.ToString() == type.id).ToList();

                        foreach (var pendingRequest in pendingRequests)
                        {
                            if (pendingRequest != null)
                            {
                                pendingRequest.RawMarketType = type;
                                PendingRequests.Remove(pendingRequest);
                                var cached = CachedRequests.FirstOrDefault(x => x.RawMarketType.id == pendingRequest.RawMarketType.id && x.Region == pendingRequest.Region && x.SolarSystem == pendingRequest.SolarSystem);
                                if (cached == null)
                                    CachedRequests.Add(pendingRequest);
                            }
                        }

                    }
                }
                else
                {
                    var pendingRequests = PendingRequests.Where(x => x.Region == region && x.SolarSystem == solarSystem).ToList();

                    foreach (var pendingRequest in pendingRequests)
                    {
                        if (pendingRequest != null)
                        {
                            PendingRequests.Remove(pendingRequest);
                        }
                    }
                }
            }
        }

        internal static void Import(List<MarketerDataRequest> marketData)
        {
            foreach (var data in marketData)
            {
                var cachedType = CachedRequests.FirstOrDefault(x => x.RawMarketType.id == data.Id.ToString() && x.Region == data.Region && x.SolarSystem == data.SolarSystem);
                if (cachedType != null)
                {
                    if (DateTime.Compare(cachedType.Timestamp, data.Timestamp) < 0)
                        CachedRequests.Remove(cachedType);
                    else
                        continue;
                }

                CachedRequests.Add(data);
            }
        }

        internal static List<MarketerDataRequest> Export()
        {
            return CachedRequests;
        }

        internal static void Request(MarketerDataRequest marketerDataRequest)
        {
            var cachedType = CachedRequests.FirstOrDefault(x => x.RawMarketType.id == marketerDataRequest.Id.ToString() && x.Region == marketerDataRequest.Region && x.SolarSystem == marketerDataRequest.SolarSystem);
            if (cachedType != null)
            {
                if (cachedType != null)
                {
                    var difference = marketerDataRequest.Timestamp - cachedType.Timestamp;
                    if (difference.TotalMinutes > 30)
                        CachedRequests.Remove(cachedType);
                    else
                        return;
                }
            }

            var pendingRequest = PendingRequests.FirstOrDefault(x => x.Id == marketerDataRequest.Id && x.Region == marketerDataRequest.Region && x.SolarSystem == marketerDataRequest.SolarSystem);
            if (pendingRequest != null)
                return;

            PendingRequests.Add(marketerDataRequest);

            if (PendingRequests.Count(x => x.Region == marketerDataRequest.Region && x.SolarSystem == marketerDataRequest.SolarSystem) >= 200)
                Request(marketerDataRequest.Region, marketerDataRequest.SolarSystem);
        }

        internal static void RequestAll()
        {
            var cachedRequests = CachedRequests.ToList();

            foreach (var cachedType in cachedRequests)
            {
                MarketerDataRequest request = null;

                if (cachedType != null)
                {
                    if (cachedType != null)
                    {
                        var difference = DateTime.Now - cachedType.Timestamp;
                        if (difference.TotalMinutes > 30)
                        {
                            request = new MarketerDataRequest(cachedType.Id, cachedType.Region, cachedType.SolarSystem);
                            CachedRequests.Remove(cachedType);
                        }
                        else
                            return;
                    }
                }

                var pendingRequest = PendingRequests.FirstOrDefault(x => x.Id == request.Id && x.Region == request.Region && x.SolarSystem == request.SolarSystem);
                if (pendingRequest != null)
                    return;

                PendingRequests.Add(request);

                if (PendingRequests.Count(x => x.Region == request.Region && x.SolarSystem == request.SolarSystem) >= 200)
                    Request(request.Region, request.SolarSystem);
            }

            ProcessPendingRequests();
        }

        internal static void ProcessPendingRequests()
        {
            var done = false;
            while (!done)
            {
                done = true;
                var pendingRequest = PendingRequests.FirstOrDefault();
                if (pendingRequest != null)
                {
                    Request(pendingRequest.Region, pendingRequest.SolarSystem);
                    done = false;
                }
            }
        }
    }

    public class RawExec_api
    {
        [XmlElement]
        public RawMarketStat marketstat { get; set; }
    }

    public class RawMarketStat
    {
        [XmlElement("type")]
        public List<RawMarketType> type { get; set; }
    }

    public class RawMarketType
    {
        [XmlAttribute]
        public string id { get; set; }
        [XmlElement]
        public RawMarketItemData buy { get; set; }
        [XmlElement]
        public RawMarketItemData sell { get; set; }
    }

    public class RawMarketItemData
    {
        [XmlElement]
        public double? volume { get; set; }
        [XmlElement]
        public double? avg { get; set; }
        [XmlElement]
        public double? stddev { get; set; }
        [XmlElement]
        public double? median { get; set; }
        [XmlElement]
        public double? percentile { get; set; }
        [XmlElement]
        public double? max { get; set; }
        [XmlElement]
        public double? min { get; set; }
    }
}
