using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using EveSwaggerConnection.ESI_Communication.Operations.Market;

namespace EoiData.EsiDataClasses
{
    public class EsiDataMarketHistory
    {
        public int Id { get; set; }
        public string Region { get; set; }
        public DateTime Timestamp { get; set; }
        public RawEsiPriceHistory History { get; set;}
        [XmlIgnore]
        public bool Invalid { get; }
        [XmlIgnore]
        public decimal UnitsPerSecond { get; set; }

        public EsiDataMarketHistory()
        {

        }

        public EsiDataMarketHistory(RawEsiPriceHistory history)
        {
            if (history == null)
            {
                this.Invalid = true;
                return;
            }

            History = history;

            this.Id = history.typeId;
            this.Region = history.regionId;
            this.Timestamp = history.Timestamp;

            Initialize();
        }

        internal void PostImport()
        {
            if (History != null)
            {
                History.Timestamp = this.Timestamp;
                History.typeId = this.Id;
                History.regionId = this.Region;
            }

            Initialize();
        }

        private void Initialize()
        {
            decimal sellableUnitsPerSecond = 0;
            if (History.Count > 0)
            {
                decimal sellableUnitsPerDay = 0;
                foreach (var priceHistory in History)
                    sellableUnitsPerDay += priceHistory.volume;

                sellableUnitsPerDay = sellableUnitsPerDay / History.Count;

                sellableUnitsPerSecond = sellableUnitsPerDay / 51840;
            }
            this.UnitsPerSecond = sellableUnitsPerSecond;
        }
    }
}
