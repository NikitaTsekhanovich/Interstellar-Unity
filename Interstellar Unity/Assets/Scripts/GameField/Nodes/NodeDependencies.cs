using UnityEngine;

namespace GameField.Nodes
{
    [System.Serializable]
    public class NodeDependencies
    {
        [SerializeField] private ushort _indexFirstNode;
        [SerializeField] private ushort _indexSecondNode;

        public ushort IndexFirstNode => _indexFirstNode;
        public ushort IndexSecondNode => _indexSecondNode;

        public NodeDependencies(ushort indexFirstNode, ushort indexSecondNode)
        {
            _indexFirstNode = indexFirstNode;
            _indexSecondNode = indexSecondNode;
        }
    }
}

