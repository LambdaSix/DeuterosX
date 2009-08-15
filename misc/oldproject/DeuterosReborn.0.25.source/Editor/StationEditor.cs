/*
 * Created by SharpDevelop.
 * User: Luaan
 * Date: 4.6.2007
 * Time: 17:02
 * 
 */

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using Deuteros.Stations;

namespace Deuteros.Editor
{
	/// <summary>
	/// Description of StationEditor.
	/// </summary>
	public partial class StationEditor : UserControl
	{
		/// <summary>
		/// Station
		/// </summary>
		protected StationBase station = null;
		/// <summary>
		/// Treenode
		/// </summary>
		protected TreeNode trn = null;
		
		/// <summary>
		/// Create a new StationEditor instance
		/// </summary>
		/// <param name="station">The associated station</param>
		/// <param name="trn">From this TreeNode</param>
		public StationEditor(StationBase station, TreeNode trn)
		{
			InitializeComponent();
			
			this.station = station;
			this.trn = trn;
			
			edtStationName.Text = station.Title;
		}
		
		void BtnSaveClick(object sender, EventArgs e)
		{
			station.Title = edtStationName.Text;
			trn.Text = edtStationName.Text;
			
			if ( GameEngine.Instance.CurrentStation == station )
			{
				GameEngine.Instance.SetCurrentStation(station);
			}
		}
		
		void BtnTransferToClick(object sender, EventArgs e)
		{
			GameEngine.Instance.CurrentScreen = GameScreen.Orbital;
			GameEngine.Instance.SetCurrentStation(station);
		}
	}
}
