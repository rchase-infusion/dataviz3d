using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.SceneUtilities
{
    public class StartMenu : MonoBehaviour
    {
        public void LoadGraphScene()
        {
            Load(1);
        }

        private void Load(int levelId)
        {
            SceneManager.LoadScene(levelId);
        }
    }
}