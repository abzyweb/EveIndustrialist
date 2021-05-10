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
    /// Interaction logic for AcceptIndustryContractControl.xaml
    /// </summary>
    public partial class AcceptIndustryContractControl : UserControl
    {
        public EoiContract Contract
        {
            get { return (EoiContract)GetValue(ContractProperty); }
            set { SetValue(ContractProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Contract.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ContractProperty =
            DependencyProperty.Register("Contract", typeof(EoiContract), typeof(AcceptIndustryContractControl), new PropertyMetadata(null));



        public int SelectedVolume
        {
            get { return (int)GetValue(SelectedVolumeProperty); }
            set { SetValue(SelectedVolumeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectedVolume.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedVolumeProperty =
            DependencyProperty.Register("SelectedVolume", typeof(int), typeof(AcceptIndustryContractControl), new PropertyMetadata(1, OnSelectedVolumeChanged));

        private static void OnSelectedVolumeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as AcceptIndustryContractControl;
            if (control != null)
            {
                control.OnSelectedVolumeChanged(e);
            }
        }

        public void OnSelectedVolumeChanged(DependencyPropertyChangedEventArgs args)
        {
            if ((int)args.NewValue > Contract.Volume)
                SelectedVolume = Contract.Volume;
            else if ((int)args.NewValue < 1)
                SelectedVolume = 1;
        }

        public AcceptIndustryContractControl()
        {
            InitializeComponent();
        }

        internal void Cancel()
        {
            Reset();
        }

        private void Reset()
        {
            SelectedVolume = 1;

            VolumeTextBox.IsEnabled = true;
            VolumeSlider.IsEnabled = true;
        }

        internal void SetContract(EoiContract contract)
        {
            this.Contract = contract;

            if (!Contract.EnablePartition)
            {
                VolumeTextBox.IsEnabled = false;
                VolumeSlider.IsEnabled = false;

                SelectedVolume = Contract.Volume;
            }
        }

        internal bool Accept()
        {
            var success = EoiInterface.AcceptIndustryContract(Contract, SelectedVolume);
            if (success)
            {
                Reset();
                return true;
            }
            return false;
        }
    }
}
