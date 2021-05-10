using EoiData.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EveOnlineTool.UserInterface.Controls
{
    public class SolarSystemComboBox : ComboBox
    {
        public SolarSystemComboBox()
        {
            this.DisplayMemberPath = "SolarSystemName";

            var itemsSourse = new List<SolarSystem>();
            itemsSourse.Add(SolarSystems.Amarr);
            itemsSourse.Add(SolarSystems.Jita);
            itemsSourse.Add(SolarSystems.Dodixie);
            itemsSourse.Add(SolarSystems.Hek);
            itemsSourse.Add(SolarSystems.Rens);
            //itemsSourse.Add(SolarSystems.Mani);
            this.ItemsSource = itemsSourse;
        }
    }
}
