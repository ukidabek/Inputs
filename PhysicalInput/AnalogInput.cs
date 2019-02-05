using UnityEngine;

using System;
using System.Collections;
using UnityEngine.Events;

namespace BaseGameLogic.Inputs
{
    [Serializable] public class AnalogUpdateCallback : UnityEvent<Vector2> {}

	[Serializable]
	public class AnalogInput : PhysicalInput
	{
        [Serializable] private class AxisDefinition
        {
            public string Name = string.Empty;
            public bool Invert = false;
        }

        [SerializeField] private AxisDefinition _horizontal = new AxisDefinition();
        [SerializeField] private AxisDefinition _vertical = new AxisDefinition();

        private Vector2 oldAxisReading = Vector2.zero;
		[SerializeField] private Vector2 axisReading = Vector2.zero;


		public override bool PositiveReading { get { return !axisReading.Equals (oldAxisReading); } }

		public Vector2 Axis { get { return this.axisReading; } }

        public AnalogUpdateCallback AnalogUpdateCallback = new AnalogUpdateCallback();

        public override void Read ()
		{				
			oldAxisReading = axisReading;

            axisReading.x = ReadAxis(_horizontal);
            axisReading.y = ReadAxis(_vertical);

            AnalogUpdateCallback.Invoke(axisReading);
        }

        private float ReadAxis(AxisDefinition axis)
        {
            if (string.IsNullOrEmpty(axis.Name)) return 0;
            return axis.Invert ? -Input.GetAxis(axis.Name) : Input.GetAxis(axis.Name);
        }
    }
}