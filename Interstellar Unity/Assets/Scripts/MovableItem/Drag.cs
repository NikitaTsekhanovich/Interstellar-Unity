using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace MovableItem
{
    public abstract class Drag : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
    {
        private Canvas _gameCanvas;
        private RectTransform _rectTransform;
        private Vector3 _lastPosition;
        protected CanvasGroup _canvasGroup;

        public static Action OnCheckCollision;

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
            _canvasGroup = GetComponent<CanvasGroup>();
            _gameCanvas = GameObject.FindWithTag("GameCanvas").GetComponent<Canvas>();
        }
        public void OnBeginDrag(PointerEventData eventData)
        {
            _canvasGroup.blocksRaycasts = false;
            _lastPosition = transform.localPosition;
        }

        public void OnDrag(PointerEventData eventData)
        {
            _rectTransform.anchoredPosition += eventData.delta / _gameCanvas.scaleFactor;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0;
            var hits = Physics2D.RaycastAll(mousePos, Vector2.zero);
  
            var isClickOnTarget = GetTargetCheck(hits);
            _canvasGroup.blocksRaycasts = true;

            if (isClickOnTarget)
                ClickOnTarget();
            else 
                transform.localPosition = _lastPosition;
        }
        
        protected abstract bool GetTargetCheck(RaycastHit2D[] hits);
        protected abstract void ClickOnTarget();
    }
}

