using Player;
using UnityEngine;

namespace MainMenu
{
    public class MenuController : MonoBehaviour
    {
        private void Start()
        {
            Screen.orientation = ScreenOrientation.LandscapeRight;
        }

        public void StartGame()
        {
            LoadingScreenController.Instance.ChangeScene("Game");
        }

        public void OpenMenu()
        {
            LoadingScreenController.Instance.EndFallingStarsAnimation();
        }

        public void CloseMenu()
        {
            LoadingScreenController.Instance.StartFallingStarsAnimation();
        }
    }
}

