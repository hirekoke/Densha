using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace Densha
{
    partial class ImageListItem
    {
        class TagBox
        {
            private bool _needUpdateTagLayout = false;
            private static StringFormat sf = null;

            private Rectangle _bounds = new Rectangle(0, 191, 200, 48);
            public Rectangle Bounds
            {
                get { return _bounds; }
                set { _bounds = value; }
            }
            private List<KeyValuePair<Tag, TagItem>> _rectTags = new List<KeyValuePair<Tag, TagItem>>();
            private ImageListItem _listItem = null;
            public ImageListItem ListItem
            {
                get { return _listItem; }
            }

            public TagBox(ImageListItem listItem)
            {
                _listItem = listItem;
                _needUpdateTagLayout = true;

                if (sf == null)
                {
                    sf = new StringFormat();
                    sf.Alignment = StringAlignment.Near;
                    sf.LineAlignment = StringAlignment.Center;
                    sf.Trimming = StringTrimming.EllipsisCharacter;
                }
            }

            public int GetPointedTagIndex(Point p)
            {
                Point p2 = new Point(p.X + _bounds.X, p.Y + _bounds.Y);
                return _rectTags.FindIndex(delegate(KeyValuePair<Tag, TagItem> pair)
                {
                    return pair.Value.Bounds.Contains(p2);
                });
            }

            public void Update()
            {
                _needUpdateTagLayout = true;
            }

            public void Paint(Graphics g, Point p, Point offset)
            {
                g.FillRectangle(new SolidBrush(Config.Designs.TagBoxBGColor), _bounds);
                g.DrawRectangle(new Pen(Config.Designs.TagBoxFGColor), _bounds);
                g.TranslateTransform(_bounds.X, _bounds.Y);
                foreach (KeyValuePair<Tag, TagItem> rt in _rectTags)
                {
                    rt.Value.Paint(g, p, offset);
                }
                g.TranslateTransform(-_bounds.X, -_bounds.Y);
            }

            public void Layout(Graphics g)
            {
                if (!_needUpdateTagLayout) return;

                _rectTags.Clear();

                int maxY = 2;
                if (_listItem.DenshaImage != null && _listItem.DenshaImage.Tags != null)
                {
                    int x = 2; int y = 2;

                    int maxWidth = _bounds.Width - 4;
                    foreach (Tag tag in _listItem.DenshaImage.Tags)
                    {
                        TagItem tagItem = new TagItem(tag, this);
                        Size size = tagItem.GetPreferredSize(g, maxWidth);
                        if (x + size.Width > maxWidth)
                        {
                            x = 2;
                            if (y < maxY)
                            {
                                y = maxY;
                            }
                        }
                        Rectangle rect = new Rectangle(x, y, size.Width, size.Height);
                        tagItem.Bounds = rect;
                        _rectTags.Add(new KeyValuePair<Tag, TagItem>(tag, tagItem));
                        x = rect.Right + 2;
                        if (rect.Bottom + 2 > maxY) maxY = rect.Bottom + 2;
                    }
                }
                if (_rectTags.Count > 0)
                {
                    _bounds.Height = maxY;
                }
                else
                {
                    _bounds.Height = 0;
                }

                _needUpdateTagLayout = false;
            }

            public void OnMouseClick(MouseEventArgs e)
            {
                foreach (KeyValuePair<Tag, TagItem> kv in _rectTags)
                {
                    if (kv.Value.Bounds.Contains(e.Location))
                    {
                        MouseEventArgs ne = new MouseEventArgs(e.Button, e.Clicks,
                            e.X - kv.Value.Bounds.X, e.Y - kv.Value.Bounds.Y, e.Delta);
                        kv.Value.OnMouseClick(ne);
                    }
                }
            }
        }

        class TagItem
        {
            private static Padding _padding = new Padding(2, 2, 2, 1);
            private static int _textButtonSep = 1;
            private static Size _removeButtonSize = new Size(12, 12);

            public Rectangle Bounds { get; set; }
            public Tag Tag { get; set; }

            private Rectangle _textBounds = Rectangle.Empty;
            private Rectangle _removeButtonBounds = Rectangle.Empty;
            private TagBox _tagBox = null;

            public TagItem(Tag t, TagBox box)
            {
                this.Tag = t;
                _tagBox = box;
            }

            public void OnMouseClick(MouseEventArgs e)
            {
                if (e.Button == MouseButtons.Left)
                {
                    if (_removeButtonBounds.Contains(e.Location))
                    {
                        Program.MainForm.RemoveTag(this, _tagBox.ListItem, Tag);
                    }
                }
            }

            public Size GetPreferredSize(Graphics g, int maxWidth)
            {
                int maxTextWidth = maxWidth - _padding.Horizontal - _textButtonSep - _removeButtonSize.Width;

                SizeF sizef = g.MeasureString(Tag.Description, SystemFonts.DefaultFont,
                    maxTextWidth, _stringFormat);
                Size size = new Size((int)Math.Ceiling(sizef.Width), (int)Math.Ceiling(sizef.Height));

                _textBounds = new Rectangle(_padding.Left, _padding.Top, size.Width, size.Height);
                _removeButtonBounds = new Rectangle(
                    _textBounds.Right + _textButtonSep, _padding.Top,
                    _removeButtonSize.Width, _removeButtonSize.Height);

                size.Width = _removeButtonBounds.Right + _padding.Right;
                size.Height = Math.Max(_textBounds.Bottom, _removeButtonBounds.Bottom) + _padding.Bottom;
                if (_textBounds.Height > _removeButtonBounds.Height)
                {
                    _removeButtonBounds.Y = (int)(_textBounds.Top + _textBounds.Height / 2.0 - _removeButtonBounds.Height / 2.0);
                }

                return size;
            }

            public void Paint(Graphics g, Point p, Point offset)
            {
                SolidBrush bgB = new SolidBrush(Config.Designs.TagItemBGColor);
                SolidBrush fgB = new SolidBrush(Config.Designs.TagItemFGColor);
                Pen pen = new Pen(Config.Designs.TagBoxFGColor);

                g.FillRectangle(bgB, Bounds);
                g.DrawRectangle(pen, Bounds);
                g.TranslateTransform(Bounds.X, Bounds.Y);

                g.DrawString(Tag.Description, Config.Designs.ItemTextFont,
                    fgB, _textBounds, _stringFormat);

                g.DrawRectangle(pen, _removeButtonBounds);
                g.DrawLine(pen,
                    _removeButtonBounds.Left, _removeButtonBounds.Top,
                    _removeButtonBounds.Right, _removeButtonBounds.Bottom);
                g.DrawLine(pen,
                    _removeButtonBounds.Left, _removeButtonBounds.Bottom,
                    _removeButtonBounds.Right, _removeButtonBounds.Top);

                g.TranslateTransform(-Bounds.X, -Bounds.Y);
            }
        }
    }
}
