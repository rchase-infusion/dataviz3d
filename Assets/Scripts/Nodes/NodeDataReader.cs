using System;
using System.Collections.Generic;
using Assets.Scripts.Edges;
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

            NodeRawData lastNodeRawData = null;
            foreach (var currentLine in lines)
            {
                var fields = currentLine.Split(';');
                if (fields[0].Equals("N")) // N like Node
                {
                    if (lastNodeRawData != null)
                        results.Add(lastNodeRawData);

                    lastNodeRawData = ToNodeRawData(fields);
                }
                else if (fields[0].Equals("E")) // E like Edge
                {
                    if (lastNodeRawData == null)
                        throw new InvalidOperationException("[NodeDataReader] lastNodeRawData is null! First line of graph_data.csv is of Edge type?");

                    lastNodeRawData.Edges.Add(ToEdgeRawData(fields));
                }
            }

            if (lastNodeRawData != null)
                results.Add(lastNodeRawData);

            return results;
        }

        private NodeRawData ToNodeRawData(string[] fields)
        {
            var nodeRawData = new NodeRawData()
            {
                Id = ToInt(fields[1]),
                Type = fields[2].ToNodeType(),
                Name = fields[3],
                Description = fields[4],
                Position = ToVector3(fields[5])
            };

            return nodeRawData;
        }

        private EdgeRawData ToEdgeRawData(string[] fields)
        {
            var edgeRawData = new EdgeRawData()
            {
                Type = fields[1],
                NodeId = ToInt(fields[2]),
                Data = ToFloat(fields[3])
            };

            return edgeRawData;
        }

        private int ToInt(string value)
        {
            return Int32.Parse(value);
        }

        private float ToFloat(string value)
        {
            return float.Parse(value);
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