using System.Collections;
using GameField.Nodes;
using UnityEngine;

namespace PlayerInterface.Abilities
{
   public class Key : Ability
    {
        protected override IEnumerator UseAbility(Collider2D _target)
        {
            yield return null;
            var lockNode = _target.GetComponent<LockNode>();
            lockNode.Unlock();
        }
    }
}
