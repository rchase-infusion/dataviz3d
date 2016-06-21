using System.Collections.Generic;
using Assets.Scripts.Edges;
using UnityEngine;

namespace Assets.Scripts
{
    class TrailingEdges : EdgeGenerator
    {
        private int count;
        private int centerId;

        public TrailingEdges(int count, int centerId)
        {
            this.count = count;
            this.centerId = centerId;
        }


        public override List<EdgeRawData> GenerateEdges()
        {
            var result = EdgeRawDatas(count, centerId);

            return result;
        }

        private List<EdgeRawData> EdgeRawDatas(int count, int centerId)
        {
            var result = new List<EdgeRawData>();
            for (int i = 0; i < count; i++)
            {
                var id = centerId + i + 1;
                result.Add(new EdgeRawData(id, "Edge " + id, i, i+1, GetRandomColorColor(), (float) (.004 * random.NextDouble())));
            }
            return result;
        }
    }
}
