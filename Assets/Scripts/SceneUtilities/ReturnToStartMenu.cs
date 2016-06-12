using Assets.Scripts.Utils;
using UnityEngine;

namespace Assets.Scripts.SceneUtilities
{
    public class ReturnToStartMenu : MonoBehaviour
    {
        // If Escape key is pressed, return to start menu
        void OnGUI()
        {
            var currentEvent = Event.current;

            if (currentEvent.type != EventType.KeyUp)
                return;

            if (currentEvent.keyCode == KeyCode.Escape)
                DevHelper.GoToStartMenu();
        }

        public void GoToStartMenu()
        {
            DevHelper.GoToStartMenu();
        }
    }
}