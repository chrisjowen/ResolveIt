using ICSharpCode.NRefactory.Ast;
using ResolveIt.Core.Model;

namespace ResolveIt.Core.Investigation
{
    public class DependencyTypeFactory
    {
        public IDependencyInfo For(INode node, ICodeFileInfo codeFile)
        {
            if (node.IsA<ObjectCreateExpression>())
            {
                var creationNode = (ObjectCreateExpression)node;
                return new DependencyInfo(creationNode.CreateType.Type, codeFile);
            }
            if (node.IsA<TypeDeclaration>())
            {
                var creationNode = (TypeDeclaration)node;
                return new DependencyInfo(creationNode.Name, codeFile);
            }
            if (node.IsA<TypeReference>())
            {
                var creationNode = (TypeReference)node;
                return new DependencyInfo(creationNode.Type, codeFile);
            }
            if (node.IsA<TemplateDefinition>())
            {
                var creationNode = (TemplateDefinition)node;
                return new DependencyInfo(creationNode.Name, codeFile);
            }
            return null;
        }
    }
}