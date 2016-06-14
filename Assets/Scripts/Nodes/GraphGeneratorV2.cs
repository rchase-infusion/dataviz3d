using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Edges;
using Assets.Scripts.Utils;
using UnityEngine;

namespace Assets.Scripts.Nodes
{
    public interface IGraphGeneratorV2
    {
        IEnumerable<GameObject> GenerateNodes(IEnumerable<NodeRawDataV2> rowData);
        void GenerateEdges(IEnumerable<GameObject> nodes);
    }

    public class GraphGeneratorV2 : IGraphGeneratorV2
    {
        private readonly GameObject _nodesContainer;
        private readonly GameObject _edgesContainer;

        public GraphGeneratorV2(GameObject nodesContainer, GameObject edgesContainer)
        {
            _nodesContainer = nodesContainer;
            _edgesContainer = edgesContainer;
        }

        public IEnumerable<GameObject> GenerateNodes(IEnumerable<NodeRawDataV2> rowData)
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

        private GameObject GenerateNode(NodeRawDataV2 nodeRawData)
        {
            var resourceName = "node_" + nodeRawData.Shape;

            var gameObject = MonoBehaviour.Instantiate(Resources.Load(resourceName)) as GameObject;

            // Initialize Node component
            var node = gameObject.AddComponent<NodeV2>();
            node.InitializeFrom(nodeRawData);
            // Change properties of the object - name, position...
            gameObject.SetParent(_nodesContainer);
            gameObject.name = FormatNodeName(node);
            gameObject.SetPosition(nodeRawData.Position);
            gameObject.SetSize(nodeRawData.Size);
            gameObject.SetColor(nodeRawData.Color);
            
            return new GameObject();
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

        private string FormatNodeName(NodeV2 node)
        {
            return node.Id + " - " + node.Name;
        }

        private string FormatEdgeName(GameObject parent, GameObject child)
        {
            return String.Format("Edge - {0} | {1}", parent.name, child.name);
        }
    }
}