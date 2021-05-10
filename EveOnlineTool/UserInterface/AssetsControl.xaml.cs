using EoiData.EoiClasses;
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

namespace EveOnlineTool.UserInterface
{
    /// <summary>
    /// Interaction logic for Assets.xaml
    /// </summary>
    public partial class AssetsControl : UserControl
    {
        public ObservableCollection<EoiAsset> Assets { get; private set; }

        public ICollectionView AssetsCollectionView
        {
            get { return (ICollectionView)GetValue(AssetsCollectionViewProperty); }
            set { SetValue(AssetsCollectionViewProperty, value); }
        }

        // Using a DependencyProperty as the backing store for AssetsCollectionView.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AssetsCollectionViewProperty =
            DependencyProperty.Register("AssetsCollectionView", typeof(ICollectionView), typeof(AssetsControl), new PropertyMetadata(null));

        public EoiAsset SelectedAsset
        {
            get { return (EoiAsset)GetValue(SelectedAssetProperty); }
            set { SetValue(SelectedAssetProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectedAsset.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedAssetProperty =
            DependencyProperty.Register("SelectedAsset", typeof(EoiAsset), typeof(AssetsControl), new PropertyMetadata(null));

        public AssetsControl()
        {
            var descriptor = DependencyPropertyDescriptor.FromProperty(DesignerProperties.IsInDesignModeProperty, typeof(FrameworkElement));
            var isDesignMode = (bool)descriptor.Metadata.DefaultValue;

            InitializeComponent();

            if (!isDesignMode)
            {
                this.Assets = EoiInterface.GetAssets();
                var collectionView = new CollectionViewSource() { Source = this.Assets };
                var itemList = collectionView.View;
                var filter = new Predicate<object>(CustomFilter);
                itemList.Filter = filter;
                itemList.GroupDescriptions.Clear();
                itemList.GroupDescriptions.Add(new PropertyGroupDescription("Name"));
                this.AssetsCollectionView = itemList;
            }
        }

        private bool CustomFilter(object obj)
        {
            var result = true;
            var asset = obj as EoiAsset;

            return result;
        }
    }
}
