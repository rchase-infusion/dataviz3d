using System;
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
        private int _currentlySelectNodeOriginalLabelSize;
        
        [Tooltip("The material applied to the currently selected node. Defaults to 'SelectedNodeMaterial' asset from the Resources folder if not set")]
        public Material SelectedNodeMaterial;
        [Tooltip("Game object that handles the toggling of node labels.")]
        public ToggleLabels ToggleLabels;

        public float MaxHitDetectionDistance;
        public int SelectedNodeLabelSize = 50;

        public GameObject CurrentlySelectedNodeLabel { get; private set; }
        
        private void Start()
        {
            _camera = Camera.main;
            _meshRenderer = gameObject.GetComponent<MeshRenderer>();
            
            if (ToggleLabels == null)
                throw new InvalidOperationException("[Cursor.Start] ToggleLabels was not set!");

            if (SelectedNodeMaterial == null)
                SelectedNodeMaterial = Resources.Load("SelectedNodeMaterial") as Material;
        }

        private void Update()
        {
            var cameraPosition = _camera.transform.position;
            var forwardDirection = _camera.transform.forward;

            // Each frame, check if the user is looking at a node. If yes, change its appearance.
            RaycastHit hit;
            if (Physics.Raycast(cameraPosition, forwardDirection, out hit, MaxHitDetectionDistance))
            {
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

        /// <summary>
        /// Change the appearance of the selected node
        /// </summary>
        private void SetSelectedNode(GameObject selectedGameObject)
        {
            _currentlySelectedNode = selectedGameObject;
            CurrentlySelectedNodeLabel = selectedGameObject.transform.FindChild(Constants.NodeLabelGameObjectName).gameObject;
            
            // Change the appearance of the selected node by applying a different material
            var meshRenderer = _currentlySelectedNode.GetComponent<MeshRenderer>();
            _currentlySelectedNodeOriginalMaterial = meshRenderer.material;
            meshRenderer.material = SelectedNodeMaterial;
            
            // Display the label of the selected node
            _currentlySelectNodeOriginalLabelSize = _currentlySelectedNode.DisplayNodeLabel(SelectedNodeLabelSize);
        }

        /// <summary>
        /// Restore the node to its original appearance
        /// </summary>
        private void ResetSelectedNode()
        {
            if (_currentlySelectedNode == null)
                return;

            // Restore the original material
            var meshRenderer = _currentlySelectedNode.GetComponent<MeshRenderer>();
            meshRenderer.material = _currentlySelectedNodeOriginalMaterial;

            // Depending on whether the labels are being displayed or not, either hide the label or only restore its original size
            if (!ToggleLabels.AreLabelsDisplayed)
                _currentlySelectedNode.HideNodeLabel(_currentlySelectNodeOriginalLabelSize);
            else
                _currentlySelectedNode.RestoreOriginalNodeLabelSize(_currentlySelectNodeOriginalLabelSize);

            _currentlySelectedNode = null;
            CurrentlySelectedNodeLabel = null;
        }
    }
}