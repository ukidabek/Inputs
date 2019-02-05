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
		/// Indicating whether this input was read or no.
		/// </summary>
		/// <value><c>true</c> if positive reading; otherwise, <c>false</c>.</value>
		public abstract bool PositiveReading { get; }

        /// <summary>
        /// It defines how the input is read
        /// </summary>
        public abstract void Read();
    }
}