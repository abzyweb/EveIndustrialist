using EoiData.EoiDataClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EoiData.EoiClasses
{
    public class EoiContract : INotifyPropertyChanged
    {
        public string Blueprint { get; set; }
        public int Volume { get; set; }
        public decimal Price { get; set; }
        public string Client { get; set; }
        public string Contractor { get; set; }
        public string OrderType { get; set; }
        public string Description { get; set; }
        public string State { get; set; }
        public bool MaterialIncluded { get; set; }
        public bool BlueprintIncluded { get; set; }
        public long? Parent { get;  set; }
        public int Destination { get; set; }
        public bool EnablePartition { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        internal void InvokePropertyChanged()
        {
            OnPropertyChanged(nameof(Blueprint));
            OnPropertyChanged(nameof(Volume));
            OnPropertyChanged(nameof(Price));
            OnPropertyChanged(nameof(Client));
            OnPropertyChanged(nameof(Contractor));
            OnPropertyChanged(nameof(OrderType));
            OnPropertyChanged(nameof(Description));
            OnPropertyChanged(nameof(State));
            OnPropertyChanged(nameof(MaterialIncluded));
            OnPropertyChanged(nameof(BlueprintIncluded));
            OnPropertyChanged(nameof(EnablePartition));
            
        }

        public void Delete()
        {
            EoiDataInterface.Delete(this);
        }

        public bool CanDelete()
        {
            return EoiDataInterface.CanDelete(this);
        }

        public bool CanAccept()
        {
            return EoiDataInterface.CanAccept(this);
        }

        public bool CanFinish()
        {
            return EoiDataInterface.CanFinish(this);
        }

        public void Finish()
        {
            EoiDataInterface.Finish(this);
        }
    }
}
