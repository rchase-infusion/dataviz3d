using System;
using UnityEngine;

namespace Assets.Scripts.Nodes
{
    public class NodeV2 : MonoBehaviour
    {
        public int Id;
        public string Name;
        public string Description;

        public void InitializeFrom(NodeRawDataV2 nodeRawData)
        {
            Id = nodeRawData.Id;
            Name = nodeRawData.Name;
            Description = nodeRawData.Description;
        }
    }
}