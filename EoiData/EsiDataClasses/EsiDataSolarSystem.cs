using EveSwaggerConnection.ESI_Communication.Operations.Universe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EoiData.EsiDataClasses
{
    public class EsiDataSolarSystem
    {
        private RawEsiSolarSystem _solarSystem;

        public long Id { get; set; }
        public string Name { get; set; }

        public EsiDataSolarSystem()
        {

        }

        public EsiDataSolarSystem(RawEsiSolarSystem rawEsiSolarSystem)
        {
            _solarSystem = rawEsiSolarSystem;

            this.Id = rawEsiSolarSystem.system_id;
            this.Name = rawEsiSolarSystem.name;
        }

        
    }
}
