using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EoiData.EoiClasses;
using EoiData.EsiDataClasses;

namespace EoiData.EoiDataClasses
{
    internal class EoiDataSolarSystem
    {
        private EsiDataSolarSystem _solarSystem;
        private EoiSolarSystem _eoiSolarSystem;

        public EoiDataSolarSystem(EsiDataSolarSystem solarSystem)
        {
            _solarSystem = solarSystem;
        }

        public EoiSolarSystem GetEoiSolarSystem()
        {
            if (_solarSystem == null)
                return null;

            if (_eoiSolarSystem != null)
                return _eoiSolarSystem;

            _eoiSolarSystem = new EoiSolarSystem();

            _eoiSolarSystem.Name = _solarSystem.Name;
            _eoiSolarSystem.Id = _solarSystem.Id;

            return _eoiSolarSystem;
        }
    }
}
