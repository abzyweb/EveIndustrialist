using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EoiData.Constants;
using EoiData.EoiClasses;
using EoiData.EsiDataClasses;
using EoiData.MarketerDataClasses;
using EoiData.Settings;
using EoiData.StaticDataClasses;

namespace EoiData.EoiDataClasses
{
    internal class EoiDataType
    {
        private EsiDataPrice _esiPrice;

        public decimal AdjustedPrice { get; private set; }

        private StaticDataType _staticType;

        private List<EoiType> _eoiTypes = new List<EoiType>();
        private EsiDataMarketHistory _esiMarketHistory;
        private List<EsiDataMarketOrders> _esiMarketOrders = new List<EsiDataMarketOrders>();

        public bool Invalid { get; set; }

        public int Id { get; set; }
        internal string Name
        {
            get
            {
                return _staticType.Name;
            }
        }

        public EoiDataType(StaticDataType staticType)
        {
            if (staticType == null)
            {
                Invalid = true;
                return;
            }

            _staticType = staticType;

            Id = staticType.Id;
        }

        internal bool CheckMarketHistory()
        {
            var updated = false;

            var region = Regions.GetTradehubRegionId(SolarSystems.GetSolarSystem(SettingsInterface.GlobalSettings.TradeHub));
            
            var esiMarketHistory = EsiDataInterface.GetMarketHistory(Id, region);
            if (esiMarketHistory == null)
                return updated;

            if (_esiMarketHistory == null)
            {
                _esiMarketHistory = esiMarketHistory;
                updated = true;
                return updated;
            }

            if (Equals(_esiMarketHistory, esiMarketHistory))
                return updated;

            _esiMarketHistory = esiMarketHistory;
            updated = true;
            OnMarketHistoryUpdated();

            return updated;
        }

        private void OnMarketHistoryUpdated()
        {
            
        }

        internal void Init()
        {
            _esiPrice = EsiDataInterface.GetPriceById(Id);
            if (_esiPrice != null)
                AdjustedPrice = _esiPrice.AdjustedPrice;
            else
            {
                Invalid = true;
            }

            var region = Regions.GetTradehubRegionId(SolarSystems.GetSolarSystem(SettingsInterface.GlobalSettings.TradeHub));

            _esiMarketHistory = EsiDataInterface.GetMarketHistory(Id, region);
            var esiMarketOrders = EsiDataInterface.GetMarketOrders(Id, region);
            if (esiMarketOrders != null)
                _esiMarketOrders.Add(esiMarketOrders);
        }

        internal EoiType GetEoiType(EoiType eoiType)
        {
            return _eoiTypes.FirstOrDefault(x => Equals(eoiType, x));
        }

        internal StaticDataType GetStaticType()
        {
            return _staticType;
        }

        internal void AddEoiType(EoiType type)
        {
            _eoiTypes.Add(type);
        }

        internal List<EsiDataMarketOrders> GetMarketPrices()
        {
            return _esiMarketOrders;
        }

        internal EsiDataMarketHistory GetMarketHistory()
        {
            return _esiMarketHistory;
        }

        internal bool CheckMarketOrders()
        {
            var updated = false;

            var region = Regions.GetTradehubRegionId(SolarSystems.GetSolarSystem(SettingsInterface.GlobalSettings.TradeHub));

            if (CheckMarketOrders(region))
                updated = true;

            if (region != Regions.TheForge && CheckMarketOrders(Regions.TheForge))
                updated = true;

            if (false)
            {
                var esiMarketOrders = EsiDataInterface.GetMarketOrders(Id, region);
                if (esiMarketOrders == null)
                    return updated;

                var oldEsiMarketOrders = _esiMarketOrders.FirstOrDefault(x => x.Id == Id && x.Region == region);
                if (oldEsiMarketOrders == null)
                {
                    _esiMarketOrders.Add(esiMarketOrders);
                    updated = true;
                    return updated;
                }

                if (Equals(oldEsiMarketOrders, esiMarketOrders))
                    return updated;

                _esiMarketOrders.Remove(oldEsiMarketOrders);
                _esiMarketOrders.Add(esiMarketOrders);

                updated = true;
            }

            return updated;
        }

        internal bool CheckMarketOrders(string region)
        {
            var updated = false;

            var esiMarketOrders = EsiDataInterface.GetMarketOrders(Id, region);
            if (esiMarketOrders == null)
                return updated;

            var oldEsiMarketOrders = _esiMarketOrders.FirstOrDefault(x => x.Id == Id && x.Region == region);
            if (oldEsiMarketOrders == null)
            {
                _esiMarketOrders.Add(esiMarketOrders);
                updated = true;
                return updated;
            }

            if (Equals(oldEsiMarketOrders, esiMarketOrders))
                return updated;

            _esiMarketOrders.Remove(oldEsiMarketOrders);
            _esiMarketOrders.Add(esiMarketOrders);

            updated = true;

            return updated;
        }

    }
}
