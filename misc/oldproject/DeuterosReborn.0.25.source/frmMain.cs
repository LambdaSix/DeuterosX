/*
 * User: Luaan
 * Date: 24.9.2006
 * Time: 21:28
 */

using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Xml;

using Microsoft.DirectX;

namespace Deuteros
{
	/// <summary>
	/// Description of frmMain.
	/// </summary>
	public class frmMain: System.Windows.Forms.Form 
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		
		bool isLoaded = false;
		
		/// <summary>
		/// Disposes resources used by the form.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent()
		{
			Version ver = System.Reflection.Assembly.GetCallingAssembly().GetName().Version;
			
			this.SuspendLayout();
			// 
			// frmMain
			// 
			this.ClientSize = new System.Drawing.Size(800, 600);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.Name = "frmMain";
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.Text = "Deuteros Reborn - version " + ver.Major.ToString() + "." + ver.Minor.ToString("d2") + " (Build: " + ver.Build.ToString() + ")";
			this.Load += new System.EventHandler(this.FrmMainLoad);
			this.MouseEnter += new EventHandler(frmMainMouseEnter);
			this.MouseLeave += new EventHandler(frmMainMouseLeave);
			this.ResumeLayout(false);
		}

		/// <summary>
		/// Creates a new instance of frmMains
		/// </summary>
		public frmMain()
		{
			InitializeComponent();

			this.SetStyle(ControlStyles.AllPaintingInWmPaint | 
				ControlStyles.Opaque, true);		
		}
		
		/// <summary>
		/// Initialize the game engine.
		/// </summary>
		public void Initialize()
		{
			GameEngine.Instance.Initialize(this);
			GameEngine.Instance.OnQuit += new EventHandler(Instance_OnQuit);
			
			isLoaded = true;
		}
		
		private void Instance_OnQuit(object sender, EventArgs e)
		{
			this.Close();
		}
		
		/// <summary>
		/// Performs an OnPaint event.
		/// </summary>
		/// <param name="e">PaintEvent arguments</param>
		protected override void OnPaint(PaintEventArgs e)
		{
			//base.OnPaint (e);
			//this.CreateGraphics().FillRectangle(new SolidBrush(Color.Red), 0, 0, 640, 480);
			if ( !isLoaded )
			{
				base.OnPaint(e);
				
				return;
			}
			
			if ( !GameEngine.Instance.hasQuit )
			{
				GameEngine.Instance.Frame();
			}

			if ( GameEngine.Instance.hasQuit ) 
			{
				System.Threading.Thread.Sleep(0);

				//Close();
				
				isLoaded = false;
				
				GameEngine.Instance.Deinitialize();
				
				Timer tim = new Timer();
				tim.Interval = 100;
				tim.Tick += new EventHandler(DoClose);
				
				tim.Enabled = true;
				//Invoke( new EventHandler( Close ) );

				return;
			}
			else
			{			
				this.Invalidate();
			}
		}
		
		private void DoClose(object sender, EventArgs e)
		{
			Close();
		}
		
		private void frmMain_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			//TestEngine.Instance.Renderer.Device.Dispose();
			//GameEngine.Instance.Deinitialize();
		}		
		
		[DllImport("kernel32")]
		static extern IntPtr GetConsoleWindow();
		
		[DllImport("user32")]
		static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter,
			IntPtr x, IntPtr y, IntPtr cx, IntPtr cy, IntPtr flags);

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main(string[] pars)
		{
			#if !DEBUG
			System.IO.StreamWriter wri = System.IO.File.CreateText(@"output.log");
			
			Console.SetOut(wri);
			#else
         	Console.SetWindowSize(1, 1);
         	Console.SetBufferSize(120, 600);
         	Console.SetWindowSize(120, 25);
			Console.Title = "DR Console - Deuteros Reborn";
			#endif
			
			using ( frmMain frm = new frmMain() )
			{
				try
				{
					if ( pars.Length > 0 )
					{
						for ( int i = 0; i < pars.Length; i ++ )
						{
							if ( pars[i] == "-editor" )
							{
								frm.Left = 0;
								frm.Top = 0;
								
								frmEditor pom = new frmEditor();
								pom.Left = frm.Width;
								pom.Top = 0;
								pom.Width = Screen.FromControl(frm).WorkingArea.Width - pom.Left;
								pom.Height = frm.Height;
								pom.StartPosition = FormStartPosition.Manual;
								
								pom.Show();
								pom.Update();
								
								#if DEBUG
								IntPtr con = GetConsoleWindow();
								SetWindowPos(con, IntPtr.Zero, new IntPtr(0), new IntPtr(frm.Height), new IntPtr(Screen.FromControl(frm).WorkingArea.Width), new IntPtr(Screen.FromControl(frm).WorkingArea.Height - frm.Height), new IntPtr(0));
								#endif
							}
							else
							if ( pars[i].StartsWith("-mod") )
							{
								GameEngine.Instance.Mod = pars[i].Remove(0, 4);
							}
						}
					}
				
					frm.Initialize();
					#if DEBUG
					Console.Title = "DR Console - " + GameEngine.Instance.CurrentModInfo.title;
					#endif					
					
					GameEngine.Instance.SaveGame("_newGame", "New Game");
					
					GameEngine.Instance.SaveMenus();
					GameEngine.Instance.SaveFonts("Fonts");
					GameEngine.Instance.SaveTextures(GameEngine.Instance.GuiTextures, "GuiTextures");
					GameEngine.Instance.SaveItemTemplates("Items");
                    GameEngine.Instance.SaveUniverse("Universe");
					
					frm.Show();
					
					//ShowCursor(false);
					//Cursor.Hide();
					Application.Run(frm);
					//Cursor.Show();
					//ShowCursor(true);
				}
				#if !DEBUG
				catch ( ObjectDisposedException e )
				{
					Console.WriteLine(e.ToString());
					MessageBox.Show(e.ToString());
					#if DEBUG
					Console.ReadLine();
					#endif
				}
				catch ( Exception e )
				{
					Console.WriteLine(e.ToString());
					MessageBox.Show(e.ToString());
					#if DEBUG
					Console.ReadLine();
					#endif				
				}
				#endif
				finally
				{
					
				}
			}			
			
			Console.WriteLine("Closing...");
			
			#if !DEBUG
			wri.Close();
			#endif
			System.Threading.Thread.Sleep(3000);
		}	
		
		void frmMainMouseEnter(object sender, EventArgs e)
		{
			Cursor.Hide();
		}
		
		void frmMainMouseLeave(object sender, EventArgs e)
		{
			Cursor.Show();
		}
		
		void FrmMainLoad(object sender, System.EventArgs e)
		{
		
		}
	}
}
