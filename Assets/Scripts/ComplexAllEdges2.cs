using System.Collections.Generic;
using Assets.Scripts.Edges;
using UnityEngine;

namespace Assets.Scripts
{
    class ComplexAllEdges2 : EdgeGenerator
    {
        private int count;
        private int centerId;

        public ComplexAllEdges2(int count, int centerId)
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
                int start = (random.Next() % count) + centerId;
                int end = (random.Next() % count) + centerId;
                result.Add(new EdgeRawData(id, "Edge " + id, start, end, GetRandomColorColor(), (float) (.004 * random.NextDouble())));
            }
            return result;
        }
    }
}
