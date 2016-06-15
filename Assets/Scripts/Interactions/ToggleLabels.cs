using UnityEngine;
using System;

namespace Assets.Scripts.Interactions
{
    public class ToggleLabels : MonoBehaviour
    {
        [SerializeField] private InitializeGraphV2 _graph;
        [SerializeField] private Cursor _cursor;

        private bool _lastToggleValue = true;
        
        public bool AreLabelsDisplayed { get { return !_lastToggleValue; } }

        private void Start()
        {
            if (_cursor == null)
                throw new NullReferenceException("[ToggleLabels.Start] Reference to Cursor not set!");

            if (_graph == null)
                throw new NullReferenceException("[ToggleLabels.Start] Reference to InitializeGraphV2 not set!");
        }

        private void Update()
        {
            if (Input.GetKeyUp(KeyCode.T))
            {
                Toggle();
            }
        }

        public void Toggle()
        {            
            foreach (var label in _graph.Labels)
            {
                if (_cursor.CurrentlySelectedNodeLabel != label) // Dont change the state of the currently selected node
                    label.SetActive(_lastToggleValue);
            }

            _lastToggleValue = !_lastToggleValue;
        }
    }
}