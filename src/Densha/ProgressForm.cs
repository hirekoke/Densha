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
    public partial class ProgressForm : Form
    {
        public ProgressForm()
        {
            InitializeComponent();
            progressBar.Maximum = 100;
            progressBar.Minimum = 0;
        }

        public BackgroundWorker Worker
        {
            set
            {
                value.ProgressChanged += new ProgressChangedEventHandler(ProgressChanged);
            }
        }

        private void ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            Label = e.UserState.ToString();
            Progress = e.ProgressPercentage;
        }

        public string Label
        {
            get { return statusLabel.Text; }
            set
            {
                if (statusLabel.Text != value)
                {
                    statusLabel.Text = value;
                }
            }
        }

        public int Progress
        {
            get { return progressBar.Value; }
            set
            {
                if (progressBar.Value != value)
                {
                    progressBar.Value = value;
                }
            }
        }
    }
}
