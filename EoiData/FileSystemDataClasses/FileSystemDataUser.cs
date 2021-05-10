using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EoiData.WebDataClasses;

namespace EoiData.FileSystemDataClasses
{
    public class FileSystemDataUser
    {
        public RawAccessTokenResponse TokenResponse { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public int Id { get; set; }

        public FileSystemDataUser()
        {

        }

        public FileSystemDataUser(string name)
        {
            this.Name = name;
        }
    }
}
