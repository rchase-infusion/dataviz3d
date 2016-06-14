using System.Collections.Generic;

namespace Assets.Scripts.Nodes
{
    public interface INodeDataReaderV2
    {
        IEnumerable<NodeRawDataV2> Read();
    }
}