using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EoiData.EoiDataClasses;

namespace EoiData.EoiClasses
{
    public class EoiBlueprint : INotifyPropertyChanged
    {
        private bool _owned;
        private int _timeEfficency;
        private int _materialEfficency;
        private decimal _materialExpensesPerRun;
        private decimal _materialExpensesPerRunJita;
        private decimal _materialExpensesPerUnit;
        private decimal _productionBaseTaxPerRun;
        private decimal _productionBaseTaxPerUnit;
        private decimal _productionCorporationTaxPerRun;
        private decimal _productionCorporationTaxPerUnit;
        private decimal _salesTaxesPerRun;
        private decimal _brokerFeesPerRun;
        private decimal _salesPricePerRun;
        private decimal _salesPricePerUnit;

        private decimal _profitPerHour;
        private decimal _expensesPerHour;
        private decimal _incomePerHour;
        private decimal _profitPerRun;
        private decimal _expensesPerRun;
        private decimal _incomePerRun;
        private decimal _profitPerUnit;
        private decimal _expensesPerUnit;
        private decimal _expensesPerUnitJita;
        private decimal _incomePerUnit;
        private decimal _expectedProfitPerHour;
        private decimal _expectedOptimalProfitPerHour;
        private decimal _taxes;
        private decimal _price;
        private decimal _parentPrice;
        private bool _private;

        public int Id { get; internal set; }
        public string Name { get; internal set; }

        public ObservableCollection<EoiType> Materials { get; internal set; } = new ObservableCollection<EoiType>();
        public List<EoiType> Products { get; internal set; }
        public int MaterialEfficency
        {
            get { return _materialEfficency; }
            set
            {
                _materialEfficency = value;
                OnPropertyChanged("MaterialEfficency");
            }
        }
        public int TimeEfficency
        {
            get { return _timeEfficency; }
            set
            {
                _timeEfficency = value;
                OnPropertyChanged("TimeEfficency");
            }
        }
        public bool Owned { get { return _owned; } set { _owned = value; OnPropertyChanged("Owned"); } }
        
        public decimal ProfitPerHour
        {
            get { return _profitPerHour; }
            internal set
            {
                _profitPerHour = value;
            }
        }

        public bool Private
        {
            get => _private; set
            {
                _private = value;
                OnPropertyChanged("Private");
            }
        }

        public bool IsCopy { get; set; }

        public decimal ProductionBaseTaxPerRun { get => _productionBaseTaxPerRun; internal set => _productionBaseTaxPerRun = value; }
        public decimal ProductionCorporationTaxPerRun { get => _productionCorporationTaxPerRun; internal set => _productionCorporationTaxPerRun = value; }
        public decimal ProductionBaseTaxPerUnit { get => _productionBaseTaxPerUnit; internal set => _productionBaseTaxPerUnit = value; }
        public decimal ProductionCorporationTaxPerUnit { get => _productionCorporationTaxPerUnit; internal set => _productionCorporationTaxPerUnit = value; }
        public decimal MaterialExpensesPerRun { get => _materialExpensesPerRun; internal set => _materialExpensesPerRun = value; }
        public decimal MaterialExpensesPerRunJita { get => _materialExpensesPerRunJita; internal set => _materialExpensesPerRunJita = value; }
        
        public decimal MaterialExpensesPerUnit { get => _materialExpensesPerUnit; internal set => _materialExpensesPerUnit = value; }
        public decimal SalesTaxesPerRun { get => _salesTaxesPerRun; internal set => _salesTaxesPerRun = value; }
        public decimal BrokerFeesPerRun { get => _brokerFeesPerRun; internal set => _brokerFeesPerRun = value; }
        public decimal SalesPricePerRun { get => _salesPricePerRun; internal set => _salesPricePerRun = value; }
        public decimal SalesPricePerUnit { get => _salesPricePerUnit; internal set => _salesPricePerUnit = value; }

