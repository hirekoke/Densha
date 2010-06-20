using System;
using System.Collections.Generic;
using System.Text;

using System.Drawing;
using System.Drawing.Imaging;

using System.IO;
using System.Xml;
using System.Xml.Serialization;

using System.Windows.Media.Imaging;

namespace Densha
{
    public class DenshaImage
    {
        public DenshaImage(Project project, string fileName)
            : this(project, fileName, "s-" + fileName)
        {
        }
        public DenshaImage(Project project, string fileName, string thumbName)
        {
            _project = project;
            _id = getUniqId();

            OriginalName = Path.GetFileNameWithoutExtension(fileName);
            ThumbnailName = Path.GetFileNameWithoutExtension(thumbName);
            ExtName = Path.GetExtension(fileName);

            ReadExif();

            Recital = "";
            Tags = new List<Tag>();
            IsUsed = true;
        }

        private Project _project = null;
        [XmlIgnore()]
        public Project Project
        {
            get { return _project; }
        }

        private Random _rand = new Random(Environment.TickCount);
        private string getUniqId()
        {
            string cs = "";
            do
            {
                cs = _rand.Next().ToString();
            } while (_project.GetImageById(cs) != null);
            return cs;
        }

        private string _id = "";
        public string Id
        {
            get { return _id; }
            set { _id = value; }
        }

        #region ファイルパス
        /// <summary>
        /// 拡張子
        /// </summary>
        public string ExtName { get; private set; }

        /// <summary>
        /// 元のファイル名
        /// </summary>
        public string OriginalName { get; private set; }

        /// <summary>
        /// 元ファイルのパス
        /// </summary>
        public string OriginalPath
        {
            get
            {
                return _project.OriginalPath + Path.DirectorySeparatorChar +
                    OriginalName + ExtName;
            }
        }
        /// <summary>
        /// 元ファイルのフルパス
        /// </summary>
        public string OriginalFullPath
        {
            get
            {
                return _project.OriginalFullPath + Path.DirectorySeparatorChar +
                    OriginalName + ExtName;
            }
        }

        /// <summary>
        /// サムネイル画像のファイル名
        /// </summary>
        public string ThumbnailName { get; private set; }

        /// <summary>
        /// サムネイル画像のファイルパス
        /// </summary>
        public string ThumbnailPath
        {
            get
            {
                return _project.ThumbnailPath + Path.DirectorySeparatorChar +
                    ThumbnailName + ExtName;
            }
        }
        /// <summary>
        /// サムネイル画像のフルパス
        /// </summary>
        public string ThumbnailFullPath
        {
            get
            {
                return _project.ThumbnailFullPath + Path.DirectorySeparatorChar +
                    ThumbnailName + ExtName;
            }
        }
        #endregion ファイルパス

        #region 付加した情報

        /// <summary>
        /// 使うかどうか
        /// </summary>
        public bool IsUsed { get; set; }

        /// <summary>
        /// 付加されたタグ
        /// </summary>
        public List<Tag> Tags { get; set; }

        /// <summary>
        /// ファイル名に付け足す文字列
        /// </summary>
        public string Recital { get; set; }

        /// <summary>
        /// 生成されるファイル名(拡張子は除く)
        /// </summary>
        public string FileName
        {
            get
            {
                List<string> s = new List<string>();
                Tags.Sort(Tag.ComparePriority);
                foreach (Tag tag in Tags)
                {
                    if(!string.IsNullOrEmpty(tag.FileString))
                        s.Add(tag.FileString);
                }
                if (!string.IsNullOrEmpty(Recital))
                {
                    s.Add(Recital);
                }

                if (s.Count > 0)
                {
                    return string.Join(Config.Instance.TagDelimiter, s.ToArray());
                }
                else
                {
                    return "";
                }
            }
        }

        #endregion 付加した情報

        #region 撮影情報
        private DateTime _shootingTime = DateTime.Now;
        public DateTime ShootingTime
        {
            get { return _shootingTime; }
            set { _shootingTime = value; }
        }
        #endregion 撮影情報

        #region メソッド

