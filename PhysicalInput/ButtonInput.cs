using UnityEngine;

using System;
using System.Collections;
namespace BaseGameLogic.Inputs
{
    [Serializable] public class ButtonInput : PhysicalInput
    {
        private bool IsPressed = false;
        private bool WasPressed = false;

        [SerializeField] protected ButtonStateEnum buttonState = ButtonStateEnum.Released;

        [SerializeField] public bool invert = false;

        [SerializeField] private float toAnalogConversionSpeed = 1f;

        public override bool PositiveReading { get { return buttonState == ButtonStateEnum.Down || buttonState == ButtonStateEnum.Held; } }

        public virtual ButtonStateEnum State { get { return buttonState; } }

		public float AnalogTarget { get { return invert ? -1f : 1f; } }

		public bool Pressed { get { return State == ButtonStateEnum.Down || State == ButtonStateEnum.Held; } }

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

        public static ButtonStateEnum GetStatus(bool wasPressed, bool isPressed)
        {
            if (!isPressed && !wasPressed)
                return ButtonStateEnum.Released;
            else if (isPressed && !wasPressed)
                return ButtonStateEnum.Down;
            else if (isPressed && wasPressed)
                return ButtonStateEnum.Held;
            else if (!isPressed && wasPressed)
                return ButtonStateEnum.Up;

            return ButtonStateEnum.Released;
        }
    }
}