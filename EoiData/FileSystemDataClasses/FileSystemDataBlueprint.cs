using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EoiData.FileSystemDataClasses
{
    public class FileSystemDataBlueprint
    {
        public int Id { get; set; }
        public int MaterialEfficency { get; set; }
        public int TimeEfficency { get; set; }
        public bool Owned { get; set; }
        public bool Private { get; set; }
        public bool IsCopy { get; set; }
    }
}
