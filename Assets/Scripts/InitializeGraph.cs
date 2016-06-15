using System.Collections.Generic;
using Assets.Scripts.Edges;
using Assets.Scripts.Nodes;
using UnityEngine;

namespace Assets.Scripts
{
    public class InitializeGraph : MonoBehaviour
    {
        private INodeDataReader _nodeDataReader;
        private IEdgeDataReader _edgeDataReader;
        private IGraphGenerator _graphGenerator;

        public IEnumerable<GameObject> Nodes { get; private set; }
        public IEnumerable<GameObject> Labels { get; private set; }
        
        void Start()
        {
            _nodeDataReader = new NodeDataReader();
            _edgeDataReader = new EdgeDataReader();
            _graphGenerator = new GraphGenerator(GameObject.Find("NODES"), GameObject.Find("EDGES"));
            Labels = new List<GameObject>();

            Initialize();
        }

        private void Initialize()
        {
            var nodesRawData = _nodeDataReader.Read();
            Nodes = _graphGenerator.GenerateNodes(nodesRawData);
            Labels = _graphGenerator.GenerateNodeLabels(Nodes, false);

            LoadSeries1();
        }

        /// <summary>
        /// Method called on click on 'Load Series 1' button
        /// </summary>
        public void LoadSeries1()
        {
            LoadSeries("graph_data_edges_series_1");
        }

        /// <summary>
        /// Method called on click on 'Load Series 2' button
        /// </summary>
        public void LoadSeries2()
        {
            LoadSeries("graph_data_edges_series_2");
        }

        private void LoadSeries(string filename)
        {
            ClearAllEdges();
            
            var edgesRawData = _edgeDataReader.Read(filename);
            _graphGenerator.GenerateEdges(Nodes, edgesRawData);
        }

        private void ClearAllEdges()
        {
            var edgesContainer = GameObject.Find("EDGES");

            foreach (Transform edge in edgesContainer.transform)
            {
                GameObject.Destroy(edge.gameObject);
            }
        }
    }
}