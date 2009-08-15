/*
 * Created by SharpDevelop.
 * User: Luaan
 * Date: 4.6.2007
 * Time: 21:06
 * 
 */
namespace Deuteros
{
	partial class frmLoad
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		
		/// <summary>
		/// Disposes resources used by the form.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent()
		{
			this.btnLoad = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.lblFilename = new System.Windows.Forms.Label();
			this.cbxSaves = new System.Windows.Forms.ComboBox();
			this.SuspendLayout();
			// 
			// btnLoad
			// 
			this.btnLoad.Location = new System.Drawing.Point(95, 38);
			this.btnLoad.Name = "btnLoad";
			this.btnLoad.Size = new System.Drawing.Size(75, 23);
			this.btnLoad.TabIndex = 7;
			this.btnLoad.Text = "load";
			this.btnLoad.UseVisualStyleBackColor = true;
			this.btnLoad.Click += new System.EventHandler(this.BtnLoadClick);
			// 
			// btnCancel
			// 
			this.btnCancel.Location = new System.Drawing.Point(176, 38);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(75, 23);
			this.btnCancel.TabIndex = 6;
			this.btnCancel.Text = "cancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnCancel.Click += new System.EventHandler(this.BtnCancelClick);
			// 
			// lblFilename
			// 
			this.lblFilename.Location = new System.Drawing.Point(12, 15);
			this.lblFilename.Name = "lblFilename";
			this.lblFilename.Size = new System.Drawing.Size(63, 16);
			this.lblFilename.TabIndex = 8;
			this.lblFilename.Text = "Filename:";
			// 
			// cbxSaves
			// 
			this.cbxSaves.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbxSaves.FormattingEnabled = true;
			this.cbxSaves.Location = new System.Drawing.Point(81, 12);
			this.cbxSaves.Name = "cbxSaves";
			this.cbxSaves.Size = new System.Drawing.Size(170, 21);
			this.cbxSaves.TabIndex = 9;
			// 
			// frmLoad
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(263, 66);
			this.Controls.Add(this.cbxSaves);
			this.Controls.Add(this.lblFilename);
			this.Controls.Add(this.btnLoad);
			this.Controls.Add(this.btnCancel);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Name = "frmLoad";
			this.Text = "frmLoad";
			this.ResumeLayout(false);
		}
		private System.Windows.Forms.ComboBox cbxSaves;
		private System.Windows.Forms.Label lblFilename;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Button btnLoad;
	}
}
