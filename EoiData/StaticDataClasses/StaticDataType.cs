using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EoiData.StaticDataClasses.StaticDataReader;

namespace EoiData.StaticDataClasses
{
    public class StaticDataType
    {
        private RawTypeId _type;

        public string Name
        {
            get
            {
                if (_type == null || !_type.name.ContainsKey("de"))
                    return string.Empty;

                return _type.name["de"];
            }
        }

        public StaticDataType(KeyValuePair<int, RawTypeId> data)
        {
            if (data.Value == null)
            {
                Invalid = true;
                return;
            }

            _type = data.Value;

            this.Id = _type.typeID;
            this.Published = _type.published;
        }

        public bool Invalid { get; internal set; }
        public bool Published { get; internal set; }
        internal int Id { get; set; }
    }
}
