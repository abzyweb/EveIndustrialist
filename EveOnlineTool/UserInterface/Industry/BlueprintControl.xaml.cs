using EoiData.EoiClasses;
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

namespace EveOnlineTool.UserInterface.Industry
{
    /// <summary>
    /// Interaction logic for BlueprintControl.xaml
    /// </summary>
    public partial class BlueprintControl : UserControl
    {
        public bool EnableManualChanges { get; set; }

        public EoiBlueprint Blueprint
        {
            get { return (EoiBlueprint)GetValue(BlueprintProperty); }
            set { SetValue(BlueprintProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Blueprint.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BlueprintProperty =
            DependencyProperty.Register("Blueprint", typeof(EoiBlueprint), typeof(BlueprintControl), new PropertyMetadata(null));



        public BlueprintControl()
        {
            EnableManualChanges = EoiInterface.GetUsers().Any(x => x.Authenticated) ? false : true;

            InitializeComponent();
        }
    }
}
