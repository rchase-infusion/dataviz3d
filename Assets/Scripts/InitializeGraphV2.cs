﻿using System.Collections.Generic;
using Assets.Scripts.Edges;
using Assets.Scripts.Nodes;
using UnityEngine;

namespace Assets.Scripts
{
    public class InitializeGraphV2 : MonoBehaviour
    {
        private INodeDataReaderV2 _nodeDataReader;
        private IEdgeDataReaderV2 _edgeDataReader;
        private IGraphGeneratorV2 _graphGenerator;

        private IEnumerable<GameObject> _nodes; 

        void Start()
        {
            _nodeDataReader = new NodeDataReaderV2();
            _edgeDataReader = new EdgeDataReaderV2();
            _graphGenerator = new GraphGeneratorV2(GameObject.Find("NODES"), GameObject.Find("EDGES"));

            Initialize();
        }

        private void Initialize()
        {
            var nodesRawData = _nodeDataReader.Read();
            _nodes = _graphGenerator.GenerateNodes(nodesRawData);

            LoadSeries1();
        }

        public void LoadSeries1()
        {
            LoadSeries("graph_data_edges_series_1");
        }

        public void LoadSeries2()
        {
            LoadSeries("graph_data_edges_series_2");
        }

        private void LoadSeries(string filename)
        {
            ClearAllEdges();
            
            var edgesRawData = _edgeDataReader.Read(filename);
            _graphGenerator.GenerateEdges(_nodes, edgesRawData);
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