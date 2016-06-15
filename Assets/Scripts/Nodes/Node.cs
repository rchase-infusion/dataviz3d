using UnityEngine;

namespace Assets.Scripts.Nodes
{
    public class Node : MonoBehaviour
    {
        public int Id;
        public string Name;
        public string Description;

        public void InitializeFrom(NodeRawData nodeRawData)
        {
            Id = nodeRawData.Id;
            Name = nodeRawData.Name;
            Description = nodeRawData.Description;
        }
    }
}