namespace Football_Stats.Forms
{
	partial class MainForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.YearsLbl = new System.Windows.Forms.Label();
			this.GraphicsPB = new System.Windows.Forms.PictureBox();
			this.GraphsPanel = new System.Windows.Forms.Panel();
			this.ClearBtn = new System.Windows.Forms.Button();
			this.ChangeViewBtn = new System.Windows.Forms.Button();
			this.SeasonYearCMB = new System.Windows.Forms.ComboBox();
			this.YearsLbl2 = new System.Windows.Forms.Label();
			this.CountryCMB = new System.Windows.Forms.ComboBox();
			this.CriteriaCMB = new System.Windows.Forms.ComboBox();
			this.label1 = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.GraphicsPB)).BeginInit();
			this.SuspendLayout();
			// 
			// YearsLbl
			// 
			this.YearsLbl.AutoSize = true;
			this.YearsLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.YearsLbl.Location = new System.Drawing.Point(290, 718);
			this.YearsLbl.Name = "YearsLbl";
			this.YearsLbl.Size = new System.Drawing.Size(50, 16);
			this.YearsLbl.TabIndex = 1;
			this.YearsLbl.Text = "232323";
			// 
			// GraphicsPB
			// 
			this.GraphicsPB.BackColor = System.Drawing.SystemColors.ButtonHighlight;
			this.GraphicsPB.Location = new System.Drawing.Point(270, 2);
			this.GraphicsPB.Name = "GraphicsPB";
			this.GraphicsPB.Size = new System.Drawing.Size(764, 714);
			this.GraphicsPB.TabIndex = 2;
			this.GraphicsPB.TabStop = false;
			// 
			// GraphsPanel
			// 
			this.GraphsPanel.AutoScroll = true;
			this.GraphsPanel.Location = new System.Drawing.Point(240, 15);
			this.GraphsPanel.Name = "GraphsPanel";
			this.GraphsPanel.Size = new System.Drawing.Size(801, 703);
			this.GraphsPanel.TabIndex = 3;
			this.GraphsPanel.Scroll += new System.Windows.Forms.ScrollEventHandler(this.GraphsPanel_Scroll);
			// 
			// ClearBtn
			// 
			this.ClearBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.ClearBtn.Location = new System.Drawing.Point(23, 540);
			this.ClearBtn.Name = "ClearBtn";
			this.ClearBtn.Size = new System.Drawing.Size(194, 36);
			this.ClearBtn.TabIndex = 4;
			this.ClearBtn.Text = "Clear all";
			this.ClearBtn.UseVisualStyleBackColor = true;
			this.ClearBtn.Click += new System.EventHandler(this.ClearBtn_Click);
			// 
			// ChangeViewBtn
			// 
			this.ChangeViewBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.ChangeViewBtn.Location = new System.Drawing.Point(23, 704);
			this.ChangeViewBtn.Name = "ChangeViewBtn";
			this.ChangeViewBtn.Size = new System.Drawing.Size(194, 36);
			this.ChangeViewBtn.TabIndex = 4;
			this.ChangeViewBtn.Text = "Single season";
			this.ChangeViewBtn.UseVisualStyleBackColor = true;
			this.ChangeViewBtn.Click += new System.EventHandler(this.ChangeViewBtn_Click);
			// 
			// SeasonYearCMB
			// 
			this.SeasonYearCMB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.SeasonYearCMB.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.SeasonYearCMB.FormattingEnabled = true;
			this.SeasonYearCMB.Location = new System.Drawing.Point(23, 665);
			this.SeasonYearCMB.Name = "SeasonYearCMB";
			this.SeasonYearCMB.Size = new System.Drawing.Size(194, 24);
			this.SeasonYearCMB.TabIndex = 5;
			this.SeasonYearCMB.SelectedIndexChanged += new System.EventHandler(this.SeasonYearCMB_SelectedIndexChanged);
			// 
			// YearsLbl2
			// 
			this.YearsLbl2.AutoSize = true;
			this.YearsLbl2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.YearsLbl2.Location = new System.Drawing.Point(290, 1);
			this.YearsLbl2.Name = "YearsLbl2";
			this.YearsLbl2.Size = new System.Drawing.Size(50, 16);
			this.YearsLbl2.TabIndex = 1;
			this.YearsLbl2.Text = "232323";
			// 
			// CountryCMB
			// 
			this.CountryCMB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.CountryCMB.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.CountryCMB.FormattingEnabled = true;
			this.CountryCMB.Items.AddRange(new object[] {
            "               England",
            "               Spain",
            "               Italy",
            "               Germany",
            "               France"});
			this.CountryCMB.Location = new System.Drawing.Point(20, 12);
			this.CountryCMB.Name = "CountryCMB";
			this.CountryCMB.Size = new System.Drawing.Size(200, 28);
			this.CountryCMB.TabIndex = 5;
			this.CountryCMB.SelectedIndexChanged += new System.EventHandler(this.CountryCMB_SelectedIndexChanged);
			// 
			// CriteriaCMB
			// 
			this.CriteriaCMB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.CriteriaCMB.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.CriteriaCMB.FormattingEnabled = true;
			this.CriteriaCMB.Items.AddRange(new object[] {
            "               Position",
            "         Goal Difference"});
			this.CriteriaCMB.Location = new System.Drawing.Point(53, 610);
			this.CriteriaCMB.Name = "CriteriaCMB";
			this.CriteriaCMB.Size = new System.Drawing.Size(164, 24);
			this.CriteriaCMB.TabIndex = 5;
			this.CriteriaCMB.SelectedIndexChanged += new System.EventHandler(this.CriteriaCMB_SelectedIndexChanged);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label1.Location = new System.Drawing.Point(20, 611);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(27, 20);
			this.label1.TabIndex = 1;
			this.label1.Text = "By";
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1067, 757);
			this.Controls.Add(this.CountryCMB);
			this.Controls.Add(this.CriteriaCMB);
			this.Controls.Add(this.SeasonYearCMB);
			this.Controls.Add(this.ChangeViewBtn);
			this.Controls.Add(this.ClearBtn);
			this.Controls.Add(this.GraphsPanel);
			this.Controls.Add(this.GraphicsPB);
			this.Controls.Add(this.YearsLbl2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.YearsLbl);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Name = "MainForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Form1";
			this.Load += new System.EventHandler(this.MainForm_Load);
			this.Paint += new System.Windows.Forms.PaintEventHandler(this.MainForm_Paint);
			((System.ComponentModel.ISupportInitialize)(this.GraphicsPB)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label YearsLbl;
		private System.Windows.Forms.PictureBox GraphicsPB;
		private System.Windows.Forms.Panel GraphsPanel;
		private System.Windows.Forms.Button ClearBtn;
		private System.Windows.Forms.Button ChangeViewBtn;
		private System.Windows.Forms.ComboBox SeasonYearCMB;
		private System.Windows.Forms.Label YearsLbl2;
		private System.Windows.Forms.ComboBox CountryCMB;
		private System.Windows.Forms.ComboBox CriteriaCMB;
		private System.Windows.Forms.Label label1;
	}
}