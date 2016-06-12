using Assets.Scripts.Nodes;
using UnityEngine;

namespace Assets.Scripts
{
    public class InitializeGraph : MonoBehaviour
    {
        private INodeDataReader _nodeDataReader;
        private IGraphGenerator _graphGenerator;

        void Start()
        {
            _nodeDataReader = new NodeDataReader();
            _graphGenerator = new GraphGenerator(GameObject.Find("NODES"), GameObject.Find("EDGES"));

            Initialize();
        }

        private void Initialize()
        {
            var nodesRawData = _nodeDataReader.Read();
            var nodes = _graphGenerator.GenerateNodes(nodesRawData);
            _graphGenerator.GenerateEdges(nodes);
        }
    }
}