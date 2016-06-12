using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Edges;
using Assets.Scripts.Utils;
using UnityEngine;

namespace Assets.Scripts.Nodes
{
    public interface IGraphGenerator
    {
        IEnumerable<GameObject> GenerateNodes(IEnumerable<NodeRawData> rowData);
        void GenerateEdges(IEnumerable<GameObject> nodes);
    }

    public class GraphGenerator : IGraphGenerator
    {
        private readonly GameObject _nodesContainer;
        private readonly GameObject _edgesContainer;

        public GraphGenerator(GameObject nodesContainer, GameObject edgesContainer)
        {
            _nodesContainer = nodesContainer;
            _edgesContainer = edgesContainer;
        }

        public IEnumerable<GameObject> GenerateNodes(IEnumerable<NodeRawData> rowData)
        {
            return rowData.Select(data => GenerateNode(data)).ToList();
        }

        public void GenerateEdges(IEnumerable<GameObject> nodes)
        {
            var nodesList = nodes.ToList();
            foreach (var node in nodesList)
            {
                GenerateEdges(nodesList, node);
            }
        }

        private GameObject GenerateNode(NodeRawData nodeRawData)
        {
            var gameObject = MonoBehaviour.Instantiate(Resources.Load(nodeRawData.Type.ToString())) as GameObject;

            var node = gameObject.GetComponent<Node>();
            node.InitializeFrom(nodeRawData);

            gameObject.SetPosition(nodeRawData.Position);
            gameObject.SetParent(_nodesContainer);
            gameObject.name = FormatNodeName(node);

            return gameObject;
        }

        private void GenerateEdges(IEnumerable<GameObject> nodes, GameObject nodeGameObject)
        {
            var node = nodeGameObject.GetComponent<Node>();

            foreach (var edgeRawData in node.EdgesRawData)
            {
                var child = nodes.First(n => n.GetComponent<Node>().Id == edgeRawData.NodeId);
                GenerateEdge(edgeRawData, nodeGameObject, child);
            }
        }

        private void GenerateEdge(EdgeRawData rowData, GameObject parent, GameObject child)
        {
            var edgeGameObject = new GameObject(FormatEdgeName(parent, child));
            edgeGameObject.SetParent(_edgesContainer);

            var edge = edgeGameObject.AddComponent<Edge>();
            edge.InitializeFrom(rowData, parent, child);

            var lineRenderer = edgeGameObject.AddComponent<LineRenderer>();
            lineRenderer.SetWidth(0.05f, 0.05f);
            lineRenderer.SetColors(Color.red, Color.red);
            lineRenderer.SetVertexCount(2);
            
            // Draw edge
            lineRenderer.SetPosition(0, parent.transform.position);
            lineRenderer.SetPosition(1, child.transform.position);
        }

        private string FormatNodeName(Node node)
        {
            return node.Id + " - " + node.Name;
        }

        private string FormatEdgeName(GameObject parent, GameObject child)
        {
            return String.Format("Edge - {0} | {1}", parent.name, child.name);
        }
    }
}