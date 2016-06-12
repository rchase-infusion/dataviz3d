using UnityEngine;

namespace Assets.Scripts.Edges
{
    public static class NodeExtensions
    {
        public static void InitializeFrom(this Edge edge, EdgeRawData rowData, GameObject parent, GameObject child)
        {
            edge.ParentNode = parent;
            edge.ChildNode = child;
            edge.Type = rowData.Type;
            edge.Data = rowData.Data;
        }
    }
}