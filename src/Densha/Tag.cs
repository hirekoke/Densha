using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;

namespace Densha
{
    public class Tag
    {
        public const int DEFAULT_ID = -1;

        public Tag()
        {
            Id = DEFAULT_ID;
            Type = TagType.Default;
            FileString = "";
            Description = "";
        }
        public Tag(TagType type, string idString, string description)
        {
            this.Id = DEFAULT_ID;
            this.Type = type;
            this.FileString = idString;
            this.Description = description;
        }

        public override string ToString()
        {
            return ("Tag{" + FileString + "," + Description + "(" + Id.ToString() + ")");
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

        public int Id { get; set; }

        private string _description = "";
        public string Description
        {
            get { return _description; }
            set
            {
                _description = value == null ? "" : value;
            }
        }

        private string _fileString = "";
        public string FileString
        {
            get { return _fileString; }
            set
            {
                _fileString = value == null ? "" : value;
            }
        }
        public TagType Type { get; set; }

        public void WriteXml(XmlWriter writer)
        {
            writer.WriteStartElement("tag");

            writer.WriteAttributeString("id", Id.ToString());
            writer.WriteAttributeString("type", Type.ID);
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
                type = project.TagTypes.Find(delegate(TagType tt) {
                    return tt.ID == tid;
                }) ?? TagType.Default;
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

    public class TagCollection : List<Tag>
    {
        public TagCollection(Project project)
            : base()
        {
            this.Project = project;
            _rand = new Random(DateTime.Now.Millisecond);
            _idMap = new Dictionary<int, Tag>();
        }

        public Project Project { get; private set; }
        public Tag FindById(int id)
        {
            if (_idMap.ContainsKey(id)) return _idMap[id];
            else return null;
        }

        private Dictionary<int, Tag> _idMap = null;
        private Random _rand = null;

        #region 追加系override
        new public void Add(Tag tag)
        {
            if (tag.Id == Tag.DEFAULT_ID) tag.Id = getUniqId();
            _idMap.Add(tag.Id, tag);
            base.Add(tag);
        }
        new public void AddRange(IEnumerable<Tag> tags)
        {
            foreach (Tag tag in tags)
            {
                Add(tag);
            }
        }
        new public void Insert(int index, Tag tag)
        {
            if (tag.Id == Tag.DEFAULT_ID) tag.Id = getUniqId();
            _idMap.Add(tag.Id, tag);
            base.Insert(index, tag);
        }
        new public void InsertRange(int index, IEnumerable<Tag> tags)
        {
            int i = index;
            foreach (Tag tag in tags)
            {
                Insert(i++, tag);
            }
        }
        #endregion

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
                tag.Id = getUniqId();
                _idMap.Add(tag.Id, tag);
            }
        }

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

