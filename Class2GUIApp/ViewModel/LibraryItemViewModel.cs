using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Jpinsoft.Class2GUIApp.ViewModel
{
    public class LibraryItemViewModel : ViewModelBase
    {
        public Type PocoType { get; set; }

        public string PocoTypeName
        {
            get { return PocoType != null ? PocoType.FullName : string.Empty; }
        }

        public PropertyInfo PocoProperty { get; set; }

        public string PropertyName
        {
            get { return PocoProperty != null ? PocoProperty.Name : string.Empty; }
        }

        public Visibility PocoTypeVisibility
        {
            get { return PocoType != null ? Visibility.Visible : Visibility.Collapsed; }
        }

        public Visibility PropertyVisibility
        {
            get { return PocoTypeVisibility == Visibility.Collapsed ? Visibility.Visible : Visibility.Collapsed; }
        }

        private bool generateThis = true;

        public bool GenerateThis
        {
            get { return generateThis; }
            set { SetProperty(ref generateThis, value); }
        }

    }
}
