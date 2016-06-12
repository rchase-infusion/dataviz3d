using System.Collections.Generic;
using Assets.Scripts.Edges;
using UnityEngine;

namespace Assets.Scripts.Nodes
{
    public class NodeRawData
    {
        public NodeRawData()
        {
            Edges = new List<EdgeRawData>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public NodeType Type { get; set; }
        public Vector3 Position { get; set; }

        public IList<EdgeRawData> Edges;
    }
}