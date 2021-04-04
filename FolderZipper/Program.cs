using CommandLine;
using System;

namespace FolderZipper
{
    class Program
    {
        static void Main(string[] args)
        {
            Parser.Default.ParseArguments<Arguments>(args).WithParsed(o =>
            {
                Console.WriteLine("Starting execution with these parameters:");
                Console.WriteLine($"{nameof(Arguments.Path)}: {o.Path}");
                Console.WriteLine($"{nameof(Arguments.Name)}: {o.Name}");
                Console.WriteLine($"{nameof(Arguments.ExcludedExtensions)}: {string.Join(",", o.ExcludedExtensions)}");
                Console.WriteLine($"{nameof(Arguments.ExcludedDirectories)}: {string.Join(",", o.ExcludedDirectories)}");
                Console.WriteLine($"{nameof(Arguments.ExcludedFiles)}: {string.Join(",", o.ExcludedFiles)}");
                Console.WriteLine($"{nameof(Arguments.Output)}: {o.Output}");
                Console.WriteLine($"{nameof(Arguments.AdditionalParam)}: {o.AdditionalParam}");

                var zipper = new Zipper(o);
                var dest = zipper.CompactFolder();

                Console.WriteLine($"File created! Location: {dest}");
            });
        }
    }
}
