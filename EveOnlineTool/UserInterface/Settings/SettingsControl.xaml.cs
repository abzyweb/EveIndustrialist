using EoiData.Constants;
using EoiData.EoiClasses;
using EoiData.Settings;
using EveOnlineTool.Helper;
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

namespace EveOnlineTool.UserInterface.Settings
{
    /// <summary>
    /// Interaction logic for SettingsControl.xaml
    /// </summary>
    public partial class SettingsControl : UserControl
    {
        public SettingsControl()
        {
            InitializeComponent();

            EnableAutoUpdaterCheckBox.IsChecked = SettingsInterface.GlobalSettings.EnableAutoUpdater;
            AutoUpdaterIntervalTextBox.Text = Helpers.MilisecondsToMinutes(SettingsInterface.GlobalSettings.AutoUpdaterInterval).ToString();
            EnableMarketUpdatesCheckBox.IsChecked = SettingsInterface.GlobalSettings.EnableMarketerUpdates;
            EnableMarketHistoryUpdatesCheckBox.IsChecked = SettingsInterface.GlobalSettings.EnableMarketHistoryUpdates;
            EnableCorporationBlueprintsUpdatesCheckBox.IsChecked = SettingsInterface.GlobalSettings.EnableCorporationBlueprintsUpdates;
            EnableCorporationContractsUpdatesCheckBox.IsChecked = SettingsInterface.GlobalSettings.EnableCorporationContractsUpdates;
            EnableEsiBlueprintsUpdatesCheckBox.IsChecked = SettingsInterface.GlobalSettings.EnableEsiBlueprintsUpdates;
            CorporationServerTextBox.Text = SettingsInterface.GlobalSettings.CorporationServer;

            EnableCalculationCheckBox.IsChecked = SettingsInterface.GlobalSettings.EnableCalculation;
            SaleTaxTextBox.Text = SettingsInterface.GlobalSettings.SaleTax.ToString("N2");
            BrokerFeeTextBox.Text = SettingsInterface.GlobalSettings.BrokerFee.ToString("N2");
            IndustryTaxTextBox.Text = SettingsInterface.GlobalSettings.IndustryTax.ToString("N2");
            StructureTaxBonusTextBox.Text = SettingsInterface.GlobalSettings.StructureTaxBonus.ToString("N2");
            StructureMaterialBonusTextBox.Text = SettingsInterface.GlobalSettings.StructureMaterialBonus.ToString("N2");
            NotOwnedMeTextBox.Text = SettingsInterface.GlobalSettings.NotOwnedMe.ToString("N0");
            NotOwnedTeTextBox.Text = SettingsInterface.GlobalSettings.NotOwnedTe.ToString("N0");
            ProfitPerHourForPriceTextBox.Text = SettingsInterface.GlobalSettings.ProfitPerHourForPrice.ToString("N0");

            ImportOrderTypeComboBox.SelectedItem = SettingsInterface.GlobalSettings.ImportOrderType;
            ExportOrderTypeComboBox.SelectedItem = SettingsInterface.GlobalSettings.ExportOrderType;

            var tradeHub = SolarSystems.GetSolarSystem(SettingsInterface.GlobalSettings.TradeHub);
            TradeHubComboBox.SelectedItem = tradeHub;

            ShowBlueprintCopiesCheckBox.IsChecked = SettingsInterface.GlobalSettings.ShowBlueprintCopies;
        }

        private void EnableAutoUpdaterChanged(object sender, RoutedEventArgs e)
        {
            SettingsInterface.GlobalSettings.EnableAutoUpdater = EnableAutoUpdaterCheckBox.IsChecked.Value;
        }

        private void AutoUpdaterIntervalChanged(object sender, TextChangedEventArgs e)
        {
            int result;
            if (!string.IsNullOrWhiteSpace(AutoUpdaterIntervalTextBox.Text) && int.TryParse(AutoUpdaterIntervalTextBox.Text, out result))
            {
                SettingsInterface.GlobalSettings.AutoUpdaterInterval = Helpers.MinutesToMiliseconds(result);
            }
        }

        private void EnableMarketUpdatesChanged(object sender, RoutedEventArgs e)
        {
            SettingsInterface.GlobalSettings.EnableMarketerUpdates = EnableMarketUpdatesCheckBox.IsChecked.Value;
        }

        private void EnableMarketHistoryUpdatesChanged(object sender, RoutedEventArgs e)
        {
            SettingsInterface.GlobalSettings.EnableMarketHistoryUpdates = EnableMarketHistoryUpdatesCheckBox.IsChecked.Value;
        }

        private void EnableCorporationBlueprintsUpdatesChanged(object sender, RoutedEventArgs e)
        {
            SettingsInterface.GlobalSettings.EnableCorporationBlueprintsUpdates = EnableCorporationBlueprintsUpdatesCheckBox.IsChecked.Value;
        }
        private void EnableCorporationContractsUpdatesChanged(object sender, RoutedEventArgs e)
        {
            SettingsInterface.GlobalSettings.EnableCorporationContractsUpdates = EnableCorporationContractsUpdatesCheckBox.IsChecked.Value;
        }
        private void EnableEsiBlueprintsUpdatesChanged(object sender, RoutedEventArgs e)
        {
            SettingsInterface.GlobalSettings.EnableEsiBlueprintsUpdates = EnableEsiBlueprintsUpdatesCheckBox.IsChecked.Value;
        }
        
