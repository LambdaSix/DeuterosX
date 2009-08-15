/*
 * Created by SharpDevelop.
 * User: Luaan
 * Date: 7.6.2007
 * Time: 12:45
 * 
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;

using Microsoft.DirectX.Direct3D;

namespace Deuteros.Gui
{
	/// <summary>
	/// Description of GuiFont.
	/// </summary>
	public sealed class GuiFont
	{
		private string id = "";
		/// <summary>
		/// Font id
		/// </summary>
		public string Id
		{
			get { return this.id; }
			set { this.id = value; }
		}
		
		private GuiFontType fontSource = GuiFontType.SystemFont;
		/// <summary>
		/// Font source
		/// </summary>
		public GuiFontType FontSource
		{
			get { return this.fontSource; }
			set { this.fontSource = value; }
		}
		
		private Dictionary<string, string> fontParams = null;
		/// <summary>
		/// Font params
		/// </summary>
		public Dictionary<string, string> FontParams
		{
			get { return this.fontParams; }
		}
		
		private Font dxFont = null;
		/// <summary>
		/// DirectX font
		/// </summary>
		public Font DXFont
		{
			get { return this.dxFont; }
			set { this.dxFont = value; }
		}
		
		public GuiFont(string id, Font dxFont)
		{
			this.fontParams = new Dictionary<string, string>();
			this.id = id;
			this.dxFont = dxFont;
		}
		
		/// <summary>
		/// Loads GuiFont from Xml
		/// </summary>
		/// <param name="node"></param>
		public GuiFont(XmlNode node)
		{
			this.fontParams = new Dictionary<string, string>();
			
			FromXml(node);
		}
		
		public void FromXml(XmlNode node)
		{
			if ( node["Id"] != null ) this.id = node["Id"].InnerText;
			if ( node["Source"] != null ) this.FontSource = (GuiFontType)GuiFontType.Parse(typeof(GuiFontType), node["Source"].InnerText);
			
			if ( node["Params"] != null )
			{
				FontParams.Clear();
				
				foreach ( XmlElement el in node["Params"].ChildNodes )
				{
					FontParams.Add(el.Name, el.InnerText);
				}
			}
			
			CreateDXFont();
		}
		
		/// <summary>
		/// Creates DXFont from params
		/// </summary>
		public void CreateDXFont()
		{
			switch ( FontSource )
			{
				case GuiFontType.SystemFont:
					{
						string name = "Tahoma";
						float size = 12.0f;
						System.Drawing.FontStyle style = System.Drawing.FontStyle.Regular;
						
						if ( FontParams.ContainsKey("FontName") ) name = FontParams["FontName"];
						if ( FontParams.ContainsKey("FontSize") ) float.TryParse(FontParams["FontSize"], out size);
						if ( FontParams.ContainsKey("FontStyle") ) style = (System.Drawing.FontStyle)System.Drawing.FontStyle.Parse(style.GetType(), FontParams["FontStyle"]);
						
						this.dxFont = new Microsoft.DirectX.Direct3D.Font(GameEngine.Instance.RenderEngine.Device,
						                                                  new System.Drawing.Font(name, size, style));
						
						break;
					}
				default:
					{
						this.dxFont = new Microsoft.DirectX.Direct3D.Font(GameEngine.Instance.RenderEngine.Device, GameEngine.Instance.Target.Font);
						Console.Write("\t\tUnknown FontSource: " + FontSource.ToString());
						
						break;
					}
			}
		}
		
		public void ToXml(XmlNode node)
		{
			XmlDocument doc = node.OwnerDocument;
			
			XmlElement el = doc.CreateElement("Id");
			el.InnerText = this.id;
			node.AppendChild(el);
			
			el = doc.CreateElement("Source");
			el.InnerText = this.fontSource.ToString();
			node.AppendChild(el);
			
			el = doc.CreateElement("Params");
			node.AppendChild(el);
			
			XmlElement el2;
				
			foreach( KeyValuePair<string, string> par in this.fontParams )
			{
				el2 = doc.CreateElement(par.Key);
				el2.InnerText = par.Value;
				el.AppendChild(el2);
			}
		}
	}

	public enum GuiFontType
	{
		SystemFont = 0,
		BitmapFont
	}
}
