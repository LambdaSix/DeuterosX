/*
 * Created by SharpDevelop.
 * User: Luaan
 * Date: 5.6.2007
 * Time: 12:18
 * 
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;

using System.Xml;

using Deuteros;

namespace Deuteros.Gui
{
	/// <summary>
	/// Description of GuiMenu.
	/// </summary>
	public class GuiMenu
	{
		/// <summary>
		/// Creates a new GuiMenu
		/// </summary>
		/// <param name="id">Menu id (must be unique)</param>
		public GuiMenu(string id): this()
		{
			this.id = id;
		}
		
		/// <summary>
		/// Only for use in InvokeCreate
		/// </summary>
		public GuiMenu()
		{
			buttons = new Dictionary<string, GuiButtonInfo>();
		}
		
		protected Rectangle textureArea = Rectangle.Empty;
		/// <summary>
		/// Area of gui image in texture
		/// </summary>
		public Rectangle TextureArea
		{
			get { return textureArea; }
			set { textureArea = value; }
		}
		
		protected Point location = Point.Empty;
		/// <summary>
		/// Menu drawing location
		/// </summary>
		public Point Location
		{
			get 
			{
				return new Point( (location.X < 0? location.X + GameEngine.Instance.RenderEngine.Device.Viewport.Width: location.X),
				                 (location.Y < 0? location.Y + GameEngine.Instance.RenderEngine.Device.Viewport.Height: location.Y) );
			}
			set { location = value; }
		}
		
		protected string picture = string.Empty;
		/// <summary>
		/// Menu texture
		/// </summary>
		public string Picture
		{
			get { return picture; }
			set { picture = value; }
		}
		
		protected string id = "";
		/// <summary>
		/// Gets menu id
		/// </summary>
		public string Id
		{
			get { return this.id; }
		}
		
		protected Dictionary<string, GuiButtonInfo> buttons = null;
		/// <summary>
		/// Menu buttons
		/// </summary>
		public Dictionary<string, GuiButtonInfo> Buttons
		{
			get { return buttons; }
		}
		
		public static GuiMenu InvokeCreate(string typ)
		{
			GuiMenu mnu = StructXml.CreateInstanceFromType(typ) as GuiMenu;
			
			if ( mnu == null ) return new GuiMenu();
			else return mnu;
		}
		
		public virtual void FromXml(XmlNode node)
		{
			FromXml(node, true);
		}
		
		public virtual void FromXml(XmlNode node, bool loadButtons)
		{
			this.id = node.SelectSingleNode("Id").InnerText;
			this.textureArea = StructXml.RectangleFromXml(node.SelectSingleNode("TextureArea"));
			this.picture = node.SelectSingleNode("Picture").InnerText;
			this.location = StructXml.PointFromXml(node.SelectSingleNode("Location"));
			
			this.buttons.Clear();
			if ( loadButtons )
			{
				foreach ( XmlNode xBtn in node.SelectSingleNode("Buttons") )
				{
					string typ = xBtn.Name;
					
					if ( xBtn.Attributes["Template"] != null ) typ = xBtn.Attributes["Template"].Value;
						
					GuiButtonInfo btn = GameEngine.Instance.GuiButtonTemplates[typ].Clone();
					
					btn.GuiMenuId = this.id;
					
					this.buttons.Add(xBtn.Name, btn);
				}
			}
		}
		
		public virtual void ToXml(XmlNode node)
		{
			XmlDocument doc = node.OwnerDocument;
			XmlAttribute at;
			
			XmlElement el = doc.CreateElement("Id");
			el.InnerText = this.id;
			node.AppendChild(el);
			
			el = doc.CreateElement("TextureArea");
			node.AppendChild(el);
			
			StructXml.RectangleToXml(this.textureArea, el);
			
			el = doc.CreateElement("Picture");
			el.InnerText = this.picture;
			node.AppendChild(el);
			
			el = doc.CreateElement("Location");
			node.AppendChild(el);
			
			StructXml.PointToXml(this.location, el);
			
			el = doc.CreateElement("Buttons");
			node.AppendChild(el);
			
			XmlElement el2;
			
			foreach ( KeyValuePair<string, GuiButtonInfo> btn in this.buttons )
			{
				// ALTERNATIVE: use this to save the button info: 
				// btn.ToXml(el2);
				// el2 = doc.CreateElement(btn.GetType().ToString());
				
				el2 = doc.CreateElement(btn.Key);
				el.AppendChild(el2);
				
				at = doc.CreateAttribute("Template");
				at.Value = btn.Value.Template;
			}
		}
	}
}
