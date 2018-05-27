﻿using UnityEngine;

using System.Collections;
using System.Collections.Generic;

using BaseGameLogic.Singleton;
using System;

namespace BaseGameLogic.Inputs
{
	public class InputCollectorManager : Singleton<InputCollectorManager>
    {
        [SerializeField, HideInInspector] private List<BaseInputCollector> _inputCollectors = new List<BaseInputCollector>();
        public BaseInputCollector this [int index] { get { return _inputCollectors[index]; } }
        public int Count { get { return _inputCollectors.Count; } }

        /// <summary>
        /// 
        /// </summary>
		private Dictionary<int, BaseInputCollector> _inputCollectorsDictionary = new Dictionary<int, BaseInputCollector>();
        
		protected override void Awake()
		{
            base.Awake();

            // Creating dictionary from collected InputCollectors.
            for (int i = 0; i < _inputCollectors.Count; i++) 
				_inputCollectorsDictionary.Add(_inputCollectors [i].PlayerNumber, _inputCollectors [i]);
		}

        /// <summary>
        /// Returns InputCollector by it's PlayerNumber. 
        /// If there are only one InputCollector instance method will return that one.
        /// </summary>
        /// <param name="playerNumber">PlayerNumber</param>
        /// <returns>InputCollector instance.</returns>
		public BaseInputCollector GetInputCollector(int playerNumber = 0)
		{
			BaseInputCollector _inputCollector = null;

            if(_inputCollectorsDictionary.Count == 1)
                _inputCollectorsDictionary.TryGetValue(0, out _inputCollector);
            else
                _inputCollectorsDictionary.TryGetValue (playerNumber, out _inputCollector);

			return _inputCollector;
		}

        public void AddInputCollector()
        {
            Type[] types = AssemblyExtension.GetDerivedTypes<BaseInputCollector>();
            if(types != null && types.Length > 0 && _inputCollectors.Count <= 0)
            {
                GameObject gameObject = new GameObject();
                gameObject.transform.SetParent(this.transform, false);
                _inputCollectors.Add(gameObject.AddComponent(types[0]) as BaseInputCollector);
                int lastIndex = _inputCollectors.Count - 1;
                _inputCollectors[lastIndex].PlayerNumber = lastIndex;
                _inputCollectors[lastIndex].gameObject.name = string.Format("Player {0}", lastIndex);
            }
            else
            {
                Debug.LogError("There is no class that extends abstract class BaseInputCollector.");
            }
        }

        public void AddInputCollector(GameObject inputCollectorPrefab, int playerID = -1)
        {
            BaseInputCollector inputCollector = Instantiate(inputCollectorPrefab, this.transform, false).GetComponent<BaseInputCollector>();
            inputCollector.transform.Reset();
            inputCollector.PlayerNumber = playerID > -1 ? playerID : _inputCollectorsDictionary.Count;

            inputCollector.gameObject.name = string.Format("Player {0}", inputCollector.PlayerNumber);
            _inputCollectors.Add(inputCollector);
            _inputCollectorsDictionary.Add(inputCollector.PlayerNumber, inputCollector);
        }

        public void RemoveAt(int index)
        {
            DestroyImmediate(_inputCollectors[index].gameObject);
            _inputCollectors.RemoveAt(index);
        }
	}
}