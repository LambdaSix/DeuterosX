/*
 * User: Luaan
 * Date: 24.9.2006
 * Time: 21:34
 */

using System;
using System.Collections;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;
using System.Xml;

using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;

using Keys = System.Windows.Forms.Keys;
using MouseButtons = System.Windows.Forms.MouseButtons;

using Deuteros.Gui;
using Deuteros.Items;
using Deuteros.Menu;
using Deuteros.Stations;
using Deuteros.Teams;
using Deuteros.Universe;

namespace Deuteros
{
	/// <summary>
	/// Basic class of the Blackbird Engine.
	/// </summary>
	public sealed class GameEngine
	{
		/// <summary>
		/// Singleton for GameEngine
		/// </summary>
		public static readonly GameEngine Instance = new GameEngine();
		
		private Microsoft.DirectX.Direct3D.Font fntMain
		{
			get { return GuiFonts["fntMain"].DXFont; }
		}
		private Microsoft.DirectX.Direct3D.Font fntMenuButton
		{
			get { return GuiFonts["fntMenuButton"].DXFont; }
		}
		private Microsoft.DirectX.Direct3D.Font fntGUI
		{
			get { return GuiFonts["fntGui"].DXFont; }
		}

		private System.Windows.Forms.Control target = null;
		/// <summary>
		/// Renderer target
		/// </summary>
		public System.Windows.Forms.Control Target
		{
			get
			{
				return target;
			}
		}
		private RenderEngine renderEngine = null;
		/// <summary>
		/// Rendering engine
		/// </summary>
		public RenderEngine RenderEngine
		{
			get
			{
				return renderEngine;
			}
		}

		private InputEngine inputEngine = null;
		/// <summary>
		/// Input engine
		/// </summary>
		public InputEngine InputEngine
		{
			get
			{
				return inputEngine;
			}
		}
		
		#region Camera info
		private float camX = 0.0f;
		/// <summary>
		/// Gets or sets X-coordinate of the camera.
		/// </summary>
		public float CameraX { get { return camX; } set { camX = value; } } 
	
		private float camY = 0.0f;
		/// <summary>
		/// Gets or sets Y-coordinate of the camera.
		/// </summary>
		public float CameraY { get { return camY; } set { camY = value; } }

		private float camZ = 0.0f;
		/// <summary>
		/// Gets or sets Z-coordinate of the camera.
		/// </summary>
		public float CameraZ { get { return camZ; } set { camZ = value; } }

		private float camTrgX = 0.0f;
		/// <summary>
		/// Gets or sets X-coordinate of the camera target.
		/// </summary>
		public float CameraTargetX { get { return camTrgX; } set { camTrgX = value; } }

		private float camTrgY = 0.0f;
		/// <summary>
		/// Gets or sets Y-coordinate of the camera target.
		/// </summary>
		public float CameraTargetY { get { return camTrgY; } set { camTrgY = value; } }

		private float camTrgZ = 0.0f;
		/// <summary>
		/// Gets or sets Z-coordinate of the camera target.
		/// </summary>
		public float CameraTargetZ { get { return camTrgZ; } set { camTrgZ = value; } }

		private float camUpX = 0.0f;
		/// <summary>
		/// Gets or sets X-coordinate of the camera Up vector.
		/// </summary>
		public float CameraUpX { get { return camUpX; } set { camUpX = value; } }

		private float camUpY = 0.0f;
		/// <summary>
		/// Gets or sets Y-coordinate of the camera Up vector.
		/// </summary>
		public float CameraUpY { get { return camUpY; } set { camUpY = value; } }

		private float camUpZ = 0.0f;
		/// <summary>
		/// Gets or sets Z-coordinate of the camera Up vector.
		/// </summary>
		public float CameraUpZ { get { return camUpZ; } set { camUpZ = value; } }

		#endregion;
		
		/// <summary>
		/// Main texture pool.
		/// </summary>
		public TextureManager mainTextures = null;
		private bool renderReady = false;

		private Sprite Overlay = null;
		
		private bool gameRunning = false;

		/// <summary>
		/// Collection of all loaded textures managed by the core
		/// </summary>
		public Dictionary<string, TextureInfo> GuiTextures = null;

		/// <summary>
		/// Gets or sets whether has the game already started or not.
		/// Note: When false, PlayerActor should not be active.
		/// </summary>
		public bool GameRunning { get { return gameRunning; } set { gameRunning = value; } }

		private bool showMenu = false;
		private MenuType menu = MenuType.MainMenu;
		private MenuData menuData = null;

		/// <summary>
		/// Specifies whether or not show the menu
		/// </summary>
		public bool ShowMenu { get { return showMenu; } set { showMenu = value; } }
		/// <summary>
		/// Specifies the type of the menu
		/// </summary>
		public MenuType Menu { get { return menu; } set { menu = value; } }

		private bool showMessage = false;
		private string messageText = "";

		/// <summary>
		/// Specifies whether to show the message box or not
		/// </summary>
		public bool ShowMessage { get { return showMessage; } set { showMessage = value; } }
		/// <summary>
		/// Specifies the message text
		/// </summary>
		public string MessageText { get { return messageText; } set { messageText = value; } }

		private Point mouseCoords = new Point(0,0);
		/// <summary>
		/// Gets the current mouse coordinates.
		/// </summary>
		public Point MouseCoords { get { return mouseCoords; } }

		private MouseButtons mouseButton = MouseButtons.None;
		/// <summary>
		/// Gets the mouse button pushed, if any.
		/// </summary>
		public MouseButtons MouseButton { get { return mouseButton; } }

		private bool isMouseDown = false;
		/// <summary>
		/// Gets whether any mouse button is pushed.
		/// </summary>
		public bool IsMouseDown { get { return isMouseDown; } }

		/// <summary>
		/// OnQuit event handler.
		/// </summary>
		public event EventHandler OnQuit;

		/// <summary>
		/// Semi-private attribute.
		/// </summary>
		public bool hasQuit = false;
		
		///// <summary>
		///// Objects in the whole game.
		///// </summary>
		//public Saves.Objects SpaceObjects = null;	
		
		/// <summary>
		/// Helper view matrix
		/// </summary>
		public Matrix pomViewMatrix = new Matrix();

		/// <summary>
		/// Helper projection matrix
		/// </summary>
		public Matrix pomProjectionMatrix = new Matrix();		
		
		/// <summary>
		/// Current solar system
		/// </summary>
		public string CurrentSystem = "Sol";
		
		/// <summary>
		/// Current year
		/// </summary>
		public int CurrentYear = 3100;
		
		/// <summary>
		/// Current time unit
		/// </summary>
		public int CurrentCycle = 1;
		
		/// <summary>
		/// Current tooltip
		/// </summary>
		public string CurrentTooltip = "";
		
		/// <summary>
		/// Current game screen
		/// </summary>
		public GameScreen CurrentScreen = GameScreen.None;
		
		/// <summary>
		/// Current location
		/// </summary>
		public string CurrentLocation = "Earth City";
		
		private StationBase currentStation = null;
		
		/// <summary>
		/// Current station
		/// </summary>
		public StationBase CurrentStation
		{
			get { return currentStation; }
			set { currentStation = value; }
		}
		
		private StationModuleBase currentModule = null;
		/// <summary>
		/// Current station module
		/// </summary>
		public StationModuleBase CurrentModule
		{
			get { return currentModule; }
			set { currentModule = value; }
		}
		
		/// <summary>
		/// Advance time. This disables mouse and moves the clock.
		/// </summary>
		public bool AdvanceTime = false;
		
		/// <summary>
		/// Advance time only as long as the mouse is down
		/// </summary>
		public bool TimeAdvanceOnHold = false;
		
		/// <summary>
		/// Gets the tick the game last advanced time.
		/// </summary>
		private int LastAdvanceTick = -1;
		
		/// <summary>
		/// Earth City station
		/// </summary>
		public StationBase EarthCity = null;
		
		/// <summary>
		/// All stations in the game
		/// </summary>
		public List<StationBase> StationList = null;
		
		/// <summary>
		/// All StoreItem templates in the mod.
		/// </summary>
		public Dictionary<string, StoreItem> ItemTemplates = null;
		
		/// <summary>
		/// Global gui menu list (doesn't change normally)
		/// </summary>
		public Dictionary<string, GuiMenu> GuiMenuGlobal = null;
		
		/// <summary>
		/// List of gui menus to be cleared after every station change
		/// </summary>
		public Dictionary<string, GuiMenu> GuiMenuStation = null;
		
		/// <summary>
		/// List of gui menus to show for the current station module
		/// </summary>
		public Dictionary<string, GuiMenu> GuiMenuModule = null;
		
		/// <summary>
		/// Selected game mod
		/// </summary>
		public string Mod = "default";
		
