using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ICSharpCode.NRefactory.Ast;
using WellItCouldWork.SyntaxHelpers;

namespace WellItCouldWork.Investigation
{
    public static class NodeExt
    {
        public static bool HasChildren(this INode node)
        {
            return node.Children.Count > 0;
        }

        public static IEnumerable<T> OfType<T>(this IList<INode> nodes)
        {
            return nodes.Where(node => node is T).Cast<T>();
        }

        public static bool IsA<T>(this INode node)
        {
            return node.GetType()==typeof(T);
        }

        public static IEnumerable<INode> Flatten(this INode node)
        {
            return SubNodesFor(node);
        }

        private static IList<INode> SubNodesFor(INode node)
        {
            var returnNodes = new List<INode>();
            var properties = PropertiesOfTypeINodeFor(node, "Parent");

            var nodes = properties.SelectMany(p =>
            {
                var property = p.GetValue(node, null);
                return property is INode
                    ? new List<INode> { (INode)property }
                    : property as IEnumerable<INode>;
            });

            returnNodes.AddRange(nodes);

            if (nodes.Count() > 0)
                returnNodes.AddRange(nodes.SelectMany(SubNodesFor));

            return returnNodes.ToList();
        }

        private static IEnumerable<PropertyInfo> PropertiesOfTypeINodeFor(INode node, params string[] excluding)
        {
            return node.GetType()
                .GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                .Where(p => p.PropertyType.IsNodeOrNodeList())
                .Where(p => !excluding.Contains(p.Name));
        }
    }
}