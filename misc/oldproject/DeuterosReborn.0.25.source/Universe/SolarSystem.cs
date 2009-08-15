/*
 * Created by SharpDevelop.
 * User: Luaan
 * Date: 12.7.2007
 * Time: 18:44
 * 
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml;

namespace Deuteros.Universe
{
	/// <summary>
	/// Description of SolarSystem.
	/// </summary>
	public sealed class SolarSystem
	{
		public SolarSystem()
		{
            bodies = new Dictionary<string, SpaceItem>();
		}

        private string name = "Unknown solar system";
        /// <summary>
        /// Gets or sets the name of this solar system
        /// </summary>
        [Description("Name of this solar system")]
        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }

        private Dictionary<string, SpaceItem> bodies = null;
        /// <summary>
        /// Gets the bodies in this galaxy
        /// </summary>
        [Browsable(false)]
        public Dictionary<string, SpaceItem> Bodies
        {
            get { return this.bodies; }
        }

        public static SolarSystem InvokeCreate(string typ)
        {
            SolarSystem si = StructXml.CreateInstanceFromType(typ) as SolarSystem;

            if (si == null) return new SolarSystem();
            else return si;
        }

        public void FromXml(XmlNode node)
        {
            this.name = node.Attributes["Name"].Value;

            SpaceItem sit;
            bodies.Clear();
            foreach (XmlNode xSit in node.ChildNodes)
            {
                sit = SpaceItem.InvokeCreate(xSit.Attributes["Type"].Value);
                sit.FromXml(xSit);

                bodies.Add(xSit.Name, sit);
            }
        }

        public void ToXml(XmlNode node)
        {
            XmlDocument doc = node.OwnerDocument;

            XmlAttribute at = doc.CreateAttribute("Name");
            at.Value = this.name;
            node.Attributes.Append(at);

            XmlNode xBody;
            foreach (KeyValuePair<string, SpaceItem> body in this.bodies)
            {
                xBody = doc.CreateElement(body.Key);
                at = doc.CreateAttribute("Type");
                at.Value = body.Value.GetType().ToString();
                xBody.Attributes.Append(at);

                node.AppendChild(xBody);

                body.Value.ToXml(xBody);
            }
        }
	}
}
