using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EoiData.EoiClasses
{
    public class EoiUser : INotifyPropertyChanged
    {
        private string _name;

        public string Name { get => _name; set => _name = value; }
        public string Corporation { get; set; }
        public string Alliance { get; set; }
        public bool Authenticated { get; set; }
        public bool IsDefault { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        internal void InvokePropertyChanged()
        {
            OnPropertyChanged("Name");
            OnPropertyChanged("Authenticated");
            OnPropertyChanged("IsDefault");
        }
    }
}
