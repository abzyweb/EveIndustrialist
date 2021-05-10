using EoiData.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace EveOnlineTool.UserInterface.Controls
{
    public class ContractTypeComboBox : ComboBox
    {
        public ContractTypeComboBox()
        {
            var itemsSource = new List<string>();
            itemsSource.Add(ContractType.Buy);
            itemsSource.Add(ContractType.Sell);
            this.ItemsSource = itemsSource;
        }
    }
}
