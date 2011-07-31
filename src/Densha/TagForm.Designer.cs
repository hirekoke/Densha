namespace Densha
{
    partial class TagForm
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
            this.tagGridView = new System.Windows.Forms.DataGridView();
            this.tagTypeCollectionBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.tagCollectionBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.FileString = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.descriptionDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Type = new System.Windows.Forms.DataGridViewComboBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.tagGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tagTypeCollectionBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tagCollectionBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // tagGridView
            // 
            this.tagGridView.AllowUserToOrderColumns = true;
            this.tagGridView.AutoGenerateColumns = false;
            this.tagGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tagGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.FileString,
            this.descriptionDataGridViewTextBoxColumn,
            this.Type});
            this.tagGridView.DataSource = this.tagCollectionBindingSource;
            this.tagGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tagGridView.Location = new System.Drawing.Point(0, 0);
            this.tagGridView.Name = "tagGridView";
            this.tagGridView.RowTemplate.Height = 21;
            this.tagGridView.Size = new System.Drawing.Size(428, 337);
            this.tagGridView.TabIndex = 0;
            // 
            // tagTypeCollectionBindingSource
            // 
            this.tagTypeCollectionBindingSource.DataSource = typeof(Densha.TagTypeCollection);
            // 
            // tagCollectionBindingSource
            // 
            this.tagCollectionBindingSource.AllowNew = true;
            this.tagCollectionBindingSource.DataSource = typeof(Densha.TagCollection);
            // 
            // FileString
            // 
            this.FileString.DataPropertyName = "FileString";
            this.FileString.HeaderText = "ファイル用文字列";
            this.FileString.Name = "FileString";
            this.FileString.Width = 120;
            // 
            // descriptionDataGridViewTextBoxColumn
            // 
            this.descriptionDataGridViewTextBoxColumn.DataPropertyName = "Description";
            this.descriptionDataGridViewTextBoxColumn.HeaderText = "説明";
            this.descriptionDataGridViewTextBoxColumn.Name = "descriptionDataGridViewTextBoxColumn";
            // 
            // Type
            // 
            this.Type.DataPropertyName = "Type";
            this.Type.DataSource = this.tagTypeCollectionBindingSource;
            this.Type.DisplayMember = "Name";
            this.Type.HeaderText = "タグタイプ";
            this.Type.Name = "Type";
            this.Type.ValueMember = "Self";
            // 
            // TagForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(428, 337);
            this.Controls.Add(this.tagGridView);
            this.Name = "TagForm";
            this.Text = "TagForm";
            ((System.ComponentModel.ISupportInitialize)(this.tagGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tagTypeCollectionBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tagCollectionBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView tagGridView;
        private System.Windows.Forms.BindingSource tagCollectionBindingSource;
        private System.Windows.Forms.BindingSource tagTypeCollectionBindingSource;
        private System.Windows.Forms.DataGridViewTextBoxColumn FileString;
        private System.Windows.Forms.DataGridViewTextBoxColumn descriptionDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewComboBoxColumn Type;
    }
}