using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace Densha
{
    class Config
    {
        private Config()
        {
            TagDelimiter = "_";
            ThumbnailSize = new Size(128, 96);
            DateTimeDisplayFormat = "MM/dd HH:mm:ss";
            DefaultThumbnailNamePattern = "s-{1}.{2}";

            StatusProgressBarWidth = 400;

            DesignConfig = new DesignConfig();
        }

        public string TagDelimiter { get; set; }
        public Size ThumbnailSize { get; set; }
        public string DateTimeDisplayFormat { get; set; }
        public string DefaultThumbnailNamePattern { get; set; }

        public int StatusProgressBarWidth { get; set; }

        public DesignConfig DesignConfig { get; set; }
        [XmlIgnore()]
        public static DesignConfig Designs
        {
            get
            {
                return Instance.DesignConfig;
            }
        }

        private const string _configPath = "config.xml";
        [XmlIgnore()]
        public static string ConfigPath
        {
            get { return Path.GetFullPath(_configPath); }
        }

        private static Config _instance = null;
        [XmlIgnore()]
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
            XmlSerializer serializer = new XmlSerializer(typeof(Config));
            using (FileStream stream = new FileStream(ConfigPath,
                FileMode.Open, FileAccess.Read))
            {
                Config ret = (Config)serializer.Deserialize(stream);
                return ret;
            }
        }

        public void Save()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Config));
            using (FileStream stream = new FileStream(ConfigPath, FileMode.OpenOrCreate, FileAccess.Write))
            {
                serializer.Serialize(stream, this);
            }
        }
    }


    class DesignConfig
    {
        public DesignConfig()
        {
            _designs = new Dictionary<string, Design>();

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
        public class Design
        {
            public string Name;
            public string Description;
            public object Value;
        }

        private Dictionary<string, Design> _designs = null;
        public Dictionary<string, Design> Designs { get { return _designs; } }
        private void addDesign(string name, string desc, object value)
        {
            Design d = new Design();
            d.Name = name;
            d.Description = desc;
            d.Value = value;
            if (!_designs.ContainsKey(name))
            {
                _designs.Add(name, d);
            }
        }
        private Color getColor(string label)
        {
            return (Color)_designs[label].Value;
        }
        private Font getFont(string label)
        {
            return (Font)_designs[label].Value;
        }
        private void setValue(string label, object value)
        {
            _designs[label].Value = value;
        }

        private const string LABEL_ItemBGColor = "ItemBGColor";
        public Color ItemBGColor
        {
            get { return getColor(LABEL_ItemBGColor); }
            set { setValue(LABEL_ItemBGColor, value); }
        }

        private const string LABEL_ItemBGSelectedColor = "ItemBGSelectedColor";
        public Color ItemBGSelectedColor
        {
            get { return getColor(LABEL_ItemBGSelectedColor); }
            set { setValue(LABEL_ItemBGSelectedColor, value); }
        }

        private const string LABEL_ItemFGColor = "ItemFGColor";
        public Color ItemFGColor
        {
            get { return getColor(LABEL_ItemFGColor); }
            set { setValue(LABEL_ItemFGColor, value); }
        }

        private const string LABEL_ItemFGSelectedColor = "ItemFGSelectedColor";
        public Color ItemFGSelectedColor
        {
            get { return getColor(LABEL_ItemFGSelectedColor); }
            set { setValue(LABEL_ItemFGSelectedColor, value); }
        }

        private const string LABEL_ItemTextFont = "ItemTextFont";
        public Font ItemTextFont
        {
            get { return getFont(LABEL_ItemTextFont); }
            set { setValue(LABEL_ItemTextFont, value); }
        }

        private const string LABEL_TagBoxBGColor = "TagBoxBGColor";
        public Color TagBoxBGColor
        {
            get { return getColor(LABEL_TagBoxBGColor); }
            set { setValue(LABEL_TagBoxBGColor, value); }
        }
        private const string LABEL_TagBoxFGColor = "TagBoxFGColor";
        public Color TagBoxFGColor
        {
            get { return getColor(LABEL_TagBoxFGColor); }
            set { setValue(LABEL_TagBoxFGColor, value); }
        }

        private const string LABEL_TagItemBGColor = "TagItemBGColor";
        public Color TagItemBGColor
        {
            get { return getColor(LABEL_TagItemBGColor); }
            set { setValue(LABEL_TagItemBGColor, value); }
        }
        private const string LABEL_TagItemFGColor = "TagItemFGColor";
        public Color TagItemFGColor
        {
            get { return getColor(LABEL_TagItemFGColor); }
            set { setValue(LABEL_TagItemFGColor, value); }
        }
    }
}
