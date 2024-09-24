using Levels;
using Player;
using TMPro;
using UnityEngine;
using DG.Tweening;

namespace GameLogic
{
    public class CoinsController : MonoBehaviour
    {
        [SerializeField] private TMP_Text _currentCoinsText;
        [SerializeField] private AudioSource _destroyNodeSound;
        private int _coins;

        private void Awake()
        {
            _coins = PlayerPrefs.GetInt(PlayerDataKeys.CoinsDataKey);
        }

        private void Start()
        {
            ShowCoins();
        }

        private void OnEnable()
        {
            LevelController.OnIncreaseCoins += IncreaseCoins;
        }

        private void OnDisable()
        {
            LevelController.OnIncreaseCoins -= IncreaseCoins;
        }

        private void IncreaseCoins(int coins)
        {
            DOTween.Sequence()
                .Append(_currentCoinsText.DOColor(Color.green, 0.4f))
                .AppendInterval(0.2f)
                .Append(_currentCoinsText.DOColor(new Color(0.25f, 0.24f, 1f, 1f), 0.4f));

            _destroyNodeSound.Play();
            _coins += coins;
            PlayerPrefs.SetInt(PlayerDataKeys.CoinsDataKey, _coins);
            ShowCoins();
        }

        private bool CanDecreaseCoins(int coins)
        {
            if (_coins - coins >= 0)
            {
                _coins -= coins;
                PlayerPrefs.SetInt(PlayerDataKeys.CoinsDataKey, _coins);
                return true;
            }
            
            return false;
        }

        private void ShowCoins()
        {
            _currentCoinsText.text = $"{_coins}";
        }
    }
}

