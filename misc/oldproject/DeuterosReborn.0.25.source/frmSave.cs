/*
 * Created by SharpDevelop.
 * User: Luaan
 * Date: 4.6.2007
 * Time: 20:55
 * 
 */

using System;
using System.Drawing;
using System.Windows.Forms;

namespace Deuteros
{
	/// <summary>
	/// Description of frmSave.
	/// </summary>
	public partial class frmSave : Form
	{
		/// <summary>
		/// Creates a new instance of frmSave
		/// </summary>
		public frmSave()
		{
			InitializeComponent();
		}
		
		void BtnSaveClick(object sender, EventArgs e)
		{
			GameEngine.Instance.SaveGame(edtFilename.Text, edtTitle.Text);
			
			Close();
		}
		
		void BtnCancelClick(object sender, EventArgs e)
		{
			Close();
		}
	}
}
