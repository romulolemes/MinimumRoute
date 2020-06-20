using MinimumRoute.Data;
using MinimumRoute.Exceptions;
using MinimumRoute.IO;
using MinimumRoute.Model;
using MinimumRoute.Service;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace MinimumRoute.UnitTests.Service
{
    public class FileServiceTest
    {
        [Fact]
        public void ReadAllText()
        {
            var mockFileSystem = new Mock<FileSystem>();
            mockFileSystem.Setup(s => s.ReadAllText(It.IsAny<string>())).Returns(It.IsAny<String>());

            FileService fileService = new FileService(mockFileSystem.Object);
            var content = fileService.ReadAllText(".\ttt");

            mockFileSystem.Verify(s => s.ReadAllText(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public void WriteAllText()
        {
            var mockFileSystem = new Mock<FileSystem>();
            mockFileSystem.Setup(s => s.WriteAllText(It.IsAny<string>(), It.IsAny<string>()));

            FileService fileService = new FileService(mockFileSystem.Object);
            fileService.WriteAllText("Path", "Content");

            mockFileSystem.Verify(s => s.WriteAllText(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        }
    }
}
