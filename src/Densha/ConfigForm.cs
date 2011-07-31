using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Densha
{
    public partial class ConfigForm : Form
    {
        public ConfigForm()
        {
            InitializeComponent();

            okButton.Click += new EventHandler(okButton_Click);
            applyButton.Click += new EventHandler(applyButton_Click);
            cancelButton.Click += new EventHandler(cancelButton_Click);

            designView.DoubleClick += new EventHandler(designView_DoubleClick);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            updateUI();
        }

        #region ボタン操作
        void okButton_Click(object sender, EventArgs e)
        {
            updateConfig();
            Config.Instance.Save();

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        void applyButton_Click(object sender, EventArgs e)
        {
            updateConfig();
            Config.Instance.Save();
        }

        void cancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
        #endregion

        private void updateUI()
        {
            //
            dateTimeDisplayFormatBox.Text = Config.Instance.DateTimeDisplayFormat;
            thumbnailNamePatternBox.Text = Config.Instance.DefaultThumbnailNamePattern;
            tagDelimiterBox.SelectedItem = Config.Instance.TagDelimiter;
            thumbSizeWidthBox.Value = Config.Instance.ThumbnailSize.Width;
            thumbSizeHeightBox.Value = Config.Instance.ThumbnailSize.Height;

            // design tab
            designView.BeginUpdate();
            designView.Items.Clear();
            foreach (Design design in Config.Designs)
            {
                ListViewItem item = new ListViewItem(new string[] { design.Name, design.Value.ToString() });
                item.Text = design.Name;
                item.Name = design.Name;
                item.Tag = design.Value;
                item.ToolTipText = design.Description;

                designView.Items.Add(item);
            }
            designView.EndUpdate();
        }

        private void updateConfig()
        {
            Config.Instance.DateTimeDisplayFormat = dateTimeDisplayFormatBox.Text;
            Config.Instance.DefaultThumbnailNamePattern = thumbnailNamePatternBox.Text;
            Config.Instance.TagDelimiter = tagDelimiterBox.SelectedItem.ToString();
            Config.Instance.ThumbnailSize = new Size(
                (int)thumbSizeWidthBox.Value, (int)thumbSizeHeightBox.Value);

            foreach (ListViewItem item in designView.Items)
            {
                string name = item.Text;
                Config.Designs.SetValue(name, item.Tag);
            }
        }


        private void designView_DoubleClick(object sender, EventArgs e)
        {
            if (designView.SelectedItems.Count < 1) return;
            ListViewItem item = designView.SelectedItems[0];
            object value = item.Tag;
            if (value != null && value is Color)
            {
                using (ColorPickerForm frm = new ColorPickerForm())
                {
                    frm.SelectedColor = (Color)value;
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        object val = frm.SelectedColor;
                        item.SubItems[1].Name = val.ToString();
                        item.SubItems[1].Text = val.ToString();
                        item.Tag = val;
                    }
                }
            }
            else if (value != null && value is Font)
            {
                using (FontDialog dlg = new FontDialog())
                {
                    dlg.Font = value as Font;
                    dlg.FontMustExist = true;
                    dlg.ShowColor = false;
                    if (dlg.ShowDialog() == DialogResult.OK)
                    {
                        object val = dlg.Font;
                        item.SubItems[1].Name = val.ToString();
                        item.SubItems[1].Text = val.ToString();
                        item.Tag = val;
                    }
                }
            }
        }
    }
}
