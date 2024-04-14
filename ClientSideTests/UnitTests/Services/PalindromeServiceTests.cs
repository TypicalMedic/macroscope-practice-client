using ClientSide.Data.Interfaces;
using ClientSide.Models;
using ClientSide.PalindromeValidator.Interfaces;
using ClientSide.Services;
using ClientSide.Services.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientSideTests.UnitTests.Services
{
    [TestClass]
    public class PalindromeServiceTests
    {
        private readonly Mock<IData> DataMock = new();
        private readonly Mock<IPalindromeValidator> ValidatorMock = new();

        private PalindromeService SetupServiceWithMocks(string dirPath, Dictionary<string, TextFile> files)
        {
            DataMock.Setup(dMock => dMock.GetDirFileNames(dirPath)).Returns(files.Keys);
            foreach (string fileName in files.Keys)
            {
                var file = files[fileName];
                DataMock.Setup(dMock => dMock.GetFile(fileName)).Returns(file);
                ValidatorMock.Setup(validatorMock => validatorMock.IsValid(file.Text)).Returns(file.IsPalindrome);
            }
            return new PalindromeService(DataMock.Object, ValidatorMock.Object);
        }

        private PalindromeService SetupServiceWithMocksAsync(string dirPath, Dictionary<string, TextFile> files)
        {
            DataMock.Setup(dMock => dMock.GetDirFileNames(dirPath)).Returns(files.Keys);
            foreach (string fileName in files.Keys)
            {
                TextFile file = files[fileName];
                DataMock.Setup(dMock => dMock.GetFileAsync(fileName)).Returns(Task.FromResult(file));
                ValidatorMock.Setup(validatorMock => validatorMock.IsValidAsync(file.Text)).Returns(Task.FromResult(file.IsPalindrome));
            }
            return new PalindromeService(DataMock.Object, ValidatorMock.Object);
        }

        #region CheckFiles 
        [TestMethod]
        public void GoodFiles_TrueFalseTrue()
        {
            // arrange
            string dirPath = "C:/";
            Dictionary<string, TextFile> files = new() {
                { "1.txt", new TextFile( "1.txt", "", true) },
                { "2.txt", new TextFile( "2.txt", "asd", false) },
                { "3.txt", new TextFile( "3.txt", "ads l}llll,s./da", true) } };
            var pService = SetupServiceWithMocks(dirPath, files);

            // act
            var result = pService.CheckFilesForPalindromes(dirPath);

            // assert
            foreach (var r in result)
            {
                Assert.AreEqual(r, files[r.FileName]);
            }
            Assert.AreEqual(result.Count(), files.Count);
        }

        [TestMethod]
        public void NoFiles_Empty()
        {
            // arrange
            string dirPath = "C:/";
            Dictionary<string, TextFile> files = [];
            DataMock.Setup(dMock => dMock.GetDirFileNames(dirPath)).Returns([]);
            ValidatorMock.Reset();
            var pService = new PalindromeService(DataMock.Object, ValidatorMock.Object);

            // act
            var result = pService.CheckFilesForPalindromes(dirPath);

            // assert
            foreach (var r in result)
            {
                Assert.AreEqual(r, files[r.FileName]);
            }
            Assert.AreEqual(result.Count(), files.Count);
        }

        [TestMethod]
        public void DirNotExisits_Empty()
        {
            // arrange
            string dirPath = "C:/";
            DataMock.Setup(dMock => dMock.GetDirFileNames(dirPath)).Returns([]);
            ValidatorMock.Reset();
            var pService = new PalindromeService(DataMock.Object, ValidatorMock.Object);

            // act
            var result = pService.CheckFilesForPalindromes(dirPath);

            // assert
            Assert.AreEqual(result.Count(), 0);
        }
        #endregion

        #region CheckFilesAsync 
        [TestMethod]
        public async Task GoodFiles_TrueFalseTrueAsync()
        {
            // arrange
            string dirPath = "C:/";
            Dictionary<string, TextFile> files = new() {
                { "1.txt", new TextFile( "1.txt", "", true) },
                { "2.txt", new TextFile( "2.txt", "asd", false) },
                { "3.txt", new TextFile( "3.txt", "ads l}llll,s./da", true) } };
            var pService = SetupServiceWithMocksAsync(dirPath, files);

            // act
            var result = pService.CheckFilesForPalindromesAsync(dirPath);

            // assert
            int count = 0;
            await foreach (var r in result)
            {
                count++;
                Assert.AreEqual(r, files[r.FileName]);
            }
            Assert.AreEqual(count, files.Count);
        }

        [TestMethod]
        public async Task NoFiles_EmptyAsync()
        {
            // arrange
            string dirPath = "C:/";
            Dictionary<string, TextFile> files = [];
            DataMock.Setup(dMock => dMock.GetDirFileNames(dirPath)).Returns([]);
            ValidatorMock.Reset();
            var pService = new PalindromeService(DataMock.Object, ValidatorMock.Object);

            // act
            var result = pService.CheckFilesForPalindromesAsync(dirPath);

            // assert
            int count = 0;
            await foreach (var r in result)
            {
                count++;
                Assert.AreEqual(r, files[r.FileName]);
            }
            Assert.AreEqual(count, files.Count);
        }

        [TestMethod]
        public async Task DirNotExisits_EmptyAsync()
        {
            // arrange
            string dirPath = "C:/";
            DataMock.Setup(dMock => dMock.GetDirFileNames(dirPath)).Returns([]);
            ValidatorMock.Reset();
            var pService = new PalindromeService(DataMock.Object, ValidatorMock.Object);

            // act
            var result = pService.CheckFilesForPalindromesAsync(dirPath);

            // assert
            int count = 0;
            await foreach (var r in result)
            {
                count++;
            }
            Assert.AreEqual(count, 0);
        }
        #endregion
    }
}
