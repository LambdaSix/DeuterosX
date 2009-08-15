/*
 * Created by SharpDevelop.
 * User: Luaan
 * Date: 4.6.2007
 * Time: 20:55
 * 
 */
namespace Deuteros
{
	partial class frmSave
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
			this.lblFilename = new System.Windows.Forms.Label();
			this.edtFilename = new System.Windows.Forms.TextBox();
			this.edtTitle = new System.Windows.Forms.TextBox();
			this.lblTitle = new System.Windows.Forms.Label();
			this.btnCancel = new System.Windows.Forms.Button();
			this.btnSave = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// lblFilename
			// 
			this.lblFilename.Location = new System.Drawing.Point(13, 13);
			this.lblFilename.Name = "lblFilename";
			this.lblFilename.Size = new System.Drawing.Size(63, 16);
			this.lblFilename.TabIndex = 0;
			this.lblFilename.Text = "Filename:";
			// 
			// edtFilename
			// 
			this.edtFilename.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.edtFilename.Location = new System.Drawing.Point(82, 10);
			this.edtFilename.Name = "edtFilename";
			this.edtFilename.Size = new System.Drawing.Size(169, 20);
			this.edtFilename.TabIndex = 1;
			this.edtFilename.Text = "savegame01";
			// 
			// edtTitle
			// 
			this.edtTitle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.edtTitle.Location = new System.Drawing.Point(82, 36);
			this.edtTitle.Name = "edtTitle";
			this.edtTitle.Size = new System.Drawing.Size(169, 20);
			this.edtTitle.TabIndex = 3;
			this.edtTitle.Text = "Example of savegame";
			// 
			// lblTitle
			// 
			this.lblTitle.Location = new System.Drawing.Point(13, 39);
			this.lblTitle.Name = "lblTitle";
			this.lblTitle.Size = new System.Drawing.Size(63, 16);
			this.lblTitle.TabIndex = 2;
			this.lblTitle.Text = "Title:";
			// 
			// btnCancel
			// 
			this.btnCancel.Location = new System.Drawing.Point(176, 62);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(75, 23);
			this.btnCancel.TabIndex = 4;
			this.btnCancel.Text = "cancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnCancel.Click += new System.EventHandler(this.BtnCancelClick);
			// 
			// btnSave
			// 
			this.btnSave.Location = new System.Drawing.Point(95, 62);
			this.btnSave.Name = "btnSave";
			this.btnSave.Size = new System.Drawing.Size(75, 23);
			this.btnSave.TabIndex = 5;
			this.btnSave.Text = "save";
			this.btnSave.UseVisualStyleBackColor = true;
			this.btnSave.Click += new System.EventHandler(this.BtnSaveClick);
			// 
			// frmSave
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(263, 91);
			this.Controls.Add(this.btnSave);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.edtTitle);
			this.Controls.Add(this.lblTitle);
			this.Controls.Add(this.edtFilename);
			this.Controls.Add(this.lblFilename);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Name = "frmSave";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Save game";
			this.ResumeLayout(false);
			this.PerformLayout();
		}
		private System.Windows.Forms.Button btnSave;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Label lblTitle;
		private System.Windows.Forms.TextBox edtTitle;
		private System.Windows.Forms.TextBox edtFilename;
		private System.Windows.Forms.Label lblFilename;
	}
}
