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

            tagTypeGridView.CellEndEdit += new DataGridViewCellEventHandler(OnCellEndEdit);
        }

        void OnCellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if(Program.MainForm != null)
                Program.MainForm.UpdateTagType(this, (tagTypeGridView.Rows[e.RowIndex].DataBoundItem) as TagType);
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
                if(Program.MainForm != null)
                {
                    Program.MainForm.ShowTagTypeForm(this, false);
                }
            }
        }
    }
}
