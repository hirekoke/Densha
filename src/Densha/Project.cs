using System;
using System.Collections.Generic;
using System.Text;

using System.IO;
using System.Xml;

using System.ComponentModel;
using System.Collections.Specialized;

namespace Densha
{
    public class Project : INotifyPropertyChangedBase
    {
        public Project()
        {
            _notifyChange = false;

            _tagTypes.CollectionChanged += new NotifyCollectionChangedEventHandler(_tagTypes_CollectionChanged);
            _tags = new TagCollection(this);
            _tags.CollectionChanged += new NotifyCollectionChangedEventHandler(_tags_CollectionChanged);

        }

        private bool _notifyChange = false;

        public string ProjectFilePath { get; set; }

        #region データ
        protected string _originalPath = ".";
        /// <summary>元画像のあるディレクトリ</summary>
        public string OriginalPath
        {
            get { return _originalPath; }
            set
            {
                if (_originalPath != value)
                {
                    _originalPath = value;
                    OnPropertyChanged("OriginalPath");
                    OnPropertyChanged("OriginalFullPath");
                }
                if (string.IsNullOrEmpty(_originalPath))
                {
                    _originalPath = ".";
                    OnPropertyChanged("OriginalPath");
                    OnPropertyChanged("OriginalFullPath");
                }
            }
        }
        /// <summary>元画像のあるディレクトリ</summary>
        public string OriginalFullPath { get { return Path.GetFullPath(_originalPath); } }

        protected string _thumbnailPath = ".";
        /// <summary>サムネイル画像のあるディレクトリ</summary>
        public string ThumbnailPath
        {
            get { return _thumbnailPath; }
            set
            {
                if (_thumbnailPath != value)
                {
                    _thumbnailPath = value;
                    OnPropertyChanged("ThumbnailPath");
                    OnPropertyChanged("ThumbnailFullPath");
                }
                if (string.IsNullOrEmpty(_thumbnailPath))
                {
                    _thumbnailPath = ".";
                    OnPropertyChanged("ThumbnailPath");
                    OnPropertyChanged("ThumbnailFullPath");
                }
            }
        }
        /// <summary>サムネイル画像のあるディレクトリ</summary>
        public string ThumbnailFullPath { get { return Path.GetFullPath(_thumbnailPath); } }

        protected TagTypeCollection _tagTypes = new TagTypeCollection();
        /// <summary>タグ種類のセット</summary>
        public TagTypeCollection TagTypes
        {
            get { return _tagTypes; }
            set
            {
                if (_tagTypes != value)
                {
                    _tagTypes = value;
                    _tagTypes.CollectionChanged += new NotifyCollectionChangedEventHandler(_tagTypes_CollectionChanged);
                    OnPropertyChanged("TagTypes");
                }
            }
        }

        protected TagCollection _tags = null;
        /// <summary>タグのセット</summary>
        public TagCollection Tags
        {
            get { return _tags; }
            set
            {
                if (_tags != value)
                {
                    _tags = value;
                    _tags.CollectionChanged += new NotifyCollectionChangedEventHandler(_tags_CollectionChanged);
                    OnPropertyChanged("Tags");
                }
            }
        }

