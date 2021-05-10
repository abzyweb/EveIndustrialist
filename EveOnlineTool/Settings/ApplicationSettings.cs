using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveOnlineTool.Settings
{
    public class ApplicationSettings
    {
        public double SaleTax { get; set; } = 1.4;
        public double IndustryTax { get; set; } = 2;
        public string WebServer { get; set; }
        public double IndustryStructureBonus { get; set; } = 4;
    }
}
