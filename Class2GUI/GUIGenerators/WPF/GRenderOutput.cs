using Jpinsoft.Class2GUI.GUIGenerators.WPF.GControls;
using Jpinsoft.Class2GUI.Types;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Jpinsoft.Class2GUI.GUIGenerators.WPF
{
    public class GRenderOutput
    {
        public List<GControl> Controls { get; set; }

        public StringBuilder Xaml { get; set; }

        public StringBuilder XamlCodebehind { get; set; }

        public StringBuilder ViewModel { get; set; }

        public StringBuilder ViewModelBase { get; set; }

        public StringBuilder DisplayNameConverter { get; set; }

        public StringBuilder GridRows { get; set; }

        public StringBuilder XamlResources { get; set; }

        public StringBuilder XamlWindowAttributes { get; set; }

        public StringBuilder CsUsingReferences { get; set; }

        public List<string> LookUpSourceInViewModel { get; set; }

        public List<Type> EnumObjectDataProviderTypes { get; set; }

        public int rowNum = 0;
        public int colNum = 0;
        public int tabIndex = 10;

        /// <summary>
        /// Kluc je namespace + assembly, hodnota je prefix
        /// </summary>
        public Dictionary<string, NamespaceInfo> NamespacePrefixTable { get; private set; }

        public GRenderOutput()
        {
            ViewModel = new StringBuilder();
            ViewModelBase = new StringBuilder();
            Xaml = new StringBuilder();
            XamlCodebehind = new StringBuilder();
            DisplayNameConverter = new StringBuilder();
            GridRows = new StringBuilder();
            Controls = new List<GControl>();
            XamlResources = new StringBuilder();
            XamlWindowAttributes = new StringBuilder();
            CsUsingReferences = new StringBuilder();
            LookUpSourceInViewModel = new List<string>();
            NamespacePrefixTable = new Dictionary<string, NamespaceInfo>();
            EnumObjectDataProviderTypes = new List<Type>();
        }

    }
}
