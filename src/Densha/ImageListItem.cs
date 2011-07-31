using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using System.IO;

namespace Densha.view
{
    partial class ImageListItem : IDisposable
    {
        public ImageListItem(DenshaImage dImage)
        {
            _dImage = dImage;
            if(File.Exists(_dImage.ThumbnailFullPath))
                _image = Image.FromFile(_dImage.ThumbnailFullPath);

            initImageAttributes();
            initStringFormat();

            _tagBox = new TagBox(this);
        }
        ~ImageListItem()
        {
            this.Dispose();
        }
        public void Dispose()
        {
            if (_image != null) _image.Dispose();
        }

        private DenshaImage _dImage = null;
        public DenshaImage DenshaImage
        {
            get { return _dImage; }
        }
        private Image _image = null;
        public Image Image
        {
            get { return _image; }
        }

        #region 位置
        private int _x = 0;
        private int _y = 0;
        private int _width = 240;
        private int _height = 200;

        public int X
        {
            get { return _x; }
            set { _x = value; }
        }
        public int Y
        {
            get { return _y; }
            set { _y = value; }
        }
        public int Width
        {
            get { return _width; }
            set { _width = value; }
        }
        public int Height
        {
            get { return _height; }
            set { _height = value; }
        }

        public Point Location
        {
            get { return new Point(_x, _y); }
        }
        public Size Size
        {
            get { return new Size(_width, _height); }
        }
        public Rectangle Bounds
        {
            get { return new Rectangle(_x, _y, _width, _height); }
        }


        private Padding _padding = new Padding(3);
        public Padding Padding
        {
            get { return _padding; }
            set { _padding = value; }
        }

        private Padding _margin = new Padding(3);
        public Padding Margin
        {
            get { return _margin; }
            set { _margin = value; }
        }

        #endregion

        private enum ListComponent
        {
            UseCheckBox,
            DateTimeLabel,
            OriginalNameLabel,
            Thumbnail,
            FileNameLabel,
            TagsBox,
            Recital,
            None,
        }

        private static ImageAttributes _grayImageAttributes;
        private static void initImageAttributes()
        {
            if (_grayImageAttributes == null)
            {
                const float r = 0.298912f;
                const float g = 0.586611f;
                const float b = 0.114478f;

                ColorMatrix grayMatrix;
                float[][] grayMatrixElement = {
                                              new float[] { r, r, r, 0, 0 },
                                              new float[] { g, g, g, 0, 0 },
                                              new float[] { b, b, b, 0, 0 },
                                              new float[] { 0, 0, 0, 0.8f, 0 },
                                              new float[] { 0, 0, 0, 0, 1 }
                                          };
                grayMatrix = new ColorMatrix(grayMatrixElement);
                _grayImageAttributes = new ImageAttributes();
                _grayImageAttributes.SetColorMatrix(grayMatrix);
            }
        }
        private static StringFormat _stringFormat = new StringFormat();
        private static void initStringFormat()
        {
            if (_stringFormat == null)
            {
                _stringFormat = new StringFormat();
                _stringFormat.Alignment = StringAlignment.Near;
                _stringFormat.LineAlignment = StringAlignment.Center;
                _stringFormat.Trimming = StringTrimming.EllipsisCharacter;
            }
        }

        private Rectangle _rectUseCheckBox = new Rectangle(0, 0, 16, 16);
        private Rectangle _rectDateTimeLabel = new Rectangle(16, 0, 200, 16);
        private Rectangle _rectOriginalNameLabel = new Rectangle(0, 0, 200, 16);
        private Rectangle _rectThumbnail = new Rectangle(3, 19, 200, 150);
        private Rectangle _rectFileNameLabel = new Rectangle(0, 172, 200, 16);
        private TagBox _tagBox = null;
        private Rectangle _rectRecital = new Rectangle(0, 190, 200, 16);

        private int _lineHeight = -1;
        private Font _font = SystemFonts.DefaultFont;

        private static bool _renderVisual = Application.RenderWithVisualStyles;

        private bool _hovered = false;
        private bool _checkBoxHover = false;
        private bool _checkBoxPressed = false;

        private bool _selected = false;
        public bool Selected
        {
            get { return _selected; }
            set { _selected = value; }
        }

        #region ヒット判定
        public bool Contains(Point p)
        {
            return Bounds.Contains(p);
        }
        private ListComponent getPointedComponent(Point p)
        {
            if (_rectThumbnail.Contains(p)) return ListComponent.Thumbnail;
            if (_rectUseCheckBox.Contains(p)) return ListComponent.UseCheckBox;
            if (_rectDateTimeLabel.Contains(p)) return ListComponent.DateTimeLabel;
            if (_rectOriginalNameLabel.Contains(p)) return ListComponent.OriginalNameLabel;
            if (_rectFileNameLabel.Contains(p)) return ListComponent.FileNameLabel;
            if (_tagBox.Bounds.Contains(p)) return ListComponent.TagsBox;
            if (_rectRecital.Contains(p)) return ListComponent.Recital;
            return ListComponent.None;
        }
        private int getPointedTagIndex(Point p)
        {
            return _tagBox.GetPointedTagIndex(p);
        }
        #endregion


