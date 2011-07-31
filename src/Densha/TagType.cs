using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace Densha {
    public class TagType : INotifyPropertyChangedBase
    {
        public const string DEFAULT_ID = "_default";
        public const string DEFAULT_NAME = "_Default";

        public TagType() : this(DEFAULT_ID, DEFAULT_NAME, int.MaxValue) { }
        public TagType(string id, string name, int priority)
        {
            _id = id;
            _name = name;
            _priority = priority;
        }

        private string _id = DEFAULT_ID;
        public string Id
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

        private int _priority = int.MaxValue;
        public int Priority
        {
            get { return _priority; }
            set
            {
                if (_priority != value)
                {
                    _priority = value;
                    OnPropertyChanged("Priority");
                }
            }
        }

        private string _name = DEFAULT_NAME;
        public string Name
        {
            get { return _name; }
            set
            {
                if (_name != value)
                {
                    _name = value;
                    OnPropertyChanged("Name");
                }
            }
        }

        public TagType Self
        {
            get { return this; }
        }

        private static TagType defaultInstance = null;
        public static TagType Default
        {
            get
            {
                if (defaultInstance == null)
                {
                    defaultInstance = new TagType(TagType.DEFAULT_ID, TagType.DEFAULT_NAME, int.MaxValue);
                }
                return defaultInstance;
            }
        }

        public override string ToString()
        {
            if (this.Priority == int.MaxValue)
            {
                return this.Name;
            }
            else
            {
                return this.Priority + ":" + this.Name;
            }
        }
        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is TagType)) return false;
            TagType tagType = obj as TagType;
            return (tagType.Priority == this.Priority && tagType.Name == this.Name);
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public void WriteXml(XmlWriter writer)
        {
            writer.WriteStartElement("tagtype");

            writer.WriteAttributeString("id", Id);
            writer.WriteAttributeString("name", Name);
            writer.WriteAttributeString("priority", Priority.ToString());

            writer.WriteEndElement();
        }

        public static TagType ReadXml(Project project, XmlNode node)
        {
            if (node.Name != "tagtype") return null;

            XmlAttribute idAttr = node.Attributes["id"];
            string id = idAttr == null ? "" : idAttr.InnerText.Trim();

            XmlAttribute nameAttr = node.Attributes["name"];
            string name = nameAttr == null ? "" : nameAttr.InnerText.Trim();

            XmlAttribute priorityAttr = node.Attributes["priority"];
            int priority;
            if (priorityAttr == null || !int.TryParse(priorityAttr.InnerText, out priority)) priority = int.MaxValue;

            return new TagType(id, name, priority);
        }
    }

    public class TagTypeCollection : ObservableCollection<TagType>
    {
        public TagTypeCollection()
            : base()
        {
            _rand = new Random(DateTime.Now.Millisecond);
            _idMap = new Dictionary<string, TagType>();
            this.Add(TagType.Default);
        }

        public TagTypeCollection(TagTypeCollection collection)
            : base(collection)
        {
            _rand = new Random(DateTime.Now.Millisecond);
            _idMap = new Dictionary<string, TagType>();
        }

        #region uniqId
        public TagType FindById(string id)
        {
            if (_idMap.ContainsKey(id)) return _idMap[id];
            else return null;
        }
        private Dictionary<string, TagType> _idMap = null;
        private Random _rand = null;
        private string getUniqId()
        {
            string ret = TagType.DEFAULT_ID;
            while (ret == TagType.DEFAULT_ID || _idMap.ContainsKey(ret))
            {
                ret = _rand.Next().ToString();
            }
            return ret;
        }
        public void SetUniqId(TagType tt)
        {
            if (tt.Id == TagType.DEFAULT_ID)
            {
                tt.Id = getUniqId();
                _idMap.Add(tt.Id, tt);
                tt.PropertyChanged += new PropertyChangedEventHandler(tt_PropertyChanged);
            }
            else if(!_idMap.ContainsKey(tt.Id))
            {
                _idMap.Add(tt.Id, tt);
                tt.PropertyChanged += new PropertyChangedEventHandler(tt_PropertyChanged);
            }
        }

        void tt_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            NotifyCollectionChangedEventArgs arg =
                new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, sender);
            OnCollectionChanged(arg);
        }
        #endregion

        #region 追加系override
        new public void Add(TagType tt)
        {
            if (tt.Id == TagType.DEFAULT_ID) tt.Id = getUniqId();
            _idMap.Add(tt.Id, tt);
            tt.PropertyChanged += new PropertyChangedEventHandler(tt_PropertyChanged);
            base.Add(tt);
        }
        new public void Insert(int index, TagType tt)
        {
            if (tt.Id == TagType.DEFAULT_ID) tt.Id = getUniqId();
            _idMap.Add(tt.Id, tt);
            tt.PropertyChanged += new PropertyChangedEventHandler(tt_PropertyChanged);
            base.Insert(index, tt);
        }
        #endregion


        #region load/save

        public void WriteXml(XmlWriter writer)
        {
            writer.WriteStartElement("tagtypes");

            foreach (TagType tagType in this)
            {
                if (tagType == TagType.Default) continue;
                tagType.WriteXml(writer);
            }

            writer.WriteEndElement();
        }

        public static TagTypeCollection ReadXml(Project project, XmlNode node)
        {
            if (node.Name != "tagtypes") return null;

            TagTypeCollection col = new TagTypeCollection();

            foreach (XmlNode child in node.ChildNodes)
            {
                TagType tt = TagType.ReadXml(project, child);
                if (tt != null)
                {
                    col.Add(tt);
                }
            }

            return col;
        }

        #endregion
    }
}
