/*
 * Created by SharpDevelop.
 * User: Luaan
 * Date: 4.6.2007
 * Time: 16:07
 * 
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

using Deuteros.Editor;
using Deuteros.Items;
using Deuteros.Stations;
using Deuteros.Universe;

namespace Deuteros
{
	/// <summary>
	/// Description of frmEditor.
	/// </summary>
	public partial class frmEditor : Form
	{
		/// <summary>
		/// Creates a new instance of frmEditor
		/// </summary>
		public frmEditor()
		{
			InitializeComponent();
		}
		
		void BtnLoadStationsRTClick(object sender, EventArgs e)
		{
			trvStations.Nodes.Clear();
			
			for ( int i = 0; i < GameEngine.Instance.StationList.Count; i ++ )
			{
				StationBase stat = GameEngine.Instance.StationList[i];
				if ( stat != null )
				{
					TreeNode trn = trvStations.Nodes.Add(stat.Title);
					trn.Tag = stat;
					
					for ( int j = 0; j < stat.Modules.Count; j ++ )
					{
						if ( stat.Modules[j] != null )
						{
							TreeNode trnMod = trn.Nodes.Add(stat.Modules[j].GetType().ToString());
							trnMod.Tag = stat.Modules[j];
						}
					}
				}
			}
		}
		
		void TrvStationsAfterSelect(object sender, TreeViewEventArgs e)
		{
			pnlStationEdit.Controls.Clear();
			
			if ( e.Node != null )
			{
				if ( e.Node.Tag is StationBase )
				{
					StationEditor sedt = new StationEditor( e.Node.Tag as StationBase, e.Node );
					
					pnlStationEdit.Controls.Add(sedt);
					sedt.Dock = DockStyle.Fill;
				}
				else
				if ( e.Node.Tag is StationModuleBase )
				{
					StationModuleEditor smedt = new StationModuleEditor( e.Node.Tag as StationModuleBase );
					
					pnlStationEdit.Controls.Add(smedt);
					smedt.Dock = DockStyle.Fill;
				}
			}
		}
		
		void BtnGlobalDateYearFetchClick(object sender, EventArgs e)
		{
			edtGlobalDateYear.Text = GameEngine.Instance.CurrentYear.ToString();
		}
		
		void BtnGlobalDateYearSetClick(object sender, EventArgs e)
		{
			int.TryParse(edtGlobalDateYear.Text, out GameEngine.Instance.CurrentYear);
		}
		
		void BtnGlobalDateTickFetchClick(object sender, EventArgs e)
		{
			edtGlobalDateTick.Text = GameEngine.Instance.CurrentCycle.ToString();
		}
		
		void BtnGlobalDateTickSetClick(object sender, EventArgs e)
		{
			int.TryParse(edtGlobalDateTick.Text, out GameEngine.Instance.CurrentCycle);
		}
		
		void BtnExportStationsClick(object sender, EventArgs e)
		{
			frmSave frm = new frmSave();
			frm.ShowDialog();
		}
		
		void BtnImportStationsClick(object sender, EventArgs e)
		{
			frmLoad frm = new frmLoad();
			frm.ShowDialog();
			
			BtnLoadStationsRTClick(null, EventArgs.Empty);
		}
		
		void BtnLoadItemTemplatesRTClick(object sender, EventArgs e)
		{
			trvItemTemplates.Nodes.Clear();
			
			foreach ( KeyValuePair<string, StoreItem> it in GameEngine.Instance.ItemTemplates )
			{
				TreeNode nod = new TreeNode();
				nod.Text = it.Key;
				nod.Tag = it.Value;
				
				trvItemTemplates.Nodes.Add(nod);
			}
		}
		
		void TrvItemTemplatesAfterSelect(object sender, TreeViewEventArgs e)
		{
//			pnlItemTemplateEdit.Controls.Clear();
//			
//			if ( e.Node != null )
//			{
//				if ( e.Node.Tag is StoreItem )
//				{
//					StoreItemEditor siedt = new StoreItemEditor( e.Node.Tag as StoreItem, e.Node );
//					
//					pnlItemTemplateEdit.Controls.Add(siedt);
//					siedt.Dock = DockStyle.Fill;
//				}
//			}
			
			prgItemTemplate.SelectedObject = e.Node.Tag;
		}
		
		void BtnImportItemTemplatesClick(object sender, EventArgs e)
		{
			OpenFileDialog ofd = new OpenFileDialog();
			ofd.Filter = "Item template file (*.fdrp) | *.fdrp";
			
			if ( ofd.ShowDialog() == DialogResult.OK )
			{
				GameEngine.Instance.LoadItemTemplates(System.IO.Path.ChangeExtension(ofd.FileName, null));
				
				BtnLoadItemTemplatesRTClick(null, EventArgs.Empty);
			}
		}
		
		void BtnExportItemTemplatesClick(object sender, EventArgs e)
		{
			SaveFileDialog sfd = new SaveFileDialog();
			sfd.Filter = "Item template file (*.fdrp) | *.fdrp";
			
			if ( sfd.ShowDialog() == DialogResult.OK )
			{
				GameEngine.Instance.SaveItemTemplates(System.IO.Path.ChangeExtension(sfd.FileName, null));
			}
		}

        private void btnLoadUniverseRT_Click(object sender, EventArgs e)
        {
            trvUniverse.Nodes.Clear();
            TreeNode universe = new TreeNode("Universe");
            trvUniverse.Nodes.Add(universe);

            foreach (KeyValuePair<string, Galaxy> gal in GameEngine.Instance.Universe)
            {
                TreeNode nGal = new TreeNode(gal.Key);
                nGal.Tag = gal.Value;

                foreach (KeyValuePair<string, SolarSystem> sol in gal.Value.Systems)
                {
                    TreeNode nSol = new TreeNode(sol.Key);
                    nSol.Tag = sol.Value;

                    foreach (KeyValuePair<string, SpaceItem> body in sol.Value.Bodies)
                    {
                        TreeNode nBody = new TreeNode(body.Key);
                        nBody.Tag = body.Value;
                        nSol.Nodes.Add(nBody);
                    }

                    nGal.Nodes.Add(nSol);
                }

                universe.Nodes.Add(nGal);
            }

            universe.Expand();
        }

        private void trvUniverse_AfterSelect(object sender, TreeViewEventArgs e)
        {
            prgUniverse.SelectedObject = e.Node.Tag;
        }

        private void btnImportUniverse_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Universe config file (*.fdru) | *.fdru";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                GameEngine.Instance.LoadUniverse(System.IO.Path.ChangeExtension(ofd.FileName, null));

                btnLoadUniverseRT_Click(null, EventArgs.Empty);
            }
        }

        private void btnExportUniverse_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Universe config file (*.fdru) | *.fdru";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                GameEngine.Instance.SaveUniverse(System.IO.Path.ChangeExtension(sfd.FileName, null));
            }
        }
	}
}
