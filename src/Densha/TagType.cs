using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;

namespace Densha {
    public class TagType
    {
        public TagType() : this("others", "Others", int.MaxValue) { }
        public TagType(string id, string name, int priority)
        {
            this.ID = id;
            this.Name = name;
            this.Priority = priority;
        }

        public string ID { get; set; }
        public int Priority { get; set; }
        public string Name { get; set; }

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
                    defaultInstance = new TagType("default", "Default", int.MaxValue);
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

            writer.WriteAttributeString("id", ID);
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

    public class TagTypeCollection : List<TagType>
    {
        public TagTypeCollection()
            : base()
        {
            this.Add(TagType.Default);
        }

        public TagTypeCollection(TagTypeCollection collection)
            : base(collection)
        {
        }

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
