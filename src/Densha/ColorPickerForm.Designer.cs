namespace Densha
{
    partial class ColorPickerForm
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
                if (_selectedCustomBmp != null) _selectedCustomBmp.Dispose();
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
            this.systemColorsBox = new System.Windows.Forms.ComboBox();
            this.namedColorsBox = new System.Windows.Forms.ComboBox();
            this.systemColorRadioButton = new System.Windows.Forms.RadioButton();
            this.namedColorRadioButton = new System.Windows.Forms.RadioButton();
            this.customColorRadioButton = new System.Windows.Forms.RadioButton();
            this.selectCustomColorButton = new System.Windows.Forms.Button();
            this.okButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // systemColorsBox
            // 
            this.systemColorsBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.systemColorsBox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.systemColorsBox.FormattingEnabled = true;
            this.systemColorsBox.Location = new System.Drawing.Point(119, 12);
            this.systemColorsBox.Name = "systemColorsBox";
            this.systemColorsBox.Size = new System.Drawing.Size(163, 20);
            this.systemColorsBox.TabIndex = 3;
            // 
            // namedColorsBox
            // 
            this.namedColorsBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.namedColorsBox.FormattingEnabled = true;
            this.namedColorsBox.Location = new System.Drawing.Point(119, 45);
            this.namedColorsBox.Name = "namedColorsBox";
            this.namedColorsBox.Size = new System.Drawing.Size(163, 20);
            this.namedColorsBox.TabIndex = 4;
            // 
            // systemColorRadioButton
            // 
            this.systemColorRadioButton.AutoSize = true;
            this.systemColorRadioButton.Location = new System.Drawing.Point(13, 13);
            this.systemColorRadioButton.Name = "systemColorRadioButton";
            this.systemColorRadioButton.Size = new System.Drawing.Size(88, 16);
            this.systemColorRadioButton.TabIndex = 5;
            this.systemColorRadioButton.TabStop = true;
            this.systemColorRadioButton.Text = "システムカラー";
            this.systemColorRadioButton.UseVisualStyleBackColor = true;
            // 
            // namedColorRadioButton
            // 
            this.namedColorRadioButton.AutoSize = true;
            this.namedColorRadioButton.Location = new System.Drawing.Point(12, 46);
            this.namedColorRadioButton.Name = "namedColorRadioButton";
            this.namedColorRadioButton.Size = new System.Drawing.Size(97, 16);
            this.namedColorRadioButton.TabIndex = 6;
            this.namedColorRadioButton.TabStop = true;
            this.namedColorRadioButton.Text = "定義済みカラー";
            this.namedColorRadioButton.UseVisualStyleBackColor = true;
            // 
            // customColorRadioButton
            // 
            this.customColorRadioButton.AutoSize = true;
            this.customColorRadioButton.Location = new System.Drawing.Point(12, 77);
            this.customColorRadioButton.Name = "customColorRadioButton";
            this.customColorRadioButton.Size = new System.Drawing.Size(86, 16);
            this.customColorRadioButton.TabIndex = 7;
            this.customColorRadioButton.TabStop = true;
            this.customColorRadioButton.Text = "カスタムカラー";
            this.customColorRadioButton.UseVisualStyleBackColor = true;
            // 
            // selectCustomColorButton
            // 
            this.selectCustomColorButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.selectCustomColorButton.Location = new System.Drawing.Point(119, 74);
            this.selectCustomColorButton.Name = "selectCustomColorButton";
            this.selectCustomColorButton.Size = new System.Drawing.Size(163, 23);
            this.selectCustomColorButton.TabIndex = 8;
            this.selectCustomColorButton.Text = "選択...";
            this.selectCustomColorButton.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.selectCustomColorButton.UseVisualStyleBackColor = true;
            // 
            // okButton
            // 
            this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okButton.Location = new System.Drawing.Point(126, 125);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 9;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(207, 125);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 10;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // ColorPickerForm
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(294, 160);
            this.ControlBox = false;
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.selectCustomColorButton);
            this.Controls.Add(this.customColorRadioButton);
            this.Controls.Add(this.namedColorRadioButton);
            this.Controls.Add(this.systemColorRadioButton);
            this.Controls.Add(this.namedColorsBox);
            this.Controls.Add(this.systemColorsBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ColorPickerForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "色選択";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox systemColorsBox;
        private System.Windows.Forms.ComboBox namedColorsBox;
        private System.Windows.Forms.RadioButton systemColorRadioButton;
        private System.Windows.Forms.RadioButton namedColorRadioButton;
        private System.Windows.Forms.RadioButton customColorRadioButton;
        private System.Windows.Forms.Button selectCustomColorButton;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button cancelButton;

    }
}