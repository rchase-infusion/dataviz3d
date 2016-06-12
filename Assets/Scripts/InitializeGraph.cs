using Assets.Scripts.Nodes;
using UnityEngine;

namespace Assets.Scripts
{
    public class InitializeGraph : MonoBehaviour
    {
        private INodeDataReader _nodeDataReader;
        private INodeGenerator _nodeGenerator;

        void Start()
        {
            _nodeDataReader = new NodeDataReader();
            _nodeGenerator = new NodeGenerator(GameObject.Find("GRAPH"));

            Initialize();
        }

        private void Initialize()
        {
            var nodes = _nodeDataReader.Read();
            _nodeGenerator.GenerateNodes(nodes);
        }
    }
}