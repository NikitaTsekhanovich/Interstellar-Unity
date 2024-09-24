using UnityEngine;
using System.Collections.Generic;

namespace GameField.Lines
{
    public class Line : MonoBehaviour
    {
        [SerializeField] private LineRenderer _render;
        [SerializeField] private Material _colorLine;
        [SerializeField] private Material _redLine;
        [SerializeField] private BoxCollider2D _clickCollider;
        private List<Transform> _nodes = new();
        private BoxCollider2D _colliderLine;
        private Vector3 _startPos;
        private Vector3 _endPos;

        public void SetPosition(Transform startPos, Transform endPos)
        {
            _render.material = _colorLine;
            _render.positionCount++;
            _nodes.Add(startPos);
            _render.positionCount++;
            _nodes.Add(endPos);

            CreateCollider();
        }

        public void ChangeMaterialStartCollision()
        {
            _render.material = _redLine;
        }

        public void ChangeMaterialEndCollision()
        {
            _render.material = _colorLine;
        }

        private void CreateCollider()
        {
            _colliderLine = GetComponent<BoxCollider2D>();
            _colliderLine.transform.parent = gameObject.transform;
        }

        private void Update()
        {
            _render.SetPositions(_nodes.ConvertAll(n => n.position - new Vector3(0, 0, 5)).ToArray());
            CalculateColliderPosition(_colliderLine, 0.05f);
            CalculateColliderPosition(_clickCollider, 1.3f);
        }

        private void CalculateColliderPosition(BoxCollider2D _colliderLine, float lineWidth)
        {
            _startPos = _nodes[0].position - new Vector3(0, 0, 5); 
            _endPos = _nodes[1].position - new Vector3(0, 0, 5);
                
            var lineLength = Vector3.Distance (_startPos, _endPos); 
            var newSize = new Vector3 (lineLength - 0.9f, lineWidth, 1f);
            
            if (newSize.x <= 1f)
                _colliderLine.size = new Vector3 (1f, lineWidth, 1f);
            else
                _colliderLine.size = newSize;

            var midPoint = (_startPos + _endPos) / 2;
            _colliderLine.transform.position = midPoint; 

            CalculateColliderAngle();
        }

        private void CalculateColliderAngle()
        {

            if (Mathf.Abs(_startPos.x - _endPos.x) == 0)
                return;

            var angle = Mathf.Abs(_startPos.y - _endPos.y) / Mathf.Abs(_startPos.x - _endPos.x);
            if((_startPos.y<_endPos.y && _startPos.x>_endPos.x) || (_endPos.y<_startPos.y && _endPos.x>_startPos.x))
            {
                angle *= -1;
            }

            angle = Mathf.Rad2Deg * Mathf.Atan(angle);
            _colliderLine.transform.eulerAngles  = new Vector3(0, 0, angle);
        }
    }
}