		/// <summary>
		/// List of all fonts
		/// </summary>
		public Dictionary<string, GuiFont> GuiFonts = null;
		
		/// <summary>
		/// List of all buttons - to be cloned into GuiMenu
		/// </summary>
		public Dictionary<string, GuiButtonInfo> GuiButtonTemplates = null;

        /// <summary>
        /// List of all galaxies in the universe
        /// </summary>
        public Dictionary<string, Galaxy> Universe = null;

        public int RandomSeed = new Random().Next();

        /// <summary>
        /// Saves the universe config file
        /// </summary>
        /// <param name="filename"></param>
        public void SaveUniverse(string filename)
        {
            XmlDocument doc = new XmlDocument();

            doc.AppendChild(doc.CreateXmlDeclaration("1.0", "utf-8", string.Empty));

            XmlElement el = doc.CreateElement("FDRFile");
            XmlAttribute atr;

            atr = doc.CreateAttribute("version");
            atr.Value = "0.10";
            el.Attributes.Append(atr);

            atr = doc.CreateAttribute("type");
            atr.Value = "universe";
            el.Attributes.Append(atr);

            doc.AppendChild(el);

            XmlElement el2, el3;

            el2 = doc.CreateElement("Galaxies");
            el.AppendChild(el2);

            foreach (KeyValuePair<string, Galaxy> gal in Universe)
            {
                el3 = doc.CreateElement(gal.Key);
                el2.AppendChild(el3);

                gal.Value.ToXml(el3);
            }

            doc.Save(filename + ".fdru");
        }

        /// <summary>
        /// Loads the universe config file
        /// </summary>
        /// <param name="filename"></param>
        /// <returns>True if success</returns>
        public bool LoadUniverse(string filename)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(filename + ".fdru");

            XmlElement el = null;

            for (int i = 0; i < doc.ChildNodes.Count; i++)
            {
                if (doc.ChildNodes[i].Name.ToUpperInvariant() == "FDRFILE")
                {
                    el = (XmlElement)doc.ChildNodes[i];

                    break;
                }
            }

            if (el != null)
            {
                string version = el.Attributes["version"].Value;

#if GenLog
                Console.WriteLine("\t\tUniverse config file version: " + version);
#endif

                if (el.Attributes["type"].Value.ToLowerInvariant() == "universe")
                {
                    // Loading GuiButtonTemplates
                    XmlNode node = el["Galaxies"];
                    Universe.Clear();

                    foreach (XmlNode xGal in node.ChildNodes)
                    {
                        Galaxy gal = new Galaxy();
                        gal.FromXml(xGal);

                        Universe.Add(xGal.Name, gal);
                    }

                    return true;
                }
                else
                {
                    Console.WriteLine("\t\tError loading mod. Universe config file " + filename + ".fdru is not in the correct format.");

                    return false;
                }
            }
            else
            {
                Console.WriteLine("\t\tError loading mod. Universe config file " + filename + ".fdru is not in the correct format.");

                return false;
            }
        }

		/// <summary>
		/// Returns the current time after dot
		/// </summary>
		public int CurrentDotTime
		{
			get { return ((Environment.TickCount / 100) % 100); }
		}
		
		/// <summary>
		/// Current mod info
		/// </summary>
		public ModInfo CurrentModInfo = new ModInfo();
		
		/// <summary>
		/// Invokes object <paramref name="inv"/> member with the specified format.
		/// </summary>
		/// <param name="inv">Invocation target</param>
		/// <param name="fldnam">Field name</param>
		/// <param name="format">Output format</param>
		/// <returns></returns>
		public string InvokeFormat(object inv, string fldnam, string format)
		{
			Type typ = inv.GetType();
			object obj = null;
			string val = "";

			MemberInfo[] mi = typ.GetMember(fldnam);
		
			if ( mi.Length > 0 )
			{
                //GuiAccessibleAttribute attr = GuiAccessibleAttribute.GetCustomAttribute(mi[0], typeof(GuiAccessibleAttribute)) as GuiAccessibleAttribute;

                //if (attr != null)
                //{
                //    if (attr.IsAccessible == false) return "Member not accessible";
                //}

				switch ( mi[0].MemberType )
				{
					case MemberTypes.Field:
						{
							FieldInfo fi = typ.GetField(fldnam);
							
							if ( fi != null )
							{
								obj = fi.GetValue(inv);
							}
							
							break;
						}
					case MemberTypes.Property:
						{
							PropertyInfo pi = typ.GetProperty(fldnam);
							
							if ( pi != null )
							{
								obj = pi.GetValue(inv, null);
							}
							
							break;
						}
				}
			}
			
			if ( obj != null )
			{
				if ( format == "" )
				{
					val = obj.ToString();
				}
				else
				{
					MethodInfo met = obj.GetType().GetMethod("ToString", new Type[1] { typeof(string) });
					
					if ( met != null )
					{
						val = met.Invoke(obj, new object[1] { format } ).ToString();
					}
					else
					{
						val = obj.ToString();
					}
				}
			}
			
			return val;
		}
		
		/// <summary>
		/// Formats the specified text
		/// </summary>
		/// <param name="input">Input text with parameters</param>
		/// <returns>Formatted text</returns>
		public string FormatText(string input)
		{
			//this.GetType().GetMember();
			string pom = input;
			
			Type typ = GetType();
			
			while ( pom.Contains("$#[") )
			{
				int idx = pom.IndexOf("$#[");
				idx += 2;
				int idxEnd = pom.IndexOf("]", idx);
				int idxFormat = pom.IndexOf(":", idx, idxEnd - idx);
				string fldnam = "";
				string format = "";
				
				if ( idxFormat != -1 )
				{
					fldnam = pom.Substring(idx + 1, idxFormat - idx - 1);
					format = pom.Substring(idxFormat + 1, idxEnd - idxFormat - 1);
				}
				else
				{
					fldnam = pom.Substring(idx + 1, idxEnd - idx - 1);
				}
				
				string val = InvokeFormat(this, fldnam, format);
				
				pom = pom.Remove(idx - 2, idxEnd - idx + 3);
				
				pom = pom.Insert(idx - 2, val);
			}
			
			return pom;
		}
		
		/// <summary>
		/// Saves all menus to menu config in mod directory
		/// </summary>
		public void SaveMenus()
		{
			XmlDocument doc = new XmlDocument();
			
			doc.AppendChild( doc.CreateXmlDeclaration("1.0", "utf-8", string.Empty) );
			
			XmlElement el = doc.CreateElement("FDRFile");
			XmlAttribute atr;
			
			atr = doc.CreateAttribute("version");
			atr.Value = "0.10";
			el.Attributes.Append(atr);
			
			atr = doc.CreateAttribute("type");
			atr.Value = "menu";
			el.Attributes.Append(atr);
			
			doc.AppendChild(el);
			
			XmlElement el2, el3;
			
			el2 = doc.CreateElement("GuiMenuGlobal");
			el.AppendChild(el2);
			
			foreach( KeyValuePair<string, GuiMenu> mnu in GuiMenuGlobal )
			{
				el3 = doc.CreateElement(mnu.Value.Id);
				mnu.Value.ToXml(el3);
				el2.AppendChild(el3);
				
				atr = doc.CreateAttribute("Type");
                atr.Value = mnu.Value.GetType().Name;
				el3.Attributes.Append(atr);
			}
			
			el2 = doc.CreateElement("GuiMenuStation");
			el.AppendChild(el2);
			
			foreach( KeyValuePair<string, GuiMenu> mnu in GuiMenuStation )
			{
				el3 = doc.CreateElement(mnu.Value.Id);
				mnu.Value.ToXml(el3);
				el2.AppendChild(el3);
				
				atr = doc.CreateAttribute("Type");
				atr.Value = mnu.Value.GetType().Name;
				el3.Attributes.Append(atr);
			}
			
			el2 = doc.CreateElement("GuiButtonTemplates");
			el.AppendChild(el2);
			
			foreach ( KeyValuePair<string, GuiButtonInfo> btn in GuiButtonTemplates )
			{
				el3 = doc.CreateElement(btn.Key);
				atr = doc.CreateAttribute("Type");
				atr.Value = btn.Value.GetType().Name;
				el3.Attributes.Append(atr);
				
				btn.Value.ToXml(el3);
				el2.AppendChild(el3);
			}
			
			doc.Save("ModGui.fdrm");
		}
		
