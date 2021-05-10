using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EoiData.StaticDataClasses
{
    public static class StaticDataInterface
    {
        private static List<StaticDataType> _types;
        private static List<StaticDataBlueprint> _blueprints;

        public static void Init()
        {
            _types = StaticDataReader.StaticDataReader.LoadTypes();
            _blueprints = StaticDataReader.StaticDataReader.LoadBlueprints();

            
        }

        internal static List<StaticDataType> GetTypes()
        {
            return _types.Where(x => x.Published && !x.Invalid).ToList();
        }

        //internal static StaticDataType GetTypeByName(string text)
        //{
        //    var type = _typeIds.Where(x => x.Value.name.ContainsKey("de") && x.Value.name["de"].Equals(text, StringComparison.InvariantCultureIgnoreCase));
        //    if (type.Count() == 1)
        //        return type.First().Value;

        //    return null;
        //}

        internal static StaticDataType GetTypeById(int id)
        {
            return _types.FirstOrDefault(x => x.Id == id && x.Published && !x.Invalid);
        }

        //internal static RawBlueprint GetBlueprintById(int typeId)
        //{
        //    RawBlueprint result;
        //    var success = _blueprints.TryGetValue(typeId, out result);
        //    if (success)
        //        return result;
        //    return null;
        //}

        internal static List<StaticDataBlueprint> GetBlueprints()
        {
            return _blueprints.Where(x => x.Published && !x.Invalid).ToList();
        }
    }
}
