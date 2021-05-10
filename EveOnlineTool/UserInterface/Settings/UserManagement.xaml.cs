using EoiData.EoiClasses;
using EoiData.WebDataClasses;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
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
using System.Windows.Threading;

namespace EveOnlineTool.UserInterface.Settings
{
    /// <summary>
    /// Interaction logic for UserManagement.xaml
    /// </summary>
    public partial class UserManagement : UserControl
    {
        public ObservableCollection<EoiUser> Users
        {
            get { return (ObservableCollection<EoiUser>)GetValue(UsersProperty); }
            set { SetValue(UsersProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Users.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty UsersProperty =
            DependencyProperty.Register("Users", typeof(ObservableCollection<EoiUser>), typeof(UserManagement), new PropertyMetadata(new ObservableCollection<EoiUser>()));

        public EoiUser SelectedUser
        {
            get { return (EoiUser)GetValue(SelectedUserProperty); }
            set { SetValue(SelectedUserProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectedUser.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedUserProperty =
            DependencyProperty.Register("SelectedUser", typeof(EoiUser), typeof(UserManagement), new PropertyMetadata(null));



        public UserManagement()
        {
            Users = EoiInterface.GetUsers();

            InitializeComponent();
        }


    }
}
