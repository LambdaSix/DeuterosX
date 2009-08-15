/*
 * User: Luaan
 * Date: 24.9.2006
 * Time: 21:45
 */

using System;
using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;

using System.Runtime.InteropServices;

namespace Deuteros
{
	/// <summary>
	/// Engine for rendering graphics.
	/// </summary>
	public class RenderEngine
	{
		/// <summary>
		/// Creates new render engine
		/// </summary>
		/// <param name="target">Render target</param>
		public RenderEngine(System.Windows.Forms.Control target) 
		{
			#if GenLog
			Console.WriteLine("Initializing RenderEngine...");
			#endif
			
			this.target = target;

			pars = new PresentParameters();

			pars.Windowed = true;
			pars.SwapEffect = SwapEffect.Discard;

			pars.AutoDepthStencilFormat = DepthFormat.D16;
			pars.EnableAutoDepthStencil = true;

			device = new Device(0, DeviceType.Hardware, target, CreateFlags.HardwareVertexProcessing/* | CreateFlags.FpuPreserve*/, pars);
			device.DeviceReset += new EventHandler(OnDeviceReset);

			OnDeviceReset(device, null);

			overlay = new Sprite(device);
			
			#if GenLog
			Console.WriteLine("RenderEngine initialized");
			#endif
		}

		[DllImport("Kernel32.dll")]
		private static extern bool QueryPerformanceCounter(
			out long lpPerformanceCount);

		[DllImport("Kernel32.dll")]
		private static extern bool QueryPerformanceFrequency(
			out long lpFrequency);

		private PresentParameters pars;

		private System.Windows.Forms.Control target = null;
		
		private long lastTicks = Environment.TickCount;
		private long lastTickCounter = 0;
		private long lastFPS = 100;

		private bool usesHPT = false;
		private long HPTfreq = 1;
		private long HPTlast = 0;
		private long HPTnow  = 0;
		private float HPTtimePassed = 0.01f;
		private float HPTlowFPS = 0.01f;
		private long HPTlastLow = 0;


		private Sprite overlay;
		/// <summary>
		/// Overlay sprite.
		/// </summary>
		public Sprite Overlay
		{
			get
			{
				return overlay;
			}
		}

		private Device device = null;
		/// <summary>
		/// RenderEngine device
		/// </summary>
		public Device Device
		{
			get
			{
				return device;
			}
		}

		/// <summary>
		/// Gets current FPS accurately.
		/// </summary>
		public float AccurateFPS
		{
			get
			{
				if ( usesHPT )
				{
					return (float)(1.0f / TimePassed);
				}
				else
				{
					// not supported without HPT
					return (float)FPS; 
				}
			}
		}

		/// <summary>
		/// Gets DisplayFPS - uses TickCounter even when HPT available.
		/// </summary>
		public long DisplayFPS
		{
			get
			{
				if ( usesHPT )
				{
					//if ( lastFPS == 0 ) return 1;
					return (long)HPTlowFPS;

					//return lastFPS;
				}
				else
				{
					return FPS;
				}
			}
		}

		/// <summary>
		/// Gets current FPS.
		/// </summary>
		public long FPS
		{
			get
			{
				if ( usesHPT )
				{
					return (long)(1.0f / TimePassed);
				}
				else
				{
					if ( lastFPS == 0 ) return 1;

					return lastFPS;
				}
			}
		}

		/// <summary>
		/// Gets the time passed between this and past frame.
		/// </summary>
		public float TimePassed
		{
			get
			{
				if ( usesHPT )
				{
					if ( HPTtimePassed == 0 ) HPTtimePassed = 0.01f;
					return HPTtimePassed;
				}
				else
				{
					return ( 1.0f / (float)FPS );
				}
			}
		}

		/// <summary>
		/// Begins the RenderEngine scene
		/// </summary>
		public void BeginScene()
		{
			try
			{
				device.Clear(ClearFlags.Target | ClearFlags.ZBuffer, System.Drawing.Color.FromArgb(13, 13, 13), 1.0f, 0);
				
				device.BeginScene();
			}
			catch (DeviceLostException e)
			{
				if ( target == null ) return;
				
				if ( e != null ) {}
				System.Threading.Thread.Sleep(1000);

				device.Reset(pars);
			}
		}

