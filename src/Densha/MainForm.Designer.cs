namespace Densha {
    partial class MainForm {
        /// <summary>
        /// 必要なデザイナ変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナで生成されたコード

        /// <summary>
        /// デザイナ サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディタで変更しないでください。
        /// </summary>
        private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.toolStripContainer = new System.Windows.Forms.ToolStripContainer();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.statusProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.mainSplit = new System.Windows.Forms.SplitContainer();
            this.imageList = new Densha.ImageList();
            this.imageTabs = new Densha.ImageTabControl();
            this.mainMenu = new System.Windows.Forms.MenuStrip();
            this.menuFileItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuNewProjectItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuOpenProjectItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuOverwriteItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuSaveasItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuEditItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuExportCommandItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuViewItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuShowTagTypeItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuShowTagItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tagTypeFormViewButton = new System.Windows.Forms.ToolStripButton();
            this.tagFormViewButton = new System.Windows.Forms.ToolStripButton();
            this.showUnusedButton = new System.Windows.Forms.ToolStripButton();
            this.statusProgressLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripContainer.BottomToolStripPanel.SuspendLayout();
            this.toolStripContainer.ContentPanel.SuspendLayout();
            this.toolStripContainer.TopToolStripPanel.SuspendLayout();
            this.toolStripContainer.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.mainSplit.Panel1.SuspendLayout();
            this.mainSplit.Panel2.SuspendLayout();
            this.mainSplit.SuspendLayout();
            this.mainMenu.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripContainer
            // 
            // 
            // toolStripContainer.BottomToolStripPanel
            // 
            this.toolStripContainer.BottomToolStripPanel.Controls.Add(this.statusStrip);
            // 
            // toolStripContainer.ContentPanel
            // 
            this.toolStripContainer.ContentPanel.Controls.Add(this.mainSplit);
            this.toolStripContainer.ContentPanel.Size = new System.Drawing.Size(673, 385);
            this.toolStripContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStripContainer.Location = new System.Drawing.Point(0, 0);
            this.toolStripContainer.Name = "toolStripContainer";
            this.toolStripContainer.Size = new System.Drawing.Size(673, 459);
            this.toolStripContainer.TabIndex = 0;
            this.toolStripContainer.Text = "toolStripContainer1";
            // 
            // toolStripContainer.TopToolStripPanel
            // 
            this.toolStripContainer.TopToolStripPanel.Controls.Add(this.mainMenu);
            this.toolStripContainer.TopToolStripPanel.Controls.Add(this.toolStrip1);
            // 
            // statusStrip
            // 
            this.statusStrip.Dock = System.Windows.Forms.DockStyle.None;
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusProgressLabel,
            this.statusProgressBar});
            this.statusStrip.Location = new System.Drawing.Point(0, 0);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(673, 23);
            this.statusStrip.TabIndex = 0;
            // 
            // statusProgressBar
            // 
            this.statusProgressBar.AutoSize = false;
            this.statusProgressBar.Maximum = 200;
            this.statusProgressBar.Name = "statusProgressBar";
            this.statusProgressBar.Size = new System.Drawing.Size(400, 17);
            this.statusProgressBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.statusProgressBar.Visible = false;
            // 
            // mainSplit
            // 
            this.mainSplit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainSplit.Location = new System.Drawing.Point(0, 0);
            this.mainSplit.Name = "mainSplit";
            // 
            // mainSplit.Panel1
            // 
            this.mainSplit.Panel1.BackColor = System.Drawing.SystemColors.Control;
            this.mainSplit.Panel1.Controls.Add(this.imageList);
            // 
            // mainSplit.Panel2
            // 
            this.mainSplit.Panel2.BackColor = System.Drawing.SystemColors.Control;
            this.mainSplit.Panel2.Controls.Add(this.imageTabs);
            this.mainSplit.Size = new System.Drawing.Size(673, 385);
            this.mainSplit.SplitterDistance = 235;
            this.mainSplit.TabIndex = 0;
            // 
            // imageList
            // 
            this.imageList.AllowDrop = true;
            this.imageList.BackColor = System.Drawing.SystemColors.Window;
            this.imageList.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.imageList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.imageList.Location = new System.Drawing.Point(0, 0);
            this.imageList.Name = "imageList";
            this.imageList.ShowUnusedItems = true;
            this.imageList.Size = new System.Drawing.Size(235, 385);
            this.imageList.TabIndex = 0;
            // 
            // imageTabs
            // 
            this.imageTabs.AllowDrop = true;
            this.imageTabs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.imageTabs.HotTrack = true;
            this.imageTabs.Location = new System.Drawing.Point(0, 0);
            this.imageTabs.Multiline = true;
            this.imageTabs.Name = "imageTabs";
            this.imageTabs.SelectedIndex = 0;
            this.imageTabs.Size = new System.Drawing.Size(434, 385);
            this.imageTabs.TabIndex = 0;
            // 
            // mainMenu
            // 
            this.mainMenu.Dock = System.Windows.Forms.DockStyle.None;
            this.mainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuFileItem,
            this.menuEditItem,
            this.menuViewItem});
            this.mainMenu.Location = new System.Drawing.Point(0, 0);
            this.mainMenu.Name = "mainMenu";
            this.mainMenu.Size = new System.Drawing.Size(673, 26);
            this.mainMenu.TabIndex = 0;
            this.mainMenu.Text = "menuStrip1";
            // 
            // menuFileItem
            // 
            this.menuFileItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuNewProjectItem,
            this.menuOpenProjectItem,
            this.menuOverwriteItem,
            this.menuSaveasItem});
            this.menuFileItem.Name = "menuFileItem";
            this.menuFileItem.Size = new System.Drawing.Size(85, 22);
            this.menuFileItem.Text = "ファイル(&F)";
            // 
            // menuNewProjectItem
            // 
            this.menuNewProjectItem.Name = "menuNewProjectItem";
            this.menuNewProjectItem.Size = new System.Drawing.Size(196, 22);
            this.menuNewProjectItem.Text = "新規プロジェクト...";
            // 
            // menuOpenProjectItem
            // 
            this.menuOpenProjectItem.Name = "menuOpenProjectItem";
            this.menuOpenProjectItem.Size = new System.Drawing.Size(196, 22);
            this.menuOpenProjectItem.Text = "プロジェクトを開く...";
            // 
            // menuOverwriteItem
            // 
            this.menuOverwriteItem.Name = "menuOverwriteItem";
            this.menuOverwriteItem.Size = new System.Drawing.Size(196, 22);
            this.menuOverwriteItem.Text = "上書き保存";
            // 
            // menuSaveasItem
            // 
            this.menuSaveasItem.Name = "menuSaveasItem";
            this.menuSaveasItem.Size = new System.Drawing.Size(196, 22);
            this.menuSaveasItem.Text = "名前を付けて保存...";
            // 
            // menuEditItem
            // 
            this.menuEditItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuExportCommandItem});
            this.menuEditItem.Name = "menuEditItem";
            this.menuEditItem.Size = new System.Drawing.Size(61, 22);
            this.menuEditItem.Text = "編集(&E)";
            // 
            // menuExportCommandItem
            // 
            this.menuExportCommandItem.Name = "menuExportCommandItem";
            this.menuExportCommandItem.Size = new System.Drawing.Size(160, 22);
            this.menuExportCommandItem.Text = "コマンド出力...";
            // 
            // menuViewItem
            // 
            this.menuViewItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuShowTagTypeItem,
            this.menuShowTagItem});
            this.menuViewItem.Name = "menuViewItem";
            this.menuViewItem.Size = new System.Drawing.Size(62, 22);
            this.menuViewItem.Text = "表示(&V)";
            // 
            // menuShowTagTypeItem
            // 
            this.menuShowTagTypeItem.CheckOnClick = true;
            this.menuShowTagTypeItem.Name = "menuShowTagTypeItem";
            this.menuShowTagTypeItem.Size = new System.Drawing.Size(160, 22);
            this.menuShowTagTypeItem.Text = "タグタイプ設定";
            // 
            // menuShowTagItem
            // 
            this.menuShowTagItem.CheckOnClick = true;
            this.menuShowTagItem.Name = "menuShowTagItem";
            this.menuShowTagItem.Size = new System.Drawing.Size(160, 22);
            this.menuShowTagItem.Text = "タグ設定";
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tagTypeFormViewButton,
            this.tagFormViewButton,
            this.showUnusedButton});
            this.toolStrip1.Location = new System.Drawing.Point(3, 26);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(252, 25);
            this.toolStrip1.TabIndex = 1;
            // 
            // tagTypeFormViewButton
            // 
            this.tagTypeFormViewButton.CheckOnClick = true;
            this.tagTypeFormViewButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tagTypeFormViewButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tagTypeFormViewButton.Name = "tagTypeFormViewButton";
            this.tagTypeFormViewButton.Size = new System.Drawing.Size(96, 22);
            this.tagTypeFormViewButton.Text = "タグタイプ設定";
            this.tagTypeFormViewButton.ToolTipText = "タグタイプ設定";
            // 
            // tagFormViewButton
            // 
            this.tagFormViewButton.CheckOnClick = true;
            this.tagFormViewButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tagFormViewButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tagFormViewButton.Name = "tagFormViewButton";
            this.tagFormViewButton.Size = new System.Drawing.Size(60, 22);
            this.tagFormViewButton.Text = "タグ設定";
            // 
            // showUnusedButton
            // 
            this.showUnusedButton.Checked = true;
            this.showUnusedButton.CheckOnClick = true;
            this.showUnusedButton.CheckState = System.Windows.Forms.CheckState.Checked;
            this.showUnusedButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.showUnusedButton.Image = ((System.Drawing.Image)(resources.GetObject("showUnusedButton.Image")));
            this.showUnusedButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.showUnusedButton.Name = "showUnusedButton";
            this.showUnusedButton.Size = new System.Drawing.Size(84, 22);
            this.showUnusedButton.Text = "非選択も表示";
            // 
            // statusProgressLabel
            // 
            this.statusProgressLabel.Name = "statusProgressLabel";
            this.statusProgressLabel.Size = new System.Drawing.Size(134, 18);
            this.statusProgressLabel.Text = "toolStripStatusLabel1";
            this.statusProgressLabel.Visible = false;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(673, 459);
            this.Controls.Add(this.toolStripContainer);
            this.MainMenuStrip = this.mainMenu;
            this.Name = "MainForm";
            this.Text = "Densha";
            this.toolStripContainer.BottomToolStripPanel.ResumeLayout(false);
            this.toolStripContainer.BottomToolStripPanel.PerformLayout();
            this.toolStripContainer.ContentPanel.ResumeLayout(false);
            this.toolStripContainer.TopToolStripPanel.ResumeLayout(false);
            this.toolStripContainer.TopToolStripPanel.PerformLayout();
            this.toolStripContainer.ResumeLayout(false);
            this.toolStripContainer.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.mainSplit.Panel1.ResumeLayout(false);
            this.mainSplit.Panel2.ResumeLayout(false);
            this.mainSplit.ResumeLayout(false);
            this.mainMenu.ResumeLayout(false);
            this.mainMenu.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStripContainer toolStripContainer;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.MenuStrip mainMenu;
        private System.Windows.Forms.SplitContainer mainSplit;
        private System.Windows.Forms.ToolStripMenuItem menuFileItem;
        private System.Windows.Forms.ToolStripMenuItem menuNewProjectItem;
        private ImageList imageList;
        private ImageTabControl imageTabs;
        private System.Windows.Forms.ToolStripMenuItem menuEditItem;
        private System.Windows.Forms.ToolStripMenuItem menuViewItem;
        private System.Windows.Forms.ToolStripMenuItem menuShowTagTypeItem;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tagTypeFormViewButton;
        private System.Windows.Forms.ToolStripButton tagFormViewButton;
        private System.Windows.Forms.ToolStripMenuItem menuOpenProjectItem;
        private System.Windows.Forms.ToolStripMenuItem menuOverwriteItem;
        private System.Windows.Forms.ToolStripMenuItem menuSaveasItem;
        private System.Windows.Forms.ToolStripMenuItem menuShowTagItem;
        private System.Windows.Forms.ToolStripMenuItem menuExportCommandItem;
        private System.Windows.Forms.ToolStripButton showUnusedButton;
        private System.Windows.Forms.ToolStripProgressBar statusProgressBar;
        private System.Windows.Forms.ToolStripStatusLabel statusProgressLabel;
    }
}

