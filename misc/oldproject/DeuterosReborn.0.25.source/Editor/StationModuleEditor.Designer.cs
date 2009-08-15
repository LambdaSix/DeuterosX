/*
 * Created by SharpDevelop.
 * User: Luaan
 * Date: 4.6.2007
 * Time: 18:08
 * 
 */
namespace Deuteros.Editor
{
	partial class StationModuleEditor
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
			this.btnSave = new System.Windows.Forms.Button();
			this.edtButtonIndex = new System.Windows.Forms.TextBox();
			this.lblButtonIndex = new System.Windows.Forms.Label();
			this.edtButtonTooltip = new System.Windows.Forms.TextBox();
			this.lblButtonTooltip = new System.Windows.Forms.Label();
			this.edtButtonTexture = new System.Windows.Forms.TextBox();
			this.lblButtonTexture = new System.Windows.Forms.Label();
			this.lblNextScreen = new System.Windows.Forms.Label();
			this.cbxNextScreen = new System.Windows.Forms.ComboBox();
			this.SuspendLayout();
			// 
			// btnSave
			// 
			this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnSave.Location = new System.Drawing.Point(197, 101);
			this.btnSave.Name = "btnSave";
			this.btnSave.Size = new System.Drawing.Size(75, 23);
			this.btnSave.TabIndex = 6;
			this.btnSave.Text = "save";
			this.btnSave.UseVisualStyleBackColor = true;
			this.btnSave.Click += new System.EventHandler(this.BtnSaveClick);
			// 
			// edtButtonIndex
			// 
			this.edtButtonIndex.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.edtButtonIndex.Location = new System.Drawing.Point(100, 3);
			this.edtButtonIndex.Margin = new System.Windows.Forms.Padding(1);
			this.edtButtonIndex.Name = "edtButtonIndex";
			this.edtButtonIndex.Size = new System.Drawing.Size(172, 20);
			this.edtButtonIndex.TabIndex = 5;
			// 
			// lblButtonIndex
			// 
			this.lblButtonIndex.Location = new System.Drawing.Point(4, 6);
			this.lblButtonIndex.Margin = new System.Windows.Forms.Padding(1);
			this.lblButtonIndex.Name = "lblButtonIndex";
			this.lblButtonIndex.Size = new System.Drawing.Size(90, 15);
			this.lblButtonIndex.TabIndex = 4;
			this.lblButtonIndex.Text = "Button index:";
			// 
			// edtButtonTooltip
			// 
			this.edtButtonTooltip.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.edtButtonTooltip.Location = new System.Drawing.Point(100, 25);
			this.edtButtonTooltip.Margin = new System.Windows.Forms.Padding(1);
			this.edtButtonTooltip.Name = "edtButtonTooltip";
			this.edtButtonTooltip.Size = new System.Drawing.Size(172, 20);
			this.edtButtonTooltip.TabIndex = 8;
			// 
			// lblButtonTooltip
			// 
			this.lblButtonTooltip.Location = new System.Drawing.Point(4, 28);
			this.lblButtonTooltip.Margin = new System.Windows.Forms.Padding(1);
			this.lblButtonTooltip.Name = "lblButtonTooltip";
			this.lblButtonTooltip.Size = new System.Drawing.Size(90, 15);
			this.lblButtonTooltip.TabIndex = 7;
			this.lblButtonTooltip.Text = "Button tooltip:";
			// 
			// edtButtonTexture
			// 
			this.edtButtonTexture.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.edtButtonTexture.Location = new System.Drawing.Point(100, 47);
			this.edtButtonTexture.Margin = new System.Windows.Forms.Padding(1);
			this.edtButtonTexture.Name = "edtButtonTexture";
			this.edtButtonTexture.Size = new System.Drawing.Size(172, 20);
			this.edtButtonTexture.TabIndex = 10;
			// 
			// lblButtonTexture
			// 
			this.lblButtonTexture.Location = new System.Drawing.Point(4, 50);
			this.lblButtonTexture.Margin = new System.Windows.Forms.Padding(1);
			this.lblButtonTexture.Name = "lblButtonTexture";
			this.lblButtonTexture.Size = new System.Drawing.Size(90, 15);
			this.lblButtonTexture.TabIndex = 9;
			this.lblButtonTexture.Text = "Button texture:";
			// 
			// lblNextScreen
			// 
			this.lblNextScreen.Location = new System.Drawing.Point(4, 72);
			this.lblNextScreen.Margin = new System.Windows.Forms.Padding(1);
			this.lblNextScreen.Name = "lblNextScreen";
			this.lblNextScreen.Size = new System.Drawing.Size(90, 15);
			this.lblNextScreen.TabIndex = 11;
			this.lblNextScreen.Text = "Next screen:";
			// 
			// cbxNextScreen
			// 
			this.cbxNextScreen.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.cbxNextScreen.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbxNextScreen.FormattingEnabled = true;
			this.cbxNextScreen.Location = new System.Drawing.Point(100, 69);
			this.cbxNextScreen.Name = "cbxNextScreen";
			this.cbxNextScreen.Size = new System.Drawing.Size(172, 21);
			this.cbxNextScreen.TabIndex = 12;
			// 
			// StationModuleEditor
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.Controls.Add(this.cbxNextScreen);
			this.Controls.Add(this.lblNextScreen);
			this.Controls.Add(this.edtButtonTexture);
			this.Controls.Add(this.lblButtonTexture);
			this.Controls.Add(this.edtButtonTooltip);
			this.Controls.Add(this.lblButtonTooltip);
			this.Controls.Add(this.btnSave);
			this.Controls.Add(this.edtButtonIndex);
			this.Controls.Add(this.lblButtonIndex);
			this.Margin = new System.Windows.Forms.Padding(1);
			this.Name = "StationModuleEditor";
			this.Size = new System.Drawing.Size(275, 127);
			this.ResumeLayout(false);
			this.PerformLayout();
		}
		private System.Windows.Forms.ComboBox cbxNextScreen;
		private System.Windows.Forms.Label lblNextScreen;
		private System.Windows.Forms.Label lblButtonTexture;
		private System.Windows.Forms.TextBox edtButtonTexture;
		private System.Windows.Forms.Label lblButtonTooltip;
		private System.Windows.Forms.TextBox edtButtonTooltip;
		private System.Windows.Forms.Label lblButtonIndex;
		private System.Windows.Forms.TextBox edtButtonIndex;
		private System.Windows.Forms.Button btnSave;
	}
}
