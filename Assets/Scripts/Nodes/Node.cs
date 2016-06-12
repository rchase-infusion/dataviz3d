using UnityEngine;

namespace Assets.Scripts.Nodes
{
    public class Node : MonoBehaviour
    {
        public int Id;
        public string Name;
        public string Description;
        public NodeType Type;

        public Node[] Relations;
    }
}