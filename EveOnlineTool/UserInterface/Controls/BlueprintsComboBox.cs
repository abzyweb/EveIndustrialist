using EoiData.EoiClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace EveOnlineTool.UserInterface.Controls
{
    public class BlueprintsComboBox : ComboBox
    {
        public BlueprintsComboBox()
        {
            this.ItemsSource = EoiInterface.GetBlueprints();
        }
    }
}
