using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Densha
{
    public partial class CreateProjectForm : Form
    {
        public CreateProjectForm(string origDir, string thumbDir)
        {
            InitializeComponent();

            originalDirBox.Text = System.IO.Path.GetFullPath(origDir);
            thumbnailDirBox.Text = System.IO.Path.GetFullPath(thumbDir);
            useExistThumbDirButton.Checked = true;
            thumbPatternBox.Text = _thumbnailNamePattern;

            okButton.Click += new EventHandler(okButton_Click);
            cancelButton.Click += new EventHandler(cancelButton_Click);

            originalDirSelectButton.Click += new EventHandler(originalDirSelectButton_Click);
            thumbnailDirSelectButton.Click += new EventHandler(thumbnailDirSelectButton_Click);
        }

        private string _originalDirPath = AppDomain.CurrentDomain.BaseDirectory;
        public string OriginalDir
        {
            get { return Utilities.GetRelativePath(_originalDirPath); }
        }

        private string _thumbnailDirPath = AppDomain.CurrentDomain.BaseDirectory;
        public string ThumbnailDir
        {
            get { return Utilities.GetRelativePath(_thumbnailDirPath); }
        }

        private bool _createThumbnail = false;
        public bool CreateThumbnail
        {
            get { return _createThumbnail; }
        }

        private string _thumbnailNamePattern = Config.Instance.DefaultThumbnailNamePattern;
        public string ThumbnailNamePattern
        {
            get { return _thumbnailNamePattern; }
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            if (updateData())
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show(this, "サムネイルの生成方法を選択してください", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }


        private bool updateData()
        {
            _originalDirPath = Utilities.GetRelativePath(originalDirBox.Text);
            _thumbnailDirPath = Utilities.GetRelativePath(thumbnailDirBox.Text);
            _thumbnailNamePattern = string.IsNullOrEmpty(thumbPatternBox.Text) ? 
                Config.Instance.DefaultThumbnailNamePattern : thumbPatternBox.Text.Trim();
            if (createThumbDirButton.Checked || useExistThumbDirButton.Checked)
            {
                _createThumbnail = createThumbDirButton.Checked;
                return true;
            }
            return false;
        }

        private void originalDirSelectButton_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog dlg = new FolderBrowserDialog())
            {
                dlg.SelectedPath = originalDirBox.Text;
                dlg.ShowNewFolderButton = true;
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    originalDirBox.Text = dlg.SelectedPath;
                    _originalDirPath = Utilities.GetRelativePath(originalDirBox.Text);
                }
            }
        }
        private void thumbnailDirSelectButton_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog dlg = new FolderBrowserDialog())
            {
                dlg.SelectedPath = thumbnailDirBox.Text;
                dlg.ShowNewFolderButton = true;
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    thumbnailDirBox.Text = dlg.SelectedPath;
                    _thumbnailDirPath = Utilities.GetRelativePath(thumbnailDirBox.Text);
                }
            }
        }

    }
}
