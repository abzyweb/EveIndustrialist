using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EoiData.EoiDataClasses;

namespace EoiData.EoiClasses
{
    public class EoiType : INotifyPropertyChanged
    {
        public EoiType()
        {
            
        }

        public int Id { get; internal set; }
        public string Name { get; internal set; }
        public int Quantity { get; internal set; }
        public decimal Price { get; internal set; }
        public decimal PricePerUnit { get; internal set; }
        public bool Produced { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        internal void InvokePropertyChanged()
        {
            OnPropertyChanged("Price");
            OnPropertyChanged("PricePerUnit");
            OnPropertyChanged("Produced");
            OnPropertyChanged("Quantity");

        }
    }
}
