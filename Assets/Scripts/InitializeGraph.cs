using System.Collections.Generic;
using AssemblyCSharpWSA.Scripts;
using Assets.Scripts.Edges;
using Assets.Scripts.Nodes;
using UnityEngine;

namespace Assets.Scripts
{
    public class InitializeGraph : MonoBehaviour
    {
        private int i = 0;
        private int countFirstSphere;
        private int countSecondSphere;
        private int firstCenterId;
        private int secondCenterId;
        private IGraphGenerator _graphGenerator;
        private SphereGenerator _sphereGenerator;
        private List<EdgeGenerator> _edgeGenerators;

        public IEnumerable<GameObject> Nodes { get; private set; }
        public IEnumerable<GameObject> Labels { get; private set; }
        
        void Start()
        {
            countFirstSphere = 250;
            countSecondSphere = 100;
            firstCenterId = 0;
            secondCenterId = countFirstSphere + 1;
            _graphGenerator = new GraphGenerator(GameObject.Find("NODES"), GameObject.Find("EDGES"));
            _sphereGenerator = new SphereGenerator();
            Labels = new List<GameObject>();
            _edgeGenerators = new List<EdgeGenerator>
            {
                new SimpleAllEdges(countFirstSphere, firstCenterId, countSecondSphere, secondCenterId),
                new ComplexAllEdges(countFirstSphere, firstCenterId, countSecondSphere, secondCenterId),
                new ComplexAllEdges2(countFirstSphere + countSecondSphere, firstCenterId),
                new TrailingEdges(countFirstSphere + countSecondSphere, firstCenterId),
                new EmptySet(),
                new SimpleEdgesReduced(countFirstSphere, firstCenterId, countSecondSphere, secondCenterId, 2),
                new SimpleEdgesReduced(countFirstSphere, firstCenterId, countSecondSphere, secondCenterId, 5),
                new SimpleEdgesReduced(countFirstSphere, firstCenterId, countSecondSphere, secondCenterId, 7),

            };
            Initialize();
        }

        private void Initialize()
        {
            var nodes = _sphereGenerator.GetSphere(new Vector3(-1, 0, 3), 0.07f, .02f, .4f, countFirstSphere, firstCenterId, false);
            nodes.AddRange(_sphereGenerator.GetSphere(new Vector3(1, 0, 3), 0.04f, .01f, .2f, countSecondSphere, secondCenterId, true));
            Nodes = _graphGenerator.GenerateNodes(nodes);
            Labels = _graphGenerator.GenerateNodeLabels(Nodes, false);
            ClearAllEdges();
        }

        private void ClearAllEdges()
        {
            var edgesContainer = GameObject.Find("EDGES");

            foreach (Transform edge in edgesContainer.transform)
            {
                GameObject.Destroy(edge.gameObject);
            }
        }

        public void LoadNextSeries()
        {
            ClearAllEdges();
            var edgesRawData = _edgeGenerators[i++ % _edgeGenerators.Count].GenerateEdges();
            _graphGenerator.GenerateEdges(Nodes, edgesRawData);

        }
    }
}