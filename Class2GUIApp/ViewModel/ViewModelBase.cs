using Jpinsoft.Class2GUI.Types;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Jpinsoft.Class2GUIApp.ViewModel
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        #region Fields And Props

        private static List<GeneratedTypeInfo> pocoTypes = new List<GeneratedTypeInfo>();
        private static ObservableCollection<LibraryItemViewModel> libraryItems = new ObservableCollection<LibraryItemViewModel>();
        private static MainWindowViewModel mainViewModel;

        public event PropertyChangedEventHandler PropertyChanged;

        public List<GeneratedTypeInfo> PocoTypes
        {
            get { return pocoTypes; }
            set { pocoTypes = value; }
        }

        public ObservableCollection<LibraryItemViewModel> LibraryItems
        {
            get { return libraryItems; }
            set { libraryItems = value; }
        }

        public MainWindowViewModel MainViewModel
        {
            get { return mainViewModel; }
            set { mainViewModel = value; }
        }

        #endregion


        #region Property changed

        protected virtual bool SetProperty<T>(ref T storage, T value,
            [CallerMemberName] string propertyName = null)
        {
            if (Equals(storage, value)) return false;

            storage = value;
            OnPropertyChanged(propertyName);

            return true;
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var eventHandler = PropertyChanged;
            if (eventHandler != null)
            {
                eventHandler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion
    }
}
