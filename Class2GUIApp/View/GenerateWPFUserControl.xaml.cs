using Jpinsoft.Class2GUIApp.ViewModel;
using Jpinsoft.Class2GUIApp.Wizard;
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
    /// Interaction logic for GenerateWPFUserControl.xaml
    /// </summary>
    public partial class GenerateWPFUserControl : UserControl, IWizardModule
    {
        private GenerateWPFViewModel ViewModel { get { return this.DataContext as GenerateWPFViewModel; } }

        public GenerateWPFUserControl()
        {
            InitializeComponent();
            this.DataContext = new GenerateWPFViewModel();
        }

        public void OnActivated()
        {
        }

        private void BtnGenerate_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.GenerateWPF();
        }

        private void BtnSelectFolder_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.SelectOutputFolder();
        }
    }
}
