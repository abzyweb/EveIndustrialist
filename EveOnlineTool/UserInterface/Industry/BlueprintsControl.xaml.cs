using EoiData.EoiClasses;
using EveOnlineTool.Helper;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
    /// Interaction logic for BlueprintsControl.xaml
    /// </summary>
    public partial class BlueprintsControl : UserControl
    {
        public ObservableCollection<EoiBlueprint> Blueprints { get; private set; }

        public event EventHandler<EoiBlueprint> CreateContractRequested;

        public ICollectionView BlueprintsCollectionView
        {
            get { return (ICollectionView)GetValue(BlueprintsCollectionViewProperty); }
            set { SetValue(BlueprintsCollectionViewProperty, value); }
        }

        // Using a DependencyProperty as the backing store for BlueprintsCollectionView.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BlueprintsCollectionViewProperty =
            DependencyProperty.Register("BlueprintsCollectionView", typeof(ICollectionView), typeof(BlueprintsControl), new PropertyMetadata(null));

        public EoiBlueprint SelectedBlueprint
        {
            get { return (EoiBlueprint)GetValue(SelectedBlueprintProperty); }
            set { SetValue(SelectedBlueprintProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectedBlueprint.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedBlueprintProperty =
            DependencyProperty.Register("SelectedBlueprint", typeof(EoiBlueprint), typeof(BlueprintsControl), new PropertyMetadata(null));

        public BlueprintsControl()
        {
            var descriptor = DependencyPropertyDescriptor.FromProperty(DesignerProperties.IsInDesignModeProperty, typeof(FrameworkElement));
            var isDesignMode = (bool)descriptor.Metadata.DefaultValue;

            InitializeComponent();

            if (!isDesignMode)
            {
                this.Blueprints = EoiInterface.GetBlueprints();
                var collectionView = new CollectionViewSource() { Source = this.Blueprints };
                var itemList = collectionView.View;
                var filter = new Predicate<object>(CustomFilter);
                itemList.Filter = filter;
                this.BlueprintsCollectionView = itemList;
            }
        }

        private bool CustomFilter(object obj)
        {
            var result = true;

            var blueprint = obj as EoiBlueprint;

            var searchString = this.SearchTextBox.Text;
            if (!string.IsNullOrWhiteSpace(searchString))
            {
                if (!blueprint.Name.Contains(searchString, StringComparison.CurrentCultureIgnoreCase))
                    return false;
            }

            var subResult = false;

            if (BuyableBlueprintsCheckBox.IsChecked == true && blueprint.Price > 0)
                subResult = true;

            if (OwnedBlueprintsCheckBox.IsChecked == true && blueprint.Owned)
                subResult = true;

            if (CorporationOwnedBlueprintsCheckBox.IsChecked == true && blueprint.CorporationOwned)
                subResult = true;

            if (InventableBlueprintsCheckBox.IsChecked == true)
            {
                if (OwnedBlueprintsCheckBox.IsChecked == true)
                {
                    if (blueprint.Inventable)
                        subResult = true;
                }
                else
                {
                    if (BuyableBlueprintsCheckBox.IsChecked == true)
                    {
                        if (blueprint.HasParent && blueprint.ParentPrice > 0)
                            subResult = true;
                    }
                    else
                    {
                        if (blueprint.HasParent)
                            subResult = true;
                    }
                }
            }
            if (MarketBlueprintsCheckBox.IsChecked == true && blueprint.HasCharacterOrders)
                subResult = true;

            if (BuyableBlueprintsCheckBox.IsChecked == false &&
                OwnedBlueprintsCheckBox.IsChecked == false &&
                InventableBlueprintsCheckBox.IsChecked == false &&
                MarketBlueprintsCheckBox.IsChecked == false &&
                CorporationOwnedBlueprintsCheckBox.IsChecked == false)
                return result;
            else
                return subResult;
        }

        private void BuyableChanged(object sender, RoutedEventArgs e)
        {
            BlueprintsCollectionView.Refresh();
        }

        private void OwnedChanged(object sender, RoutedEventArgs e)
        {
            BlueprintsCollectionView.Refresh();
        }

        private void CorporationOwnedChanged(object sender, RoutedEventArgs e)
        {
            BlueprintsCollectionView.Refresh();
        }

        private void SearchTextChanged(object sender, TextChangedEventArgs e)
        {
            BlueprintsCollectionView.Refresh();
        }

        private void CreateContract(object sender, RoutedEventArgs e)
        {
            var handler = CreateContractRequested;
            if (handler != null)
                handler(this, SelectedBlueprint);
        }

        private void InventableChanged(object sender, RoutedEventArgs e)
        {
            BlueprintsCollectionView.Refresh();
        }
        private void MarketChanged(object sender, RoutedEventArgs e)
        {
            BlueprintsCollectionView.Refresh();
        }
    }
}
