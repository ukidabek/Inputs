using UnityEngine;

using System;
using System.Collections;
namespace BaseGameLogic.Inputs
{
    [Serializable] public class ButtonInput : PhysicalInput
    {
        private bool IsPressed = false;
        private bool WasPressed = false;

        [SerializeField] protected InputStateEnum buttonState = InputStateEnum.Released;

        [SerializeField] public bool invert = false;

        [SerializeField] private float toAnalogConversionSpeed = 1f;

        public override bool PositiveReading { get { return buttonState == InputStateEnum.Down || buttonState == InputStateEnum.Held; } }

        public override InputStateEnum State { get { return buttonState; } }

		public float AnalogTarget { get { return invert ? -1f : 1f; } }

		public bool Pressed { get { return State == InputStateEnum.Down || State == InputStateEnum.Held; } }

        [SerializeField] private float anagloValue;
        public float AnagloValue
        {
            get
            {
				if (Pressed)
                    return anagloValue = Mathf.MoveTowards(anagloValue, AnalogTarget, toAnalogConversionSpeed * Time.deltaTime);
                else
                    return anagloValue = Mathf.MoveTowards(anagloValue, 0f, toAnalogConversionSpeed * Time.deltaTime);
            }
        }

        public float AnalogValueRaw
        {
            get
            {
                if (Pressed)
                    return anagloValue = AnalogTarget;
                else
                    return anagloValue = 0;
            }
        }

        [SerializeField] public KeyCode keyCode;

        public ButtonInput() {}

        public ButtonInput(KeyCode keyCode)
        {
            this.keyCode = keyCode;
        }

        public override void Read ()
    	{
			WasPressed = IsPressed;
			IsPressed = Input.GetKey(keyCode);

            buttonState = GetStatus(WasPressed, IsPressed);
    	}

        public void SetStatus(bool wasPressed, bool isPressed)
        {
            buttonState = GetStatus(wasPressed, isPressed);
        }

        public static InputStateEnum GetStatus(bool wasPressed, bool isPressed)
        {
            if (!isPressed && !wasPressed)
                return InputStateEnum.Released;
            else if (isPressed && !wasPressed)
                return InputStateEnum.Down;
            else if (isPressed && wasPressed)
                return InputStateEnum.Held;
            else if (!isPressed && wasPressed)
                return InputStateEnum.Up;

            return InputStateEnum.Released;
        }
    }
}