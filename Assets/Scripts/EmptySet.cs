using System.Collections.Generic;
using Assets.Scripts.Edges;
using UnityEngine;

namespace Assets.Scripts
{
    class EmptySet : EdgeGenerator
    {
       public override List<EdgeRawData> GenerateEdges()
        {
            return new List<EdgeRawData>();
        }
    }
}