        private void EnableCalculationChanged(object sender, RoutedEventArgs e)
        {
            SettingsInterface.GlobalSettings.EnableCalculation = EnableCalculationCheckBox.IsChecked.Value;

            EoiInterface.CalculateAllBlueprints();
        }

        private void SaleTaxChanged(object sender, TextChangedEventArgs e)
        {
            decimal result;
            if (!string.IsNullOrWhiteSpace(SaleTaxTextBox.Text) && decimal.TryParse(SaleTaxTextBox.Text, out result))
                SettingsInterface.GlobalSettings.SaleTax = result;

            EoiInterface.CalculateAllBlueprints();
        }

        private void BrokerFeeChanged(object sender, TextChangedEventArgs e)
        {
            decimal result;
            if (!string.IsNullOrWhiteSpace(BrokerFeeTextBox.Text) && decimal.TryParse(BrokerFeeTextBox.Text, out result))
                SettingsInterface.GlobalSettings.BrokerFee = result;

            EoiInterface.CalculateAllBlueprints();
        }

        private void IndustryTaxChanged(object sender, TextChangedEventArgs e)
        {
            decimal result;
            if (!string.IsNullOrWhiteSpace(IndustryTaxTextBox.Text) && decimal.TryParse(IndustryTaxTextBox.Text, out result))
                SettingsInterface.GlobalSettings.IndustryTax = result;

            EoiInterface.CalculateAllBlueprints();
        }

        private void StructureTaxBonusChanged(object sender, TextChangedEventArgs e)
        {
            decimal result;
            if (!string.IsNullOrWhiteSpace(StructureTaxBonusTextBox.Text) && decimal.TryParse(StructureTaxBonusTextBox.Text, out result))
                SettingsInterface.GlobalSettings.StructureTaxBonus = result;

            EoiInterface.CalculateAllBlueprints();
        }

        private void StructureMaterialBonusChanged(object sender, TextChangedEventArgs e)
        {
            decimal result;
            if (!string.IsNullOrWhiteSpace(StructureMaterialBonusTextBox.Text) && decimal.TryParse(StructureMaterialBonusTextBox.Text, out result))
                SettingsInterface.GlobalSettings.StructureMaterialBonus = result;

            EoiInterface.CalculateAllBlueprints();
        }
        private void NotOwnedMeChanged(object sender, TextChangedEventArgs e)
        {
            int result;
            if (!string.IsNullOrWhiteSpace(NotOwnedMeTextBox.Text) && int.TryParse(NotOwnedMeTextBox.Text, out result))
                SettingsInterface.GlobalSettings.NotOwnedMe = result;

            EoiInterface.UpdateAllBlueprintEfficencies();

            EoiInterface.CalculateAllBlueprints();
        }
        private void NotOwnedTeChanged(object sender, TextChangedEventArgs e)
        {
            int result;
            if (!string.IsNullOrWhiteSpace(NotOwnedTeTextBox.Text) && int.TryParse(NotOwnedTeTextBox.Text, out result))
                SettingsInterface.GlobalSettings.NotOwnedTe = result;

            EoiInterface.CalculateAllBlueprints();
        }


        private void ImportOrderChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ImportOrderTypeComboBox.SelectedItem != null)
                SettingsInterface.GlobalSettings.ImportOrderType = (OrderType)ImportOrderTypeComboBox.SelectedItem;

            EoiInterface.CalculateAllBlueprints();
        }

        private void ExportOrderChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ExportOrderTypeComboBox.SelectedItem != null)
                SettingsInterface.GlobalSettings.ExportOrderType = (OrderType)ExportOrderTypeComboBox.SelectedItem;

            EoiInterface.CalculateAllBlueprints();
        }

        private void TradeHubChanged(object sender, SelectionChangedEventArgs e)
        {
            if (TradeHubComboBox.SelectedItem != null)
                SettingsInterface.GlobalSettings.TradeHub = ((SolarSystem)TradeHubComboBox.SelectedItem).SolarSysteId.ToString();

            EoiInterface.CalculateAllBlueprints();
        }

        private void ShowBlueprintCopiesChanged(object sender, RoutedEventArgs e)
        {
            SettingsInterface.GlobalSettings.ShowBlueprintCopies = ShowBlueprintCopiesCheckBox.IsChecked.Value;
        }

        private void ProfitPerHourForPriceChanged(object sender, TextChangedEventArgs e)
        {
            decimal result;
            if (!string.IsNullOrWhiteSpace(ProfitPerHourForPriceTextBox.Text) && decimal.TryParse(ProfitPerHourForPriceTextBox.Text, out result))
                SettingsInterface.GlobalSettings.ProfitPerHourForPrice = result;

            EoiInterface.CalculateAllBlueprints();
        }
    }
}
