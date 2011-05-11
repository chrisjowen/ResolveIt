using ICSharpCode.NRefactory.Ast;
using ResolveIt.Core.Model;

namespace ResolveIt.Core.Investigation
{
    public class DependencyTypeFactory
    {
        public IDependencyInfo For(INode node)
        {
            if (node.IsA<ObjectCreateExpression>())
            {
                var creationNode = (ObjectCreateExpression)node;
                return new DependencyInfo(creationNode.CreateType.Type);
            }
            if (node.IsA<TypeReference>())
            {
                var creationNode = (TypeReference)node;
                return new DependencyInfo(creationNode.Type);
            }
            if (node.IsA<TemplateDefinition>())
            {
                var creationNode = (TemplateDefinition)node;
                return new DependencyInfo(creationNode.Name);
            }
            return null;
        }

        public IDeclerationInfo DeclerationFor(INode node, ICodeFileInfo codeFile)
        {
            if (node.IsA<TypeDeclaration>())
            {
                var creationNode = (TypeDeclaration)node;
                return new DeclerationInfo(creationNode.Name, codeFile);
            }
            return null;
        }
    }
}