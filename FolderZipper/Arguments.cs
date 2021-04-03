using CommandLine;
using System.Collections.Generic;

namespace FolderZipper
{
    public class Arguments
    {
        [Value(0, Required = true, HelpText = "Path of the folder to be compacted.", MetaName = "path", MetaValue = @"C:\path\to\folder")]
        public string Path { get; set; }
        [Value(1, Required = true, HelpText = "Name of the file generated (without the extension).", MetaName = "name", MetaValue = "zipped_folder")]
        public string Name { get; set; }
        [Option('e', "extensions", Required = false, HelpText = "List of extensions to be excluded.")]
        public IEnumerable<string> ExcludeExtensions { get; set; }
        [Option('d', "directories", Required = false, HelpText = "List of directories to be excluded.")]
        public IEnumerable<string> ExcludeDirectories { get; set; }
        [Option('f', "files", Required = false, HelpText = "List of files to be excluded.")]
        public IEnumerable<string> ExcludeFiles { get; set; }
        [Option('o', "files", Required = false, HelpText = "List of files to be excluded.")]
        public string Output { get; set; }
        [Option(Min = 1, Required = false, HelpText = "Additional parameters")]
        public IEnumerable<string> AdditionalParams { get; set; }
    }
}
