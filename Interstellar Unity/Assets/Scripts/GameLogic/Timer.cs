using System;
using System.Collections;
using TMPro;
using UnityEngine;

namespace GameLogic
{
    public class Timer : MonoBehaviour
    {
        [SerializeField] private TMP_Text _timeText;
        private int _currentTime = 100; 

        public static Action OnLoseGame;

        private void Start()
        {
            StartCoroutine(StartTime());
        }

        private IEnumerator StartTime()
        {
            while (true)
            {
                yield return new WaitForSeconds(1f);
                _currentTime--;

                _timeText.text = $"{_currentTime}";

                if (_currentTime <= 0)
                    OnLoseGame?.Invoke();
            }
        }
    }
}

