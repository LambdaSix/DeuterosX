/*
 * Created by SharpDevelop.
 * User: Luaan
 * Date: 6.7.2007
 * Time: 1:04
 * 
 */
namespace Deuteros.Editor
{
	partial class StoreItemEditor
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
			this.edtItemTitle = new System.Windows.Forms.TextBox();
			this.lblItemTitle = new System.Windows.Forms.Label();
			this.chbResearched = new System.Windows.Forms.CheckBox();
			this.chbCanResearch = new System.Windows.Forms.CheckBox();
			this.lblItemType = new System.Windows.Forms.Label();
			this.cbxItemType = new System.Windows.Forms.ComboBox();
			this.edtDescription = new System.Windows.Forms.TextBox();
			this.lblDescription = new System.Windows.Forms.Label();
			this.nudBuildTime = new System.Windows.Forms.NumericUpDown();
			this.lblBuildTime = new System.Windows.Forms.Label();
			this.cbxBuildRank = new System.Windows.Forms.ComboBox();
			this.lblBuildRank = new System.Windows.Forms.Label();
			this.cbxResearchRank = new System.Windows.Forms.ComboBox();
			this.lblResearchRank = new System.Windows.Forms.Label();
			this.chbOrbitalOnly = new System.Windows.Forms.CheckBox();
			this.edtPicture = new System.Windows.Forms.TextBox();
			this.lblPicture = new System.Windows.Forms.Label();
			this.edtTextureArea = new System.Windows.Forms.TextBox();
			this.lblTextureArea = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.nudBuildTime)).BeginInit();
			this.SuspendLayout();
			// 
			// btnSave
			// 
			this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnSave.Location = new System.Drawing.Point(190, 314);
			this.btnSave.Name = "btnSave";
			this.btnSave.Size = new System.Drawing.Size(75, 23);
			this.btnSave.TabIndex = 7;
			this.btnSave.Text = "save";
			this.btnSave.UseVisualStyleBackColor = true;
			this.btnSave.Click += new System.EventHandler(this.BtnSaveClick);
			// 
			// edtItemTitle
			// 
			this.edtItemTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.edtItemTitle.Location = new System.Drawing.Point(99, 3);
			this.edtItemTitle.Margin = new System.Windows.Forms.Padding(1);
			this.edtItemTitle.Name = "edtItemTitle";
			this.edtItemTitle.Size = new System.Drawing.Size(166, 20);
			this.edtItemTitle.TabIndex = 6;
			// 
			// lblItemTitle
			// 
			this.lblItemTitle.Location = new System.Drawing.Point(3, 6);
			this.lblItemTitle.Margin = new System.Windows.Forms.Padding(1);
			this.lblItemTitle.Name = "lblItemTitle";
			this.lblItemTitle.Size = new System.Drawing.Size(90, 15);
			this.lblItemTitle.TabIndex = 5;
			this.lblItemTitle.Text = "Title:";
			// 
			// chbResearched
			// 
			this.chbResearched.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.chbResearched.Location = new System.Drawing.Point(3, 25);
			this.chbResearched.Margin = new System.Windows.Forms.Padding(1);
			this.chbResearched.Name = "chbResearched";
			this.chbResearched.Size = new System.Drawing.Size(110, 17);
			this.chbResearched.TabIndex = 8;
			this.chbResearched.Text = "Researched";
			this.chbResearched.UseVisualStyleBackColor = true;
			// 
			// chbCanResearch
			// 
			this.chbCanResearch.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.chbCanResearch.Location = new System.Drawing.Point(3, 44);
			this.chbCanResearch.Margin = new System.Windows.Forms.Padding(1);
			this.chbCanResearch.Name = "chbCanResearch";
			this.chbCanResearch.Size = new System.Drawing.Size(110, 17);
			this.chbCanResearch.TabIndex = 9;
			this.chbCanResearch.Text = "Researchable";
			this.chbCanResearch.UseVisualStyleBackColor = true;
			// 
			// lblItemType
			// 
			this.lblItemType.Location = new System.Drawing.Point(3, 63);
			this.lblItemType.Margin = new System.Windows.Forms.Padding(1);
			this.lblItemType.Name = "lblItemType";
			this.lblItemType.Size = new System.Drawing.Size(90, 15);
			this.lblItemType.TabIndex = 10;
			this.lblItemType.Text = "Item type:";
			// 
			// cbxItemType
			// 
			this.cbxItemType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.cbxItemType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbxItemType.FormattingEnabled = true;
			this.cbxItemType.Location = new System.Drawing.Point(99, 60);
			this.cbxItemType.Margin = new System.Windows.Forms.Padding(1);
			this.cbxItemType.Name = "cbxItemType";
			this.cbxItemType.Size = new System.Drawing.Size(166, 21);
			this.cbxItemType.TabIndex = 11;
			// 
			// edtDescription
			// 
			this.edtDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.edtDescription.Location = new System.Drawing.Point(99, 83);
			this.edtDescription.Margin = new System.Windows.Forms.Padding(1);
			this.edtDescription.Multiline = true;
			this.edtDescription.Name = "edtDescription";
			this.edtDescription.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.edtDescription.Size = new System.Drawing.Size(166, 77);
			this.edtDescription.TabIndex = 13;
			// 
			// lblDescription
			// 
			this.lblDescription.Location = new System.Drawing.Point(3, 87);
			this.lblDescription.Margin = new System.Windows.Forms.Padding(1);
			this.lblDescription.Name = "lblDescription";
			this.lblDescription.Size = new System.Drawing.Size(90, 15);
			this.lblDescription.TabIndex = 12;
			this.lblDescription.Text = "Description:";
			// 
			// nudBuildTime
			// 
			this.nudBuildTime.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.nudBuildTime.Increment = new decimal(new int[] {
									100,
									0,
									0,
									0});
			this.nudBuildTime.Location = new System.Drawing.Point(99, 162);
			this.nudBuildTime.Margin = new System.Windows.Forms.Padding(1);
			this.nudBuildTime.Maximum = new decimal(new int[] {
									50000,
									0,
									0,
									0});
			this.nudBuildTime.Minimum = new decimal(new int[] {
									50000,
									0,
									0,
									-2147483648});
			this.nudBuildTime.Name = "nudBuildTime";
			this.nudBuildTime.Size = new System.Drawing.Size(166, 20);
			this.nudBuildTime.TabIndex = 14;
			this.nudBuildTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.nudBuildTime.ThousandsSeparator = true;
			// 
			// lblBuildTime
			// 
			this.lblBuildTime.Location = new System.Drawing.Point(3, 164);
			this.lblBuildTime.Margin = new System.Windows.Forms.Padding(1);
			this.lblBuildTime.Name = "lblBuildTime";
			this.lblBuildTime.Size = new System.Drawing.Size(90, 15);
			this.lblBuildTime.TabIndex = 15;
			this.lblBuildTime.Text = "Build time:";
			// 
			// cbxBuildRank
			// 
			this.cbxBuildRank.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.cbxBuildRank.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbxBuildRank.FormattingEnabled = true;
			this.cbxBuildRank.Location = new System.Drawing.Point(99, 184);
			this.cbxBuildRank.Margin = new System.Windows.Forms.Padding(1);
			this.cbxBuildRank.Name = "cbxBuildRank";
			this.cbxBuildRank.Size = new System.Drawing.Size(166, 21);
			this.cbxBuildRank.TabIndex = 17;
			// 
			// lblBuildRank
			// 
			this.lblBuildRank.Location = new System.Drawing.Point(3, 187);
			this.lblBuildRank.Margin = new System.Windows.Forms.Padding(1);
			this.lblBuildRank.Name = "lblBuildRank";
			this.lblBuildRank.Size = new System.Drawing.Size(90, 15);
			this.lblBuildRank.TabIndex = 16;
			this.lblBuildRank.Text = "Build rank:";
			// 
			// cbxResearchRank
			// 
			this.cbxResearchRank.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.cbxResearchRank.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbxResearchRank.FormattingEnabled = true;
			this.cbxResearchRank.Location = new System.Drawing.Point(99, 207);
			this.cbxResearchRank.Margin = new System.Windows.Forms.Padding(1);
			this.cbxResearchRank.Name = "cbxResearchRank";
			this.cbxResearchRank.Size = new System.Drawing.Size(166, 21);
			this.cbxResearchRank.TabIndex = 19;
			// 
			// lblResearchRank
			// 
			this.lblResearchRank.Location = new System.Drawing.Point(3, 210);
			this.lblResearchRank.Margin = new System.Windows.Forms.Padding(1);
			this.lblResearchRank.Name = "lblResearchRank";
			this.lblResearchRank.Size = new System.Drawing.Size(90, 15);
			this.lblResearchRank.TabIndex = 18;
			this.lblResearchRank.Text = "Research rank:";
			// 
			// chbOrbitalOnly
			// 
			this.chbOrbitalOnly.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.chbOrbitalOnly.Location = new System.Drawing.Point(3, 230);
			this.chbOrbitalOnly.Margin = new System.Windows.Forms.Padding(1);
			this.chbOrbitalOnly.Name = "chbOrbitalOnly";
			this.chbOrbitalOnly.Size = new System.Drawing.Size(110, 17);
			this.chbOrbitalOnly.TabIndex = 20;
			this.chbOrbitalOnly.Text = "Orbital only";
			this.chbOrbitalOnly.UseVisualStyleBackColor = true;
			// 
			// edtPicture
			// 
			this.edtPicture.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.edtPicture.Location = new System.Drawing.Point(99, 249);
			this.edtPicture.Margin = new System.Windows.Forms.Padding(1);
			this.edtPicture.Name = "edtPicture";
			this.edtPicture.Size = new System.Drawing.Size(166, 20);
			this.edtPicture.TabIndex = 22;
			// 
			// lblPicture
			// 
			this.lblPicture.Location = new System.Drawing.Point(3, 252);
			this.lblPicture.Margin = new System.Windows.Forms.Padding(1);
			this.lblPicture.Name = "lblPicture";
			this.lblPicture.Size = new System.Drawing.Size(90, 15);
			this.lblPicture.TabIndex = 21;
			this.lblPicture.Text = "Texture ID:";
			// 
			// edtTextureArea
			// 
			this.edtTextureArea.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.edtTextureArea.Location = new System.Drawing.Point(99, 271);
			this.edtTextureArea.Margin = new System.Windows.Forms.Padding(1);
			this.edtTextureArea.Name = "edtTextureArea";
			this.edtTextureArea.Size = new System.Drawing.Size(166, 20);
			this.edtTextureArea.TabIndex = 24;
			// 
			// lblTextureArea
			// 
			this.lblTextureArea.Location = new System.Drawing.Point(3, 274);
			this.lblTextureArea.Margin = new System.Windows.Forms.Padding(1);
			this.lblTextureArea.Name = "lblTextureArea";
			this.lblTextureArea.Size = new System.Drawing.Size(90, 15);
			this.lblTextureArea.TabIndex = 23;
			this.lblTextureArea.Text = "Texture area:";
			// 
			// StoreItemEditor
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.Controls.Add(this.edtTextureArea);
			this.Controls.Add(this.lblTextureArea);
			this.Controls.Add(this.edtPicture);
			this.Controls.Add(this.lblPicture);
			this.Controls.Add(this.chbOrbitalOnly);
			this.Controls.Add(this.cbxResearchRank);
			this.Controls.Add(this.lblResearchRank);
			this.Controls.Add(this.cbxBuildRank);
			this.Controls.Add(this.lblBuildRank);
			this.Controls.Add(this.lblBuildTime);
			this.Controls.Add(this.nudBuildTime);
			this.Controls.Add(this.edtDescription);
			this.Controls.Add(this.lblDescription);
			this.Controls.Add(this.cbxItemType);
			this.Controls.Add(this.lblItemType);
			this.Controls.Add(this.chbCanResearch);
			this.Controls.Add(this.chbResearched);
			this.Controls.Add(this.btnSave);
			this.Controls.Add(this.edtItemTitle);
			this.Controls.Add(this.lblItemTitle);
			this.Margin = new System.Windows.Forms.Padding(1);
			this.Name = "StoreItemEditor";
			this.Size = new System.Drawing.Size(268, 340);
			((System.ComponentModel.ISupportInitialize)(this.nudBuildTime)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();
		}
		private System.Windows.Forms.Label lblTextureArea;
		private System.Windows.Forms.TextBox edtTextureArea;
		private System.Windows.Forms.TextBox edtPicture;
		private System.Windows.Forms.Label lblPicture;
		private System.Windows.Forms.CheckBox chbOrbitalOnly;
		private System.Windows.Forms.Label lblResearchRank;
		private System.Windows.Forms.ComboBox cbxResearchRank;
		private System.Windows.Forms.Label lblBuildRank;
		private System.Windows.Forms.ComboBox cbxBuildRank;
		private System.Windows.Forms.Label lblBuildTime;
		private System.Windows.Forms.NumericUpDown nudBuildTime;
		private System.Windows.Forms.Label lblDescription;
		private System.Windows.Forms.TextBox edtDescription;
		private System.Windows.Forms.ComboBox cbxItemType;
		private System.Windows.Forms.Label lblItemType;
		private System.Windows.Forms.CheckBox chbCanResearch;
		private System.Windows.Forms.CheckBox chbResearched;
		private System.Windows.Forms.TextBox edtItemTitle;
		private System.Windows.Forms.Label lblItemTitle;
		private System.Windows.Forms.Button btnSave;
	}
}
