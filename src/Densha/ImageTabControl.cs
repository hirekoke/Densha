using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

using System.Runtime.InteropServices;

namespace Densha
{
    partial class ImageTabControl : TabControl
    {
        public ImageTabControl()
        {
            InitializeComponent();
            _contextMenu = new ImageTabMenuStrip();
            this.AllowDrop = true;
        }

        private ImageTabMenuStrip _contextMenu = null;
        private int _contextMenuIdx = -1;
        private Dictionary<string, ImageTabPanel> _openTabs = new Dictionary<string, ImageTabPanel>();

        public void AddTab(DenshaImage image)
        {
            if (_openTabs.ContainsKey(image.Id))
            {
                ImageTabPanel tab = _openTabs[image.Id];
                this.SelectTab(tab);
            }
            else
            {
                ImageTabPanel tab = new ImageTabPanel(image);
                this.TabPages.Add(tab);
                this.SelectTab(tab);
                _openTabs.Add(image.Id, tab);
            }
        }
        public void RemoveTab(ImageTabPanel tab)
        {
            if (_openTabs.ContainsKey(tab.DenshaImage.Id))
            {
                _openTabs.Remove(tab.DenshaImage.Id);
                this.TabPages.Remove(tab);
                tab.Dispose();
            }
        }
        public void ClearTabs()
        {
            this.TabPages.Clear();
            foreach (KeyValuePair<string, ImageTabPanel> kv in _openTabs)
            {
                kv.Value.Dispose();
            }
            _openTabs.Clear();
        }

        public int HitTest(int x, int y)
        {
            TCHITTESTINFO htInfo = new TCHITTESTINFO(x, y);
            int idx = SendMessage(this.Handle, (int)TabCtrlMessage.HITTEST, IntPtr.Zero, ref htInfo).ToInt32();
            return idx;
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            if (e.Button == MouseButtons.Right)
            {
                _contextMenuIdx = HitTest(e.X, e.Y);
                ImageTabPanel hotTab = TabPages[_contextMenuIdx] as ImageTabPanel;
                _contextMenu.ImageTabPage = hotTab;
                this.ContextMenuStrip = _contextMenu;
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            this.ContextMenuStrip = null;
            _contextMenuIdx = -1;
        }

        protected override void OnDragOver(DragEventArgs drgevent)
        {
            base.OnDragOver(drgevent);
            if (drgevent.Data.GetDataPresent(Properties.Resources.DndIdTag))
            {
                Point pc = this.PointToClient(new Point(drgevent.X, drgevent.Y));
                _contextMenuIdx = HitTest(pc.X, pc.Y);
                ImageTabPanel hotTab = TabPages[_contextMenuIdx] as ImageTabPanel;
                if (hotTab != null)
                {
                    drgevent.Effect = DragDropEffects.Copy;
                }
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
                    Point pc = this.PointToClient(new Point(drgevent.X, drgevent.Y));
                    _contextMenuIdx = HitTest(pc.X, pc.Y);
                    ImageTabPanel hotTab = TabPages[_contextMenuIdx] as ImageTabPanel;
                    if (hotTab != null)
                    {
                        Program.MainForm.AddTag(this, hotTab.DenshaImage, tag);
                        Program.MainForm.UpdateImage(this, hotTab.DenshaImage);
                    }
                }
            }
        }

        #region Tab Hit Test
        private enum TabCtrlMessage : int
        {
            HITTEST = 0x130D,
        }

        [Flags()]
        private enum TCHITTESTFLAGS
        {
            TCHT_NOWHERE = 1,
            TCHT_ONITEMICON = 2,
            TCHT_ONITEMLABEL = 4,
            TCHT_ONITEM = TCHT_ONITEMICON | TCHT_ONITEMLABEL
        }
        [StructLayout(LayoutKind.Sequential)]
        private struct TCHITTESTINFO
        {
            public Point pt;
            public TCHITTESTFLAGS flags;
            public TCHITTESTINFO(int x, int y)
            {
                pt = new Point(x, y);
                flags = TCHITTESTFLAGS.TCHT_ONITEM;
            }
        }

        [DllImport("user32.dll")]
        private static extern IntPtr SendMessage(IntPtr hwnd, int msg, IntPtr wParam, ref TCHITTESTINFO lParam);
        #endregion

    }
}
