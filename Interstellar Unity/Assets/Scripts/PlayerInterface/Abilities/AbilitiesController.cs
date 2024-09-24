using Player;
using UnityEngine;

namespace PlayerInterface.Abilities
{
    public class AbilitiesController : MonoBehaviour
    {
        [SerializeField] private Scissors _scissorsAbility;
        [SerializeField] private Pencil _pencilAbility;
        [SerializeField] private Key _keyAbility;

        private void Start()
        {
            LoadAbiliesData();
        }

        private void LoadAbiliesData()
        {
            _scissorsAbility.SetAbilityState(PlayerDataKeys.ScissorsDataKey);
            _pencilAbility.SetAbilityState(PlayerDataKeys.PencilsDataKey);
            _keyAbility.SetAbilityState(PlayerDataKeys.KeysDataKey);
        }
    }
}

