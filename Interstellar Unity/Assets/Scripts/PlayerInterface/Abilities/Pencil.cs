using System.Collections;
using GameField.Nodes;
using UnityEngine;

namespace PlayerInterface.Abilities
{
    public class Pencil : Ability
    {
        protected override IEnumerator UseAbility(Collider2D _target)
        {
            var node = _target.GetComponent<Node>();
            yield return node.StartDestroyNode();
            OnCheckCollision?.Invoke();
        }
    }
}
