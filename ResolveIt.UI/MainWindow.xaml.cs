using System;
using System.Windows;
using Microsoft.Win32;
using ResolveIt.Core.Model;

namespace ResolveIt.UI
{
    public partial class MainWindow
    {
        private readonly ApplicationWiringFactory wiring = new ApplicationWiringFactory();

        public MainWindow()
        {
            InitializeComponent();
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

            Declerations.DataContext = new { 
                Dependencies = dependencies, 
                selectedCodeFile.Declerations, 
                selectedCodeFile.Project.References };
        }

        private void LoadTree(string filePath)
        {
            var solutionLoader = wiring.GetSolutionLoader();
            var solutionInfo = solutionLoader.FromFile(filePath);
            SolutionTreeView.DataContext = solutionInfo;
        }
    }
}
