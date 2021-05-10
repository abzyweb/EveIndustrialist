using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EveSwaggerConnection.ESI_Communication.Operations.Industry;

namespace EoiData.EsiDataClasses
{
    internal class EsiDataSolarSystemCostIndices
    {
        private RawSolarSystemCostIndices _indices;

        public EsiDataSolarSystemCostIndices(RawSolarSystemCostIndices indices)
        {
            if (indices == null)
                return;

            _indices = indices;

            SolarSystemId = _indices.solar_system_id;
        }

        internal decimal GetManufacturingCostIndex()
        {
            if (_indices?.cost_indices?.FirstOrDefault(x => x.activity == "manufacturing") == null)
                return 0;

            return _indices.cost_indices.First(x => x.activity == "manufacturing").cost_index;
        }

        public int SolarSystemId { get; internal set; }
    }
}
