using CommandLine;
using System;
using System.Collections.Generic;

namespace FolderZipper
{
    public class Arguments
    {
        /// <summary>
        /// The path where the files to be compressed are.
        /// </summary>
        [Value(0, Required = true, HelpText = "Path of the folder to be compacted.", MetaName = "path", MetaValue = @"C:\path\to\folder")]
        public string Path { get; set; }
        /// <summary>
        /// The name of the file to be generated (without extension).
        /// </summary>
        [Value(1, Required = true, HelpText = "Name of the file generated (without the extension).", MetaName = "name", MetaValue = "zipped_folder")]
        public string Name { get; set; }
        /// <summary>
        /// List of the extensions to be excluded. Any file with these extensions will be left out of the compressed file.
        /// </summary>
        [Option('e', "extensions", Required = false, HelpText = "List of extensions to be excluded separated by comma.", Separator = ',')]
        public IEnumerable<string> ExcludedExtensions { get; set; }
        /// <summary>
        /// List of the directories to be excluded. Any file inside these directories will be left out of the compressed file.
        /// </summary>
        [Option('d', "directories", Required = false, HelpText = "List of directories to be excluded separated by comma.", Separator = ',')]
        public IEnumerable<string> ExcludedDirectories { get; set; }
        /// <summary>
        /// List of the files to be excluded.
        /// </summary>
        [Option('f', "files", Required = false, HelpText = "List of files to be excluded separated by comma.", Separator = ',')]
        public IEnumerable<string> ExcludedFiles { get; set; }
        /// <summary>
        /// If provided, the compressed file will be copied to this destination.
        /// </summary>
        [Option('o', "output", Required = false, HelpText = "Output mode. Can be: localfile | fileshare | wetransfer")]
        public OutputOptions Output { get; set; }

        [Option('a', "additional_params", Required = false, HelpText = "Optional parameters according to the output selected.")]
        public string AdditionalParam { get; set; }

        public bool IsValid()
        {
            var isValid = true;

            if (string.IsNullOrWhiteSpace(Path))
            {
                Console.WriteLine("Empty value provided for Path");
                isValid &= false;
            }



            return isValid;
        }
    }

    public enum OutputOptions
    {
        localfile,
        fileshare,
        wetransfer,
    }
}
