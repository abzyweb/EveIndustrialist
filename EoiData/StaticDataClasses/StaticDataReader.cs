using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YamlDotNet.Serialization;

namespace EoiData.StaticDataClasses.StaticDataReader
{
    internal static class StaticDataReader
    {
        private static string _sdePath = "sde/fsd/";

        internal static List<StaticDataBlueprint> LoadBlueprints()
        {
            var assemblyLocation = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            var path = Path.Combine(assemblyLocation, _sdePath, "blueprints.yaml");

            var fileInfo = new FileInfo(path);

            if (!fileInfo.Exists)
                throw new InvalidOperationException("Failed to load static blueprints");

            var text = System.IO.File.ReadAllText(path);
            var input = new StringReader(text);

            var deserializer = new Deserializer();
            var rawData = deserializer.Deserialize<Dictionary<int, RawBlueprint>>(input);

            var result = new List<StaticDataBlueprint>();
            foreach (var data in rawData)
                result.Add(new StaticDataBlueprint(data));

            return result;
        }

        internal static List<StaticDataType> LoadTypes()
        {   
            var assemblyLocation = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            var path = Path.Combine(assemblyLocation, _sdePath, "typeIDs.yaml");

            var fileInfo = new FileInfo(path);

            if (!fileInfo.Exists)
                throw new InvalidOperationException("Failed to load static types");

            string text = File.ReadAllText(path);
            var input = new StringReader(text);

            var deserializer = new Deserializer();
            var rawData =  deserializer.Deserialize<Dictionary<int, RawTypeId>>(input);

            var result = new List<StaticDataType>();
            foreach (var data in rawData)
            {
                data.Value.typeID = data.Key;
                result.Add(new StaticDataType(data));
            }

            return result;
        }
    }

    public class RawTypeId
    {
        public int? groupID { get; set; }
        public double? mass { get; set; }
        public Dictionary<string, string> name { set; get; }
        public int portionSize { get; set; }
        public bool published { get; set; }
        public double? basePrice { get; set; }
        public Dictionary<string, string> description { set; get; }
        public int? iconID { get; set; }
        public int? marketGroupID { get; set; }
        public double? volume { get; set; }
        public string sofFactionName { get; set; }
        public int? sofMaterialSetID { get; set; }
        public double? capacity { get; set; }
        public Int64? factionID { get; set; }
        public int? graphicID { get; set; }
        public Dictionary<int, List<int>> masteries { set; get; }
        public int? raceID { get; set; }
        public double? radius { get; set; }
        public int? soundID { get; set; }
        public RawTraits traits { set; get; }
        public int typeID { get; internal set; }
    }

    public class RawTraits
    {
        public List<RawRoleBonus> miscBonuses { set; get; }
        public List<RawRoleBonus> roleBonuses { set; get; }
        public Dictionary<int, List<RawTraitType>> types { set; get; }
    }

    public class RawTraitType
    {
        public double? bonus { get; set; }
        public Dictionary<string, string> bonusText { set; get; }
        public int? importance { get; set; }
        public int? unitID { get; set; }
    }

    public class RawRoleBonus
    {
        public double? bonus { get; set; }
        public int? importance { get; set; }
        public int? unitID { get; set; }
        public Dictionary<string, string> bonusText { set; get; }
    }

    public class RawBlueprint
    {
        public int? blueprintTypeID { get; set; }
        public int? maxProductionLimit { get; set; }
        public RawActivity activities { get; set; }
    }

    public class RawActivity
    {
        public RawCopy copying { get; set; }
        public RawManufacture manufacturing { get; set; }
        public RawResearchMaterial research_material { get; set; }
        public RawResearchTime research_time { get; set; }
        public RawInvention invention { get; set; }
        public RawReaction reaction { get; set; }
    }
    public class RawReaction
    {
        public List<RawMaterial> materials { set; get; }
        public List<RawProduct> products { set; get; }
        public List<RawSkill> skills { set; get; }
        public int? time { get; set; }
    }
    public class RawInvention
    {
        public List<RawMaterial> materials { set; get; }
        public List<RawProduct> products { set; get; }
        public List<RawSkill> skills { set; get; }
        public int? time { get; set; }
    }

    public class RawSkill
    {
        public int? level { get; set; }
        public int? typeID { get; set; }
    }

    public class RawProduct : RawItem
    {
        public double? probability { get; set; }
        public int? quantity { get; set; }
    }
    public class RawMaterial : RawItem
    {
        public int? quantity { get; set; }
    }

    public class RawItem
    {
        public int? typeID { get; set; }
    }

    public class RawResearchTime
    {
        public List<RawProduct> products { set; get; }
        public List<RawSkill> skills { set; get; }
        public List<RawMaterial> materials { set; get; }
        public int? time { get; set; }
    }
    public class RawResearchMaterial
    {
        public List<RawProduct> products { set; get; }
        public List<RawSkill> skills { set; get; }
        public List<RawMaterial> materials { set; get; }
        public int? time { get; set; }
    }

    public class RawManufacture
    {
        public List<RawMaterial> materials { set; get; }
        public List<RawProduct> products { set; get; }
        public List<RawSkill> skills { set; get; }
        public int? time { get; set; }
    }

    public class RawCopy
    {
        public List<RawMaterial> materials { set; get; }
        public List<RawProduct> products { set; get; }
        public List<RawSkill> skills { set; get; }
        public int? time { get; set; }
    }
}
