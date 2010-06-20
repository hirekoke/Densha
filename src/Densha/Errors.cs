using System;
using System.Collections.Generic;
using System.Text;

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

    public class TagTypeCollectionParseError : Exception
    {
        public TagTypeCollectionParseError(string message) : base(message) { }
        public TagTypeCollectionParseError(string message, Exception innerException) : base(message, innerException) { }
    }

    public class ProjectParseError : Exception
    {
        public ProjectParseError(string message) : base(message) { }
        public ProjectParseError(string message, Exception innerException) : base(message, innerException) { }
    }
}
