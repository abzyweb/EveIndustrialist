using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EoiData.Constants
{
    public static class Regions
    {
        public static string Domain { get; set; } = "10000043";
        public static string TheForge { get; set; } = "10000002";
        public static string Metropolis { get; set; } = "10000042";
        public static string Heimatar { get; set; } = "10000030";
        public static string SinqLaison { get; set; } = "10000032";
        public static string TashMurkon { get; set; } = "10000020";
        

        public static string GetTradehubRegionId(SolarSystem solarsystem)
        {
            string region = null;

            if (solarsystem == SolarSystems.Jita)
                region = Regions.TheForge;
            if (solarsystem == SolarSystems.Amarr)
                region = Regions.Domain;
            if (solarsystem == SolarSystems.Rens)
                region = Regions.Heimatar;
            if (solarsystem == SolarSystems.Dodixie)
                region = Regions.SinqLaison;
            if (solarsystem == SolarSystems.Hek)
                region = Regions.Metropolis;
            if (solarsystem == SolarSystems.Mani)
                region = Regions.TashMurkon;


            return region;
        }
    }
}
