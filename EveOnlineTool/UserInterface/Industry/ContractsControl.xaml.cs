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
using EoiData.Constants;
using EoiData.EoiClasses;
using EveOnlineTool.Helper;

namespace EveOnlineTool.UserInterface.Industry
{
    /// <summary>
    /// Interaction logic for ContractControl.xaml
    /// </summary>
    public partial class ContractsControl : UserControl
    {
        
        private RelayCommand<object> _deleteAcceptedContractCommand;
        public ICommand DeleteAcceptedContractCommand
        {
            get
            {
                if (_deleteAcceptedContractCommand == null)
                {
                    _deleteAcceptedContractCommand = new RelayCommand<object>(DeleteAcceptedContract, CanDeleteAcceptedContract);
                }
                return _deleteAcceptedContractCommand;
            }
        }

        private RelayCommand<object> _finishAcceptedContractCommand;
        public ICommand FinishAcceptedContractCommand
        {
            get
            {
                if (_finishAcceptedContractCommand == null)
                {
                    _finishAcceptedContractCommand = new RelayCommand<object>(FinishAcceptedContract, CanFinishAcceptedContract);
                }
                return _finishAcceptedContractCommand;
            }
        }

        private RelayCommand<object> _deleteAvailableContractCommand;
        public ICommand DeleteAvailableContractCommand
        {
            get
            {
                if (_deleteAvailableContractCommand == null)
                {
                    _deleteAvailableContractCommand = new RelayCommand<object>(DeleteAvailableContract, CanDeleteAvailableContract);
                }
                return _deleteAvailableContractCommand;
            }
        }

        

        private RelayCommand<object> _acceptAvailableContractCommand;
        public ICommand AcceptAvailableContractCommand
        {
            get
            {
                if (_acceptAvailableContractCommand == null)
                {
                    _acceptAvailableContractCommand = new RelayCommand<object>(AcceptAvailableContract, CanAcceptAvailableContract);
                }
                return _acceptAvailableContractCommand;
            }
        }

        

        public ICollectionView AvailableContractsCollectionView
        {
            get { return (ICollectionView)GetValue(AvailableContractsCollectionViewProperty); }
            set { SetValue(AvailableContractsCollectionViewProperty, value); }
        }

        // Using a DependencyProperty as the backing store for AvailableContractsCollectionView.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AvailableContractsCollectionViewProperty =
            DependencyProperty.Register("AvailableContractsCollectionView", typeof(ICollectionView), typeof(ContractsControl), new PropertyMetadata(null));

        public ICollectionView AcceptedContractsCollectionView
        {
            get { return (ICollectionView)GetValue(AcceptedContractsCollectionViewProperty); }
            set { SetValue(AcceptedContractsCollectionViewProperty, value); }
        }

        // Using a DependencyProperty as the backing store for AcceptedContractsCollectionView.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AcceptedContractsCollectionViewProperty =
            DependencyProperty.Register("AcceptedContractsCollectionView", typeof(ICollectionView), typeof(ContractsControl), new PropertyMetadata(null));


