/*
 * Created by SharpDevelop.
 * User: Luaan
 * Date: 4.6.2007
 * Time: 11:10
 * 
 */

using System;
using System.Xml;

namespace Deuteros.Teams
{
	/// <summary>
	/// Description of WorkTeamProduction.
	/// </summary>
	public class WorkTeamProduction: WorkTeamBase
	{
		public WorkTeamProduction()
		{
			
		}
		
		public WorkTeamProduction(string teamLeader, int teamSize, ProductionTeamRank teamRank): this()
		{
			this.teamLeader = teamLeader;
			this.teamSize = teamSize;
			this.teamRank = teamRank;
		}
		
		protected ProductionTeamRank teamRank = ProductionTeamRank.Apprentice;
		/// <summary>
		/// Gets or sets the team rank
		/// </summary>
		public ProductionTeamRank TeamRank
		{
			get { return teamRank; }
			set { teamRank = value; }
		}
		
		/// <summary>
		/// Returns how much work can this team do in a cycle
		/// </summary>
		/// <returns>Number of build pieces</returns>
		public int GetProductionValue()
		{
			switch ( teamRank )
			{
				case ProductionTeamRank.Apprentice:
					{
						
						return TeamSize;
					}
				default:
					{
						
						return TeamSize;
					}
			}
		}
		
		public override string GetTeamRank()
		{
			return teamRank.ToString();
		}
		
		public override int GetTeamRankOrdinal()
		{
			return (int)teamRank;
		}
		
		public override void FromXml(XmlNode node)
		{
			base.FromXml(node);
			
			this.teamRank = (ProductionTeamRank)ProductionTeamRank.Parse(typeof(ProductionTeamRank), node["ProductionTeamRank"].InnerText);
		}
		
		public override void ToXml(System.Xml.XmlNode node)
		{
			base.ToXml(node);
			
			XmlDocument doc = node.OwnerDocument;
			
			XmlElement el = doc.CreateElement("ProductionTeamRank");
			el.InnerText = teamRank.ToString();
			node.AppendChild(el);
		}
	}
	
	public enum ProductionTeamRank
	{
		None = 0,
		Apprentice = 1
	}
}
