using Jpinsoft.Class2GUI.Types;
using Jpinsoft.Class2GUI.WPF;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Jpinsoft.Class2GUIApp.ViewModel
{
    public class GenerateWPFViewModel : ViewModelBase
    {
        private string outputFolder;

        public string OutputFolder
        {
            get { return outputFolder; }
            set { SetProperty(ref outputFolder, value); }
        }

        public string OutputNamespace { get; set; }

        public List<IGUIGenerator> Generators { get; set; }

        public IGUIGenerator SelectedGenerator { get; set; }

        public GenerateWPFViewModel()
        {
            OutputFolder = Path.Combine(Path.GetPathRoot(Directory.GetCurrentDirectory()), "Class2GUI_Output");
            OutputNamespace = "Class2GUI";

            Generators = new List<IGUIGenerator>();
            Generators.Add(new WPFGUIGenerator());
            SelectedGenerator = Generators.First();
        }

        public void GenerateWPF()
        {
            IEnumerable<IGrouping<Type, LibraryItemViewModel>> groupedPropsByType = LibraryItems.Where(item => item.PocoProperty != null && item.GenerateThis).GroupBy(item => item.PocoProperty.DeclaringType);

            SelectedGenerator.GenerateGUIProject(OutputFolder, groupedPropsByType.Select(g => g.Key).ToList(), OutputNamespace, 4);

            Process.Start(OutputFolder);
        }

        public void SelectOutputFolder()
        {
            FolderBrowserDialog dlg = new FolderBrowserDialog();

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                OutputFolder = dlg.SelectedPath;
            }
        }
    }
}
