using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EoiData.Constants
{
    public static class ContractStates
    {
        public static string Accepted { get; set; } = "Angenommen";
        public static string Pending { get; set; } = "Aktiv";
        public static string Finish { get; set; } = "Fertig";
    }
}
