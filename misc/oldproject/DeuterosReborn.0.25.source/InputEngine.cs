/*
 * User: Luaan
 * Date: 24.9.2006
 * Time: 21:45
 */

using System;

using Microsoft.DirectX;
using Microsoft.DirectX.DirectInput;

namespace Deuteros
{
	/// <summary>
	/// Engine for capturing inputs.
	/// </summary>
	public class InputEngine
	{
		private Device device = null;
		/// <summary>
		/// Returns the DirectInput device.
		/// </summary>
		public Device Device
		{
			get { return device; }
		}

		private System.Windows.Forms.Control target = null;
		/// <summary>
		/// Specifies the capture target (ie. the base form)
		/// </summary>
		public System.Windows.Forms.Control CaptureTarget
		{
			get { return target; }
		}

		/// <summary>
		/// Creates and initializes the input engine.
		/// </summary>
		/// <param name="target">Capture target</param>
		public InputEngine(System.Windows.Forms.Control target)
		{
			this.target = target;

			device = new Device(SystemGuid.Keyboard);
			device.SetCooperativeLevel(target, CooperativeLevelFlags.Background | CooperativeLevelFlags.NonExclusive);
			device.Acquire();
			
			#if GenLog
			Console.WriteLine("InputEngine Initialized");
			#endif
		}

		/// <summary>
		/// Deinitializes the input engine.
		/// </summary>
		public void Deinitialize()
		{
			device.Unacquire();
			
			#if GenLog
			Console.WriteLine("InputEngine Deinitialized");
			#endif
		}
	}

}
