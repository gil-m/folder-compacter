using FolderZipper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Linq;

namespace FolderZipper.Tests
{
    [TestClass]
    public class FolderOperationsTest
    {
        private readonly FolderWrapper _folderWrapper;

        public FolderOperationsTest()
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
        public void Test_GetFilesFromFolder_WithoutFilter()
        {
            var voidArray = new string[] { };
            var files = FolderOperations.GetFilesFromFolder(_folderWrapper.DirectoryInfo, voidArray, voidArray, voidArray);
            Assert.AreEqual(12, files.Count());
        }

        [TestMethod]
        public void Test_GetFilesFromFolder_WithOneExtensionFilter()
        {
            var voidArray = new string[] { };
            var extensions = new string[] { ".html" };
            var files = FolderOperations.GetFilesFromFolder(_folderWrapper.DirectoryInfo, voidArray, voidArray, extensions);
            Assert.AreEqual(8, files.Count());
        }
        
        [TestMethod]
        public void Test_GetFilesFromFolder_WithAllExtensionFilters()
        {
            var voidArray = new string[] { };
            var extensions = new string[] { ".html", ".css", ".js" };
            var files = FolderOperations.GetFilesFromFolder(_folderWrapper.DirectoryInfo, voidArray, voidArray, extensions);
            Assert.AreEqual(0, files.Count());
        }

        [TestMethod]
        public void Test_GetFilesFromFolder_WithOneFileFilter()
        {
            var voidArray = new string[] { };
            var excluded = new string[] { "test.html" };
            var files = FolderOperations.GetFilesFromFolder(_folderWrapper.DirectoryInfo, voidArray, excluded, voidArray);
            Assert.AreEqual(8, files.Count());
        }
        
        [TestMethod]
        public void Test_GetFilesFromFolder_WithAllFileFilters()
        {
            var voidArray = new string[] { };
            var excluded = new string[] { "test.html", "test.css", "test.js" };
            var files = FolderOperations.GetFilesFromFolder(_folderWrapper.DirectoryInfo, voidArray, excluded, voidArray);
            Assert.AreEqual(0, files.Count());
        }

        [TestMethod]
        public void Test_GetFilesFromFolder_WithOneDirectoryFilter()
        {
            var voidArray = new string[] { };
            var excluded = new string[] { "subdir1" };
            var files = FolderOperations.GetFilesFromFolder(_folderWrapper.DirectoryInfo, excluded, voidArray, voidArray);
            Assert.AreEqual(9, files.Count());
        }
        
        [TestMethod]
        public void Test_GetFilesFromFolder_WithAllDirectoryFilters()
        {
            var voidArray = new string[] { };
            var excluded = new string[] { "subdir1", "subdir2", "subdir3" };
            var files = FolderOperations.GetFilesFromFolder(_folderWrapper.DirectoryInfo, excluded, voidArray, voidArray);
            Assert.AreEqual(3, files.Count());
        }
    }
}
