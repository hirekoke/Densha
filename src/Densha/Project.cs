using System;
using System.Collections.Generic;
using System.Text;

using System.IO;
using System.Xml;
using System.Xml.Serialization;

using System.ComponentModel;

namespace Densha
{
    public class Project
    {
        public Project()
        {
            this.Tags = new TagCollection(this);
        }

        public string ProjectFilePath { get; set; }

        #region データ
        protected string _originalPath = ".";
        /// <summary>
        /// 元画像のあるディレクトリ
        /// </summary>
        public string OriginalPath
        {
            get { return _originalPath; }
            set
            {
                _originalPath = value;
                if (string.IsNullOrEmpty(_originalPath))
                    _originalPath = ".";
            }
        }
        /// <summary>
        /// 元画像のあるディレクトリ
        /// </summary>
        [XmlIgnore()]
        public string OriginalFullPath
        {
            get { return Path.GetFullPath(_originalPath); }
        }

        protected string _thumbnailPath = ".";
        /// <summary>
        /// サムネイル画像のあるディレクトリ
        /// </summary>
        public string ThumbnailPath
        {
            get { return _thumbnailPath; }
            set
            {
                _thumbnailPath = value;
                if (string.IsNullOrEmpty(_thumbnailPath))
                    _thumbnailPath = ".";
            }
        }
        /// <summary>
        /// サムネイル画像のあるディレクトリ
        /// </summary>
        [XmlIgnore()]
        public string ThumbnailFullPath
        {
            get { return Path.GetFullPath(_thumbnailPath); }
        }

        protected TagTypeCollection _tagTypes = new TagTypeCollection();
        /// <summary>
        /// タグ種類のセット
        /// </summary>
        public TagTypeCollection TagTypes
        {
            get { return _tagTypes; }
            set
            { //_tagTypes = new TagTypeCollection(value); 
                _tagTypes = value;
            }
        }

        /// <summary>
        /// タグのセット
        /// </summary>
        public TagCollection Tags { get; private set; }

        protected List<DenshaImage> _images = new List<DenshaImage>();
        /// <summary>
        /// 画像オブジェクト
        /// </summary>
        [XmlIgnore()]
        public List<DenshaImage> Images
        {
            get { return _images; }
            set { _images = value; }
        }
        private Dictionary<string, DenshaImage> _imgIdMap = new Dictionary<string, DenshaImage>();
        public DenshaImage GetImageById(string id)
        {
            if (_imgIdMap.ContainsKey(id))
            {
                return _imgIdMap[id];
            }
            else
            {
                return null;
            }
        }

        #endregion データ

        public void AddImage(DenshaImage image)
        {
            if (!_imgIdMap.ContainsKey(image.Id))
            {
                _images.Add(image);
                _imgIdMap.Add(image.Id, image);
            }
            else
            {
#warning TODO: throw exception
            }
        }


        public void WriteXml(XmlWriter writer)
        {
            writer.WriteStartElement("project");

            // OriginalPath
            writer.WriteElementString("origpath", OriginalPath);

            // ThumbnailPath
            writer.WriteElementString("thumbpath", ThumbnailPath);

            // TagTypes
            _tagTypes.WriteXml(writer);

            // Tags
            Tags.WriteXml(writer);

            // Images
            writer.WriteStartElement("images");
            foreach (DenshaImage img in _images)
            {
                img.WriteXml(writer);
            }
            writer.WriteEndElement();


            writer.WriteEndElement();
        }

