using System;
using System.Linq;
using System.Windows;
using Microsoft.Win32;
using ResolveIt.Core.BuildCreation;
using ResolveIt.Core.Model;

namespace ResolveIt.UI
{
    public partial class MainWindow
    {
        private readonly ApplicationWiringFactory wiring = new ApplicationWiringFactory();
        private BuildCodeFiles filesToBuild = null;

        public MainWindow()
        {
            InitializeComponent();
            DependencyViewer.BuilderWindow.BuildButton.Click += BuildButtonClick;
        }

        private void BuildButtonClick(object sender, RoutedEventArgs e)
        {
            if (filesToBuild == null) return;
            var compilationResult = wiring.GetBuildFileCompiler().Compile(filesToBuild);
            if(!compilationResult.HasErrors)
            {
                var builderWindow = DependencyViewer.BuilderWindow;
                builderWindow.AssemblyLocation.Text = compilationResult.AssemblyLocation;
            }
            else
            {
                MessageBox.Show(String.Join(",", compilationResult.Errors));
            }
        }

        private void LoadSolution(object sender, RoutedEventArgs e)
        {
            SolutionTreeView.ProjectTree.SelectedItemChanged += ProjectTreeSelectedItemChanged;
            var dialog = new OpenFileDialog();
            if (dialog.ShowDialog() == true)
            {
                LoadTree(dialog.FileName);
            }
        }

        private void ProjectTreeSelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            var selectedItem = SolutionTreeView.ProjectTree.SelectedItem as ICodeFileInfo;
            if(selectedItem!=null)
            {
                ResolveDependenciesFor(selectedItem);
            }
        }

        private void ResolveDependenciesFor(ICodeFileInfo selectedCodeFile)
        {
            var dependencies = wiring.GetDependencyExaminer().ExamineSource(selectedCodeFile, selectedCodeFile.Solution);

            DependencyViewer.DataContext = new
            { 
                Dependencies = dependencies.Where(d => !d.IsExternal), 
                selectedCodeFile.Declerations, 
                selectedCodeFile.Project.References };

            filesToBuild = new BuildCodeFiles(selectedCodeFile, dependencies, selectedCodeFile.Project.References);
        }

        private void LoadTree(string filePath)
        {
            var solutionLoader = wiring.GetSolutionLoader();
            var solutionInfo = solutionLoader.FromFile(filePath);
            SolutionTreeView.DataContext = solutionInfo;
        }
    }
}
