using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CorporationWebConnection.WebCommunication
{
    [XmlRoot("request")]
    public class WebRequest
    {
        [XmlAttribute("status")]
        public string Status { get; set; }
        [XmlAttribute("error")]
        public string Error { get; set; }
    }

    [XmlRoot("request")]
    public class WebBlueprintRequest : WebRequest
    {
        [XmlArray("blueprints")]
        [XmlArrayItem("blueprint")]
        public List<WebBlueprint> Blueprints { get; set; }
    }

    [XmlRoot("request")]
    public class WebContractRequest : WebRequest
    {
        [XmlArray("contracts")]
        [XmlArrayItem("contract")]
        public List<WebContract> Contracts { get; set; }
    }

    [XmlRoot("contract")]
    public class WebContract
    {
        [XmlAttribute("id")]
        public Int64 Id { get; set; }

        [XmlIgnore]
        public Int64? ParentId { get; set; }

        [XmlAttribute("parentId")]
        public string ParentIdAsText
        {
            get
            {
                return ParentId.ToString();
            }
            set
            {
                ParentId = !string.IsNullOrEmpty(value) ? Int64.Parse(value) : default(Int64?);
            }
        }

        [XmlAttribute("blueprintId")]
        public int BlueprintId { get; set; }

        [XmlAttribute("contractType")]
        public int ContractType { get; set; }

        [XmlAttribute("volume")]
        public int Volume { get; set; }

        [XmlAttribute("price")]
        public decimal Price { get; set; }

        [XmlAttribute("contractor")]
        public string Contractor { get; set; }

        [XmlAttribute("client")]
        public string Client { get; set; }

        [XmlAttribute("state")]
        public int State { get; set; }

        [XmlAttribute("blueprintIncluded")]
        public int BlueprintIncluded { get; set; }

        [XmlAttribute("materialIncluded")]
        public int MaterialIncluded { get; set; }

        [XmlAttribute("destination")]
        public int Destination { get; set; }

        [XmlAttribute("description")]
        public string Description { get; set; }

        [XmlAttribute("enablePartition")]
        public int EnablePartition { get; set; }
    }

    [XmlRoot("blueprint")]
    public class WebBlueprint
    {
        [XmlAttribute("id")]
        public int Id { get; set; }
    }

    public static class WebRequestStatus
    {
        public static string Ok { get; } = "ok";
        public static string Failed { get; } = "failed";
    }
}
