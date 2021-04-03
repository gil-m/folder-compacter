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
                Console.WriteLine($"{nameof(Arguments.ExcludeExtensions)}: {string.Join(",", o.ExcludeExtensions)}");
                Console.WriteLine($"{nameof(Arguments.ExcludeDirectories)}: {string.Join(",", o.ExcludeDirectories)}");
                Console.WriteLine($"{nameof(Arguments.ExcludeFiles)}: {string.Join(",", o.ExcludeFiles)}");
                Console.WriteLine($"{nameof(Arguments.Output)}: {o.Output}");
                Console.WriteLine($"{nameof(Arguments.AdditionalParams)}: {string.Join(",", o.AdditionalParams)}");
            });
        }
    }
}
