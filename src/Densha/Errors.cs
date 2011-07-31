using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Densha.errors {
    /// <summary>
    /// Exif情報のパースに失敗した
    /// </summary>
    public class ExifParseFailException : Exception
    {
        public ExifParseFailException(string idName, string src)
        {
            this.IdName = idName;
            this.Src = src;
        }

        public string IdName { get; private set; }
        public string Src { get; private set; }
    }

    public class ProjectParseError : Exception
    {
        public ProjectParseError(string message) : base(message) { }
        public ProjectParseError(string message, Exception innerException) : base(message, innerException) { }
    }

    public class ErrorHandler
    {
        public static void HandleProjectParseError(ProjectParseError err)
        {
            MessageBox.Show(err.Message, Properties.Resources.Title_MyName, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static void HandleAllError(string message)
        {
            MessageBox.Show(message, Properties.Resources.Title_MyName, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