        public decimal ExpensesPerHour { get => _expensesPerHour; internal set => _expensesPerHour = value; }
        public decimal IncomePerHour { get => _incomePerHour; internal set => _incomePerHour = value; }
        public decimal ProfitPerRun { get => _profitPerRun; internal set => _profitPerRun = value; }
        public decimal ExpensesPerRun { get => _expensesPerRun; internal set => _expensesPerRun = value; }
        public decimal IncomePerRun { get => _incomePerRun; internal set => _incomePerRun = value; }
        public decimal ProfitPerUnit { get => _profitPerUnit; internal set => _profitPerUnit = value; }
        public decimal ExpensesPerUnit { get => _expensesPerUnit; internal set => _expensesPerUnit = value; }
        public decimal ExpensesPerUnitJita { get => _expensesPerUnitJita; internal set => _expensesPerUnitJita = value; }
        public decimal IncomePerUnit { get => _incomePerUnit; internal set => _incomePerUnit = value; }
        public decimal ExpectedProfitPerHour { get => _expectedProfitPerHour; internal set => _expectedProfitPerHour = value; }
        public decimal ExpectedOptimalProfitPerHour { get => _expectedOptimalProfitPerHour; internal set => _expectedOptimalProfitPerHour = value; }
        public decimal Taxes { get => _taxes; internal set => _taxes = value; }
        public decimal Price { get => _price; internal set => _price = value; }
        public decimal ParentPrice { get => _parentPrice; internal set => _parentPrice = value; }
        public decimal BuyPrice { get; set; }
        public decimal SellPrice { get; set; }
        public decimal OptimalPrice { get; set; }
        public decimal OptimalMarketPrice { get; set; }
        public bool CorporationOwned { get; set; }
        public bool Inventable { get; set; }
        public bool HasParent { get; set; }
        public bool HasCharacterOrders { get; set; }
        

        public EoiBlueprint()
        {

        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        internal void InvokePropertyChanged()
        {

            OnPropertyChanged("HasCharacterOrders");
            OnPropertyChanged("IsCopy");
            OnPropertyChanged("Price");
            OnPropertyChanged("ParentPrice");
            OnPropertyChanged("Taxes");
            OnPropertyChanged("ExpectedProfitPerHour");
            OnPropertyChanged("IncomePerUnit");
            OnPropertyChanged("ExpensesPerUnit");
            OnPropertyChanged("ExpensesPerUnitJita");
            OnPropertyChanged("ProfitPerUnit");
            OnPropertyChanged("IncomePerRun");
            OnPropertyChanged("ExpensesPerRun");
            OnPropertyChanged("ProfitPerRun");
            OnPropertyChanged("IncomePerHour");
            OnPropertyChanged("ExpensesPerHour");
            OnPropertyChanged("ProfitPerHour");
            OnPropertyChanged("CorporationOwned");

            OnPropertyChanged("ProductionBaseTaxPerRun");
            OnPropertyChanged("ProductionCorporationTaxPerRun");
            OnPropertyChanged("MaterialExpensesPerRun");
            OnPropertyChanged("MaterialExpensesPerRunJita");
            OnPropertyChanged("SalesTaxesPerRun");
            OnPropertyChanged("BrokerFeesPerRun");
            OnPropertyChanged("SalesPricePerRun");
            OnPropertyChanged("SalesPricePerUnit");

            OnPropertyChanged("BuyPrice");
            OnPropertyChanged("SellPrice");
            OnPropertyChanged("OptimalPrice");
            OnPropertyChanged("OptimalMarketPrice");
            OnPropertyChanged("Inventable");
            OnPropertyChanged("HasParent");

            // OnPropertyChanged("MaterialEfficency");
            // OnPropertyChanged("TimeEfficency");

            foreach (var material in Materials)
                material.InvokePropertyChanged();

        }

        public override string ToString()
        {
            return Name;
        }


    }
}
