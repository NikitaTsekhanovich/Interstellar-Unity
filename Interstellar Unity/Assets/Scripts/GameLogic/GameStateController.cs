using System;
using GameField.Nodes;
using Levels;
using UnityEngine;

namespace GameLogic
{
    public class GameStateController : MonoBehaviour
    {
        [SerializeField] private GameObject _loseScreen;

        public static Action OnLevelComplete;
        public static Action OnLoadNextLevel;

        private void OnEnable()
        {
            Timer.OnLoseGame += LoseGame;
            LevelController.OnWin += WinGame;
        }

        private void OnDisable()
        {
            Timer.OnLoseGame -= LoseGame;
            LevelController.OnWin -= WinGame;
        }

        private void WinGame(int coins)
        {
            OnLevelComplete?.Invoke();
            OnLoadNextLevel?.Invoke();
        }

        private void LoseGame()
        {
            _loseScreen.SetActive(true);
        }
    }
}

