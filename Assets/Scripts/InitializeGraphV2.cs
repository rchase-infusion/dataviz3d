using Assets.Scripts.Nodes;
using UnityEngine;

namespace Assets.Scripts
{
    public class InitializeGraphV2 : MonoBehaviour
    {
        private INodeDataReaderV2 _nodeDataReader;
        private IGraphGeneratorV2 _graphGenerator;

        void Start()
        {
            _nodeDataReader = new NodeDataReaderV2();
            _graphGenerator = new GraphGeneratorV2(GameObject.Find("NODES"), GameObject.Find("EDGES"));

            Initialize();
        }

        private void Initialize()
        {
            var nodesRawData = _nodeDataReader.Read();
            var nodes = _graphGenerator.GenerateNodes(nodesRawData);
//            _graphGenerator.GenerateEdges(nodes);
        }
    }
}