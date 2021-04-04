using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FolderZipper.Tests
{
    public class FolderWrapper
    {
        public DirectoryInfo DirectoryInfo { get; private set; }
        public IEnumerable<FileInfo> Files { get; private set; }
        public IEnumerable<FolderWrapper> SubDirectories { get; private set; }

        public FolderWrapper(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            DirectoryInfo = new DirectoryInfo(path);
            RefreshFiles();
            RefreshSubDirectories();
        }

        public FolderWrapper(DirectoryInfo directoryInfo)
        {
            DirectoryInfo = directoryInfo;
            RefreshFiles();
            RefreshSubDirectories();
        }

        public void AddMockFile(string filename, string extension)
        {
            var path = Path.Combine(DirectoryInfo.FullName, $"{filename}.{extension}");
            if (!File.Exists(path))
            {
                File.Create(path).Dispose();
                RefreshFiles();
            }
        }

        public void AddMockDir(string name)
        {
            var path = Path.Combine(DirectoryInfo.FullName, name);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
                RefreshSubDirectories();
            }
        }

        private void RefreshFiles()
        {
            Files = DirectoryInfo.GetFiles();
        }

        private void RefreshSubDirectories()
        {
            SubDirectories = DirectoryInfo.GetDirectories().Select(x => new FolderWrapper(x));
        }

    }
}
