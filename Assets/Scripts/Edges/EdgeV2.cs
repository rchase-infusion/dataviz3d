using UnityEngine;

namespace Assets.Scripts.Edges
{
    [RequireComponent(typeof(LineRenderer))]
    public class EdgeV2 : MonoBehaviour
    {
        public int Id;
        public string Name;
        public GameObject ParentNode;
        public GameObject ChildNode;
        public float Thickness;
        public Color Color;

        private LineRenderer _lineRenderer;

        public void InitializeFrom(EdgeRawDataV2 edgeRawData, GameObject parentNode, GameObject childNode)
        {
            Id = edgeRawData.Id;
            Name = edgeRawData.Name;
            ParentNode = parentNode;
            ChildNode = childNode;
            Thickness = edgeRawData.Thickness;
            Color = edgeRawData.Color;
        }

        public void Draw()
        {
            if (_lineRenderer == null)
            {
                _lineRenderer = gameObject.GetComponent<LineRenderer>();
                var lineMaterial = Resources.Load("LineMaterial") as Material;
                _lineRenderer.material = lineMaterial;
            }
                
            _lineRenderer.SetWidth(Thickness, Thickness);
//            _lineRenderer.SetColors(Color, Color);
            _lineRenderer.SetVertexCount(2);
            _lineRenderer.material.color = Color;

            // Draw edge
            _lineRenderer.SetPosition(0, ParentNode.transform.position);
            _lineRenderer.SetPosition(1, ChildNode.transform.position);
        }

        // Manual clean up apparently needed
        private void OnDestroy()
        {
            if (_lineRenderer != null)
                Destroy(_lineRenderer.material);
        }
    }
}