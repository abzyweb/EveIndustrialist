using EveOnlineTool.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveOnlineTool.Personal_Data
{
    public static class PersonalData
    {
        private static EoiBlueprints _blueprints;
        private static Inventory _inventory;
        private static EoiUsers _users;
        private static ApplicationSettings _applicationSettings;

        public static Inventory Inventory { get { return _inventory; } set { _inventory = value; OnInventoryChanged(value); } }
        public static EoiUsers Users { get { return _users; } set { _users = value; OnUsersChanged(value); } }
        public static ApplicationSettings ApplicationSettings { get { return _applicationSettings; } set { _applicationSettings = value; OnApplicationSettingsChanged(value); } }
        public static EoiBlueprints Blueprints { get { return _blueprints; } set { _blueprints = value; OnBlueprintsChanged(value); } }

        static PersonalData()
        {
            Inventory = new Inventory();
            Users = new EoiUsers();
            ApplicationSettings = new ApplicationSettings();
            Blueprints = new EoiBlueprints();
        }

        private static void OnInventoryChanged(Inventory inventory)
        {
            // Nothing here ...
        }

        private static void OnUsersChanged(EoiUsers value)
        {
            // Nothing here ...
        }

        private static void OnApplicationSettingsChanged(ApplicationSettings value)
        {
            // Nothing here ...
        }

        private static void OnBlueprintsChanged(EoiBlueprints value)
        {
            // Nothing here ...
        }
    }
}
