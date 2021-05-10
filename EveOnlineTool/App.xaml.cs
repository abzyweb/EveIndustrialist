using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace EveOnlineIndustrialist
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        static App()
        {
            AppDomain.CurrentDomain.UnhandledException -= Static_UnhandledException;
            AppDomain.CurrentDomain.UnhandledException += Static_UnhandledException;
        }

        private void App_OnStartup(object sender, StartupEventArgs e)
        {
            
            AppDomain.CurrentDomain.UnhandledException -= Static_UnhandledException;

            AppDomain.CurrentDomain.UnhandledException -= CurrentDomain_UnhandledException;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
        }

        private static void Static_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            if (e.ExceptionObject is OutOfMemoryException)
            {
                MessageBox.Show("Das Programm ist an die Grenze des vorhandenen Arbeitsspeichers gestoßen.");
            }
            else
            {
                var ex = (e.ExceptionObject as Exception);

                while (ex != null)
                {
                    LogException(ex);

                    if (ex.InnerException != null)
                        ex = ex.InnerException;
                    else
                        ex = null;
                }
                
                MessageBox.Show("Es kam zu einem unerwarteten Fehler: " + e.ToString());
            }
        }
        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            if (e.ExceptionObject is OutOfMemoryException)
            {
                MessageBox.Show("Das Programm ist an die Grenze des vorhandenen Arbeitsspeichers gestoßen.");
            }
            else
            {
                var ex = (e.ExceptionObject as Exception);
                while (ex != null)
                {
                    LogException(ex);

                    if (ex.InnerException != null)
                        ex = ex.InnerException;
                    else
                        ex = null;
                }

                MessageBox.Show("Es kam zu einem unerwarteten Fehler: " + e.ToString());
            }
        }

        private static void LogException(Exception ex)
        {
            var assemblyLocation = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            var path = Path.Combine(assemblyLocation, "logfile.log");

            using (StreamWriter writer = new StreamWriter(path, true))
            {
                writer.WriteLine("-----------------------------------------------------------------------------");
                writer.WriteLine("Date : " + DateTime.Now.ToString());
                writer.WriteLine();

                while (ex != null)
                {
                    writer.WriteLine(ex.GetType().FullName);
                    writer.WriteLine("Message : " + ex.Message);
                    writer.WriteLine("StackTrace : " + ex.StackTrace);

                    ex = ex.InnerException;
                }
            }
        }
    }
}
