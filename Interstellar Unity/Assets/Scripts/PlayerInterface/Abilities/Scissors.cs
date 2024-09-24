using System.Collections;
using UnityEngine;

namespace PlayerInterface.Abilities
{
    public class Scissors : Ability
    {
        protected override IEnumerator UseAbility(Collider2D _target)
        {
            yield return null;
            Destroy(_target.gameObject);
            OnCheckCollision?.Invoke();
        }
    }
}

