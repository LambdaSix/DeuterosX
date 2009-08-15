/*
 * Created by SharpDevelop.
 * User: Luaan
 * Date: 4.6.2007
 * Time: 11:05
 * 
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;

namespace Deuteros.Stations
{
	/// <summary>
	/// Base for all stations
	/// </summary>
	public class StationBase
	{
		public StationBase()
		{
			modules = new List<StationModuleBase>();
		}
		
		protected string title = "Station";
		/// <summary>
		/// Gets or sets the station title
		/// </summary>
		public string Title
		{
			get { return title; }
			set { title = value; }
		}

        private string location = "MilkyWay.Sol.Earth";
        /// <summary>
        /// Gets or sets the station location (Galaxy.SolarSystem.Body)
        /// </summary>
        public string Location
        {
            get { return location; }
            set { location = value; }
        }

        private string owner = "Player";
        /// <summary>
        /// Gets or sets the station owner
        /// </summary>
        public string Owner
        {
            get { return owner; }
            set { owner = value; }
        }

		protected List<StationModuleBase> modules = null;
		/// <summary>
		/// Gets station modules
		/// </summary>
		public List<StationModuleBase> Modules
		{
			get { return this.modules; }
		}
		
		public static StationBase InvokeCreate(string typ)
		{
			StationBase si = StructXml.CreateInstanceFromType(typ) as StationBase;
			
			if ( si == null ) return new StationBase();
			else return si;
		}
		
		public virtual void FromXml(XmlNode node)
		{
			this.title = node.Attributes["Title"].Value;
            if (node.Attributes["Location"] != null)
            {
                this.location = node.Attributes["Location"].Value;
            }
            if (node.Attributes["Owner"] != null)
            {
                this.owner = node.Attributes["Owner"].Value;
            }
			
			this.modules.Clear();
			foreach ( XmlNode xMod in node["Modules"].ChildNodes )
			{
				StationModuleBase mod = StationModuleBase.InvokeCreate(xMod.Name);
				
				mod.FromXml(xMod);
				this.MountModule(mod);
			}
		}
		
		public virtual void ToXml(XmlNode node)
		{
			XmlDocument doc = node.OwnerDocument;
			
			XmlAttribute at = doc.CreateAttribute("Type");
			at.Value = this.GetType().ToString();
			node.Attributes.Append(at);
			
			at = doc.CreateAttribute("Title");
			at.Value = title;
			node.Attributes.Append(at);

            at = doc.CreateAttribute("Location");
            at.Value = this.location;
            node.Attributes.Append(at);

            at = doc.CreateAttribute("Owner");
            at.Value = this.owner;
            node.Attributes.Append(at);

			XmlElement el = doc.CreateElement("Modules");
			node.AppendChild(el);
			
			XmlNode xModule;
			
			for ( int i = 0; i < modules.Count; i ++ )
			{
				if ( modules[i] != null )
				{
					xModule = doc.CreateElement(modules[i].GetType().ToString());
					modules[i].ToXml(xModule);
					el.AppendChild(xModule);
				}
			}
		}
		
		/// <summary>
		/// Mounts a new module to the station
		/// </summary>
		/// <param name="module">Station module to mount</param>
		/// <returns>Negative if error. If successful, returns the module index.</returns>
		public virtual int MountModule(StationModuleBase module)
		{
			if ( modules != null )
			{
				for ( int i = 0; i < modules.Count; i ++ )
				{
					if ( modules[i] != null && modules[i].GetType() == module.GetType() )
					{
						return -2;
					}
				}
				
				modules.Add(module);
				module.parent = this;
				module.OnMount();
				
				return 0;
			}
			else
			return -3;
		}
		
		/// <summary>
		/// Dismounts mounted module
		/// </summary>
		/// <param name="index">Index of module to dismount</param>
		/// <returns>True if successful.</returns>
		public virtual bool DismountModule(StationModuleBase module)
		{
			if ( modules != null )
			{
				module.OnDismount();
				
				return modules.Remove(module);
			}
			else
			return false;
		}
		
		/// <summary>
		/// Process turn
		/// </summary>
		public void OnNextTurn()
		{
			for (int i = 0; i < Modules.Count; i++)
			{
				if ( Modules[i] != null )
				{
					Modules[i].OnNextTurn();
				}
			}
		}
	}
}
