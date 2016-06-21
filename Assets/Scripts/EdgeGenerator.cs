using System;
using System.Collections.Generic;
using Assets.Scripts.Edges;
using Assets.Scripts.Utils;
using UnityEngine;
using Random = System.Random;

namespace Assets.Scripts
{
    internal abstract class EdgeGenerator
    {
        protected Random random = new Random();
        String[] eColours = new string[] { "magenta", "cyan" };
        public abstract List<EdgeRawData> GenerateEdges();

        protected Color GetRandomColorColor()
        {
            var color = eColours[(int)Math.Floor(random.NextDouble() * eColours.Length)].ToColor();
            return color;
        }
    }
}