/*
 * Created by SharpDevelop.
 * User: Luaan
 * Date: 4.6.2007
 * Time: 11:16
 * 
 */

using System;
using System.Collections;
using System.Xml;

using Deuteros.Gui;

namespace Deuteros.Stations
{
	/// <summary>
	/// Description of StationModuleBase.
	/// </summary>
	public class StationModuleBase
	{
		public StationModuleBase()
		{
			
		}
		
		internal StationBase parent = null;
		/// <summary>
		/// Gets parent station
		/// </summary>
		public StationBase Parent
		{
			get { return this.parent; }
		}
		
		protected GuiButtonInfo buttonInfo = null;
		public virtual GuiButtonInfo ButtonInfo
		{
			get { return buttonInfo; }
		}
		
		protected int buttonIndex = -1;
		public virtual int ButtonIndex
		{
			get { return buttonIndex; }
			set { buttonIndex = value; }
		}
		
		public static StationModuleBase InvokeCreate(string typ)
		{
			StationModuleBase si = StructXml.CreateInstanceFromType(typ) as StationModuleBase;
			
			if ( si == null ) return new StationModuleBase();
			else return si;
		}
		
		public virtual void FromXml(XmlNode node)
		{
		}
		
		public virtual void ToXml(XmlNode node)
		{
			XmlDocument doc = node.OwnerDocument;
			
			//el = doc.CreateElement("ButtonInfo");
			//node.AppendChild(el);
			
			//buttonInfo.ToXml(el);
			
//			el = doc.CreateElement("ButtonIndex");
//			el.InnerText = buttonIndex.ToString();
//			node.AppendChild(el);
		}
		
		/// <summary>
		/// Occurs when module is mounted on a station
		/// </summary>
		public virtual void OnMount()
		{
			// Base class does nothing
		}

		/// <summary>
		/// Occurs when module is dismounted from a station
		/// </summary>
		public virtual void OnDismount()
		{
			// Base class does nothing
		}
		
		/// <summary>
		/// Handles any special actions to take when the station moves a frame
		/// </summary>
		public virtual void OnFrame()
		{
			// Base class does nothing
		}
		
		/// <summary>
		/// Handles any special actions to take when rendering the parent station
		/// </summary>
		public virtual void OnRender()
		{
			// Base class does nothing
		}
		
		/// <summary>
		/// Renders the interface
		/// </summary>
		/// <param name="overlay">Overlay sprite</param>
		public virtual void OnRenderInterface(Microsoft.DirectX.Direct3D.Sprite overlay)
		{
			// Base class does nothing
		}
		
		/// <summary>
		/// Initializes the station module menus
		/// </summary>
		public virtual void OnInitMenus()
		{
			// Base class does nothing
		}
		
		/// <summary>
		/// Handles next turn calculations
		/// </summary>
		public virtual void OnNextTurn()
		{
			// Base class does nothing
		}
	}
}
