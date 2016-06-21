using UnityEngine;

namespace Assets.Scripts.Nodes
{
    public class NodeRawData
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public float Size { get; set; }
        public Color Color { get; set; }
        public string Shape { get; set; }
        public Vector3 Position { get; set; }

        public NodeRawData(int id, string name, string description, float size, Color color, string shape, Vector3 position)
        {
            Id = id;
            Name = name;
            Description = description;
            Size = size;
            Color = color;
            Shape = shape;
            Position = position;
        }
    }
}