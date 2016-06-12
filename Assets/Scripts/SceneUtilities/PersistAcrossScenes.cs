﻿using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    /// <summary>
    /// Apply this script to GameObjects you want to persist from one scene to another.
    /// Such GameObjects should be loaded only once (i.e. on a scene that is loaded at game startup and never loaded again).
    /// </summary>
    public class PersistAcrossScenes : MonoBehaviour
    {
        private static List<string> _createdGameObjects = new List<string>();

        private void Awake()
        {
            var currentObject = transform.gameObject;

            if (!_createdGameObjects.Contains(currentObject.name))
            {
                DontDestroyOnLoad(currentObject);
                _createdGameObjects.Add(currentObject.name);
            }
        }
    }
}