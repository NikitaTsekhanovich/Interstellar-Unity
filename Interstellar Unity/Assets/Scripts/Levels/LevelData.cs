using System.Collections.Generic;
using GameField.Nodes;
using UnityEngine;

namespace Levels
{
    [CreateAssetMenu(fileName = "LevelData", menuName = "Levels data/ Level")]
    public class LevelData : ScriptableObject
    {
        [SerializeField] private int _levelIndex;
        [SerializeField] private List<Transform> _nodesPositions = new();
        [SerializeField] private List<NodeDependencies> _nodeDependencies = new();
        [SerializeField] private List<Transform> _lockNodesPositions = new();

        public int LevelIndex => _levelIndex;
        public List<Transform> NodesPositions => _nodesPositions;
        public List<NodeDependencies> NodeDependencies => _nodeDependencies;
        public List<Transform> LockNodesPositions => _lockNodesPositions;
    }
}

