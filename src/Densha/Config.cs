using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

using System.IO;
using System.Xml;

namespace Densha
{
    public class Config
    {
        private Config()
        {
            TagDelimiter = "_";
            ThumbnailSize = new Size(128, 96);

            DateTimeDisplayFormat = "MM/dd HH:mm:ss";
            DefaultThumbnailNamePattern = "s-{1}.{2}";

            DesignConfig = new DesignConfig();
        }

        public string TagDelimiter { get; set; }
        public Size ThumbnailSize { get; set; }
        public int MinimumItemWidth { get { return ThumbnailSize.Width + 10; } }

        public string DateTimeDisplayFormat { get; set; }
        public string DefaultThumbnailNamePattern { get; set; }

        public DesignConfig DesignConfig { get; set; }
        public static DesignConfig Designs
        {
            get
            {
                return Instance.DesignConfig;
            }
        }

        private const string _configPath = "config.xml";
        public static string ConfigPath
        {
            get { return Path.GetFullPath(_configPath); }
        }

        private static Config _instance = null;
        public static Config Instance
        {
            get
            {
                if (_instance == null)
                {
                    if (File.Exists(ConfigPath))
                    {
                        _instance = Load();
                    }
                    else
                    {
                        _instance = new Config();
                    }
                }
                return _instance;
            }
        }

        public static Config Load()
        {
            Config config = new Config();

            XmlDocument doc = new XmlDocument();
            using (StreamReader reader = new StreamReader(Config.ConfigPath, Encoding.UTF8))
            {
                doc.Load(reader);

                XmlNode node;

                node = doc.SelectSingleNode("/config/TagDelimiter");
                if (node != null)
                    config.TagDelimiter = node.InnerText.Trim();

                node = doc.SelectSingleNode("/config/DefaultThumbnailNamePattern");
                if (node != null)
                    config.DefaultThumbnailNamePattern = node.InnerText.Trim();

                node = doc.SelectSingleNode("/config/DateTimeDisplayFormat");
                if (node != null)
                    config.DateTimeDisplayFormat = node.InnerText.Trim();

                node = doc.SelectSingleNode("/config/ThumbnailSize");
                #region ThumbnailSize
                if (node != null)
                {
                    int width = config.ThumbnailSize.Width;
                    int height = config.ThumbnailSize.Height;
                    foreach (XmlNode sizeChildren in node.ChildNodes)
                    {
                        switch (sizeChildren.Name)
                        {
                            case "Width":
                                width = int.Parse(sizeChildren.InnerText.Trim());
                                break;
                            case "Height":
                                height = int.Parse(sizeChildren.InnerText.Trim());
                                break;
                        }
                    }
                    config.ThumbnailSize = new Size(width, height);
                }
                #endregion

                node = doc.SelectSingleNode("/config/designs");
                if (node == null)
                    config.DesignConfig = new DesignConfig();
                else
                    config.DesignConfig = DesignConfig.ReadXml(node);
            }

            return config;
        }

        public void Save()
        {
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.OmitXmlDeclaration = false;
            settings.CheckCharacters = true;

            using (XmlWriter writer = XmlWriter.Create(ConfigPath, settings))
            {
                writer.WriteStartDocument();
                writer.WriteStartElement("config");

                writer.WriteElementString("TagDelimiter", this.TagDelimiter);
                writer.WriteElementString("DefaultThumbnailNamePattern", this.DefaultThumbnailNamePattern);
                writer.WriteElementString("DateTimeDisplayFormat", this.DateTimeDisplayFormat);
                writer.WriteStartElement("ThumbnailSize");
                writer.WriteElementString("Width", ThumbnailSize.Width.ToString());
                writer.WriteElementString("Height", ThumbnailSize.Height.ToString());
                writer.WriteEndElement();
                this.DesignConfig.WriteXml(writer);

                writer.WriteEndElement();
                writer.WriteEndDocument();
            }
        }
    }

    public class Design
    {
        public string Name;
        public string Description;
        public object Value;
    }

