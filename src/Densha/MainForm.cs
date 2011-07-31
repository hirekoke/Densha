using System;
using System.ComponentModel;
using System.Windows.Forms;

using Densha;

namespace Densha.view
{
    partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

            menuNewProjectItem.Click += new EventHandler(menuNewProjectItem_Click);
            menuOpenProjectItem.Click += new EventHandler(menuOpenProjectItem_Click);
            menuOverwriteItem.Click += new EventHandler(menuOverwriteItem_Click);
            menuSaveasItem.Click += new EventHandler(menuSaveasItem_Click);

            menuExportCommandItem.Click += new EventHandler(menuExportCommandItem_Click);
            menuConfigItem.Click += new EventHandler(menuConfigItem_Click);

            menuShowTagTypeItem.Click += new EventHandler(menuShowTagTypeItem_Click);
            menuShowTagItem.Click += new EventHandler(menuShowTagItem_Click);

            tagTypeFormViewButton.Click += new EventHandler(tagTypeFormViewButton_Click);
            tagFormViewButton.Click += new EventHandler(tagFormViewButton_Click);
            showUnusedButton.CheckedChanged += new EventHandler(showUnusedButton_CheckedChanged);

            toolStripButton1.Click += new EventHandler(toolStripButton1_Click);
        }

        void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (_project == null) return;
            ImageListItem i = null;
            foreach (ImageListItem item in imageList.SelectedItems)
            {
                i = item; break;
            }
            if (i != null)
            {
                imageList.SelectTimeCluster(i);
                imageList.Invalidate();
            }
            //timeline.TimeLineForm frm = new timeline.TimeLineForm(imageList.Items);
            //frm.ShowDialog();
        }

        private TagTypeForm _tagTypeForm = null;
        private TagForm _tagForm = null;

        private Project _project = null;
        public Project Project
        {
            get { return _project; }
            set
            {
                _project = value;
                setProject();
            }
        }
        private bool _projectChanged = false;

        void _project_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            _projectChanged = true;
            this.Text = createFormTitle();

            if (Project != null && e.PropertyName == "UsedCount")
            {
                usingImageStatusUsing.Text = Project.UsedCount.ToString();
            }
        }

        public ImageList ImageList { get { return imageList; } }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            e.Cancel = ConfirmAbandonProject();
        }

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

        /// <summary>
        /// プロジェクトを破棄するかどうかのチェック
        /// </summary>
        /// <returns>次の操作をキャンセルするかどうか</returns>
        public bool ConfirmAbandonProject()
        {
            bool cancel = false;
            if (Project != null && _projectChanged)
            {
                DialogResult result =
                    MessageBox.Show(Properties.Resources.Message_ConfirmSaveProject,
                    Properties.Resources.Title_MyName,
                    MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                switch (result)
                {
                    case DialogResult.Yes:
                        SaveProject(true);
                        break;
                    case DialogResult.No:
                        break;
                    case DialogResult.Cancel:
                        cancel = true;
                        break;
                }
            }
            return cancel;
        }

        private string createFormTitle()
        {
            if (Project == null) return Properties.Resources.Title_MyName;

            if (Project.ProjectFilePath == null)
            {
                return Properties.Resources.Title_NewProject + " - " + Properties.Resources.Title_MyName;
            }
            else
            {
                return (_projectChanged ? Properties.Resources.Title_ProjectChangedMark : "") +
                    Project.ProjectFilePath + " - " + Properties.Resources.Title_MyName;
            }
        }

        #region メニュー・ツールバー イベントハンドラ
        private void menuNewProjectItem_Click(object sender, EventArgs e)
        {
            if(!ConfirmAbandonProject())
                CreateNewProject();
        }
        private void menuOpenProjectItem_Click(object sender, EventArgs e)
        {
            if(!ConfirmAbandonProject())
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

        private void menuConfigItem_Click(object sender, EventArgs e)
        {
            ShowConfig();
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
            if (image.IsUsed != use)
            {
                image.IsUsed = use;
            }

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
        public void AddTags(object sender, Tag tag)
        {
            if (Project != null)
            {
                foreach (ImageListItem item in imageList.SelectedItems)
                {
                    if (imageList.ShowUnusedItems || item.DenshaImage.IsUsed)
                    {
                        AddTag(sender, item, tag);
                    }
                }
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
        public void RemoveTags(object sender, Tag tag)
        {
            if (Project != null)
            {
                foreach (ImageListItem item in imageList.SelectedItems)
                {
                    if (imageList.ShowUnusedItems || item.DenshaImage.IsUsed)
                    {
                        RemoveTag(sender, item, tag);
                    }
                }
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

        public void ShowConfig()
        {
            using (ConfigForm cf = new ConfigForm())
            {
                if (cf.ShowDialog() == DialogResult.OK)
                {
                    imageList.Invalidate();
                    imageTabs.Invalidate();
                }
            }
        }
        #endregion

        #region タグ設定フォーム
        public void ShowTagTypeForm(object sender, bool visible)
        {
            if (_tagTypeForm == null || _tagTypeForm.IsDisposed || !_tagTypeForm.IsHandleCreated)
            {
                _tagTypeForm = new TagTypeForm();
                _tagTypeForm.Owner = this;
            }
            if (_tagTypeForm != null && !_tagTypeForm.Disposing && sender != _tagTypeForm)
            {
                if (Project != null)
                {
                    _tagTypeForm.TagTypes = Project.TagTypes;
                }
                else
                {
                    //_tagTypeForm.TagTypes = null;
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
            if (_tagForm == null || _tagForm.IsDisposed || !_tagForm.IsHandleCreated)
            {
                _tagForm = new TagForm();
                _tagForm.Owner = this;
            }
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

        private ProgressForm _progressForm = null;
        private void startLoadProject()
        {
            ShowTagTypeForm(this, false);
            ShowTagForm(this, false);

            this.Enabled = false;
            _progressForm = new ProgressForm();
            _progressForm.StartPosition = FormStartPosition.CenterParent;
            _progressForm.Show(this);
        }
        private void endLoadProject()
        {
            _progressForm.Close();
            _progressForm.Dispose();
            _progressForm = null;
            this.Enabled = true;
            this.Activate();
        }

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
                    startLoadProject();

                    BackgroundWorker createProjectWorker = new BackgroundWorker();
                    createProjectWorker.WorkerReportsProgress = true;
                    createProjectWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(onProjectLoadComplete);

                    createProjectWorker.DoWork += delegate(object sender, DoWorkEventArgs e)
                    {
                        BackgroundWorker worker = (BackgroundWorker)sender;
                        string[] args = e.Argument as string[];
                        e.Result = Project.CreateProject(args[0], args[1], args[2], worker);
                    };

                    _progressForm.Worker = createProjectWorker;
                    createProjectWorker.RunWorkerAsync(new string[] { frm.OriginalDir, frm.ThumbnailDir, frm.ThumbnailNamePattern });
                }
            }
        }

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
                dlg.Title = Properties.Resources.Title_OpenProjectFile;
                dlg.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory;
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    startLoadProject();

                    BackgroundWorker loadProjectWorker = new BackgroundWorker();
                    loadProjectWorker.WorkerReportsProgress = true;
                    loadProjectWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(onProjectLoadComplete);

                    loadProjectWorker.DoWork += delegate(object sender, DoWorkEventArgs e)
                    {
                        try
                        {
                            BackgroundWorker worker = (BackgroundWorker)sender;
                            e.Result = Project.Load(e.Argument as string, worker);
                        }
                        catch (errors.ProjectParseError ex)
                        {
                            errors.ErrorHandler.HandleProjectParseError(ex);
                            e.Result = null;
                        }
                    };

                    _progressForm.Worker = loadProjectWorker;
                    loadProjectWorker.RunWorkerAsync(dlg.FileName);
                }
            }
        }

        void onProjectLoadComplete(object sender, RunWorkerCompletedEventArgs e)
        {
            // set project
            Project project = e.Result as Project;
            if (project != null)
            {
                Project = project;
            }
            else
            {
                Project = null;
            }
        }

        #region setProject
        private void setProject()
        {
            BackgroundWorker setProjectWorker = new BackgroundWorker();
            setProjectWorker.WorkerReportsProgress = true;
            setProjectWorker.DoWork += new DoWorkEventHandler(setProjectWorker_DoWork);
            setProjectWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(setProjectWorker_RunWorkerCompleted);
            _progressForm.Worker = setProjectWorker;
            setProjectWorker.RunWorkerAsync();
        }

        private void setProjectWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker bgWorker = (BackgroundWorker)sender;
            bgWorker.ReportProgress(0, "set project");

            lock (imageList.Items)
            {
                imageList.Items.Clear();
                if (Project != null)
                {
                    int imgCount = Project.Images.Count;
                    int i = 0;
                    foreach (DenshaImage img in Project.Images)
                    {
                        ImageListItem item = new ImageListItem(img);
                        imageList.Items.Add(item);
                        bgWorker.ReportProgress((int)(++i * 100 / (double)imgCount), "set image");
                    }
                }
            }
            if (Project != null)
            {
                Project.PropertyChanged += new PropertyChangedEventHandler(_project_PropertyChanged);
            }
        }
        void setProjectWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            endLoadProject();
            this.Text = createFormTitle();
            usingImageStatusAll.Text = Project.Images.Count.ToString();
            usingImageStatusUsing.Text = Project.UsedCount.ToString();
            _projectChanged = false;
            imageList.Invalidate();
        }
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
                        dlg.Title = Properties.Resources.Title_SaveProjectFile;
                        
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

                    _projectChanged = false;
                    this.Text = createFormTitle();
                }
            }
            else
            {

            }
        }

        public void ExportCommand()
        {
            if (Project != null)
            {
                using (CommandForm frm = new CommandForm(Project))
                {
                    frm.ShowDialog();
                }
            }
        }
        #endregion


    }
}
