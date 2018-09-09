using UnityEngine;

using System;
using System.Collections;

namespace BaseGameLogic.Inputs
{
	/// <summary>
	/// Physical input.
	/// </summary>
	[Serializable]
	public abstract class PhysicalInput  
	{
		/// <summary>
		/// The name of the input.
		/// </summary>
		[SerializeField]
		private string inputName = "";
		public string InputName 
		{
			get { return this.inputName; }
			set { this.inputName = value; }
		}

		/// <summary>
		/// Indicating whether this input was read or no.
		/// </summary>
		/// <value><c>true</c> if positive reading; otherwise, <c>false</c>.</value>
		public abstract bool PositiveReading { get; }

        public virtual InputStateEnum State { get { return InputStateEnum.Released; } }

        /// <summary>
        /// It defines how the input is read
        /// </summary>
        public abstract void Read();
    }
}