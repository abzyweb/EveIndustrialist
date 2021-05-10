using System;
using EveOnlineIndustrialist.EveData;
using EveOnlineTool;
using EveOnlineTool.Eve_Data;
using EveOnlineTool.Personal_Data;
using EveOnlineTool.Settings;

namespace EveOnlineIndustrialist.Eve_Data
{
    public class Material : Market_Data.MarketData
    {
        //private RawMaterial _material;
        //private RawTypeId _typeID;

        //public int? Quantity { get; set; }
        //public bool Invalid { get; private set; }
        //public Blueprint Blueprint { get; private set; }
        //public RawMaterial RawMaterial { get => _material; set => _material = value; }
        //public MaterialSource Source { get; internal set; }

        //public Material(RawMaterial material) : base()
        //{
        //    if (material == null)
        //    {
        //        Invalid = true;
        //        return;
        //    }

        //    _material = material;
        //    _typeID = EveData.RawEveData.GetTypeIdById(material.typeID.Value);

        //    this.Quantity = material.quantity;

        //    this.TypeId = material.typeID;

        //    this.SolarSystem.Add(SolarSystems.Amarr);
        //}

        //internal void BuildBlueprintConnection()
        //{
        //    this.Blueprint = PersonalData.Blueprints.GetByProductId(this.TypeId);
        //}

        //public override string ToString()
        //{
        //    if (_typeID != null)
        //    {
        //        var result = Quantity.Value.ToString("N0") + "x " + _typeID.name["de"];
        //        if (Source != MaterialSource.NotSpcified)
        //            result += " | " + Source.ToString();
        //        if (Source == MaterialSource.Inventory)
        //        {
        //            var lowestPriceItem = PersonalData.Inventory.GetLowestPriceItemByTypeId(TypeId);
        //            if (lowestPriceItem != null)
        //            {
        //                result += " zu je " + lowestPriceItem.Price.ToString("N2");
        //            }
        //        }
        //        else if (Source == MaterialSource.Market)
        //        {
        //            if (CalculationSettings.ImportOrderType == OrderType.Buy)
        //            {
        //                if (Sell.HasValue)
        //                    result += " zu je " + this.Sell.Value.ToString("N2");
        //            }
        //            else if (CalculationSettings.ImportOrderType == OrderType.Sell)
        //            {
        //                if (Buy.HasValue)
        //                    result += " zu je " + this.Buy.Value.ToString("N2");
        //            }
                        
        //        }
                
                
        //        return result;
        //    }

        //    return _material.typeID + "x " + _typeID.name["de"];
        //}
    }
}