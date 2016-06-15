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
    }
}