using System.Collections.Generic;

namespace Assets.Scripts.Nodes
{
    public interface INodeDataReader
    {
        IEnumerable<NodeRawData> Read();
    }
}