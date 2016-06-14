using System;
using System.Collections.Generic;
using Assets.Scripts.Utils;
using UnityEngine;

namespace Assets.Scripts.Nodes
{
    public class NodeDataReaderV2 : INodeDataReaderV2
    {
        public IEnumerable<NodeRawDataV2> Read()
        {
            var results = new List<NodeRawDataV2>();

            // Had to change to this read method because the standard StreamReader is not allowed in UWP apps!
            TextAsset graphDataFile = (TextAsset) Resources.Load("graph_data_nodes", typeof(TextAsset));
            string graphData = graphDataFile.text;
            var lines = graphData.Split("\n\r".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

            foreach (var currentLine in lines)
            {
                results.Add(ToNodeRawData(currentLine));
            }
            
            return results;
        }

        private NodeRawDataV2 ToNodeRawData(string line)
        {
            var fields = line.Split(';');

            var nodeRawData = new NodeRawDataV2()
            {
                Id = fields[0].ToInt(),
                Name = fields[1],
                Description = fields[2],
                Size = fields[3].ToFloat(),
                Color = fields[4].ToColor(),
                Shape = fields[5].ToLower(),
                Position = fields[6].ToVector3()
            };

            return nodeRawData;
        }
    }
}