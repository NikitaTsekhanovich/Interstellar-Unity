using System;
using System.Collections;
using System.Collections.Generic;
using GameField.Lines;
using GameLogic;
using TMPro;
using UnityEngine;
using DG.Tweening;
using MovableItem;
using GameField.Nodes;
using PlayerInterface;

namespace Levels
{
    public class LevelController : MonoBehaviour
    {
        [SerializeField] private Line _line;
        [SerializeField] private Node _node;
        [SerializeField] private LockNode _lockNode;
        [SerializeField] private Transform _gameField;
        [SerializeField] private TMP_Text _currentLevelText;
        [SerializeField] private Transform _positionScoreCoins;

        private List<Node> _currentNodes = new();
        private List<Line> _currentLines = new();
        private List<LineCollision> _currentLineCollisions = new();

        public static Action<int> OnWin;
        public static Action<int> OnIncreaseCoins;

        private void Start()
        {
            LoadLevel();
        }

        private void OnEnable()
        {
            Drag.OnCheckCollision += CheckCollisionLines;
            GameStateController.OnLoadNextLevel += LoadLevel;
            GameMenuController.OnRestart += LoadLevel;
        }   

        private void OnDisable()
        {
            Drag.OnCheckCollision -= CheckCollisionLines;
            GameStateController.OnLoadNextLevel -= LoadLevel;
            GameMenuController.OnRestart -= LoadLevel;
        }

        private void LoadLevel()
        {
            ClearPreviousData();

            var currentLevelData = LevelStorage.GetLevelData();
            _currentLevelText.text = $"{LevelStorage.CurrentLevelIndex + 1}";

            SpawnNodes(currentLevelData.LockNodesPositions, _lockNode);
            SpawnNodes(currentLevelData.NodesPositions, _node);
            SpawnDependency(currentLevelData);
        }

        private void SpawnNodes<T>(List<Transform> nodes, T nodePrefab)
            where T : Node
        {
            foreach (var nodePosition in nodes)
            {
                var newNode = Instantiate(
                    nodePrefab,
                    _gameField);
                
                newNode.transform.localPosition = new Vector3(
                    nodePosition.localPosition.x,
                    nodePosition.localPosition.y,
                    0);

                _currentNodes.Add(newNode);
            }
        }

        private void SpawnDependency(LevelData currentLevelData)
        {
            var index = 0;

            foreach(var nodeDependency in currentLevelData.NodeDependencies)
            {
                var newLine = Instantiate(_line);
                newLine.name = $"{index}";

                newLine.SetPosition(
                    _currentNodes[nodeDependency.IndexFirstNode].transform, 
                    _currentNodes[nodeDependency.IndexSecondNode].transform);

                _currentNodes[nodeDependency.IndexFirstNode].SetLine(newLine);
                _currentNodes[nodeDependency.IndexSecondNode].SetLine(newLine);

                _currentLines.Add(newLine);
                _currentLineCollisions.Add(newLine.GetComponent<LineCollision>());

                index++;
            }
        }

        private void CheckCollisionLines()
        {
            var hasNotCollisions = false;

            foreach (var collision in _currentLineCollisions)
            {
                if (collision.CountCollisons > 0)
                {
                    hasNotCollisions = true;
                    break;
                }
            }

            if (!hasNotCollisions)
                StartCoroutine(StartAnimWin());
        }

        private IEnumerator StartAnimWin()
        {
            OffClickOnNodes();
            var animations = new List<Sequence>();

            DestroyLineCollision();

            foreach (var node in _currentNodes)
            {
                if (node == null)
                    continue;
                    
                var animation = DOTween.Sequence()
                    .Append(node.transform.DOMove(_positionScoreCoins.position, 1f))
                    .AppendCallback(() => OnIncreaseCoins?.Invoke(1))
                    .AppendCallback(() => node.StartDestroyParticle())
                    .Append(node.transform.DOScale(Vector3.zero, 0.4f));

                animations.Add(animation);
                yield return new WaitForSeconds(0.5f);
            }
            yield return new WaitForSeconds(0.5f * _currentNodes.Count);

            foreach (var animation in animations)
                animation.Kill();

            animations.Clear();

            OnWin?.Invoke(_currentNodes.Count);
        }

        private void OffClickOnNodes()
        {
            foreach (var node in _currentNodes)
            {
                if (node == null)
                    continue;
                    
                node.OffClickCollider();
            }
        }

        private void DestroyLineCollision()
        {
            foreach (var lineCollision in _currentLineCollisions)
                Destroy(lineCollision);
        }

        private void ClearPreviousData()
        {
            _currentLineCollisions.Clear();

            foreach (var line in _currentLines)
            {
                if (line != null)
                    Destroy(line.gameObject);
            }
            _currentLines.Clear();

            foreach (var node in _currentNodes)
            {
                if (node != null)
                    Destroy(node.gameObject);
            }
            _currentNodes.Clear();
        }
    }
}