        /// <summary>
        /// 画像にタグを追加する
        /// </summary>
        /// <returns>追加されたかどうか</returns>
        /// <param name="tag">追加するタグ</param>
        public bool AddTag(Tag tag)
        {
            if (Tags.Contains(tag)) return false;

            if (!_project.Tags.Contains(tag))
            {
                _project.Tags.Add(tag);
            }

            int idx = -1;
            for (int i = 0; i < Tags.Count; i++)
            {
                Tag t = Tags[i];
                if (Tag.ComparePriority(tag, t) < 0)
                {
                    idx = i; break;
                }
            }
            if (idx < 0) Tags.Add(tag);
            else Tags.Insert(idx, tag);
            return true;
        }
        public void SortTags()
        {
            Tags.Sort(Tag.ComparePriority);
        }
        public bool RemoveTag(Tag tag)
        {
            if (!Tags.Contains(tag)) return false;
            Tags.Remove(tag);
            return true;
        }

        #endregion メソッド

        #region 画像情報取得メソッド

        public void ReadExif()
        {
            try
            {
                using (FileStream fs = File.OpenRead(OriginalFullPath))
                {
                    BitmapDecoder decoder = BitmapDecoder.Create(fs,
                        BitmapCreateOptions.None,
                        BitmapCacheOption.None);

                    BitmapMetadata metaData = (BitmapMetadata)decoder.Frames[0].Metadata;
                    _shootingTime = DateTime.Parse(metaData.DateTaken);
                }
            }
            catch (Exception ex)
            {
#warning TODO error
            }
        }

        #endregion 画像情報取得メソッド


        private const string SAVETIME_FORMAT = "yyyyMMdd-HHmmss";
        public void WriteXml(XmlWriter writer)
        {
            writer.WriteStartElement("image");

            writer.WriteElementString("origname", this.OriginalName);
            writer.WriteElementString("thumbname", this.ThumbnailName);
            writer.WriteElementString("extname", this.ExtName);
            writer.WriteElementString("time", this.ShootingTime.ToString(SAVETIME_FORMAT));

            writer.WriteElementString("use", this.IsUsed ? "1" : "0");
            writer.WriteElementString("recital", this.Recital);

            writer.WriteStartElement("tags");
            foreach (Tag tag in this.Tags)
            {
                writer.WriteElementString("tag", tag.Id.ToString());
            }
            writer.WriteEndElement();

            writer.WriteEndElement();
        }

        public static DenshaImage ReadXml(Project project, XmlNode node)
        {
            if (node.Name != "image") return null;

            string origname = null;
            string thumbname = null;
            string extname = null;
            DateTime time = DateTime.Now;
            bool use = false;
            string recital = "";
            List<Tag> tags = new List<Tag>();

            foreach (XmlNode child in node.ChildNodes)
            {
                switch (child.Name)
                {
                    case "origname":
                        origname = child.InnerText.Trim();
                        break;
                    case "thumbname":
                        thumbname = child.InnerText.Trim();
                        break;
                    case "extname":
                        extname = child.InnerText.Trim();
                        break;
                    case "time":
                        DateTime.TryParseExact(child.InnerText.Trim(), SAVETIME_FORMAT, null, 
                            System.Globalization.DateTimeStyles.AllowWhiteSpaces, out time);
                        break;
                    case "use":
                        use = child.InnerText.Trim() == "1";
                        break;
                    case "recital":
                        recital = child.InnerText.Trim();
                        break;
                    case "tags":
                        {
                            foreach (XmlNode tagNode in child.ChildNodes)
                            {
                                int tagId = -1;
                                int.TryParse(tagNode.InnerText.Trim(), out tagId);
                                Tag tag = project.Tags.FindById(tagId);
                                if (tag != null) tags.Add(tag);
                            }
                        }
                        break;
                }
            }

            if (String.IsNullOrEmpty(origname) || String.IsNullOrEmpty(thumbname)) return null;

            extname = extname ?? ".jpg";
            DenshaImage img = new DenshaImage(project, origname + extname, thumbname + extname);
            img.IsUsed = use;
            img.Recital = recital;
            img.ShootingTime = time;
            foreach (Tag tag in tags)
            {
                img.AddTag(tag);
            }

            return img;
        }
    }
}
