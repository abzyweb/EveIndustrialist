using EoiData.EsiDataClasses;
using EoiData.MarketerDataClasses;
using EoiData.Settings;
using EoiData.WebDataClasses;
using EveSwaggerConnection.ESI_Communication.Operations.Market;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace EoiData.FileSystemDataClasses
{
    class FileSystemDataReader
    {
        internal static string _applicationDirectory;
        internal static string _universeDataDirectory = "UniverseData";
        internal static string _personalDataDirectory = "PersonalData";
        internal static string _marketDataDirectory = "MarketData";

        static FileSystemDataReader()
        {
            _applicationDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "EveOnlineIndustrialist");

            if (!Directory.Exists(_applicationDirectory))
                Directory.CreateDirectory(_applicationDirectory);

            if (!Directory.Exists(Path.Combine(_applicationDirectory, _marketDataDirectory)))
                Directory.CreateDirectory(Path.Combine(_applicationDirectory, _marketDataDirectory));

            if (!Directory.Exists(Path.Combine(_applicationDirectory, _personalDataDirectory)))
                Directory.CreateDirectory(Path.Combine(_applicationDirectory, _personalDataDirectory));

            if (!Directory.Exists(Path.Combine(_applicationDirectory, _universeDataDirectory)))
                Directory.CreateDirectory(Path.Combine(_applicationDirectory, _universeDataDirectory));
        }

        internal static List<EsiDataMarketHistory> ImportMarketHistory()
        {
            var fileName = "MarketHistory.eoi";
            var importFile = Path.Combine(_applicationDirectory, _marketDataDirectory, fileName);

            if (File.Exists(importFile))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<EsiDataMarketHistory>));
                TextReader reader = new StreamReader(importFile);

                var marketDataHistory = serializer.Deserialize(reader) as List<EsiDataMarketHistory>;
                reader.Close();

                foreach (var history in marketDataHistory)
                {
                    history.PostImport();
                }

                return marketDataHistory;
            }

            return null;
        }

        internal static List<EsiDataMarketOrders> ImportMarketOrders()
        {
            var fileName = "MarketOrders.eoi";
            var importFile = Path.Combine(_applicationDirectory, _marketDataDirectory, fileName);

            if (File.Exists(importFile))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<EsiDataMarketOrders>));
                TextReader reader = new StreamReader(importFile);

                var marketDataOrders = serializer.Deserialize(reader) as List<EsiDataMarketOrders>;
                reader.Close();

                foreach (var orders in marketDataOrders)
                {
                    orders.PostImport();
                }

                return marketDataOrders;
            }

            return null;
        }

        internal static void ExportAccessToken(RawAccessTokenResponse tokenResponse)
        {
            var fileName = "AccessToken.eoi";
            var exportFile = Path.Combine(_applicationDirectory, _personalDataDirectory, fileName);

            XmlSerializer serializer = new XmlSerializer(typeof(RawAccessTokenResponse));
            TextWriter writer = new StreamWriter(exportFile);

            serializer.Serialize(writer, tokenResponse);
            writer.Close();
        }

        internal static void ExportMarketHistory(List<EsiDataMarketHistory> history)
        {
            var fileName = "MarketHistory.eoi";
            var exportFile = Path.Combine(_applicationDirectory, _marketDataDirectory, fileName);

            XmlSerializer serializer = new XmlSerializer(typeof(List<EsiDataMarketHistory>));
            TextWriter writer = new StreamWriter(exportFile);

            serializer.Serialize(writer, history);
            writer.Close();
        }

        internal static void ExportMarketOrders(List<EsiDataMarketOrders> orders)
        {
            var fileName = "MarketOrders.eoi";
            var exportFile = Path.Combine(_applicationDirectory, _marketDataDirectory, fileName);

            XmlSerializer serializer = new XmlSerializer(typeof(List<EsiDataMarketOrders>));
            TextWriter writer = new StreamWriter(exportFile);

            serializer.Serialize(writer, orders);
            writer.Close();
        }

        internal static List<MarketerDataRequest> ImportMarketData()
        {
            var fileName = "MarketData.eoi";
            var importFile = Path.Combine(_applicationDirectory, _marketDataDirectory, fileName);

            if (File.Exists(importFile))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<MarketerDataRequest>));
                TextReader reader = new StreamReader(importFile);

                var marketDataRequests = serializer.Deserialize(reader) as List<MarketerDataRequest>;
                reader.Close();

                return marketDataRequests;
            }

            return null;
        }

        internal static void ExportMarketData(List<MarketerDataRequest> marketData)
        {
            var fileName = "MarketData.eoi";
            var exportFile = Path.Combine(_applicationDirectory, _marketDataDirectory, fileName);

            XmlSerializer serializer = new XmlSerializer(typeof(List<MarketerDataRequest>));
            TextWriter writer = new StreamWriter(exportFile);

            serializer.Serialize(writer, marketData);
            writer.Close();
        }

        internal static void ImportGlobalSettings()
        {
            var fileName = "GlobalSettings.eoi";
            var importFile = Path.Combine(_applicationDirectory, _personalDataDirectory, fileName);

            if (File.Exists(importFile))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(GlobalSettings));
                TextReader reader = new StreamReader(importFile);

                var globalSettings = serializer.Deserialize(reader) as GlobalSettings;
                reader.Close();

                SettingsInterface.GlobalSettings = globalSettings;
            }
        }

        internal static void ExportGlobalSettings()
        {
            var fileName = "GlobalSettings.eoi";
            var exportFile = Path.Combine(_applicationDirectory, _personalDataDirectory, fileName);

            XmlSerializer serializer = new XmlSerializer(typeof(GlobalSettings));
            TextWriter writer = new StreamWriter(exportFile);

            serializer.Serialize(writer, SettingsInterface.GlobalSettings);
            writer.Close();
        }

        internal static List<FileSystemDataUser> ImportUsers()
        {
            var fileName = "Users.eoi";
            var importFile = Path.Combine(_applicationDirectory, _personalDataDirectory, fileName);

            if (File.Exists(importFile))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<FileSystemDataUser>));
                TextReader reader = new StreamReader(importFile);

                var users = serializer.Deserialize(reader) as List<FileSystemDataUser>;
                reader.Close();

                return users;
            }

            return null;
        }

        internal static void ExportUsers(List<FileSystemDataUser> users)
        {
            var fileName = "Users.eoi";
            var exportFile = Path.Combine(_applicationDirectory, _personalDataDirectory, fileName);

            XmlSerializer serializer = new XmlSerializer(typeof(List<FileSystemDataUser>));
            TextWriter writer = new StreamWriter(exportFile);

            serializer.Serialize(writer, users);
            writer.Close();

        }
        internal static List<FileSystemDataAsset> ImportAssets()
        {
            var fileName = "Assets.eoi";
            var importFile = Path.Combine(_applicationDirectory, _personalDataDirectory, fileName);

            if (File.Exists(importFile))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<FileSystemDataAsset>));
                TextReader reader = new StreamReader(importFile);

                var assets = serializer.Deserialize(reader) as List<FileSystemDataAsset>;
                reader.Close();

                return assets;
            }

            return null;
        }

        internal static void ExportAssets(List<FileSystemDataAsset> assets)
        {
            var fileName = "Assets.eoi";
            var exportFile = Path.Combine(_applicationDirectory, _personalDataDirectory, fileName);

            XmlSerializer serializer = new XmlSerializer(typeof(List<FileSystemDataAsset>));
            TextWriter writer = new StreamWriter(exportFile);

            serializer.Serialize(writer, assets);
            writer.Close();
        }
        
        internal static List<FileSystemDataBlueprint> ImportBlueprints()
        {
            var fileName = "Blueprints.eoi";
            var importFile = Path.Combine(_applicationDirectory, _personalDataDirectory, fileName);

            if (File.Exists(importFile))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<FileSystemDataBlueprint>));
                TextReader reader = new StreamReader(importFile);

                var blueprints = serializer.Deserialize(reader) as List<FileSystemDataBlueprint>;
                reader.Close();

                return blueprints;
            }

            return null;
        }

        internal static void ExportBlueprints(List<FileSystemDataBlueprint> blueprints)
        {
            var fileName = "Blueprints.eoi";
            var exportFile = Path.Combine(_applicationDirectory, _personalDataDirectory, fileName);

            XmlSerializer serializer = new XmlSerializer(typeof(List<FileSystemDataBlueprint>));
            TextWriter writer = new StreamWriter(exportFile);

            serializer.Serialize(writer, blueprints);
            writer.Close();

        }

        public static void ExportSolarSystems(List<EsiDataSolarSystem> solarSystems)
        {
            var fileName = "SolarSystems.eoi";
            var exportFile = Path.Combine(_applicationDirectory, _universeDataDirectory, fileName);

            XmlSerializer serializer = new XmlSerializer(typeof(List<EsiDataSolarSystem>));
            using (TextWriter writer = new StreamWriter(exportFile))
            {
                serializer.Serialize(writer, solarSystems);
                writer.Close();
            }
        }

        public static List<EsiDataSolarSystem> ImportSolarSystems()
        {
            var fileName = "SolarSystems.eoi";
            var importFile = Path.Combine(_applicationDirectory, _universeDataDirectory, fileName);

            if (File.Exists(importFile))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<EsiDataSolarSystem>));
                TextReader reader = new StreamReader(importFile);

                var solarSystems = serializer.Deserialize(reader) as List<EsiDataSolarSystem>;
                reader.Close();

                return solarSystems;
            }

            return null;
        }
    }
}
