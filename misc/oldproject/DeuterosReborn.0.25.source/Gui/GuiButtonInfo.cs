/*
 * Created by SharpDevelop.
 * User: Luaan
 * Date: 5.6.2007
 * Time: 12:11
 * 
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;
using System.Xml;

using DrawTextFormat = Microsoft.DirectX.Direct3D.DrawTextFormat;

using Deuteros;
using Deuteros.Stations;

namespace Deuteros.Gui
{
	/// <summary>
	/// Contains information about a Gui button
	/// </summary>
	public class GuiButtonInfo
	{
		/// <summary>
		/// The id of the menu this button is hosted on
		/// </summary>
		public string GuiMenuId = "";
		/// <summary>
		/// Button area
		/// </summary>
		public Rectangle ButtonRect = Rectangle.Empty;
		/// <summary>
		/// Button tooltip
		/// </summary>
		public string Tooltip = "";
		/// <summary>
		/// Button parameters
		/// </summary>
		public Dictionary<string, GuiButtonParam> Params = null;
		/// <summary>
		/// Button picture (id of texture)
		/// </summary>
		public string Picture = "";
		/// <summary>
		/// Button text
		/// </summary>
		public string Text = "";
		/// <summary>
		/// Id of font used to render button text
		/// </summary>
		public string Font = "";
		/// <summary>
		/// Button text color
		/// </summary>
		public Color ForeColor = Color.Empty;
		/// <summary>
		/// Button text draw format
		/// </summary>
		public DrawTextFormat TextFormat = DrawTextFormat.None;
		/// <summary>
		/// Button is visible and responds to mouse hover and click
		/// </summary>
		public bool Enabled = true;
		/// <summary>
		/// Button graphics animation frames
		/// </summary>
		public int AnimationFrames = 1;
		/// <summary>
		/// Button animation time before change
		/// </summary>
		public int AnimationTime = 100;
		/// <summary>
		/// Color of the picture drawn
		/// </summary>
		public Color PictureColor = Color.White;
		/// <summary>
		/// The template this button is based on
		/// </summary>
		public string Template = "";
		
		public GuiButtonInfo Clone()
		{
			GuiButtonInfo btn = new GuiButtonInfo();
			btn.Template = this.Template;
			btn.AnimationFrames = this.AnimationFrames;
			btn.AnimationTime = this.AnimationTime;
			btn.ButtonRect = this.ButtonRect;
			btn.Enabled = this.Enabled;
			btn.Font = this.Font;
			btn.ForeColor = this.ForeColor;
			btn.GuiMenuId = this.GuiMenuId;
			
			foreach ( KeyValuePair<string, GuiButtonParam> par in this.Params )
			{
				btn.Params.Add(par.Key, par.Value.Clone());
			}
			
			btn.Picture = this.Picture;
			btn.PictureColor = this.PictureColor;
			btn.Text = this.Text;
			btn.TextFormat = this.TextFormat;
			btn.Tooltip = this.Tooltip;
			
			return btn;
		}
		
		/// <summary>
		/// Create a new instance of GuiButtonInfo
		/// </summary>
		/// <param name="guiMenuId">ID of associated menu</param>
		/// <param name="buttonRect">Button area</param>
		/// <param name="tooltip">Tooltip text</param>
		/// <param name="picture">Texture ID</param>
		/// <param name="enabled">If false doesn't show the button nor does the button react to mouse clicks.</param>
		public GuiButtonInfo(string guiMenuId, Rectangle buttonRect, string tooltip, 
			string picture, bool enabled): this()
		{
			this.GuiMenuId = guiMenuId;
			this.ButtonRect = buttonRect;
			this.Tooltip = tooltip;
			this.Picture = picture;
			this.Enabled = enabled;
		}
		
		public GuiButtonInfo(string guiMenuId, string text, string font, 
			Color foreColor, DrawTextFormat textFormat, Rectangle buttonRect, 
			string tooltip, string picture, bool enabled): this()
		{
			this.GuiMenuId = guiMenuId;
			this.Text = text;
			this.Font = font;
			this.ForeColor = foreColor;
			this.TextFormat = textFormat;
			this.ButtonRect = buttonRect;
			this.Tooltip = tooltip;
			this.Picture = picture;
			this.Enabled = enabled;
		}
		
		public GuiButtonInfo(string guiMenuId, Rectangle buttonRect, string tooltip, 
			string picture, bool enabled, int animationFrames, 
			int animationTime): this()
		{
			this.GuiMenuId = guiMenuId;
			this.ButtonRect = buttonRect;
			this.Tooltip = tooltip;
			this.Picture = picture;
			this.Enabled = enabled;
			this.AnimationFrames = animationFrames;
			this.AnimationTime = animationTime;
		}
		
		public GuiButtonInfo(string guiMenuId, string text, string font, 
			Color foreColor, DrawTextFormat textFormat, Rectangle buttonRect, 
			string tooltip, string picture, bool enabled, 
			int animationFrames, int animationTime): this()
		{
			this.GuiMenuId = guiMenuId;
			this.Text = text;
			this.Font = font;
			this.ForeColor = foreColor;
			this.TextFormat = textFormat;
			this.ButtonRect = buttonRect;
			this.Tooltip = tooltip;
			this.Picture = picture;
			this.Enabled = enabled;
			this.AnimationFrames = animationFrames;
			this.AnimationTime = animationTime;
		}
		
		/// <summary>
		/// Creates a new instance of GuiButtonInfo. Should not be used out of the InvokeCreate method
		/// </summary>
		public GuiButtonInfo()
		{
			Params = new Dictionary<string, GuiButtonParam>();
		}
		
		public static GuiButtonInfo InvokeCreate(string typ)
		{
			GuiButtonInfo si = StructXml.CreateInstanceFromType(typ) as GuiButtonInfo;
			
			if ( si == null ) return new GuiButtonInfo();
			else return si;
		}
		
		public void FromXml(XmlNode node)
		{
			//this.GuiMenuId = node.SelectSingleNode("GuiMenuId").InnerText;
			this.Text = node.SelectSingleNode("Text").InnerText;
			this.Font = node.SelectSingleNode("Font").InnerText;
			this.ForeColor = StructXml.ColorFromXml(node.SelectSingleNode("ForeColor").InnerText);
			this.TextFormat = (DrawTextFormat)DrawTextFormat.Parse(typeof(DrawTextFormat), node.SelectSingleNode("TextFormat").InnerText);
			this.ButtonRect = StructXml.RectangleFromXml(node.SelectSingleNode("ButtonRect"));
			
			this.Tooltip = node.SelectSingleNode("Tooltip").InnerText;
			
			this.Params.Clear();
			foreach ( XmlNode xPar in node.SelectSingleNode("Params").ChildNodes )
			{
				GuiButtonParam par = GuiButtonParam.InvokeCreate(xPar.Attributes["Type"].Value);
				par.FromXml(xPar);
				this.Params.Add(xPar.Name, par);
			}
			
			this.Picture = node.SelectSingleNode("Picture").InnerText;
			this.PictureColor = StructXml.ColorFromXml(node.SelectSingleNode("PictureColor").InnerText);
			this.Enabled = bool.Parse(node.SelectSingleNode("Enabled").InnerText);
			this.AnimationFrames = int.Parse(node.SelectSingleNode("AnimationFrames").InnerText);
			this.AnimationTime = int.Parse(node.SelectSingleNode("AnimationTime").InnerText);
		}
		
		public void ToXml(XmlNode node)
		{
			XmlDocument doc = node.OwnerDocument;
			
			XmlElement el;
			XmlAttribute at;
			
//			el = doc.CreateElement("GuiMenuId");
//			el.InnerText = GuiMenuId;
//			node.AppendChild(el);
			
			el = doc.CreateElement("Text");
			el.InnerText = Text;
			node.AppendChild(el);
			
			el = doc.CreateElement("Font");
			el.InnerText = Font;
			node.AppendChild(el);
			
			el = doc.CreateElement("ForeColor");
			el.InnerText = StructXml.ColorToXml(ForeColor);
			node.AppendChild(el);
			
			el = doc.CreateElement("TextFormat");
			el.InnerText = TextFormat.ToString();
			node.AppendChild(el);
			
			el = doc.CreateElement("ButtonRect");
			node.AppendChild(el);
			
			StructXml.RectangleToXml(this.ButtonRect, el);
			
			el = doc.CreateElement("Tooltip");
			el.InnerText = Tooltip;
			node.AppendChild(el);
			
			el = doc.CreateElement("Params");
			node.AppendChild(el);
			
			XmlElement el2;
			
			foreach ( KeyValuePair<string, GuiButtonParam> par in Params )
			{
				if ( par.Value != null )
				{
					el2 = doc.CreateElement(par.Key);
					at = doc.CreateAttribute("Type");
					at.Value = par.Value.GetType().ToString();
					el2.Attributes.Append(at);
					el.AppendChild(el2);
					
					par.Value.ToXml(el2);
				}
			}
			
			el = doc.CreateElement("Picture");
			el.InnerText = Picture;
			node.AppendChild(el);
			
			el = doc.CreateElement("PictureColor");
			el.InnerText = StructXml.ColorToXml(PictureColor);
			node.AppendChild(el);
			
			el = doc.CreateElement("Enabled");
			el.InnerText = Enabled.ToString();
			node.AppendChild(el);
			
			el = doc.CreateElement("AnimationFrames");
			el.InnerText = AnimationFrames.ToString();
			node.AppendChild(el);
			
			el = doc.CreateElement("AnimationTime");
			el.InnerText = AnimationTime.ToString();
			node.AppendChild(el);
		}
	}
	
	public class MethodParamInfo
	{
		public string ParamType = "";
		public string Value = "";
		
		public MethodParamInfo(string ParamType, string Value)
		{
			this.ParamType = ParamType;
			this.Value = Value;
		}
	}
	
	public abstract class GuiButtonParam
	{
		public virtual void FromXml(XmlNode node)
		{}
		
		public virtual void ToXml(XmlNode node)
		{}
		
		public virtual void DoAction()
		{}
		
		/// <summary>
		/// For check support
		/// </summary>
		/// <returns>True if check is successful, false if not</returns>
		public virtual bool DoCheck()
		{
			return true;
		}
		
		public virtual GuiButtonParam Clone()
		{
			
			return this;
		}
		
		public static GuiButtonParam InvokeCreate(string typ)
		{
			GuiButtonParam si = StructXml.CreateInstanceFromType(typ) as GuiButtonParam;
			
			if ( si == null ) return new GuiButtonParamDoNothing();
			else return si;
		}
	}
	
	public class GuiButtonParamDoNothing: GuiButtonParam
	{
		public GuiButtonParamDoNothing()
		{
			
		}
	}
	
	public class GuiButtonParamExecuteModuleMethod: GuiButtonParam
	{
		public string moduleMethod = "";
		public string moduleId = "";
		public List<MethodParamInfo> methodParams = null;
		
		public override GuiButtonParam Clone()
		{
			GuiButtonParamExecuteModuleMethod par = new GuiButtonParamExecuteModuleMethod(this.moduleMethod, this.moduleId);
			par.methodParams.AddRange(this.methodParams);
			
			return par;
		}
		
		public GuiButtonParamExecuteModuleMethod()
		{
			
		}
		
		public GuiButtonParamExecuteModuleMethod(string moduleMethod, string moduleId)
		{
			this.moduleId = moduleId;
			this.moduleMethod = moduleMethod;
			this.methodParams = new List<MethodParamInfo>();
		}
		
		public override void FromXml(XmlNode node)
		{
			this.moduleId = node["ModuleId"].InnerText;
			this.moduleMethod = node["ModuleMethod"].InnerText;
			
			this.methodParams.Clear();
			foreach ( XmlNode xPar in node["MethodParams"].ChildNodes )
			{
				MethodParamInfo par = new MethodParamInfo(xPar.Name, xPar.InnerText);
				
				this.methodParams.Add(par);
			}
		}
		
		public override void ToXml(XmlNode node)
		{
			XmlDocument doc = node.OwnerDocument;
			XmlElement el;
			
			el = doc.CreateElement("ModuleId");
			el.InnerText = this.moduleId;
			node.AppendChild(el);
			
			el = doc.CreateElement("ModuleMethod");
			el.InnerText = this.moduleMethod;
			node.AppendChild(el);
			
			el = doc.CreateElement("MethodParams");
			node.AppendChild(el);
			
			XmlElement el2;
			
			for ( int i = 0; i < methodParams.Count; i ++ )
			{
				el2 = doc.CreateElement(methodParams[i].ParamType);
				el2.InnerText = methodParams[i].Value;
				el.AppendChild(el2);
			}
		}
		
		public override void DoAction()
		{
			if ( GameEngine.Instance.CurrentStation != null )
			{
				for ( int i = 0; i < GameEngine.Instance.CurrentStation.Modules.Count; i ++ )
				{
					if ( GameEngine.Instance.CurrentStation.Modules[i].GetType().ToString() == moduleId )
					{
						Type typ = GameEngine.Instance.CurrentStation.Modules[i].GetType();
						Type parTyp;
						object[] pars = new object[methodParams.Count];
						
						for ( int j = 0; j < methodParams.Count; j ++ )
						{
							parTyp = Type.GetType(methodParams[j].ParamType);
							if ( parTyp == typeof(string) )
							{
								pars[j] = methodParams[j].Value;
							}
							else
							{
								// TODO: Parse parameter from string to parTyp
							}
						}
						
						typ.InvokeMember(moduleMethod, System.Reflection.BindingFlags.InvokeMethod, null, GameEngine.Instance.CurrentStation.Modules[i], pars );
					}
				}
			}
		}
	}
	
	public class GuiButtonParamChangeModuleProperty: GuiButtonParam
	{
		public string moduleProperty = "";
		public string moduleId = "";
		public string nextValue = "";
		
		public GuiButtonParamChangeModuleProperty()
		{
			
		}
		
		public GuiButtonParamChangeModuleProperty(string moduleProperty, string moduleId, string nextValue)
		{
			this.moduleId = moduleId;
			this.moduleProperty = moduleProperty;
			this.nextValue = nextValue;
		}
		
		public override void FromXml(XmlNode node)
		{
			this.moduleId = node["ModuleId"].InnerText;
			this.moduleProperty = node["ModuleProperty"].InnerText;
			this.nextValue = node["NextValue"].InnerText;
		}
		
		public override void ToXml(XmlNode node)
		{
			XmlDocument doc = node.OwnerDocument;
			XmlElement el;
			
			el = doc.CreateElement("ModuleId");
			el.InnerText = this.moduleId;
			node.AppendChild(el);
			
			el = doc.CreateElement("ModuleProperty");
			el.InnerText = this.moduleProperty;
			node.AppendChild(el);
			
			el = doc.CreateElement("NextValue");
			el.InnerText = this.nextValue;
			node.AppendChild(el);
		}
		
		public override void DoAction()
		{
			// TODO: Change StationModule property
		}
	}
	
	public class GuiButtonParamChangeScreen: GuiButtonParam
	{
		public GameScreen NextScreen = GameScreen.NoChange;
		
		public GuiButtonParamChangeScreen()
		{
			
		}
		
		public GuiButtonParamChangeScreen(GameScreen nextScreen)
		{
			this.NextScreen = nextScreen;
		}
		
		public override void FromXml(XmlNode node)
		{
			this.NextScreen = (GameScreen)GameScreen.Parse(typeof(GameScreen), node.InnerText);
		}
		
		public override void ToXml(XmlNode node)
		{
			node.InnerText = NextScreen.ToString();
		}
		
		public override void DoAction()
		{
			if ( NextScreen != GameScreen.NoChange )
			{
				GameEngine.Instance.CurrentScreen = NextScreen;
			}
		}
	}
	
	public class GuiButtonParamChangeStation: GuiButtonParam
	{
		public string NextStation = "";
		
		public GuiButtonParamChangeStation()
		{
			
		}
		
		public GuiButtonParamChangeStation(string nextStation)
		{
			this.NextStation = nextStation;
		}
		
		public override void FromXml(XmlNode node)
		{
			this.NextStation = node.InnerText;
		}
		
		public override void ToXml(XmlNode node)
		{
			node.InnerText = NextStation;
		}
		
		public override void DoAction()
		{
			// Find station
			bool found = false;
			for ( int j = 0; j < GameEngine.Instance.StationList.Count; j ++ )
			{
				if ( GameEngine.Instance.StationList[j] != null && GameEngine.Instance.StationList[j].Title == NextStation )
				{
					GameEngine.Instance.SetCurrentStation(GameEngine.Instance.StationList[j]);
					found = true;
					
					break;
				}
			}
			
			if ( !found ) GameEngine.Instance.SetCurrentStation(null);
		}
	}
	
	public class GuiButtonParamChangeStationModule: GuiButtonParam
	{
		public string NextStationModule = "";
		
		public GuiButtonParamChangeStationModule()
		{
			
		}
				
		public GuiButtonParamChangeStationModule(string nextStationModule)
		{
			this.NextStationModule = nextStationModule;
		}
		
		public override void FromXml(XmlNode node)
		{
			this.NextStationModule = node.InnerText;
		}
		
		public override void ToXml(XmlNode node)
		{
			node.InnerText = NextStationModule;
		}
		
		public override void DoAction()
		{
			GameEngine.Instance.SetCurrentModule(this.NextStationModule);
		}
	}
	
	public class GuiButtonParamChangeLocation: GuiButtonParam
	{
		public string NextLocation = "";
		
		public GuiButtonParamChangeLocation()
		{
			
		}
		
		public GuiButtonParamChangeLocation(string nextLocation)
		{
			this.NextLocation = nextLocation;
		}
		
		public override void FromXml(XmlNode node)
		{
			this.NextLocation = node.InnerText;
		}
		
		public override void ToXml(XmlNode node)
		{
			node.InnerText = NextLocation;
		}
		
		public override void DoAction()
		{
			GameEngine.Instance.CurrentLocation = NextLocation;
		}
	}
	
	public class GuiButtonParamTimeAdvance: GuiButtonParam
	{
		public bool OnHold = false;
		
		public GuiButtonParamTimeAdvance()
		{
			
		}
		
		public GuiButtonParamTimeAdvance(bool onHold)
		{
			this.OnHold = onHold;
		}
		
		public override void FromXml(XmlNode node)
		{
			this.OnHold = bool.Parse(node.InnerText);
		}
		
		public override void ToXml(XmlNode node)
		{
			node.InnerText = OnHold.ToString();
		}
		
		public override void DoAction()
		{
			GameEngine.Instance.TimeAdvanceOnHold = OnHold;
			GameEngine.Instance.StartTimeAdvance();
		}
	}
	
	public class GuiButtonParamCheckStation: GuiButtonParam
	{
		public string CheckStation = "";
		
		public GuiButtonParamCheckStation()
		{
			
		}
		
		public GuiButtonParamCheckStation(string checkStation)
		{
			this.CheckStation = checkStation;
		}
		
		public override void FromXml(XmlNode node)
		{
			this.CheckStation = node.InnerText;
		}
		
		public override void ToXml(XmlNode node)
		{
			node.InnerText = CheckStation;
		}
		
		public override bool DoCheck()
		{
			return GameEngine.Instance.CurrentStation != null && GameEngine.Instance.CurrentStation.Title == CheckStation;
		}
	}
	
	public class GuiButtonParamCheckLocation: GuiButtonParam
	{
		public string CheckLocation = "";
		
		public GuiButtonParamCheckLocation()
		{
			
		}
		
		public GuiButtonParamCheckLocation(string checkLocation)
		{
			this.CheckLocation = checkLocation;
		}
		
		public override void FromXml(XmlNode node)
		{
			this.CheckLocation = node.InnerText;
		}
		
		public override void ToXml(XmlNode node)
		{
			node.InnerText = CheckLocation;
		}
		
		public override bool DoCheck()
		{
			return GameEngine.Instance.CurrentLocation == CheckLocation;
		}
	}
	
	public class GuiButtonParamCheckTimeAdvance: GuiButtonParam
	{
		public bool state = true;
		
		public GuiButtonParamCheckTimeAdvance()
		{
			
		}
		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="state">If true, check returns true when TimeAdvance is running and vice versa.</param>
		public GuiButtonParamCheckTimeAdvance(bool state)
		{
		}
		
		public override void FromXml(XmlNode node)
		{
			this.state = bool.Parse(node.InnerText);
		}
		
		public override void ToXml(XmlNode node)
		{
			node.InnerText = state.ToString();
		}
		
		public override bool DoCheck()
		{
			return GameEngine.Instance.AdvanceTime ^ state;
		}
	}
}
