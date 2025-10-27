namespace CBIR_Project
{
    partial class Form1
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

        private void InitializeComponent()
        {
            this.lblTitle = new System.Windows.Forms.Label();
            this.btnChooseImage = new System.Windows.Forms.Button();
            this.btnExtractFeatures = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this.picQuery = new System.Windows.Forms.PictureBox();
            this.flowResults = new System.Windows.Forms.FlowLayoutPanel();
            this.statusBar = new System.Windows.Forms.StatusStrip();
            this.lblStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblGroupInfo = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.picQuery)).BeginInit();
            this.statusBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(44, 62, 80);
            this.lblTitle.Location = new System.Drawing.Point(0, 9);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(984, 45);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "🖼️ Content-Based Image Retrieval (CBIR)";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnChooseImage
            // 
            this.btnChooseImage.BackColor = System.Drawing.Color.FromArgb(52, 152, 219);
            this.btnChooseImage.FlatAppearance.BorderSize = 0;
            this.btnChooseImage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnChooseImage.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnChooseImage.ForeColor = System.Drawing.Color.White;
            this.btnChooseImage.Location = new System.Drawing.Point(45, 70);
            this.btnChooseImage.Name = "btnChooseImage";
            this.btnChooseImage.Size = new System.Drawing.Size(180, 40);
            this.btnChooseImage.TabIndex = 1;
            this.btnChooseImage.Text = "Chọn ảnh truy vấn";
            this.btnChooseImage.UseVisualStyleBackColor = false;
            this.btnChooseImage.Click += new System.EventHandler(this.btnChooseImage_Click);
            // 
            // btnExtractFeatures
            // 
            this.btnExtractFeatures.BackColor = System.Drawing.Color.FromArgb(241, 196, 15);
            this.btnExtractFeatures.FlatAppearance.BorderSize = 0;
            this.btnExtractFeatures.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExtractFeatures.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnExtractFeatures.ForeColor = System.Drawing.Color.White;
            this.btnExtractFeatures.Location = new System.Drawing.Point(245, 70);
            this.btnExtractFeatures.Name = "btnExtractFeatures";
            this.btnExtractFeatures.Size = new System.Drawing.Size(200, 40);
            this.btnExtractFeatures.TabIndex = 2;
            this.btnExtractFeatures.Text = "Trích đặc trưng Dataset";
            this.btnExtractFeatures.UseVisualStyleBackColor = false;
            this.btnExtractFeatures.Click += new System.EventHandler(this.btnExtractFeatures_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.BackColor = System.Drawing.Color.FromArgb(46, 204, 113);
            this.btnSearch.FlatAppearance.BorderSize = 0;
            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearch.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnSearch.ForeColor = System.Drawing.Color.White;
            this.btnSearch.Location = new System.Drawing.Point(470, 70);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(150, 40);
            this.btnSearch.TabIndex = 3;
            this.btnSearch.Text = "🔍 Tìm kiếm";
            this.btnSearch.UseVisualStyleBackColor = false;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // picQuery
            // 
            this.picQuery.BackColor = System.Drawing.Color.White;
            this.picQuery.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picQuery.Location = new System.Drawing.Point(45, 130);
            this.picQuery.Name = "picQuery";
            this.picQuery.Size = new System.Drawing.Size(350, 300);
            this.picQuery.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picQuery.TabIndex = 4;
            this.picQuery.TabStop = false;
            // 
            // flowResults
            // 
            this.flowResults.AutoScroll = true;
            this.flowResults.BackColor = System.Drawing.Color.WhiteSmoke;
            this.flowResults.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flowResults.Location = new System.Drawing.Point(420, 130);
            this.flowResults.Name = "flowResults";
            this.flowResults.Padding = new System.Windows.Forms.Padding(10);
            this.flowResults.Size = new System.Drawing.Size(520, 450);
            this.flowResults.TabIndex = 5;
            // 
            // statusBar
            // 
            this.statusBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblStatus});
            this.statusBar.Location = new System.Drawing.Point(0, 598);
            this.statusBar.Name = "statusBar";
            this.statusBar.Size = new System.Drawing.Size(984, 22);
            this.statusBar.TabIndex = 6;
            // 
            // lblStatus
            // 
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(128, 17);
            this.lblStatus.Text = "Sẵn sàng để tìm kiếm...";
            // 
            // lblGroupInfo
            // 
            this.lblGroupInfo.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic);
            this.lblGroupInfo.ForeColor = System.Drawing.Color.DimGray;
            this.lblGroupInfo.Location = new System.Drawing.Point(12, 565);
            this.lblGroupInfo.Name = "lblGroupInfo";
            this.lblGroupInfo.Size = new System.Drawing.Size(960, 23);
            this.lblGroupInfo.TabIndex = 7;
            this.lblGroupInfo.Text = "Nhóm 3 – Hệ đa phương tiện – GVHD: ...";
            this.lblGroupInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(984, 620);
            this.Controls.Add(this.lblGroupInfo);
            this.Controls.Add(this.statusBar);
            this.Controls.Add(this.flowResults);
            this.Controls.Add(this.picQuery);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.btnExtractFeatures);
            this.Controls.Add(this.btnChooseImage);
            this.Controls.Add(this.lblTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CBIR - Content-Based Image Retrieval";
            ((System.ComponentModel.ISupportInitialize)(this.picQuery)).EndInit();
            this.statusBar.ResumeLayout(false);
            this.statusBar.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Button btnChooseImage;
        private System.Windows.Forms.Button btnExtractFeatures;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.PictureBox picQuery;
        private System.Windows.Forms.FlowLayoutPanel flowResults;
        private System.Windows.Forms.StatusStrip statusBar;
        private System.Windows.Forms.ToolStripStatusLabel lblStatus;
        private System.Windows.Forms.Label lblGroupInfo;
    }
}
