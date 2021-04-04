using FolderZipper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FolderZipperTest
{
    [TestClass]
    public class ZipperTest
    {
        private readonly FolderWrapper _folderWrapper;

        public ZipperTest()
        {
            var baseDir = AppDomain.CurrentDomain.BaseDirectory;
            _folderWrapper = new FolderWrapper(Path.Combine(baseDir, "mock"));
        }

        [TestInitialize]
        public void Initialize()
        {
            _folderWrapper.AddMockDir("subdir1");
            _folderWrapper.AddMockDir("subdir2");
            _folderWrapper.AddMockDir("subdir3");

            _folderWrapper.SubDirectories.ToList().ForEach(subdir =>
            {
                subdir.AddMockFile("test", "html");
                subdir.AddMockFile("test", "js");
                subdir.AddMockFile("test", "css");
            });

            _folderWrapper.AddMockFile("test", "html");
            _folderWrapper.AddMockFile("test", "js");
            _folderWrapper.AddMockFile("test", "css");
        }

        [TestMethod]
        public void Test_ZipperCompactFolder_GivenInvalidFolder_ThrowsException()
        {
            var args = new Arguments
            {
                Path = Path.Combine(_folderWrapper.DirectoryInfo.FullName, Guid.NewGuid().ToString()),
                Name = Guid.NewGuid().ToString(),
            };
            var zipper = new Zipper(args);
            Assert.ThrowsException<DirectoryNotFoundException>(() =>
            {
                zipper.CompactFolder();
            });
        }

        [TestMethod]
        public void Test_ZipperCompactFolder_GivenValidFolder_CreatesFile()
        {
            var voidArray = new string[] { };
            var args = new Arguments
            {
                Path = _folderWrapper.DirectoryInfo.FullName,
                Name = Guid.NewGuid().ToString(),
                Output = OutputOptions.localfile,
                AdditionalParam = AppDomain.CurrentDomain.BaseDirectory,
                ExcludedDirectories = voidArray,
                ExcludedExtensions = voidArray,
                ExcludedFiles = voidArray,
            };

            var zipper = new Zipper(args);
            var destination = zipper.CompactFolder();

            Assert.IsTrue(File.Exists(destination));

            File.Delete(destination);
        }

        [TestMethod]
        public void Test_ZipperCompactFolder_GivenValidFolderWithExtensionFilter_CreatesFile()
        {
            var voidArray = new string[] { };
            var extensions = new string[] { ".html", ".css", ".js" };
            var args = new Arguments
            {
                Path = _folderWrapper.DirectoryInfo.FullName,
                Name = Guid.NewGuid().ToString(),
                Output = OutputOptions.localfile,
                AdditionalParam = AppDomain.CurrentDomain.BaseDirectory,
                ExcludedDirectories = voidArray,
                ExcludedExtensions = extensions,
                ExcludedFiles = voidArray,
            };

            var zipper = new Zipper(args);
            var destination = zipper.CompactFolder();

            Assert.IsTrue(File.Exists(destination));

            File.Delete(destination);
        }

        [TestMethod]
        public void Test_ZipperCompactFolder_GivenValidFolderWithFileFilter_CreatesFile()
        {
            var voidArray = new string[] { };
            var files = new string[] { "test.html", "test.css", "test.js" };
            var args = new Arguments
            {
                Path = _folderWrapper.DirectoryInfo.FullName,
                Name = Guid.NewGuid().ToString(),
                Output = OutputOptions.localfile,
                AdditionalParam = AppDomain.CurrentDomain.BaseDirectory,
                ExcludedDirectories = voidArray,
                ExcludedExtensions = voidArray,
                ExcludedFiles = files,
            };

            var zipper = new Zipper(args);
            var destination = zipper.CompactFolder();

            Assert.IsTrue(File.Exists(destination));

            File.Delete(destination);
        }

        [TestMethod]
        public void Test_ZipperCompactFolder_GivenValidFolderWithDirectoryFilter_CreatesFile()
        {
            var voidArray = new string[] { };
            var folders = new string[] { "subdir1", "subdir2", "subdir3" };
            var args = new Arguments
            {
                Path = _folderWrapper.DirectoryInfo.FullName,
                Name = Guid.NewGuid().ToString(),
                Output = OutputOptions.localfile,
                AdditionalParam = AppDomain.CurrentDomain.BaseDirectory,
                ExcludedDirectories = folders,
                ExcludedExtensions = voidArray,
                ExcludedFiles = voidArray,
            };

            var zipper = new Zipper(args);
            var destination = zipper.CompactFolder();

            Assert.IsTrue(File.Exists(destination));

            File.Delete(destination);
        }
    }
}
