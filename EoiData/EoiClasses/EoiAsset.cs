using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EoiData.EoiClasses
{
    public class EoiAsset : INotifyPropertyChanged
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public decimal TradeHubPrice { get; set; }
        public int Quantity { get; set; }
        public decimal PriceChange { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        internal void InvokePropertyChanged()
        {
            OnPropertyChanged("PriceChange");
            OnPropertyChanged("TradeHubPrice");
            OnPropertyChanged("Price");
            OnPropertyChanged("Quantity");
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
