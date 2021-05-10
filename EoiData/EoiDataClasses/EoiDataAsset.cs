using EoiData.Constants;
using EoiData.EoiClasses;
using EoiData.EsiDataClasses;
using EoiData.FileSystemDataClasses;
using EoiData.Settings;
using EveSwaggerConnection.ESI_Communication.Operations.Assets;
using EveSwaggerConnection.ESI_Communication.Operations.Wallet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EoiData.EoiDataClasses
{
    public class EoiDataAsset
    {
        private FileSystemDataAsset _fileSystemAsset;
        private EoiDataType _type;
        private List<EoiAsset> _eoiAssets = new List<EoiAsset>();
        private EsiDataMarketOrders _esiMarketOrders;

        public long Id { get; set; }

        public bool Synchronized { get; set; }
        

        public EoiDataAsset(FileSystemDataAsset fileSystemDataAsset)
        {
            if (fileSystemDataAsset == null)
                return;

            _fileSystemAsset = fileSystemDataAsset;

            this.Id = fileSystemDataAsset.TypeId;

            _type = EoiDataInterface.GetTypeById(this.Id);
        }

        public EoiDataAsset(RawEsiAsset esiAsset)
        {
            if (esiAsset != null)
            {
                this.Id = esiAsset.type_id;

                _type = EoiDataInterface.GetTypeById(this.Id);

                if (_fileSystemAsset == null)
                    _fileSystemAsset = FileSystemDataInterface.CreateAsset(esiAsset.type_id, esiAsset.quantity);
            }
        }

        internal void Synchronize(RawEsiAsset esiAsset)
        {
            if (esiAsset.type_id != this.Id)
                return;

            if (_fileSystemAsset == null)
                _fileSystemAsset = FileSystemDataInterface.CreateAsset(esiAsset.type_id, esiAsset.quantity);
            else
                _fileSystemAsset.Quantity += esiAsset.quantity;
        }

        internal void Reset()
        {
            _fileSystemAsset.Quantity = 0;
        }

        internal void AddTransaction(RawEsiTransaction transaction)
        {
            if (_fileSystemAsset != null)
                _fileSystemAsset.AddTransaction(transaction);
        }

        internal void Clean()
        {
            if (_fileSystemAsset != null)
                _fileSystemAsset.Clean();
        }

        internal List<EoiAsset> GetEoiAssets()
        {
            if (!_eoiAssets.Any())
                InitializeEoiAssets();

            return _eoiAssets;
        }

        internal void UpdateEoiAssets()
        {
            if (_fileSystemAsset == null)
                return;

            var transactions = _fileSystemAsset.GetOrderedTransactions();
            var quantity = _fileSystemAsset.Quantity;

            var usedEoiAssets = new List<EoiAsset>();

            _eoiAssets.ForEach(x => x.Quantity = 0);

            foreach (var transaction in transactions)
            {
                var eoiAsset = _eoiAssets.FirstOrDefault(x => x.Price == transaction.unit_price);

                if (eoiAsset == null)
                {
                    eoiAsset = new EoiAsset();

                    if (_type != null)
                    {
                        eoiAsset.Name = _type.Name;
                    }
                    else
                    {
                        eoiAsset.Name = "Unknown";
                    }

                    eoiAsset.Price = transaction.unit_price;

                    if (quantity > transaction.quantity)
                        eoiAsset.Quantity = transaction.quantity;
                    else if (quantity > 0)
                        eoiAsset.Quantity = quantity;

                    quantity -= transaction.quantity;

                    _eoiAssets.Add(eoiAsset);
                    EoiInterface.AddAsset(eoiAsset);
                }
                else
                {
                    if (quantity > transaction.quantity)
                        eoiAsset.Quantity += transaction.quantity;
                    else if (quantity > 0)
                        eoiAsset.Quantity += quantity;

                    quantity -= transaction.quantity;
                }

                usedEoiAssets.Add(eoiAsset);
            }

            if (quantity > 0)
            {
                var eoiAsset = _eoiAssets.FirstOrDefault(x => x.Price == 0);

                if (eoiAsset == null)
                {
                    eoiAsset = new EoiAsset();

                    if (_type != null)
                    {
                        eoiAsset.Name = _type.Name;
                    }

                    eoiAsset.Price = 0;
                    eoiAsset.Quantity = quantity;

                    _eoiAssets.Add(eoiAsset);
                }
                else
                {
                    eoiAsset.Quantity += quantity;
                }

                usedEoiAssets.Add(eoiAsset);
            }

            foreach (var eoiAsset in _eoiAssets.ToList())
            {
                if (!usedEoiAssets.Contains(eoiAsset))
                {
                    _eoiAssets.Remove(eoiAsset);
                    EoiInterface.RemoveAsset(eoiAsset);
                }
            }
        }

        private void InitializeEoiAssets()
        {
            if (_fileSystemAsset == null)
                return;

            var transactions = _fileSystemAsset.GetOrderedTransactions();
            var quantity = _fileSystemAsset.Quantity;

            foreach (var transaction in transactions)
            {
                var eoiAsset = _eoiAssets.FirstOrDefault(x => x.Price == transaction.unit_price);

                if (eoiAsset == null)
                {    
                    eoiAsset = new EoiAsset();

                    if (_type != null)
                    {
                        eoiAsset.Name = _type.Name;
                    }

                    eoiAsset.Price = transaction.unit_price;

                    if (quantity > transaction.quantity)
                        eoiAsset.Quantity = transaction.quantity;
                    else if (quantity > 0)
                        eoiAsset.Quantity = quantity;

                    quantity -= transaction.quantity;
                    
                    _eoiAssets.Add(eoiAsset);
                }
                else
                {
                    if (quantity > transaction.quantity)
                        eoiAsset.Quantity += transaction.quantity;
                    else if (quantity > 0)
                        eoiAsset.Quantity += quantity;

                    quantity -= transaction.quantity;
                }
            }

            if (quantity > 0)
            {
                var eoiAsset = _eoiAssets.FirstOrDefault(x => x.Price == 0);

                if (eoiAsset == null)
                {
                    eoiAsset = new EoiAsset();

                    if (_type != null)
                    {
                        eoiAsset.Name = _type.Name;
                    }

                    eoiAsset.Price = 0;
                    eoiAsset.Quantity = quantity;

                    _eoiAssets.Add(eoiAsset);
                }
                else
                {
                    eoiAsset.Quantity += quantity;
                }
            }
        }

        internal void Delete()
        {
            if (_fileSystemAsset != null)
                FileSystemDataInterface.RemoveAsset(_fileSystemAsset);
        }

        internal int GetQuantity()
        {
            if (_fileSystemAsset != null)
                return _fileSystemAsset.Quantity;

            return 0;
        }

        internal bool HasPrice()
        {
            if (_fileSystemAsset == null)
                return false;

            if (_fileSystemAsset.Transactions.Count == 0)
                return false;

            if (_fileSystemAsset.Transactions.All(x => x.unit_price == 0))
                return false;

            return true;
        }

        internal void CheckMarketOrders()
        {
            var region = Regions.GetTradehubRegionId(SolarSystems.GetSolarSystem(SettingsInterface.GlobalSettings.TradeHub));

            var esiMarketOrders = EsiDataInterface.GetMarketOrders(this.Id, region);
            if (esiMarketOrders == null)
                return;

            if (_esiMarketOrders == null)
            {
                _esiMarketOrders = esiMarketOrders;
            }
            else
            {
                //if (Equals(_esiMarketOrders, esiMarketOrders))
                //    return;

                _esiMarketOrders = esiMarketOrders;
            }

            decimal tradeHubPrice = 0;
            if (SettingsInterface.GlobalSettings.ExportOrderType == OrderType.Buy)
            {
                if (esiMarketOrders != null)
                {
                    var materialMarketPricesBuyOrders = esiMarketOrders.Orders.Where(x => x.is_buy_order && x.system_id.ToString() == SettingsInterface.GlobalSettings.TradeHub);
                    if (materialMarketPricesBuyOrders.Any())
                        tradeHubPrice = materialMarketPricesBuyOrders.Max(x => x.price);
                }
            }
            else if (SettingsInterface.GlobalSettings.ExportOrderType == OrderType.Sell)
            {
                if (esiMarketOrders != null)
                {
                    var materialMarketPricesBuyOrders = esiMarketOrders.Orders.Where(x => !x.is_buy_order && x.system_id.ToString() == SettingsInterface.GlobalSettings.TradeHub);
                    if (materialMarketPricesBuyOrders.Any())
                    {
                        tradeHubPrice = materialMarketPricesBuyOrders.Min(x => x.price);
                    }
                }
            }

            foreach (var eoiAsset in _eoiAssets)
            {
                eoiAsset.TradeHubPrice = tradeHubPrice;
                eoiAsset.PriceChange = eoiAsset.TradeHubPrice - eoiAsset.Price;
            }
        }
    }
}
