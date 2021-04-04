using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FolderZipper
{
    public static class FolderOperations
    {
        public static List<FileInfo> GetFilesFromFolder(DirectoryInfo rootDir, string[] excludeDirectories, string[] excludeFiles, string[] excludeExtensions)
        {
            List<DirectoryInfo> dirs = rootDir.GetDirectories().ToList();
            List<FileInfo> files = new List<FileInfo>();
            foreach(var dir in dirs)
            {
                if (excludeDirectories.Any() && excludeDirectories.Contains(dir.Name)) continue;
                
                files.AddRange(GetFilesFromFolder(dir, excludeDirectories, excludeFiles, excludeExtensions));
            }

            files.AddRange(FilterFiles(rootDir.GetFiles().ToList(), excludeFiles, excludeExtensions));
            return files;
        }

        private static List<FileInfo> FilterFiles(List<FileInfo> files, string[] excludeFiles, string[] excludeExtensions)
        {

            if (excludeFiles != null && excludeFiles.Any())
                files = files.Where(x => !excludeFiles.Contains(x.Name)).ToList();

            if (excludeExtensions != null && excludeExtensions.Any())
                files = files.Where(x => !excludeExtensions.Contains(x.Extension)).ToList();

            return files;
        }
    }
}
