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
	/// Description of Galaxy.
	/// </summary>
	public sealed class Galaxy
	{
		public Galaxy()
		{
            systems = new Dictionary<string, SolarSystem>();
		}

        private string name = "Unknown galaxy";
        /// <summary>
        /// Gets or sets the name of this galaxy
        /// </summary>
        [Description("Name of this galaxy")]
        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }

        private Dictionary<string, SolarSystem> systems = null;
        /// <summary>
        /// Gets the solar systems in this galaxy
        /// </summary>
        [Browsable(false)]
        public Dictionary<string, SolarSystem> Systems
        {
            get { return this.systems; }
        }

        public void FromXml(XmlNode node)
        {
            this.name = node.Attributes["Name"].Value;

            SolarSystem sol;
            systems.Clear();
            foreach ( XmlNode xSol in node.ChildNodes )
            {
                sol = SolarSystem.InvokeCreate(xSol.Attributes["Type"].Value);
                sol.FromXml(xSol);

                systems.Add(xSol.Name, sol);
            }
        }

        public void ToXml(XmlNode node)
        {
            XmlDocument doc = node.OwnerDocument;

            XmlAttribute at = doc.CreateAttribute("Name");
            at.Value = this.name;
            node.Attributes.Append(at);

            XmlNode xSol;
            foreach (KeyValuePair<string, SolarSystem> sol in this.systems)
            {
                xSol = doc.CreateElement(sol.Key);
                at = doc.CreateAttribute("Type");
                at.Value = sol.Value.GetType().ToString();
                xSol.Attributes.Append(at);

                node.AppendChild(xSol);

                sol.Value.ToXml(xSol);
            }
        }
	}
}
