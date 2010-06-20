using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Densha
{
    public partial class CommandForm : Form
    {
        Project _project = null;

        public CommandForm(Project project)
        {
            InitializeComponent();

            _project = project;

            destDirBox.Text =
                Utilities.GetRelativePath(
                Path.Combine(
                Path.GetDirectoryName(_project.OriginalFullPath),
                "gen"));

            selectDestDirButton.Click += new EventHandler(selectDestDirButton_Click);
            runButton.Click += new EventHandler(runButton_Click);
            copyCBButton.Click += new EventHandler(copyCBButton_Click);
        }

        void selectDestDirButton_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog dlg = new FolderBrowserDialog())
            {
                dlg.SelectedPath = Path.GetFullPath(destDirBox.Text);
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    destDirBox.Text = dlg.SelectedPath;
                }
            }
        }

        void runButton_Click(object sender, EventArgs e)
        {
#warning TODO:出力先チェック

            commandListBox.Items.Clear();
            foreach (string line in _project.GetCommands(Utilities.GetRelativePath(destDirBox.Text)))
            {
                commandListBox.Items.Add(line);
            }
        }

        void copyCBButton_Click(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            foreach (object line in commandListBox.Items)
            {
                if (line is string)
                {
                    sb.Append(line + "\r\n");
                }
            }
            Clipboard.SetText(sb.ToString());
        }

        
    }
}
