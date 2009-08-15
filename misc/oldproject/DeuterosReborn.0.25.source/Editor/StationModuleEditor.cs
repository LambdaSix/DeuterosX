/*
 * Created by SharpDevelop.
 * User: Luaan
 * Date: 4.6.2007
 * Time: 18:08
 * 
 */

using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

using Deuteros.Stations;

namespace Deuteros.Editor
{
	/// <summary>
	/// Description of StationModuleEditor.
	/// </summary>
	public partial class StationModuleEditor : UserControl
	{
		/// <summary>
		/// StationModule
		/// </summary>
		protected StationModuleBase module = null;
		
		/// <summary>
		/// Creates a new instance of StationModuleEditor
		/// </summary>
		/// <param name="module">Associated module</param>
		public StationModuleEditor(StationModuleBase module)
		{
			InitializeComponent();
			
			this.module = module;
			
//			DataTable DT = new DataTable();
//			DT.Columns.Add("text", typeof(string));
//			DT.Columns.Add("val", typeof(GameScreen));
//			
//			string[] vals = GameScreen.GetNames( typeof(GameScreen) );
//			
//			for ( int i = 0; i < vals.Length; i++ )
//			{
//				DT.Rows.Add(vals[i], GameScreen.Parse(vals[i]));
//			}
			
			cbxNextScreen.DataSource = GameScreen.GetValues(typeof(GameScreen));
			//cbxNextScreen.SelectedItem = module.ButtonInfo.NextScreen;
			// TODO: Make editor work with new GuiButtonInfo
			
			edtButtonIndex.Text = module.ButtonIndex.ToString();
			edtButtonTooltip.Text = module.ButtonInfo.Tooltip;
			edtButtonTexture.Text = module.ButtonInfo.Picture;
		}
		
		void BtnSaveClick(object sender, EventArgs e)
		{
			int pom = 0;
			if ( int.TryParse(edtButtonIndex.Text, out pom) ) module.ButtonIndex = pom;
			module.ButtonInfo.Tooltip = edtButtonTooltip.Text;
			module.ButtonInfo.Picture = edtButtonTexture.Text;
			//module.ButtonInfo.NextScreen = (GameScreen)cbxNextScreen.SelectedValue;
		}
	}
}
