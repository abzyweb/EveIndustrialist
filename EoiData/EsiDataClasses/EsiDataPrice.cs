using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EveSwaggerConnection.ESI_Communication.Operations.Market;

namespace EoiData.EsiDataClasses
{
    internal class EsiDataPrice
    {
        private RawEsiPrice _price;

        public EsiDataPrice(RawEsiPrice price)
        {
            if (price == null)
                return;

            _price = price;

            this.Id = _price.type_id;
            this.AdjustedPrice = Convert.ToDecimal(_price.adjusted_price);
            this.AveragePrice = Convert.ToDecimal(_price.average_price);
        }

        public int Id { get; internal set; }
        internal decimal AdjustedPrice { get; set; }
        internal decimal AveragePrice { get; set; }
    }
}
