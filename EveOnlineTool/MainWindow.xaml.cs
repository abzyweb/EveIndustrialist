using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.IO;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using EveOnlineIndustrialist.EveData;
using EveOnlineIndustrialist.Eve_Data;
using EveOnlineIndustrialist.Market_Data;
using EveOnlineTool.Eve_Data;
using EveOnlineTool;
using EveOnlineTool.Personal_Data;
using CorporationWebConnection.WebCommunication.Operations;
using EveOnlineTool.Application_Data;
using EveOnlineTool.Settings;
using EveOnlineIndustrialist.EoiThreadManager;
using EveSwaggerConnection;
using EoiData.EoiClasses;
using System.Windows.Threading;

namespace EveOnlineIndustrialist
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<EoiBlueprint> _blueprints;
        private DispatcherTimer _progressTimer;

        public string ProgressText
        {
            get { return (string)GetValue(ProgressTextProperty); }
            set { SetValue(ProgressTextProperty, value); }
        }

        public List<string> OwnerFilterItemsSource
        {
            get
            {
                return OwnerFilter.List;
            }
        }

        public Inventory Inventory
        {
            get { return (Inventory)GetValue(InventoryProperty); }
            set { SetValue(InventoryProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Inventory.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty InventoryProperty =
            DependencyProperty.Register("Inventory", typeof(Inventory), typeof(MainWindow), new PropertyMetadata(null));



        public InventoryItem SelectedInventoryItem
        {
            get { return (InventoryItem)GetValue(SelectedInventoryItemProperty); }
            set { SetValue(SelectedInventoryItemProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectedInventoryItem.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedInventoryItemProperty =
            DependencyProperty.Register("SelectedInventoryItem", typeof(InventoryItem), typeof(MainWindow), new PropertyMetadata(null));




        // Using a DependencyProperty as the backing store for SortOrder.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SortOrderProperty =
            DependencyProperty.Register("SortOrder", typeof(SortOrder), typeof(MainWindow), new PropertyMetadata(SortOrder.ProfitPerHourBuyOrder));



        // Using a DependencyProperty as the backing store for ProgressText.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ProgressTextProperty =
            DependencyProperty.Register("ProgressText", typeof(string), typeof(MainWindow), new PropertyMetadata(string.Empty));


        public int ProgressValue
        {
            get { return (int)GetValue(ProgressValueProperty); }
            set { SetValue(ProgressValueProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ProgressValue.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ProgressValueProperty =
            DependencyProperty.Register("ProgressValue", typeof(int), typeof(MainWindow), new PropertyMetadata(0));



        public EoiBlueprint SelectedBlueprint
        {
            get { return (EoiBlueprint)GetValue(SelectedBlueprintProperty); }
            set { SetValue(SelectedBlueprintProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectedBlueprint.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedBlueprintProperty =
            DependencyProperty.Register("SelectedBlueprint", typeof(EoiBlueprint), typeof(MainWindow), new PropertyMetadata(null));




        public int RequestCapacityValue
        {
            get { return (int)GetValue(RequestCapacityValueProperty); }
            set { SetValue(RequestCapacityValueProperty, value); }
        }

        // Using a DependencyProperty as the backing store for RequestCapacityValue.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RequestCapacityValueProperty =
            DependencyProperty.Register("RequestCapacityValue", typeof(int), typeof(MainWindow), new PropertyMetadata(0));



        public string RequestTimerText
        {
            get { return (string)GetValue(RequestTimerTextProperty); }
            set { SetValue(RequestTimerTextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for RequestTimerText.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RequestTimerTextProperty =
            DependencyProperty.Register("RequestTimerText", typeof(string), typeof(MainWindow), new PropertyMetadata(string.Empty));

        public MainWindow()
        {
            this.Closing += MainWindow_Closing;
            this.Loaded += MainWindow_Loaded;

            InitializeComponent();

            _progressTimer = new DispatcherTimer();
            _progressTimer.Tick += new EventHandler(ProgressTimerTick);
            _progressTimer.Interval = new TimeSpan(0, 0, 1);
            _progressTimer.Start();
        }

        private void ProgressTimerTick(object sender, EventArgs e)
        {
            var status = EoiInterface.GetAutoUpdaterStatus();
            if (status != null)
            {
                if (status.Progress == -1)
                    this.AutoUpdaterProgressBar.IsIndeterminate = true;
                else
                {
                    this.AutoUpdaterProgressBar.IsIndeterminate = false;
                    this.AutoUpdaterProgressBar.Value = status.Progress;
                }

                this.AutoUpdaterStatusTextBlock.Text = status.Status;
            }
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        private void MainWindow_Closing(object sender, CancelEventArgs e)
        {
            
        }

        private void MenuItem_ExitClick(object sender, RoutedEventArgs e)
        {
            App.Current.Shutdown();
        }

        private void CreateIndustryContractRequested(object sender, EoiBlueprint e)
        {
            IndustryContractsTabItem.IsSelected = true;
            IndustryContractsControl.CreateNewContract(e);
        }
    }
}
