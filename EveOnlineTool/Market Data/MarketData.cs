using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using EveOnlineTool.Eve_Data;
using EveSwaggerConnection;
using System.Linq;
using EveSwaggerConnection.ESI_Communication;
using EveSwaggerConnection.ESI_Communication.Operations.Market;

namespace EveOnlineIndustrialist.Market_Data
{
    public class MarketData
    {
        //ObservableCollection<RawMarketType> _rawMarketData = new ObservableCollection<RawMarketType>();
        //public RequestState RequestState { get; set; }
        //public int? TypeId { get; set; } = null;
        //public ObservableCollection<RawMarketType> RawMarketData { get => _rawMarketData; set
        //    {
        //        _rawMarketData = value;

        //        RawMarketDataChanged(value);
        //    }
        //}

        //public bool Buyable { get; set; }
        //public double? Buy { get; private set; }
        //public long? BuyVolume { get; set; }
        //public bool Sellable { get; private set; }
        //public double? Sell { get; private set; }
        //public long? SellVolume { get; set; }
        //public string Region { get; set; }
        //public List<string> SolarSystem { get; set; } = new List<string>();
        //public RawEsiPriceHistory RawMarketHistoryData { get; private set; }

        //private void RawMarketDataChanged(ObservableCollection<RawMarketType> value)
        //{
            
        //}

        //internal void ClearMarketData()
        //{
        //    if (RawMarketData != null)
        //        this.RawMarketData.CollectionChanged -= RawMarketData_CollectionChanged;

        //    this.RawMarketData = new ObservableCollection<RawMarketType>();
        //    this.RawMarketData.CollectionChanged += RawMarketData_CollectionChanged;

        //    this.RequestState = RequestState.Unrequestet;
        //}

        //protected virtual void OnMarketDataChanged()
        //{
            
        //}

        //public MarketData()
        //{
        //    _rawMarketData = new ObservableCollection<RawMarketType>();

        //    this.RawMarketData.CollectionChanged += RawMarketData_CollectionChanged;
        //}

        //private void RawMarketData_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        //{
        //    if (RawMarketData.Count == 0)
        //    {
        //        Buyable = false;
        //        Buy = 0;
        //        BuyVolume = 0;

        //        Sellable = false;
        //        Sell = 0;
        //        SellVolume = 0;

        //        OnMarketDataChanged();

        //        return;
        //    }

        //    if (e.NewItems?.Count > 0)
        //    {
        //        foreach (RawMarketType marketData in e.NewItems)
        //        {
        //            if (marketData.sell.volume > 0)
        //            {
        //                Buyable = true;
        //                if (Buy == null || Buy > marketData.sell.min)
        //                    Buy = marketData.sell.min;
        //                BuyVolume = BuyVolume.GetValueOrDefault(0) + Convert.ToInt64(marketData.sell.volume);
        //            }

        //            if (marketData.buy.volume > 0)
        //            {
        //                Sellable = true;
        //                if (Sell == null || Sell < marketData.buy.max)
        //                    Sell = marketData.buy.max;
        //                SellVolume = SellVolume.GetValueOrDefault(0) + Convert.ToInt64(marketData.sell.volume);
        //            }
        //        }

        //        OnMarketDataChanged();
        //    }
        //}

        //public virtual async System.Threading.Tasks.Task RequestAsync()
        //{
        //    if (TypeId == null)
        //        return;

        //    if (this.SolarSystem.Count > 0)
        //    {
        //        foreach (var solarSystem in this.SolarSystem)
        //        {
        //            await EveOnlineIndustrialist.Market_Data.RawMarketData.RequestAsync(new MarketDataRequest(this, this.Region, solarSystem));
        //        }
        //    }
        //    else
        //        await EveOnlineIndustrialist.Market_Data.RawMarketData.RequestAsync(new MarketDataRequest(this, this.Region, null));

        //}

        //internal void RequestMarketHistory()
        //{
        //    if (this.TypeId == null || !EveSwaggerInterface.Types.Contains(this.TypeId.Value))
        //        return;

        //    if (this.RawMarketHistoryData != null)
        //        return;

        //    var marketHistoryRequest = new MarketHistory(Regions.Domain, this.TypeId.Value);
        //    marketHistoryRequest.Finished += MarketHistoryRequest_Finished;
        //    EveSwaggerConnection.EveSwaggerInterface.AddOperation(marketHistoryRequest);
        //}

        //private void MarketHistoryRequest_Finished(object sender, List<RawEsiPriceHistoryEntry> e)
        //{
        //    if (e == null)
        //        return;

        //    this.RawMarketHistoryData = e as RawEsiPriceHistory;
        //    EveSwaggerInterface.AddMarketHistory(this.RawMarketHistoryData);
        //    OnMarketHistoryDataChanged();
        //}

        //protected virtual void OnMarketHistoryDataChanged()
        //{
            
        //}

        //internal void ClearMarketHistory()
        //{
        //    this.RawMarketHistoryData = null;
        //}
    }
}