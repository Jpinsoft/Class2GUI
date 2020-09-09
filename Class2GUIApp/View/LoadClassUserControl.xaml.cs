using Jpinsoft.Class2GUIApp.ViewModel;
using Jpinsoft.Class2GUIApp.Wizard;
using Microsoft.Win32;
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

namespace Jpinsoft.Class2GUIApp.View
{
    /// <summary>
    /// Interaction logic for LoadClassUserControl.xaml
    /// </summary>
    public partial class LoadClassUserControl : UserControl, IWizardModule
    {
        public LoadClassViewModel ViewModel
        {
            get { return this.DataContext as LoadClassViewModel; }
        }

        public LoadClassUserControl()
        {
            InitializeComponent();
            this.DataContext = new LoadClassViewModel();
        }

        public void OnActivated()
        {
            ViewModel.Activated();
            return;
        }

        private void BtnLoadDll_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            if (openFileDialog.ShowDialog() == true)
                ViewModel.LoadDLL(openFileDialog.FileName);
        }

        private void BorderLoadLibrary_Drop(object sender, DragEventArgs e)
        {
            try
            {
                if (e.Data.GetDataPresent(DataFormats.FileDrop))
                {
                    ViewModel.LoadDLL((e.Data.GetData(DataFormats.FileDrop) as string[])?.FirstOrDefault());
                }
            }
            catch (Exception ex)
            {
                App.CatchExeption(ex);
            }
        }
    }
}
