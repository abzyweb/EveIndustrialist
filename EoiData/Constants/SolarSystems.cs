using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EoiData.Constants
{
    public static class SolarSystems
    {

        public static SolarSystem Amarr { get; set; } = new SolarSystem(30002187, "Amarr");
        public static SolarSystem Jita { get; set; } = new SolarSystem(30000142, "Jita");
        public static SolarSystem Ashab { get; set; } = new SolarSystem(30003491, "Ashab");
        public static SolarSystem Ebo { get; set; } = new SolarSystem(30002269, "Ebo");
        public static SolarSystem Irnal { get; set; } = new SolarSystem(30003524, "Irnal");
        public static SolarSystem Miakie { get; set; } = new SolarSystem(30003539, "Miakie");
        public static SolarSystem Hek { get; set; } = new SolarSystem(30002053, "Hek");
        public static SolarSystem Dodixie { get; set; } = new SolarSystem(30002659, "Dodixie");
        public static SolarSystem Rens { get; set; } = new SolarSystem(30002510, "Rens");
        public static SolarSystem Mani { get; set; } = new SolarSystem(30001658, "Mani");
        

        public static List<SolarSystem> GetTradeHubs()
        {
            var result = new List<SolarSystem>();
            result.Add(SolarSystems.Amarr);
            result.Add(SolarSystems.Jita);
            result.Add(SolarSystems.Dodixie);
            result.Add(SolarSystems.Hek);
            result.Add(SolarSystems.Rens);
            result.Add(SolarSystems.Mani);
            return result;
        }

        public static List<SolarSystem> GetSolarSystems()
        {
            var result = new List<SolarSystem>();
            result.Add(SolarSystems.Amarr);
            result.Add(SolarSystems.Jita);
            result.Add(SolarSystems.Ashab);
            result.Add(SolarSystems.Ebo);
            result.Add(SolarSystems.Irnal);
            result.Add(SolarSystems.Miakie);
            result.Add(SolarSystems.Dodixie);
            result.Add(SolarSystems.Hek);
            result.Add(SolarSystems.Rens);
            result.Add(SolarSystems.Mani);
            return result;
        }

        public static SolarSystem GetSolarSystem(string solarSystemId)
        {
            return SolarSystems.GetSolarSystems().FirstOrDefault(x => x.SolarSysteId.ToString() == solarSystemId);
        }
    }

    public class SolarSystem
    {
        public SolarSystem(int id, string name)
        {
            this.SolarSysteId = id;
            this.SolarSystemName = name;
        }

        public int SolarSysteId { get; set; }
        public string SolarSystemName { get; set; }

        public override string ToString()
        {
            return SolarSystemName;
        }
    }
}
