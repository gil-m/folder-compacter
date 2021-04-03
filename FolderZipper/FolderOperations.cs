using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FolderZipper
{
    public static class FolderOperations
    {
        /// <summary>
        /// Copies the destination folder files and subdirectories to a temporary folder applying the exclusions.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="destination"></param>
        /// <param name="copySubDirs"></param>
        public static DirectoryInfo DirectoryCopy(string source,
                                          string destination,
                                          string[] excludeDirectories,
                                          string[] excludeFiles,
                                          string[] excludeExtensions,
                                          bool copySubDirs)
        {
            // Get the subdirectories for the specified directory.
            DirectoryInfo dir = new DirectoryInfo(source);

            if (!dir.Exists)
            {
                throw new DirectoryNotFoundException(
                    "Source directory does not exist or could not be found: "
                    + source);
            }

            List<DirectoryInfo> dirs = dir.GetDirectories().ToList();

            if (excludeDirectories != null && excludeDirectories.Any())
                dirs = dirs.Where(x => !excludeDirectories.Contains(x.Name)).ToList();

            // If the destination directory doesn't exist, create it.       
            var dest = Directory.CreateDirectory(destination);

            // Get the files in the directory and copy them to the new location.
            List<FileInfo> files = dir.GetFiles().ToList();

            if (excludeFiles != null && excludeFiles.Any())
                files = files.Where(x => !excludeFiles.Contains(x.Name)).ToList();
            if (excludeExtensions != null && excludeExtensions.Any())
                files = files.Where(x => !excludeExtensions.Contains(x.Extension)).ToList();

            foreach (FileInfo file in files)
            {
                string tempPath = Path.Combine(destination, file.Name);
                file.CopyTo(tempPath, false);
            }

            // If copying subdirectories, copy them and their contents to new location.
            if (copySubDirs)
            {
                foreach (DirectoryInfo subdir in dirs)
                {
                    string tempPath = Path.Combine(destination, subdir.Name);
                    DirectoryCopy(subdir.FullName, tempPath, excludeDirectories, excludeFiles, excludeExtensions, copySubDirs);
                }
            }

            return dest;
        }

        public static void RecursiveDelete(string path)
        {
            DirectoryInfo directory = new DirectoryInfo(path);

            RecursiveDelete(directory);
        }

        public static void RecursiveDelete(DirectoryInfo directory)
        {
            if (!directory.Exists)
                return;

            foreach (var dir in directory.EnumerateDirectories())
            {
                RecursiveDelete(dir.FullName);
            }

            directory.Delete(true);
        }
    }
}
