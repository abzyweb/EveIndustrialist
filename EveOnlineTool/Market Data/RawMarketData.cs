using EveOnlineIndustrialist.Market_Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace EveOnlineIndustrialist.Market_Data
{
    public static class RawMarketData
    {
        //private static bool _allRequested = false;
        //private static bool _bulkRequest = true;

        //private static string _marketerApi = "https://api.evemarketer.com/ec/marketstat";
        //public static int RequestCapacity { get; set; }
        //public static TimeSpan RequestResetTimer { get; set; }
        //public static bool AllRequested {  get { return _allRequested; } }
        
        
        //public static bool BulkRequest { get { return _bulkRequest; }
        //    set
        //    {
        //        _bulkRequest = value;
        //    }
        //}
        //public static List<MarketDataRequest> PendingRequests { get; private set; } = new List<MarketDataRequest>();
        //public static List<MarketDataRequest> _cachedTypes { get; private set; } = new List<MarketDataRequest>();
        

        //public static async Task RequestAsync(int? typeId, string region, string solarSystem)
        //{
        //    using (var client = new HttpClient())
        //    {
        //        var values = new Dictionary<string, string>();
                
        //        if (BulkRequest)
        //        {
        //            var typeIds = string.Empty;
        //            var requests = PendingRequests.Where(x => x.Region == region && x.SolarSystem == solarSystem);
        //            foreach (var value in requests)
        //            {
        //                if (typeIds != string.Empty)
        //                    typeIds += ",";

        //                typeIds += value.MarketData.TypeId.ToString();
        //            }
        //            values.Add("typeid", typeIds);
        //        }
        //        else
        //        {
        //            values.Add("typeid", typeId.ToString());
        //        }

        //        if (region != null)
        //            values.Add("regionlimit", region);

        //        if (solarSystem != null)
        //            values.Add("usesystem", solarSystem);

        //        var content = new FormUrlEncodedContent(values);
                
        //        var result = await client.PostAsync(_marketerApi, content);

        //        var remainingRequests = result.Headers.FirstOrDefault(x => x.Key == "X-Ratelimit-Remaining");
        //        RequestCapacity = int.Parse(remainingRequests.Value.First());

        //        var remainingTime = result.Headers.FirstOrDefault(x => x.Key == "X-Ratelimit-Reset");
        //        var resetDateTime = new DateTime(1970, 1, 1).AddSeconds(Int32.Parse(remainingTime.Value.First()));
        //        RequestResetTimer = resetDateTime.Subtract((DateTime.UtcNow));
                

        //        string resultContent = await result.Content.ReadAsStringAsync();

        //        TextReader input = new StringReader(resultContent);

        //        XmlSerializer serializer = new XmlSerializer(typeof(RawExec_api), new XmlRootAttribute("exec_api"));

        //        var marketResponse = (RawExec_api)serializer.Deserialize(input);

        //        foreach (var type in marketResponse.marketstat.type)
        //        {
        //            var pendingRequests = PendingRequests.Where(x => x.Region == region && x.SolarSystem == solarSystem && x.MarketData.TypeId.ToString() == type.id).ToList();

        //            foreach (var pendingRequest in pendingRequests)
        //            {
        //                if (pendingRequest != null)
        //                {
        //                    pendingRequest.RawMarketType = type;
        //                    pendingRequest.MarketData.RawMarketData.Add(type);
        //                    pendingRequest.MarketData.RequestState = RequestState.Ready;
        //                    PendingRequests.Remove(pendingRequest);
        //                    pendingRequest.MarketData = null;
        //                    var cached = _cachedTypes.FirstOrDefault(x => x.RawMarketType.id == pendingRequest.RawMarketType.id && x.Region == pendingRequest.Region && x.SolarSystem == pendingRequest.SolarSystem);
        //                    if (cached == null)
        //                        _cachedTypes.Add(pendingRequest);
        //                }
        //            }
                    
        //        }
        //    }
        //}

        //internal static List<MarketDataRequest> GetMarketDataCache()
        //{
        //    return _cachedTypes;
        //}

        //internal static void SetMarketDataCache(List<MarketDataRequest> requests)
        //{
        //    _cachedTypes = requests;
        //}

        //internal static async Task RequestAllAsync()
        //{
        //    if (PendingRequests.Count == 0)
        //        return;

        //    MarketDataRequest request = PendingRequests.FirstOrDefault();

        //    do
        //    {
        //        await RequestAsync(null, request.Region, request.SolarSystem);

        //        request = PendingRequests.FirstOrDefault();

        //    } while (request != null);

        //    _allRequested = true;
        //}

        //internal static void ClearCache()
        //{
        //    _cachedTypes.Clear();
        //}

        //internal static async Task RequestAsync(MarketDataRequest marketDataRequest)
        //{

        //    if (marketDataRequest.MarketData.TypeId == null)
        //        return;

        //    var cachedType = _cachedTypes.FirstOrDefault(x => x.RawMarketType.id == marketDataRequest.MarketData.TypeId.ToString() && x.Region == marketDataRequest.Region && x.SolarSystem == marketDataRequest.SolarSystem);
        //    if (cachedType != null)
        //    {
        //        marketDataRequest.MarketData.RawMarketData.Add(cachedType.RawMarketType);
        //        marketDataRequest.MarketData.RequestState = RequestState.Ready;
        //        return;
        //    }

        //    marketDataRequest.MarketData.RequestState = RequestState.Pending;

        //    if (BulkRequest)
        //    {
        //        PendingRequests.Add(marketDataRequest);

        //        if (PendingRequests.Count(x => x.Region == marketDataRequest.Region && x.SolarSystem == marketDataRequest.SolarSystem) >= 200)
        //            await RequestAsync(null, marketDataRequest.Region, marketDataRequest.SolarSystem);
        //    }
        //    else
        //        await RequestAsync(marketDataRequest.MarketData.TypeId.Value, marketDataRequest.Region, marketDataRequest.SolarSystem);
        //}
    }
    

    //[XmlRoot]
    //public class RawExec_api
    //{
    //    [XmlElement]
    //    public RawMarketStat marketstat { get; set; }
    //}

    //public class RawMarketStat
    //{
    //    [XmlElement("type")]
    //    public List<RawMarketType> type { get; set; }
    //}

    //public class RawMarketType
    //{
    //    [XmlAttribute]
    //    public string id { get; set; }
    //    [XmlElement]
    //    public RawMarketItemData buy { get; set; }
    //    [XmlElement]
    //    public RawMarketItemData sell { get; set; }
    //}
    
    //public class RawMarketItemData
    //{
    //    [XmlElement]
    //    public double? volume { get; set; }
    //    [XmlElement]
    //    public double? avg { get; set; }
    //    [XmlElement]
    //    public double? stddev { get; set; }
    //    [XmlElement]
    //    public double? median { get; set; }
    //    [XmlElement]
    //    public double? percentile { get; set; }
    //    [XmlElement]
    //    public double? max { get; set; }
    //    [XmlElement]
    //    public double? min { get; set; }
    //}
}
