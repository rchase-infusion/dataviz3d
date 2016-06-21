using Assets.Scripts.Nodes;
using Assets.Scripts.Utils;
using System;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

namespace AssemblyCSharpWSA.Scripts
{
    class SphereGenerator
    {
        private Random random = new Random();
        String[] vColours = { "red", "white", "yellow", "grey" };

        public List<NodeRawData> GetSphere(Vector3 centerPosition, float centerSize, float partSize, float scale, int count, int centerId, bool randomise)
        {
            var result = new List<NodeRawData>();
            var center = new NodeRawData(centerId, "Party " + centerId, "Party " + centerId, centerSize, Color.blue, "Cube", centerPosition);
            result.Add(center);

            var sphere = GetSphereVectors(count, randomise, centerPosition, scale);
            int i = centerId;
            foreach (var vector3 in sphere)
            {
                i++;
                result.Add(new NodeRawData(i, "Party " + i, "Party " + i, partSize, GetRandomColorColor(), "Sphere", vector3));
            }

            return result;
        }

        private Color GetRandomColorColor()
        {
            var color = vColours[(int) Math.Floor(random.NextDouble()*vColours.Length)].ToColor();
            return color;
        }

        private List<Vector3> GetSphereVectors(int count, bool randomize, Vector3 center, float scale)
        {
            float rnd = 1;
            if (randomize)
                rnd = random.Next() * count;

            var points = new List<Vector3>();
            float offset = 2.0f / count;
            float increment = (float) (Math.PI * (3.0 - Math.Sqrt(5.0)));

            for (int i = 0; i < count; i++)
            {
                float y = ((i * offset) - 1) + (offset / 2);
                float r = (float) Math.Sqrt(1 - Math.Pow(y, 2));

                float phi = ((i + rnd) % count) * increment;
                float x = (float) (Math.Cos(phi) * r);
                float z = (float) (Math.Sin(phi) * r);
                points.Add(CreateVector(x, y, z, center, scale));
            }

            return points;
        }

        private Vector3 CreateVector(float x, float y, float z, Vector3 center, float scale)
        {
            x = (x * scale) + center.x;
            y = (y * scale) + center.y;
            z = (z * scale) + center.z;

            return new Vector3(x, y, z);
        }
    }
}
