using EveSwaggerConnection.ESI_Communication.Operations.Market;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace EoiData.EsiDataClasses
{
    public class EsiDataMarketOrders
    {
        public long Id { get; set; }
        public string Region { get; set; }
        public DateTime Timestamp { get; set; }
        public RawEsiMarketOrders Orders { get; set; }
        [XmlIgnore]
        public bool Invalid { get; }
       

        public EsiDataMarketOrders()
        {

        }

        public EsiDataMarketOrders(RawEsiMarketOrders orders)
        {
            if (orders == null)
            {
                this.Invalid = true;
                return;
            }

            Orders = orders;

            this.Id = orders.typeId;
            this.Region = orders.regionId;
            this.Timestamp = orders.Timestamp;
        }

        internal void PostImport()
        {
            if (Orders != null)
            {
                Orders.Timestamp = this.Timestamp;
                Orders.typeId = this.Id;
                Orders.regionId = this.Region;
            }
        }
    }
}
