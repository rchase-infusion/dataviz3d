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
        void GenerateEdges(IEnumerable<GameObject> nodes, IEnumerable<EdgeRawDataV2> rowData);
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

        public void GenerateEdges(IEnumerable<GameObject> nodes, IEnumerable<EdgeRawDataV2> rowData)
        {
            var nodesList = nodes.ToList();
            foreach (var edgeRawData in rowData)
            {
                var parentNode = FindNode(nodesList, edgeRawData.ParentNodeId);
                var childNode = FindNode(nodesList, edgeRawData.ChildNodeId);

                GenerateEdge(edgeRawData, parentNode, childNode);
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

            return gameObject;
        }

        private void GenerateEdge(EdgeRawDataV2 rowData, GameObject parent, GameObject child)
        {
            var edgeGameObject = new GameObject(FormatEdgeName(rowData.Id, parent, child));
            edgeGameObject.SetParent(_edgesContainer);

            // Initialize the edge component
            var edge = edgeGameObject.AddComponent<EdgeV2>();
            edge.InitializeFrom(rowData, parent, child);
            // Draw the edge
            edge.Draw();
        }

        private string FormatNodeName(NodeV2 node)
        {
            return node.Id + " - " + node.Name;
        }

        private string FormatEdgeName(int edgeId, GameObject parent, GameObject child)
        {
            return String.Format("{0}: {1} -> {2}", edgeId, parent.name, child.name);
        }

        private GameObject FindNode(IEnumerable<GameObject> nodes, int nodeId)
        {
            return nodes.FirstOrDefault(n => n.GetComponent<NodeV2>().Id == nodeId);
        }
    }
}