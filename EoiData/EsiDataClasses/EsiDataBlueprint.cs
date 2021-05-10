using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EveSwaggerConnection.ESI_Communication.Operations.Characters;

namespace EoiData.EsiDataClasses
{
    internal class EsiDataBlueprint
    {
        private RawEsiBlueprint _blueprint;

        public EsiDataBlueprint(RawEsiBlueprint blueprint)
        {
            _blueprint = blueprint;

            if (_blueprint != null)
            {
                this.Id = _blueprint.type_id;
                this.MaterialEfficency = _blueprint.material_efficiency;
                this.TimeEfficency = blueprint.time_efficiency;
                this.IsCopy = blueprint.runs != -1;
            }
        }

        public int Id { get; set; }
        public int MaterialEfficency { get;  set; }
        public int TimeEfficency { get; set; }
        public bool IsCopy { get; set; }
    }
}
