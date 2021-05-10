using EoiData.EoiClasses;
using EveOnlineIndustrialist;
using EveOnlineIndustrialist.EoiThreadManager;
using EveOnlineIndustrialist.EveData;
using EveOnlineTool.Eve_Data;
using EveOnlineTool.Personal_Data;
using EveSwaggerConnection;
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
using System.Windows.Shapes;

namespace EveOnlineTool
{
    /// <summary>
    /// Interaction logic for Splash.xaml
    /// </summary>
    public partial class Splash : Window
    {
        public string ProgressText
        {
            get { return (string)GetValue(ProgressTextProperty); }
            set { SetValue(ProgressTextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ProgressText.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ProgressTextProperty =
            DependencyProperty.Register("ProgressText", typeof(string), typeof(Splash), new PropertyMetadata(string.Empty));



        public int ProgressValue
        {
            get { return (int)GetValue(ProgressValueProperty); }
            set { SetValue(ProgressValueProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ProgressValue.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ProgressValueProperty =
            DependencyProperty.Register("ProgressValue", typeof(int), typeof(Splash), new PropertyMetadata(0));
        private EoiThread _backgroundWorker;
        private bool _threadManagerInitialized;

        public Splash()
        {
            InitializeComponent();

            Loaded += Splash_Loaded;
            Closing += Splash_Closing;
        }

        private void Splash_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (_backgroundWorker != null)
            {
                EoiInterface.StopBackgroundWorker();
                _backgroundWorker.Thread.Join();

                EoiInterface.Close();
            }
            
            if (_threadManagerInitialized)
                EoiThreadManager.ThreadManager.Stop();
        }

        private void Splash_Loaded(object sender, RoutedEventArgs e)
        {
            var version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
            if (!EoiInterface.IsVersionValid(version))
            {
                MessageBox.Show("Die Version konnte nicht validiert werden. Mögliche Ursachen: Version zu alt bzw. ungültig, Keine Internet Verbindung, Server nicht erreichbar");

                App.Current.Shutdown();
                return;
            }

            EoiThreadManager.Init();
            _threadManagerInitialized = true;

            var init = new EoiThread(EoiInterface.Init);
            EoiThreadManager.ThreadManager.ThreadFinished += (thread, args) =>
            {
                if (Equals(init, thread))
                {
                    OnStartupFinish();
                }
            };
            EoiThreadManager.ThreadManager.Add(init);
        }

        private void OnStartupFinish()
        {
            _backgroundWorker = new EoiThread(EoiInterface.StartBackgroundWorker, System.Threading.Thread.CurrentThread);
            EoiThreadManager.ThreadManager.Add(_backgroundWorker);
            
            this.Visibility = Visibility.Collapsed;

            var mainWindow = new MainWindow();
            mainWindow.Owner = this;
            mainWindow.Closed += MainWindow_Closed;
            mainWindow.Show();
        }

        private void MainWindow_Closed(object sender, EventArgs e)
        {
            App.Current.Shutdown();
        }
    }
}
