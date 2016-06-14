using System.Collections.Generic;

namespace Assets.Scripts.Edges
{
    public interface IEdgeDataReaderV2
    {
        IEnumerable<EdgeRawDataV2> Read(string filename);
    }
}