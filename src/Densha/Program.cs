using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Densha
{
    static class Program
    {
        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //try
            //{
                _mainForm = new MainForm();
                Application.Run(_mainForm);
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.ToString());
            //}
        }

        private static MainForm _mainForm = null;
        public static MainForm MainForm { get { return _mainForm; } }
    }
}
