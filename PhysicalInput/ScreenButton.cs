using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace BaseGameLogic.Inputs
{
    public class ScreenButton : Button
    {
        private bool _isHeald = false;
        private bool _oldIsHeald = false;

        private InputStateEnum _state = InputStateEnum.Released;
        public InputStateEnum State { get { return _state; } }

        public override void OnPointerDown(PointerEventData eventData)
        {
            base.OnPointerDown(eventData);
            _isHeald = true;
        }

        public override void OnPointerUp(PointerEventData eventData)
        {
            base.OnPointerUp(eventData);
            _isHeald = false;
        }

        private void Update()
        {
            _state = ButtonInput.GetStatus(_oldIsHeald, _isHeald);
            _oldIsHeald = _isHeald;
        }
    }
}
