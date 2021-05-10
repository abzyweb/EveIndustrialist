using EoiData.EoiClasses;
using EoiData.EoiDataClasses;
using EoiData.EsiDataClasses;
using EoiData.MarketerDataClasses;
using EoiData.WebDataClasses;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace EoiData.FileSystemDataClasses
{
    internal static class FileSystemDataInterface
    {
        private static List<FileSystemDataBlueprint> _blueprints = new List<FileSystemDataBlueprint>();
        private static List<FileSystemDataUser> _users = new List<FileSystemDataUser>();
        private static List<FileSystemDataAsset> _assets = new List<FileSystemDataAsset>();

        internal static void Init()
        {
            FileSystemDataReader.ImportGlobalSettings();

            _users = FileSystemDataReader.ImportUsers();
            if (_users == null)
                _users = new List<FileSystemDataUser>();

            MarketerDataInterface.ImportMarketerDataRequests(FileSystemDataReader.ImportMarketData());

            var fileSystemBlueprints = FileSystemDataReader.ImportBlueprints();
            if (fileSystemBlueprints != null)
                _blueprints = fileSystemBlueprints;

            EsiDataInterface.ImportMarketHistory(FileSystemDataReader.ImportMarketHistory());

            EsiDataInterface.ImportMarketOrders(FileSystemDataReader.ImportMarketOrders());

            EsiDataInterface.ImportSolarSystems(FileSystemDataReader.ImportSolarSystems());

            _assets = FileSystemDataReader.ImportAssets();
            if (_assets == null)
                _assets = new List<FileSystemDataAsset>();

            if (!_users.Any(x => x.Name == "Default"))
            {
                CreateDefaultUser();
            }
        }

        internal static FileSystemDataAsset CreateAsset(long type_id, int quantity)
        {
            var asset = new FileSystemDataAsset();
            asset.TypeId = type_id;
            asset.Quantity = quantity;

            _assets.Add(asset);

            return asset;
        }

        internal static void RemoveAsset(FileSystemDataAsset fileSystemAsset)
        {
            _assets.Remove(fileSystemAsset);
        }

        internal static void CreateDefaultUser()
        {
            var user = new FileSystemDataUser("Default");
            _users.Add(user);
        }

        internal static void Close()
        {
            FileSystemDataReader.ExportGlobalSettings();
            FileSystemDataReader.ExportUsers(_users);
            FileSystemDataReader.ExportAssets(_assets);
            FileSystemDataReader.ExportMarketData(MarketerDataInterface.ExportMarketerDataRequests());
            FileSystemDataReader.ExportBlueprints(_blueprints);
            FileSystemDataReader.ExportMarketHistory(EsiDataInterface.ExportMarketHistory());
            FileSystemDataReader.ExportMarketOrders(EsiDataInterface.ExportMarketOrders());
            FileSystemDataReader.ExportSolarSystems(EsiDataInterface.ExportSolarSystems());
        }

        internal static FileSystemDataBlueprint GetBlueprintById(int id)
        {
            if (_blueprints == null)
                return null;

            return _blueprints.FirstOrDefault(x => x.Id == id);
        }

        internal static FileSystemDataBlueprint CreateBlueprint(int id)
        {
            if (_blueprints == null)
                _blueprints = new List<FileSystemDataBlueprint>();

            var blueprint = new FileSystemDataBlueprint();
            blueprint.Id = id;
            _blueprints.Add(blueprint);

            return blueprint;

        }

        internal static List<FileSystemDataUser> GetUsers()
        {
            return _users;
        }

        internal static void ExportUsers()
        {
            FileSystemDataReader.ExportUsers(_users);
        }

        internal static void RemoveBlueprint(FileSystemDataBlueprint fileSystemBlueprint)
        {
            _blueprints.Remove(fileSystemBlueprint);
        }

        internal static List<FileSystemDataAsset> GetAssets()
        {
            return _assets;
        }
    }
}
