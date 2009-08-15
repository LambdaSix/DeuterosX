/*
 * Created by SharpDevelop.
 * User: Luaan
 * Date: 8.7.2007
 * Time: 16:48
 * 
 */

using System;
using System.Drawing;
using System.Xml;

namespace Deuteros
{
	/// <summary>
	/// Description of StructXml.
	/// </summary>
	public static class StructXml
	{
		public static string ColorToXml(Color col)
		{
			if ( col.IsNamedColor ) return col.Name;
			else return "#" + col.A.ToString("X2") + col.R.ToString("X2") + col.G.ToString("X2")
				+ col.B.ToString("X2");
		}
		
		public static Color ColorFromXml(string col)
		{
			if ( col.StartsWith("#") ) return Color.FromArgb(Convert.ToByte(col.Substring(1, 2), 16), Convert.ToByte(col.Substring(3, 2), 16),
			                                                 Convert.ToByte(col.Substring(5, 2), 16), Convert.ToByte(col.Substring(7, 2)));
			else return Color.FromName(col);
		}
		
		public static void RectangleToXml(Rectangle rect, XmlNode node)
		{
			XmlDocument doc = node.OwnerDocument;
			
			XmlAttribute at;
			at = doc.CreateAttribute("Left");
			at.Value = rect.Left.ToString();
			node.Attributes.Append(at);
			
			at = doc.CreateAttribute("Top");
			at.Value = rect.Top.ToString();
			node.Attributes.Append(at);
			
			at = doc.CreateAttribute("Width");
			at.Value = rect.Width.ToString();
			node.Attributes.Append(at);
			
			at = doc.CreateAttribute("Height");
			at.Value = rect.Height.ToString();
			node.Attributes.Append(at);
		}
		
		public static Rectangle RectangleFromXml(XmlNode node)
		{
			return new Rectangle(int.Parse(node.Attributes["Left"].Value),
			                     int.Parse(node.Attributes["Top"].Value),
			                     int.Parse(node.Attributes["Width"].Value),
			                     int.Parse(node.Attributes["Height"].Value));
		}
		
		public static void PointToXml(Point pnt, XmlNode node)
		{
			XmlDocument doc = node.OwnerDocument;
			XmlAttribute at;
			
			at = doc.CreateAttribute("X");
			at.Value = pnt.X.ToString();
			node.Attributes.Append(at);
			
			at = doc.CreateAttribute("Y");
			at.Value = pnt.Y.ToString();
			node.Attributes.Append(at);
		}
		
		public static Point PointFromXml(XmlNode node)
		{
			return new Point(int.Parse(node.Attributes["X"].Value),
			                 int.Parse(node.Attributes["Y"].Value));
		}
		
		public static object CreateInstanceFromType(string typ)
		{
			Type ttyp = System.Type.GetType(typ);
			if ( ttyp != null )
			{
				object si = ttyp.GetConstructor( new Type[0] {} ).Invoke( new object[0] {} );
				
				return si;
			}
			else
			{
				return null;
			}
		}
	}
}
