using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using EoiData.Constants;
using EoiData.EoiClasses;

namespace EveOnlineTool.UserInterface.Industry
{
    /// <summary>
    /// Interaction logic for NewIndustryContractControl.xaml
    /// </summary>
    public partial class NewIndustryContractControl : UserControl
    {
        public static readonly DependencyProperty SolarSystemsProperty = DependencyProperty.Register(
            "SolarSystems", typeof(ObservableCollection<EoiSolarSystem>), typeof(NewIndustryContractControl), new PropertyMetadata(default(ObservableCollection<EoiSolarSystem>)));

        public ObservableCollection<EoiSolarSystem> SolarSystems
        {
            get { return (ObservableCollection<EoiSolarSystem>) GetValue(SolarSystemsProperty); }
            set { SetValue(SolarSystemsProperty, value); }
        }

        public EoiBlueprint Blueprint
        {
            get { return (EoiBlueprint)GetValue(BlueprintProperty); }
            set { SetValue(BlueprintProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Blueprint.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BlueprintProperty =
            DependencyProperty.Register("Blueprint", typeof(EoiBlueprint), typeof(NewIndustryContractControl), new PropertyMetadata(null));

        public NewIndustryContractControl()
        {
            InitializeComponent();
        }

        internal void SetBlueprint(EoiBlueprint e)
        {
            Blueprint = e;
        }

        internal bool Create()
        {
            int volume;
            if (!int.TryParse(OrderVolumeTextBox.Text, out volume))
            {
                return false;
            }

            decimal price;
            if (!decimal.TryParse(PriceTextBox.Text, out price))
            {
                return false;
            }

            var success = EoiInterface.CreateIndustryContract(Blueprint, volume, price, (string)ContractTypeComboBox.SelectedItem, MaterialsDeliveredCheckBox.IsChecked.Value, BlueprintDeliveredCheckBox.IsChecked.Value, DescriptionTextBox.Text, EnablePartitionCheckBox.IsChecked.Value);
            if (!success)
            {
                return false;
            }

            Reset();
            return true;
        }

        internal void Cancel()
        {
            Reset();
        }

        private void Reset()
        {
            Blueprint = null;

            ContractTypeComboBox.SelectedItem = ContractType.Sell;

            EnablePartitionCheckBox.IsChecked = true;

            OrderVolumeTextBox.Text = string.Empty;
            PriceTextBox.Text = string.Empty;
            DescriptionTextBox.Text = string.Empty;
        }

        private void ContractTypeChanged(object sender, SelectionChangedEventArgs e)
        {
            if (MaterialsDeliveredCheckBox == null || BlueprintDeliveredCheckBox == null)
                return;

            if ((string)ContractTypeComboBox.SelectedItem == ContractType.Buy)
            {

            }
            else if ((string)ContractTypeComboBox.SelectedItem == ContractType.Sell)
            {
                
            }
        }
    }
}
