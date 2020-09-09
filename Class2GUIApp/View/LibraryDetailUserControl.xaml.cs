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
    /// Interaction logic for LibraryDetailUserControl.xaml
    /// </summary>
    public partial class LibraryDetailUserControl : UserControl, IWizardModule
    {
        public LibraryDetailViewModel ViewModel { get { return this.DataContext as LibraryDetailViewModel; } }

        public LibraryDetailUserControl()
        {
            InitializeComponent();
            this.DataContext = new LibraryDetailViewModel();
        }

        private void CheckBoxZone_Click(object sender, RoutedEventArgs e)
        {
            CheckBox chb = ((CheckBox)sender);

            if (chb.IsChecked.HasValue)
                ViewModel.PocoTypeCheckedChange((LibraryItemViewModel)chb.DataContext, chb.IsChecked.Value);
        }

        public void OnActivated()
        {
            ViewModel.OnActivated();
        }

        public bool NextEnabled
        {
            get { return true; }
        }

        private void listBoxZone_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                LibraryItemViewModel selectedItem = listBoxZone.SelectedItem as LibraryItemViewModel;

                if (selectedItem != null)
                {
                    selectedItem.GenerateThis = !selectedItem.GenerateThis;

                    if (selectedItem.PocoType != null)
                        ViewModel.PocoTypeCheckedChange(selectedItem, selectedItem.GenerateThis);

                    e.Handled = true;
                }
            }
        }
    }
}
