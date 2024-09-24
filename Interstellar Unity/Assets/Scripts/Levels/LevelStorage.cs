using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using GameLogic;
using Player;

namespace Levels
{
    public class LevelStorage : MonoBehaviour
    {
        private static List<LevelData> _levelsData;

        public static int CurrentLevelIndex { get; private set; }

        private void Awake()
        {
            LoadProgressData();
            LoadLevelData();
        }

        private void OnEnable()
        {
            GameStateController.OnLevelComplete += IncreaseLevel;
        }

        private void OnDisable()
        {
            GameStateController.OnLevelComplete -= IncreaseLevel;
        }

        private static void LoadLevelData()
        {
            _levelsData = Resources.LoadAll<LevelData>("ScriptableObjects/Levels")
                .OrderBy(x => x.LevelIndex)
                .ToList();
        }

        private static void LoadProgressData()
        {
            CurrentLevelIndex = PlayerPrefs.GetInt(PlayerDataKeys.LevelProgressDataKey);
        }

        private static void IncreaseLevel()
        {
            CurrentLevelIndex++;
            SaveProgressData(CurrentLevelIndex);
        }

        private static void SaveProgressData(int currentLevelIndex)
        {
            PlayerPrefs.SetInt(PlayerDataKeys.LevelProgressDataKey, currentLevelIndex);
        }

        public static LevelData GetLevelData()
        {
            if (CurrentLevelIndex >= _levelsData.Count - 1)
            {
                CurrentLevelIndex = 0;
                SaveProgressData(CurrentLevelIndex);
            }
            return _levelsData[CurrentLevelIndex];
        }
    }
}

