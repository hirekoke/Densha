using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Densha
{
    public partial class ColorPickerForm : Form
    {
        public ColorPickerForm()
        {
            InitializeComponent();
            SelectedColor = Color.Gray;

            systemColorsBox.DrawMode = DrawMode.OwnerDrawFixed;
            namedColorsBox.DrawMode = DrawMode.OwnerDrawFixed;
            systemColorsBox.DrawItem += new DrawItemEventHandler(ColorsBox_DrawItem);
            namedColorsBox.DrawItem += new DrawItemEventHandler(ColorsBox_DrawItem);

            systemColorsBox.SelectedIndexChanged += new EventHandler(systemColorsBox_SelectedIndexChanged);
            namedColorsBox.SelectedIndexChanged += new EventHandler(namedColorsBox_SelectedIndexChanged);
            selectCustomColorButton.Click += new EventHandler(selectCustomColorButton_Click);

            okButton.Click += new EventHandler(okButton_Click);
            cancelButton.Click += new EventHandler(cancelButton_Click);
        }

        public Color SelectedColor { get; set; }
        private void initColors()
        {
            _stringFormat = new StringFormat();
            _stringFormat.Alignment = StringAlignment.Near;
            _stringFormat.LineAlignment = StringAlignment.Center;

            KnownColor[] colors = (KnownColor[])Enum.GetValues(typeof(KnownColor));
            foreach (KnownColor kc in colors)
            {
                Color color = Color.FromKnownColor(kc);
                if (color.IsSystemColor)
                {
                    systemColorsBox.Items.Add(color);
                }
                else if (color.IsNamedColor)
                {
                    namedColorsBox.Items.Add(color);
                }
            }

            if (SelectedColor.IsSystemColor)
            {
                systemColorsBox.SelectedItem = SelectedColor;
                systemColorRadioButton.Checked = true;
            }
            else if (SelectedColor.IsNamedColor)
            {
                namedColorsBox.SelectedItem = SelectedColor;
                namedColorRadioButton.Checked = true;
            }
            else
            {
                customColorRadioButton.Checked = true;
            }

            _selectedCustomColor = SelectedColor;
            _selectedCustomBmp = new Bitmap(24, 16);
            updateSelectedCustomBmp();
            selectCustomColorButton.Image = _selectedCustomBmp;
        }

        private Color _selectedCustomColor = Color.Gray;
        private Bitmap _selectedCustomBmp = null;
        private void updateSelectedCustomBmp()
        {
            using (Graphics g = Graphics.FromImage(_selectedCustomBmp))
            {
                g.FillRectangle(new SolidBrush(_selectedCustomColor), g.ClipBounds);
                g.DrawRectangle(new Pen(selectCustomColorButton.ForeColor),
                    g.ClipBounds.X, g.ClipBounds.Y, g.ClipBounds.Width - 1, g.ClipBounds.Height - 1);
            }
        }

        private int _colorRectWidth = 32;
        private StringFormat _stringFormat = null;

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            initColors();
        }

        void selectCustomColorButton_Click(object sender, EventArgs e)
        {
            using (ColorDialog cdlg = new ColorDialog())
            {
                cdlg.Color = _selectedCustomColor;
                DialogResult result = cdlg.ShowDialog();
                if (result == DialogResult.OK)
                {
                    _selectedCustomColor = cdlg.Color;
                    customColorRadioButton.Checked = true;
                    updateSelectedCustomBmp();
                }
            }
        }

        void namedColorsBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            namedColorRadioButton.Checked = true;
        }

        void systemColorsBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            systemColorRadioButton.Checked = true;
        }

        void cancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            this.Close();
        }

        void okButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            if (systemColorRadioButton.Checked)
            {
                SelectedColor = (Color)systemColorsBox.SelectedItem;
            }
            else if (namedColorRadioButton.Checked)
            {
                SelectedColor = (Color)namedColorsBox.SelectedItem;
            }
            else
            {
                SelectedColor = _selectedCustomColor;
            }
            this.Close();
        }

        void ColorsBox_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0) return;
            e.DrawBackground();

            Color color = (Color)(sender as ComboBox).Items[e.Index];
            if (color == null) return;

            string name = color.Name;
            if (sender == systemColorsBox)
                name = color.ToKnownColor().ToString();

            Brush bgB = new SolidBrush(color);
            Brush fgB = new SolidBrush(e.ForeColor);
            Pen fgP = new Pen(e.ForeColor);

            Rectangle colorRect = new Rectangle(e.Bounds.X + 2, e.Bounds.Y + 2, 
                _colorRectWidth, e.Bounds.Height - 4);
            e.Graphics.FillRectangle(bgB, colorRect);
            e.Graphics.DrawRectangle(fgP, colorRect);

            Rectangle nameRect = new Rectangle(colorRect.Right + 2, e.Bounds.Y,
                e.Bounds.Width - colorRect.Width - 4, e.Bounds.Height);
            e.Graphics.DrawString(name, systemColorsBox.Font, fgB, nameRect, _stringFormat);

            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
            {
                e.DrawFocusRectangle();
            }
        }

    }
}
