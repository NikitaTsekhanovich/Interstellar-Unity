using System;
using MainMenu;
using UnityEngine;

namespace PlayerInterface
{
    public class GameMenuController : MonoBehaviour
    {
        public static Action OnRestart;

        public void BackToMenu()
        {
            LoadingScreenController.Instance.ChangeScene("Menu");
        }

        public void RestartLevel()
        {
            OnRestart?.Invoke();
        }
    }
}

