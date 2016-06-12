using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Edges
{
    public class DrawEdges : MonoBehaviour
    {
        private LineRenderer _lineRenderer;
        private GameObject _node1;
        private GameObject _node2;

        private void Start()
        {
            var nodes = new List<GameObject>();
            nodes.AddRange(GameObject.FindGameObjectsWithTag("Person"));
            nodes.AddRange(GameObject.FindGameObjectsWithTag("Organisation"));
            nodes.AddRange(GameObject.FindGameObjectsWithTag("Product"));

            Draw(nodes);
        }

        private void Update()
        {}

        private void Draw(List<GameObject> nodes)
        {
            _node1 = nodes[0];
            _node2 = nodes[1];

            var newLine = new GameObject("line");
            _lineRenderer = newLine.AddComponent<LineRenderer>();
            _lineRenderer.SetWidth(0.1f, 0.1f);
            _lineRenderer.SetColors(Color.red, Color.red);
            _lineRenderer.SetVertexCount(2);
            _lineRenderer.SetPosition(0, _node1.transform.position);
            _lineRenderer.SetPosition(1, _node2.transform.position);
        }
    }
}