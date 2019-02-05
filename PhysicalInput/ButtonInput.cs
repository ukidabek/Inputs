using UnityEngine;

using System;
using System.Collections;
using UnityEngine.Events;

namespace BaseGameLogic.Inputs
{
    [Serializable] public class ButtonInput : PhysicalInput
    {
        [SerializeField] public KeyCode _keyCode;

        public UnityEvent OnKeyDown = new UnityEvent();
        public UnityEvent OnKey = new UnityEvent();
        public UnityEvent OnKeyUp = new UnityEvent();

        private bool _isKeyUp;
        private bool _isKey;
        private bool _isKeyDown;

        public ButtonInput() {}

        public override bool PositiveReading { get { return _isKeyUp || _isKey || _isKeyDown; } }

        public override void Read ()
    	{
            if (_isKeyDown = Input.GetKeyDown(_keyCode)) OnKeyDown.Invoke();
            if (_isKey = Input.GetKey(_keyCode)) OnKeyDown.Invoke();
            if (_isKeyUp = Input.GetKeyUp(_keyCode)) OnKeyDown.Invoke();
        }
    }
}