		/// <summary>
		/// Loads menus from menu config file
		/// </summary>
		public bool LoadMenus(string filename)
		{
			XmlDocument doc = new XmlDocument();
			doc.Load(filename + ".fdrm");
			
			XmlElement el = null;
			
			for ( int i = 0; i < doc.ChildNodes.Count; i ++ )
			{
				if ( doc.ChildNodes[i].Name.ToUpperInvariant() == "FDRFILE" )
				{
					el = (XmlElement)doc.ChildNodes[i];
					
					break;
				}
			}
			
			if ( el != null )
			{
				string version = el.Attributes["version"].Value;
				
				#if GenLog
				Console.WriteLine("\t\tMenu config file version: " + version);
				#endif
				
				if ( el.Attributes["type"].Value.ToLowerInvariant() == "menu" )
				{
					// Loading GuiButtonTemplates
					XmlNode node = el.SelectSingleNode("GuiButtonTemplates");
					GuiButtonTemplates.Clear();
					
					foreach ( XmlNode xBtn in node.ChildNodes )
					{
						GuiButtonInfo btn = GuiButtonInfo.InvokeCreate(xBtn.Attributes["Type"].Value);
						btn.FromXml(xBtn);
						btn.Template = xBtn.Name;
						
						GuiButtonTemplates.Add(btn.Template, btn);
					}
					
					// Loading menus
					node = el.SelectSingleNode("GuiMenuGlobal");
					GuiMenuGlobal.Clear();
					foreach ( XmlNode xMnu in node.ChildNodes )
					{
						GuiMenu mnu;
						if ( xMnu.Attributes["Type"] != null )
							mnu = GuiMenu.InvokeCreate(xMnu.Attributes["Type"].Value);
						else mnu = new GuiMenu();
						
						mnu.FromXml(xMnu);
						GuiMenuGlobal.Add(mnu.Id, mnu);
					}
					
					node = el.SelectSingleNode("GuiMenuStation");
					GuiMenuStation.Clear();
					foreach ( XmlNode xMnu in node.ChildNodes )
					{
						GuiMenu mnu;
						if ( xMnu.Attributes["Type"] != null )
							mnu = GuiMenu.InvokeCreate(xMnu.Attributes["Type"].Value);
						else mnu = new GuiMenu();
						
						mnu.FromXml(xMnu, false);
						
						GuiMenuStation.Add(mnu.Id, mnu);
					}
					
					return true;
				}
				else
				{
					Console.WriteLine("\t\tError loading mod. Menu config file " + filename + ".fdrm is not in the correct format.");
					
					return false;
				}
			}
			else
			{
				Console.WriteLine("\t\tError loading mod. Menu config file " + filename + ".fdrm is not in the correct format.");
				
				return false;
			}
		}
		
		/// <summary>
		/// Saves fonts
		/// </summary>
		public void SaveFonts(string filename)
		{
			XmlDocument doc = new XmlDocument();
			
			doc.AppendChild( doc.CreateXmlDeclaration("1.0", "utf-8", string.Empty) );
			
			XmlElement el = doc.CreateElement("FDRFile");
			XmlAttribute atr;
			
			atr = doc.CreateAttribute("version");
			atr.Value = "0.07";
			el.Attributes.Append(atr);
			
			atr = doc.CreateAttribute("type");
			atr.Value = "font";
			el.Attributes.Append(atr);
			
			doc.AppendChild(el);
			
			XmlElement el2, el3;
			
			el2 = doc.CreateElement("FontList");
			el.AppendChild(el2);
			
			foreach (KeyValuePair<string, GuiFont> fnt in GuiFonts)
			{
				el3 = doc.CreateElement("Font");
				el2.AppendChild(el3);
				
				fnt.Value.ToXml(el3);
			}
			
			doc.Save(filename + ".fdrf");
		}
		
		/// <summary>
		/// Loads fonts
		/// </summary>
		public bool LoadFonts(string filename)
		{
			XmlDocument doc = new XmlDocument();
			doc.Load(filename + ".fdrf");
			
			XmlElement el = null;
			
			for ( int i = 0; i < doc.ChildNodes.Count; i ++ )
			{
				if ( doc.ChildNodes[i].Name.ToUpperInvariant() == "FDRFILE" )
				{
					el = (XmlElement)doc.ChildNodes[i];
					
					break;
				}
			}
			
			if ( el != null )
			{
				string version = el.Attributes["version"].Value;
				
				#if GenLog
				Console.WriteLine("\t\tFont list file version: " + version);
				#endif
				
				if ( el.Attributes["type"].Value.ToLowerInvariant() == "font" )
				{
					el = el["FontList"];
					
					if ( el == null )
					{
						Console.WriteLine("\t\tError loading mod. Font list file " + filename + ".fdrf is not in the correct format.");
						
						return false;
					}

					GuiFonts.Clear();
					GuiFont fnt;
					for ( int i = 0; i < el.ChildNodes.Count; i ++ )
					{
						if ( el.ChildNodes[i].Name.ToLowerInvariant() == "font" )
						{
							fnt = new GuiFont(el.ChildNodes[i]);
							
							GuiFonts.Add(fnt.Id, fnt);
						}
					}
					
					return true;
				}
				else
				{
					Console.WriteLine("\t\tError loading mod. Font list file " + filename + ".fdrf is not in the correct format.");
					
					return false;
				}
			}
			else
			{
				Console.WriteLine("\t\tError loading mod. Font list file " + filename + ".fdrf is not in the correct format.");
				
				return false;
			}
		}
		
		/// <summary>
		/// Saves texture list
		/// </summary>
		public void SaveTextures(Dictionary<string, TextureInfo> texList, string filename)
		{
			XmlDocument doc = new XmlDocument();
			
			doc.AppendChild( doc.CreateXmlDeclaration("1.0", "utf-8", string.Empty) );
			
			XmlElement el = doc.CreateElement("FDRFile");
			XmlAttribute atr;
			
			atr = doc.CreateAttribute("version");
			atr.Value = "0.03";
			el.Attributes.Append(atr);
			
			atr = doc.CreateAttribute("type");
			atr.Value = "textures";
			el.Attributes.Append(atr);
			
			doc.AppendChild(el);
			
			XmlElement el2, el3, el4;
			
			el2 = doc.CreateElement("TextureList");
			el.AppendChild(el2);
			
			foreach ( KeyValuePair<string, TextureInfo> ti in texList )
			{
				el3 = doc.CreateElement("Texture");
				el2.AppendChild(el3);
				
				el4 = doc.CreateElement("Id");
				el4.InnerText = ti.Key;
				el3.AppendChild(el4);
				
				el4 = doc.CreateElement("FileName");
				el4.InnerText = ti.Value.filename;
				el3.AppendChild(el4);
			}
			
			doc.Save(filename + ".fdrt");
		}
		
		/// <summary>
		/// Loads textures
		/// </summary>
		public bool LoadTextures(Dictionary<string, TextureInfo> texList, string filename)
		{
			XmlDocument doc = new XmlDocument();
			doc.Load(filename + ".fdrt");
			
			XmlElement el = null;
			
			for ( int i = 0; i < doc.ChildNodes.Count; i ++ )
			{
				if ( doc.ChildNodes[i].Name.ToUpperInvariant() == "FDRFILE" )
				{
					el = (XmlElement)doc.ChildNodes[i];
					
					break;
				}
			}
			
			if ( el != null )
			{
				string version = el.Attributes["version"].Value;
				
				#if GenLog
				Console.WriteLine("\t\tTexture list file version: " + version);
				#endif
				
				if ( el.Attributes["type"].Value.ToLowerInvariant() == "textures" )
				{
					el = el["TextureList"];
					
					if ( el == null )
					{
						Console.WriteLine("\t\tError loading mod. Texture list file " + filename + ".fdrt is not in the correct format.");
						
						return false;
					}
					
					string id = "";
					string fname = "";
					
					texList.Clear();
					for ( int i = 0; i < el.ChildNodes.Count; i ++ )
					{
						if ( el.ChildNodes[i].Name.ToLowerInvariant() == "texture" )
						{
							if ( el.ChildNodes[i]["Id"] != null ) id = el.ChildNodes[i]["Id"].InnerText;
							else id = "";
							if ( el.ChildNodes[i]["FileName"] != null ) fname = el.ChildNodes[i]["FileName"].InnerText;
							else fname = "";
							
							if ( id == "" )
							{
								id = System.IO.Path.GetFileNameWithoutExtension(fname);
							}
							
							if ( id != "" && fname != "" )
							{
								texList.Add(id, new TextureInfo(fname, mainTextures.AddTexture(fname)));
							}
						}
					}
					
					return true;
				}
				else
				{
					Console.WriteLine("\t\tError loading mod. Texture list file " + filename + ".fdrt is not in the correct format.");
					
					return false;
				}
			}
			else
			{
				Console.WriteLine("\t\tError loading mod. Texture list file " + filename + ".fdrt is not in the correct format.");
				
				return false;
			}
		}
		
