using Jpinsoft.Class2GUIApp.Properties;
using Jpinsoft.Class2GUIApp.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Jpinsoft.Class2GUIApp.Wizard;
using Jpinsoft.Class2GUIApp.View;

namespace Jpinsoft.Class2GUIApp.ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {
        public string T001 { get { return Resources.T001; } }

        public string T002 { get { return Resources.T002; } }

        private IWizardModule currentContentView;

        public IWizardModule CurrentContentView
        {
            get { return currentContentView; }
            set
            {
                SetProperty(ref currentContentView, value);
            }
        }

        private bool isNextBtnEnabled;

        public bool IsNextBtnEnabled
        {
            get { return isNextBtnEnabled; }
            set { SetProperty(ref isNextBtnEnabled, value); }
        }

        private bool isPrevtBtnEnabled;

        public bool IsPrevBtnEnabled
        {
            get { return isPrevtBtnEnabled; }
            set { SetProperty(ref isPrevtBtnEnabled, value); }
        }

        public List<IWizardModule> WizardModules { get; set; }

        public bool IsNextEnabled { get; set; } = true;

        public bool IsPrevEnabled { get; set; } = true;

        public MainWindowViewModel()
        {
            base.MainViewModel = this;

            WizardModules = new List<IWizardModule> {
                new LoadClassUserControl(),
                new LibraryDetailUserControl(),
                new GenerateWPFUserControl()
            };

            ActivateModule(WizardModules.First(), false, false);
        }

        public void NextStep()
        {
            ActivateModule(WizardModules[WizardModules.IndexOf(CurrentContentView) + 1], true, true);

            if (WizardModules.IndexOf(CurrentContentView) + 1 >= WizardModules.Count)
            {
                IsNextBtnEnabled = false;
                return;
            }
        }

        public void PrevStep()
        {
            ActivateModule(WizardModules[WizardModules.IndexOf(CurrentContentView) - 1], true, true);

            if (CurrentContentView == WizardModules.First())
            {
                IsPrevBtnEnabled = false;
                return;
            }
        }

        private void ActivateModule(IWizardModule wizardModule, bool isNextBtnEnabled, bool isPrevBtnEnabled)
        {
            IsNextBtnEnabled = isNextBtnEnabled;
            IsPrevBtnEnabled = isPrevBtnEnabled;

            CurrentContentView = wizardModule;
            CurrentContentView.OnActivated();
        }
    }
}
