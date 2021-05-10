using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EoiData.Settings
{
    public static class SettingsInterface
    {
        public static GlobalSettings GlobalSettings { get; set; } = new GlobalSettings();
    }
}
