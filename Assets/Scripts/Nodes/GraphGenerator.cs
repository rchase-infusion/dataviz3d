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
        void GenerateEdges(IEnumerable<GameObject> nodes, IEnumerable<EdgeRawData> rowData);
        IEnumerable<GameObject> GenerateNodeLabels(IEnumerable<GameObject> nodes, bool enabledByDefault);
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

        public void GenerateEdges(IEnumerable<GameObject> nodes, IEnumerable<EdgeRawData> rowData)
        {
            var nodesList = nodes.ToList();
            foreach (var edgeRawData in rowData)
            {
                var parentNode = FindNode(nodesList, edgeRawData.ParentNodeId);
                var childNode = FindNode(nodesList, edgeRawData.ChildNodeId);

                GenerateEdge(edgeRawData, parentNode, childNode);
            }
        }

        public IEnumerable<GameObject> GenerateNodeLabels(IEnumerable<GameObject> nodes, bool enabledByDefault)
        {
            return nodes.Select(data => GenerateLabel(data, enabledByDefault    )).ToList();
        }

        private GameObject GenerateNode(NodeRawData nodeRawData)
        {
            var resourceName = "node_" + nodeRawData.Shape;
            var gameObject = MonoBehaviour.Instantiate(Resources.Load(resourceName)) as GameObject;

            // Initialize Node component
            var node = gameObject.AddComponent<Node>();
            node.InitializeFrom(nodeRawData);
            // Change properties of the object - name, position...
            gameObject.SetParent(_nodesContainer);
            gameObject.name = FormatNodeName(node);
            gameObject.SetPosition(nodeRawData.Position);
            gameObject.SetSize(nodeRawData.Size);
            gameObject.SetColor(nodeRawData.Color);

            return gameObject;
        }

        private GameObject GenerateLabel(GameObject parentGameObject, bool enabledByDefault)
        {
            var label = MonoBehaviour.Instantiate(Resources.Load("node_label")) as GameObject;

            if (!enabledByDefault)
                label.SetActive(false);

            var textMesh = label.GetComponent<TextMesh>();
            label.SetParent(parentGameObject);
            label.name = Constants.NodeLabelGameObjectName;

            // Calculate position
            var parentPosition = parentGameObject.transform.position;
            var parentScale = parentGameObject.transform.localScale;
            var labelPosition = new Vector3(
                parentPosition.x + (parentScale.x / 2) + 0.01f,
                parentPosition.y + (parentScale.y / 2) + 0.01f,
                parentPosition.z);
            
            label.SetPosition(labelPosition);
            textMesh.text = parentGameObject.GetComponent<Node>().Name;

            return label;
        }

        private void GenerateEdge(EdgeRawData rowData, GameObject parent, GameObject child)
        {
            var edgeGameObject = new GameObject(FormatEdgeName(rowData.Id, parent, child));
            edgeGameObject.SetParent(_edgesContainer);

            // Initialize the edge component
            var edge = edgeGameObject.AddComponent<Edge>();
            edge.InitializeFrom(rowData, parent, child);
            // Draw the edge
            edge.Draw();
        }

        private string FormatNodeName(Node node)
        {
            return String.Format("[{0}] {1}", node.Id, node.Name);
        }

        private string FormatEdgeName(int edgeId, GameObject parent, GameObject child)
        {
            return String.Format("[{0}] {1} -> {2}", edgeId, parent.name, child.name);
        }

        private GameObject FindNode(IEnumerable<GameObject> nodes, int nodeId)
        {
            return nodes.FirstOrDefault(n => n.GetComponent<Node>().Id == nodeId);
        }
    }
}