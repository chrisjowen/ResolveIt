using ResolveIt.UI.TreeControl.Domain;

namespace ResolveIt.UI.TreeControl
{
    public partial class SolutionTreeView
    {
        public SolutionTreeView()
        {
            InitializeComponent();

            const string path = @"c:\";
            var solutionModel = new SolutionModel("Some solution", path);
            var projectModel = new ProjectModel("Project.UI", path, solutionModel );
            projectModel.Files.Add(new FileModel("Some file", path, projectModel));
            solutionModel.Projects.Add(projectModel);
            solutionModel.Projects.Add(new ProjectModel("Project.Web", path, solutionModel ));
            DataContext = solutionModel;
        }
    }
}
