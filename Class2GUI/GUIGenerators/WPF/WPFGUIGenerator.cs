using Jpinsoft.Class2GUI;
using Jpinsoft.Class2GUI.GUIGenerators.WPF;
using Jpinsoft.Class2GUI.GUIGenerators.WPF.GControls;
using Jpinsoft.Class2GUI.Properties;
using Jpinsoft.Class2GUI.Types;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Jpinsoft.Class2GUI.WPF
{
    public class WPFGUIGenerator : IGUIGenerator
    {
        #region Fields And Const

        public const string CN_VIEW_FOLDER = "View";
        public const string CN_VM_FOLDER = "ViewModel";
        public const string CN_VM_BASE_FILE = "ViewModelBase.cs";
        public const string CN_DISPLAYNAMECONVERTER_FILE = "DisplayNameConverter.cs";

        public const string CN_PROJ_TEMPLATE = "GeneratedWPF";

        private string outNamespace;

        public List<Type> TargetTypes { get; private set; }

        public string Description { get { return "WPFGUIGenerator is a straightforward UI generator. This tool generate a WPF project (with use MVVM) from .NET library."; } }

        #endregion

        #region IGUIGenerator

        public void GenerateGUIProject(string outFolder, List<Type> targetTypes, string outNamespace, byte maxRecursionLevel)
        {
            StringBuilder csProjItemsGroup = new StringBuilder();

            this.outNamespace = outNamespace;
            this.TargetTypes = targetTypes;

            List<Type> renderedTypes = new List<Type>();
            Dictionary<string, StringBuilder> res = new Dictionary<string, StringBuilder>();

            foreach (Type tarType in targetTypes)
            {
                this.GenerateGUIFiles(res, tarType, outNamespace, maxRecursionLevel);
            }

            UnpackProj(targetTypes, outFolder, outNamespace);

            this.RenderFiles(res, Path.Combine(outFolder, CN_PROJ_TEMPLATE), csProjItemsGroup);

            AddItemsToProj(csProjItemsGroup, outFolder);
        }

        public void GenerateGUIFiles(Dictionary<string, StringBuilder> outputFiles, Type targetType, string outNamespace, byte maxRecursionLevel)
        {
            if (targetType.IsGenericType)
                throw new NotImplementedException("Generic Types are not supported.");

            List<Type> renderedTypes = new List<Type>();

            WPFClass2GUIBuilder wpfGen = new WPFClass2GUIBuilder();
            wpfGen.BuildControls(targetType, targetType.GetProperties().ToList(), outNamespace, maxRecursionLevel);

            foreach (WPFClass2GUIBuilder resWpfGen in wpfGen.InnerBuilders)
            {
                resWpfGen.Render();

                string xamlFile = $"{CN_VIEW_FOLDER}\\{resWpfGen.TargetType.Name}Window.xaml";
                string xamlCS = $"{CN_VIEW_FOLDER}\\{resWpfGen.TargetType.Name}Window.xaml.cs";
                string vmFile = $"{CN_VM_FOLDER}\\{resWpfGen.TargetType.Name}ViewModel.cs";

                outputFiles.AddIfNotContainsKey($"{CN_VM_BASE_FILE}", resWpfGen.RenderOutput.ViewModelBase);
                outputFiles.AddIfNotContainsKey($"{CN_DISPLAYNAMECONVERTER_FILE}", resWpfGen.RenderOutput.DisplayNameConverter);

                outputFiles.AddIfNotContainsKey(xamlFile, resWpfGen.RenderOutput.Xaml);
                outputFiles.AddIfNotContainsKey(xamlCS, resWpfGen.RenderOutput.XamlCodebehind);
                outputFiles.AddIfNotContainsKey(vmFile, resWpfGen.RenderOutput.ViewModel);
            }
        }

        #endregion

        #region Internal

        private void UnpackProj(IEnumerable<Type> generatedTypes, string outFolder, string outNamespace)
        {
            using (MemoryStream ms = new MemoryStream(Resources_WPFGenerator.GeneratedWPF))
            {
                new ZipArchive(ms).ExtractToDirectory(outFolder);
            }

            // Rename namespaces
            FormatFileString("App.xaml", outFolder, outNamespace);
            FormatFileString("App.xaml.cs", outFolder, outNamespace);

            // Main Window Buttons
            StringBuilder sbMainVindowXAMLButtons = new StringBuilder();
            StringBuilder sbMainVindowCSButtons = new StringBuilder();

            foreach (Type type in generatedTypes)
            {
                sbMainVindowXAMLButtons.AppendFormat(Resources_WPFGenerator.T035, type.Name);
                sbMainVindowXAMLButtons.AppendLine();

                sbMainVindowCSButtons.AppendFormat(Resources_WPFGenerator.T034, type.Name);
                sbMainVindowCSButtons.AppendLine();
            }

            FormatFileString("MainWindow.xaml", outFolder, outNamespace, sbMainVindowXAMLButtons.ToString());
            FormatFileString("MainWindow.xaml.cs", outFolder, outNamespace, sbMainVindowCSButtons.ToString());

            FormatFileString("Properties\\AssemblyInfo.cs", outFolder, outNamespace);
            FormatFileString("Properties\\Resources.Designer.cs", outFolder, outNamespace);
            FormatFileString("Properties\\Settings.Designer.cs", outFolder, outNamespace);
        }

        private void RenderFiles(Dictionary<string, StringBuilder> files, string outFolder, StringBuilder csProjItemsGroup)
        {
            if (!Directory.Exists(outFolder))
                Directory.CreateDirectory(outFolder);

            string viewFolder = Path.Combine(outFolder, CN_VIEW_FOLDER);

            if (!Directory.Exists(viewFolder))
                Directory.CreateDirectory(viewFolder);

            string vmFolder = Path.Combine(outFolder, CN_VM_FOLDER);

            if (!Directory.Exists(vmFolder))
                Directory.CreateDirectory(vmFolder);

            foreach (var item in files)
            {
                File.WriteAllText(Path.Combine(outFolder, item.Key), item.Value.ToString(), Encoding.Unicode);

                if (item.Key.EndsWith(".xaml"))
                {
                    csProjItemsGroup.AppendLine(string.Format(Resources_WPFGenerator.T030, item.Key));

                    // Add XAML Codebehind
                    // TODO ID NOT CONTAINS...
                    csProjItemsGroup.AppendLine(string.Format(Resources_WPFGenerator.T031, item.Key + ".cs", item.Key.Split('\\')[1]));
                }
                else if (!item.Key.EndsWith(".xaml.cs"))
                {
                    // Add ViewModelBase, DisplayNameConverter to CSProj And ViewModel.cs
                    // Add VM file to CSProj
                    csProjItemsGroup.AppendLine(string.Format(Resources_WPFGenerator.T032, item.Key));
                }
            }
        }

        private void FormatFileString(string fileName, string outFolder, params string[] args)
        {
            string fullPath = Path.Combine(outFolder, CN_PROJ_TEMPLATE, fileName);
            File.WriteAllText(fullPath, string.Format(File.ReadAllText(fullPath), args));
        }

        private void AddItemsToProj(StringBuilder csProjItemsGroup, string outFolder)
        {
            Assembly assmebly = this.TargetTypes.First().Assembly;

            string projFolderPath = Path.Combine(outFolder, CN_PROJ_TEMPLATE);

            File.Copy(assmebly.Location, Path.Combine(projFolderPath, Path.GetFileName(assmebly.Location)));

            string projFilePath = Path.Combine(projFolderPath, $"{CN_PROJ_TEMPLATE}.csproj");
            string newProjFilePath = Path.Combine(projFolderPath, this.outNamespace + ".csproj");
            string projFile = File.ReadAllText(projFilePath);

            File.Delete(projFilePath);

            File.WriteAllText(newProjFilePath, string.Format(projFile, "{" + Guid.NewGuid() + "}", assmebly.GetName(), Path.GetFileName(assmebly.Location), csProjItemsGroup, this.outNamespace));

            // UPDATE SLN
            string slnFilePath = Path.Combine(outFolder, $"{CN_PROJ_TEMPLATE}Sln.sln");
            string slnFile = File.ReadAllText(slnFilePath);

            File.WriteAllText(slnFilePath, string.Format(slnFile, this.outNamespace));

            // Rename Project Dir
            string newProjFolder = Path.Combine(outFolder, this.outNamespace);

            if (!Directory.Exists(newProjFolder))
                Directory.Move(projFolderPath, newProjFolder);
        }

        #endregion
    }
}
