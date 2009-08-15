/*
 * Created by SharpDevelop.
 * User: Luaan
 * Date: 4.6.2007
 * Time: 21:06
 * 
 */

using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Deuteros
{
	/// <summary>
	/// Description of frmLoad.
	/// </summary>
	public partial class frmLoad : Form
	{
		/// <summary>
		/// Creates a new instance of frmLoad
		/// </summary>
		public frmLoad()
		{
			InitializeComponent();
			
			cbxSaves.DataSource = Directory.GetFiles(".\\save\\");
		}
		
		void BtnLoadClick(object sender, EventArgs e)
		{
			GameEngine.Instance.LoadGame(System.IO.Path.GetFileNameWithoutExtension(cbxSaves.Text));
			
			Close();
		}
		
		void BtnCancelClick(object sender, EventArgs e)
		{
			Close();
		}
	}
}
