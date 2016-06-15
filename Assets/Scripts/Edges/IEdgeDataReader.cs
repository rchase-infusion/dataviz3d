using System.Collections.Generic;

namespace Assets.Scripts.Edges
{
    public interface IEdgeDataReader
    {
        IEnumerable<EdgeRawData> Read(string filename);
    }
}