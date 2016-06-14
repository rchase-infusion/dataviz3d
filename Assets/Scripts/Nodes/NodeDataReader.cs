using System;
using System.Collections.Generic;
using Assets.Scripts.Edges;
using Assets.Scripts.Utils;
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
                Id = fields[1].ToInt(),
                Type = fields[2].ToNodeType(),
                Name = fields[3],
                Description = fields[4],
                Position = fields[5].ToVector3()
            };

            return nodeRawData;
        }

        private EdgeRawData ToEdgeRawData(string[] fields)
        {
            var edgeRawData = new EdgeRawData()
            {
                Type = fields[1],
                NodeId = fields[2].ToInt(),
                Data = fields[3].ToFloat()
            };

            return edgeRawData;
        }
    }
}