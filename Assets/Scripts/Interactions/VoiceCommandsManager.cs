﻿using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.SceneUtilities;
using UnityEngine;
using UnityEngine.Windows.Speech;

namespace Assets.Scripts.Interactions
{
    [RequireComponent(typeof(AudioSource))]
    public class VoiceCommandsManager : MonoBehaviour
    {
        private KeywordRecognizer _keywordRecognizer;
        private Dictionary<string, Action> _commands;
        private AudioSource _audioSource;
        
        public ResetScene ResetScene;
        public InitializeGraph Graph;
        public ToggleLabels ToggleLabels;
        
        private void Start()
        {
            if (ResetScene == null)
                throw new NullReferenceException("[VoiceCommandsManager.Start] Reference to ResetScene not set!");
            if (Graph == null)
                throw new NullReferenceException("[VoiceCommandsManager.Start] Reference to InitializeGraph not set!");
            if (ToggleLabels == null)
                throw new NullReferenceException("[VoiceCommandsManager.Start] Reference to ToggleLabels not set!");

            _commands = new Dictionary<string, Action>();
            _audioSource = gameObject.AddComponent<AudioSource>();
            _audioSource.clip = Resources.Load<AudioClip>("command_sound");
            
            // Reset scene
            _commands.Add("Reset", () =>
            {
                ResetScene.Reset();
            });

            // Toggle labels
            _commands.Add("Text", () =>
            {
                _audioSource.Play();
                ToggleLabels.Toggle();
            });

            // Load first series of edges
            _commands.Add("Series one", () =>
            {
                _audioSource.Play();
                Graph.LoadSeries1();
            });

            // Load second series of edges
            _commands.Add("Series two", () =>
            {
                _audioSource.Play();
                Graph.LoadSeries2();
            });

            // Initialize the keyword recognizer
            _keywordRecognizer = new KeywordRecognizer(_commands.Keys.ToArray());
            _keywordRecognizer.OnPhraseRecognized += CallCommand;
            _keywordRecognizer.Start();
        }

        private void CallCommand(PhraseRecognizedEventArgs args)
        {
            Action command;
            if (_commands.TryGetValue(args.text, out command))
            {
                command.Invoke();
            }
        }
    }
}