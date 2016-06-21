using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Edges;
using UnityEngine;

namespace Assets.Scripts
{
    class SimpleEdgesReduced : SimpleAllEdges
    {
        private int mod;
        public SimpleEdgesReduced(int count1, int centerId1, int count2, int centerId2, int mod) : base(count1, centerId1, count2, centerId2)
        {
            this.mod = mod;
        }

        public override List<EdgeRawData> GenerateEdges()
        {
            var result = base.GenerateEdges();
            result = result.Where((data, i) => i%mod == 0).ToList();

            return result;
        }
    }
}
