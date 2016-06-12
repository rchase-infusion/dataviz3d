using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Nodes
{
    public class NodeDataReader : INodeDataReader
    {
        public IEnumerable<NodeRawData> Read()
        {
            var results = new List<NodeRawData>();

            // Had to change to this read method because the standard StreamReader is not allowed in UWP apps!
            TextAsset graphDataFile = (TextAsset) Resources.Load("graph_data", typeof(TextAsset));
            string graphData = graphDataFile.text;
            var lines = graphData.Split("\n\r".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

            foreach (var currentLine in lines)
            {
                results.Add(ToNodeRawData(currentLine));
            }

            return results;
        }

        private NodeRawData ToNodeRawData(string line)
        {
            var fields = line.Split(';');

            var nodeRawData = new NodeRawData()
            {
                Id = ToInt(fields[0]),
                Type = fields[1].ToNodeType(),
                Name = fields[2],
                Description = fields[3],
                Position = ToVector3(fields[4])
            };

            return nodeRawData;
        }

        private int ToInt(string value)
        {
            return Int32.Parse(value);
        }

        private Vector3 ToVector3(string value)
        {
            var coordinates = value.Split(',');
            return new Vector3(ToInt(coordinates[0]), ToInt(coordinates[1]), ToInt(coordinates[2]));
        }
    }

    public static class NodeTypeExtensions
    {
        public static NodeType ToNodeType(this string value)
        {
            return (NodeType) Enum.Parse(typeof(NodeType), value);
        }
    }
}