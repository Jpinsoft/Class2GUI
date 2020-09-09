using Jpinsoft.Class2GUIApp.Helpers;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;

namespace Jpinsoft.Class2GUIApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            CatchExeption(e.Exception);
            e.Handled = true;
        }

        public static void CatchExeption(Exception ex)
        {
            if (ex is ReflectionTypeLoadException)
            {
                string message = "Unable to load dll:";

                if (((ReflectionTypeLoadException)ex).LoaderExceptions != null)
                    message += string.Join(Environment.NewLine, ((ReflectionTypeLoadException)ex).LoaderExceptions.Select(exception => exception.Message));

                MessageBoxWPF.ShowError(Application.Current.MainWindow, MessageBoxButton.OK, message);
            }
            else
            {
                MessageBoxWPF.ShowError(Application.Current.MainWindow, MessageBoxButton.OK, "An unhandled exception just occurred: " + ex.Message);
            }
        }
    }
}
