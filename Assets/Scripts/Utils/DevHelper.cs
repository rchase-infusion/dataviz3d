using UnityEngine.SceneManagement;

namespace Assets.Scripts.Utils
{
    public static class DevHelper
    {
        public static void GoToStartMenu()
        {
            var startMenuBuildIndex = 0;

            if (SceneManager.GetActiveScene().buildIndex == startMenuBuildIndex)
                return;

            SceneManager.LoadScene(startMenuBuildIndex);
        }
    }
}