using EoiData.EoiDataClasses;
using EoiData.Helper;
using EoiData.WebDataClasses;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace EoiData.EoiClasses
{
    public static class EoiInterface
    {
        private static ObservableCollection<EoiBlueprint> _eoiBlueprints;
        private static ObservableCollection<EoiUser> _eoiUsers;
        private static ObservableCollection<EoiContract> _eoiContracts;
        private static ObservableCollection<EoiAsset> _eoiAssets;
        private static ObservableCollection<EoiSolarSystem> _eoiSolarSystems;

        public static void Init()
        {
            EoiDataInterface.Init();
            Initialize();
            EoiDataInterface.CalculateAllBlueprints();
        }

        private static void Initialize()
        {
            _eoiBlueprints = new ObservableCollection<EoiBlueprint>();

            var blueprints = EoiDataInterface.GetBlueprints();
            foreach (var blueprint in blueprints)
                _eoiBlueprints.Add(blueprint.GetEoiBlueprint());

            _eoiUsers = new ObservableCollection<EoiUser>();

            var users = EoiDataInterface.GetUsers();
            foreach (var user in users)
            {
                _eoiUsers.Add(user.GetEoiUser());
            }

            _eoiContracts = new ObservableCollection<EoiContract>();
            var contracts = EoiDataInterface.GetContracts();
            foreach (var contract in contracts)
            {
                _eoiContracts.Add(contract.GetEoiContract());
            }

            _eoiAssets = new ObservableCollection<EoiAsset>();
            var eoiDataAssets = EoiDataInterface.GetAssets();
            foreach (var eoiDataAsset in eoiDataAssets)
            {
                foreach (var eoiAsset in eoiDataAsset.GetEoiAssets())
                {
                    _eoiAssets.Add(eoiAsset);
                }
            }

            _eoiSolarSystems = new ObservableCollection<EoiSolarSystem>();
            var solarSystems = EoiDataInterface.GetSolarSystems();
            if (solarSystems != null)
            {
                foreach (var solarSystem in solarSystems)
                {
                    _eoiSolarSystems.Add(solarSystem.GetEoiSolarSystem());
                }
            }
        }

        public static void AssetPropertyChanged()
        {
            foreach (var asset in _eoiAssets)
            {
                asset.InvokePropertyChanged();
            }
        }

        public static void BlueprintPropertyChanged()
        {
            foreach (var blueprint in _eoiBlueprints)
            {
                blueprint.InvokePropertyChanged();
            }
        }

        public static ObservableCollection<EoiAsset> GetAssets()
        {
            return _eoiAssets;
        }

        public static void AddAsset(EoiAsset eoiAsset)
        {
            var dispatcher = Dispatcher.FromThread(BackgroundWorker.MainThread);
            if (dispatcher != null)
                dispatcher.Invoke(DispatcherPriority.Background, new Action(() => { _eoiAssets.Add(eoiAsset); }));
        }

        public static void RemoveAsset(EoiAsset eoiAsset)
        {
            var dispatcher = Dispatcher.FromThread(BackgroundWorker.MainThread);
            if (dispatcher != null)
                dispatcher.Invoke(DispatcherPriority.Background, new Action(() => { _eoiAssets.Remove(eoiAsset); }));
        }

        public static void InvokeDispatcherContractsPropertyChanged()
        {
            var dispatcher = Dispatcher.FromThread(BackgroundWorker.MainThread);
            if (dispatcher != null)
            {
                dispatcher.Invoke(DispatcherPriority.Background, new Action(ContractsPropertyChanged));
            }
        }

        public static void ContractsPropertyChanged()
        {
            foreach (var contract in _eoiContracts)
            {
                contract.InvokePropertyChanged();
            }
        }

        public static void Close()
        {
            EoiDataInterface.Close();
        }

        public static ObservableCollection<EoiBlueprint> GetBlueprints()
        {
            return _eoiBlueprints;
        }

        public static ObservableCollection<EoiContract> GetContracts()
        {
            return _eoiContracts;
        }

        public static ObservableCollection<EoiUser> GetUsers()
        {
            return _eoiUsers;
        }

        public static bool CreateIndustryContract(EoiBlueprint blueprint, int volume, decimal price, string orderType, bool materialIncluded, bool blueprintIncluded, string description, bool enablePartition)
        {
            var success = EoiDataInterface.CreateContract(blueprint.Id, volume, price, orderType, materialIncluded, blueprintIncluded, description, enablePartition);

            return success;
        }

        public static bool AcceptIndustryContract(EoiContract contract, int selectedVolume)
        {
            return EoiDataInterface.AcceptContract(contract, selectedVolume);
        }

        public static void StartBackgroundWorker(object mainThread)
        {
            if (mainThread is Thread)
                BackgroundWorker.Start(mainThread as Thread);
        }

        public static void StopBackgroundWorker()
        {
            BackgroundWorker.Stop();
        }

        internal static void AddUser(EoiUser eoiUser)
        {
            _eoiUsers.Add(eoiUser);
        }

        public static void CalculateAllBlueprints()
        {
            BackgroundWorker.CalculateAllBlueprints();
        }

        public static BackgroundWorkerStatus GetAutoUpdaterStatus()
        {
            return BackgroundWorker.Status;
        }

        public static bool IsVersionValid(string version)
        {
            if (string.IsNullOrWhiteSpace(version))
                return false;

            return WebDataInterface.IsVersionValid(version);
        }
        
        public static void InvokeDispatcherAddContract(EoiContract eoiContract)
        {
            var dispatcher = Dispatcher.FromThread(BackgroundWorker.MainThread);
            if (dispatcher != null)
            {
                dispatcher.Invoke(DispatcherPriority.Background, new Action(() => { AddContract(eoiContract); }));
            }
        }
        internal static void AddContract(EoiContract eoiContract)
        {
            _eoiContracts.Add(eoiContract);
        }

        internal static void RemoveContract(EoiContract eoiContract)
        {
            _eoiContracts.Remove(eoiContract);
        }

        public static void InvokeDispatcherRemoveContract(EoiContract eoiContract)
        {
            var dispatcher = Dispatcher.FromThread(BackgroundWorker.MainThread);
            if (dispatcher != null)
            {
                dispatcher.Invoke(DispatcherPriority.Background, new Action(() => { RemoveContract(eoiContract); }));
            }
        }

        public static void UpdateAllBlueprintEfficencies()
        {
            
        }
    }
}
