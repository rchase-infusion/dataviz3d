using System;
using System.Collections.Generic;
using Assets.Scripts.Utils;
using UnityEngine;

namespace Assets.Scripts.Edges
{
    public class EdgeDataReaderV2 : IEdgeDataReaderV2
    {
        public IEnumerable<EdgeRawDataV2> Read(string filename)
        {
            var results = new List<EdgeRawDataV2>();

            // Had to change to this read method because the standard StreamReader is not allowed in UWP apps!
            TextAsset graphDataFile = (TextAsset) Resources.Load(filename, typeof(TextAsset));
            string graphData = graphDataFile.text;
            var lines = graphData.Split("\n\r".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

            foreach (var currentLine in lines)
            {
                results.Add(ToEdgeRawData(currentLine));
            }

            return results;
        }

        private EdgeRawDataV2 ToEdgeRawData(string line)
        {
            var fields = line.Split(';');

            var edgeRawData = new EdgeRawDataV2()
            {
                Id = fields[0].ToInt(),
                Name = fields[1],
                ParentNodeId = fields[2].ToInt(),
                ChildNodeId = fields[3].ToInt(),
                Color = fields[4].ToColor(),
                Thickness = fields[5].ToFloat()
            };

            return edgeRawData;
        }
    }
}