/*
 * Created by SharpDevelop.
 * User: Luaan
 * Date: 6.7.2007
 * Time: 13:40
 * 
 */

using System;

namespace Deuteros.Teams
{
	/// <summary>
	/// Description of WorkTeamResearch.
	/// </summary>
	public class WorkTeamResearch: WorkTeamBase
	{
		public WorkTeamResearch(string teamLeader, int teamSize, ResearchTeamRank teamRank)
		{
			this.teamLeader = teamLeader;
			this.teamSize = teamSize;
			this.teamRank = teamRank;
		}
		
		protected ResearchTeamRank teamRank = ResearchTeamRank.Technician;
		/// <summary>
		/// Gets or sets the team rank
		/// </summary>
		public ResearchTeamRank TeamRank
		{
			get { return teamRank; }
			set { teamRank = value; }
		}
		
		public override string GetTeamRank()
		{
			return teamRank.ToString();
		}
		
		public override int GetTeamRankOrdinal()
		{
			return (int)teamRank;
		}
	}
	
	public enum ResearchTeamRank
	{
		None = 0,
		Technician = 1,
		SecondIDontKnow = 2,
		Professor = 3		
	}
}