		/// <summary>
		/// Saves ItemTemplates
		/// </summary>
		/// <param name="filename">Filename</param>
		public void SaveItemTemplates(string filename)
		{
			XmlDocument doc = new XmlDocument();
			
			doc.AppendChild( doc.CreateXmlDeclaration("1.0", "utf-8", string.Empty) );
			
			XmlElement el = doc.CreateElement("FDRFile");
			XmlAttribute atr;
			
			atr = doc.CreateAttribute("version");
			atr.Value = "0.12";
			el.Attributes.Append(atr);
			
			atr = doc.CreateAttribute("type");
			atr.Value = "prods";
			el.Attributes.Append(atr);
			
			doc.AppendChild(el);
			
			XmlElement el2, el3;
			
			el2 = doc.CreateElement("ItemTemplates");
			el.AppendChild(el2);
			
			foreach ( KeyValuePair<string, StoreItem> si in ItemTemplates )
			{
				el3 = doc.CreateElement(si.Key);
				el2.AppendChild(el3);
				
				si.Value.ToXml(el3);
			}
			
			doc.Save(filename + ".fdrp");
		}
		
		/// <summary>
		/// Loads ItemTemplates
		/// </summary>
		/// <param name="filename">Filename</param>
		/// <returns>True if success</returns>
		public bool LoadItemTemplates(string filename)
		{
			XmlDocument doc = new XmlDocument();
			doc.Load(filename + ".fdrp");
			
			XmlElement el = null;
			
			for ( int i = 0; i < doc.ChildNodes.Count; i ++ )
			{
				if ( doc.ChildNodes[i].Name.ToUpperInvariant() == "FDRFILE" )
				{
					el = (XmlElement)doc.ChildNodes[i];
					
					break;
				}
			}
			
			if ( el != null )
			{
				string version = el.Attributes["version"].Value;
				
				#if GenLog
				Console.WriteLine("\t\tItem template file version: " + version);
				#endif
				
				if ( el.Attributes["type"].Value.ToLowerInvariant() == "prods" )
				{
					el = el["ItemTemplates"];
					
					if ( el == null )
					{
						Console.WriteLine("\t\tError loading mod. Item template file " + filename + ".fdrp is not in the correct format.");
						
						return false;
					}
					
					StoreItem si;
					ItemTemplates.Clear();
					for ( int i = 0; i < el.ChildNodes.Count; i ++ )
					{
						si = StoreItem.InvokeCreate(el.ChildNodes[i].Attributes["Type"].Value);
						si.FromXml(el.ChildNodes[i]);
						
						ItemTemplates.Add(el.ChildNodes[i].Name, si);
					}
					
					return true;
				}
				else
				{
					Console.WriteLine("\t\tError loading mod. Item template file " + filename + ".fdrp is not in the correct format.");
					
					return false;
				}
			}
			else
			{
				Console.WriteLine("\t\tError loading mod. Item template file " + filename + ".fdrp is not in the correct format.");
				
				return false;
			}
		}
		
		/// <summary>
		/// Loads the currently selected mod
		/// </summary>
		public void LoadMod()
		{
			#if GenLog
			Console.WriteLine("\tLoading mod info...");
			#endif
			
			CurrentModInfo.LoadFromXml("GameInfo");
			
			#if GenLog
			Console.WriteLine("\tMod info loaded.");
			#endif
			
			#if GenLog
			Console.WriteLine("\tLoading textures...");
			#endif
			
			if ( GuiTextures == null )
			{
				GuiTextures = new Dictionary<string, TextureInfo>();
			}
			else
			{
				GuiTextures.Clear();
			}
			
			if ( mainTextures == null )
			{
				mainTextures = new TextureManager();
			}
			else
			{
				mainTextures.Clear();
			}

			LoadTextures(GuiTextures, "GuiTextures");

			#if GenLog
			Console.WriteLine("\tTextures loaded.");
			#endif
			
			if ( GuiFonts == null )
			{
				GuiFonts = new Dictionary<string, GuiFont>();
			}
			else
			{
				GuiFonts.Clear();
			}
			
			#if GenLog
			Console.WriteLine("\tLoading fonts...");
			#endif
			
			LoadFonts("Fonts");
			
			#if GenLog
			Console.WriteLine("\tFonts loaded.");
			#endif
			
			#if GenLog
			Console.WriteLine("\tLoading items...");
			#endif
			
			if ( ItemTemplates == null )
			{
				ItemTemplates = new Dictionary<string, StoreItem>();
			}
			else
			{
				ItemTemplates.Clear();
			}
			
			LoadItemTemplates("Items");
			
			#if GenLog
			Console.WriteLine("\tItems loaded.");
			#endif
			
			#if GenLog
			Console.WriteLine("\tLoading Gui...");
			#endif
			
			if ( GuiMenuGlobal == null )
			{
				GuiMenuGlobal = new Dictionary<string, GuiMenu>();
			}
			else
			{
				GuiMenuGlobal.Clear();
			}
			
			if ( GuiMenuStation == null )
			{
				GuiMenuStation = new Dictionary<string, GuiMenu>();
			}
			else
			{
				GuiMenuStation.Clear();
			}
			
			if ( GuiMenuModule == null )
			{
				GuiMenuModule = new Dictionary<string, GuiMenu>();
			}
			else
			{
				GuiMenuModule.Clear();
			}
			
			if ( GuiButtonTemplates == null )
			{
				GuiButtonTemplates = new Dictionary<string, GuiButtonInfo>();
			}
			else
			{
				GuiButtonTemplates.Clear();
			}
			
			LoadMenus("ModGui");
			
			#if GenLog
			Console.WriteLine("\tGui loaded.");
			#endif

            #if GenLog
            Console.WriteLine("\tLoading universe...");
            #endif

            if (Universe == null)
            {
                Universe = new Dictionary<string, Galaxy>();
            }
            else
            {
                Universe.Clear();
            }

            LoadUniverse("Universe");

            #if GenLog
            Console.WriteLine("\tUniverse loaded.");
            #endif
		}
		
		/// <summary>
		/// Sets the the module defined by CurrentStation and NextStationModule as currently selected
		/// </summary>
		/// <param name="NextStationModule"></param>
		public void SetCurrentModule(string NextStationModule)
		{
			GuiMenuModule.Clear();
			
			currentModule = null;
			if ( currentStation != null )
			{
				for ( int i = 0; i < currentStation.Modules.Count; i ++ )
				{
					if ( currentStation.Modules[i].GetType().Name == NextStationModule )
					{
						currentModule = currentStation.Modules[i];
						
						break;
					}
				}
			}
			
			if ( currentModule != null )
			{
				currentModule.OnInitMenus();
			}
		}
		
		/// <summary>
		/// Sets current station
		/// </summary>
		/// <param name="stat">Station to set as current</param>
		public void SetCurrentStation(StationBase stat)
		{
			// clear all buttons
			foreach ( GuiMenu mnu in GuiMenuStation.Values )
			{
				mnu.Buttons.Clear();
			}
			
			#if DebugStations
			if ( stat == null ) Console.WriteLine("Station null");
			else Console.WriteLine("Station " + stat.Title);
			#endif
			
			if ( stat == null ) 
			{
				CurrentStation = null;
				
				return;
			}
			
			// reset buttons
			StationModuleBase mod;
			for ( int i = 0; i < stat.Modules.Count; i ++ )
			{
				mod = stat.Modules[i];
				if ( mod != null && mod.ButtonInfo != null && GuiMenuStation.ContainsKey(mod.ButtonInfo.GuiMenuId) )
				{
					GuiMenuStation[mod.ButtonInfo.GuiMenuId].Buttons.Add(mod.GetType().Name, mod.ButtonInfo);
				}
			}
			
			CurrentLocation = stat.Title;
			CurrentStation = stat;
		}
		
		/// <summary>
		/// Starts TimeAdvance feature
		/// </summary>
		public void StartTimeAdvance()
		{
			//frmMain.ShowCursor(false);
			LastAdvanceTick = Environment.TickCount - 400;
			AdvanceTime = true;
		}
		
		/// <summary>
		/// Advances a turn
		/// </summary>
		public void NextTurn()
		{
			if ( Environment.TickCount - LastAdvanceTick > 400 )
			{
				CurrentCycle++;
				if ( CurrentCycle == 1000 )
				{
					CurrentCycle = 0;
					CurrentYear++;
				}
				
				for ( int i = 0; i < StationList.Count; i ++ )
				{
					if ( StationList[i] != null ) StationList[i].OnNextTurn();
				}
				
				LastAdvanceTick = Environment.TickCount;
			}
		}
		
		/// <summary>
		/// Stops the TimeAdvance feature
		/// </summary>
		public void StopTimeAdvance()
		{
			AdvanceTime = false;
			TimeAdvanceOnHold = false;
			//frmMain.ShowCursor(true);
		}
		
