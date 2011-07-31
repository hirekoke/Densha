using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Densha.view
{
    partial class ImageTabMenuStrip : ContextMenuStrip
    {
        private ImageTabPanel _imageTabPage = null;
        public ImageTabPanel ImageTabPage
        {
            get { return _imageTabPage; }
            set { _imageTabPage = value; }
        }

        public ImageTabMenuStrip()
        {
            InitializeComponent();

            closeTabItem.Click += new EventHandler(closeTabItem_Click);
            closeAllTabsItem.Click += new EventHandler(closeAllTabsItem_Click);
        }

        void closeAllTabsItem_Click(object sender, EventArgs e)
        {
            if (_imageTabPage != null)
            {
                ImageTabControl ctrl = _imageTabPage.Parent as ImageTabControl;
                ctrl.ClearTabs();
                _imageTabPage = null;
            }
        }

        void closeTabItem_Click(object sender, EventArgs e)
        {
            if (_imageTabPage != null)
            {
                ImageTabControl ctrl = _imageTabPage.Parent as ImageTabControl;
                ctrl.RemoveTab(_imageTabPage);
                _imageTabPage = null;
            }
        }
    }
}