    public class DesignConfig
    {
        public DesignConfig()
        {
            _designsDic = new Dictionary<string, Design>();
            _designsList = new List<Design>();

            addDesign(LABEL_ItemBGColor,
                "", 
                SystemColors.Control);
            addDesign(LABEL_ItemBGSelectedColor,
                "",
                SystemColors.Highlight);
            addDesign(LABEL_ItemFGColor,
                "",
                SystemColors.ControlText);
            addDesign(LABEL_ItemFGSelectedColor,
                "",
                SystemColors.HighlightText);


            addDesign(LABEL_ItemTextFont,
                "",
                SystemFonts.DialogFont);

            addDesign(LABEL_TagBoxBGColor,
                "",
                SystemColors.Control);
            addDesign(LABEL_TagBoxFGColor,
                "",
                SystemColors.ControlText);

            addDesign(LABEL_TagItemBGColor,
                "",
                Color.White);
            addDesign(LABEL_TagItemFGColor,
                "",
                Color.Indigo);
        }

        private List<Design> _designsList = null;
        private Dictionary<string, Design> _designsDic = null;
        public List<Design> Designs { get { return _designsList; } }
        public IEnumerator<Design> GetEnumerator()
        {
            foreach (Design design in _designsList)
            {
                yield return design;
            }
        }

        private void addDesign(string name, string desc, object value)
        {
            Design d = new Design();
            d.Name = name;
            d.Description = desc;
            d.Value = value;
            if (!_designsDic.ContainsKey(name))
            {
                _designsList.Add(d);
                _designsDic.Add(name, d);
            }
        }
        private Color getColor(string label)
        {
            return (Color)_designsDic[label].Value;
        }
        private Font getFont(string label)
        {
            return (Font)_designsDic[label].Value;
        }
        public void SetValue(string label, object value)
        {
            _designsDic[label].Value = value;
        }

        #region 定義
        private const string LABEL_ItemBGColor = "ItemBGColor";
        public Color ItemBGColor
        {
            get { return getColor(LABEL_ItemBGColor); }
            set { SetValue(LABEL_ItemBGColor, value); }
        }

        private const string LABEL_ItemBGSelectedColor = "ItemBGSelectedColor";
        public Color ItemBGSelectedColor
        {
            get { return getColor(LABEL_ItemBGSelectedColor); }
            set { SetValue(LABEL_ItemBGSelectedColor, value); }
        }

        private const string LABEL_ItemFGColor = "ItemFGColor";
        public Color ItemFGColor
        {
            get { return getColor(LABEL_ItemFGColor); }
            set { SetValue(LABEL_ItemFGColor, value); }
        }

        private const string LABEL_ItemFGSelectedColor = "ItemFGSelectedColor";
        public Color ItemFGSelectedColor
        {
            get { return getColor(LABEL_ItemFGSelectedColor); }
            set { SetValue(LABEL_ItemFGSelectedColor, value); }
        }

        private const string LABEL_ItemTextFont = "ItemTextFont";
        public Font ItemTextFont
        {
            get { return getFont(LABEL_ItemTextFont); }
            set { SetValue(LABEL_ItemTextFont, value); }
        }

        private const string LABEL_TagBoxBGColor = "TagBoxBGColor";
        public Color TagBoxBGColor
        {
            get { return getColor(LABEL_TagBoxBGColor); }
            set { SetValue(LABEL_TagBoxBGColor, value); }
        }
        private const string LABEL_TagBoxFGColor = "TagBoxFGColor";
        public Color TagBoxFGColor
        {
            get { return getColor(LABEL_TagBoxFGColor); }
            set { SetValue(LABEL_TagBoxFGColor, value); }
        }

        private const string LABEL_TagItemBGColor = "TagItemBGColor";
        public Color TagItemBGColor
        {
            get { return getColor(LABEL_TagItemBGColor); }
            set { SetValue(LABEL_TagItemBGColor, value); }
        }
        private const string LABEL_TagItemFGColor = "TagItemFGColor";
        public Color TagItemFGColor
        {
            get { return getColor(LABEL_TagItemFGColor); }
            set { SetValue(LABEL_TagItemFGColor, value); }
        }
        #endregion


        #region IXmlSerializable メンバ

