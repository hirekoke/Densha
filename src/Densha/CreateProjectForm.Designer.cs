namespace Densha
{
    partial class CreateProjectForm
    {
        /// <summary>
        /// 必要なデザイナ変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナで生成されたコード

        /// <summary>
        /// デザイナ サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディタで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.originalDirBox = new System.Windows.Forms.TextBox();
            this.originalDirSelectButton = new System.Windows.Forms.Button();
            this.okButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.thumbnailGroup = new System.Windows.Forms.GroupBox();
            this.thumbPatternBox = new System.Windows.Forms.TextBox();
            this.thumbnailDirSelectButton = new System.Windows.Forms.Button();
            this.thumbnailDirBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.createThumbDirButton = new System.Windows.Forms.RadioButton();
            this.useExistThumbDirButton = new System.Windows.Forms.RadioButton();
            this.label3 = new System.Windows.Forms.Label();
            this.thumbnailGroup.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "写真があるフォルダ";
            // 
            // originalDirBox
            // 
            this.originalDirBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.originalDirBox.Location = new System.Drawing.Point(34, 24);
            this.originalDirBox.Name = "originalDirBox";
            this.originalDirBox.Size = new System.Drawing.Size(278, 19);
            this.originalDirBox.TabIndex = 1;
            // 
            // originalDirSelectButton
            // 
            this.originalDirSelectButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.originalDirSelectButton.Location = new System.Drawing.Point(318, 22);
            this.originalDirSelectButton.Name = "originalDirSelectButton";
            this.originalDirSelectButton.Size = new System.Drawing.Size(75, 23);
            this.originalDirSelectButton.TabIndex = 2;
            this.originalDirSelectButton.Text = "参照…";
            this.originalDirSelectButton.UseVisualStyleBackColor = true;
            // 
            // okButton
            // 
            this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.okButton.Location = new System.Drawing.Point(237, 191);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 3;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(318, 191);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 4;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // thumbnailGroup
            // 
            this.thumbnailGroup.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.thumbnailGroup.Controls.Add(this.label3);
            this.thumbnailGroup.Controls.Add(this.thumbPatternBox);
            this.thumbnailGroup.Controls.Add(this.thumbnailDirSelectButton);
            this.thumbnailGroup.Controls.Add(this.thumbnailDirBox);
            this.thumbnailGroup.Controls.Add(this.label2);
            this.thumbnailGroup.Controls.Add(this.createThumbDirButton);
            this.thumbnailGroup.Controls.Add(this.useExistThumbDirButton);
            this.thumbnailGroup.Location = new System.Drawing.Point(12, 60);
            this.thumbnailGroup.Name = "thumbnailGroup";
            this.thumbnailGroup.Size = new System.Drawing.Size(381, 114);
            this.thumbnailGroup.TabIndex = 12;
            this.thumbnailGroup.TabStop = false;
            this.thumbnailGroup.Text = "サムネイル生成方法";
            // 
            // thumbPatternBox
            // 
            this.thumbPatternBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.thumbPatternBox.Location = new System.Drawing.Point(149, 84);
            this.thumbPatternBox.Name = "thumbPatternBox";
            this.thumbPatternBox.Size = new System.Drawing.Size(226, 19);
            this.thumbPatternBox.TabIndex = 16;
            // 
            // thumbnailDirSelectButton
            // 
            this.thumbnailDirSelectButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.thumbnailDirSelectButton.Location = new System.Drawing.Point(300, 57);
            this.thumbnailDirSelectButton.Name = "thumbnailDirSelectButton";
            this.thumbnailDirSelectButton.Size = new System.Drawing.Size(75, 23);
            this.thumbnailDirSelectButton.TabIndex = 15;
            this.thumbnailDirSelectButton.Text = "参照…";
            this.thumbnailDirSelectButton.UseVisualStyleBackColor = true;
            // 
            // thumbnailDirBox
            // 
            this.thumbnailDirBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.thumbnailDirBox.Location = new System.Drawing.Point(22, 59);
            this.thumbnailDirBox.Name = "thumbnailDirBox";
            this.thumbnailDirBox.Size = new System.Drawing.Size(272, 19);
            this.thumbnailDirBox.TabIndex = 14;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 44);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(99, 12);
            this.label2.TabIndex = 13;
            this.label2.Text = "サムネイルのフォルダ";
            // 
            // createThumbDirButton
            // 
            this.createThumbDirButton.AutoSize = true;
            this.createThumbDirButton.Location = new System.Drawing.Point(151, 18);
            this.createThumbDirButton.Name = "createThumbDirButton";
            this.createThumbDirButton.Size = new System.Drawing.Size(102, 16);
            this.createThumbDirButton.TabIndex = 12;
            this.createThumbDirButton.TabStop = true;
            this.createThumbDirButton.Text = "サムネイルを作る";
            this.createThumbDirButton.UseVisualStyleBackColor = true;
            // 
            // useExistThumbDirButton
            // 
            this.useExistThumbDirButton.AutoSize = true;
            this.useExistThumbDirButton.Location = new System.Drawing.Point(6, 18);
            this.useExistThumbDirButton.Name = "useExistThumbDirButton";
            this.useExistThumbDirButton.Size = new System.Drawing.Size(139, 16);
            this.useExistThumbDirButton.TabIndex = 11;
            this.useExistThumbDirButton.TabStop = true;
            this.useExistThumbDirButton.Text = "既存のサムネイルを使用";
            this.useExistThumbDirButton.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 87);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(137, 12);
            this.label3.TabIndex = 17;
            this.label3.Text = "サムネイルファイル名パターン";
            // 
            // CreateProjectForm
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(405, 226);
            this.ControlBox = false;
            this.Controls.Add(this.thumbnailGroup);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.originalDirSelectButton);
            this.Controls.Add(this.originalDirBox);
            this.Controls.Add(this.label1);
            this.Name = "CreateProjectForm";
            this.Text = "CreateProjectForm";
            this.thumbnailGroup.ResumeLayout(false);
            this.thumbnailGroup.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox originalDirBox;
        private System.Windows.Forms.Button originalDirSelectButton;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.GroupBox thumbnailGroup;
        private System.Windows.Forms.TextBox thumbPatternBox;
        private System.Windows.Forms.Button thumbnailDirSelectButton;
        private System.Windows.Forms.TextBox thumbnailDirBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RadioButton createThumbDirButton;
        private System.Windows.Forms.RadioButton useExistThumbDirButton;
        private System.Windows.Forms.Label label3;
    }
}