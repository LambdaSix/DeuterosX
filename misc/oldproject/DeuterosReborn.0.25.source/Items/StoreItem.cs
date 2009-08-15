/*
 * Created by SharpDevelop.
 * User: Luaan
 * Date: 4.6.2007
 * Time: 11:55
 * 
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Xml;

using Deuteros.Teams;

namespace Deuteros.Items
{
	/// <summary>
	/// Description of StoreItem.
	/// </summary>
	public class StoreItem
	{
		public StoreItem()
		{
			this.resources = new List<FactoryResource>();
		}
		
		public StoreItem(string title): this()
		{
			this.title = title;
		}
		
		public virtual StoreItem Clone()
		{
			StoreItem it = new StoreItem(this.title);
			it.buildTime = buildTime;
			FactoryResource[] pom = new FactoryResource[this.resources.Count];
			this.resources.CopyTo(pom);
			
			it.resources = new List<FactoryResource>(pom);
			
			
			return it;
		}
		
		protected bool researched = false;
		/// <summary>
		/// Gets or sets whether the item is ready for production
		/// </summary>
		[Browsable(true)]
		[Description("Item is ready for production.")]
		public virtual bool Researched
		{
			get { return this.researched; }
			set { this.researched = value; }
		}
		
		protected bool canResearch = true;
		/// <summary>
		/// Gets or sets whether the item can be researched
		/// </summary>
		[Description("Item may be researched.")]
		public virtual bool CanResearch
		{
			get { return this.canResearch; }
			set { this.canResearch = value; }
		}
		
		protected string title = "Item";
		/// <summary>
		/// Gets item title
		/// </summary>
		[Description("Item title.")]
		public virtual string Title
		{
			get { return title; }
			set { title = value; }
		}
		
		protected string description = "";
		/// <summary>
		/// Gets/sets the item description
		/// </summary>
		//[Description("Item description as shown in the storage interface.")]
		[Browsable(false)]
		public virtual string Description			
		{
			get { return this.description; }
			set { this.description = value; }
		}
		
		[Editor(@"System.Windows.Forms.Design.StringCollectionEditor, 
		System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", 
		typeof(System.Drawing.Design.UITypeEditor))]
		[Description("Item description as shown in the storage interface.")]
		public virtual string[] DescriptionLines
		{
			get { return this.description.Split( new string[1] {"\r\n"}, StringSplitOptions.None); }
			set { this.description = string.Join("\r\n", value); }
		}
		
		protected int buildTime = 100;
		/// <summary>
		/// Gets item build time
		/// </summary>
		[Description("Specifies the build time in work units.")]
		public virtual int BuildTime
		{
			get { return buildTime; }
			set { buildTime = value; }
		}
		
		protected int buildRank = 0;
		/// <summary>
		/// Gets/sets the necessary team rank to build
		/// </summary>
		[Browsable(false)]
		public virtual int BuildRank
		{
			get { return this.buildRank; }
			set { this.buildRank = value; }
		}
		
		/// <summary>
		/// Gets or sets the necessary team rank to build using a ProductionTeamRank enum
		/// </summary>
		[Description("Specifies the lowest team rank that is allowed to build this item.")]
		[DisplayName("Build rank")]
		public virtual ProductionTeamRank BuildRankValue
		{
			get { return (ProductionTeamRank)this.buildRank; }
			set { this.buildRank = (int)value; }
		}
		
		protected int researchRank = 0;
		/// <summary>
		/// Gets/sets the necessary team rank to research
		/// </summary>
		[Browsable(false)]
		public virtual int ResearchRank
		{
			get { return this.researchRank; }
			set { this.researchRank = value; }
		}
		
		/// <summary>
		/// Gets or sets the necessary team rank to research using a ResearchTeamRank enum
		/// </summary>
		[Description("Specifies the lowest team rank that is allowed to research this item.")]
		[DisplayName("Research rank")]
		public virtual ResearchTeamRank ResearchRankValue
		{
			get { return (ResearchTeamRank)this.buildRank; }
			set { this.buildRank = (int)value; }
		}
		
		protected bool orbitalOnly = false;
		/// <summary>
		/// Gets/sets whether the item may be produced in orbit only or not
		/// </summary>
		[Description("Item may only be built on orbit.")]
		public virtual bool OrbitalOnly
		{
			get { return this.orbitalOnly; }
			set { this.orbitalOnly = value; }
		}
		
		protected ItemType type = ItemType.Unknown;
		/// <summary>
		/// Gets item type
		/// </summary>
		[Description("Item type.")]
		public virtual ItemType Type
		{
			get { return type; }
			set { type = value; }
		}
		
		protected string picture = "";
		/// <summary>
		/// Gets or sets the texture for item production blueprint
		/// </summary>
		[Description("Texture ID")]
		public virtual string Picture
		{
			get { return this.picture; }
			set { this.picture = value; }
		}
		
		protected Rectangle textureArea = Rectangle.Empty;
		/// <summary>
		/// Gets or sets the texture area for item production blueprint
		/// </summary>
		[Description("Texture area (only 1 stage of the picture)")]
		public virtual Rectangle TextureArea
		{
			get { return this.textureArea; }
			set { this.textureArea = value; }
		}
		
		protected List<FactoryResource> resources = null;
		/// <summary>
		/// List of all resources required to build this item.
		/// </summary>
		[Description("Resources required to build the item.")]
		public List<FactoryResource> Resources
		{
			get { return resources; }
		}

		protected int mass = 1;
		/// <summary>
		/// Item mass
		/// </summary>
		[Description("Item mass.")]
		public int Mass
		{
			get { return this.mass; }
			set { this.mass = value; }
		}
		
		protected int researchProgress = 0;
		/// <summary>
		/// Research progress. Doesn't save into template.
		/// </summary>
		[Description("Research progress. Doesn't save into TemplateItems, saves with SaveGame.")]
		public int ResearchProgress
		{
			get { return this.researchProgress; }
			set { this.researchProgress = value; }
		}
		
		protected int researchPointsNeeded = 0;
		/// <summary>
		/// Research points needed to finish the research.
		/// </summary>
		[Description("Total amount of research points required to research the item.")]
		public int ResearchPointsNeeded
		{
			get { return this.researchPointsNeeded; }
			set { this.researchPointsNeeded = value; }
		}
		
		public static StoreItem InvokeCreate(string typ)
		{
			StoreItem si = StructXml.CreateInstanceFromType(typ) as StoreItem;
			
			if ( si == null ) return new StoreItem();
			else return si;
		}
		
		public virtual void FromXml(XmlNode node)
		{
			this.title = node.SelectSingleNode("Title").InnerText;
			this.researched = bool.Parse(node.SelectSingleNode("Researched").InnerText);
			this.canResearch = bool.Parse(node.SelectSingleNode("CanResearch").InnerText);
			this.type = (ItemType)ItemType.Parse(typeof(ItemType), node.SelectSingleNode("ItemType").InnerText);
			this.description = node.SelectSingleNode("Description").InnerText;
			this.buildTime = int.Parse(node.SelectSingleNode("BuildTime").InnerText);
			this.buildRank = int.Parse(node.SelectSingleNode("BuildRank").InnerText);
			this.researchRank = int.Parse(node.SelectSingleNode("ResearchRank").InnerText);
			this.orbitalOnly = bool.Parse(node.SelectSingleNode("OrbitalOnly").InnerText);
			this.picture = node.SelectSingleNode("Picture").InnerText;
			
			XmlNode par;
			
			this.textureArea = StructXml.RectangleFromXml(node.SelectSingleNode("TextureArea"));
			
			par = node.SelectSingleNode("Resources");
			
			this.resources.Clear();
			for ( int i = 0; i < par.ChildNodes.Count; i ++ )
			{
				FactoryResource res = new FactoryResource();
				res.FromXml(par.ChildNodes[i]);
				
				this.resources.Add(res);
			}
			
			if ( node.SelectSingleNode("Mass") != null )
				this.mass = int.Parse(node.SelectSingleNode("Mass").InnerText);
			if ( node.SelectSingleNode("ResearchPointsNeeded") != null )
				this.researchPointsNeeded = int.Parse(node.SelectSingleNode("ResearchPointsNeeded").InnerText);
		}
		
		public virtual void ToXml(XmlNode node)
		{
			XmlDocument doc = node.OwnerDocument;
			
			XmlAttribute at = doc.CreateAttribute("Type");
			at.Value = this.GetType().ToString();
			node.Attributes.Append(at);
			
			XmlElement el = doc.CreateElement("Title");
			el.InnerText = title;
			node.AppendChild(el);
			
			el = doc.CreateElement("Researched");
			el.InnerText = researched.ToString();
			node.AppendChild(el);
			
			el = doc.CreateElement("CanResearch");
			el.InnerText = canResearch.ToString();
			node.AppendChild(el);

			el = doc.CreateElement("ItemType");
			el.InnerText = type.ToString();
			node.AppendChild(el);
			
			el = doc.CreateElement("Description");
			el.InnerText = this.description;
			node.AppendChild(el);
			
			el = doc.CreateElement("BuildTime");
			el.InnerText = this.buildTime.ToString();
			node.AppendChild(el);
			
			el = doc.CreateElement("BuildRank");
			el.InnerText = this.buildRank.ToString();
			node.AppendChild(el);
			
			el = doc.CreateElement("ResearchRank");
			el.InnerText = this.researchRank.ToString();
			node.AppendChild(el);
			
			el = doc.CreateElement("OrbitalOnly");
			el.InnerText = this.orbitalOnly.ToString();
			node.AppendChild(el);
			
			el = doc.CreateElement("Picture");
			el.InnerText = this.picture;
			node.AppendChild(el);
			
			el = doc.CreateElement("TextureArea");
			node.AppendChild(el);
			
			StructXml.RectangleToXml(this.textureArea, el);
			
			XmlElement el2;
			
			el = doc.CreateElement("Resources");
			node.AppendChild(el);
			
			for ( int i = 0; i < this.resources.Count; i ++ )
			{
				el2 = doc.CreateElement("Resource");
				el.AppendChild(el2);
				
				this.resources[i].ToXml(el2);
			}
			
			el = doc.CreateElement("Mass");
			el.InnerText = this.mass.ToString();
			node.AppendChild(el);
			
			el = doc.CreateElement("ResearchPointsNeeded");
			el.InnerText = this.researchPointsNeeded.ToString();
			node.AppendChild(el);
		}
	}
	
	public sealed class FactoryResource 
	{
		public string itemId = "";
		public int amount = 1;
		
		/// <summary>
		/// Item Id
		/// </summary>
		[Description("Id of this resource.")]
		public string ItemId
		{
			get { return this.itemId; }
			set { this.itemId = value; }
		}
		
		/// <summary>
		/// Item amount
		/// </summary>
		[Description("Amount of this item required")]
		public int Amount
		{
			get { return this.amount; }
			set { this.amount = value; }
		}
		
		public FactoryResource()
		{
			
		}
		
		public FactoryResource(string itemId, int amount): this()
		{
			this.itemId = itemId;
			this.amount = amount;
		}
		
		public void FromXml(XmlNode node)
		{
			this.itemId = node.Attributes["Id"].Value;
			this.amount = int.Parse(node.Attributes["Amount"].Value);
		}
		
		public void ToXml(XmlNode node)
		{
			XmlAttribute at;
			XmlDocument doc = node.OwnerDocument;
			
			at = doc.CreateAttribute("Id");
			at.Value = this.itemId;
			node.Attributes.Append(at);
			
			at = doc.CreateAttribute("Amount");
			at.Value = this.amount.ToString();
			node.Attributes.Append(at);
		}
	}
	
	public enum ItemType
	{
		Unknown = 0,
		/// <summary>
		/// Basic resources, like iron etc. Need no resources to be built
		/// </summary>
		Mineral,
		/// <summary>
		/// Special item like the Derrick
		/// </summary>
		Special,
		/// <summary>
		/// Used as a shuttle chassis
		/// </summary>
		ShuttleChassis,
		/// <summary>
		/// Used as a shuttle drive
		/// </summary>
		ShuttleDrive
	}
}