        private static object parseValueXml(XmlNode node)
        {
            switch (node.Name)
            {
                case "NamedColor":
                    {
                        XmlAttribute attr = node.Attributes["name"];
                        if (attr == null) return null;
                        return Color.FromName(attr.InnerText.Trim());
                    }
                case "CustomColor":
                    {
                        int a = -1; int r = -1; int g = -1; int b = -1;
                        foreach (XmlAttribute attr in node.Attributes)
                        {
                            int val = -1;
                            if (int.TryParse(attr.InnerText.Trim(), out val))
                            {
                                switch (attr.Name)
                                {
                                    case "alpha":
                                        a = val; break;
                                    case "red":
                                        r = val; break;
                                    case "green":
                                        g = val; break;
                                    case "blue":
                                        b = val; break;
                                }
                            }
                        }
                        if (a < 0 || r < 0 || g < 0 || b < 0) return null;
                        return Color.FromArgb(a, r, g, b);
                    }
                case "SystemFont":
                    {
                        XmlAttribute attr = node.Attributes["name"];
                        if (attr == null) return null;
                        return SystemFonts.GetFontByName(attr.InnerText.Trim());
                    }
                case "Font":
                    {
                        string name = null;
                        float emSize = -1f;
                        FontStyle style = FontStyle.Regular;
                        foreach (XmlAttribute attr in node.Attributes)
                        {
                            switch (attr.Name)
                            {
                                case "name":
                                    name = attr.InnerText.Trim(); break;
                                case "size":
                                    emSize = float.Parse(attr.InnerText.Trim()); break;
                                case "style":
                                    {
                                        object v = Enum.Parse(typeof(FontStyle), attr.InnerText.Trim());
                                        if (v != null) style = (FontStyle)v;
                                        break;
                                    }
                            }
                        }
                        if (name == null) return null;
                        return new Font(name, emSize, style, GraphicsUnit.Point);
                    }
                default:
                    return null;
            }
        }
        private static void writeValueXml(XmlWriter writer, object value)
        {
            if (value is Color)
            {
                Color c = (Color)value;
                if (c.IsNamedColor)
                {
                    writer.WriteStartElement("NamedColor");
                    writer.WriteAttributeString("name", c.Name);
                    writer.WriteEndElement();
                }
                else
                {
                    writer.WriteStartElement("CustomColor");
                    writer.WriteAttributeString("alpha", c.A.ToString());
                    writer.WriteAttributeString("red", c.R.ToString());
                    writer.WriteAttributeString("green", c.G.ToString());
                    writer.WriteAttributeString("blue", c.B.ToString());
                    writer.WriteEndElement();
                }
            }
            else if (value is Font)
            {
                Font f = (Font)value;
                if (f.IsSystemFont)
                {
                    writer.WriteStartElement("SystemFont");
                    writer.WriteAttributeString("name", f.SystemFontName);
                    writer.WriteEndElement();
                }
                else
                {
                    writer.WriteStartElement("Font");

                    writer.WriteAttributeString("name", f.Name);
                    writer.WriteAttributeString("size", f.SizeInPoints.ToString());
                    writer.WriteAttributeString("style", f.Style.ToString());

                    writer.WriteEndElement();
                }
            }
            else
            {
#warning TODO: error
            }
        }

        public static DesignConfig ReadXml(XmlNode node)
        {
            if (node.Name != "designs") return new DesignConfig();

            DesignConfig dc = new DesignConfig();

            foreach (XmlNode children in node.ChildNodes)
            {
                string name = children.Name;
                XmlAttribute attr = children.Attributes["type"];
                string type = attr == null ? "" : attr.InnerText.Trim();
                if (children.HasChildNodes)
                {
                    object value = parseValueXml(children.ChildNodes[0]);
                    if (value != null)
                        dc.SetValue(name, value);
                }
            }

            return dc;
        }

        public void WriteXml(XmlWriter writer)
        {
            writer.WriteStartElement("designs");
            foreach (Design design in _designsList)
            {
                writer.WriteStartElement(design.Name);
                writeValueXml(writer, design.Value);
                writer.WriteEndElement();
            }
            writer.WriteEndElement();
        }

        #endregion
    }
}
