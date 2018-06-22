﻿using UnityEngine;

using BaseGameLogic.Character;

namespace BaseGameLogic.Inputs
{
    public abstract class BaseInputHandlingModule<T> : MonoBehaviour where T : BaseInputSource
    {
        [SerializeField] protected PlayerCharacterController _playerController = null;

        [SerializeField] protected T _currentinputSourceDefinition = null;

        [SerializeField] protected BaseInputCollector inputCollector = null;
        public BaseInputCollector InputCollector
        {
            get { return this.inputCollector; }
            protected set { inputCollector = value; }
        }

        protected T ConvertToInputSourceDefinition(BaseInputSource source)
        {
            if (source is T) return source as T;

            return null;
        }

        protected void Start()
        {
            if (InputCollectorManager.Instance != null)
            {
                InputCollector = InputCollectorManager.Instance.GetInputCollector(_playerController.PlayerNumber);
                _currentinputSourceDefinition = ConvertToInputSourceDefinition(InputCollector.CurrentInputSourceInstance);

                InputCollector.InputSourceChanged -= InputSourceChanged;
                InputCollector.InputSourceChanged += InputSourceChanged;
            }
        }

        private void OnDestroy()
        {
            if (InputCollector != null)
            {
                InputCollector.InputSourceChanged -= InputSourceChanged;
            }
        }

        private void InputSourceChanged(BaseInputSource source)
        {
            _currentinputSourceDefinition = ConvertToInputSourceDefinition(source);
        }
    }
}