        #region layout
        public void UpdateTagLayout()
        {
            _tagBox.Update();
        }

        public void Layout(Graphics g)
        {
            textFont = Config.Designs.ItemTextFont;
            _lineHeight = (int)textFont.GetHeight(g);

            _rectUseCheckBox.X = this.Padding.Left;
            _rectUseCheckBox.Y = this.Padding.Top;

            _rectDateTimeLabel.X = _rectUseCheckBox.Right;
            _rectDateTimeLabel.Y = _rectUseCheckBox.Y;
            _rectDateTimeLabel.Width = this.Width - this.Padding.Horizontal - _rectUseCheckBox.Width;
            _rectDateTimeLabel.Height = _lineHeight;

            _rectOriginalNameLabel.X = this.Padding.Left;
            _rectOriginalNameLabel.Y = _rectUseCheckBox.Bottom + 3;
            _rectOriginalNameLabel.Width = this.Width - this.Padding.Horizontal;
            _rectOriginalNameLabel.Height = _lineHeight;

            _rectThumbnail.Size = Config.Instance.ThumbnailSize;
            _rectThumbnail.X = this.Padding.Left;
            _rectThumbnail.Y = _rectOriginalNameLabel.Bottom + 3;

            _rectFileNameLabel.X = this.Padding.Left;
            _rectFileNameLabel.Y = _rectThumbnail.Bottom + 3;
            _rectFileNameLabel.Width = this.Width - this.Padding.Horizontal;
            _rectFileNameLabel.Height = _lineHeight;

            _tagBox.Bounds = new Rectangle(this.Padding.Left, _rectFileNameLabel.Bottom + 3,
                this.Width - this.Padding.Horizontal, _tagBox.Bounds.Height);
            _tagBox.Layout(g);

            _rectRecital.X = this.Padding.Left;
            _rectRecital.Y = _tagBox.Bounds.Bottom + 3;
            _rectRecital.Width = this.Width - this.Padding.Horizontal;
            if (Program.MainForm.ImageList != null)
            {
                _rectRecital.Height = 
                    Program.MainForm.ImageList.GetRecitalBoxHeight(textFont);
            }
            else
            {
                _rectRecital.Height = _lineHeight;
            }

            this.Height = _rectRecital.Bottom + this.Padding.Bottom;
        }

        #endregion

        #region paint

        private SolidBrush textBrush = new SolidBrush(Config.Designs.ItemBGColor);
        private Font textFont = Config.Designs.ItemTextFont;

        private void paintBackground(Graphics g)
        {
            // 背景
            Color color = Selected ?
                Config.Designs.ItemBGSelectedColor :
                Config.Designs.ItemBGColor;
            g.FillRectangle(new SolidBrush(color), 0, 0, _width, _height);
            if (_hovered)
            {
                int rd = (int)(color.R / 2.0);
                int gr = (int)(color.G / 2.0);
                int bl = (int)(color.B / 2.0);
                Color color2 = Color.FromArgb(color.A,
                    rd > 255 ? 255 : rd, gr > 255 ? 255 : gr, bl > 255 ? 255 : bl);
                SolidBrush brush = new SolidBrush(color2);
                g.FillRectangle(brush, _width, 2, 2, _height);
                g.FillRectangle(brush, 2, _height, _width - 2, 2);
            }
            ControlPaint.DrawVisualStyleBorder(g, new Rectangle(0, 0, _width, _height));
        }

