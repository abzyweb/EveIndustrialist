using EoiData.Constants;
using EoiData.EoiDataClasses;
using EoiData.Settings;
using EveSwaggerConnection;
using EveSwaggerConnection.ESI_Communication.Operations.Market;
using EveSwaggerConnection.ESI_Communication.Operations.Verify;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EoiData.EsiDataClasses
{
    public static class EsiDataInterface
    {
        private static List<EsiDataPrice> _prices;
        private static List<EsiDataSolarSystemCostIndices> _systemCostIndices;
        private static List<EsiDataMarketHistory> _marketHistory;
        private static List<EsiDataMarketOrders> _marketOrders;
        private static List<EsiDataBlueprint> _blueprints;
        private static List<EsiDataSolarSystem> _solarSystems;

        internal static void Init()
        {
            EveSwaggerInterface.Load();

            if (_solarSystems == null)
                EveSwaggerInterface.LoadSolarSystems();

            InitializePrices();
            InitializeSystemCostIndices();
            InitializeMarketHistory();
            InitializeMarketOrders();
            InitializeBlueprints();
            InititalizeSolarSystems();
        }

        private static void InititalizeSolarSystems()
        {
            if (_solarSystems == null)
                _solarSystems = new List<EsiDataSolarSystem>();

            foreach (var solarSystem in EveSwaggerInterface.SolarSystems)
            {
                _solarSystems.Add(new EsiDataSolarSystem(solarSystem));
            }
        }

        private static void InitializeBlueprints()
        {
            if (_blueprints == null)
                _blueprints = new List<EsiDataBlueprint>();
        }

        private static void InitializeMarketHistory()
        {
            if (_marketHistory == null)
                _marketHistory = new List<EsiDataMarketHistory>();
        }

        private static void InitializeMarketOrders()
        {
            if (_marketOrders == null)
                _marketOrders = new List<EsiDataMarketOrders>();
        }

        internal static RawEsiVerify Verify(string access_token)
        {
            return EveSwaggerInterface.Verify(access_token);
        }

        internal static void RequestMarketHistory()
        {
            if (EveSwaggerInterface.Working())
                return;

            var failedRequests = EveSwaggerInterface.GetFailedHistoryRequests();

            foreach (var failedRequest in failedRequests)
            {
                var esiType = EveSwaggerInterface.GetType(failedRequest.Id);
                if (esiType == null || !esiType.published)
                {
                    var product = EoiDataInterface.GetTypeById(failedRequest.Id);
                    if (product != null)
                    {
                        product.Invalid = true;
                        EoiDataInterface.CheckInvalid();
                    }
                }
            }

            EveSwaggerInterface.ClearFailedHistoryRequests();

            var blueprints = EoiDataInterface.GetBlueprints();

            foreach (var blueprint in blueprints)
            {
                foreach (var product in blueprint.Products)
                {
                    RawEsiPriceHistory marketHistory = null;

                    var region = Regions.GetTradehubRegionId(SolarSystems.GetSolarSystem(SettingsInterface.GlobalSettings.TradeHub));

                    marketHistory = EveSwaggerInterface.GetMarketHistory(product.Id, region);
                    if (marketHistory != null)
                    {
                        var esiDataMarketHistory = _marketHistory.FirstOrDefault(x => Equals(x.History, marketHistory));
                        if (esiDataMarketHistory == null)
                        {
                            var oldEsiDataMarketHistory = _marketHistory.FirstOrDefault(x => x.Id == marketHistory.typeId && x.Region == marketHistory.regionId);
                            if (oldEsiDataMarketHistory != null)
                                _marketHistory.Remove(oldEsiDataMarketHistory);

                            _marketHistory.Add(new EsiDataMarketHistory(marketHistory));
                        }
                    }
                }
            }
        }

        internal static void RequestMarketOrders()
        {
            if (EveSwaggerInterface.Working())
                return;

            var failedRequests = EveSwaggerInterface.GetFailedOrdersRequests();

            foreach (var failedRequest in failedRequests)
            {
                var esiType = EveSwaggerInterface.GetType(failedRequest.Id);
                if (esiType == null || !esiType.published)
                {
                    var type = EoiDataInterface.GetTypeById(failedRequest.Id);
                    if (type != null)
                    {
                        type.Invalid = true;
                        EoiDataInterface.CheckInvalid();
                    }
                }
            }

            EveSwaggerInterface.ClearFailedOrdersRequests();

            var assets = EoiDataInterface.GetAssets();
            foreach (var asset in assets)
            {
                RawEsiMarketOrders marketOrders = null;

                var region = Regions.GetTradehubRegionId(SolarSystems.GetSolarSystem(SettingsInterface.GlobalSettings.TradeHub));

                marketOrders = EveSwaggerInterface.GetMarketOrders(asset.Id, region);
                if (marketOrders != null)
                {
                    var esiDataMarketOrders = _marketOrders.FirstOrDefault(x => Equals(x.Orders, marketOrders));
                    if (esiDataMarketOrders == null)
                    {
                        var oldEsiDataMarketOrders = _marketOrders.FirstOrDefault(x => x.Id == marketOrders.typeId && x.Region == marketOrders.regionId);
                        if (oldEsiDataMarketOrders != null)
                            _marketOrders.Remove(oldEsiDataMarketOrders);

                        _marketOrders.Add(new EsiDataMarketOrders(marketOrders));
                    }
                }
            }

            var blueprints = EoiDataInterface.GetBlueprints();

            foreach (var blueprint in blueprints)
            {
                {
                    RawEsiMarketOrders marketOrders = null;

                    var region = Regions.GetTradehubRegionId(SolarSystems.GetSolarSystem(SettingsInterface.GlobalSettings.TradeHub));

                    marketOrders = EveSwaggerInterface.GetMarketOrders(blueprint.Id, region);
                    if (marketOrders != null)
                    {
                        var esiDataMarketOrders = _marketOrders.FirstOrDefault(x => Equals(x.Orders, marketOrders));
                        if (esiDataMarketOrders == null)
                        {
                            var oldEsiDataMarketOrders = _marketOrders.FirstOrDefault(x => x.Id == marketOrders.typeId && x.Region == marketOrders.regionId);
                            if (oldEsiDataMarketOrders != null)
                                _marketOrders.Remove(oldEsiDataMarketOrders);

                            _marketOrders.Add(new EsiDataMarketOrders(marketOrders));
                        }
                    }
                }

                foreach (var materials in blueprint.Materials)
                {
                    RawEsiMarketOrders marketOrders = null;

                    var region = Regions.GetTradehubRegionId(SolarSystems.GetSolarSystem(SettingsInterface.GlobalSettings.TradeHub));

                    marketOrders = EveSwaggerInterface.GetMarketOrders(materials.Id, region);
                    if (marketOrders != null)
                    {
                        var esiDataMarketOrders = _marketOrders.FirstOrDefault(x => Equals(x.Orders, marketOrders));
                        if (esiDataMarketOrders == null)
                        {
                            var oldEsiDataMarketOrders = _marketOrders.FirstOrDefault(x => x.Id == marketOrders.typeId && x.Region == marketOrders.regionId);
                            if (oldEsiDataMarketOrders != null)
                                _marketOrders.Remove(oldEsiDataMarketOrders);

                            _marketOrders.Add(new EsiDataMarketOrders(marketOrders));
                        }
                    }

                    if (region != Regions.TheForge)
                    {
                        marketOrders = EveSwaggerInterface.GetMarketOrders(materials.Id, Regions.TheForge);
                        if (marketOrders != null)
                        {
                            var esiDataMarketOrders = _marketOrders.FirstOrDefault(x => Equals(x.Orders, marketOrders));
                            if (esiDataMarketOrders == null)
                            {
                                var oldEsiDataMarketOrders = _marketOrders.FirstOrDefault(x => x.Id == marketOrders.typeId && x.Region == marketOrders.regionId);
                                if (oldEsiDataMarketOrders != null)
                                    _marketOrders.Remove(oldEsiDataMarketOrders);

                                _marketOrders.Add(new EsiDataMarketOrders(marketOrders));
                            }
                        }
                    }
                }

                foreach (var product in blueprint.Products)
                {
                    RawEsiMarketOrders marketOrders = null;

                    var region = Regions.GetTradehubRegionId(SolarSystems.GetSolarSystem(SettingsInterface.GlobalSettings.TradeHub));

                    marketOrders = EveSwaggerInterface.GetMarketOrders(product.Id, region);
                    if (marketOrders != null)
                    {
                        var esiDataMarketOrders = _marketOrders.FirstOrDefault(x => Equals(x.Orders, marketOrders));
                        if (esiDataMarketOrders == null)
                        {
                            var oldEsiDataMarketOrders = _marketOrders.FirstOrDefault(x => x.Id == marketOrders.typeId && x.Region == marketOrders.regionId);
                            if (oldEsiDataMarketOrders != null)
                                _marketOrders.Remove(oldEsiDataMarketOrders);

                            _marketOrders.Add(new EsiDataMarketOrders(marketOrders));
                        }
                    }

                    if (region != Regions.TheForge)
                    {
                        marketOrders = EveSwaggerInterface.GetMarketOrders(product.Id, Regions.TheForge);
                        if (marketOrders != null)
                        {
                            var esiDataMarketOrders = _marketOrders.FirstOrDefault(x => Equals(x.Orders, marketOrders));
                            if (esiDataMarketOrders == null)
                            {
                                var oldEsiDataMarketOrders = _marketOrders.FirstOrDefault(x => x.Id == marketOrders.typeId && x.Region == marketOrders.regionId);
                                if (oldEsiDataMarketOrders != null)
                                    _marketOrders.Remove(oldEsiDataMarketOrders);

                                _marketOrders.Add(new EsiDataMarketOrders(marketOrders));
                            }
                        }
                    }
                }
            }
        }

        internal static List<EsiDataBlueprint> GetBlueprints()
        {
            return _blueprints;
        }

        internal static void UpdateBlueprints(string token, int userId)
        {
            if (string.IsNullOrWhiteSpace(token) || userId == 0)
                return;

            var blueprints = EveSwaggerInterface.GetBlueprints(token, userId);
            if (blueprints == null)
                return;

            foreach (var blueprint in blueprints)
            {
                //if (!SettingsInterface.GlobalSettings.ShowBlueprintCopies)
                {
                    if (blueprint.runs != -1)
                        continue;
                }

                var esiDataBlueprint = _blueprints.FirstOrDefault(x => x.Id == blueprint.type_id);
                if (esiDataBlueprint != null)
                    _blueprints.Remove(esiDataBlueprint);

                _blueprints.Add(new EsiDataBlueprint(blueprint));
            }
        }

        internal static void ClearBlueprints()
        {
            _blueprints.Clear();
        }

        internal static bool Working()
        {
            return EveSwaggerInterface.Working();
        }

        internal static void ImportMarketHistory(List<EsiDataMarketHistory> history)
        {
            if (history == null)
                return;

            if (_marketHistory == null)
                _marketHistory = new List<EsiDataMarketHistory>();

            foreach (var h in history)
                _marketHistory.Add(h);

            EveSwaggerInterface.AddMarketHistory(history.Select(x => x.History).ToList());
        }

        internal static void ImportMarketOrders(List<EsiDataMarketOrders> orders)
        {
            if (orders == null)
                return;

            if (_marketOrders == null)
                _marketOrders = new List<EsiDataMarketOrders>();

            foreach (var h in orders)
                _marketOrders.Add(h);

            EveSwaggerInterface.AddMarketOrders(orders.Select(x => x.Orders).ToList());
        }

        private static void InitializeSystemCostIndices()
        {
            if (_systemCostIndices == null)
                _systemCostIndices = new List<EsiDataSolarSystemCostIndices>();

            foreach (var indices in EveSwaggerInterface.SolarSystemCostIndices)
            {
                _systemCostIndices.Add(new EsiDataSolarSystemCostIndices(indices));
            }
        }

        internal static List<EsiDataMarketHistory> ExportMarketHistory()
        {
            return _marketHistory;
        }

        internal static List<EsiDataMarketOrders> ExportMarketOrders()
        {
            return _marketOrders;
        }

        internal static EsiDataPrice GetPriceById(int id)
        {
            if (_prices == null)
                return null;

            return _prices.FirstOrDefault(x => x.Id == id);
        }

        private static void InitializePrices()
        {
            if (_prices == null)
                _prices = new List<EsiDataPrice>();

            if (EveSwaggerInterface.MarketPrices == null)
                return;

            foreach (var price in EveSwaggerInterface.MarketPrices)
            {
                _prices.Add(new EsiDataPrice(price));
            }
        }

        internal static EsiDataSolarSystemCostIndices GetSolarSystemCostIndices(int solarSystemId)
        {
            if (_systemCostIndices == null)
                return null;

            return _systemCostIndices.FirstOrDefault(x => x.SolarSystemId == solarSystemId);
        }
        
        internal static EsiDataMarketHistory GetMarketHistory(int id, string region)
        {
            if (_marketHistory == null)
                return null;

            
            var esiDataMarketHistory = _marketHistory.FirstOrDefault(x => x.Id == id && x.Region == region);

            return esiDataMarketHistory;
        }

        internal static EsiDataMarketOrders GetMarketOrders(long id, string region)
        {
            if (_marketOrders == null)
                return null;

            var esiDataMarketOrders = _marketOrders.FirstOrDefault(x => x.Id == id && x.Region == region);

            return esiDataMarketOrders;
        }

        internal static List<EsiDataSolarSystem> ExportSolarSystems()
        {
            return _solarSystems;
        }

        internal static void ImportSolarSystems(List<EsiDataSolarSystem> esiDataSolarSystems)
        {
            if (esiDataSolarSystems != null)
                _solarSystems = esiDataSolarSystems;
        }
    }
}
