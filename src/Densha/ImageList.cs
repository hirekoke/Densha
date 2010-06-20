using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Densha
{
    partial class ImageList : UserControl
    {
        private List<ImageListItem> _items = new List<ImageListItem>();
        public List<ImageListItem> Items
        {
            get { return _items; }
        }

        public IEnumerable<ImageListItem> SelectedItems
        {
            get
            {
                foreach (ImageListItem item in _items)
                {
                    if (item.Selected) yield return item;
                }
            }
        }

        private bool _showUnusedItems = true;
        public bool ShowUnusedItems
        {
            get { return _showUnusedItems; }
            set
            {
                if (_showUnusedItems != value)
                {
                    _showUnusedItems = value;
                    Invalidate();
                }
            }
        }

        private int _viewIndexMin = 0;
        private int _viewIndexMax = 0;
        private int _heightSum = 0;

        public ImageList()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.DoubleBuffer, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);

            AllowDrop = true;

            _recitalBox.TextBox = new TextBox();
            _recitalBox.ListItem = null;
            _recitalBox.TextBox.LostFocus += new EventHandler(_textBox_LostFocus);
            vScrollBar.ValueChanged += new EventHandler(vScrollBar_ValueChanged);
        }

        private void disposeImageListItems()
        {
            CloseTextBox();
            foreach (ImageListItem item in _items)
            {
                item.Dispose();
            }
        }

        #region イベント委譲
        public void FireMouseWheel(MouseEventArgs e)
        {
            OnMouseWheel(e);
        }

        private ImageListItem _prevHitItem = null;
        private MouseEventArgs convertMouseEvent(MouseEventArgs e, ImageListItem target)
        {
            MouseEventArgs oe = new MouseEventArgs(e.Button, e.Clicks,
                e.X - target.X, e.Y - (target.Y - vScrollBar.Value), e.Delta);
            return oe;
        }
        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);
            ImageListItem hit = HitTest(e.Location, 0, vScrollBar.Value);
            if (hit != null)
            {
                MouseEventArgs oe = convertMouseEvent(e, hit);
                if (!hit.OnMouseClick(oe))
                {
                    int hitIdx = _items.FindIndex(delegate(ImageListItem item)
                    {
                        return item.DenshaImage.Id == hit.DenshaImage.Id;
                    });

                    if (hitIdx >= 0)
                    {
                        // 処理されなかったクリック

                        CloseTextBox();

                        // 選択処理
                        _focusIdx = hitIdx;
                        if ((Control.ModifierKeys & Keys.Shift) == Keys.Shift)
                        {
                            SelectImages();
                        }
                        else if ((Control.ModifierKeys & Keys.Control) == Keys.Control)
                        {
                            _selectStartIdx = hitIdx;
                            ToggleSelectImage();
                        }
                        else
                        {
                            _selectStartIdx = hitIdx;
                            SelectImage();
                        }
                    }
                }
            }
            else
            {
                DeselectAllImages();
            }
            if (_prevHitItem != null && _prevHitItem != hit)
            {
                MouseEventArgs oe = convertMouseEvent(e, _prevHitItem);
                _prevHitItem.OnMouseLeave(oe);
            }
            Invalidate();
            _prevHitItem = hit;
        }
        protected override void OnMouseDoubleClick(MouseEventArgs e)
        {
            base.OnMouseDoubleClick(e);
            ImageListItem hit = HitTest(e.Location, 0, vScrollBar.Value);
            if (hit != null)
            {
                MouseEventArgs oe = convertMouseEvent(e, hit);
                hit.OnMouseDoubleClick(oe);
            }
            if (_prevHitItem != null && _prevHitItem != hit)
            {
                MouseEventArgs oe = convertMouseEvent(e, _prevHitItem);
                _prevHitItem.OnMouseLeave(oe);
            }
            Invalidate();
            _prevHitItem = hit;
        }
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            ImageListItem hit = HitTest(e.Location, 0, vScrollBar.Value);
            if (hit != null)
            {
                MouseEventArgs oe = convertMouseEvent(e, hit);
                hit.OnMouseDown(oe);
            }
            if (_prevHitItem != null && _prevHitItem != hit)
            {
                MouseEventArgs oe = convertMouseEvent(e, _prevHitItem);
                _prevHitItem.OnMouseLeave(oe);
            }
            Invalidate();
            _prevHitItem = hit;
        }
        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            ImageListItem hit = HitTest(e.Location, 0, vScrollBar.Value);
            if (hit != null)
            {
                MouseEventArgs oe = convertMouseEvent(e, hit);
                hit.OnMouseUp(oe);
            }
            if (_prevHitItem != null && _prevHitItem != hit)
            {
                MouseEventArgs oe = convertMouseEvent(e, _prevHitItem);
                _prevHitItem.OnMouseLeave(oe);
            }
            Invalidate();
            _prevHitItem = hit;
        }
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            ImageListItem hit = HitTest(e.Location, 0, vScrollBar.Value);
            if (hit != null)
            {
                MouseEventArgs oe = convertMouseEvent(e, hit);
                if (hit != _prevHitItem)
                {
                    hit.OnMouseEnter(oe);
                }
                hit.OnMouseMove(oe);
            }
            if (hit != _prevHitItem && _prevHitItem != null)
            {
                MouseEventArgs oe = convertMouseEvent(e, _prevHitItem);
                _prevHitItem.OnMouseLeave(oe);
            }
            Invalidate();
            _prevHitItem = hit;
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            Invalidate();
            _prevHitItem = null;
        }
        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            if (_prevHitItem != null)
            {
                _prevHitItem.OnMouseLeave(e);
            }
            Invalidate();
            _prevHitItem = null;
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            base.OnMouseWheel(e);

            int delta = (int)(e.Delta / 2.0);

            if (vScrollBar.Value - delta < vScrollBar.Minimum)
            {
                vScrollBar.Value = vScrollBar.Minimum;
            }
            else if (vScrollBar.Value - delta > vScrollBar.Maximum - vScrollBar.LargeChange)
            {
                vScrollBar.Value = vScrollBar.Maximum - vScrollBar.LargeChange;
            }
            else
            {
                vScrollBar.Value -= delta;
            }

            LayoutImages();

            ImageListItem hit = HitTest(e.Location, 0, vScrollBar.Value);
            if (hit != null)
            {
                MouseEventArgs oe = convertMouseEvent(e, hit);
                hit.OnMouseEnter(oe);
            }
            if (_prevHitItem != null && _prevHitItem != hit)
            {
                MouseEventArgs oe = convertMouseEvent(e, _prevHitItem);
                _prevHitItem.OnMouseLeave(oe);
            }
            Invalidate();
            _prevHitItem = hit;
        }
        #endregion

        #region 描画
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (Disposing) return;

            LayoutImages();

            Graphics g = e.Graphics;

            if (_items == null || _items.Count == 0)
            {
            }
            else
            {
                int hvalue = vScrollBar.Value;
                int vvalue = 0;
                g.TranslateTransform(-vvalue, -hvalue);
                for (int i = _viewIndexMin; i <= _viewIndexMax; i++)
                {
                    if (_viewIndexMin < 0 || _items.Count < _viewIndexMax + 1) break;
                    ImageListItem item = _items[i];
                    if (!_showUnusedItems && !item.DenshaImage.IsUsed) continue;
                    item.Paint(e.Graphics, new Point(-vvalue, -hvalue));
                }
                g.TranslateTransform(vvalue, hvalue);
            }
        }
        private void vScrollBar_ValueChanged(object sender, EventArgs e)
        {
            CloseTextBox();
            Invalidate();
        }
        #endregion

        #region レイアウト更新

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            if (vScrollBar.LargeChange != this.Height)
                vScrollBar.LargeChange = this.Height;
            UpdateWidth();
            Invalidate();
        }

        /// <summary>
        /// タグのレイアウトを更新する
        /// </summary>
        /// <param name="tag">変更のあったタグ</param>
        public void UpdateTag(Tag tag)
        {
            CloseTextBox();
            foreach (ImageListItem item in _items)
            {
                if (item.DenshaImage.Tags.Contains(tag))
                {
                    item.DenshaImage.SortTags();
                    item.UpdateTagLayout();
                }
            }
            Invalidate();
        }
        /// <summary>
        /// タグタイプに関連するレイアウトを更新する
        /// </summary>
        /// <param name="tagType">変更のあったタグタイプ</param>
        public void UpdateTagType(TagType tagType)
        {
            CloseTextBox();
            foreach (ImageListItem item in _items)
            {
                if (item.DenshaImage.Tags.FindIndex(delegate(Tag t)
                {
                    return t.Type == tagType;
                }) >= 0)
                {
                    item.DenshaImage.SortTags();
                    item.UpdateTagLayout();
                }
            }
            Invalidate();
        }
        public void UpdateImage(DenshaImage image)
        {
            CloseTextBox();
            foreach (ImageListItem item in _items)
            {
                if (item.DenshaImage.Id == image.Id)
                {
                    item.DenshaImage.SortTags();
                    item.UpdateTagLayout();
                }
            }
            Invalidate();
        }

        /// <summary>
        /// 幅に基づいてレイアウトを更新する
        /// </summary>
        public void UpdateWidth()
        {
            CloseTextBox();
            foreach (ImageListItem item in _items)
            {
                item.UpdateTagLayout();
            }
        }

        /// <summary>
        /// 保持しているイメージの座標を決める
        /// </summary>
        public void LayoutImages()
        {
            if (_items == null || _items.Count == 0)
            {
                if (vScrollBar.Minimum != 0) vScrollBar.Minimum = 0;
                if (vScrollBar.Maximum != 0) vScrollBar.Maximum = 0;
                if (vScrollBar.SmallChange != 0) vScrollBar.SmallChange = 0;
                if (vScrollBar.LargeChange != 0) vScrollBar.LargeChange = 0;
                if (vScrollBar.Enabled) vScrollBar.Enabled = false;
                return;
            }

            int height = this.Height;
            int heightSum = 0;

            int viewMin = 0;
            int viewMax = 0;
            int min = vScrollBar.Value + Padding.Top;
            int max = min + height;
            
            int i = -1;
            int top = Padding.Top;
            using (Graphics g = this.CreateGraphics())
            {
                lock (_items)
                {
                    foreach (ImageListItem item in _items)
                    {
                        i++;
                        if (!_showUnusedItems && !item.DenshaImage.IsUsed) continue;

                        item.Width = vScrollBar.Left - this.Padding.Left - item.Margin.Horizontal;
                        item.Layout(g);

                        item.X = this.Padding.Left + item.Margin.Left;
                        item.Y = top + item.Margin.Top;

                        top += item.Margin.Vertical + item.Height;
                        heightSum += item.Height + item.Margin.Vertical;

                        if (min <= item.Y + item.Height + item.Margin.Bottom && min >= item.Y - item.Margin.Top)
                        {
                            viewMin = i;
                        }
                        if (max <= item.Y + item.Height + item.Margin.Bottom && max >= item.Y - item.Margin.Top)
                        {
                            viewMax = i;
                        }
                    }
                }
            }
            if (heightSum <= height)
            {
                viewMin = 0;
                viewMax = _items.Count - 1;
            }
            else
            {
                if (heightSum <= max)
                {
                    viewMax = _items.Count - 1;
                }
            }
            _viewIndexMin = viewMin;
            _viewIndexMax = viewMax;
            _heightSum = heightSum + Padding.Vertical;

            #region スクロールバー制御
            if (vScrollBar.Minimum != 0)
                vScrollBar.Minimum = 0;
            if (vScrollBar.Maximum != _heightSum)
                vScrollBar.Maximum = _heightSum;
            if (vScrollBar.SmallChange != 1)
                vScrollBar.SmallChange = 1;
            if (vScrollBar.LargeChange != this.Height)
                vScrollBar.LargeChange = this.Height;
            if (vScrollBar.Maximum <= vScrollBar.LargeChange)
            {
                vScrollBar.Value = 0;
                if (vScrollBar.Enabled)
                    vScrollBar.Enabled = false;
            }
            else
            {
                if (!vScrollBar.Enabled)
                    vScrollBar.Enabled = true;
            }
            #endregion
        }
        #endregion

        /// <summary>
        /// 指定したイメージの部分にまでスクロールする
        /// </summary>
        /// <param name="image">表示したいイメージ</param>
        public void ScrollToImage(DenshaImage image)
        {
            ImageListItem found = _items.Find(delegate(ImageListItem item)
            {
                return item.DenshaImage.Id == image.Id;
            });
            if (found != null && _heightSum != 0)
            {
                int v = (int)(found.Y - found.Height / 3.0);
                int vm = v + this.Height;
                vScrollBar.Value = v < vScrollBar.Minimum ? vScrollBar.Minimum :
                    (vm > _heightSum ? vScrollBar.Maximum - this.Height : v);
            }
        }

        public ImageListItem HitTest(Point p, int xOffset, int yOffset)
        {
            Point p2 = new Point(p.X + xOffset, p.Y + yOffset);
            return HitTest(p2);
        }
        public ImageListItem HitTest(Point p)
        {
            if (_items == null || _items.Count == 0) return null;
            for (int i = _viewIndexMin; i <= _viewIndexMax; i++)
            {
                ImageListItem item = _items[i];
                if (item.Contains(p))
                {
                    return item;
                }
            }
            return null;
        }

        private struct RecitalBox
        {
            public TextBox TextBox;
            public ImageListItem ListItem;
        }
        private RecitalBox _recitalBox = new RecitalBox();
        public int GetRecitalBoxHeight(Font font)
        {
            if (_recitalBox.TextBox.Font != font)
            {
                _recitalBox.TextBox.Font = font;
            }
            return _recitalBox.TextBox.Height;
        }
        private bool _showingTextBox = false;
        public void ShowTextBox(ImageListItem item, Rectangle rect)
        {
            if (_showingTextBox)
            {
                CloseTextBox();
            }
            _recitalBox.ListItem = item;
            _recitalBox.TextBox.Text = item.DenshaImage.Recital;
            _recitalBox.TextBox.Left = rect.X + item.X;
            _recitalBox.TextBox.Top = rect.Top + item.Y - vScrollBar.Value;
            _recitalBox.TextBox.Width = rect.Width;
            if (_recitalBox.TextBox.Text.Length > 0)
            {
                _recitalBox.TextBox.SelectAll();
            }
            if (!_showingTextBox)
            {
                this.Controls.Add(_recitalBox.TextBox);
                _showingTextBox = true;
            }
            _recitalBox.TextBox.Focus();
        }
        public void CloseTextBox()
        {
            if (_showingTextBox)
            {
                _showingTextBox = false;
                if (_recitalBox.ListItem != null)
                {
                    _recitalBox.ListItem.DenshaImage.Recital = _recitalBox.TextBox.Text;
                    _recitalBox.ListItem = null;
                }
                this.Controls.Remove(_recitalBox.TextBox);
                this.Invalidate();
            }
        }
        private void _textBox_LostFocus(object sender, EventArgs e)
        {
            CloseTextBox();
        }

        #region 選択
        private int _focusIdx = 0;
        private int _selectStartIdx = 0;
        private bool checkIdxes()
        {
            bool ret = false;
            if (_focusIdx < 0 || _focusIdx >= _items.Count)
            {
                _focusIdx = 0;
                ret = true;
            }
            if (_selectStartIdx < 0 || _selectStartIdx >= _items.Count)
            {
                _selectStartIdx = 0;
                ret = true;
            }
            return ret;
        }

        public void DeselectAllImages()
        {
            checkIdxes();
            foreach (ImageListItem item in _items)
            {
                item.Selected = false;
            }
        }

        public void SelectImage()
        {
            if (!checkIdxes())
            {
                int i = 0;
                foreach (ImageListItem item in _items)
                {
                    item.Selected = (i == _focusIdx);
                    i++;
                }
            }
        }

        public void SelectImages()
        {
            if (!checkIdxes())
            {
                int min = Math.Min(_selectStartIdx, _focusIdx);
                int max = Math.Max(_selectStartIdx, _focusIdx);
                int i = 0;
                foreach (ImageListItem item in _items)
                {
                    if (i < min || i > max) item.Selected = false;
                    else item.Selected = true;
                    i++;
                }
            }
        }

        public void ToggleSelectImage()
        {
            if (!checkIdxes())
            {
                _items[_focusIdx].Selected = !_items[_focusIdx].Selected;
            }
        }
        #endregion

        #region ドロップ
        protected override void OnDragOver(DragEventArgs e)
        {
            base.OnDragOver(e);

            Point pc = this.PointToClient(new Point(e.X, e.Y));
            ImageListItem itm = this.HitTest(pc, 0, vScrollBar.Value);
            if (e.Data.GetDataPresent(Properties.Resources.DndIdTag))
            {
                if (itm == null) e.Effect = DragDropEffects.None;
                else e.Effect = DragDropEffects.Copy;
            }
        }

        protected override void OnDragDrop(DragEventArgs e)
        {
            base.OnDragDrop(e);

            Point pc = this.PointToClient(new Point(e.X, e.Y));
            ImageListItem itm = this.HitTest(pc, 0, vScrollBar.Value);
            if (e.Data.GetDataPresent(Properties.Resources.DndIdTag))
            {
                if (itm != null)
                {
                    // drop
                    Tag tag = e.Data.GetData(Properties.Resources.DndIdTag) as Tag;
                    if (tag != null)
                    {
                        if (itm.Selected)
                        {
                            foreach (ImageListItem item in SelectedItems)
                            {
                                if (_showUnusedItems || item.DenshaImage.IsUsed)
                                {
                                    Program.MainForm.AddTag(this, item, tag);
                                }
                            }
                        }
                        else
                        {
                            Program.MainForm.AddTag(this, itm, tag);
                        }
                    }
                }
            }
        }

        #endregion
    }

}
