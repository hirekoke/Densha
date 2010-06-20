using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Densha
{
    partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

            _tagTypeForm = new TagTypeForm();
            _tagTypeForm.Owner = this;
            _tagForm = new TagForm();
            _tagForm.Owner = this;


            menuNewProjectItem.Click += new EventHandler(menuNewProjectItem_Click);
            menuOpenProjectItem.Click += new EventHandler(menuOpenProjectItem_Click);
            menuOverwriteItem.Click += new EventHandler(menuOverwriteItem_Click);
            menuSaveasItem.Click += new EventHandler(menuSaveasItem_Click);

            menuExportCommandItem.Click += new EventHandler(menuExportCommandItem_Click);

            menuShowTagTypeItem.Click += new EventHandler(menuShowTagTypeItem_Click);
            menuShowTagItem.Click += new EventHandler(menuShowTagItem_Click);

            tagTypeFormViewButton.Click += new EventHandler(tagTypeFormViewButton_Click);
            tagFormViewButton.Click += new EventHandler(tagFormViewButton_Click);
            showUnusedButton.CheckedChanged += new EventHandler(showUnusedButton_CheckedChanged);
        }


        private TagTypeForm _tagTypeForm = null;
        private TagForm _tagForm = null;
        private Project _project = null;
        public Project Project
        {
            get { return _project; }
            set
            {
                if (_project != value)
                {
                    _project = value;
                    SetProject();
                }
            }
        }

        public ImageList ImageList { get { return imageList; } }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            if (imageList.Bounds.Contains(e.Location))
            {
                MouseEventArgs ne = new MouseEventArgs(e.Button, e.Clicks,
                    e.X + imageList.Bounds.X, e.Y + imageList.Bounds.Y,
                    e.Delta);
                imageList.FireMouseWheel(ne);
            }
            else
            {
                base.OnMouseWheel(e);
            }
        }

        #region メニュー・ツールバー イベントハンドラ
        private void menuNewProjectItem_Click(object sender, EventArgs e)
        {
            CreateNewProject();
        }
        private void menuOpenProjectItem_Click(object sender, EventArgs e)
        {
            LoadProject();
        }
        private void menuSaveasItem_Click(object sender, EventArgs e)
        {
            SaveProject(true);
        }
        private void menuOverwriteItem_Click(object sender, EventArgs e)
        {
            SaveProject(false);
        }

        private void menuExportCommandItem_Click(object sender, EventArgs e)
        {
            ExportCommand();
        }

        void menuShowTagTypeItem_Click(object sender, EventArgs e)
        {
            ShowTagTypeForm(menuShowTagTypeItem, menuShowTagTypeItem.Checked);
        }
        void menuShowTagItem_Click(object sender, EventArgs e)
        {
            ShowTagForm(menuShowTagItem, menuShowTagItem.Checked);
        }


        private void tagTypeFormViewButton_Click(object sender, EventArgs e)
        {
            ShowTagTypeForm(tagTypeFormViewButton, tagTypeFormViewButton.Checked);
        }
        private void tagFormViewButton_Click(object sender, EventArgs e)
        {
            ShowTagForm(tagFormViewButton, tagFormViewButton.Checked);
        }
        private void showUnusedButton_CheckedChanged(object sender, EventArgs e)
        {
            SetShowUnusedItem(showUnusedButton, showUnusedButton.Checked);
        }

        #endregion

        #region 更新・操作

        public void ShowLargeImage(DenshaImage image)
        {
            imageTabs.AddTab(image);
        }

        public void SetShowUnusedItem(object sender, bool show)
        {
            imageList.ShowUnusedItems = show;
        }
        public void ScrollListToImage(DenshaImage image)
        {
            imageList.ScrollToImage(image);
        }


        public void SetUse(object sender, DenshaImage image, bool use)
        {
            image.IsUsed = use;

            imageList.Invalidate();

            // tabの方
            foreach (ImageTabPanel tab in imageTabs.TabPages)
            {
                tab.SetUse(sender, image, use);
            }
        }

        public void AddTag(object sender, ImageListItem item, Tag tag)
        {
            if (item.DenshaImage.AddTag(tag))
            {
                item.UpdateTagLayout();
                imageList.CloseTextBox();
            }
        }
        public void AddTag(object sender, DenshaImage image, Tag tag)
        {
            ImageListItem item = imageList.Items.Find(delegate(ImageListItem i)
            {
                return i.DenshaImage.Id == image.Id;
            });
            if (item != null)
            {
                AddTag(sender, item, tag);
            }
        }
        public void RemoveTag(object sender, ImageListItem item, Tag tag)
        {
            if (item.DenshaImage.RemoveTag(tag))
            {
                item.UpdateTagLayout();
                imageList.CloseTextBox();
            }
        }

        public void UpdateImage(object sender, DenshaImage image)
        {
            if (sender != imageList)
            {
                imageList.UpdateImage(image);
                imageList.CloseTextBox();
            }
        }
        public void UpdateTag(object sender, Tag tag)
        {
            // tag sort

            if (sender != imageList)
            {
                imageList.UpdateTag(tag);
            }
        }
        public void UpdateTagType(object sender, TagType type)
        {
            if (sender != imageList)
            {
                imageList.UpdateTagType(type);
            }
        }
        #endregion

        #region タグ設定フォーム
        public void ShowTagTypeForm(object sender, bool visible)
        {
            if (_tagTypeForm != null && !_tagTypeForm.Disposing && sender != _tagTypeForm)
            {
                if (Project != null)
                {
                    _tagTypeForm.TagTypes = Project.TagTypes;
                }
                else
                {
                    _tagTypeForm.TagTypes = null;
                }
                _tagTypeForm.Visible = visible;
            }

            if (sender != tagTypeFormViewButton)
                tagTypeFormViewButton.Checked = visible;
            if (sender != menuShowTagTypeItem)
                menuShowTagTypeItem.Checked = visible;
        }
        public void ShowTagForm(object sender, bool visible)
        {
            bool open = false;
            if (_tagForm != null && !_tagForm.Disposing && sender != _tagForm)
            {
                if (Project != null)
                {
                    _tagForm.TagTypes = Project.TagTypes;
                    _tagForm.Tags = Project.Tags;
                    _tagForm.Visible = visible;
                    if (visible) open = true;
                }
                else
                {
                    _tagForm.TagTypes = null;
                    _tagForm.Tags = null;
                }
            }

            bool tmp = visible && open;

            if (tagFormViewButton.Checked != tmp)
                tagFormViewButton.Checked = tmp;
            if (menuShowTagItem.Checked != tmp)
                menuShowTagItem.Checked = tmp;
        }
        #endregion

        #region 読み込み・保存

        private void suspendGUI()
        {
            statusProgressBar.Maximum = Config.Instance.StatusProgressBarWidth;
            statusProgressBar.Width = Config.Instance.StatusProgressBarWidth;

            ShowTagTypeForm(this, false);
            ShowTagForm(this, false);
            this.MainMenuStrip.Enabled = false;
            this.toolStrip1.Enabled = false;
            this.imageList.Enabled = false;
            this.imageTabs.Enabled = false;

            statusProgressLabel.Text = "";
            statusProgressBar.Value = 0;
            statusProgressLabel.Visible = true;
            statusProgressBar.Visible = true;
        }
        private void resumeGUI()
        {
            this.MainMenuStrip.Enabled = true;
            this.toolStrip1.Enabled = true;
            this.imageList.Enabled = true;
            this.imageTabs.Enabled = true;

            statusProgressLabel.Text = "";
            statusProgressBar.Value = 0;
            statusProgressLabel.Visible = false;
            statusProgressBar.Visible = false;
        }


        #region SetProject
        public void SetProject()
        {
            suspendGUI();
            BackgroundWorker setProjectWorker = new BackgroundWorker();
            setProjectWorker.DoWork += new DoWorkEventHandler(setProjectWorker_DoWork);
            setProjectWorker.ProgressChanged += new ProgressChangedEventHandler(setProjectWorker_ProgressChanged);
            setProjectWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(setProjectWorker_RunWorkerCompleted);
            setProjectWorker.RunWorkerAsync();
        }

        void setProjectWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            lock (imageList.Items)
            {
                imageList.Items.Clear();
                foreach (DenshaImage img in Project.Images)
                {
                    ImageListItem item = new ImageListItem(img);
                    imageList.Items.Add(item);
                }
            }
        }
        void setProjectWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            statusProgressLabel.Text = e.UserState.ToString();
            statusProgressBar.Value = e.ProgressPercentage;
        }
        void setProjectWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            resumeGUI();
            imageList.Invalidate();
        }
        #endregion

        #region CreateNewProject
        public void CreateNewProject()
        {
            ShowTagTypeForm(this, false);
            ShowTagForm(this, false);
            using (CreateProjectForm frm = new CreateProjectForm(
                AppDomain.CurrentDomain.BaseDirectory,
                AppDomain.CurrentDomain.BaseDirectory))
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    suspendGUI();

                    BackgroundWorker createProjectWorker = new BackgroundWorker();
                    createProjectWorker.WorkerReportsProgress = true;
                    createProjectWorker.DoWork += new DoWorkEventHandler(createProjectWorker_DoWork);
                    createProjectWorker.ProgressChanged += new ProgressChangedEventHandler(createProjectWorker_ProgressChanged);
                    createProjectWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(createProjectWorker_RunWorkerCompleted);
                    createProjectWorker.RunWorkerAsync(frm);
                }
            }
        }

        void createProjectWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            resumeGUI();
            // set project
            Project project = e.Result as Project;
            if (project != null) Project = project;
        }

        void createProjectWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            statusProgressLabel.Text = e.UserState.ToString();
            statusProgressBar.Value = e.ProgressPercentage;
        }

        void createProjectWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            CreateProjectForm frm = e.Argument as CreateProjectForm;
            BackgroundWorker worker = (BackgroundWorker)sender;
            e.Result = Project.CreateProject(frm.OriginalDir, frm.ThumbnailDir,
                frm.ThumbnailNamePattern, worker);
        }
        #endregion

        #region LoadProject
        public void LoadProject()
        {
            ShowTagTypeForm(this, false);
            ShowTagForm(this, false);
            using (OpenFileDialog dlg = new OpenFileDialog())
            {
                dlg.Multiselect = false;
                dlg.CheckFileExists = true;
                dlg.CheckPathExists = true;
                dlg.AutoUpgradeEnabled = true;
                dlg.DereferenceLinks = true;
                dlg.Filter = Properties.Resources.ProjectFileFilter;
                dlg.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory;
                dlg.Title = "プロジェクトを開く";
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    suspendGUI();

                    BackgroundWorker loadWorker = new BackgroundWorker();
                    loadWorker.WorkerReportsProgress = true;
                    loadWorker.DoWork += new DoWorkEventHandler(loadWorker_DoWork);
                    loadWorker.ProgressChanged += new ProgressChangedEventHandler(loadWorker_ProgressChanged);
                    loadWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(loadWorker_RunWorkerCompleted);
                    loadWorker.RunWorkerAsync(dlg.FileName);
                }
            }
        }
        #region loadWorker
        void loadWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            resumeGUI();
            // set project
            Project project = e.Result as Project;
            if (project != null) Project = project;
        }

        void loadWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            statusProgressLabel.Text = e.UserState.ToString();
            statusProgressBar.Value = e.ProgressPercentage;
        }

        void loadWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            // load project
            BackgroundWorker worker = (BackgroundWorker)sender;
            e.Result = Project.Load(e.Argument as string, worker);
        }
        #endregion
        #endregion

        public void SaveProject(bool saveas)
        {
            if (Project != null)
            {
                string fn = Project.ProjectFilePath;
                if (string.IsNullOrEmpty(fn) || saveas)
                {
                    using (SaveFileDialog dlg = new SaveFileDialog())
                    {
                        dlg.AutoUpgradeEnabled = true;
                        dlg.DefaultExt = ".xml";
                        dlg.Filter = Properties.Resources.ProjectFileFilter;
                        dlg.Title = "プロジェクトを保存";
                        if (string.IsNullOrEmpty(fn))
                        {
                            dlg.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory;
                        }
                        else
                        {
                            dlg.InitialDirectory = System.IO.Path.GetDirectoryName(fn);
                            dlg.FileName = System.IO.Path.GetFileName(fn);
                        }

                        if (dlg.ShowDialog() == DialogResult.OK)
                        {
                            fn = dlg.FileName;
                        }
                        else
                        {
                            fn = null;
                        }
                    }
                }
                if (fn != null)
                {
                    Project.Save(fn);
                    Project.ProjectFilePath = fn;
                }
            }
            else
            {

            }
        }

        public void ExportCommand()
        {
            if (_project != null)
            {
                using (CommandForm frm = new CommandForm(_project))
                {
                    frm.ShowDialog();
                }
            }
        }
        #endregion

    }
}
