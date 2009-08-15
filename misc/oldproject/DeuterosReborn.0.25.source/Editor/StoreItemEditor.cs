/*
 * Created by SharpDevelop.
 * User: Luaan
 * Date: 6.7.2007
 * Time: 1:04
 * 
 */

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using Deuteros.Items;
using Deuteros.Teams;

namespace Deuteros.Editor
{
	/// <summary>
	/// Description of StoreItemEditor.
	/// </summary>
	public partial class StoreItemEditor : UserControl
	{
		/// <summary>
		/// Item
		/// </summary>
		protected StoreItem item = null;
		
		/// <summary>
		/// Treenode
		/// </summary>
		protected TreeNode trn = null;
		
		public StoreItemEditor(StoreItem item, TreeNode trn)
		{
			InitializeComponent();

			this.item = item;
			this.trn = trn;
			
			edtItemTitle.Text = this.item.Title;
			chbResearched.Checked = this.item.Researched;
			chbCanResearch.Checked = this.item.CanResearch;
			cbxItemType.DataSource = ItemType.GetValues(typeof(ItemType));
			cbxItemType.SelectedItem = this.item.Type;
			edtDescription.Text = this.item.Description;
			nudBuildTime.Value = this.item.BuildTime;
			cbxBuildRank.DataSource = ProductionTeamRank.GetValues(typeof(ProductionTeamRank));
			cbxBuildRank.SelectedItem = (ProductionTeamRank)this.item.BuildRank;
			cbxResearchRank.DataSource = ResearchTeamRank.GetValues(typeof(ResearchTeamRank));
			cbxResearchRank.SelectedItem = (ResearchTeamRank)this.item.ResearchRank;
			chbOrbitalOnly.Checked = this.item.OrbitalOnly;
			edtPicture.Text = this.item.Picture;
			edtTextureArea.Text = this.item.TextureArea.Left.ToString() + ";" + this.item.TextureArea.Top.ToString() + ";"
				+ this.item.TextureArea.Width.ToString() + ";" + this.item.TextureArea.Height.ToString();
//			this.title = node.SelectSingleNode("Title").InnerText;
//			this.researched = bool.Parse(node.SelectSingleNode("Researched").InnerText);
//			this.canResearch = bool.Parse(node.SelectSingleNode("CanResearch").InnerText);
//			this.type = (ItemType)ItemType.Parse(typeof(ItemType), node.SelectSingleNode("ItemType").InnerText);
//			this.description = node.SelectSingleNode("Description").InnerText;
//			this.buildTime = int.Parse(node.SelectSingleNode("BuildTime").InnerText);
//			this.buildRank = int.Parse(node.SelectSingleNode("BuildRank").InnerText);
//			this.researchRank = int.Parse(node.SelectSingleNode("ResearchRank").InnerText);
//			this.orbitalOnly = bool.Parse(node.SelectSingleNode("OrbitalOnly").InnerText);
//			this.picture = node.SelectSingleNode("Picture").InnerText;
//			
//			XmlNode par = node.SelectSingleNode("TextureArea");
//			
//			this.textureArea = new Rectangle(int.Parse(par.Attributes["Left"].Value),
//			                                 int.Parse(par.Attributes["Top"].Value),
//			                                 int.Parse(par.Attributes["Width"].Value),
//			                                 int.Parse(par.Attributes["Height"].Value));
//			
//			par = node.SelectSingleNode("Resources");
//			
//			this.resources.Clear();
//			for ( int i = 0; i < par.ChildNodes.Count; i ++ )
//			{
//				FactoryResource res = new FactoryResource();
//				res.FromXml(par.ChildNodes[i]);
//				
//				this.resources.Add(res);
//			}
//			
//			this.mass = int.Parse(node.SelectSingleNode("Mass").InnerText);
		}
		
		void BtnSaveClick(object sender, EventArgs e)
		{
			item.Title = edtItemTitle.Text;
		}
	}
}
