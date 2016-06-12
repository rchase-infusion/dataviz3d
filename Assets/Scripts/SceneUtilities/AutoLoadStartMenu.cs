using Assets.Scripts.Utils;
using UnityEngine;

namespace Assets.Scripts.SceneUtilities
{
    /// <summary>
    /// This script should be used only once from the init_scene (scene that loads first when the game is launched)!
    /// </summary>
    public class AutoLoadStartMenu : MonoBehaviour
    {
        private void Update()
        {
            DevHelper.GoToStartMenu();
        }
    }
}