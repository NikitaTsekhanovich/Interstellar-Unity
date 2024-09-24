using System.Collections;
using MovableItem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PlayerInterface.Abilities
{
    public abstract class Ability : Drag
    {
        [SerializeField] private string _targetTag;
        [SerializeField] private Transform _parent;
        [SerializeField] private TMP_Text _countAbility;
        [SerializeField] private Image _abilityImage;
        private int _currentCountAbility;
        private string _key;
        private Collider2D _target;

        protected override void ClickOnTarget()
        {
            transform.localPosition = _parent.localPosition;
            _currentCountAbility--;
            PlayerPrefs.SetInt(_key, _currentCountAbility);
            UpdateCountText();
            CheckAvailability();
            StartCoroutine(UseAbility(_target));
        }

        protected override bool GetTargetCheck(RaycastHit2D[] hits)
        {
            foreach (var hit in hits)
            {
                if (hit.collider != null && hit.collider.CompareTag(_targetTag))
                {
                    _target = hit.collider;
                    return true;
                }
            }
            
            return false;
        }

        public void SetAbilityState(string key)
        {
            _key = key;
            _currentCountAbility = PlayerPrefs.GetInt(_key);
            UpdateCountText();
            CheckAvailability();
        }

        private void UpdateCountText()
        {
            _countAbility.text = $"{_currentCountAbility}";
        }

        private void CheckAvailability()
        {
            if (_currentCountAbility <= 0)
            {
                _abilityImage.color = Color.gray;
                _canvasGroup.blocksRaycasts = false;
            }
        }

        protected abstract IEnumerator UseAbility(Collider2D _target);
    }
}

