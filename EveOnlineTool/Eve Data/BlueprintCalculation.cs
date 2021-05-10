using EveOnlineIndustrialist.Eve_Data;
using EveOnlineTool.Personal_Data;
using EveOnlineTool.Settings;
using EveSwaggerConnection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveOnlineTool.Eve_Data
{
    public class BlueprintCalculation
    {
        private Blueprint _blueprint;

        public double? ProfitPerHour { get; private set; }
        public double? ProfitPerUnit { get; private set; }
        public double? ProfitPerRun { get; private set; }
        public double? ExpectedProfitPerHour { get; private set; }

        public double? IncomePerHour { get; set; }
        public double? TaxesPerUnit { get; private set; }
        public double? IncomePerUnit { get; set; }
        public double? IncomePerRun { get; set; }

        public double? ExpensesPerHour { get; set; }
        public double? ExpensesPerUnit { get; set; }
        public double? ExpensesPerRun { get; set; }

        public double? Taxes { get; set; }

        public int? BreakEven { get; set; }

        public List<Material> CalculatedMaterials { get; set; }

        public BlueprintCalculation(Blueprint bp)
        {
            _blueprint = bp;

            this.CalculatedMaterials = new List<Material>();
        }

        public void UpdateCalculation()
        {
            //double? profit = 0d;
            //double? investment = 0d;
            //double? income = 0d;
            //double? taxes = 0d;

            //int manufacturingTime = _blueprint.ManufacturingTime;

            //this.ProfitPerHour = 0;
            //this.ExpensesPerHour = 0;
            //this.IncomePerHour = 0;

            //this.ProfitPerRun = 0;
            //this.ExpensesPerRun = 0;
            //this.IncomePerRun = 0;

            //this.ProfitPerUnit = 0;
            //this.ExpensesPerUnit = 0;
            //this.IncomePerUnit = 0;

            //this.ExpectedProfitPerHour = 0;

            //this.Taxes = 0;

            //this.CalculatedMaterials.Clear();

            //var solarSystemCostIndices = EveSwaggerInterface.GetSolarSystemCostIndices(SolarSystems.Ebo);
            //if (solarSystemCostIndices == null)
            //    return;

            //foreach (var material in _blueprint.Materials)
            //{
            //    double? productionCost = null;

            //    if (solarSystemCostIndices != null)
            //    {
            //        var esiMarketPrice = EveSwaggerInterface.GetMarketPrice(material.TypeId);
            //        if (esiMarketPrice != null)
            //        {
            //            taxes += esiMarketPrice.adjusted_price * material.Quantity;
            //        }
            //    }

            //    var buy = false;
            //    var inventory = false;
            //    if (material.Buyable)
            //    {
            //        if (CalculationSettings.ImportOrderType == OrderType.Buy)
            //            productionCost = material.Sell;
            //        else if (CalculationSettings.ImportOrderType == OrderType.Sell)
            //            productionCost = material.Buy;

            //        buy = true;
            //    }

            //    if (PersonalData.Inventory.Items.Any())
            //    {
            //        var lowestInventoryPriceItem = PersonalData.Inventory.GetLowestPriceItemByTypeId(material.TypeId);
            //        if (lowestInventoryPriceItem != null)
            //        {
            //            if (lowestInventoryPriceItem.Price <= productionCost)
            //            {
            //                productionCost = lowestInventoryPriceItem.Price;
            //                inventory = true;
            //            }
            //        }
            //    }

            //    // Check if Material can be produced
            //    var bp = material.Blueprint;
            //    if (bp != null && bp.Buyable)
            //    {
            //        bp.UpdateCalculation();

            //        if (productionCost >= bp.BlueprintCalculation.ExpensesPerUnit)
            //        {
            //            productionCost = bp.BlueprintCalculation.ExpensesPerUnit;
            //            taxes += bp.BlueprintCalculation.Taxes;

            //            buy = false;

            //            var materials = bp.BlueprintCalculation.CalculatedMaterials;

            //            foreach (var mat in materials)
            //            {
            //                var calcMat = this.CalculatedMaterials.FirstOrDefault(x => x.TypeId == mat.TypeId);
            //                if (calcMat == null)
            //                {
            //                    calcMat = new Material(mat.RawMaterial);
            //                    this.CalculatedMaterials.Add(calcMat);
            //                    calcMat.Quantity = mat.Quantity;
            //                    calcMat.Quantity = calcMat.Quantity * Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(material.Quantity * (1 - (_blueprint.ME * 0.01)))));
            //                    calcMat.Source = mat.Source;
                                
            //                }
            //                else
            //                {
            //                    calcMat.Quantity += (mat.Quantity * Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(material.Quantity * (1 - (_blueprint.ME * 0.01))))));
            //                }

            //                foreach (var md in mat.RawMarketData)
            //                    calcMat.RawMarketData.Add(md);
            //            }

            //            manufacturingTime += bp.ManufacturingTime * Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(material.Quantity * (1 - (_blueprint.ME * 0.01)))));
            //        }
            //    }

            //    if (buy || inventory)
            //    {
            //        var calcMat = this.CalculatedMaterials.FirstOrDefault(x => x.TypeId == material.TypeId);
            //        if (calcMat == null)
            //        {
            //            calcMat = new Material(material.RawMaterial);
            //            calcMat.Quantity = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(material.Quantity * (1 - (_blueprint.ME * 0.01)))));
            //            calcMat.Source = MaterialSource.Market;
            //            if (inventory)
            //                calcMat.Source = MaterialSource.Inventory;

            //            foreach (var md in material.RawMarketData)
            //                calcMat.RawMarketData.Add(md);
                        
            //            this.CalculatedMaterials.Add(calcMat);
            //        }
            //        else
            //        {
            //            calcMat.Quantity += Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(material.Quantity * (1 - (_blueprint.ME * 0.01)))));
            //        }
            //    }

            //    profit -= (productionCost * Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(material.Quantity * (1 - (_blueprint.ME * 0.01))))));
            //    investment += (productionCost * Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(material.Quantity * (1 - (_blueprint.ME * 0.01))))));


            //    this.ExpensesPerRun = investment;
            //}

            //if (solarSystemCostIndices != null)
            //{
            //    var indices = solarSystemCostIndices.cost_indices.FirstOrDefault(x => x.activity == "manufacturing");
            //    if (indices != null)
            //    {
            //        var solarSystemTax = Math.Round(Convert.ToDouble(Math.Round(taxes.Value) * Convert.ToDouble(indices.cost_index)));
            //        var structureBonus = Math.Floor(solarSystemTax * (PersonalData.ApplicationSettings.IndustryStructureBonus / 100));
            //        solarSystemTax -= structureBonus;
            //        this.Taxes = Math.Round(solarSystemTax + (solarSystemTax * (PersonalData.ApplicationSettings.IndustryTax / 100)));

            //        this.ExpensesPerRun += this.Taxes;
            //        investment += this.Taxes;
            //        profit -= this.Taxes;
            //    }
            //}

            //foreach (var product in _blueprint.Products)
            //{
            //    if (!product.Sellable)
            //        continue;

            //    if (CalculationSettings.ExportOrderType == OrderType.Buy)
            //    {
            //        profit += ((product.Sell * (1 - (PersonalData.ApplicationSettings.SaleTax / 100))) * product.Quantity);
            //        income += ((product.Sell * (1 - (PersonalData.ApplicationSettings.SaleTax / 100))) * product.Quantity);
            //    }
            //    else if (CalculationSettings.ExportOrderType == OrderType.Sell)
            //    {
            //        profit += ((product.Buy * (1 - (PersonalData.ApplicationSettings.SaleTax / 100))) * product.Quantity);
            //        income += ((product.Buy * (1 - (PersonalData.ApplicationSettings.SaleTax / 100))) * product.Quantity);
            //    }
                

            //    this.IncomePerRun = income;
            //}

            //this.ProfitPerRun = profit;
            //if (_blueprint.Products.Count == 1)
            //{
            //    this.TaxesPerUnit = (this.Taxes / _blueprint.Products[0].Quantity);
            //    this.ExpensesPerUnit = (investment / _blueprint.Products[0].Quantity);
            //    this.IncomePerUnit = (income / _blueprint.Products[0].Quantity);
            //    this.ProfitPerUnit = (profit / _blueprint.Products[0].Quantity);

            //    if (_blueprint.Manufacturing != null && _blueprint.Products[0].RawMarketHistoryData != null)
            //    {
            //        var manufacturingTimePerUnit = manufacturingTime / _blueprint.Products[0].Quantity;
            //        var sellableUnitsPerDay = 0d;
            //        foreach (var priceHistory in _blueprint.Products[0].RawMarketHistoryData)
            //        {
            //            if (sellableUnitsPerDay == 0)
            //                sellableUnitsPerDay = priceHistory.volume;
            //            else
            //                sellableUnitsPerDay = (sellableUnitsPerDay + priceHistory.volume) / 2;
            //        }

            //        var sellableUnitsPerSecond = sellableUnitsPerDay / 51840;
                    
            //        this.ExpectedProfitPerHour = ((ProfitPerUnit / (Convert.ToDouble(manufacturingTime) / _blueprint.Products[0].Quantity)) * sellableUnitsPerSecond) * 3600;
            //    }
            //}


            //if (_blueprint.Manufacturing != null)
            //{
            //    this.ProfitPerHour = (profit / manufacturingTime) * 3600;
            //    this.ExpensesPerHour = (investment / manufacturingTime) * 3600;
            //    this.IncomePerHour = (income / manufacturingTime) * 3600;
            //}
            //if (this.ExpectedProfitPerHour > this.ProfitPerHour)
            //    this.ExpectedProfitPerHour = this.ProfitPerHour;
            //if (ProfitPerUnit > 0)
            //    this.BreakEven = Convert.ToInt32(_blueprint.Buy / ProfitPerUnit);
        }
    }
}
