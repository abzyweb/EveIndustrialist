using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveOnlineTool.Application_Data
{
    public static class OwnerFilter
    {
        public static List<string> List { get; } = new List<string>();

        public static string All { get; } = "Alle";
        public static string Buyable { get; } = "Kaufbar";
        public static string Owned { get; } = "Im Besitz";

        static OwnerFilter()
        {
            List.Add(All);
            List.Add(Buyable);
            List.Add(Owned);
        }

    }
}
