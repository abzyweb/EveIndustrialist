using EoiData.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EoiData.Settings
{
    public class GlobalSettings
    {
        public bool EnableAutoUpdater { get; set; } = true;
        public bool EnableMarketerUpdates { get; set; } = true;
        public bool EnableMarketHistoryUpdates { get; set; } = false;
        public bool EnableEsiBlueprintsUpdates { get; set; } = true;
        public bool EnableCorporationBlueprintsUpdates { get; set; } = false;
        public bool EnableCorporationContractsUpdates { get; set; } = false;

        public decimal SaleTax { get; set; } = 1.4m;
        public decimal BrokerFee { get; set; } = 2.55m;
        public decimal IndustryTax { get; set; } = 2;
        // public string CorporationServer { get; set; } = @"http://yourUrlHere.com/EveOnline/";
        public string CorporationServer { get; set; } = @"http://www.mobilies.at/EveOnline/";
        
        public decimal StructureTaxBonus { get; set; } = 4;
        public decimal ProfitPerHourForPrice { get; set; }
        public decimal StructureMaterialBonus { get; set; } = 1;
        public int AutoUpdaterInterval { get; set; } = 300000;
        public bool EnableCalculation { get; set; } = true;
        public int NotOwnedMe { get; set; } = 7;
        public int NotOwnedTe { get; set; } = 14;

        public OrderType ImportOrderType { get; set; } = OrderType.Sell;
        public OrderType ExportOrderType { get; set; } = OrderType.Sell;
        public string TradeHub { get; set; } = SolarSystems.Jita.SolarSysteId.ToString();

        public bool ShowBlueprintCopies { get; set; } = false;
        
    }
}
