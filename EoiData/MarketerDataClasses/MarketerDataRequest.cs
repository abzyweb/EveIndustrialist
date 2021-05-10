using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace EoiData.MarketerDataClasses
{
    public class MarketerDataRequest
    {
        [XmlAttribute("timestamp")]
        public DateTime Timestamp { get; set; }

        [XmlElement("marketData")]
        public RawMarketType RawMarketType { get; set; }

        [XmlAttribute("id")]
        public int Id { get; set; }

        [XmlAttribute("region")]
        public string Region { get; set; }

        [XmlAttribute("solarSystem")]
        public string SolarSystem { get; set; }

        public MarketerDataRequest(int id, string region, string solarSystem)
        {
            this.Timestamp = DateTime.Now;
            this.Id = id;
            this.Region = region;
            this.SolarSystem = solarSystem ;
        }

        public MarketerDataRequest()
        {

        }
    }
}