		/// <summary>
		/// End the RenderEngine scene. Does the actual rendering.
		/// </summary>
		public void EndScene()
		{
			try
			{
				device.EndScene();

				device.Present();
			}
			catch (DeviceLostException e)
			{
				if ( target == null ) return;
				
				if ( e != null ) {}
				System.Threading.Thread.Sleep(1000);

				device.Reset(pars);
			}

			if ( usesHPT )
			{
				HPTlast = HPTnow;
				QueryPerformanceCounter(out HPTnow);

				HPTtimePassed = (float)(HPTnow - HPTlast) / (float)HPTfreq;

				if ( lastTickCounter++ >= 50 )
				{
					float pom = ( (float)(HPTnow - HPTlastLow) / (float)(HPTfreq * lastTickCounter) );
					if ( pom == 0 ) pom = 0.01f;

					HPTlowFPS = 1.0f / pom;

					HPTlastLow = HPTnow;

					lastTickCounter = 0;
				}
			}
			else
			{
				if ( lastTickCounter++ >= 50 )  // FPS counter; not as accurate as HPT, supported on every system though. Also good for FPS display.
				{
					int fpspom = (int)(50.0f / ((Environment.TickCount - lastTicks) / 1000.0f));
			
					if ( Math.Abs( fpspom - lastFPS ) > 2.0d ) lastFPS = fpspom;

					lastTicks = Environment.TickCount;
					lastTickCounter = 0;
				}
			}
		}

		private void OnDeviceReset(object sender, EventArgs e)
		{
			#if GenLog
			Console.WriteLine("Resetting RenderEngine device...");
			#endif
			
			if (QueryPerformanceFrequency(out HPTfreq) == false)
			{
				// high-performance counter not supported
				usesHPT = false;
				
				Console.WriteLine("WARNING: High precision timer not available. Using maximal precision possible.");
			}
			else
			{
				usesHPT = true;
				QueryPerformanceCounter(out HPTlast);
				HPTlastLow = HPTlast;
				QueryPerformanceCounter(out HPTnow);

				HPTtimePassed = (float)(HPTnow - HPTlast) / (float)HPTfreq;

				float pom = (float)(HPTnow - HPTlastLow) / (float)HPTfreq;
				if ( pom == 0 ) pom = 0.01f;
				HPTlowFPS = 1.0f / pom;
			}

			Device newDevice = (Device)sender;

			newDevice.RenderState.Lighting = true;
			newDevice.RenderState.ZBufferEnable = true;
			//newDevice.RenderState.FillMode = FillMode.WireFrame;
			//newDevice.RenderState.CullMode = Cull.None;


			GameEngine.Instance.pomViewMatrix = Matrix.LookAtLH( new Vector3( 0.0f, 0.0f,-5.0f ), new Vector3( 0.0f, 0.0f, 0.0f ), new Vector3( 0.0f, 1.0f, 0.0f ) );
			device.Transform.View = GameEngine.Instance.pomViewMatrix;
			GameEngine.Instance.pomProjectionMatrix = Matrix.PerspectiveFovLH( (float)Math.PI / 4.0f, 1.0f, 1.0f, 500.0f );
			device.Transform.Projection = GameEngine.Instance.pomProjectionMatrix;

			device.RenderState.PointSpriteEnable = true;
			device.RenderState.PointScaleEnable = true;
	 		
			device.RenderState.PointScaleA = 100f;
			device.RenderState.PointScaleB = 0f;
			device.RenderState.PointScaleC = 0f;

			device.Lights[0].Type = LightType.Point;
			device.Lights[0].Diffuse = System.Drawing.Color.White;
			device.Lights[0].Range = 500f;
			//device.Lights[0].Direction = new Vector3((float)1.0f, 1.0f, 1.0f);
			device.Lights[0].Position = new Vector3((float)0.0f, 0.0f, -5.0f);

			device.Lights[0].Enabled = true;

			device.RenderState.Ambient = System.Drawing.Color.FromArgb(0x808080);
			
			#if GenLog
			Console.WriteLine("RenderEngine reset.");
			#endif
		}
	}
}

