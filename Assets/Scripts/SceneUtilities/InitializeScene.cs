using UnityEngine;

namespace Assets.Scripts.SceneUtilities
{
    public class InitializeScene : MonoBehaviour
    {
        private void Start()
        {
            DeactivateDesignTimeCamera();
        }

        // Deactivate design-time camera, if any
        private void DeactivateDesignTimeCamera()
        {
            var designTimeCamera = GameObject.Find("Design Time Camera");
            if (designTimeCamera != null)
                designTimeCamera.SetActive(false);
        }
    }
}