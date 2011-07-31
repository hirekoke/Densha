using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Densha
{
    partial class TagTypeForm : Form
    {
        public TagTypeForm()
        {
            InitializeComponent();

            tagTypeGridView.CellValidating += new DataGridViewCellValidatingEventHandler(OnCellValidating);
            tagTypeGridView.CellEndEdit += new DataGridViewCellEventHandler(OnCellEndEdit);
        }

        void OnCellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (tagTypeGridView.Columns[e.ColumnIndex].DataPropertyName == "Priority")
            {
                int r = 0;
                if (!int.TryParse(e.FormattedValue.ToString(), out r))
                {
                    e.Cancel = true;
                    errors.ErrorHandler.HandleAllError("整数を入力してください");
                }
            }
        }

        void OnCellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            tagTypeGridView[e.ColumnIndex, e.RowIndex].ErrorText = null;
            TagType tt = tagTypeGridView.Rows[e.RowIndex].DataBoundItem as TagType;
            if (tt != null)
            {
                _tagTypes.SetUniqId(tt);

                if (Program.MainForm != null)
                    Program.MainForm.UpdateTagType(this, tt);
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
                if(Program.MainForm != null)
                {
                    Program.MainForm.ShowTagTypeForm(this, false);
                }
            }
        }
    }
}
