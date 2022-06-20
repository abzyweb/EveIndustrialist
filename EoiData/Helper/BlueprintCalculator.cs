using EoiData.Constants;
using EoiData.EoiDataClasses;
using EoiData.EsiDataClasses;
using EoiData.Settings;
using EveSwaggerConnection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EoiData.Helper
{
    internal static class BlueprintCalculator
    {
        internal static void Calculate(EoiDataBlueprint blueprint)
        {
            decimal profit = 0;
            decimal investment = 0;
            decimal investmentJita = 0;
            decimal income = 0;
            decimal taxes = 0;
            decimal materialInvestment = 0;
            decimal materialInvestmentJita = 0;
            decimal baseTaxInvestment = 0;
            decimal corporationTaxInvestment = 0;

            var eoiBlueprint = blueprint.GetEoiBlueprint();
            int manufacturingTime = blueprint.GetManufacturingTime();

            eoiBlueprint.ProfitPerHour = 0;
            eoiBlueprint.ExpensesPerHour = 0;
            eoiBlueprint.IncomePerHour = 0;

            eoiBlueprint.ProfitPerRun = 0;
            eoiBlueprint.ExpensesPerRun = 0;
            eoiBlueprint.IncomePerRun = 0;

            eoiBlueprint.ProfitPerUnit = 0;
            eoiBlueprint.ExpensesPerUnit = 0;
            eoiBlueprint.IncomePerUnit = 0;

            eoiBlueprint.ExpectedProfitPerHour = 0;
            eoiBlueprint.ExpectedOptimalProfitPerHour = 0;

            eoiBlueprint.Taxes = 0;

            eoiBlueprint.SalesTaxesPerRun = 0;
            eoiBlueprint.ProductionBaseTaxPerRun = 0;
            eoiBlueprint.ProductionBaseTaxPerUnit = 0;
            eoiBlueprint.ProductionCorporationTaxPerRun = 0;
            eoiBlueprint.ProductionCorporationTaxPerUnit = 0;
            eoiBlueprint.MaterialExpensesPerRun = 0;
            eoiBlueprint.MaterialExpensesPerUnit = 0;
            eoiBlueprint.BrokerFeesPerRun = 0;
            eoiBlueprint.SalesPricePerRun = 0;
            eoiBlueprint.SalesPricePerUnit = 0;

            eoiBlueprint.BuyPrice = 0;
            eoiBlueprint.SellPrice = 0;

            eoiBlueprint.OptimalPrice = 0;
            eoiBlueprint.OptimalMarketPrice = 0;

            if (!SettingsInterface.GlobalSettings.EnableAutoUpdater)
                return;

            var optimalProfit = (SettingsInterface.GlobalSettings.ProfitPerHourForPrice / 3600) * manufacturingTime;
            var solarSystemCostIndices = EsiDataInterface.GetSolarSystemCostIndices(SolarSystems.Vashkah.SolarSysteId);
            //var solarSystemCostIndices = EsiDataInterface.GetSolarSystemCostIndices(SolarSystems.Bika.SolarSysteId);
            // var solarSystemCostIndices = EsiDataInterface.GetSolarSystemCostIndices(SolarSystems.Jerma.SolarSysteId);
            // var solarSystemCostIndices = EsiDataInterface.GetSolarSystemCostIndices(SolarSystems.Irnal.SolarSysteId);

            if (solarSystemCostIndices == null)
                return;

            var region = Regions.GetTradehubRegionId(SolarSystems.GetSolarSystem(SettingsInterface.GlobalSettings.TradeHub));

            foreach (var material in blueprint.Materials)
            {
                var eoiType = eoiBlueprint.Materials.FirstOrDefault(x => material.GetEoiType(x) != null);
                eoiType.Produced = false;

                var possible = false;
                var possibleJita = false;

                decimal productionCost = 0;
                decimal productionCostJita = 0;
                decimal baseTaxCost = 0;
                decimal corporationTaxCost = 0;
                decimal materialCost = 0;
                decimal materialCostJita = 0;

                taxes += material.AdjustedPrice * blueprint.GetCalculatedMaterialQuantity(1, material);   
                
                var materialMarketPrices = material.GetMarketPrices().FirstOrDefault(x => x.Region == region);

                if (SettingsInterface.GlobalSettings.ImportOrderType == OrderType.Buy)
                {
                    if (materialMarketPrices != null)
                    {
                        var materialMarketPricesBuyOrders = materialMarketPrices.Orders.Where(x => x.is_buy_order && x.system_id.ToString() == SettingsInterface.GlobalSettings.TradeHub);
                        if (materialMarketPricesBuyOrders.Any())
                        {
                            productionCost = materialMarketPricesBuyOrders.Max(x => x.price);
                            possible = true;
                        }
                    }
                    if (productionCost == 0)
                    {
                        // TODO No Buy Price ...
                    }
                }
                else if (SettingsInterface.GlobalSettings.ImportOrderType == OrderType.Sell)
                {
                    if (materialMarketPrices != null)
                    {
                        var materialMarketOrders = materialMarketPrices.Orders.Where(x => !x.is_buy_order && x.system_id.ToString() == SettingsInterface.GlobalSettings.TradeHub).ToList();
                        if (materialMarketOrders.Any())
                        {
                            productionCost = materialMarketOrders.Min(x => x.price);
                            possible = true;
                        }
                    }
                }

                var materialMarketPricesJita = material.GetMarketPrices().FirstOrDefault(x => x.Region == Regions.TheForge);

                if (SettingsInterface.GlobalSettings.ImportOrderType == OrderType.Buy)
                {
                    if (materialMarketPricesJita != null)
                    {
                        var materialMarketPricesBuyOrders = materialMarketPricesJita.Orders.Where(x => x.is_buy_order && x.system_id.ToString() == SolarSystems.Jita.SolarSysteId.ToString());
                        if (materialMarketPricesBuyOrders.Any())
                        {
                            productionCostJita = materialMarketPricesBuyOrders.Max(x => x.price);
                            possibleJita = true;
                        }
                    }
                    if (productionCost == 0)
                    {
                        // TODO No Buy Price ...
                    }
                }
                else if (SettingsInterface.GlobalSettings.ImportOrderType == OrderType.Sell)
                {
                    if (materialMarketPricesJita != null)
                    {
                        var materialMarketOrders = materialMarketPricesJita.Orders.Where(x => !x.is_buy_order && x.system_id.ToString() == SolarSystems.Jita.SolarSysteId.ToString()).ToList();
                        if (materialMarketOrders.Any())
                        {
                            productionCostJita = materialMarketOrders.Min(x => x.price);
                            possibleJita = true;
                        }
                    }
                }

                if (possible)
                    materialCost = productionCost;

                if (possibleJita)
                    materialCostJita = productionCostJita;

                // Check if Material can be produced
                var bp = EoiDataInterface.GetBlueprintByProduct(material.Id);
                if (bp != null && bp.GetMarketPrices().Any(y => y?.Orders.Any(x => !x.is_buy_order) == true))
                {
                    BlueprintCalculator.Calculate(bp);

                    var subEoiBlueprint = bp.GetEoiBlueprint();

                    var blueprintExpenses = subEoiBlueprint.ExpensesPerUnit;
                    if (blueprintExpenses > 0 && productionCost > blueprintExpenses)
                    {
                        materialCost = subEoiBlueprint.MaterialExpensesPerUnit;
                        baseTaxCost = subEoiBlueprint.ProductionBaseTaxPerUnit;
                        corporationTaxCost = subEoiBlueprint.ProductionCorporationTaxPerUnit;

                        taxes += subEoiBlueprint.Taxes;
                        productionCost = subEoiBlueprint.ExpensesPerUnit;

                        possible = true;

                        eoiType.Produced = true;
                    }
                }

                if (!possible)
                    return;

                var calculatedMaterialQuantity = blueprint.GetCalculatedMaterialQuantity(1, material);

                eoiType.Quantity = calculatedMaterialQuantity;
                eoiType.PricePerUnit = productionCost;
                eoiType.Price = productionCost * calculatedMaterialQuantity;

                profit -= productionCost * calculatedMaterialQuantity;
                investment += productionCost * calculatedMaterialQuantity;
                investmentJita += productionCostJita * calculatedMaterialQuantity;

                materialInvestmentJita += materialCostJita * calculatedMaterialQuantity;
                materialInvestment += materialCost * calculatedMaterialQuantity;
                baseTaxInvestment += baseTaxCost * calculatedMaterialQuantity;
                corporationTaxInvestment =+ corporationTaxCost * calculatedMaterialQuantity;
            }

            eoiBlueprint.MaterialExpensesPerRun = materialInvestment;
            eoiBlueprint.MaterialExpensesPerRunJita = materialInvestmentJita;

            if (solarSystemCostIndices != null)
            {
                var solarSystemTax = Math.Round(Math.Round(taxes) * solarSystemCostIndices.GetManufacturingCostIndex());
                var structureBonus = Math.Floor(solarSystemTax * (SettingsInterface.GlobalSettings.StructureTaxBonus / 100));
                solarSystemTax -= structureBonus;

                baseTaxInvestment += solarSystemTax;

                var corpTax = solarSystemTax * (SettingsInterface.GlobalSettings.IndustryTax / 100);

                corporationTaxInvestment += corpTax;

                eoiBlueprint.Taxes = Math.Round(solarSystemTax + corpTax);

                investment += eoiBlueprint.Taxes;
                investmentJita += eoiBlueprint.Taxes;

                profit -= eoiBlueprint.Taxes;
            }

            eoiBlueprint.ExpensesPerRun = investment;
            eoiBlueprint.ProductionBaseTaxPerRun = baseTaxInvestment;
            eoiBlueprint.ProductionCorporationTaxPerRun = corporationTaxInvestment;

            foreach (var product in blueprint.Products)
            {
                decimal productPrice = 0;

                var productMarketPrices = product.GetMarketPrices().FirstOrDefault(x => x.Region == region);
                // var productMarketPrices = product.GetMarketPrices().FirstOrDefault(x => x.Region == Regions.TashMurkon);
                if (productMarketPrices != null)
                {
                    // var productMarketBuyOrders = productMarketPrices.Orders.Where(x => x.is_buy_order && x.system_id.ToString() == SolarSystems.Mani.SolarSysteId.ToString()).ToList();
                    var productMarketBuyOrders = productMarketPrices.Orders.Where(x => x.is_buy_order && x.system_id.ToString() == SettingsInterface.GlobalSettings.TradeHub).ToList();
                    if (SettingsInterface.GlobalSettings.TradeHub == SolarSystems.Amarr.SolarSysteId.ToString())
                        productMarketBuyOrders.AddRange(productMarketPrices.Orders.Where(x => x.is_buy_order && x.system_id.ToString() == SolarSystems.Ashab.SolarSysteId.ToString()).ToList());
                    if (productMarketBuyOrders.Any())
                        eoiBlueprint.BuyPrice = productMarketBuyOrders.Max(x => x.price);

                    // var productMarketSellOrders = productMarketPrices.Orders.Where(x => !x.is_buy_order && x.system_id.ToString() == SolarSystems.Mani.SolarSysteId.ToString()).ToList();
                    var productMarketSellOrders = productMarketPrices.Orders.Where(x => !x.is_buy_order && x.system_id.ToString() == SettingsInterface.GlobalSettings.TradeHub).ToList();
                    if (productMarketSellOrders.Any())
                        eoiBlueprint.SellPrice = productMarketSellOrders.Min(x => x.price);
                }


                if (SettingsInterface.GlobalSettings.ExportOrderType == OrderType.Buy)
                {
                    if (eoiBlueprint.BuyPrice > 0)
                    {
                        productPrice = eoiBlueprint.BuyPrice;
                    }
                }
                else if (SettingsInterface.GlobalSettings.ExportOrderType == OrderType.Sell)
                {
                    if (eoiBlueprint.SellPrice > 0)
                    {
                        productPrice = eoiBlueprint.SellPrice;
                    }
                }

                eoiBlueprint.SalesPricePerUnit = productPrice;
                eoiBlueprint.SalesPricePerRun = productPrice * blueprint.GetProductQuantity(product);

                var salesTax = eoiBlueprint.SalesPricePerRun * (SettingsInterface.GlobalSettings.SaleTax / 100);
                decimal brokerFee = 0;
                if (SettingsInterface.GlobalSettings.ExportOrderType == OrderType.Sell)
                    brokerFee = eoiBlueprint.SalesPricePerRun * (SettingsInterface.GlobalSettings.BrokerFee / 100);

                eoiBlueprint.SalesTaxesPerRun = salesTax;

                profit += eoiBlueprint.SalesPricePerRun - salesTax;
                income += eoiBlueprint.SalesPricePerRun - salesTax;

                eoiBlueprint.BrokerFeesPerRun = brokerFee;

                profit -= brokerFee;
                income -= brokerFee;

                eoiBlueprint.IncomePerRun = income;
            }

            eoiBlueprint.ProfitPerRun = profit;

            if (blueprint.Products.Count == 1)
            {
                var productQuantity = blueprint.GetProductQuantity(blueprint.Products[0]);

                eoiBlueprint.MaterialExpensesPerUnit = eoiBlueprint.MaterialExpensesPerRun / productQuantity;
                eoiBlueprint.ProductionBaseTaxPerUnit = eoiBlueprint.ProductionBaseTaxPerRun / productQuantity;
                eoiBlueprint.ProductionCorporationTaxPerUnit = eoiBlueprint.ProductionCorporationTaxPerRun / productQuantity;
                
                eoiBlueprint.ExpensesPerUnit = investment / productQuantity;
                eoiBlueprint.ExpensesPerUnitJita = investmentJita / productQuantity;
                eoiBlueprint.IncomePerUnit = income / productQuantity;
                var optimalProfitPerUnit = optimalProfit / productQuantity;
                eoiBlueprint.ProfitPerUnit = profit / productQuantity;
                
                var history = blueprint.Products[0].GetMarketHistory();
                if (history != null)
                {
                    if (history.UnitsPerSecond <= 0 || productQuantity <= 0)
                    {
                        eoiBlueprint.ExpectedProfitPerHour = 0;
                        eoiBlueprint.ExpectedOptimalProfitPerHour = 0;
                    }
                    else
                    {
                        var manufacturingTimePerUnit = manufacturingTime / productQuantity;
                        if (manufacturingTimePerUnit <= 0)
                        {
                            eoiBlueprint.ExpectedProfitPerHour = 0;
                            eoiBlueprint.ExpectedOptimalProfitPerHour = 0;
                        }
                            
                        else
                        {
                            eoiBlueprint.ExpectedProfitPerHour = (eoiBlueprint.ProfitPerUnit * history.UnitsPerSecond) * 3600;
                            eoiBlueprint.ExpectedOptimalProfitPerHour = (Math.Min(optimalProfitPerUnit, eoiBlueprint.ProfitPerUnit) * history.UnitsPerSecond) * 3600;
                        }
                    }
                }
                else
                {
                    eoiBlueprint.ExpectedProfitPerHour = 0;
                    eoiBlueprint.ExpectedOptimalProfitPerHour = 0;
                }
            }
            
            eoiBlueprint.ProfitPerHour = (profit / manufacturingTime) * 3600;
            eoiBlueprint.ExpensesPerHour = (investment / manufacturingTime) * 3600;
            eoiBlueprint.IncomePerHour = (income / manufacturingTime) * 3600;
            

            if (blueprint.Products.Count == 1)
            {
                var productQuantity = blueprint.GetProductQuantity(blueprint.Products[0]);
                var optimalProfitPerUnit = optimalProfit / productQuantity;
                eoiBlueprint.OptimalPrice = eoiBlueprint.ExpensesPerUnit + optimalProfitPerUnit;

                if (SettingsInterface.GlobalSettings.ExportOrderType == OrderType.Buy)
                {
                    var salesTax = eoiBlueprint.OptimalPrice * (SettingsInterface.GlobalSettings.SaleTax / 100);

                    eoiBlueprint.OptimalMarketPrice = eoiBlueprint.ExpensesPerUnit + optimalProfitPerUnit + salesTax;
                }
                else if (SettingsInterface.GlobalSettings.ExportOrderType == OrderType.Sell)
                {
                    var salesTax = eoiBlueprint.OptimalPrice * (SettingsInterface.GlobalSettings.SaleTax / 100);
                    decimal brokerFee = 0;
                    if (SettingsInterface.GlobalSettings.ExportOrderType == OrderType.Sell)
                        brokerFee = eoiBlueprint.OptimalPrice * (SettingsInterface.GlobalSettings.BrokerFee / 100);

                    eoiBlueprint.OptimalMarketPrice = eoiBlueprint.ExpensesPerUnit + optimalProfitPerUnit + salesTax + brokerFee;
                }
            }

            //if (eoiBlueprint.ExpectedProfitPerHour > eoiBlueprint.ProfitPerHour)
            //    eoiBlueprint.ExpectedProfitPerHour = eoiBlueprint.ProfitPerHour;
        }
    }
}
