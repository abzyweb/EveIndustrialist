using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveOnlineTool.Settings
{
    public static class FilterSettings
    {
        public static OwnerFilterSettings Owner { get; set; } = new OwnerFilterSettings();
    }

    public class OwnerFilterSettings
    {
        public bool Buyable { get; set; } = true;
        public bool Owned { get; set; } = true;
    }
}
