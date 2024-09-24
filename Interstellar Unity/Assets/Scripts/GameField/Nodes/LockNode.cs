using UnityEngine;

namespace GameField.Nodes
{
    public class LockNode : Node
    {
        [SerializeField] private Sprite _unlockNodeSprite;
        [SerializeField] private ParticleSystem _destroyLockNodeParticle;
        [SerializeField] private AudioSource _destroyLockNodeSound;
        private bool _isUnlock;

        public void Unlock()
        {
            StartDestroyParticle();
            _isUnlock = true;
            transform.gameObject.tag = "Node";
            _nodeImage.sprite = _unlockNodeSprite;
            gameObject.AddComponent<Movement>();
        }

        public override void StartDestroyParticle()
        {
            if (!_isUnlock)
            {
                _destroyLockNodeSound.Play();
                _destroyLockNodeParticle.Play();
            }
            else
                base.StartDestroyParticle();
        }
    }
}

