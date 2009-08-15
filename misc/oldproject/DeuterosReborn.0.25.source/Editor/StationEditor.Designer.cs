/*
 * Created by SharpDevelop.
 * User: Luaan
 * Date: 4.6.2007
 * Time: 17:02
 * 
 */
namespace Deuteros.Editor
{
	partial class StationEditor
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		
		/// <summary>
		/// Disposes resources used by the control.
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
			this.lblStationName = new System.Windows.Forms.Label();
			this.edtStationName = new System.Windows.Forms.TextBox();
			this.btnSave = new System.Windows.Forms.Button();
			this.btnTransferTo = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// lblStationName
			// 
			this.lblStationName.Location = new System.Drawing.Point(3, 7);
			this.lblStationName.Name = "lblStationName";
			this.lblStationName.Size = new System.Drawing.Size(90, 15);
			this.lblStationName.TabIndex = 0;
			this.lblStationName.Text = "Name:";
			// 
			// edtStationName
			// 
			this.edtStationName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.edtStationName.Location = new System.Drawing.Point(99, 4);
			this.edtStationName.Name = "edtStationName";
			this.edtStationName.Size = new System.Drawing.Size(161, 20);
			this.edtStationName.TabIndex = 2;
			// 
			// btnSave
			// 
			this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnSave.Location = new System.Drawing.Point(185, 30);
			this.btnSave.Name = "btnSave";
			this.btnSave.Size = new System.Drawing.Size(75, 23);
			this.btnSave.TabIndex = 3;
			this.btnSave.Text = "save";
			this.btnSave.UseVisualStyleBackColor = true;
			this.btnSave.Click += new System.EventHandler(this.BtnSaveClick);
			// 
			// btnTransferTo
			// 
			this.btnTransferTo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnTransferTo.Location = new System.Drawing.Point(74, 30);
			this.btnTransferTo.Name = "btnTransferTo";
			this.btnTransferTo.Size = new System.Drawing.Size(105, 23);
			this.btnTransferTo.TabIndex = 4;
			this.btnTransferTo.Text = "transfer to station";
			this.btnTransferTo.UseVisualStyleBackColor = true;
			this.btnTransferTo.Click += new System.EventHandler(this.BtnTransferToClick);
			// 
			// StationEditor
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.Controls.Add(this.btnTransferTo);
			this.Controls.Add(this.btnSave);
			this.Controls.Add(this.edtStationName);
			this.Controls.Add(this.lblStationName);
			this.Name = "StationEditor";
			this.Size = new System.Drawing.Size(263, 58);
			this.ResumeLayout(false);
			this.PerformLayout();
		}
		private System.Windows.Forms.Button btnTransferTo;
		private System.Windows.Forms.Button btnSave;
		private System.Windows.Forms.TextBox edtStationName;
		private System.Windows.Forms.Label lblStationName;
	}
}