		/// <summary>
		/// Loads game
		/// </summary>
		/// <param name="gameName">Savegame file</param>
		public bool LoadGame(string gameName)
		{
			if ( StationList == null )
			{
				StationList = new List<StationBase>();
			}
			else StationList.Clear();
			
			XmlDocument doc = new XmlDocument();
			doc.Load(".\\save\\" + gameName + ".fdrs");
			
			XmlElement el = null;
			
			for ( int i = 0; i < doc.ChildNodes.Count; i ++ )
			{
				if ( doc.ChildNodes[i].Name.ToUpperInvariant() == "FDRFILE" )
				{
					el = (XmlElement)doc.ChildNodes[i];
					
					break;
				}
			}
			
			if ( el != null )
			{
				string version = el.Attributes["version"].Value;
				
				#if GenLog
				Console.WriteLine("\t\tSavegame file version: " + version);
				#endif
				
				if ( el.Attributes["type"].Value.ToLowerInvariant() == "save" )
				{
					XmlElement inf = el["SaveInfo"];
					
					Console.WriteLine("\t\t\t" + inf["Title"].InnerText + " (" + inf["Timestamp"].InnerText + ")");
					Console.WriteLine("\t\t\t" + inf["EngineVersion"].InnerText + ", mod " + inf["ModVersion"].InnerText);
                    if (inf["CurrentYear"] != null)
                    {
                        CurrentYear = int.Parse(inf["CurrentYear"].InnerText);
                    }
                    if (inf["CurrentCycle"] != null)
                    {
                        CurrentCycle = int.Parse(inf["CurrentCycle"].InnerText);
                    }
					
					StationList.Clear();
					foreach ( XmlNode xStat in el["SaveData"].ChildNodes )
					{
						StationBase sb = StationBase.InvokeCreate(xStat.Attributes["Type"].Value);
						sb.FromXml(xStat);
						
						StationList.Add(sb);
					}
					
					EarthCity = StationList[0];
					CurrentScreen = GameScreen.EarthCity;
					SetCurrentStation(EarthCity);
					
					return true;
				}
				else
				{
					Console.WriteLine("\t\tError loading mod. Savegame file " + gameName + ".fdrs is not in the correct format.");
					
					return false;
				}
			}
			else
			{
				Console.WriteLine("\t\tError loading mod. Savegame file " + gameName + ".fdrs is not in the correct format.");
				
				return false;
			}
		}
		
		/// <summary>
		/// Saves game
		/// </summary>
		/// <param name="gameName">Savegame file</param>
		/// <param name="title">Savegame title</param>
		public void SaveGame(string gameName, string title)
		{
			XmlDocument doc = new XmlDocument();
			doc.AppendChild(doc.CreateXmlDeclaration("1.0", "utf-8", string.Empty));
			XmlElement el = doc.CreateElement("FDRFile");
			XmlAttribute atr;
			
			atr = doc.CreateAttribute("version");
			atr.Value = "0.12";
			el.Attributes.Append(atr);
			
			atr = doc.CreateAttribute("type");
			atr.Value = "save";
			el.Attributes.Append(atr);					
			
			doc.AppendChild(el);
			
			XmlElement el2 = doc.CreateElement("SaveInfo");
			el.AppendChild(el2);
			
			XmlElement el3 = doc.CreateElement("Title");
			el3.InnerText = title;
			el2.AppendChild(el3);
			
			el3 = doc.CreateElement("Timestamp");
			el3.InnerText = DateTime.Now.ToString();
			el2.AppendChild(el3);
			
			Version ver = System.Reflection.Assembly.GetCallingAssembly().GetName().Version;
			
			el3 = doc.CreateElement("EngineVersion");
			el3.InnerText = ver.Major.ToString() + "." + ver.Minor.ToString("d2") + "." + ver.Build.ToString();
			el2.AppendChild(el3);
			
			el3 = doc.CreateElement("ModVersion");
			el3.InnerText = CurrentModInfo.modVersion;
			el2.AppendChild(el3);

            el3 = doc.CreateElement("CurrentYear");
            el3.InnerText = CurrentYear.ToString();
            el2.AppendChild(el3);

            el3 = doc.CreateElement("CurrentCycle");
            el3.InnerText = CurrentCycle.ToString();
            el2.AppendChild(el3);
			
			el2 = doc.CreateElement("SaveData");
			el.AppendChild(el2);
			
			for ( int i = 0; i < StationList.Count; i ++ )
			{
				if ( StationList[i] != null )
				{
					el3 = doc.CreateElement("Station");
					el2.AppendChild(el3);
					StationList[i].ToXml(el3);
				}
			}
			
			el2 = doc.CreateElement("ResearchedItems");
			el.AppendChild(el2);
			
			foreach ( KeyValuePair<string, StoreItem> si in ItemTemplates )
			{
				if ( si.Value.Researched )
				{
					el3 = doc.CreateElement(si.Key);
					el2.AppendChild(el3);
				}
			}
			
			el2 = doc.CreateElement("ResearchItems");
			el.AppendChild(el2);
			
			foreach ( KeyValuePair<string, StoreItem> si in ItemTemplates )
			{
				if ( si.Value.CanResearch && !si.Value.Researched )
				{
					el3 = doc.CreateElement(si.Key);
					el3.InnerText = si.Value.ResearchProgress.ToString();
					el2.AppendChild(el3);
				}
			}
			
			doc.Save(".\\save\\" + gameName + ".fdrs");
			
			//SaveItemTemplates(".\\save\\" + gameName);
		}
		
		/// <summary>
		/// Dispose all resources and deinitialize all subsequent engines.
		/// </summary>
		public void Deinitialize()
		{
			InputEngine.Deinitialize();

//			for ( int i = 0; i < SpaceObjects.Count; i ++ ) 
//			{
//				if ( SpaceObjects[i] is MeshActor )
//				{
//					try
//					{
//						(SpaceObjects[i] as MeshActor).Destroy();
//					}
//					catch {}
//				}
//
//				SpaceObjects.RemoveActor(i);
//			}
			
			mainTextures.Clear();		
		}

		/// <summary>
		/// Called by parent form when a mouse up event occured
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if ( TimeAdvanceOnHold && AdvanceTime )
			{
				StopTimeAdvance();
			}
			
			isMouseDown = false;
		}

		/// <summary>
		/// Does action if menu was clicked
		/// </summary>
		/// <param name="mnu">Menu to check</param>
		private void CheckMenuClick(GuiMenu mnu)
		{
			if ( mnu != null && new Rectangle(mnu.Location, mnu.TextureArea.Size).Contains(mouseCoords) )
			{
				foreach ( KeyValuePair<string, GuiButtonInfo> btn in mnu.Buttons )
				{
					Rectangle rect = btn.Value.ButtonRect;
					rect.Offset(mnu.Location);
					
					if ( rect.Contains(mouseCoords) )
					{
						// Use params
						foreach ( KeyValuePair<string, GuiButtonParam> par in btn.Value.Params )
						{
							par.Value.DoAction();
						}
					}
				}
			}
		}
		
		/// <summary>
		/// Checks whether any button of the specified menu has mouse cursor on itself
		/// </summary>
		/// <param name="mnu">Menu to check</param>
		/// <returns>Null if no hover. Button with mouse if hover.</returns>
		private GuiButtonInfo CheckMenuHover(GuiMenu mnu)
		{
			if ( mnu != null && new Rectangle(mnu.Location, mnu.TextureArea.Size).Contains(mouseCoords) )
			{
				foreach ( KeyValuePair<string, GuiButtonInfo> btn in mnu.Buttons )
				{
					Rectangle rect = btn.Value.ButtonRect;
					rect.Offset(mnu.Location);
						
					if ( rect.Contains(mouseCoords) )
					{
						return btn.Value;
					}
				}
			}
			
			return null;
		}
		
		/// <summary>
		/// Called by parent form when a mouse down event occured
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if ( AdvanceTime ) 
			{
				StopTimeAdvance();
				
				return;
			}
			
			mouseButton = e.Button;
			isMouseDown = true;

			foreach ( KeyValuePair<string, GuiMenu> mnu in GuiMenuGlobal )
			{
				CheckMenuClick(mnu.Value);
			}
			foreach ( KeyValuePair<string, GuiMenu> mnu in GuiMenuStation )
			{
				CheckMenuClick(mnu.Value);
			}
			foreach ( KeyValuePair<string, GuiMenu> mnu in GuiMenuModule )
			{
				CheckMenuClick(mnu.Value);
			}
			
//			if ( new Rectangle(256, 10 + renderEngine.Device.Viewport.Height - 42, 224, 32).Contains(mouseCoords) )
//			{
//				TimeAdvanceOnHold = true;
//				StartTimeAdvance();
//			}
		}

		#region Old code
		
