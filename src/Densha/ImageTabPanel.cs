using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Densha
{
    partial class ImageTabPanel : TabPage
    {
        public ImageTabPanel(DenshaImage img)
        {
            InitializeComponent();

            this.AllowDrop = true;

            _dImage = img;
            if (img != null)
            {
                pictureBox.WaitOnLoad = false;
                pictureBox.ImageLocation = _dImage.OriginalFullPath;
                Text = _dImage.OriginalName;
                checkBox.Checked = _dImage.IsUsed;
            }

            checkBox.CheckedChanged += new EventHandler(checkBox_CheckedChanged);
            pictureBox.DoubleClick += new EventHandler(pictureBox_DoubleClick);
        }

        private DenshaImage _dImage = null;
        public DenshaImage DenshaImage
        {
            get { return _dImage; }
        }

        public void SetUse(object sender, DenshaImage image, bool use)
        {
            if (sender != this && image == _dImage)
            {
                checkBox.Checked = use;
            }
        }

        protected override void OnDragOver(DragEventArgs drgevent)
        {
            base.OnDragOver(drgevent);
            if (drgevent.Data.GetDataPresent(Properties.Resources.DndIdTag))
            {
                drgevent.Effect = DragDropEffects.Copy;
            }
        }
        protected override void OnDragDrop(DragEventArgs drgevent)
        {
            base.OnDragDrop(drgevent);

            if (drgevent.Data.GetDataPresent(Properties.Resources.DndIdTag))
            {
                Tag tag = drgevent.Data.GetData(Properties.Resources.DndIdTag) as Tag;
                if (tag != null)
                {
                    Program.MainForm.AddTag(this, _dImage, tag);
                    Program.MainForm.UpdateImage(this, _dImage);
                }
            }
        }

        private void checkBox_CheckedChanged(object sender, EventArgs e)
        {
            if (_dImage == null) return;
            Program.MainForm.SetUse(this, _dImage, checkBox.Checked);
        }

        private void pictureBox_DoubleClick(object sender, EventArgs e)
        {
            if (_dImage == null) return;
            Program.MainForm.ScrollListToImage(_dImage);
        }
    }
}
