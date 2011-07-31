using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Densha.timeline
{
    partial class TimeLineForm : Form
    {
        public TimeLineForm(List<view.ImageListItem> items)
        {
            InitializeComponent();

            timeLinePanel1.Items = items;
        }
    }
}