//		public GuiButtonInfo[] leftGuiTop = { 
//				new GuiButtonInfo("Advance Time", GuiButtonAction.AdvanceTime, GameScreen.NoChange, "", true),
//				new GuiButtonInfo("Master Control", GuiButtonAction.ChangeScreen, GameScreen.MasterControl, "", true), 
//				new GuiButtonInfo("Stocktaker", GuiButtonAction.ChangeScreen, GameScreen.Stocktaker, "", true),
//				new GuiButtonInfo("Deposit Analysis", GuiButtonAction.ChangeScreen, GameScreen.DepositAnalysis, "", true),
//				new GuiButtonInfo("Disk Access", GuiButtonAction.ChangeScreen, GameScreen.SaveLoad, "", true),
//				new GuiButtonInfo("News Bulletins", GuiButtonAction.ChangeScreen, GameScreen.NewsBulletins, "", true),
//				new GuiButtonInfo("Earth City", GuiButtonAction.ChangeScreen, GameScreen.EarthCity, "leftGui.EarthCity", true, 7, 250),
//				new GuiButtonInfo("Star Selector", GuiButtonAction.ChangeScreen, GameScreen.StarSelector, "", true)
//			};
//		
//		public GuiButtonInfo[] leftGuiBottom = {
//				new GuiButtonInfo("Production", GuiButtonAction.ChangeScreen, GameScreen.Production, "leftGui.Production", true),
//				new GuiButtonInfo("Stores", GuiButtonAction.ChangeScreen, GameScreen.Stores, "", false),
//				new GuiButtonInfo("Space Bay", GuiButtonAction.ChangeScreen, GameScreen.SpaceBay, "", false),
//				new GuiButtonInfo("Spacedock", GuiButtonAction.ChangeScreen, GameScreen.SpaceDock, "", false),
//				new GuiButtonInfo("Shuttle", GuiButtonAction.ChangeScreen, GameScreen.ShipWindow, "leftGui.Shuttle", true),
//				new GuiButtonInfo("Self Destruct", GuiButtonAction.ChangeScreen, GameScreen.SelfDestruct, "", false),
//				new GuiButtonInfo("Training", GuiButtonAction.ChangeScreen, GameScreen.Training, "leftGui.Training", true),
//				new GuiButtonInfo("Research", GuiButtonAction.ChangeScreen, GameScreen.Research, "leftGui.Research", true),
//				new GuiButtonInfo("Shuttle Bay", GuiButtonAction.ChangeScreen, GameScreen.ShuttleBay, "leftGui.ShuttleBay", true),
//				new GuiButtonInfo("Resource", GuiButtonAction.ChangeScreen, GameScreen.Resources, "leftGui.Resources", true),
//				new GuiButtonInfo("Dunno", GuiButtonAction.ChangeScreen, GameScreen.NoChange, "", false),
//				new GuiButtonInfo("Mining Stores", GuiButtonAction.ChangeScreen, GameScreen.MiningStores, "leftGui.MiningStores", true)
//			};
		
