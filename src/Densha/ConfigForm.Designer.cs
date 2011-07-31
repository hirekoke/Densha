namespace Densha
{
    partial class ConfigForm
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
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.cancelButton = new System.Windows.Forms.Button();
            this.applyButton = new System.Windows.Forms.Button();
            this.okButton = new System.Windows.Forms.Button();
            this.formatTabPage = new System.Windows.Forms.TabPage();
            this.label3 = new System.Windows.Forms.Label();
            this.tagDelimiterBox = new System.Windows.Forms.ComboBox();
            this.dateTimeDisplayFormatBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.designPage = new System.Windows.Forms.TabPage();
            this.designView = new System.Windows.Forms.ListView();
            this.designDescHeader = new System.Windows.Forms.ColumnHeader();
            this.designValueHeader = new System.Windows.Forms.ColumnHeader();
            this.label2 = new System.Windows.Forms.Label();
            this.thumbnailNamePatternBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.thumbSizeWidthBox = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.thumbSizeHeightBox = new System.Windows.Forms.NumericUpDown();
            this.flowLayoutPanel1.SuspendLayout();
            this.formatTabPage.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.designPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.thumbSizeWidthBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.thumbSizeHeightBox)).BeginInit();
            this.SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoSize = true;
            this.flowLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel1.Controls.Add(this.cancelButton);
            this.flowLayoutPanel1.Controls.Add(this.applyButton);
            this.flowLayoutPanel1.Controls.Add(this.okButton);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 290);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(443, 29);
            this.flowLayoutPanel1.TabIndex = 2;
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(365, 3);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 3;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // applyButton
            // 
            this.applyButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.applyButton.Location = new System.Drawing.Point(271, 3);
            this.applyButton.Margin = new System.Windows.Forms.Padding(3, 3, 16, 3);
            this.applyButton.Name = "applyButton";
            this.applyButton.Size = new System.Drawing.Size(75, 23);
            this.applyButton.TabIndex = 2;
            this.applyButton.Text = "Apply";
            this.applyButton.UseVisualStyleBackColor = true;
            // 
            // okButton
            // 
            this.okButton.Location = new System.Drawing.Point(190, 3);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 4;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            // 
            // formatTabPage
            // 
            this.formatTabPage.Controls.Add(this.thumbSizeHeightBox);
            this.formatTabPage.Controls.Add(this.label5);
            this.formatTabPage.Controls.Add(this.thumbSizeWidthBox);
            this.formatTabPage.Controls.Add(this.label4);
            this.formatTabPage.Controls.Add(this.thumbnailNamePatternBox);
            this.formatTabPage.Controls.Add(this.label2);
            this.formatTabPage.Controls.Add(this.label3);
            this.formatTabPage.Controls.Add(this.tagDelimiterBox);
            this.formatTabPage.Controls.Add(this.dateTimeDisplayFormatBox);
            this.formatTabPage.Controls.Add(this.label1);
            this.formatTabPage.Location = new System.Drawing.Point(4, 22);
            this.formatTabPage.Name = "formatTabPage";
            this.formatTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.formatTabPage.Size = new System.Drawing.Size(435, 264);
            this.formatTabPage.TabIndex = 0;
            this.formatTabPage.Text = "全般";
            this.formatTabPage.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 79);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(78, 12);
            this.label3.TabIndex = 5;
            this.label3.Text = "タグ区切り文字";
            // 
            // tagDelimiterBox
            // 
            this.tagDelimiterBox.FormattingEnabled = true;
            this.tagDelimiterBox.Items.AddRange(new object[] {
            "_",
            "-",
            "/",
            " "});
            this.tagDelimiterBox.Location = new System.Drawing.Point(92, 76);
            this.tagDelimiterBox.Name = "tagDelimiterBox";
            this.tagDelimiterBox.Size = new System.Drawing.Size(100, 20);
            this.tagDelimiterBox.TabIndex = 4;
            // 
            // dateTimeDisplayFormatBox
            // 
            this.dateTimeDisplayFormatBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dateTimeDisplayFormatBox.Location = new System.Drawing.Point(117, 16);
            this.dateTimeDisplayFormatBox.Name = "dateTimeDisplayFormatBox";
            this.dateTimeDisplayFormatBox.Size = new System.Drawing.Size(310, 19);
            this.dateTimeDisplayFormatBox.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(103, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "日時表示フォーマット";
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.formatTabPage);
            this.tabControl.Controls.Add(this.designPage);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(443, 290);
            this.tabControl.TabIndex = 3;
            // 
            // designPage
            // 
            this.designPage.Controls.Add(this.designView);
            this.designPage.Location = new System.Drawing.Point(4, 22);
            this.designPage.Name = "designPage";
            this.designPage.Padding = new System.Windows.Forms.Padding(3);
            this.designPage.Size = new System.Drawing.Size(435, 264);
            this.designPage.TabIndex = 1;
            this.designPage.Text = "色・フォント";
            this.designPage.UseVisualStyleBackColor = true;
            // 
            // designView
            // 
            this.designView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.designDescHeader,
            this.designValueHeader});
            this.designView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.designView.FullRowSelect = true;
            this.designView.GridLines = true;
            this.designView.Location = new System.Drawing.Point(3, 3);
            this.designView.MultiSelect = false;
            this.designView.Name = "designView";
            this.designView.ShowItemToolTips = true;
            this.designView.Size = new System.Drawing.Size(429, 258);
            this.designView.TabIndex = 0;
            this.designView.UseCompatibleStateImageBehavior = false;
            this.designView.View = System.Windows.Forms.View.Details;
            // 
            // designDescHeader
            // 
            this.designDescHeader.Text = "説明";
            this.designDescHeader.Width = 209;
            // 
            // designValueHeader
            // 
            this.designValueHeader.Text = "値";
            this.designValueHeader.Width = 198;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(137, 12);
            this.label2.TabIndex = 6;
            this.label2.Text = "サムネイルファイル名パターン";
            // 
            // thumbnailNamePatternBox
            // 
            this.thumbnailNamePatternBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.thumbnailNamePatternBox.Location = new System.Drawing.Point(151, 46);
            this.thumbnailNamePatternBox.Name = "thumbnailNamePatternBox";
            this.thumbnailNamePatternBox.Size = new System.Drawing.Size(276, 19);
            this.thumbnailNamePatternBox.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 111);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(83, 12);
            this.label4.TabIndex = 8;
            this.label4.Text = "サムネイルサイズ";
            // 
            // thumbSizeWidthBox
            // 
            this.thumbSizeWidthBox.Increment = new decimal(new int[] {
            8,
            0,
            0,
            0});
            this.thumbSizeWidthBox.Location = new System.Drawing.Point(97, 109);
            this.thumbSizeWidthBox.Maximum = new decimal(new int[] {
            512,
            0,
            0,
            0});
            this.thumbSizeWidthBox.Name = "thumbSizeWidthBox";
            this.thumbSizeWidthBox.Size = new System.Drawing.Size(95, 19);
            this.thumbSizeWidthBox.TabIndex = 9;
            this.thumbSizeWidthBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(198, 111);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(17, 12);
            this.label5.TabIndex = 10;
            this.label5.Text = "×";
            // 
            // thumbSizeHeightBox
            // 
            this.thumbSizeHeightBox.Increment = new decimal(new int[] {
            8,
            0,
            0,
            0});
            this.thumbSizeHeightBox.Location = new System.Drawing.Point(221, 111);
            this.thumbSizeHeightBox.Maximum = new decimal(new int[] {
            512,
            0,
            0,
            0});
            this.thumbSizeHeightBox.Name = "thumbSizeHeightBox";
            this.thumbSizeHeightBox.Size = new System.Drawing.Size(95, 19);
            this.thumbSizeHeightBox.TabIndex = 11;
            this.thumbSizeHeightBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // ConfigForm
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(443, 319);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.flowLayoutPanel1);
            this.MinimizeBox = false;
            this.Name = "ConfigForm";
            this.Text = "設定";
            this.flowLayoutPanel1.ResumeLayout(false);
            this.formatTabPage.ResumeLayout(false);
            this.formatTabPage.PerformLayout();
            this.tabControl.ResumeLayout(false);
            this.designPage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.thumbSizeWidthBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.thumbSizeHeightBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button applyButton;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.TabPage formatTabPage;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabPage designPage;
        private System.Windows.Forms.TextBox dateTimeDisplayFormatBox;
        private System.Windows.Forms.ComboBox tagDelimiterBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListView designView;
        private System.Windows.Forms.ColumnHeader designDescHeader;
        private System.Windows.Forms.ColumnHeader designValueHeader;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox thumbnailNamePatternBox;
        private System.Windows.Forms.NumericUpDown thumbSizeHeightBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown thumbSizeWidthBox;
        private System.Windows.Forms.Label label4;
    }
}