using ICSharpCode.NRefactory.Ast;

namespace WellItCouldWork.Investigation
{
    public class TypeFactory
    {
        public TypeInfo For(INode node)
        {
            if (node.IsA<ObjectCreateExpression>())
            {
                var creationNode = (ObjectCreateExpression)node;
                return new TypeInfo(creationNode.CreateType.Type);
            }
            if (node.IsA<TypeDeclaration>())
            {
                var creationNode = (TypeDeclaration)node;
                return new TypeInfo(creationNode.Name);
            }
            if (node.IsA<TypeReference>())
            {
                var creationNode = (TypeReference)node;
                return new TypeInfo(creationNode.Type);
            }
            if (node.IsA<TemplateDefinition>())
            {
                var creationNode = (TemplateDefinition)node;
                return new TypeInfo(creationNode.Name);
            }
            return null;
        }
    }
}