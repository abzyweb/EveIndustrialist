using EoiData.EoiClasses;
using EoiData.WebDataClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
using System.Windows.Threading;

namespace EveOnlineTool.UserInterface.Settings
{
    /// <summary>
    /// Interaction logic for UserControl.xaml
    /// </summary>
    public partial class EoiUserControl : UserControl
    {
        private DispatcherTimer _dispatcherTimer;
        private Guid _guid;
        private EoiUser _userToAuthenticate;
        private string _code;

        public EoiUser User
        {
            get { return (EoiUser)GetValue(UserProperty); }
            set { SetValue(UserProperty, value); }
        }

        // Using a DependencyProperty as the backing store for User.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty UserProperty =
            DependencyProperty.Register("User", typeof(EoiUser), typeof(EoiUserControl), new PropertyMetadata(null));

        public EoiUserControl()
        {
            InitializeComponent();
        }

        private void OnButtonClick_EveOnlineLogin(object sender, RoutedEventArgs e)
        {
            if (User == null || User.Authenticated)
                return;

            // var url = @"http://yourUrlHere.com/EveOnline/Authentication/?action=start&guid=";
            var url = @"http://www.mobilies.at/EveOnline/Authentication/?action=start&guid=";
            _guid = Guid.NewGuid();
            url += WebUtility.UrlEncode(_guid.ToString());

            System.Diagnostics.Process.Start(url);

            _dispatcherTimer = new DispatcherTimer();
            _dispatcherTimer.Tick += new EventHandler(Timer_Tick);
            _dispatcherTimer.Interval = new TimeSpan(0, 0, 5);
            _dispatcherTimer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            _code = WebDataInterface.GetAuthenticationCode(_guid);
            if (!string.IsNullOrWhiteSpace(_code))
            {
                if (_dispatcherTimer != null)
                {
                    _dispatcherTimer.Stop();
                    _dispatcherTimer = null;
                }

                WebDataInterface.GetUserAccessToken(_code);
            }
        }
    }
}
