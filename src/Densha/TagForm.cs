using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Densha
{
    partial class TagForm : Form
    {
        public TagForm()
        {
            InitializeComponent();

            tagGridView.RowPostPaint += new DataGridViewRowPostPaintEventHandler(tagGridView_RowPostPaint);

            tagGridView.CellEndEdit += new DataGridViewCellEventHandler(OnCellEndEdit);
            tagGridView.RowsAdded += new DataGridViewRowsAddedEventHandler(tagGridView_RowsAdded);
            tagCollectionBindingSource.AddingNew += new AddingNewEventHandler(tagCollectionBindingSource_AddingNew);

            tagGridView.MouseDown += new MouseEventHandler(tagGridView_MouseDown);
            tagGridView.MouseUp += new MouseEventHandler(tagGridView_MouseUp);
            tagGridView.MouseMove += new MouseEventHandler(tagGridView_MouseMove);
        }


        private int _grabRow = -1;
        private Point _grabPoint = Point.Empty;
        private Pen _draggingFrame = new Pen(Color.Red, 2.0f);

        // rowの描画
        private void tagGridView_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            // ドラッグ中のもののみ枠色付け
            if (e.RowIndex == _grabRow)
            {
                e.Graphics.DrawRectangle(_draggingFrame,
                    e.RowBounds.X + 1, e.RowBounds.Y + 1,
                    e.RowBounds.Width - 2, e.RowBounds.Height - 2);
            }
        }

        private void tagGridView_MouseMove(object sender, MouseEventArgs e)
        {
            if (!_grabPoint.IsEmpty)
            {
                //ドラッグとしないマウスの移動範囲を取得する
                Rectangle moveRect = new Rectangle(
                    _grabPoint.X - SystemInformation.DragSize.Width / 2,
                    _grabPoint.Y - SystemInformation.DragSize.Height / 2,
                    SystemInformation.DragSize.Width,
                    SystemInformation.DragSize.Height);
                //ドラッグとする移動範囲を超えたか調べる
                if (!moveRect.Contains(e.X, e.Y))
                {
                    // ドラッグ開始
                    DataGridView.HitTestInfo hit = tagGridView.HitTest(_grabPoint.X, _grabPoint.Y);
                    if (hit.ColumnIndex >= 0)
                    {
                        if (tagGridView.Columns[hit.ColumnIndex].DataPropertyName == "Type")
                        {
                            _grabRow = -1;
                            _grabPoint = Point.Empty;
                        }
                        else
                        {
                            _grabRow = hit.RowIndex;
                            tagGridView.InvalidateRow(_grabRow);

                            Tag tag = tagGridView.Rows[_grabRow].DataBoundItem as Tag;
                            if (tag != null)
                            {
                                DataObject dobj = new DataObject(Properties.Resources.DndIdTag, tag);
                                tagGridView.DoDragDrop(dobj, DragDropEffects.Copy);
                            }
                            tagGridView.InvalidateRow(_grabRow);
                            _grabRow = -1;
                            _grabPoint = Point.Empty;
                        }
                    }
                }
            }
        }

        private void tagGridView_MouseUp(object sender, MouseEventArgs e)
        {
            if (_grabRow >= 0 && _grabRow < tagGridView.Rows.Count)
            {
                tagGridView.InvalidateRow(_grabRow);
            }
            _grabRow = -1;
            _grabPoint = Point.Empty;
        }

        private void tagGridView_MouseDown(object sender, MouseEventArgs e)
        {
            _grabRow = -1;
            _grabPoint = Point.Empty;
            if (e.Button != MouseButtons.Left) return;
            _grabPoint = e.Location;
        }


        private void OnCellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            Tag tag = tagGridView.Rows[e.RowIndex].DataBoundItem as Tag;
            if (tag != null)
            {
                _tags.SetUniqId(tag);
            }

            if(Program.MainForm != null)
                Program.MainForm.UpdateTag(this, (tagGridView.Rows[e.RowIndex].DataBoundItem) as Tag);
        }

        private void tagGridView_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
        }

        void tagCollectionBindingSource_AddingNew(object sender, AddingNewEventArgs e)
        {
        }

        private TagCollection _tags = null;
        public TagCollection Tags
        {
            get { return _tags; }
            set
            {
                if (_tags != value)
                {
                    _tags = value;
                    if (_tags != null)
                    {
                        tagCollectionBindingSource.DataSource = _tags;
                    }
                }
            }
        }
        private TagTypeCollection _tagTypes = null;
        public TagTypeCollection TagTypes
        {
            get { return _tagTypes; }
            set
            {
                if (_tagTypes != value)
                {
                    _tagTypes = value;
                    if (_tagTypes != null)
                    {
                        tagTypeCollectionBindingSource.DataSource = _tagTypes;
                    }
                }
            }
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            if (!Disposing)
            {
                e.Cancel = true;
                this.Visible = false;
                if (Program.MainForm != null)
                {
                    Program.MainForm.ShowTagForm(this, false);
                }
            }
        }

    }
}
