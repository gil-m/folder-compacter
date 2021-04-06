[![.NET](https://github.com/gil-m/folder-zipper/actions/workflows/dotnet.yml/badge.svg?branch=main)](https://github.com/gil-m/folder-zipper/actions/workflows/dotnet.yml)
# folder-zipper
.Net executable that compacts a folder into a zip file, allowing the user to provide a list of extensions, files or subdirectories to exclude.

## Requirements

The goal is to build a console application which is able to create a zip file from a folder and respective sub-folders, 
allowing the exclusion of determined extensions, subfolders or file names. The program also should allow the output file
to be generated in a local folder or copyed to a shared folder ~or sent to WeTransfer~. The program should be developed
using the best OOP and SOLID practices, as well respective integration and unit tests.

### Requirement #1
As an user, I can invoke the application through the command line passing as arguments:
- the folder to zip
- the name of the file to be generated
- a list of extensions to exclude
- a list of directories to exclude
- a list of files to exclude
- output type (localfile | fileShare)
- optional parameters according to the type of output

### Requirement #2
All files and folders must be included in the output file as a zip file.

### Requirement #3
Create an output design in which it's easy to include new outputs.

### Requirement #4
Develop unit tests for the code achieving the maximum coverage possible.

### Requirement #5
The application must be executed in .Net Core 2.1 and .Net Framework 4.8. The language is C# and possible IDEs are Visual Studio or Visual Studio Code.
