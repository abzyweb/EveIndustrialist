using CorporationWebConnection.WebCommunication;
using CorporationWebConnection.WebCommunication.CorporationWebClasses;
using EoiData.Constants;
using EoiData.EoiClasses;
using EoiData.EsiDataClasses;
using EoiData.FileSystemDataClasses;
using EoiData.Helper;
using EoiData.MarketerDataClasses;
using EoiData.Settings;
using EoiData.StaticDataClasses;
using EoiData.WebDataClasses;
using EveSwaggerConnection.ESI_Communication.Operations.Market;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EoiData.EoiDataClasses
{
    internal class EoiDataBlueprint
    {
        private StaticDataBlueprint _staticBlueprint;
        private EoiBlueprint _eoiBlueprint;
        private FileSystemDataBlueprint _fileSystemBlueprint;

        //private WebDataBlueprint _webBlueprint;


        private List<EsiDataMarketOrders> _esiMarketOrders = new List<EsiDataMarketOrders>();
        private List<RawEsiCharacterMarketOrderEntry> _characterOrders = new List<RawEsiCharacterMarketOrderEntry>();

        public List<EoiDataType> Products { get; set; }
        public List<EoiDataType> Materials { get; set; }
        public EoiDataBlueprint Parent {get; set;}

        public bool Invalid { get; set; }
        public int Id { get; set; }
        public bool EsiSynchronized { get; set; }
        public bool CorporationOwned { get; internal set; }
        public bool Owned { get; internal set; }
        public bool IsCopy { get; internal set; }

        public EoiDataBlueprint(StaticDataBlueprint staticBlueprint)
        {
            if (staticBlueprint == null)
            {
                Invalid = true;
                return;
            }
                
            _staticBlueprint = staticBlueprint;

            this.Id = staticBlueprint.Id;

            this.Products = new List<EoiDataType>();
            var products = _staticBlueprint.GetProducts();
            if (products == null)
            {
                this.Invalid = true;
                return;
            }
            foreach (var product in products)
            {
                var type = EoiDataInterface.GetType(product);
                if (type == null)
                {
                    this.Invalid = true;
                    return;
                }
                this.Products.Add(type);
            }
                

            this.Materials = new List<EoiDataType>();
            var materials = _staticBlueprint.GetMaterials();
            if (materials == null)
            {
                this.Invalid = true;
                return;
            }
            foreach (var material in materials)
            {
                var type = EoiDataInterface.GetType(material);
                if (type == null)
                {
                    this.Invalid = true;
                    return;
                }
                this.Materials.Add(type);
            }
        }

        internal void InitializeInvention()
        {
            var inventionProducts = _staticBlueprint.GetInventionProducts();
            if (inventionProducts != null)
            {
                foreach (var inventionProduct in inventionProducts)
                {
                    var blueprint = EoiDataInterface.GetBlueprint(inventionProduct.Id);
                    if (blueprint != null)
                    {
                        blueprint.Parent = this;
                    }
                }
            }
        }

        internal string GetName()
        {
            if (_staticBlueprint != null)
                return _staticBlueprint.Name;

            return string.Empty;
        }

        internal bool CheckMarketHistory()
        {
            var updated = false;
            foreach (var product in Products)
            {
                if (product.CheckMarketHistory())
                    updated = true;
            }

            if (updated)
                OnMarketHistoryUpdated();

            return updated;
        }

        internal bool CheckMarketOrders()
        {
            var updated = false;

            if (CheckMarketOrdersImpl())
                updated = true;
            
            foreach (var material in Materials)
            {
                if (material.CheckMarketOrders())
                    updated = true;
            }
            foreach (var product in Products)
            {
                if (product.CheckMarketOrders())
                    updated = true;
            }

            if (updated)
                OnMarketOrdersUpdated();

            return updated;
        }

        private bool CheckMarketOrdersImpl()
        {
            var updated = false;

            var region = Regions.GetTradehubRegionId(SolarSystems.GetSolarSystem(SettingsInterface.GlobalSettings.TradeHub));

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

        internal List<EsiDataMarketOrders> GetMarketPrices()
        {
            return _esiMarketOrders;
        }

        private void OnMarketOrdersUpdated()
        {
            var region = Regions.GetTradehubRegionId(SolarSystems.GetSolarSystem(SettingsInterface.GlobalSettings.TradeHub));

            if (_esiMarketOrders != null)
            {
                var esiMarketOrders = _esiMarketOrders.FirstOrDefault(x => x.Region == region);
                if (esiMarketOrders != null)
                {
                    if (esiMarketOrders.Orders.Any(x => !x.is_buy_order))
                    {
                        _eoiBlueprint.Price = esiMarketOrders.Orders.Where(x => !x.is_buy_order).Min(x => x.price);
                    }
                    else
                        _eoiBlueprint.Price = 0;
                }
                else
                    _eoiBlueprint.Price = 0;
            }
            else
                _eoiBlueprint.Price = 0;

            BlueprintCalculator.Calculate(this);
        }

        internal bool Synchronize(EsiDataBlueprint blueprint)
        {
            var updated = false;

            if (blueprint == null)
                return false;

            this.EsiSynchronized = true;

            if (_fileSystemBlueprint == null)
                _fileSystemBlueprint = FileSystemDataInterface.CreateBlueprint(this.Id);

            if (_fileSystemBlueprint.MaterialEfficency != blueprint.MaterialEfficency)
            {
                _fileSystemBlueprint.MaterialEfficency = blueprint.MaterialEfficency;

                if (_eoiBlueprint != null)
                    _eoiBlueprint.MaterialEfficency = blueprint.MaterialEfficency;

                updated = true;
            }

            if (_fileSystemBlueprint.TimeEfficency != blueprint.TimeEfficency)
            {
                _fileSystemBlueprint.TimeEfficency = blueprint.TimeEfficency;

                if (_eoiBlueprint != null)
                    _eoiBlueprint.TimeEfficency = blueprint.TimeEfficency;

                updated = true;
            }

            if (!this.Owned)
            {
                _fileSystemBlueprint.Owned = true;
                this.Owned = true;

                if (_eoiBlueprint != null)
                    _eoiBlueprint.Owned = true;

                updated = true;
            }

            return updated;
        }

        internal bool SetCorporationBlueprint(WebBlueprint bp)
        {
            var updated = false;

            if (!CorporationOwned)
            {
                this.CorporationOwned = true;

                if (_eoiBlueprint != null)
                {
                    _eoiBlueprint.CorporationOwned = true;
                }

                updated = true;
            }
            
            return updated;
        }

        internal CorporationWebBlueprint GetCorporationBlueprint()
        {
            if (_fileSystemBlueprint == null || !EsiSynchronized)
                return null;

            return new CorporationWebBlueprint(this.Id, _staticBlueprint.Name, _fileSystemBlueprint.MaterialEfficency, _fileSystemBlueprint.TimeEfficency, _fileSystemBlueprint.Private);
        }

        internal bool SynchronizedReset()
        {
            var updated = false;

            this.EsiSynchronized = true;
            this.Owned = false;

            if (_fileSystemBlueprint == null)
                return updated;
            else
                updated = true;

            FileSystemDataInterface.RemoveBlueprint(_fileSystemBlueprint);

            if (_eoiBlueprint != null)
            {
                _eoiBlueprint.MaterialEfficency = 0;
                _eoiBlueprint.TimeEfficency = 0;
                _eoiBlueprint.Owned = false;
            }

            return updated;
        }

        internal bool CheckInvalid()
        {
            foreach (var material in Materials)
            {
                if (material.Invalid)
                    this.Invalid = true;
            }

            foreach (var product in Products)
            {
                if (product.Invalid)
                    this.Invalid = true;
            }

            return Invalid;
        }

        private void OnMarketHistoryUpdated()
        {
            BlueprintCalculator.Calculate(this);
        }

        internal void UnsetCorporationBlueprint()
        {
            this.CorporationOwned = false;
            if (_eoiBlueprint != null)
                _eoiBlueprint.CorporationOwned = false;
        }

        internal StaticDataBlueprint GetStaticBlueprint()
        {
            return _staticBlueprint;
        }

        internal void Init()
        {
            var region = Regions.GetTradehubRegionId(SolarSystems.GetSolarSystem(SettingsInterface.GlobalSettings.TradeHub));

            var marketOrders = EsiDataInterface.GetMarketOrders(Id, region);
            if (marketOrders != null)
                _esiMarketOrders.Add(marketOrders);

            _fileSystemBlueprint = FileSystemDataInterface.GetBlueprintById(Id);

            if (_fileSystemBlueprint!= null && _fileSystemBlueprint.IsCopy)
                this.IsCopy = true;
        }

        

        internal EoiBlueprint GetEoiBlueprint()
        {
            if (_eoiBlueprint == null)
            {
                _eoiBlueprint = new EoiBlueprint();

                InitializeEoiBlueprint();
            }

            return _eoiBlueprint;
        }

        private void EoiBlueprint_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(EoiBlueprint.Private))
            {
                if (_fileSystemBlueprint == null)
                    _fileSystemBlueprint = FileSystemDataInterface.CreateBlueprint(this.Id);

                _fileSystemBlueprint.Private = _eoiBlueprint.Private;
            }
            else if (e.PropertyName == nameof(EoiBlueprint.Owned))
            {
                if (_fileSystemBlueprint == null)
                    _fileSystemBlueprint = FileSystemDataInterface.CreateBlueprint(this.Id);

                _fileSystemBlueprint.Owned = _eoiBlueprint.Owned;
                this.Owned = _eoiBlueprint.Owned;
            }
            else if (e.PropertyName == nameof(EoiBlueprint.MaterialEfficency))
            {
                if (_fileSystemBlueprint == null)
                    _fileSystemBlueprint = FileSystemDataInterface.CreateBlueprint(this.Id);

                _fileSystemBlueprint.MaterialEfficency = _eoiBlueprint.MaterialEfficency;

                BlueprintCalculator.Calculate(this);
            }
            else if (e.PropertyName == nameof(EoiBlueprint.TimeEfficency))
            {
                if (_fileSystemBlueprint == null)
                    _fileSystemBlueprint = FileSystemDataInterface.CreateBlueprint(this.Id);

                _fileSystemBlueprint.TimeEfficency = _eoiBlueprint.TimeEfficency;

                BlueprintCalculator.Calculate(this);
            }
        }

        internal void ClearCharacterOrders()
        {
            _characterOrders.Clear();

            if (_eoiBlueprint != null)
                _eoiBlueprint.HasCharacterOrders = false;
        }

        private void InitializeEoiBlueprint()
        {
            _eoiBlueprint.Id = this.Id;
            _eoiBlueprint.Name = _staticBlueprint.Name;
            _eoiBlueprint.Owned = this.Owned;
            _eoiBlueprint.Inventable = this.Parent?.Owned == true;

            _eoiBlueprint.Materials.Clear();
            foreach (var material in GetEoiMaterials())
                _eoiBlueprint.Materials.Add(material);
            
            _eoiBlueprint.Products = GetEoiProducts();

            if (_fileSystemBlueprint != null)
            {
                _eoiBlueprint.MaterialEfficency = _fileSystemBlueprint.MaterialEfficency;
                _eoiBlueprint.TimeEfficency = _fileSystemBlueprint.TimeEfficency;
                // _eoiBlueprint.Owned = _fileSystemBlueprint.Owned;
                _eoiBlueprint.Private = _fileSystemBlueprint.Private;
                _eoiBlueprint.IsCopy = _fileSystemBlueprint.IsCopy;
            }
            else
            {
                _eoiBlueprint.MaterialEfficency = SettingsInterface.GlobalSettings.NotOwnedMe;
                _eoiBlueprint.TimeEfficency = SettingsInterface.GlobalSettings.NotOwnedTe;
            }

            var region = Regions.GetTradehubRegionId(SolarSystems.GetSolarSystem(SettingsInterface.GlobalSettings.TradeHub));

            if (_esiMarketOrders != null)
            {
                var esiMarketOrders = _esiMarketOrders.FirstOrDefault(x => x.Region == region);
                if (esiMarketOrders != null)
                {
                    if (esiMarketOrders.Orders.Any(x => !x.is_buy_order))
                    {
                        _eoiBlueprint.Price = esiMarketOrders.Orders.Where(x => !x.is_buy_order).Min(x => x.price);
                    }
                    else
                        _eoiBlueprint.Price = 0;
                }
                else
                    _eoiBlueprint.Price = 0;
            }
            else
                _eoiBlueprint.Price = 0;

            _eoiBlueprint.PropertyChanged += EoiBlueprint_PropertyChanged;
        }

        internal void AddCharacterOrder(RawEsiCharacterMarketOrderEntry order)
        {
            _characterOrders.Add(order);

            if (_eoiBlueprint != null)
                _eoiBlueprint.HasCharacterOrders = true;
        }

        private List<EoiType> GetEoiProducts()
        {
            var result = new List<EoiType>();
            foreach (var product in Products)
            {
                var eoiType = new EoiType();

                eoiType.Id = product.Id;
                eoiType.Name = product.Name;
                eoiType.Quantity = GetProductQuantity(product);

                product.AddEoiType(eoiType);

                result.Add(eoiType);
            }

            return result;
        }

        private List<EoiType> GetEoiMaterials()
        {
            var result = new List<EoiType>();
            foreach (var material in Materials)
            {
                var eoiType = new EoiType();

                eoiType.Id = material.Id;
                eoiType.Name = material.Name;
                eoiType.Quantity = GetCalculatedMaterialQuantity(1, material);

                material.AddEoiType(eoiType);

                result.Add(eoiType);
            }

            return result;
        }

        internal int GetProductQuantity(EoiDataType product)
        {
            return _staticBlueprint.GetProductQuantity(product.GetStaticType());
        }

        internal int GetCalculatedMaterialQuantity(int runs, EoiDataType material)
        {
            //if (_fileSystemBlueprint == null)
            //    return _staticBlueprint.GetMaterialQuantity(material.GetStaticType());

            int result = 0;
            decimal baseQuantity = _staticBlueprint.GetMaterialQuantity(material.GetStaticType());
            int materialEfficeny = SettingsInterface.GlobalSettings.NotOwnedMe;
            if (_fileSystemBlueprint != null)
            {
                materialEfficeny = _fileSystemBlueprint.MaterialEfficency;
            }

            decimal materialModifier = Convert.ToDecimal(1 - (materialEfficeny * 0.01)) * Convert.ToDecimal(1 - (SettingsInterface.GlobalSettings.StructureMaterialBonus * 0.01m));
            result = Math.Max(runs, Convert.ToInt32(Math.Ceiling(Math.Round(runs * baseQuantity * materialModifier, 2))));
            return result;
        }

        internal int GetManufacturingTime()
        {
            if (_staticBlueprint == null)
                return 0;

            if (_fileSystemBlueprint != null)
                return Convert.ToInt32(Math.Ceiling(_staticBlueprint.ManufacturingTime * Convert.ToDecimal(1 - (_fileSystemBlueprint.TimeEfficency * 0.01))));

            // return _staticBlueprint.ManufacturingTime;
            return Convert.ToInt32(Math.Ceiling(_staticBlueprint.ManufacturingTime * Convert.ToDecimal(1 - (SettingsInterface.GlobalSettings.NotOwnedTe * 0.01))));
        }

        internal void UpdateInventable()
        {
            if (_eoiBlueprint != null)
                _eoiBlueprint.Inventable = this.Parent?.Owned == true;
        }
    }
}
