using UnityEngine;
using System;

namespace Assets.Scripts.Interactions
{
    public class ToggleLabels : MonoBehaviour
    {
        [SerializeField]
        private InitializeGraphV2 _graph;
        private bool _lastToggleValue = true;

        private void Start()
        {
            if (_graph == null)
            {
                throw new NullReferenceException("[ToggleLabels.Start] Reference to InitializeGraphV2 not set!");
            } 
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
                label.SetActive(_lastToggleValue);
            }

            _lastToggleValue = !_lastToggleValue;
        }
    }
}