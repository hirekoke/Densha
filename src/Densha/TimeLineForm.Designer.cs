namespace Densha.timeline
{
    partial class TimeLineForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TimeLineForm));
            this.timeLinePanel1 = new Densha.timeline.TimeLinePanel();
            this.SuspendLayout();
            // 
            // timeLinePanel1
            // 
            this.timeLinePanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.timeLinePanel1.Location = new System.Drawing.Point(0, 0);
            this.timeLinePanel1.Name = "timeLinePanel1";
            this.timeLinePanel1.Size = new System.Drawing.Size(409, 158);
            this.timeLinePanel1.TabIndex = 0;
            // 
            // TimeLineForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(409, 158);
            this.Controls.Add(this.timeLinePanel1);
            this.Name = "TimeLineForm";
            this.Text = "TimeHistogramForm";
            this.ResumeLayout(false);

        }

        #endregion

        private TimeLinePanel timeLinePanel1;


    }
}