using UnityEngine;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace BaseGameLogic.Inputs
{
    /// <summary>
    /// Base Input source class. Classes with will extend that class will be used for handling
    /// a input device like key board, game pad or mouse.
    /// </summary>
	public abstract class BaseInputSource : MonoBehaviour
	{
		[Tooltip("The reference to input collector that collect input from this source.")]
        [SerializeField] private BaseInputCollector _owner = null;
        /// <summary>
        /// The reference to input collector that collect input from this source.
        /// </summary>
		public BaseInputCollector Owner 
		{ 
			get { return _owner; } 
			set { _owner = value; } 
		}

        /// <summary>
        /// List of all PhysicalInputs  (buttons, analog values, ect.) that will be checked when this InputSource is collecting it’s inputs.
        /// </summary>
		protected List<PhysicalInput> physicalInputs = new List<PhysicalInput> ();
        /// <summary>
        /// List of all PhysicalInputs  (buttons, analog values, ect.) that will be checked when this InputSource is collecting it’s inputs.
        /// </summary>
        public List<PhysicalInput> PhysicalInputs { get { return physicalInputs; } }

        /// <summary>
        /// Return true if only one PhysicalInput is positive. For example button is pleased or analog value is greater then 0 or less.
        /// </summary>
		public bool PositiveReading 
		{
			get 
			{
                for (int i = 0; i < physicalInputs.Count; i++)
                    if (physicalInputs[i].PositiveReading)
                        return true;

				return false;
			} 
		}
         
        /// <summary>
        /// Vector used for controlling player motion (WASD, left analog on game pad).
        /// </summary>
		public virtual Vector3 MovementVector { get { return Vector3.zero; } }

        /// <summary>
        /// Vector containing a analog values (greater then 0 or less) reader from game pad triggers.
        /// </summary>
		public virtual Vector3 TriggersVector { get { return Vector3.zero; } }

        /// <summary>
        /// Vector used for controlling camera motion (mouse, right analog on game pad).
        /// </summary>
        public virtual Vector3 LookVector { get { return Vector3.zero; } }

		protected virtual void Awake ()
		{
            Type type = this.GetType();

            do
            {
                var fields = type.GetFields(BindingFlags.NonPublic | BindingFlags.Instance);

                for (int i = 0; i < fields.Length; i++)
                    if (fields[i].FieldType.IsSubclassOf(typeof(PhysicalInput)))
                        physicalInputs.Add(fields[i].GetValue(this) as PhysicalInput);

                type = type.BaseType;
            }
            while (type != typeof(BaseInputSource).BaseType);


            if (physicalInputs.Count == 0) 
				Debug.LogError ("No input detected. Add input source and set inputs");
		}

		protected virtual void Start() {}

        /// <summary>
        /// Rad all PhysicalInputs in InputSource.
        /// </summary>
        public virtual void ReadInputs ()
		{
            for (int i = 0; i < physicalInputs.Count; i++)
            {
                PhysicalInput input = physicalInputs[i];
                input.Read();
            }
		}
	}
}