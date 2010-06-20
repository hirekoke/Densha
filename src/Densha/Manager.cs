using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Densha
{
    class Manager
    {
        public Manager()
        {

        }

        private MainForm _mainForm = null;
        public MainForm MainForm
        {
            get { return _mainForm; }
            set { _mainForm = value; }
        }

        private Project _project = null;
        public Project Project
        {
            get { return _project; }
            set
            {
                _project = value;
                _mainForm.SetProject();
            }
        }

        public Project CreateProject(string origDirPath, string thumbDirPath)
        {
            Project project = new Project();
            project.OriginalPath = origDirPath;
            project.ThumbnailPath = thumbDirPath;
            project.TagTypes = new TagTypeCollection();
            Tag tag1 = new Tag(TagType.Default, "a", "あaaほあえｌｊうぇｌｇｋｊｗヶｇawefwefw");
            Tag tag2 = new Tag(TagType.Default, "c", "bbb");
            Tag tag3 = new Tag(TagType.Default, "ｂ", "あaaほあえｌｊうぇｌｇｋｊｗヶｇawefwefw");
            project.Tags.Add(tag1);
            project.Tags.Add(tag2);
            project.Tags.Add(tag3);

            DirectoryInfo dOriginalDirInfo = new DirectoryInfo(project.OriginalFullPath);
            DirectoryInfo dThumbnailDirInfo = new DirectoryInfo(project.ThumbnailFullPath);
            if (!dOriginalDirInfo.Exists)
            {

            }
            else
            {
                FileInfo[] fInfos = dOriginalDirInfo.GetFiles("*.jpg");
                for (int i = 0; i < 100; i++)
                {
                    foreach (FileInfo fInfo in fInfos)
                    {
                        DenshaImage img = new DenshaImage(project, fInfo.Name);
                        img.AddTag(tag1);
                        img.AddTag(tag2);
                        img.AddTag(tag3);
                        project.AddImage(img);
                    }
                }
            }

            return project;
        }


        public void SetUse(object sender, DenshaImage image, bool use)
        {
            image.IsUsed = use;
            if (_mainForm != null)
            {
                _mainForm.SetUse(sender, image, use);
            }
        }
    }
}
