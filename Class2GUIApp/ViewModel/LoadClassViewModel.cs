using Jpinsoft.Class2GUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jpinsoft.Class2GUIApp.ViewModel
{
    public class LoadClassViewModel : ViewModelBase
    {
        public LoadClassViewModel()
        {
        }

        private string infoText = $"Library is not Loaded.{Environment.NewLine}Click here for Load .NET DLL or Drag .NET DLL file to this Drop Zone.";

        public string InfoText
        {
            get { return infoText; }
            set { SetProperty<string>(ref infoText, value); }
        }

        public void Activated()
        {
        }

        public void LoadDLL(string fileName)
        {
            if (string.IsNullOrEmpty(fileName) || !fileName.ToLower().EndsWith(".dll"))
                return;

            base.PocoTypes = ClassToGUITools.LoadPocoLibrary(fileName);
            base.LibraryItems.Clear();

            InfoText = $"Library '{fileName}' was succesfully loaded.";
            MainViewModel.IsNextBtnEnabled = true;
        }
    }
}
