using CorporationWebConnection;
using CorporationWebConnection.Constants;
using CorporationWebConnection.WebCommunication.CorporationWebClasses;
using EoiData.Constants;
using EoiData.EoiClasses;
using EoiData.EoiDataClasses;
using EoiData.EsiDataClasses;
using EoiData.FileSystemDataClasses;
using EoiData.Helper;
using EoiData.MarketerDataClasses;
using EoiData.Settings;
using EoiData.StaticDataClasses;
using EoiData.WebDataClasses;
using EveSwaggerConnection;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EoiData.EoiDataClasses
{
    public static class EoiDataInterface
    {
        private static List<EoiDataBlueprint> _eoiDataBlueprints;
        private static List<EoiDataType> _eoiDataTypes;
        private static List<EoiDataUser> _eoiDataUsers;
        private static List<EoiDataContract> _eoiDataContracts;
        private static List<EoiDataAsset> _eoiDataAsset;
        private static List<EoiDataSolarSystem> _eoiSolarSystems;

        public static void Init()
        {
            StaticDataInterface.Init();
            FileSystemDataInterface.Init();

            InitializeEoiData();

            EsiDataInterface.Init();

            InitEoiDataContent();
        }

        internal static void AuthenticateUser(RawAccessTokenResponse token)
        {
            var verifyResult = EsiDataInterface.Verify(token.access_token);
            if (verifyResult != null)
            {
                var user = GetUser(verifyResult.CharacterName);
                if (user != null)
                    user.VerifyUser(verifyResult, token);
                else
                {
                    var defaultUser = GetDefaultUser();
                    if (defaultUser != null)
                    {
                        defaultUser.VerifyUser(verifyResult, token);
                        CreateDefaultUser();
                    }
                }
            }
        }

        

        internal static void Delete(EoiContract eoiContract)
        {
            var contract = _eoiDataContracts.FirstOrDefault(x => Equals(x.GetEoiContract(), eoiContract));
            if (contract != null)
            {
                var users = GetUsers();
                var user = users.FirstOrDefault(x => x.GetAccessToken() != null);
                if (user != null)
                {
                    var success = CorporationWebInterface.DeleteContract(user.Name, contract.Id);
                    if (success)
                        SynchronizeCorporationContracts();
                }
            }
        }

        internal static void Finish(EoiContract eoiContract)
        {
            var contract = _eoiDataContracts.FirstOrDefault(x => Equals(x.GetEoiContract(), eoiContract));
            if (contract != null)
            {
                var users = GetUsers();
                var user = users.FirstOrDefault(x => x.GetAccessToken() != null);
                if (user != null)
                {
                    var success = CorporationWebInterface.FinishContract(user.Name, contract.Id);
                    if (success)
                        SynchronizeCorporationContracts();
                }
            }
        }

        internal static bool CanFinish(EoiContract eoiContract)
        {
            var contract = _eoiDataContracts.FirstOrDefault(x => Equals(x.GetEoiContract(), eoiContract));
            if (contract != null)
            {
                var users = GetUsers();
                var user = users.FirstOrDefault(x => x.GetAccessToken() != null);
                if (user == null)
                    return false;

                if (eoiContract.Client == user.Name)
                    return false;

                return true;
            }

            return false;
        }

        internal static bool CanAccept(EoiContract eoiContract)
        {
            var contract = _eoiDataContracts.FirstOrDefault(x => Equals(x.GetEoiContract(), eoiContract));
            if (contract != null)
            {
                var users = GetUsers();
                var user = users.FirstOrDefault(x => x.GetAccessToken() != null);
                if (user == null)
                    return false;

                if (eoiContract.Contractor == user.Name)
                    return false;

                if (eoiContract.Client == user.Name)
                    return false;

                if (string.IsNullOrEmpty(eoiContract.Contractor) && !contract.GetEoiDataBlueprint().GetEoiBlueprint().Owned)
                    return false;

                return true;
            }

            return false;
        }

        internal static bool AcceptContract(EoiContract eoiContract, int selectedVolume)
        {
            var contract = _eoiDataContracts.FirstOrDefault(x => Equals(x.GetEoiContract(), eoiContract));
            if (contract != null)
            {
                var users = GetUsers();
                var user = users.FirstOrDefault(x => x.GetAccessToken() != null);
                if (user != null)
                {
                    var success = CorporationWebInterface.AcceptContract(user.Name, contract.Id, selectedVolume);
                    if (success)
                    {
                        SynchronizeCorporationContracts();
                        return true;
                    }
                }
            }

            return false;
        }

        

        internal static bool CanDelete(EoiContract eoiContract)
        {
            var contract = _eoiDataContracts.FirstOrDefault(x => Equals(x.GetEoiContract(), eoiContract));
            if (contract != null)
            {
                var users = GetUsers();
                var user = users.FirstOrDefault(x => x.GetAccessToken() != null);
                if (user == null)
                    return false;

                if (eoiContract.State == ContractStates.Pending)
                {
                    if (eoiContract.Contractor == user.Name)
                        return true;

                    if (eoiContract.Client == user.Name)
                        return true;
                }
                else if (eoiContract.State == ContractStates.Finish)
                    return false;
                else if (eoiContract.State == ContractStates.Accepted)
                {
                    if (eoiContract.Contractor == user.Name)
                        return true;
                }
                
            }

            return false;
        }

        private static void CreateDefaultUser()
        {
            FileSystemDataInterface.CreateDefaultUser();
            var user = FileSystemDataInterface.GetUsers().FirstOrDefault(x => x.Name == "Default");
            if (user != null)
            {
                var eoiDataUser = new EoiDataUser(user);
                _eoiDataUsers.Add(eoiDataUser);
                EoiInterface.AddUser(eoiDataUser.GetEoiUser());
            }
                
        }

        internal static EoiDataType GetTypeById(long id)
        {
            return _eoiDataTypes.FirstOrDefault(x => x.Id == id);
        }

        private static EoiDataUser GetDefaultUser()
        {
            return _eoiDataUsers.FirstOrDefault(x => x.Name == "Default");
        }

        private static EoiDataUser GetUser(string characterName)
        {
            return _eoiDataUsers.FirstOrDefault(x => x.Name == characterName);
        }

        internal static EoiDataUser GetUser(EoiUser user)
        {
            return _eoiDataUsers.FirstOrDefault(x => Equals(x.GetEoiUser(), user));
        }

        internal static bool CreateContract(int id, int volume, decimal price, string contractType, bool materialIncluded, bool blueprintIncluded, string description, bool enablePartition)
        {
            var users = GetUsers();
            var user = users.FirstOrDefault(x => x.GetAccessToken() != null);
            if (user == null)
                return false;

            var blueprint = _eoiDataBlueprints.FirstOrDefault(x => x.Id == id);
            if (blueprint == null)
                return false;

            var corporationWebContractType = CorporationWebContractTypes.Buy;
            if (contractType == ContractType.Sell)
                corporationWebContractType = CorporationWebContractTypes.Sell;

            var success = CorporationWebInterface.CreateContract(user.Name, id, volume, price, corporationWebContractType, materialIncluded, blueprintIncluded, description, enablePartition);
            if (success)
            {
                SynchronizeCorporationContracts();
                return true;
            }

            return false;
        }

        internal static List<EoiDataUser> GetUsers()
        {
            return _eoiDataUsers;
        }

        internal static List<EoiDataContract> GetContracts()
        {
            return _eoiDataContracts;
        }

        internal static List<EoiDataAsset> GetAssets()
        {
            return _eoiDataAsset;
        }

        internal static bool CheckMarketHistory()
        {
            if (EsiDataInterface.Working())
                return false;

            var updated = false;
            var blueprints = GetBlueprints();
            foreach (var blueprint in blueprints)
            {
                if (blueprint.CheckMarketHistory())
                    updated = true;
            }
            return updated;
        }

        internal static bool CheckMarketOrders()
        {
            if (EsiDataInterface.Working())
                return false;

            var updated = false;
            var blueprints = GetBlueprints();
            foreach (var blueprint in blueprints)
            {
                if (blueprint.CheckMarketOrders())
                    updated = true;
            }

            var assets = GetAssets();
            foreach (var asset in assets)
                asset.CheckMarketOrders();

            return updated;
        }

        private static void InitEoiDataContent()
        {
            var types = GetTypes();
            foreach (var type in types)
                type.Init();

            var blueprints = GetBlueprints();
            foreach (var blueprint in blueprints)
                blueprint.Init();

        }

        internal static void SynchronizeWallet()
        {
            var users = GetUsers();
            foreach (var user in users)
            {
                if (!user.SynchronizeTransactions)
                    continue;

                var token = user.GetAccessToken();
                if (token != null)
                {
                    // var balance = EveSwaggerInterface.GetWalletBalance(token, user.Id);
                    var transactions = EveSwaggerInterface.GetWalletTransactions(token, user.Id);

                    if (transactions == null)
                    {
                        user.SynchronizeTransactions = false;
                        continue;
                    }

                    foreach (var transaction in transactions)
                    {
                        if (!transaction.is_buy)
                            continue;

                        var eoiDataAsset = _eoiDataAsset.FirstOrDefault(x => x.Id == transaction.type_id);
                        if (eoiDataAsset != null)
                            eoiDataAsset.AddTransaction(transaction);
                    }
                }
            }

            var eoiDataAssets = _eoiDataAsset.ToList();
            foreach (var asset in eoiDataAssets)
            {
                asset.Clean();

                if (!asset.HasPrice())
                {
                    foreach (var eoiAsset in asset.GetEoiAssets())
                        EoiInterface.RemoveAsset(eoiAsset);

                    asset.Delete();
                    _eoiDataAsset.Remove(asset);
                }
                else
                {
                    asset.UpdateEoiAssets();

                    foreach (var eoiAsset in asset.GetEoiAssets())
                    {

                    }
                }
            }
        }

        internal static void SynchronizeCharacterOrders()
        {
            _eoiDataBlueprints.ForEach(x => x.ClearCharacterOrders());

            var users = GetUsers();
            foreach (var user in users)
            {
                var token = user.GetAccessToken();
                if (token != null)
                {
                    var esiCharacterOrders = EveSwaggerInterface.GetCharacterOrders(token, user.Id);
                    if (esiCharacterOrders == null)
                    {
                        continue;
                    }

                    foreach (var order in esiCharacterOrders)
                    {
                        if (order.region_id.ToString() != Regions.GetTradehubRegionId(SolarSystems.GetSolarSystem(SettingsInterface.GlobalSettings.TradeHub)))
                            continue;

                        var eoiDataBlueprint = GetBlueprintByProduct(order.type_id);
                        if (eoiDataBlueprint != null)
                        {
                            eoiDataBlueprint.AddCharacterOrder(order);
                        }
                    }
                }
            }
        }

        internal static void SynchronizeAssets()
        {
            foreach (var eoiDataAsset in _eoiDataAsset)
            {
                eoiDataAsset.Synchronized = false;
                eoiDataAsset.Reset();
            }

            var users = GetUsers();
            foreach (var user in users)
            {
                if (!user.SynchronizeAssets)
                    continue;

                var token = user.GetAccessToken();
                if (token != null)
                {
                    var esiAssets = EveSwaggerInterface.GetAssets(token, user.Id);
                    if (esiAssets == null)
                    {
                        user.SynchronizeAssets = false;
                        continue;
                    }

                    foreach (var esiAsset in esiAssets)
                    {
                        if (esiAsset.is_singleton)
                            continue;

                        if (esiAsset.location_flag != "Unlocked" && esiAsset.location_flag != "Locked" && esiAsset.location_flag != "Cargo" && esiAsset.location_flag != "Deliveries"
                            && esiAsset.location_flag != "Hangar" && esiAsset.location_flag != "HangarAll")
                            continue;
                        
                        if (esiAsset.is_blueprint_copy)
                            continue;

                        var eoiDataAsset = _eoiDataAsset.FirstOrDefault(x => x.Id == esiAsset.type_id);
                        if (eoiDataAsset == null)
                        {
                            eoiDataAsset = new EoiDataAsset(esiAsset);
                            _eoiDataAsset.Add(eoiDataAsset);
                        }
                        else
                        {
                            eoiDataAsset.Synchronize(esiAsset);
                        }

                        eoiDataAsset.Synchronized = true;
                    }
                }
            }

            var eoiDataAssetsToRemove = _eoiDataAsset.Where(x => x.Synchronized == false || x.GetQuantity() <= 0).ToList();
            foreach (var asset in eoiDataAssetsToRemove)
            {
                foreach (var eoiAsset in asset.GetEoiAssets())
                    EoiInterface.RemoveAsset(eoiAsset);

                asset.Delete();
                _eoiDataAsset.Remove(asset);
            }
        }

        internal static bool SynchronizeCorporationContracts()
        {
            var updated = false;

            var users = GetUsers();
            var user = users.FirstOrDefault(x => x.GetAccessToken() != null);
            if (user != null)
            {
                _eoiDataContracts.ForEach(x => x.Synchronized = false);
                var corpContracts = CorporationWebInterface.GetContracts(user.Name);

                foreach (var corpContract in corpContracts)
                {
                    EoiDataContract eoiDataContract = _eoiDataContracts.FirstOrDefault(x => x.Id == corpContract.Id);
                    if (eoiDataContract == null)
                    {
                        eoiDataContract = new EoiDataContract(corpContract);
                        eoiDataContract.Synchronized = true;
                        _eoiDataContracts.Add(eoiDataContract);
                        EoiInterface.InvokeDispatcherAddContract(eoiDataContract.GetEoiContract());
                        updated = true;
                    }
                    else
                    {
                        if (eoiDataContract.Synchronize(corpContract))
                            updated = true;
                    }
                }

                var notSynchronizedContracts = _eoiDataContracts.Where(x => !x.Synchronized).ToList();
                foreach (var contract in notSynchronizedContracts)
                {
                    EoiInterface.InvokeDispatcherRemoveContract(contract.GetEoiContract());
                    contract.Dispose();
                    _eoiDataContracts.Remove(contract);
                }
            }

            if (updated)
                EoiInterface.InvokeDispatcherContractsPropertyChanged();

            return updated;
        }

        internal static bool SynchronizeBlueprints()
        {
            var updated = false;

            if (SettingsInterface.GlobalSettings.EnableEsiBlueprintsUpdates)
            {
                BackgroundWorker.Status = new BackgroundWorkerStatus("Synchronisiere Eve Online Blueprints", -1);

                var users = GetUsers();

                if (users.Any(x => x.GetAccessToken() != null))
                {
                    EsiDataInterface.ClearBlueprints();
                    
                    foreach (var user in users)
                    {
                        var token = user.GetAccessToken();
                        if (token != null && user.Id != 0)
                            EsiDataInterface.UpdateBlueprints(token, user.Id);
                    }

                    var blueprints = EsiDataInterface.GetBlueprints();

                    foreach (var blueprint in _eoiDataBlueprints)
                    {
                        var esiDataBlueprint = blueprints.FirstOrDefault(x => x.Id == blueprint.Id);
                        if (esiDataBlueprint != null)
                        {
                            if (blueprint.Synchronize(esiDataBlueprint))
                                updated = true;
                        }
                        else
                        {
                            if (blueprint.SynchronizedReset())
                                updated = true;
                        }
                            
                    }

                    foreach (var blueprint in _eoiDataBlueprints)
                    {
                        blueprint.UpdateInventable();
                            
                    }
                }

                if (SettingsInterface.GlobalSettings.EnableCorporationBlueprintsUpdates)
                {
                    BackgroundWorker.Status = new BackgroundWorkerStatus("Synchronisiere Corporation Blueprints", -1);
                    var user = users.FirstOrDefault(x => x.GetAccessToken() != null);
                    if (user != null)
                    {
                        var eoiDataBlueprints = GetBlueprints();
                        foreach (var blueprint in eoiDataBlueprints)
                        {
                            var bp = blueprint.GetCorporationBlueprint();
                            if (bp != null)
                            {
                                var success = CorporationWebInterface.SetBlueprint(user.Name, bp);
                                if (!success)
                                {

                                }
                            }
                        }

                        var corpBlueprints = CorporationWebInterface.GetBlueprints(user.Name, user.Name);
                        foreach (var corpBp in corpBlueprints)
                        {
                            var eoiDataBlueprint = eoiDataBlueprints.FirstOrDefault(x => x.Id == corpBp.Id);
                            if (eoiDataBlueprint != null && eoiDataBlueprint.GetCorporationBlueprint() == null)
                            {
                                // Blueprint on Server that should not be there -> Delete
                                CorporationWebInterface.DeleteBlueprint(user.Name, corpBp.Id);
                            }
                        }

                        corpBlueprints = CorporationWebInterface.GetBlueprints(user.Name);
                        _eoiDataBlueprints.Where(x => x.CorporationOwned).ToList().ForEach(x => x.UnsetCorporationBlueprint());
                        foreach (var bp in corpBlueprints)
                        {
                            var eoiDataBlueprint = _eoiDataBlueprints.FirstOrDefault(x => x.Id == bp.Id);
                            if (eoiDataBlueprint != null)
                            {
                                if (eoiDataBlueprint.SetCorporationBlueprint(bp))
                                    updated = true;
                            }
                        }
                    }
                }
            }

            return updated;
        }

        internal static void CheckUserAccessTokens()
        {
            var users = GetUsers();
            foreach (var user in users)
            {
                user.CheckAccessToken();
            }
        }

        internal static List<EoiDataType> GetTypes()
        {
            return _eoiDataTypes.Where(x => !x.Invalid).ToList();
        }

        internal static List<EoiDataBlueprint> GetBlueprints()
        {
            var result = _eoiDataBlueprints.Where(x => !x.Invalid);
            //if (!SettingsInterface.GlobalSettings.ShowBlueprintCopies)
            //    result = result.Where(x => !x.IsCopy);
            return result.ToList();
        }

        private static void InitializeEoiData()
        {
            InitializeUsers();
            InitializeTypes();
            InitializeAssets();
            InitializeBlueprints();
            InitializeContracts();
        }

        internal static void CheckInvalid()
        {
            var blueprints = GetBlueprints();
            foreach (var blueprint in blueprints)
                blueprint.CheckInvalid();
        }

        private static void InitializeAssets()
        {
            _eoiDataAsset = new List<EoiDataAsset>();
            var assets = FileSystemDataInterface.GetAssets();
            foreach (var asset in assets)
            {
                _eoiDataAsset.Add(new EoiDataAsset(asset));
            }
        }

        private static void InitializeContracts()
        {
            _eoiDataContracts = new List<EoiDataContract>();
            var users = GetUsers();
            var user = users.FirstOrDefault(x => x.GetAccessToken() != null);
            if (user != null)
            {
                var contracts = CorporationWebInterface.GetContracts(user.Name);
                foreach (var contract in contracts)
                {
                    _eoiDataContracts.Add(new EoiDataContract(contract));
                }
            }
        }

        private static void InitializeUsers()
        {
            _eoiDataUsers = new List<EoiDataUser>();
            var users = FileSystemDataInterface.GetUsers();
            foreach (var user in users)
            {
                _eoiDataUsers.Add(new EoiDataUser(user));
            }
        }

        private static void InitializeTypes()
        {
            _eoiDataTypes = new List<EoiDataType>();
            var staticTypes = StaticDataInterface.GetTypes();
            foreach (var staticType in staticTypes)
            {
                _eoiDataTypes.Add(new EoiDataType(staticType));
            }
        }

        private static void InitializeBlueprints()
        {
            _eoiDataBlueprints = new List<EoiDataBlueprint>();
            var staticBlueprints = StaticDataInterface.GetBlueprints();
            foreach (var staticBlueprint in staticBlueprints)
            {
                _eoiDataBlueprints.Add(new EoiDataBlueprint(staticBlueprint));
            }

            foreach (var blueprint in _eoiDataBlueprints)
            {
                blueprint.InitializeInvention();
            }
        }

        internal static EoiDataType GetType(StaticDataType product)
        {
            return _eoiDataTypes.FirstOrDefault(x => Equals(product, x.GetStaticType()));
        }

        internal static void Close()
        {
            FileSystemDataInterface.Close();
        }

        internal static EoiDataBlueprint GetBlueprintByProduct(Int64 id)
        {
            return _eoiDataBlueprints.FirstOrDefault(x => x.Products.Any(y => y.Id == id));
        }
        internal static EoiDataBlueprint GetBlueprint(int id)
        {
            return _eoiDataBlueprints.FirstOrDefault(x => x.Id == id);
        }

        internal static void CalculateAllBlueprints()
        {
            foreach (var bp in GetBlueprints())
            {
                BlueprintCalculator.Calculate(bp);
            }
        }

        internal static List<EoiDataSolarSystem> GetSolarSystems()
        {
            return _eoiSolarSystems;
        }
    }
}
