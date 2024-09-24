using System.Collections.Generic;
using UnityEngine;

namespace GameField.Lines
{
    public class LineCollision : MonoBehaviour
    {
        private Line _line;
        private HashSet<GameObject> _collisions = new();

        public int CountCollisons => _collisions.Count;

        private void Awake()
        {
            _line = GetComponent<Line>();
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Line"))
            {
                _collisions.Add(collision.gameObject);
                _line.ChangeMaterialStartCollision();
            }
        }

        private void OnCollisionExit2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Line"))
            {
                _collisions.Remove(collision.gameObject);
                if (_collisions.Count <= 0)
                {
                    _line.ChangeMaterialEndCollision();
                }
            }
        }
    }
}

