using System.Linq;

namespace Assets.Scripts.Nodes
{
    public static class NodeExtensions
    {
        public static void InitializeFrom(this Node node, NodeRawData rowData)
        {
            node.Id = rowData.Id;
            node.Name = rowData.Name;
            node.Description = rowData.Description;
            node.Type = rowData.Type;
            node.EdgesRawData = rowData.Edges.ToArray();
        }
    }
}