        public static Project ReadXml(XmlDocument doc, BackgroundWorker worker)
        {
            if (doc.FirstChild.Name != "project") return null;

            Project project = new Project();
            XmlNodeList lst;
            
            lst = doc.GetElementsByTagName("origpath");
            if (lst.Count != 1) return null;
            project.OriginalPath = lst[0].InnerText.Trim();

            lst = doc.GetElementsByTagName("thumbpath");
            if (lst.Count != 1) return null;
            project.ThumbnailPath = lst[0].InnerText.Trim();

            lst = doc.GetElementsByTagName("tagtypes");
            if (lst.Count != 1) return null;
            TagTypeCollection ttcol = TagTypeCollection.ReadXml(project, lst[0]);
            project.TagTypes = ttcol;

            XmlNode tagNode = doc.SelectSingleNode("/project/tags");
            if (tagNode == null) return null;
            TagCollection tcol = TagCollection.ReadXml(project, tagNode);
            project.Tags = tcol;

            lst = doc.GetElementsByTagName("images");
            if (lst.Count != 1) return null;

            float imgCount = lst[0].ChildNodes.Count - 1;
            int i = 0;
            int weight = Config.Instance.StatusProgressBarWidth;
            foreach (XmlNode imgNode in lst[0].ChildNodes)
            {
                DenshaImage img = DenshaImage.ReadXml(project, imgNode);
                if (img != null) project.AddImage(img);
                if (worker != null)
                {
                    worker.ReportProgress((int)(i * weight / imgCount), "loading images");
                    i++;
                }
            }

            return project;
        }

        public void Save(string fileName)
        {
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.OmitXmlDeclaration = true;
            settings.CheckCharacters = true;

            try
            {
                using (XmlWriter writer = XmlWriter.Create(fileName, settings))
                {
                    writer.WriteStartDocument();
                    WriteXml(writer);
                    writer.WriteEndDocument();
                }
            }
            catch (Exception ex)
            {
                throw new errors.ProjectParseError(ex.Message, ex);
            }
        }

        public static Project Load(string fileName, BackgroundWorker worker)
        {
            Project project;

            worker.ReportProgress(0, "loading project");

            XmlDocument doc = new XmlDocument();
            using (StreamReader reader = new StreamReader(fileName, Encoding.UTF8))
            {
                doc.Load(reader);
                project = Project.ReadXml(doc, worker);
            }

            project.ProjectFilePath = fileName;
            return project;
        }

        public static Project CreateProject(string origDirPath, string thumbDirPath, string thumbnailNamePattern, BackgroundWorker worker)
        {
            worker.ReportProgress(0, "creating project");
            int weight = Config.Instance.StatusProgressBarWidth;

            Project project = new Project();
            project.OriginalPath = origDirPath;
            project.ThumbnailPath = thumbDirPath;
            project.TagTypes = new TagTypeCollection();

            DirectoryInfo dOriginalDirInfo = new DirectoryInfo(project.OriginalFullPath);
            DirectoryInfo dThumbnailDirInfo = new DirectoryInfo(project.ThumbnailFullPath);
            if (!dOriginalDirInfo.Exists)
            {
                
            }
            else
            {
                FileInfo[] fInfos = dOriginalDirInfo.GetFiles("*.jpg");
                int i = 0;
                int imgCount = fInfos.Length;

                foreach (FileInfo fInfo in fInfos)
                {
                    string origName = Path.GetFileNameWithoutExtension(fInfo.Name);
                    DenshaImage img = new DenshaImage(project, fInfo.Name,
                        Utilities.ApplyThumbnailNamePattern(fInfo.Name, thumbnailNamePattern));
                    project.AddImage(img);

                    worker.ReportProgress((int)(i * weight / imgCount), "loading images");
                    i++;
                }
            }

            worker.ReportProgress(weight, "creating project");

            return project;
        }

        public IEnumerable<string> GetCommands(string destDir)
        {
            int i = 0;
            foreach (DenshaImage img in Images)
            {
                if (img.IsUsed)
                {
                    string line = string.Format(
                        "copy \"{0}\" \"{1}\"",
                        img.OriginalFullPath,
                        Path.Combine(destDir, 
                        i.ToString("D3") + "-" + img.FileName + img.ExtName)
                        );
                    yield return line;
                    i++;
                }
            }
        }
    }
}
