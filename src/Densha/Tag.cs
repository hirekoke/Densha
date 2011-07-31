using System;
using System.Collections.Generic;
using System.Xml;
using System.IO;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace Densha
{
    public class Tag : INotifyPropertyChangedBase
    {
        public const int DEFAULT_ID = -1;

        public Tag() : this(TagType.Default, "", "") { }
        public Tag(TagType type, string idString, string description)
        {
            _id = DEFAULT_ID;
            _type = type;
            _fileString = idString;
            _description = description;
        }

        public static int ComparePriority(Tag t1, Tag t2)
        {
            if (t1.Type.Priority < t2.Type.Priority)
            {
                return -1;
            }
            else if (t1.Type.Priority > t2.Type.Priority)
            {
                return 1;
            }
            else
            {
                int com = t1.FileString.CompareTo(t2.FileString);
                if (com < 0)
                {
                    return -1;
                }
                else if (com > 0)
                {
                    return 1;
                }
                else
                {
                    int com2 = t1.Description.CompareTo(t2.Description);
                    if (com2 < 0)
                    {
                        return -1;
                    }
                    else if (com2 > 0)
                    {
                        return 1;
                    }
                    else
                    {
                        return 0;
                    }
                }
            }
        }

        private int _id = DEFAULT_ID;
        public int Id
        {
            get { return _id; }
            set
            {
                if (_id != value)
                {
                    _id = value;
                    OnPropertyChanged("Id");
                }
            }
        }

        private string _description = "";
        public string Description
        {
            get { return _description; }
            set
            {
                if (_description != value)
                {
                    _description = value == null ? "" : value;
                    OnPropertyChanged("Description");
                }
            }
        }

        private string _fileString = "";
        public string FileString
        {
            get { return _fileString; }
            set
            {
                if (_fileString != value)
                {
                    _fileString = value == null ? "" : value;
                    OnPropertyChanged("FileString");
                }
            }
        }

        private TagType _type = TagType.Default;
        public TagType Type
        {
            get { return _type; }
            set
            {
                if (_type != value)
                {
                    _type = value;
                    OnPropertyChanged("Type");
                }
            }
        }

        public override string ToString()
        {
            return ("Tag{" + FileString + "," + Description + "(" + Id.ToString() + ")}");
        }


        public void WriteXml(XmlWriter writer)
        {
            writer.WriteStartElement("tag");

            writer.WriteAttributeString("id", Id.ToString());
            writer.WriteAttributeString("type", Type.Id.ToString());
            writer.WriteAttributeString("filestr", FileString);
            writer.WriteAttributeString("desc", Description);

            writer.WriteEndElement();
        }

        public static Tag ReadXml(Project project, XmlNode node)
        {
            if (node.Name != "tag") return null;

            XmlAttribute idAttr = node.Attributes["id"];
            int id;
            if (idAttr == null || !int.TryParse(idAttr.InnerText, out id)) id = Tag.DEFAULT_ID;

            XmlAttribute typeAttr = node.Attributes["type"];
            TagType type;
            if (typeAttr == null) type = TagType.Default;
            else
            {
                string tid = typeAttr.InnerText.Trim();
                type = project.TagTypes.FindById(tid);
                if (type == null) type = TagType.Default;
            }

            XmlAttribute filestrAttr = node.Attributes["filestr"];
            string fileString = filestrAttr == null ? "" : filestrAttr.InnerText.Trim();

            XmlAttribute descAttr = node.Attributes["desc"];
            string desc = descAttr == null ? "" : descAttr.InnerText.Trim();

            Tag tag = new Tag(type, fileString, desc);
            tag.Id = id;
            return tag;
        }
    }

    public class TagCollection : ObservableCollection<Tag>
    {
        public TagCollection(Project project)
            : base()
        {
            _rand = new Random(DateTime.Now.Millisecond);
            _idMap = new Dictionary<int, Tag>();
        }

        public Tag FindById(int id)
        {
            if (_idMap.ContainsKey(id)) return _idMap[id];
            else return null;
        }
        private Dictionary<int, Tag> _idMap = null;
        private Random _rand = null;
        private int getUniqId()
        {
            int ret = Tag.DEFAULT_ID;
            while (ret == Tag.DEFAULT_ID || _idMap.ContainsKey(ret))
            {
                ret = _rand.Next(int.MaxValue);
            }
            return ret;
        }
        public void SetUniqId(Tag tag)
        {
            if (tag.Id == Tag.DEFAULT_ID)
            {
                tag.PropertyChanged += new PropertyChangedEventHandler(tag_PropertyChanged);
                tag.Id = getUniqId();
                _idMap.Add(tag.Id, tag);
            }
        }

        void tag_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            NotifyCollectionChangedEventArgs arg =
                new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, sender);
            OnCollectionChanged(arg);
        }


        #region 追加系override
        new public void Add(Tag tag)
        {
            if (tag.Id == Tag.DEFAULT_ID) tag.Id = getUniqId();
            _idMap.Add(tag.Id, tag);
            tag.PropertyChanged += new PropertyChangedEventHandler(tag_PropertyChanged);
            base.Add(tag);
        }

        new public void Insert(int index, Tag tag)
        {
            if (tag.Id == Tag.DEFAULT_ID) tag.Id = getUniqId();
            _idMap.Add(tag.Id, tag);
            tag.PropertyChanged += new PropertyChangedEventHandler(tag_PropertyChanged);
            base.Insert(index, tag);
        }
        #endregion

        public void WriteXml(XmlWriter writer)
        {
            writer.WriteStartElement("tags");
            foreach (Tag tag in this)
            {
                tag.WriteXml(writer);
            }
            writer.WriteEndElement();
        }

        public static TagCollection ReadXml(Project project, XmlNode node)
        {
            if (node.Name != "tags") return null;

            TagCollection col = new TagCollection(project);

            foreach (XmlNode child in node.ChildNodes)
            {
                Tag tag = Tag.ReadXml(project, child);
                col.Add(tag);
            }

            return col;
        }
    }
}

