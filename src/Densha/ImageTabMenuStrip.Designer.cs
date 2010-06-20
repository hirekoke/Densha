namespace Densha
{
    partial class ImageTabMenuStrip
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

        #region コンポーネント デザイナで生成されたコード

        /// <summary> 
        /// デザイナ サポートに必要なメソッドです。このメソッドの内容を 
        /// コード エディタで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.closeTabItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeAllTabsItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SuspendLayout();
            // 
            // closeTabItem
            // 
            this.closeTabItem.Name = "closeTabItem";
            this.closeTabItem.Size = new System.Drawing.Size(184, 22);
            this.closeTabItem.Tag = "CloseTab";
            this.closeTabItem.Text = "タブを閉じる";
            // 
            // closeAllTabsItem
            // 
            this.closeAllTabsItem.Name = "closeAllTabsItem";
            this.closeAllTabsItem.Size = new System.Drawing.Size(184, 22);
            this.closeAllTabsItem.Text = "全てのタブを閉じる";
            // 
            // ImageTabMenuStrip
            // 
            this.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.closeTabItem,
            this.closeAllTabsItem});
            this.Size = new System.Drawing.Size(185, 48);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStripMenuItem closeTabItem;
        private System.Windows.Forms.ToolStripMenuItem closeAllTabsItem;
    }
}
