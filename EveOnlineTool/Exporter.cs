using EveOnlineIndustrialist.Market_Data;
using EveOnlineTool;
using EveOnlineTool.Eve_Data;
using EveOnlineTool.Personal_Data;
using EveOnlineTool.Settings;
using EveSwaggerConnection;
using EveSwaggerConnection.ESI_Communication.Operations.Market;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace EveOnlineIndustrialist
{
    internal static class Exporter
    {
        //internal static string _applicationDirectory;
        //internal static string _personalDataDirectory = "PersonalData";
        //internal static string _marketDataDirectory = "MarketData";

        //static Exporter()
        //{
        //    _applicationDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "EveOnlineIndustrialist");

        //    if (!Directory.Exists(_applicationDirectory))
        //        Directory.CreateDirectory(_applicationDirectory);

        //    if (!Directory.Exists(Path.Combine(_applicationDirectory, _marketDataDirectory)))
        //        Directory.CreateDirectory(Path.Combine(_applicationDirectory, _marketDataDirectory));

        //    if (!Directory.Exists(Path.Combine(_applicationDirectory, _personalDataDirectory)))
        //        Directory.CreateDirectory(Path.Combine(_applicationDirectory, _personalDataDirectory));
        //}

        //internal static void ImportMarketHistory()
        //{
        //    var fileName = "MarketHistory.eoi";
        //    var importFile = Path.Combine(_applicationDirectory, _marketDataDirectory, fileName);

        //    if (File.Exists(importFile))
        //    {
        //        XmlSerializer serializer = new XmlSerializer(typeof(RawEsiPriceHistory));
        //        TextReader reader = new StreamReader(importFile);

        //        var marketDataHistory = serializer.Deserialize(reader) as RawEsiPriceHistory;
        //        reader.Close();

        //        EveSwaggerInterface.MarketHistory.Add(marketDataHistory);
        //    }
        //}

        //internal static void ExportMarketHistory()
        //{
        //    var fileName = "MarketHistory.eoi";
        //    var exportFile = Path.Combine(_applicationDirectory, _marketDataDirectory, fileName);

        //    XmlSerializer serializer = new XmlSerializer(typeof(List<RawEsiPriceHistory>));
        //    TextWriter writer = new StreamWriter(exportFile);

        //    serializer.Serialize(writer, EveSwaggerInterface.MarketHistory);
        //    writer.Close();
        //}

        //internal static void ImportMarketData()
        //{
        //    var fileName = "MarketData.eoi";
        //    var importFile = Path.Combine(_applicationDirectory, _marketDataDirectory, fileName);

        //    if (File.Exists(importFile))
        //    {
        //        XmlSerializer serializer = new XmlSerializer(typeof(List<MarketDataRequest>));
        //        TextReader reader = new StreamReader(importFile);

        //        var marketDataRequests = serializer.Deserialize(reader) as List<MarketDataRequest>;
        //        reader.Close();

        //        RawMarketData.SetMarketDataCache(marketDataRequests);
        //    }
        //}

        //internal static void ExportMarketData()
        //{
        //    var fileName = "MarketData.eoi";
        //    var exportFile = Path.Combine(_applicationDirectory, _marketDataDirectory, fileName);

        //    var cache = RawMarketData.GetMarketDataCache().Where(x => x.RawMarketType != null).ToList();

        //    XmlSerializer serializer = new XmlSerializer(typeof(List<MarketDataRequest>));
        //    TextWriter writer = new StreamWriter(exportFile);

        //    serializer.Serialize(writer, cache);
        //    writer.Close();
        //}

        //internal static void ImportApplicationSettings()
        //{
        //    var fileName = "ApplicationSettings.eoi";
        //    var importFile = Path.Combine(_applicationDirectory, _personalDataDirectory, fileName);

        //    if (File.Exists(importFile))
        //    {
        //        XmlSerializer serializer = new XmlSerializer(typeof(ApplicationSettings));
        //        TextReader reader = new StreamReader(importFile);

        //        var applicationSettings = serializer.Deserialize(reader) as ApplicationSettings;
        //        reader.Close();

        //        PersonalData.ApplicationSettings = applicationSettings;
        //    }
        //}

        //internal static void ExportApplicationSettings()
        //{
        //    var fileName = "ApplicationSettings.eoi";
        //    var exportFile = Path.Combine(_applicationDirectory, _personalDataDirectory, fileName);

        //    XmlSerializer serializer = new XmlSerializer(typeof(ApplicationSettings));
        //    TextWriter writer = new StreamWriter(exportFile);

        //    serializer.Serialize(writer, PersonalData.ApplicationSettings);
        //    writer.Close();
        //}

        //internal static void ImportUsers()
        //{
        //    var fileName = "Users.eoi";
        //    var importFile = Path.Combine(_applicationDirectory, _personalDataDirectory, fileName);

        //    if (File.Exists(importFile))
        //    {
        //        XmlSerializer serializer = new XmlSerializer(typeof(EoiUsers));
        //        TextReader reader = new StreamReader(importFile);

        //        var users = serializer.Deserialize(reader) as EoiUsers;
        //        reader.Close();

        //        PersonalData.Users = users;
        //    }
        //}

        //internal static void ExportUsers()
        //{
        //    var fileName = "Users.eoi";
        //    var exportFile = Path.Combine(_applicationDirectory, _personalDataDirectory, fileName);

        //    XmlSerializer serializer = new XmlSerializer(typeof(EoiUsers));
        //    TextWriter writer = new StreamWriter(exportFile);

        //    serializer.Serialize(writer, PersonalData.Users);
        //    writer.Close();

        //}

        //internal static void ImportInventory()
        //{
        //    var fileName = "Inventory.eoi";
        //    var importFile = Path.Combine(_applicationDirectory, _personalDataDirectory, fileName);

        //    XmlSerializer serializer = new XmlSerializer(typeof(Inventory));
        //    TextReader reader = new StreamReader(importFile);

        //    var inventory = serializer.Deserialize(reader) as Inventory;
        //    reader.Close();

        //    PersonalData.Inventory = inventory;
        //}

        //internal static void ExportInventory()
        //{
        //    var fileName = "Inventory.eoi";
        //    var exportFile = Path.Combine(_applicationDirectory, _personalDataDirectory, fileName);

        //    XmlSerializer serializer = new XmlSerializer(typeof(Inventory));
        //    TextWriter writer = new StreamWriter(exportFile);

        //    serializer.Serialize(writer, PersonalData.Inventory);
        //    writer.Close();

        //}

        //internal static void ImportBlueprintEfficency()
        //{
        //    var fileName = "Blueprints.eoi";
        //    var exportFile = Path.Combine(_applicationDirectory, _personalDataDirectory, fileName);

        //    XmlSerializer serializer = new XmlSerializer(typeof(XMLSettings));
        //    TextReader reader = new StreamReader(exportFile);

        //    var blueprintSettings = serializer.Deserialize(reader) as XMLSettings;
        //    reader.Close();

        //    foreach (var bpSetting in blueprintSettings.Blueprints)
        //    {
        //        var bp = PersonalData.Blueprints.GetByTypeId(bpSetting.Type_Id);
        //        if (bp != null)
        //        {
        //            bp.ME = bpSetting.ME;
        //            bp.TE = bpSetting.TE;
        //            bp.Owned = bpSetting.Owned;
        //        }
        //    }
            
        //}

        //internal static void ExportBlueprintEfficency()
        //{
        //    var blueprints = PersonalData.Blueprints.GetAll();
        //    var blueprintsWithEfficency = blueprints.Where(x => (x.ME != 0 || x.TE != 0 || x.Owned) && x.TypeId != null);

        //    // Dont't override File if we got nothing to Export? Maybe ME & TE was not loaded
        //    if (!blueprintsWithEfficency.Any())
        //        return;

        //    var xmlToExport = new XMLSettings();
        //    xmlToExport.Blueprints = new List<BlueprintXmlSettings>();
            
        //    foreach (var bp in blueprintsWithEfficency)
        //    {
        //        var bpSettings = new BlueprintXmlSettings();
        //        bpSettings.ME = bp.ME;
        //        bpSettings.TE = bp.TE;
        //        bpSettings.Owned = bp.Owned;
        //        bpSettings.Type_Id = bp.TypeId.Value;
        //        xmlToExport.Blueprints.Add(bpSettings);
        //    }
            
        //    var fileName = "Blueprints.eoi";
        //    var exportFile = Path.Combine(_applicationDirectory, _personalDataDirectory, fileName);

        //    XmlSerializer serializer = new XmlSerializer(typeof(XMLSettings));
        //    TextWriter writer = new StreamWriter(exportFile);

        //    serializer.Serialize(writer, xmlToExport);
        //    writer.Close();
            
        //}
    }

    //[XmlRoot]
    //public class XMLSettings
    //{
    //    [XmlElement]
    //    public List<BlueprintXmlSettings> Blueprints { get; set; }
    //}

    //public class BlueprintXmlSettings
    //{
    //    [XmlAttribute]
    //    public int ME { get; set; }
    //    [XmlAttribute]
    //    public int TE { get; set; }
    //    [XmlAttribute]
    //    public int Type_Id { get; set; }
    //    [XmlAttribute]
    //    public bool Owned { get; set; }
    //}
}