        private void paintCheckBoxAndDateTime(Graphics g)
        {
            if (_renderVisual)
            {
                CheckBoxState state = _dImage.IsUsed ?
                    (_checkBoxPressed ? CheckBoxState.CheckedPressed : (_checkBoxHover ? CheckBoxState.CheckedHot : CheckBoxState.CheckedNormal))
                    : (_checkBoxPressed ? CheckBoxState.UncheckedPressed : (_checkBoxHover ? CheckBoxState.UncheckedHot : CheckBoxState.UncheckedNormal));
                Size size = CheckBoxRenderer.GetGlyphSize(g, state);

                CheckBoxRenderer.DrawCheckBox(g,
                    new Point(
                        _rectUseCheckBox.Left + (int)((_rectUseCheckBox.Width - size.Width) / 2.0),
                        _rectUseCheckBox.Top + (int)((_rectUseCheckBox.Height - size.Height) / 2.0)),
                    Rectangle.Empty, "", textFont, TextFormatFlags.Default,
                    false, state);

                g.DrawString(_dImage.ShootingTime.ToString(Config.Instance.DateTimeDisplayFormat),
                    textFont, textBrush, _rectDateTimeLabel, _stringFormat);
            }
            else
            {
                ControlPaint.DrawCheckBox(g, _rectUseCheckBox,
                    _dImage.IsUsed ? ButtonState.Checked : ButtonState.Normal);
                g.DrawString(_dImage.ShootingTime.ToString(Config.Instance.DateTimeDisplayFormat),
                    textFont, textBrush, _rectDateTimeLabel, _stringFormat);
            }
        }
        private void paintOriginalFileName(Graphics g)
        {
            // オリジナルファイル名
            g.DrawString(_dImage.OriginalName, textFont,
                textBrush, _rectOriginalNameLabel, _stringFormat);
        }
        private void paintImage(Graphics g)
        {
            // 写真
            if (_image == null)
            {
                g.DrawRectangle(new Pen(textBrush.Color), _rectThumbnail);
            }
            else
            {
                if (_dImage.IsUsed)
                {
                    g.DrawImage(_image, _rectThumbnail, 0, 0,
                        _image.Width,
                        _image.Height, 
                        GraphicsUnit.Pixel);
                }
                else
                {
                    g.DrawImage(_image, _rectThumbnail, 0, 0,
                        _image.Width,
                        _image.Height,
                        GraphicsUnit.Pixel, _grayImageAttributes);
                }
            }
        }
        private void paintFileName(Graphics g)
        {
            // 今の名前
            g.DrawString(_dImage.FileName, textFont,
                textBrush, _rectFileNameLabel, _stringFormat);
        }
        private void paintRecital(Graphics g, Point p, Point offset)
        {
            // 追加文字列
            if (_renderVisual && TextBoxRenderer.IsSupported)
            {
                Rectangle rect = new Rectangle(_rectRecital.Location, _rectRecital.Size);
                rect.X += p.X + offset.X;
                rect.Y += p.Y + offset.Y;
                TextBoxRenderer.DrawTextBox(g, _rectRecital, _dImage.Recital,
                    textFont, rect,
                    TextFormatFlags.GlyphOverhangPadding | 
                    TextFormatFlags.VerticalCenter |
                    TextFormatFlags.Left,
                    TextBoxState.Normal);
            }
            else
            {
                g.DrawRectangle(new Pen(Config.Designs.ItemFGColor), _rectRecital);
                g.DrawString(_dImage.Recital, textFont,
                    textBrush, _rectRecital, _stringFormat);
            }
        }

        public void Paint(Graphics g, Point offset)
        {
            Paint(g, Location, offset);
        }
        public void Paint(Graphics g, Point p, Point offset)
        {
            g.TranslateTransform(p.X, p.Y);

            textBrush.Color = Selected ?
                Config.Designs.ItemFGSelectedColor :
                Config.Designs.ItemFGColor;
            textFont = Config.Designs.ItemTextFont;

            paintBackground(g);

            if (_dImage == null)
            {
                return;
            }

            paintCheckBoxAndDateTime(g);
            paintOriginalFileName(g);
            paintImage(g);
            paintFileName(g);

            // タグ
            _tagBox.Paint(g, p, offset);

            paintRecital(g, p, offset);

            g.TranslateTransform(-p.X, -p.Y);
        }

        #endregion

        #region マウスイベント
        public bool OnMouseClick(MouseEventArgs e)
        {
            if (e.Clicks >= 2) return true;
            ListComponent hitc = getPointedComponent(e.Location);
            switch (hitc)
            {
                case ListComponent.UseCheckBox:
                case ListComponent.DateTimeLabel:
                    Program.MainForm.SetUse(this, _dImage, !_dImage.IsUsed);
                    break;
                case ListComponent.TagsBox:
                    MouseEventArgs ne = new MouseEventArgs(e.Button, e.Clicks,
                        e.X - _tagBox.Bounds.X, e.Y - _tagBox.Bounds.Y, e.Delta);
                    _tagBox.OnMouseClick(ne);
                    break;
                case ListComponent.Recital:
                    {
                        Program.MainForm.ImageList.ShowTextBox(this, _rectRecital);
                    }
                    break;
                default:
                    return false;
            }
            return true;
        }
        public void OnMouseDoubleClick(MouseEventArgs e)
        {
            ListComponent hitc = getPointedComponent(e.Location);
            switch (hitc)
            {
                case ListComponent.DateTimeLabel:
                case ListComponent.FileNameLabel:
                case ListComponent.OriginalNameLabel:
                case ListComponent.TagsBox:
                case ListComponent.UseCheckBox:
                    break;
                case ListComponent.Thumbnail:
                case ListComponent.None:
                    Program.MainForm.ShowLargeImage(_dImage);
                    break;
            }
        }
        public void OnMouseDown(MouseEventArgs e)
        {
            ListComponent hitc = getPointedComponent(e.Location);
            switch (hitc)
            {
                case ListComponent.UseCheckBox:
                case ListComponent.DateTimeLabel:
                    _checkBoxPressed = true;
                    break;
                default:

                    break;
            }
        }
        public void OnMouseUp(MouseEventArgs e)
        {
            _checkBoxPressed = false;
        }
        public void OnMouseMove(MouseEventArgs e)
        {
            ListComponent hitc = getPointedComponent(e.Location);
            _checkBoxHover = (hitc == ListComponent.UseCheckBox || hitc == ListComponent.DateTimeLabel);
        }

        public void OnMouseEnter(EventArgs e)
        {
            _hovered = true;
        }
        public void OnMouseLeave(EventArgs e)
        {
            _hovered = false;
        }
        #endregion

    }
}
