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
	/// Description of WorkTeamBase.
	/// </summary>
	public class WorkTeamBase
	{
		public WorkTeamBase()
		{
		}
		
		protected string teamLeader = "";
		/// <summary>
		/// Gets or sets the name of team leader
		/// </summary>
		public string TeamLeader
		{
			get { return teamLeader; }
			set { teamLeader = value; }
		}
		
		protected int teamSize = 0;
		/// <summary>
		/// Gets or sets the team size
		/// </summary>
		public int TeamSize
		{
			get { return teamSize; }
			set { teamSize = value; }
		}
		
		/// <summary>
		/// Returns string representation of team rank
		/// </summary>
		/// <returns></returns>
		public virtual string GetTeamRank()
		{
			return "None";
		}
		
		/// <summary>
		/// Returns ordinal representation of team rank
		/// </summary>
		/// <returns></returns>
		public virtual int GetTeamRankOrdinal()
		{
			return 0;
		}
		
		public static WorkTeamBase InvokeCreate(string typ)
		{
			WorkTeamBase si = StructXml.CreateInstanceFromType(typ) as WorkTeamBase;
			
			if ( si == null ) return new WorkTeamBase();
			else return si;
		}
		
		public virtual void FromXml(XmlNode node)
		{
			this.teamLeader = node["TeamLeader"].InnerText;
			this.teamSize   = int.Parse(node["TeamSize"].InnerText);
		}
		
		public virtual void ToXml(XmlNode node)
		{
			XmlDocument doc = node.OwnerDocument;
			
			XmlAttribute at = doc.CreateAttribute("Type");
			at.Value = this.GetType().ToString();
			node.Attributes.Append(at);
			
			XmlElement el = doc.CreateElement("TeamLeader");
			el.InnerText = teamLeader;
			node.AppendChild(el);
			
			el = doc.CreateElement("TeamSize");
			el.InnerText = teamSize.ToString();
			node.AppendChild(el);
		}
	}
}
