using UnityEngine;

namespace Assets.Scripts.Edges
{
    public class EdgeRawData
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ParentNodeId { get; set; }
        public int ChildNodeId { get; set; }
        public Color Color { get; set; }
        public float Thickness { get; set; }

        public EdgeRawData(int id, string name, int parentNodeId, int childNodeId, Color color, float thickness)
        {
            Id = id;
            Name = name;
            ParentNodeId = parentNodeId;
            ChildNodeId = childNodeId;
            Color = color;
            Thickness = thickness;
        }
    }
}