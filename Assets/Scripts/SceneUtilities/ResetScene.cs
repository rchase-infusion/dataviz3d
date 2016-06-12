using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.SceneUtilities
{
    public class ResetScene : MonoBehaviour
    {
        public void Reset()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}