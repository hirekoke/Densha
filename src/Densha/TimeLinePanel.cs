using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Densha.timeline
{
    partial class TimeLinePanel : Panel
    {
        public TimeLinePanel()
        {
            InitializeComponent();
        }

        private List<view.ImageListItem> _items = new List<view.ImageListItem>();
        public List<view.ImageListItem> Items
        {
            get { return _items; }
            set
            {
                if (_items != value)
                {
                    _items = value;
                    if (_items == null) _items = new List<view.ImageListItem>();

                    if (_items.Count <= 1)
                    {
                        _minTime = DateTime.MinValue;
                        _maxTime = DateTime.MinValue;
                        _minMaxSpan = TimeSpan.Zero;
                    }
                    else
                    {
                        _minTime = DateTime.MaxValue;
                        _maxTime = DateTime.MinValue;
                        foreach (view.ImageListItem item in _items)
                        {
                            if (item.DenshaImage.ShootingTime > _maxTime) _maxTime = item.DenshaImage.ShootingTime;
                            if (item.DenshaImage.ShootingTime < _minTime) _minTime = item.DenshaImage.ShootingTime;
                        }
                        _minMaxSpan = _maxTime.Subtract(_minTime);
                    }
                }
            }
        }

        private DateTime _minTime;
        private DateTime _maxTime;
        private TimeSpan _minMaxSpan;

        protected override void OnResize(EventArgs eventargs)
        {
            base.OnResize(eventargs);
            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Graphics g = e.Graphics;
            Padding margin = new Padding(10, 2, 10, 2);
            int height = e.ClipRectangle.Height - margin.Vertical;
            int width = e.ClipRectangle.Width - margin.Horizontal;
            float axisY = margin.Top + height / 2.0f;

            g.Clear(this.BackColor);
            g.DrawLine(new Pen(this.ForeColor),
                0, axisY,
                e.ClipRectangle.Right, axisY);

            Brush _pointBrush = new SolidBrush(this.ForeColor);

            if (_items.Count == 0)
            {

            }
            else if (_items.Count == 1)
            {
                g.FillRectangle(_pointBrush,
                    margin.Left + width / 2.0f,
                    axisY - 1,
                    3, 3);
            }
            else
            {
                float unit = width / (float)_minMaxSpan.TotalSeconds;

                foreach (view.ImageListItem item in _items)
                {
                    g.FillRectangle(_pointBrush,
                        margin.Left + (float)item.DenshaImage.ShootingTime.Subtract(_minTime).TotalSeconds * unit - 1,
                        axisY - 1,
                        3, 3);
                }
            }
        }

    }
}