//		public GameScreen[] leftGuiTop = {GameScreen.NoChange, GameScreen.MasterControl,
//			GameScreen.Stocktaker, GameScreen.DepositAnalysis, GameScreen.SaveLoad,
//			GameScreen.NewsBulletins, GameScreen.EarthCity, GameScreen.StarSelector};
//		public GameScreen[] leftGuiBottom = {GameScreen.Production, GameScreen.Stores,
//			GameScreen.SpaceBay, GameScreen.SpaceDock, GameScreen.ShipWindow,
//			GameScreen.SelfDestruct, GameScreen.Training, GameScreen.Research, 
//			GameScreen.ShuttleBay, GameScreen.Resources, GameScreen.NoChange, 
//			GameScreen.MiningStores};
//		public string[] leftGuiTopStr = {"Advance Time", "Master Control", "Stocktaker",
//			"Deposit Analysis", "Disk Access", "News Bulletins", "Earth City", "Star Selector"};
//		public string[] leftGuiBottomStr = {"Production", "Stores", "Space bay", "Spacedock",
//			"Shuttle", "Self destruct", "Training", "Research", "Shuttle bay", "Resource",
//			"Dunno", "Mining stores"};
		
		#endregion;
		
		/// <summary>
		/// Called by parent form when a mouse move event occured
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if ( AdvanceTime ) return;
			
			mouseCoords = new Point(e.X, e.Y);
			
			CurrentTooltip = "";
			
			// tooltips
			GuiButtonInfo btn = null;
			foreach ( KeyValuePair<string, GuiMenu> mnu in GuiMenuGlobal )
			{
				btn = CheckMenuHover(mnu.Value);
				if ( btn != null )
				{
					CurrentTooltip = btn.Tooltip;
					
					break;
				}
			}
			foreach ( KeyValuePair<string, GuiMenu> mnu in GuiMenuStation )
			{
				btn = CheckMenuHover(mnu.Value);
				if ( btn != null )
				{
					CurrentTooltip = btn.Tooltip;
					
					break;
				}
			}
			foreach ( KeyValuePair<string, GuiMenu> mnu in GuiMenuModule )
			{
				btn = CheckMenuHover(mnu.Value);
				if ( btn != null )
				{
					CurrentTooltip = btn.Tooltip;
					
					break;
				}
			}
		}

		/// <summary>
		/// A key down event occured on target
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if ((int)(byte)e.KeyCode == (int)Keys.Escape)
			{
				if ( ShowMenu )
				{
					ShowMenu = false;
				}
				else
				{
					if ( GameRunning ) Menu = MenuType.Options;
					else Menu = MenuType.MainMenu;

					menuData = MenuData.CreateMenuData( Menu );

					ShowMenu = true;
				}
			}
		}		
		
		/// <summary>
		/// Performs world space to screen space transformation.
		/// </summary>
		/// <param name="device">Render device.</param>
		/// <param name="v3">World space position.</param>
		/// <returns>Screen space position.</returns>
		public Vector2 To2DTransform(Device device,Vector3 v3)
		{
			Vector4 v4 = Vector3.Transform(v3, Matrix.Translation(0, 0, 0));
 
			v4 = Vector4.Transform(v4, /*device.Transform.View*/ pomViewMatrix);
 
			v4 = Vector4.Transform(v4, /*device.Transform.Projection*/ pomProjectionMatrix );
 
			Size scrhalf = new Size(device.Viewport.Width / 2, device.Viewport.Height / 2);

			if ( v4.W <= 0 ) return new Vector2(-1, -1);

			return new Vector2((v4.X / v4.W + 1) * scrhalf.Width, (1 - v4.Y / v4.W) * scrhalf.Height);
		}		
		
		/// <summary>
		/// Initialize game engine - creates all subsequent engines.
		/// </summary>
		/// <param name="target">Visual device target</param>
		public void Initialize(System.Windows.Forms.Control target)
		{
			#if GenLog
			Console.WriteLine("Initializing GameEngine...");
			#endif
			
			try
			{
				System.IO.Directory.SetCurrentDirectory( System.IO.Path.Combine(System.Windows.Forms.Application.StartupPath, "mods\\" + Mod) );
			}
			catch 
			{
				Mod = "default";
				System.IO.Directory.SetCurrentDirectory( System.IO.Path.Combine(System.Windows.Forms.Application.StartupPath, "mods\\" + Mod) );
			}
			
			this.target = target;

			this.target.MouseDown += new System.Windows.Forms.MouseEventHandler(MouseDown);
			this.target.MouseUp += new System.Windows.Forms.MouseEventHandler(MouseUp);
			this.target.MouseMove += new System.Windows.Forms.MouseEventHandler(MouseMove);

			this.target.KeyDown += new System.Windows.Forms.KeyEventHandler(KeyDown);

			renderEngine = new RenderEngine(target);
			renderEngine.Device.ShowCursor(false);
		
			inputEngine = new InputEngine(target);

			Overlay = new Sprite(renderEngine.Device);

			//SpaceObjects = new FallTime.Blackbird.Saves.Objects();
			
			// GUI
			#if GenLog
			Console.WriteLine("Loading mod " + Mod + "...");
			#endif

			LoadMod();
			
			target.Text = CurrentModInfo.title;
			if ( CurrentModInfo.showModVersion )
			{
				target.Text += ", " + CurrentModInfo.modVersion;
			}
			if ( CurrentModInfo.showEngineVersion )
			{
				Version ver = System.Reflection.Assembly.GetCallingAssembly().GetName().Version;
				target.Text += " - engine " + ver.Major.ToString() + "." + ver.Minor.ToString("d2") + " (Build: " + ver.Build.ToString() + ")";
			}
			
			renderReady = true;
			
			#if GenLog
			Console.WriteLine("Mod loaded.");
			#endif
			
			// Game init
			#if GenLog
			Console.WriteLine("Loading new game...");
			#endif
			
			LoadGame("_newGame");
			
			#if GenLog
			Console.WriteLine("Done loading new game.");
			#endif
			
			#if GenLog
			Console.WriteLine("GameEngine initialized.");
			#endif
		}
		
		/// <summary>
		/// Quits the game
		/// </summary>
		public void QuitGame()
		{
			#if GenLog
			Console.WriteLine("QuitGame call");
			#endif
			
			if ( OnQuit.Target != null )
			{
				OnQuit.Method.Invoke(OnQuit.Target, new object[2] { null, EventArgs.Empty} );
			}

			hasQuit = true;
		}

		/// <summary>
		/// Draws the current game screen (interface)
		/// </summary>
		public void DrawGameScreen()
		{
			if ( CurrentScreen == GameScreen.StationModule && CurrentModule != null )
			{
				CurrentModule.OnRenderInterface(Overlay);
			}
		}

		/// <summary>
		/// Moves whole game by one frame
		/// </summary>
		public void Frame()
		{
			if ( /*target.Focused &&*/ renderReady )
			{
//				for ( int i = 0; i < SpaceObjects.Count; i ++ )
//				{
//					if ( SpaceObjects[i] != null ) 
//					{
//						if ( SpaceObjects[i].IsActive )
//						{
//							SpaceObjects[i].Frame();
//						}
//					}
//				}

				if ( AdvanceTime )
				{
					NextTurn();
				}
				
				Render();
			}
		}
		
		private void DrawGuiMenu(GuiMenu mnu)
		{
			TextureManagerItem it = null;
			
			if ( mnu != null )
			{
				if ( GuiTextures.ContainsKey(mnu.Picture) )
				{
					it = mainTextures.GetTexture(GuiTextures[mnu.Picture].textureIdx);
					if ( it != null && it.Texture != null )
					{
						Overlay.Transform = Matrix.Identity;
						
						Overlay.Draw(it.Texture, mnu.TextureArea, new Vector3(0, 0, 0), new Vector3(mnu.Location.X, mnu.Location.Y, 0), Color.White.ToArgb());
					}
				}
				
				foreach ( KeyValuePair<string, GuiButtonInfo> btn in mnu.Buttons )
				{
					if ( btn.Value != null && btn.Value.Enabled )
					{
						if ( GuiTextures.ContainsKey(btn.Value.Picture) )
						{
							bool checkPass = true;
							int animFrame = 0;
							
							foreach ( KeyValuePair<string, GuiButtonParam> par in btn.Value.Params )
							{
								if ( par.Value != null )
								{
									checkPass &= par.Value.DoCheck();
								}
							}
							
							if ( checkPass )
							{
								animFrame = (Environment.TickCount / btn.Value.AnimationTime) % btn.Value.AnimationFrames;
							}
							
							it = mainTextures.GetTexture(GuiTextures[btn.Value.Picture].textureIdx);
							if ( it != null && it.Texture != null )
							{
								Overlay.Transform = Matrix.Identity;
								
								Overlay.Draw(it.Texture, new Rectangle(animFrame * btn.Value.ButtonRect.Width, 0, btn.Value.ButtonRect.Width, btn.Value.ButtonRect.Height), new Vector3(0, 0, 0), new Vector3( mnu.Location.X + btn.Value.ButtonRect.Left, mnu.Location.Y + btn.Value.ButtonRect.Top, 0), btn.Value.PictureColor.ToArgb());
							}
						}
						if ( btn.Value.Text != "" && GuiFonts.ContainsKey(btn.Value.Font) )
						{
							Rectangle rect = btn.Value.ButtonRect;
							rect.Offset(mnu.Location);
							
							GuiFonts[btn.Value.Font].DXFont.DrawText(Overlay, FormatText(btn.Value.Text), rect, btn.Value.TextFormat, btn.Value.ForeColor);
						}
					}
				}
			}
		}
		
		/// <summary>
		/// Render the scene
		/// </summary>
		public void Render()
		{
			renderEngine.BeginScene();

//			for ( int i = 0; i < SpaceObjects.Count; i ++ )
//			{
//				if ( SpaceObjects[i] != null ) 
//				{
//					if ( SpaceObjects[i].IsVisible )
//					{
//						SpaceObjects[i].Render();
//					}
//				}
//			}

			// Overlay

			Overlay.Begin(SpriteFlags.AlphaBlend);
			
			
			foreach ( KeyValuePair<string, GuiMenu> mnu in GuiMenuModule )
			{
				DrawGuiMenu(mnu.Value);
			}
			
			DrawGameScreen();
			
			TextureManagerItem it = null;

			foreach ( KeyValuePair<string, GuiMenu> mnu in GuiMenuGlobal )
			{
				DrawGuiMenu(mnu.Value);
			}
			
			foreach ( KeyValuePair<string, GuiMenu> mnu in GuiMenuStation )
			{
				DrawGuiMenu(mnu.Value);
			}
			
			fntGUI.DrawText(Overlay, CurrentTooltip, 1, 3, Color.White);
			
			it = mainTextures.GetTexture(GuiTextures["bottomLog"].textureIdx);
			if ( it != null )
			{
				Overlay.Transform = Matrix.Translation( 0.0f, 0.0f, 0.0f);

				Overlay.Draw(it.Texture, new Rectangle(0, 0, 800, 66), new Vector3(400, 33, 0), new Vector3( renderEngine.Device.Viewport.Width / 2, renderEngine.Device.Viewport.Height - 42 - 33, 0), Color.White.ToArgb());
			}
			
			it = mainTextures.GetTexture(GuiTextures["systemName"].textureIdx);
			if ( it != null )
			{
				Overlay.Transform = Matrix.Translation( 0.0f, 0.0f, 0.0f);

				Overlay.Draw(it.Texture, new Rectangle(0, 0, 256, 32), new Vector3(128, 16, 0), new Vector3( renderEngine.Device.Viewport.Width - 128, 16 + 6, 0), Color.White.ToArgb());
			}
			
			fntGUI.DrawText(Overlay, CurrentSystem, new Rectangle(renderEngine.Device.Viewport.Width - 256 + 10, 6 + 7, 237, 19), DrawTextFormat.Center | DrawTextFormat.VerticalCenter | DrawTextFormat.SingleLine, Color.Yellow);
			
			// Menu
			if ( ShowMenu )
			{
				it = mainTextures.GetTexture(GuiTextures["menuBackground"].textureIdx);
				if ( it != null )
				{
					Overlay.Transform = Matrix.Translation( 0.0f, 0.0f, 0.0f);

					Overlay.Draw(it.Texture, new Rectangle(0, 0, 512, 512), new Vector3(256, 256, 0), new Vector3( renderEngine.Device.Viewport.Width / 2, 256 + 20, 0), Color.White.ToArgb());
				}

				TextureManagerItem itButton = mainTextures.GetTexture(GuiTextures["buttonMainMenu"].textureIdx);
				TextureManagerItem itButtonActive = mainTextures.GetTexture(GuiTextures["buttonMainMenuActive"].textureIdx);

				menuData.Update();
				for ( int i = 0; i < menuData.buttons.Length; i ++ )
				{
					Rectangle pomRect = menuData.buttons[i].ButtonRect;
					pomRect.Offset( renderEngine.Device.Viewport.Width / 2 - 256, 0 );
					
					if ( pomRect.Contains(MouseCoords) )
					{
						if ( IsMouseDown && !ShowMessage)
						{
							menuData.buttons[i].OnClick( MouseButton );
						}

						Overlay.Draw(itButtonActive.Texture, new Rectangle(0, 0, 256, 32), new Vector3(128, 16, 0), new Vector3(128 + pomRect.Left, 16 + pomRect.Top, 0), Color.White.ToArgb());
					}
					else
					{
						Overlay.Draw(itButton.Texture, new Rectangle(0, 0, 256, 32), new Vector3(128, 16, 0), new Vector3(128 + pomRect.Left, 16 + pomRect.Top, 0), Color.White.ToArgb());
					}

					fntMenuButton.DrawText(Overlay, menuData.buttons[i].Text, pomRect, DrawTextFormat.Center | DrawTextFormat.VerticalCenter, Color.Red );
				}
			}


			// Message box
			if ( ShowMessage )
			{
				it = mainTextures.GetTexture(GuiTextures["messageBox"].textureIdx);
				if ( it != null )
				{
					Overlay.Transform = Matrix.Translation(0.0f, 0.0f, 0.0f);

					Overlay.Draw(it.Texture, new Rectangle(0, 0, 340, 200), new Vector3(170, 100, 0), new Vector3(renderEngine.Device.Viewport.Width / 2, renderEngine.Device.Viewport.Height / 2, 0), Color.White.ToArgb());
				}

				TextureManagerItem itButton = mainTextures.GetTexture(GuiTextures["buttonMainMenu"].textureIdx);
				TextureManagerItem itButtonActive = mainTextures.GetTexture(GuiTextures["buttonMainMenuActive"].textureIdx);

				Rectangle ButtonRect = new Rectangle(42 - 170, 150 - 100 - 16, 256, 32);
				ButtonRect.Offset( renderEngine.Device.Viewport.Width / 2, renderEngine.Device.Viewport.Height / 2 );
				
				fntMenuButton.DrawText(Overlay, MessageText, new Rectangle( ButtonRect.Left, ButtonRect.Top - 100, 256, 140), DrawTextFormat.Center | DrawTextFormat.Top | DrawTextFormat.WordBreak, Color.Lime);

				if ( ButtonRect.Contains(MouseCoords) )
				{
					if ( IsMouseDown )
					{
						showMessage = false;
					}

					Overlay.Draw(itButtonActive.Texture, new Rectangle(0, 0, 256, 32), new Vector3(128, 16, 0), new Vector3(128 + ButtonRect.Left, 16 + ButtonRect.Top, 0), Color.White.ToArgb());
				}
				else
				{
					Overlay.Draw(itButton.Texture, new Rectangle(0, 0, 256, 32), new Vector3(128, 16, 0), new Vector3(128 + ButtonRect.Left, 16 + ButtonRect.Top, 0), Color.White.ToArgb());
				}

				fntMenuButton.DrawText(Overlay, "Ok", ButtonRect, DrawTextFormat.Center | DrawTextFormat.VerticalCenter, Color.Red );
			}

			#region Debug info
			fntMain.DrawText( Overlay, "FPS: " + renderEngine.DisplayFPS.ToString(), 10, 10, Color.Red );
			#endregion;

			// Mouse cursor
			if ( !AdvanceTime )
			{
				it = mainTextures.GetTexture(GuiTextures["cursor"].textureIdx);
				if ( it != null )
				{
					Overlay.Transform = Matrix.Translation( 0.0f, 0.0f, 0.0f);
	
					Overlay.Draw(it.Texture, new Rectangle(0, 0, 28, 26), new Vector3(14, 13, 0), new Vector3( MouseCoords.X, MouseCoords.Y, 0), Color.White.ToArgb());
				}
			}
			
			Overlay.End();

			renderEngine.EndScene();
		}

		/// <summary>
		/// Shows an message box in the GUI
		/// </summary>
		/// <param name="text">Text of message</param>
		public void MessageBox(string text)
		{
			showMessage = true;
			messageText = text;
		}
		
		private GameEngine()
		{

		}
	}
	
	/// <summary>
	/// Contains basic information about game modification
	/// </summary>
	public class ModInfo
	{
		/// <summary>
		/// Mod title
		/// </summary>
		public string title = "untitled mod";
		/// <summary>
		/// If true, shows the engine version in form title
		/// </summary>
		public bool showEngineVersion = true;
		/// <summary>
		/// If true, shows the mod version in form title
		/// </summary>
		public bool showModVersion = true;
		/// <summary>
		/// Mod version
		/// </summary>
		public string modVersion = "0.00";
		/// <summary>
		/// Mod author
		/// </summary>
		public string author = "unknown author";
		/// <summary>
		/// Mod description
		/// </summary>
		public string description = "mod description";
		/// <summary>
		/// Mod homesite
		/// </summary>
		public string website = "http://www.google.com/";
		
		/// <summary>
		/// Creates a new ModInfo instance
		/// </summary>
		public ModInfo()
		{
			
		}
		
		/// <summary>
		/// Loads the mod information class from a Xml file
		/// </summary>
		/// <param name="filename">Xml file name</param>
		/// <returns>True if success</returns>
		public bool LoadFromXml(string filename)
		{
			XmlDocument doc = new XmlDocument();
			doc.Load(filename + ".fdri");
			
			XmlElement el = null;
			
			for ( int i = 0; i < doc.ChildNodes.Count; i ++ )
			{
				if ( doc.ChildNodes[i].Name.ToUpperInvariant() == "FDRFILE" )
				{
					el = (XmlElement)doc.ChildNodes[i];
					
					break;
				}
			}
			
			if ( el != null )
			{
				string version = el.Attributes["version"].Value;
				
				#if GenLog
				Console.WriteLine("\tModification info file version: " + version);
				#endif
				
				if ( el.Attributes["type"].Value.ToLowerInvariant() == "info" )
				{
					el = el["GameInfo"];
					
					if ( el == null )
					{
						Console.WriteLine("\tError loading mod. Mod info file " + filename + ".fdri is not in the correct format.");
						
						return false;
					}
					
					if ( el["Title"] != null ) this.title = el["Title"].InnerText;
					if ( el["ShowEngineVersion"] != null ) this.showEngineVersion = bool.Parse(el["ShowEngineVersion"].InnerText);
					if ( el["ShowModVersion"] != null ) this.showModVersion = bool.Parse(el["ShowModVersion"].InnerText);
					if ( el["ModVersion"] != null ) this.modVersion = el["ModVersion"].InnerText;
					if ( el["Author"] != null ) this.author = el["Author"].InnerText;
					if ( el["Description"] != null ) this.description = el["Description"].InnerText;
					if ( el["Website"] != null ) this.website = el["Website"].InnerText;
					
					return true;
				}
				else
				{
					Console.WriteLine("\tError loading mod. Mod info file " + filename + ".fdri is not in the correct format.");
					
					return false;
				}
			}
			else
			{
				Console.WriteLine("\tError loading mod. Mod info file " + filename + ".fdri is not in the correct format.");
				
				return false;
			}
		}
		
		/// <summary>
		/// Saves the mod information class in a Xml file
		/// </summary>
		/// <param name="filename">Xml file name</param>
		public void SaveToXml(string filename)
		{
			XmlDocument doc = new XmlDocument();
			doc.AppendChild(doc.CreateXmlDeclaration("1.0", "utf-8", string.Empty));
			XmlElement el = doc.CreateElement("FDRFile");
			XmlAttribute atr;
			
			atr = doc.CreateAttribute("version");
			atr.Value = "0.03";
			el.Attributes.Append(atr);
			
			atr = doc.CreateAttribute("type");
			atr.Value = "info";
			el.Attributes.Append(atr);					
			
			doc.AppendChild(el);
			
			XmlElement gInfo = doc.CreateElement("GameInfo");
			el.AppendChild(gInfo);
			
			XmlElement xNode;
			
			xNode = doc.CreateElement("Title");
			xNode.InnerText = this.title;
			gInfo.AppendChild(xNode);
			
			xNode = doc.CreateElement("ShowEngineVersion");
			xNode.InnerText = this.showEngineVersion.ToString();
			gInfo.AppendChild(xNode);
			
			xNode = doc.CreateElement("ShowModVersion");
			xNode.InnerText = this.showModVersion.ToString();
			gInfo.AppendChild(xNode);
			
			xNode = doc.CreateElement("ModVersion");
			xNode.InnerText = this.modVersion;
			gInfo.AppendChild(xNode);
			
			xNode = doc.CreateElement("Author");
			xNode.InnerText = this.author;
			gInfo.AppendChild(xNode);
			
			xNode = doc.CreateElement("Description");
			xNode.InnerText = this.description;
			gInfo.AppendChild(xNode);
			
			xNode = doc.CreateElement("Website");
			xNode.InnerText = this.website;
			gInfo.AppendChild(xNode);
			
			doc.Save(filename + ".fdri");
		}
	}
	
	/// <summary>
	/// Contains texture information for the core
	/// </summary>
	public class TextureInfo
	{
		/// <summary>
		/// Filename
		/// </summary>
		public string filename = "";
		/// <summary>
		/// Texture index in TextureManager
		/// </summary>
		public int textureIdx = -1;
		
		/// <summary>
		/// Creates a new TextureInfo instance
		/// </summary>
		/// <param name="filename">Texture filename</param>
		/// <param name="textureIdx">Texture index in TextureManager</param>
		public TextureInfo(string filename, int textureIdx)
		{
			this.filename = filename;
			this.textureIdx = textureIdx;
		}
	}
	
	/// <summary>
	/// Game screen
	/// </summary>
	public enum GameScreen
	{
		/// <summary>
		/// None/any screen
		/// </summary>
		None = 0,
		/// <summary>
		/// Don't change current screen
		/// </summary>
		NoChange,
		/// <summary>
		/// Unknown screen
		/// </summary>
		Unknown,
		/// <summary>
		/// EarthCity view
		/// </summary>
		EarthCity,
		/// <summary>
		/// Orbital view
		/// </summary>
		Orbital,
		/// <summary>
		/// Station module interface
		/// </summary>
		StationModule
//		MasterControl,
//		Stocktaker,
//		DepositAnalysis,
//		SaveLoad,
//		NewsBulletins,
//		EarthCity,
//		StarSelector,
//		Orbital,
//		Production,
//		Stores,
//		SpaceBay,
//		SpaceDock,
//		ShipWindow,
//		ShipComputer,
//		SelfDestruct,
//		Training,
//		Research,
//		ShuttleBay,
//		Resources,
//		MiningStores
	}
}