        public ObservableCollection<EoiContract> Contracts
        {
            get { return (ObservableCollection<EoiContract>)GetValue(ContractsProperty); }
            set { SetValue(ContractsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Contracts.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ContractsProperty =
            DependencyProperty.Register("Contracts", typeof(ObservableCollection<EoiContract>), typeof(ContractsControl), new PropertyMetadata(new ObservableCollection<EoiContract>()));


        public ContractsControl()
        {
            var descriptor = DependencyPropertyDescriptor.FromProperty(DesignerProperties.IsInDesignModeProperty, typeof(FrameworkElement));
            var isDesignMode = (bool)descriptor.Metadata.DefaultValue;

            InitializeComponent();

            if (!isDesignMode)
            {
                this.Contracts = EoiInterface.GetContracts();

                var availableContractsView = new CollectionViewSource() { Source = this.Contracts };
                var acceptedContractsView = new CollectionViewSource() { Source = this.Contracts };

                var availableContractsItemList = availableContractsView.View;
                var acceptedContractsItemList = acceptedContractsView.View;

                var availableContractsFilter = new Predicate<object>(AvailableContractsFilter);
                var acceptedContractsFilter = new Predicate<object>(AcceptedContractsFilter);

                availableContractsItemList.Filter = availableContractsFilter;
                acceptedContractsItemList.Filter = acceptedContractsFilter;

                this.AvailableContractsCollectionView = availableContractsItemList;
                this.AcceptedContractsCollectionView = acceptedContractsItemList;
            }
        }

        private bool CanFinishAcceptedContract(object obj)
        {
            var selectedContract = AcceptedContractsDataGrid.SelectedItem as EoiContract;
            if (selectedContract == null)
                return false;

            if (!selectedContract.CanFinish())
                return false;

            return true;
        }

        private void FinishAcceptedContract(object obj)
        {
            var selectedContract = AcceptedContractsDataGrid.SelectedItem as EoiContract;
            if (selectedContract != null)
            {
                selectedContract.Finish();
            }
        }

        public void DeleteAcceptedContract(object parameters)
        {
            var selectedContract = AcceptedContractsDataGrid.SelectedItem as EoiContract;
            if (selectedContract != null)
            {
                selectedContract.Delete();

                AvailableContractsCollectionView.Refresh();
                AcceptedContractsCollectionView.Refresh();
            }
        }

        public void AcceptAvailableContract(object parameters)
        {
            var selectedContract = AvailableContractsDataGrid.SelectedItem as EoiContract;
            if (selectedContract != null)
            {
                ShowAcceptContractView();

                AcceptIndustryContractControl.SetContract(selectedContract);
            }
        }

        public bool CanAcceptAvailableContract(object parameters)
        {
            var selectedContract = AvailableContractsDataGrid.SelectedItem as EoiContract;
            if (selectedContract == null)
                return false;

            if (!selectedContract.CanAccept())
                return false;

            return true;
        }

        public void DeleteAvailableContract(object parameters)
        {
            var selectedContract = AvailableContractsDataGrid.SelectedItem as EoiContract;
            if (selectedContract != null)
            {
                selectedContract.Delete();

                AvailableContractsCollectionView.Refresh();
                AcceptedContractsCollectionView.Refresh();
            }
        }

        public bool CanDeleteAvailableContract(object parameters)
        {
            var selectedContract = AvailableContractsDataGrid.SelectedItem as EoiContract;
            if (selectedContract == null)
                return false;

            if (!selectedContract.CanDelete())
                return false;

            return true;
        }

        public bool CanDeleteAcceptedContract(object parameters)
        {
            var selectedContract = AcceptedContractsDataGrid.SelectedItem as EoiContract;
            if (selectedContract == null)
                return false;

            if (!selectedContract.CanDelete())
                return false;

            return true;
        }

        private bool AcceptedContractsFilter(object obj)
        {
            var result = true;

            var contract = obj as EoiContract;
            if (contract != null)
            {
                if (contract.State == ContractStates.Pending)
                    result = false;
            }

            return result;
        }

        private bool AvailableContractsFilter(object obj)
        {
            var result = true;

            var contract = obj as EoiContract;
            if (contract != null)
            {
                if (contract.State == ContractStates.Accepted || contract.State == ContractStates.Finish)
                    result = false;
            }

            return result;
        }

        internal void CreateNewContract(EoiBlueprint e)
        {
            ShowCreateContractView();

            NewIndustryContractControl.SetBlueprint(e);
        }

        private void ShowCreateContractView()
        {
            AcceptIndustryContractGroupBox.Visibility = Visibility.Collapsed;
            AvailableContractsGroupBox.Visibility = Visibility.Collapsed;
            AcceptedContractsGroupBox.Visibility = Visibility.Collapsed;
            CreateNewContractButton.Visibility = Visibility.Collapsed;
            AcceptButton.Visibility = Visibility.Collapsed;

            NewIndustryContractGroupBox.Visibility = Visibility.Visible;
            CreateButton.Visibility = Visibility.Visible;
            CancelButton.Visibility = Visibility.Visible;
        }

        private void ShowAcceptContractView()
        {
            NewIndustryContractGroupBox.Visibility = Visibility.Collapsed;
            AvailableContractsGroupBox.Visibility = Visibility.Collapsed;
            AcceptedContractsGroupBox.Visibility = Visibility.Collapsed;
            CreateNewContractButton.Visibility = Visibility.Collapsed;
            CreateButton.Visibility = Visibility.Collapsed;

            AcceptIndustryContractGroupBox.Visibility = Visibility.Visible;
            CancelButton.Visibility = Visibility.Visible;
            AcceptButton.Visibility = Visibility.Visible;
        }

        private void ShowMainView()
        {
            AcceptIndustryContractGroupBox.Visibility = Visibility.Collapsed;
            NewIndustryContractGroupBox.Visibility = Visibility.Collapsed;
            CreateButton.Visibility = Visibility.Collapsed;
            CancelButton.Visibility = Visibility.Collapsed;
            AcceptButton.Visibility = Visibility.Collapsed;

            AvailableContractsGroupBox.Visibility = Visibility.Visible;
            AcceptedContractsGroupBox.Visibility = Visibility.Visible;
            CreateNewContractButton.Visibility = Visibility.Visible;
        }

        private void CreateContractClick(object sender, RoutedEventArgs e)
        {
            ShowCreateContractView();

            NewIndustryContractControl.SetBlueprint(null);
        }

        private void CreateClick(object sender, RoutedEventArgs e)
        {
            if (NewIndustryContractControl.Create())
            {
                ShowMainView();

                AvailableContractsCollectionView.Refresh();
                AcceptedContractsCollectionView.Refresh();
            }
                
        }

        private void CancelClick(object sender, RoutedEventArgs e)
        {
            NewIndustryContractControl.Cancel();
            AcceptIndustryContractControl.Cancel();

            ShowMainView();
        }

        private void AcceptClick(object sender, RoutedEventArgs e)
        {
            if (AcceptIndustryContractControl.Accept())
            {
                ShowMainView();

                AvailableContractsCollectionView.Refresh();
                AcceptedContractsCollectionView.Refresh();
            }
                
        }
    }
}
