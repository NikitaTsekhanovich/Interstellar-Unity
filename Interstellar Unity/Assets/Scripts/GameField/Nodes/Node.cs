using System.Collections.Generic;
using GameField.Lines;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace GameField.Nodes
{
    public class Node : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _destroyNodeParticle;
        [SerializeField] private CanvasGroup _clickHandler;
        [SerializeField] private AudioSource _destroyNodeSound;
        [SerializeField] protected Image _nodeImage;
        private List<Line> _lines = new();

        public void SetLine(Line line)
        {
            _lines.Add(line);
        }

        public virtual void StartDestroyParticle()
        {
            _destroyNodeSound.Play();
            _destroyNodeParticle.Play();
        }

        public IEnumerator StartDestroyNode()
        {
            StartDestroyParticle();
            _nodeImage.enabled = false;
            DestroyDependentLines();
            yield return new WaitForSeconds(1f);
            DestroyNode();
        }

        public void OffClickCollider()
        {
            _clickHandler.blocksRaycasts = false;
        }

        private void DestroyDependentLines()
        {
            foreach (var line in _lines)
            {
                if (line != null)
                    Destroy(line.gameObject);
            }
        }

        private void DestroyNode()
        {
            Destroy(gameObject);
        }
    }
}

