using Assets.Scripts.Utils;
using UnityEngine;

namespace Assets.Scripts.Interactions
{
    [RequireComponent(typeof(MeshRenderer))]
    public class Cursor : MonoBehaviour
    {
        private Camera _camera;
        private MeshRenderer _meshRenderer;

        public float MaxHitDetectionDistance = 5;
        public Color DefaultColor = Color.white;
        public Color HoverColor = Color.red;
        
        void Start()
        {
            _camera = Camera.main;
            _meshRenderer = gameObject.GetComponent<MeshRenderer>();
            _meshRenderer.material.color = DefaultColor;
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
            }
            else
            {
                _meshRenderer.enabled = false;
            }
        }
    }
}