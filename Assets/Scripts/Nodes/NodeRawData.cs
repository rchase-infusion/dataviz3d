using UnityEngine;

namespace Assets.Scripts.Nodes
{
    public class NodeRawData
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public NodeType Type { get; set; }
        public Vector3 Position { get; set; }

//        public Node[] Relations;
    }
}