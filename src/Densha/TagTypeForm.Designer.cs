namespace Densha
{
    partial class TagTypeForm
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
            this.components = new System.ComponentModel.Container();
            this.tagTypeGridView = new System.Windows.Forms.DataGridView();
            this.tagTypeCollectionBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.priorityDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.tagTypeGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tagTypeCollectionBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // tagTypeGridView
            // 
            this.tagTypeGridView.AllowUserToOrderColumns = true;
            this.tagTypeGridView.AutoGenerateColumns = false;
            this.tagTypeGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tagTypeGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.priorityDataGridViewTextBoxColumn,
            this.nameDataGridViewTextBoxColumn});
            this.tagTypeGridView.DataSource = this.tagTypeCollectionBindingSource;
            this.tagTypeGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tagTypeGridView.Location = new System.Drawing.Point(0, 0);
            this.tagTypeGridView.Name = "tagTypeGridView";
            this.tagTypeGridView.RowTemplate.Height = 21;
            this.tagTypeGridView.Size = new System.Drawing.Size(365, 216);
            this.tagTypeGridView.TabIndex = 0;
            // 
            // tagTypeCollectionBindingSource
            // 
            this.tagTypeCollectionBindingSource.DataSource = typeof(Densha.TagTypeCollection);
            // 
            // priorityDataGridViewTextBoxColumn
            // 
            this.priorityDataGridViewTextBoxColumn.DataPropertyName = "Priority";
            this.priorityDataGridViewTextBoxColumn.HeaderText = "優先度";
            this.priorityDataGridViewTextBoxColumn.Name = "priorityDataGridViewTextBoxColumn";
            // 
            // nameDataGridViewTextBoxColumn
            // 
            this.nameDataGridViewTextBoxColumn.DataPropertyName = "Name";
            this.nameDataGridViewTextBoxColumn.HeaderText = "名前";
            this.nameDataGridViewTextBoxColumn.Name = "nameDataGridViewTextBoxColumn";
            // 
            // TagTypeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(365, 216);
            this.Controls.Add(this.tagTypeGridView);
            this.Name = "TagTypeForm";
            this.Text = "タグタイプ設定";
            ((System.ComponentModel.ISupportInitialize)(this.tagTypeGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tagTypeCollectionBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView tagTypeGridView;
        private System.Windows.Forms.BindingSource tagTypeCollectionBindingSource;
        private System.Windows.Forms.DataGridViewTextBoxColumn priorityDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn nameDataGridViewTextBoxColumn;
    }
}