using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;

namespace FolderZipper
{
    public class Zipper
    {
        public readonly Arguments _arguments;

        public Zipper(Arguments arguments)
        {
            _arguments = arguments;
        }

        public string CompactFolder()
        {
            var rootDir = new DirectoryInfo(_arguments.Path);

            if (!rootDir.Exists)
            {
                throw new DirectoryNotFoundException($"Source directory does not exist or could not be found: {rootDir.FullName}");
            }

            var excludeDirectories = _arguments.ExcludedDirectories?.ToArray();
            var excludeFiles = _arguments.ExcludedFiles?.ToArray();
            var excludeExtensions = _arguments.ExcludedExtensions?.Select(x =>
            {
                if (!x.StartsWith("."))
                {
                    return $".{x}";
                }

                return x;
            }).ToArray();
            string destination = Path.Combine(Environment.CurrentDirectory, $"{_arguments.Name}.zip");

            if ((_arguments.Output == OutputOptions.fileshare || _arguments.Output == OutputOptions.localfile) && !string.IsNullOrWhiteSpace(_arguments.AdditionalParam))
            {
                destination = Path.Combine(_arguments.AdditionalParam, $"{_arguments.Name}.zip");
            }

            if (!Directory.Exists(Path.GetDirectoryName(destination)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(destination));
            }

            if (File.Exists(destination))
            {
                File.Delete(destination);
            }

            List<FileInfo> files = FolderOperations.GetFilesFromFolder(rootDir, excludeDirectories, excludeFiles, excludeExtensions);

            using (FileStream zipFileStream = new FileStream(destination, FileMode.Create))
            {
                using (ZipArchive archive = new ZipArchive(zipFileStream, ZipArchiveMode.Create))
                {
                    files.ForEach(file =>
                    {
                        var fullPath = file.FullName;
                        var pathMembers = fullPath.Split(Path.DirectorySeparatorChar).ToList();
                        var index = pathMembers.IndexOf(rootDir.Name);
                        var relativePath = Path.Combine(pathMembers.Skip(index + 1).ToArray());

                        archive.CreateEntryFromFile(file.FullName, relativePath, CompressionLevel.Fastest);
                    });
                }
            }

            return destination;
        }
    }
}
