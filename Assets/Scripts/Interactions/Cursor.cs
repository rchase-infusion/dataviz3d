using Assets.Scripts.Utils;
using UnityEngine;

namespace Assets.Scripts.Interactions
{
    [RequireComponent(typeof(MeshRenderer))]
    public class Cursor : MonoBehaviour
    {
        private Camera _camera;
        private MeshRenderer _meshRenderer;
        private GameObject _currentlySelectedNode;
        private Material _currentlySelectedNodeOriginalMaterial;

        [Tooltip("The material applied to the currently selected node. Defaults to 'SelectedNodeMaterial asset from the Resources folder' if not set")]
        public Material SelectedNodeMaterial;
        public float MaxHitDetectionDistance;
        public Color HoverColor;
        
        void Start()
        {
            _camera = Camera.main;
            _meshRenderer = gameObject.GetComponent<MeshRenderer>();
            
            if (SelectedNodeMaterial == null)
                SelectedNodeMaterial = Resources.Load("SelectedNodeMaterial") as Material;
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

                var collidedGameObject = hit.collider.gameObject;
                if (collidedGameObject.IsGraphNode() && collidedGameObject != _currentlySelectedNode)
                {
                    if (_currentlySelectedNode != null)
                        ResetSelectedNode();

                    SetSelectedNode(collidedGameObject);
                }
            }
            else
            {
                ResetSelectedNode();
                _meshRenderer.enabled = false;
            }
        }

        private void SetSelectedNode(GameObject selectedGameObject)
        {
            _currentlySelectedNode = selectedGameObject;
            var meshRenderer = _currentlySelectedNode.GetComponent<MeshRenderer>();

            _currentlySelectedNodeOriginalMaterial = meshRenderer.material;
            meshRenderer.material = SelectedNodeMaterial;
        }

        private void ResetSelectedNode()
        {
            if (_currentlySelectedNode == null)
                return;

            var meshRenderer = _currentlySelectedNode.GetComponent<MeshRenderer>();
            meshRenderer.material = _currentlySelectedNodeOriginalMaterial;

            _currentlySelectedNode = null;
        }
    }
}