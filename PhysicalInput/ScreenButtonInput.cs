using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BaseGameLogic.Inputs
{
    [Serializable] public class ScreenButtonInput : ButtonInput
    {
        [SerializeField] private ScreenButton _screenButton = null;

        public override void Read()
        {
            if(_screenButton != null)
                buttonState = _screenButton.State;
        }
    }
}