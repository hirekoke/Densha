namespace Densha
{
    partial class CommandForm
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
            this.destDirBox = new System.Windows.Forms.TextBox();
            this.selectDestDirButton = new System.Windows.Forms.Button();
            this.commandListBox = new System.Windows.Forms.ListBox();
            this.copyCBButton = new System.Windows.Forms.Button();
            this.destDirLabel = new System.Windows.Forms.Label();
            this.runButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // destDirBox
            // 
            this.destDirBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.destDirBox.Location = new System.Drawing.Point(60, 12);
            this.destDirBox.Name = "destDirBox";
            this.destDirBox.Size = new System.Drawing.Size(181, 19);
            this.destDirBox.TabIndex = 0;
            // 
            // selectDestDirButton
            // 
            this.selectDestDirButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.selectDestDirButton.Location = new System.Drawing.Point(247, 10);
            this.selectDestDirButton.Name = "selectDestDirButton";
            this.selectDestDirButton.Size = new System.Drawing.Size(75, 23);
            this.selectDestDirButton.TabIndex = 1;
            this.selectDestDirButton.Text = "参照...";
            this.selectDestDirButton.UseVisualStyleBackColor = true;
            // 
            // commandListBox
            // 
            this.commandListBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.commandListBox.FormattingEnabled = true;
            this.commandListBox.ItemHeight = 12;
            this.commandListBox.Location = new System.Drawing.Point(12, 37);
            this.commandListBox.Name = "commandListBox";
            this.commandListBox.Size = new System.Drawing.Size(395, 256);
            this.commandListBox.TabIndex = 2;
            // 
            // copyCBButton
            // 
            this.copyCBButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.copyCBButton.Location = new System.Drawing.Point(296, 299);
            this.copyCBButton.Name = "copyCBButton";
            this.copyCBButton.Size = new System.Drawing.Size(111, 23);
            this.copyCBButton.TabIndex = 3;
            this.copyCBButton.Text = "クリップボードにコピー";
            this.copyCBButton.UseVisualStyleBackColor = true;
            // 
            // destDirLabel
            // 
            this.destDirLabel.AutoSize = true;
            this.destDirLabel.Location = new System.Drawing.Point(10, 15);
            this.destDirLabel.Name = "destDirLabel";
            this.destDirLabel.Size = new System.Drawing.Size(44, 12);
            this.destDirLabel.TabIndex = 4;
            this.destDirLabel.Text = "コピー先";
            // 
            // runButton
            // 
            this.runButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.runButton.Location = new System.Drawing.Point(332, 10);
            this.runButton.Name = "runButton";
            this.runButton.Size = new System.Drawing.Size(75, 23);
            this.runButton.TabIndex = 5;
            this.runButton.Text = "実行";
            this.runButton.UseVisualStyleBackColor = true;
            // 
            // CommandForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(419, 334);
            this.Controls.Add(this.runButton);
            this.Controls.Add(this.destDirLabel);
            this.Controls.Add(this.copyCBButton);
            this.Controls.Add(this.commandListBox);
            this.Controls.Add(this.selectDestDirButton);
            this.Controls.Add(this.destDirBox);
            this.MinimizeBox = false;
            this.Name = "CommandForm";
            this.Text = "コマンド出力";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox destDirBox;
        private System.Windows.Forms.Button selectDestDirButton;
        private System.Windows.Forms.ListBox commandListBox;
        private System.Windows.Forms.Button copyCBButton;
        private System.Windows.Forms.Label destDirLabel;
        private System.Windows.Forms.Button runButton;
    }
}