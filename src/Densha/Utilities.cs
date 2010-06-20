using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;

namespace Densha {
    class Utilities
    {
        public static DateTime ParseDateTime(string idName, string dtString)
        {
            try
            {
                Match m = Regex.Match(dtString, @"(\d{4})\D(\d{2})\D(\d{2})\D(\d{2})\D(\d{2})\D(\d{2})");
                if (m.Success)
                {
                    DateTime dt = new DateTime(
                        int.Parse(m.Groups[1].Value),
                        int.Parse(m.Groups[2].Value),
                        int.Parse(m.Groups[3].Value),
                        int.Parse(m.Groups[4].Value),
                        int.Parse(m.Groups[5].Value),
                        int.Parse(m.Groups[6].Value));
                    return dt;
                }
                else
                {
                    throw new errors.ExifParseFailException(idName, dtString);
                }
            }
            catch (Exception)
            {
                throw new errors.ExifParseFailException(idName, dtString);
            }
        }

        public static string GetRelativePath(string path)
        {
            if (path == "") return "";
            Uri baseUri = new Uri(AppDomain.CurrentDomain.BaseDirectory);
            Uri absoluteUri = new Uri(path);
            Uri relativeUri = baseUri.MakeRelativeUri(absoluteUri);
            string relativePath = relativeUri.ToString().Replace('/', Path.DirectorySeparatorChar);
            if (relativePath.Contains("..")) return path;
            return relativePath;
        }

        public static string ApplyThumbnailNamePattern(string fileName, string namePattern)
        {
            string fn = Path.GetFileNameWithoutExtension(fileName);
            string en = Path.GetExtension(fileName);
            if (en.StartsWith("."))
            {
                en = en.Substring(1);
            }
            string ret = namePattern;
            ret = ret.Replace("{1}", fn);
            ret = ret.Replace("{2}", en);
            return ret;
        }
    }
}
