using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Utils;
using UnityEngine;

namespace Assets.Scripts.Nodes
{
    public interface INodeGenerator
    {
        IEnumerable<GameObject> GenerateNodes(IEnumerable<NodeRawData> rowData);
    }

    public class NodeGenerator : INodeGenerator
    {
        private readonly GameObject _parentGameObject;

        public NodeGenerator(GameObject parentGameObject)
        {
            _parentGameObject = parentGameObject;
        }

        public IEnumerable<GameObject> GenerateNodes(IEnumerable<NodeRawData> rowData)
        {
            return rowData.Select(data => GenerateNode(data)).ToList();
        }

        private GameObject GenerateNode(NodeRawData nodeRawData)
        {
            var gameObject = MonoBehaviour.Instantiate(Resources.Load(nodeRawData.Type.ToString())) as GameObject;

            var node = gameObject.GetComponent<Node>();
            node.InitializeFrom(nodeRawData);

            gameObject.SetPosition(nodeRawData.Position);
            gameObject.SetParent(_parentGameObject);
            gameObject.name = FormatNodeName(node);

            return gameObject;
        }

        private string FormatNodeName(Node node)
        {
            return node.Id + " - " + node.Name;
        }
    }
}