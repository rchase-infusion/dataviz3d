using Assets.Scripts.Utils;
using UnityEngine;

namespace Assets.Scripts.Interactions
{
    [RequireComponent(typeof(MeshRenderer))]
    public class Cursor : MonoBehaviour
    {
        private Camera _camera;
        private MeshRenderer _meshRenderer;

        public float MaxHitDetectionDistance = 30;
        public Color HoverColor = Color.red;
        
        void Start()
        {
            _camera = Camera.main;
            _meshRenderer = gameObject.GetComponent<MeshRenderer>();
        }

        void Update()
        {
            var cameraPosition = _camera.transform.position;
            var forwardDirection = _camera.transform.forward;

            RaycastHit hit;
            if (Physics.Raycast(cameraPosition, forwardDirection, out hit, MaxHitDetectionDistance))
            {
                gameObject.SetPosition(hit.point);
                gameObject.transform.rotation = Quaternion.FromToRotation(Vector3.up, hit.normal);
                _meshRenderer.enabled = true;
                _meshRenderer.material.color = HoverColor;
            }
            else
            {
                _meshRenderer.enabled = false;
            }
        }
    }
}