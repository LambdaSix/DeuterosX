/*
 * Created by SharpDevelop.
 * User: Luaan
 * Date: 4.6.2007
 * Time: 16:07
 * 
 */
namespace Deuteros
{
	partial class frmEditor
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
            this.tbcMain = new System.Windows.Forms.TabControl();
            this.tpGlobal = new System.Windows.Forms.TabPage();
            this.btnGlobalDateTickSet = new System.Windows.Forms.Button();
            this.btnGlobalDateTickFetch = new System.Windows.Forms.Button();
            this.edtGlobalDateTick = new System.Windows.Forms.TextBox();
            this.lblGlobalDateTick = new System.Windows.Forms.Label();
            this.btnGlobalDateYearSet = new System.Windows.Forms.Button();
            this.btnGlobalDateYearFetch = new System.Windows.Forms.Button();
            this.edtGlobalDateYear = new System.Windows.Forms.TextBox();
            this.lblGlobalDateYear = new System.Windows.Forms.Label();
            this.tpStations = new System.Windows.Forms.TabPage();
            this.btnExportStations = new System.Windows.Forms.Button();
            this.btnImportStations = new System.Windows.Forms.Button();
            this.btnLoadStationsRT = new System.Windows.Forms.Button();
            this.grpStations = new System.Windows.Forms.GroupBox();
            this.pnlStationEdit = new System.Windows.Forms.Panel();
            this.lblWarning = new System.Windows.Forms.Label();
            this.trvStations = new System.Windows.Forms.TreeView();
            this.tpItemTemplates = new System.Windows.Forms.TabPage();
            this.btnExportItemTemplates = new System.Windows.Forms.Button();
            this.btnImportItemTemplates = new System.Windows.Forms.Button();
            this.btnLoadItemTemplatesRT = new System.Windows.Forms.Button();
            this.grpItemTemplates = new System.Windows.Forms.GroupBox();
            this.prgItemTemplate = new System.Windows.Forms.PropertyGrid();
            this.lblWarning2 = new System.Windows.Forms.Label();
            this.trvItemTemplates = new System.Windows.Forms.TreeView();
            this.tpUniverse = new System.Windows.Forms.TabPage();
            this.btnExportUniverse = new System.Windows.Forms.Button();
            this.btnImportUniverse = new System.Windows.Forms.Button();
            this.btnLoadUniverseRT = new System.Windows.Forms.Button();
            this.grpUniverse = new System.Windows.Forms.GroupBox();
            this.prgUniverse = new System.Windows.Forms.PropertyGrid();
            this.lblWarning3 = new System.Windows.Forms.Label();
            this.trvUniverse = new System.Windows.Forms.TreeView();
            this.tbcMain.SuspendLayout();
            this.tpGlobal.SuspendLayout();
            this.tpStations.SuspendLayout();
            this.grpStations.SuspendLayout();
            this.tpItemTemplates.SuspendLayout();
            this.grpItemTemplates.SuspendLayout();
            this.tpUniverse.SuspendLayout();
            this.grpUniverse.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbcMain
            // 
            this.tbcMain.Controls.Add(this.tpGlobal);
            this.tbcMain.Controls.Add(this.tpStations);
            this.tbcMain.Controls.Add(this.tpItemTemplates);
            this.tbcMain.Controls.Add(this.tpUniverse);
            this.tbcMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbcMain.Location = new System.Drawing.Point(0, 0);
            this.tbcMain.Multiline = true;
            this.tbcMain.Name = "tbcMain";
            this.tbcMain.SelectedIndex = 0;
            this.tbcMain.Size = new System.Drawing.Size(563, 400);
            this.tbcMain.TabIndex = 0;
            // 
            // tpGlobal
            // 
            this.tpGlobal.AutoScroll = true;
            this.tpGlobal.Controls.Add(this.btnGlobalDateTickSet);
            this.tpGlobal.Controls.Add(this.btnGlobalDateTickFetch);
            this.tpGlobal.Controls.Add(this.edtGlobalDateTick);
            this.tpGlobal.Controls.Add(this.lblGlobalDateTick);
            this.tpGlobal.Controls.Add(this.btnGlobalDateYearSet);
            this.tpGlobal.Controls.Add(this.btnGlobalDateYearFetch);
            this.tpGlobal.Controls.Add(this.edtGlobalDateYear);
            this.tpGlobal.Controls.Add(this.lblGlobalDateYear);
            this.tpGlobal.Location = new System.Drawing.Point(4, 22);
            this.tpGlobal.Margin = new System.Windows.Forms.Padding(1);
            this.tpGlobal.Name = "tpGlobal";
            this.tpGlobal.Size = new System.Drawing.Size(555, 374);
            this.tpGlobal.TabIndex = 0;
            this.tpGlobal.Text = "Global";
            // 
            // btnGlobalDateTickSet
            // 
            this.btnGlobalDateTickSet.Location = new System.Drawing.Point(288, 30);
            this.btnGlobalDateTickSet.Margin = new System.Windows.Forms.Padding(1);
            this.btnGlobalDateTickSet.Name = "btnGlobalDateTickSet";
            this.btnGlobalDateTickSet.Size = new System.Drawing.Size(45, 23);
            this.btnGlobalDateTickSet.TabIndex = 7;
            this.btnGlobalDateTickSet.Text = "set";
            this.btnGlobalDateTickSet.UseVisualStyleBackColor = true;
            this.btnGlobalDateTickSet.Click += new System.EventHandler(this.BtnGlobalDateTickSetClick);
            // 
            // btnGlobalDateTickFetch
            // 
            this.btnGlobalDateTickFetch.Location = new System.Drawing.Point(227, 30);
            this.btnGlobalDateTickFetch.Margin = new System.Windows.Forms.Padding(1);
            this.btnGlobalDateTickFetch.Name = "btnGlobalDateTickFetch";
            this.btnGlobalDateTickFetch.Size = new System.Drawing.Size(55, 23);
            this.btnGlobalDateTickFetch.TabIndex = 6;
            this.btnGlobalDateTickFetch.Text = "fetch";
            this.btnGlobalDateTickFetch.UseVisualStyleBackColor = true;
            this.btnGlobalDateTickFetch.Click += new System.EventHandler(this.BtnGlobalDateTickFetchClick);
            // 
            // edtGlobalDateTick
            // 
            this.edtGlobalDateTick.Location = new System.Drawing.Point(121, 32);
            this.edtGlobalDateTick.Margin = new System.Windows.Forms.Padding(1);
            this.edtGlobalDateTick.MaxLength = 3;
            this.edtGlobalDateTick.Name = "edtGlobalDateTick";
            this.edtGlobalDateTick.Size = new System.Drawing.Size(100, 20);
            this.edtGlobalDateTick.TabIndex = 5;
            // 
            // lblGlobalDateTick
            // 
            this.lblGlobalDateTick.Location = new System.Drawing.Point(8, 35);
            this.lblGlobalDateTick.Margin = new System.Windows.Forms.Padding(1);
            this.lblGlobalDateTick.Name = "lblGlobalDateTick";
            this.lblGlobalDateTick.Size = new System.Drawing.Size(107, 15);
            this.lblGlobalDateTick.TabIndex = 4;
            this.lblGlobalDateTick.Text = "Current cycle:";
            // 
            // btnGlobalDateYearSet
            // 
            this.btnGlobalDateYearSet.Location = new System.Drawing.Point(288, 5);
            this.btnGlobalDateYearSet.Margin = new System.Windows.Forms.Padding(1);
            this.btnGlobalDateYearSet.Name = "btnGlobalDateYearSet";
            this.btnGlobalDateYearSet.Size = new System.Drawing.Size(45, 23);
            this.btnGlobalDateYearSet.TabIndex = 3;
            this.btnGlobalDateYearSet.Text = "set";
            this.btnGlobalDateYearSet.UseVisualStyleBackColor = true;
            this.btnGlobalDateYearSet.Click += new System.EventHandler(this.BtnGlobalDateYearSetClick);
            // 
            // btnGlobalDateYearFetch
            // 
            this.btnGlobalDateYearFetch.Location = new System.Drawing.Point(227, 5);
            this.btnGlobalDateYearFetch.Margin = new System.Windows.Forms.Padding(1);
            this.btnGlobalDateYearFetch.Name = "btnGlobalDateYearFetch";
            this.btnGlobalDateYearFetch.Size = new System.Drawing.Size(55, 23);
            this.btnGlobalDateYearFetch.TabIndex = 2;
            this.btnGlobalDateYearFetch.Text = "fetch";
            this.btnGlobalDateYearFetch.UseVisualStyleBackColor = true;
            this.btnGlobalDateYearFetch.Click += new System.EventHandler(this.BtnGlobalDateYearFetchClick);
            // 
            // edtGlobalDateYear
            // 
            this.edtGlobalDateYear.Location = new System.Drawing.Point(121, 7);
            this.edtGlobalDateYear.Margin = new System.Windows.Forms.Padding(1);
            this.edtGlobalDateYear.MaxLength = 4;
            this.edtGlobalDateYear.Name = "edtGlobalDateYear";
            this.edtGlobalDateYear.Size = new System.Drawing.Size(100, 20);
            this.edtGlobalDateYear.TabIndex = 1;
            // 
            // lblGlobalDateYear
            // 
            this.lblGlobalDateYear.Location = new System.Drawing.Point(8, 10);
            this.lblGlobalDateYear.Margin = new System.Windows.Forms.Padding(1);
            this.lblGlobalDateYear.Name = "lblGlobalDateYear";
            this.lblGlobalDateYear.Size = new System.Drawing.Size(107, 15);
            this.lblGlobalDateYear.TabIndex = 0;
            this.lblGlobalDateYear.Text = "Current year:";
            // 
            // tpStations
            // 
            this.tpStations.Controls.Add(this.btnExportStations);
            this.tpStations.Controls.Add(this.btnImportStations);
            this.tpStations.Controls.Add(this.btnLoadStationsRT);
            this.tpStations.Controls.Add(this.grpStations);
            this.tpStations.Controls.Add(this.trvStations);
            this.tpStations.Location = new System.Drawing.Point(4, 22);
            this.tpStations.Name = "tpStations";
            this.tpStations.Padding = new System.Windows.Forms.Padding(3);
            this.tpStations.Size = new System.Drawing.Size(555, 374);
            this.tpStations.TabIndex = 1;
            this.tpStations.Text = "Stations & modules";
            this.tpStations.UseVisualStyleBackColor = true;
            // 
            // btnExportStations
            // 
            this.btnExportStations.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnExportStations.Location = new System.Drawing.Point(102, 343);
            this.btnExportStations.Name = "btnExportStations";
            this.btnExportStations.Size = new System.Drawing.Size(88, 23);
            this.btnExportStations.TabIndex = 5;
            this.btnExportStations.Text = "save to XML";
            this.btnExportStations.UseVisualStyleBackColor = true;
            this.btnExportStations.Click += new System.EventHandler(this.BtnExportStationsClick);
            // 
            // btnImportStations
            // 
            this.btnImportStations.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnImportStations.Location = new System.Drawing.Point(9, 343);
            this.btnImportStations.Name = "btnImportStations";
            this.btnImportStations.Size = new System.Drawing.Size(87, 23);
            this.btnImportStations.TabIndex = 4;
            this.btnImportStations.Text = "load from XML";
            this.btnImportStations.UseVisualStyleBackColor = true;
            this.btnImportStations.Click += new System.EventHandler(this.BtnImportStationsClick);
            // 
            // btnLoadStationsRT
            // 
            this.btnLoadStationsRT.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnLoadStationsRT.Location = new System.Drawing.Point(9, 314);
            this.btnLoadStationsRT.Name = "btnLoadStationsRT";
            this.btnLoadStationsRT.Size = new System.Drawing.Size(181, 23);
            this.btnLoadStationsRT.TabIndex = 2;
            this.btnLoadStationsRT.Text = "load from game";
            this.btnLoadStationsRT.UseVisualStyleBackColor = true;
            this.btnLoadStationsRT.Click += new System.EventHandler(this.BtnLoadStationsRTClick);
            // 
            // grpStations
            // 
            this.grpStations.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grpStations.Controls.Add(this.pnlStationEdit);
            this.grpStations.Controls.Add(this.lblWarning);
            this.grpStations.Location = new System.Drawing.Point(197, 7);
            this.grpStations.Name = "grpStations";
            this.grpStations.Size = new System.Drawing.Size(350, 359);
            this.grpStations.TabIndex = 1;
            this.grpStations.TabStop = false;
            this.grpStations.Text = "Properties";
            // 
            // pnlStationEdit
            // 
            this.pnlStationEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlStationEdit.Location = new System.Drawing.Point(7, 63);
            this.pnlStationEdit.Name = "pnlStationEdit";
            this.pnlStationEdit.Size = new System.Drawing.Size(337, 290);
            this.pnlStationEdit.TabIndex = 1;
            // 
            // lblWarning
            // 
            this.lblWarning.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblWarning.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblWarning.Location = new System.Drawing.Point(7, 16);
            this.lblWarning.Name = "lblWarning";
            this.lblWarning.Size = new System.Drawing.Size(337, 43);
            this.lblWarning.TabIndex = 0;
            this.lblWarning.Text = "Warning: any commited changes will be automatically loaded into game. Load from X" +
                "ML forces complete world rebuild in game.";
            // 
            // trvStations
            // 
            this.trvStations.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.trvStations.HideSelection = false;
            this.trvStations.Location = new System.Drawing.Point(9, 7);
            this.trvStations.Name = "trvStations";
            this.trvStations.Size = new System.Drawing.Size(181, 301);
            this.trvStations.TabIndex = 0;
            this.trvStations.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.TrvStationsAfterSelect);
            // 
            // tpItemTemplates
            // 
            this.tpItemTemplates.Controls.Add(this.btnExportItemTemplates);
            this.tpItemTemplates.Controls.Add(this.btnImportItemTemplates);
            this.tpItemTemplates.Controls.Add(this.btnLoadItemTemplatesRT);
            this.tpItemTemplates.Controls.Add(this.grpItemTemplates);
            this.tpItemTemplates.Controls.Add(this.trvItemTemplates);
            this.tpItemTemplates.Location = new System.Drawing.Point(4, 22);
            this.tpItemTemplates.Name = "tpItemTemplates";
            this.tpItemTemplates.Padding = new System.Windows.Forms.Padding(3);
            this.tpItemTemplates.Size = new System.Drawing.Size(555, 374);
            this.tpItemTemplates.TabIndex = 2;
            this.tpItemTemplates.Text = "Item templates";
            this.tpItemTemplates.UseVisualStyleBackColor = true;
            // 
            // btnExportItemTemplates
            // 
            this.btnExportItemTemplates.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnExportItemTemplates.Location = new System.Drawing.Point(102, 343);
            this.btnExportItemTemplates.Name = "btnExportItemTemplates";
            this.btnExportItemTemplates.Size = new System.Drawing.Size(88, 23);
            this.btnExportItemTemplates.TabIndex = 10;
            this.btnExportItemTemplates.Text = "save to XML";
            this.btnExportItemTemplates.UseVisualStyleBackColor = true;
            this.btnExportItemTemplates.Click += new System.EventHandler(this.BtnExportItemTemplatesClick);
            // 
            // btnImportItemTemplates
            // 
            this.btnImportItemTemplates.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnImportItemTemplates.Location = new System.Drawing.Point(9, 343);
            this.btnImportItemTemplates.Name = "btnImportItemTemplates";
            this.btnImportItemTemplates.Size = new System.Drawing.Size(87, 23);
            this.btnImportItemTemplates.TabIndex = 9;
            this.btnImportItemTemplates.Text = "load from XML";
            this.btnImportItemTemplates.UseVisualStyleBackColor = true;
            this.btnImportItemTemplates.Click += new System.EventHandler(this.BtnImportItemTemplatesClick);
            // 
            // btnLoadItemTemplatesRT
            // 
            this.btnLoadItemTemplatesRT.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnLoadItemTemplatesRT.Location = new System.Drawing.Point(9, 314);
            this.btnLoadItemTemplatesRT.Name = "btnLoadItemTemplatesRT";
            this.btnLoadItemTemplatesRT.Size = new System.Drawing.Size(181, 23);
            this.btnLoadItemTemplatesRT.TabIndex = 8;
            this.btnLoadItemTemplatesRT.Text = "load from game";
            this.btnLoadItemTemplatesRT.UseVisualStyleBackColor = true;
            this.btnLoadItemTemplatesRT.Click += new System.EventHandler(this.BtnLoadItemTemplatesRTClick);
            // 
            // grpItemTemplates
            // 
            this.grpItemTemplates.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grpItemTemplates.Controls.Add(this.prgItemTemplate);
            this.grpItemTemplates.Controls.Add(this.lblWarning2);
            this.grpItemTemplates.Location = new System.Drawing.Point(197, 7);
            this.grpItemTemplates.Name = "grpItemTemplates";
            this.grpItemTemplates.Size = new System.Drawing.Size(350, 359);
            this.grpItemTemplates.TabIndex = 7;
            this.grpItemTemplates.TabStop = false;
            this.grpItemTemplates.Text = "Properties";
            // 
            // prgItemTemplate
            // 
            this.prgItemTemplate.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.prgItemTemplate.Location = new System.Drawing.Point(7, 62);
            this.prgItemTemplate.Name = "prgItemTemplate";
            this.prgItemTemplate.PropertySort = System.Windows.Forms.PropertySort.Alphabetical;
            this.prgItemTemplate.Size = new System.Drawing.Size(337, 291);
            this.prgItemTemplate.TabIndex = 2;
            this.prgItemTemplate.ToolbarVisible = false;
            // 
            // lblWarning2
            // 
            this.lblWarning2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblWarning2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblWarning2.Location = new System.Drawing.Point(7, 16);
            this.lblWarning2.Name = "lblWarning2";
            this.lblWarning2.Size = new System.Drawing.Size(337, 43);
            this.lblWarning2.TabIndex = 0;
            this.lblWarning2.Text = "Warning: any commited changes will be automatically loaded into game. Load from X" +
                "ML forces complete world rebuild in game.";
            // 
            // trvItemTemplates
            // 
            this.trvItemTemplates.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.trvItemTemplates.HideSelection = false;
            this.trvItemTemplates.Location = new System.Drawing.Point(9, 7);
            this.trvItemTemplates.Name = "trvItemTemplates";
            this.trvItemTemplates.Size = new System.Drawing.Size(181, 301);
            this.trvItemTemplates.TabIndex = 6;
            this.trvItemTemplates.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.TrvItemTemplatesAfterSelect);
            // 
            // tpUniverse
            // 
            this.tpUniverse.Controls.Add(this.btnExportUniverse);
            this.tpUniverse.Controls.Add(this.btnImportUniverse);
            this.tpUniverse.Controls.Add(this.btnLoadUniverseRT);
            this.tpUniverse.Controls.Add(this.grpUniverse);
            this.tpUniverse.Controls.Add(this.trvUniverse);
            this.tpUniverse.Location = new System.Drawing.Point(4, 22);
            this.tpUniverse.Name = "tpUniverse";
            this.tpUniverse.Padding = new System.Windows.Forms.Padding(3);
            this.tpUniverse.Size = new System.Drawing.Size(555, 374);
            this.tpUniverse.TabIndex = 3;
            this.tpUniverse.Text = "Universe";
            this.tpUniverse.UseVisualStyleBackColor = true;
            // 
            // btnExportUniverse
            // 
            this.btnExportUniverse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnExportUniverse.Location = new System.Drawing.Point(102, 343);
            this.btnExportUniverse.Name = "btnExportUniverse";
            this.btnExportUniverse.Size = new System.Drawing.Size(88, 23);
            this.btnExportUniverse.TabIndex = 15;
            this.btnExportUniverse.Text = "save to XML";
            this.btnExportUniverse.UseVisualStyleBackColor = true;
            this.btnExportUniverse.Click += new System.EventHandler(this.btnExportUniverse_Click);
            // 
            // btnImportUniverse
            // 
            this.btnImportUniverse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnImportUniverse.Location = new System.Drawing.Point(9, 343);
            this.btnImportUniverse.Name = "btnImportUniverse";
            this.btnImportUniverse.Size = new System.Drawing.Size(87, 23);
            this.btnImportUniverse.TabIndex = 14;
            this.btnImportUniverse.Text = "load from XML";
            this.btnImportUniverse.UseVisualStyleBackColor = true;
            this.btnImportUniverse.Click += new System.EventHandler(this.btnImportUniverse_Click);
            // 
            // btnLoadUniverseRT
            // 
            this.btnLoadUniverseRT.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnLoadUniverseRT.Location = new System.Drawing.Point(9, 314);
            this.btnLoadUniverseRT.Name = "btnLoadUniverseRT";
            this.btnLoadUniverseRT.Size = new System.Drawing.Size(181, 23);
            this.btnLoadUniverseRT.TabIndex = 13;
            this.btnLoadUniverseRT.Text = "load from game";
            this.btnLoadUniverseRT.UseVisualStyleBackColor = true;
            this.btnLoadUniverseRT.Click += new System.EventHandler(this.btnLoadUniverseRT_Click);
            // 
            // grpUniverse
            // 
            this.grpUniverse.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grpUniverse.Controls.Add(this.prgUniverse);
            this.grpUniverse.Controls.Add(this.lblWarning3);
            this.grpUniverse.Location = new System.Drawing.Point(197, 7);
            this.grpUniverse.Name = "grpUniverse";
            this.grpUniverse.Size = new System.Drawing.Size(350, 359);
            this.grpUniverse.TabIndex = 12;
            this.grpUniverse.TabStop = false;
            this.grpUniverse.Text = "Properties";
            // 
            // prgUniverse
            // 
            this.prgUniverse.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.prgUniverse.Location = new System.Drawing.Point(7, 62);
            this.prgUniverse.Name = "prgUniverse";
            this.prgUniverse.PropertySort = System.Windows.Forms.PropertySort.Alphabetical;
            this.prgUniverse.Size = new System.Drawing.Size(337, 291);
            this.prgUniverse.TabIndex = 2;
            this.prgUniverse.ToolbarVisible = false;
            // 
            // lblWarning3
            // 
            this.lblWarning3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblWarning3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblWarning3.Location = new System.Drawing.Point(7, 16);
            this.lblWarning3.Name = "lblWarning3";
            this.lblWarning3.Size = new System.Drawing.Size(337, 43);
            this.lblWarning3.TabIndex = 0;
            this.lblWarning3.Text = "Warning: any commited changes will be automatically loaded into game. Load from X" +
                "ML forces complete world rebuild in game.";
            // 
            // trvUniverse
            // 
            this.trvUniverse.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.trvUniverse.HideSelection = false;
            this.trvUniverse.Location = new System.Drawing.Point(9, 7);
            this.trvUniverse.Name = "trvUniverse";
            this.trvUniverse.Size = new System.Drawing.Size(181, 301);
            this.trvUniverse.TabIndex = 11;
            this.trvUniverse.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.trvUniverse_AfterSelect);
            // 
            // frmEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(563, 400);
            this.Controls.Add(this.tbcMain);
            this.Name = "frmEditor";
            this.Text = "DR Runtime Editor - Deuteros Reborn";
            this.tbcMain.ResumeLayout(false);
            this.tpGlobal.ResumeLayout(false);
            this.tpGlobal.PerformLayout();
            this.tpStations.ResumeLayout(false);
            this.grpStations.ResumeLayout(false);
            this.tpItemTemplates.ResumeLayout(false);
            this.grpItemTemplates.ResumeLayout(false);
            this.tpUniverse.ResumeLayout(false);
            this.grpUniverse.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		private System.Windows.Forms.PropertyGrid prgItemTemplate;
		private System.Windows.Forms.TreeView trvItemTemplates;
		private System.Windows.Forms.Label lblWarning2;
		private System.Windows.Forms.GroupBox grpItemTemplates;
		private System.Windows.Forms.Button btnLoadItemTemplatesRT;
		private System.Windows.Forms.Button btnImportItemTemplates;
		private System.Windows.Forms.Button btnExportItemTemplates;
		private System.Windows.Forms.TabPage tpItemTemplates;
		private System.Windows.Forms.Label lblGlobalDateTick;
		private System.Windows.Forms.TextBox edtGlobalDateTick;
		private System.Windows.Forms.Button btnGlobalDateTickFetch;
		private System.Windows.Forms.Button btnGlobalDateTickSet;
		private System.Windows.Forms.Label lblGlobalDateYear;
		private System.Windows.Forms.TextBox edtGlobalDateYear;
		private System.Windows.Forms.Button btnGlobalDateYearFetch;
		private System.Windows.Forms.Button btnGlobalDateYearSet;
		private System.Windows.Forms.Label lblWarning;
		private System.Windows.Forms.Panel pnlStationEdit;
		private System.Windows.Forms.Button btnImportStations;
		private System.Windows.Forms.Button btnExportStations;
		private System.Windows.Forms.TreeView trvStations;
		private System.Windows.Forms.GroupBox grpStations;
		private System.Windows.Forms.Button btnLoadStationsRT;
		private System.Windows.Forms.TabPage tpStations;
		private System.Windows.Forms.TabPage tpGlobal;
		private System.Windows.Forms.TabControl tbcMain;
        private System.Windows.Forms.TabPage tpUniverse;
        private System.Windows.Forms.Button btnExportUniverse;
        private System.Windows.Forms.Button btnImportUniverse;
        private System.Windows.Forms.Button btnLoadUniverseRT;
        private System.Windows.Forms.GroupBox grpUniverse;
        private System.Windows.Forms.PropertyGrid prgUniverse;
        private System.Windows.Forms.Label lblWarning3;
        private System.Windows.Forms.TreeView trvUniverse;
	}
}
