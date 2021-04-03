using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FolderZipper
{
    public class Zipper
    {
        public readonly Arguments _arguments;
        private readonly string _temporaryFolder;

        public Zipper(Arguments arguments)
        {
            _arguments = arguments;
            _temporaryFolder = Path.Combine(Environment.GetEnvironmentVariable("USERPROFILE"), ".temp_zip", Guid.NewGuid().ToString());
        }

        public void CompactFolder()
        {
            var excludedDirectories = _arguments.ExcludedDirectories.ToArray();
            var excludedFiles = _arguments.ExcludedFiles.ToArray();
            var excludedExtensions = _arguments.ExcludedExtensions.ToArray();

            try
            {
                var dest = FolderOperations.DirectoryCopy(_arguments.Path, _temporaryFolder, excludedDirectories, excludedFiles, excludedExtensions, true);
            }
            finally
            {
                FolderOperations.RecursiveDelete(_temporaryFolder);
            }

        }
    }
}
