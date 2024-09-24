using MovableItem;
using UnityEngine;

namespace GameField.Nodes
{
    public class Movement : Drag
    {
        protected override void ClickOnTarget()
        {
            OnCheckCollision?.Invoke();
        }

        protected override bool GetTargetCheck(RaycastHit2D[] hits)
        {
            foreach(var hit in hits)
            {
                if (hit.collider != null && hit.collider.CompareTag("GameField"))
                    return true;
            }

            return false;
        }
    }
}

