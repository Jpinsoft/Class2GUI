using Jpinsoft.Class2GUI.Types;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jpinsoft.Class2GUIApp.ViewModel
{
    public class LibraryDetailViewModel : ViewModelBase
    {
        public LibraryDetailViewModel()
        {
        }

        public void OnActivated()
        {
            if (base.LibraryItems.Count == 0)
            {
                foreach (GeneratedTypeInfo pocoType in base.PocoTypes)
                {
                    LibraryItems.Add(new LibraryItemViewModel { PocoType = pocoType.TypeInfo });

                    IEnumerable<LibraryItemViewModel> lst = pocoType.Properties.Select(p => new LibraryItemViewModel { PocoProperty = p.PocoProperty });

                    foreach (LibraryItemViewModel item in lst)
                        base.LibraryItems.Add(item);
                }
            }
        }

        public void PocoTypeCheckedChange(LibraryItemViewModel pocoTypeInfo, bool isChecked)
        {
            LibraryItems.Where(t => t.PocoProperty != null && t.PocoProperty.ReflectedType == pocoTypeInfo.PocoType).ToList()
                .ForEach(t => t.GenerateThis = pocoTypeInfo.GenerateThis);
        }

    }
}