        protected List<DenshaImage> _images = new List<DenshaImage>();
        /// <summary>画像オブジェクト</summary>
        public List<DenshaImage> Images
        {
            get { return _images; }
            set
            {
                if (_images != value)
                {
                    _images = value;
                    OnPropertyChanged("Images");
                }
            }
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

        public int UsedCount
        {
            get
            {
                int n = 0;
                foreach (DenshaImage img in _images)
                {
                    if (img.IsUsed) n++;
                }
                return n;
            }
        }

        private void _tagTypes_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (_notifyChange)
            {
                if (e.Action == NotifyCollectionChangedAction.Add)
                {
                    TagType newItem = e.NewItems[0] as TagType;
                    if (newItem != null && newItem.Id == TagType.DEFAULT_ID) return;
                }
                OnPropertyChanged("TagTypes");
            }
        }
        private void _tags_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (_notifyChange)
            {
                if (e.Action == NotifyCollectionChangedAction.Add)
                {
                    Tag newItem = e.NewItems[0] as Tag;
                    if (newItem != null && newItem.Id == Tag.DEFAULT_ID) return;
                }
                OnPropertyChanged("Tags");
            }
        }
        private void image_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (_notifyChange)
            {
                if (e.PropertyName == "IsUsed")
                    OnPropertyChanged("UsedCount");
                OnPropertyChanged("Images");
            }
        }
        #endregion データ

        #region 操作
        public void AddImage(DenshaImage image)
        {
            if (!_imgIdMap.ContainsKey(image.Id))
            {
                _images.Add(image);
                _imgIdMap.Add(image.Id, image);
                image.PropertyChanged += new PropertyChangedEventHandler(image_PropertyChanged);
            }
            else
            {
#warning TODO: throw exception
            }
        }
        #endregion

        #region 保存・読み込み
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
            if (doc.GetElementsByTagName("project") == null) return null;

            Project project = new Project();

            XmlNode origpathNode = doc.SelectSingleNode("/project/origpath");
            if (origpathNode == null) return null;
            project.OriginalPath = origpathNode.InnerText.Trim();

            XmlNode thumbpathNode = doc.SelectSingleNode("/project/thumbpath");
            if (thumbpathNode == null) return null;
            project.ThumbnailPath = thumbpathNode.InnerText.Trim();

            XmlNode tagtypesNode = doc.SelectSingleNode("/project/tagtypes");
            if (tagtypesNode == null) return null;
            TagTypeCollection ttcol = TagTypeCollection.ReadXml(project, tagtypesNode);
            project.TagTypes = ttcol;

            XmlNode tagNode = doc.SelectSingleNode("/project/tags");
            if (tagNode == null) return null;
            TagCollection tcol = TagCollection.ReadXml(project, tagNode);
            project.Tags = tcol;

            XmlNode imagesNode = doc.SelectSingleNode("/project/images");
            if (imagesNode == null) return null;
            float imgCount = imagesNode.ChildNodes.Count - 1;
            int i = 0;
            foreach (XmlNode imgNode in imagesNode.ChildNodes)
            {
                DenshaImage img = DenshaImage.ReadXml(project, imgNode);
                if (img != null) project.AddImage(img);
                if (worker != null)
                {
                    worker.ReportProgress((int)(i * 100 / imgCount), "loading images");
                    i++;
                }
            }
            return project;
        }

        public void Save(string fileName)
        {
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.OmitXmlDeclaration = false;
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

            if (project == null) return null;
            project.ProjectFilePath = fileName;

            project._notifyChange = true;
            return project;
        }

        public static Project CreateProject(string origDirPath, string thumbDirPath, string thumbnailNamePattern, BackgroundWorker worker)
        {
            worker.ReportProgress(0, "creating project");

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

                    worker.ReportProgress((int)(i * 100 / imgCount), "loading images");
                    i++;
                }
            }
            project.ProjectFilePath = null;

            worker.ReportProgress(100, "creating project");

            project._notifyChange = true;
            return project;
        }

        #endregion

        #region 出力
        public IEnumerable<string> GetCommands(string destDir)
        {
            int i = 0;
            foreach (DenshaImage img in Images)
            {
                if (img.IsUsed)
                {
                    string line = string.Format(
                        "cp \"{0}\" \"{1}\"",
                        Utilities.GetRelativePath(img.OriginalFullPath, ProjectFilePath),
                        Utilities.GetRelativePath(
                            Path.Combine(destDir, 
                            i.ToString("D3") + Config.Instance.TagDelimiter + img.FileName + img.ExtName),
                            ProjectFilePath)
                        );
                    yield return line;
                    i++;
                }
            }
        }
        #endregion
    }
}
