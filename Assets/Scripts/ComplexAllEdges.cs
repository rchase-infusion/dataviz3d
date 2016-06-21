using System.Collections.Generic;
using Assets.Scripts.Edges;
using UnityEngine;

namespace Assets.Scripts
{
    class ComplexAllEdges : EdgeGenerator
    {
        private int count1;
        private int centerId1;
        private int count2;
        private int centerId2;

        public ComplexAllEdges(int count1, int centerId1, int count2, int centerId2)
        {
            this.count1 = count1;
            this.centerId1 = centerId1;
            this.count2 = count2;
            this.centerId2 = centerId2;
        }


        public override List<EdgeRawData> GenerateEdges()
        {
            var result = EdgeRawDatas(count1, centerId1);
            result.AddRange(EdgeRawDatas(count2, centerId2));
            result.Add(new EdgeRawData(-1, "Edge", centerId1, centerId2, Color.red, .01f));

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
