using ClientSide.Data.FileStorage;
using ClientSide.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientSideTests.UnitTests.Data
{
    [TestClass]
    public class FileStorageTests
    {
        private readonly FileStorage storage = new();

        #region GetFileNames

        [TestMethod]
        public void GetFileNames_OK_Array()
        {
            // arrange
            string dir = Environment.CurrentDirectory + "\\testinginput";
            Directory.CreateDirectory(dir);
            List<string> filenames = [];
            for (int i = 0; i < 10; i++)
            {
                string name = $"{dir}\\{i}.txt";
                filenames.Add(name);
                File.Create(name).Close();
            }

            // act
            var result = storage.GetDirFileNames(dir);

            // assert
            foreach (var r in result)
            {
                Assert.IsTrue(filenames.Contains(r));
            }
            Assert.AreEqual(result.Count(), filenames.Count);

            // cleaning
            foreach (var f in filenames)
            {
                File.Delete(f);
            }
            Directory.Delete(dir);
        }

        [TestMethod]
        public void GetFileNames_EmptyDir_Empty()
        {
            // arrange
            string dir = Environment.CurrentDirectory + "\\testinginput";
            Directory.CreateDirectory(dir);

            // act
            var result = storage.GetDirFileNames(dir);

            // assert
            Assert.AreEqual(result.Count(), 0);

            // cleaning
            Directory.Delete(dir);
        }

        [TestMethod]
        public void GetFileNames_WrongDir_Empty()
        {
            // arrange
            string dir = Environment.CurrentDirectory + new Random().Next();

            // act
            var result = storage.GetDirFileNames(dir);

            // assert
            Assert.AreEqual(result.Count(), 0);
        }

        #endregion

        #region GetFileNamesAsync
        [TestMethod]
        public async Task GetFileNames_OK_ArrayAsync()
        {
            // arrange
            string dir = Environment.CurrentDirectory + "\\testinginput";
            Directory.CreateDirectory(dir);
            List<string> filenames = [];
            for (int i = 0; i < 10; i++)
            {
                string name = $"{dir}\\{i}.txt";
                filenames.Add(name);
                File.Create(name).Close();
            }

            // act
            var result = storage.GetDirFileNamesAsync(dir);

            // assert
            int count = 0;
            await foreach (var r in result)
            {
                count++;
                Assert.IsTrue(filenames.Contains(r));
            }
            Assert.AreEqual(count, filenames.Count);

            // cleaning
            foreach (var f in filenames)
            {
                File.Delete(f);
            }
            Directory.Delete(dir);
        }

        [TestMethod]
        public async Task GetFileNames_EmptyDir_EmptyAsync()
        {
            // arrange
            string dir = Environment.CurrentDirectory + "\\testinginput";
            Directory.CreateDirectory(dir);

            // act
            var result = storage.GetDirFileNamesAsync(dir);

            // assert
            int count = 0;
            await foreach (var r in result)
            {
                count++;
            }
            Assert.AreEqual(count, 0);

            // cleaning
            Directory.Delete(dir);
        }

        [TestMethod]
        public async Task GetFileNames_WrongDir_EmptyAsync()
        {
            // arrange
            string dir = Environment.CurrentDirectory + new Random().Next();

            // act
            var result = storage.GetDirFileNamesAsync(dir);

            // assert
            int count = 0;
            await foreach (var r in result)
            {
                count++;
            }
            Assert.AreEqual(count, 0);
        }
        #endregion

        #region GetFile

        [TestMethod]
        public void GetFile_OK_File()
        {
            // arrange
            string dir = Environment.CurrentDirectory + "\\testinginput";
            Directory.CreateDirectory(dir);
            TextFile file = new ($"{dir}\\test123.txt", "12345");
            using (var f = File.Create(file.FileName))
            {
                f.Write(Encoding.ASCII.GetBytes(file.Text));
            };

            // act
            var result = storage.GetFile(file.FileName);

            // assert
            Assert.AreEqual(result, file);

            // cleaning
            File.Delete(file.FileName);
            Directory.Delete(dir);
        }
        [TestMethod]
        public void GetFile_NoFile_Empty()
        {
            // arrange
            string dir = Environment.CurrentDirectory + "\\testinginput";
            Directory.CreateDirectory(dir);
            TextFile file = new ($"{dir}\\test123.txt", "12345");

            // act
            var result = storage.GetFile(file.FileName);

            // assert
            Assert.IsNull(result);

            // cleaning
            Directory.Delete(dir);
        }
        #endregion

        #region GetFileAsync

        [TestMethod]
        public async Task GetFile_OK_FileAsync()
        {
            // arrange
            string dir = Environment.CurrentDirectory + "\\testinginput";
            Directory.CreateDirectory(dir);
            TextFile file = new($"{dir}\\test123.txt", "12345");
            using (var f = File.Create(file.FileName))
            {
                f.Write(Encoding.ASCII.GetBytes(file.Text));
            };

            // act
            var result = await storage.GetFileAsync(file.FileName);

            // assert
            Assert.AreEqual(result, file);

            // cleaning
            File.Delete(file.FileName);
            Directory.Delete(dir);
        }
        [TestMethod]
        public async Task GetFile_NoFile_EmptyAsync()
        {
            // arrange
            string dir = Environment.CurrentDirectory + "\\testinginput";
            Directory.CreateDirectory(dir);
            TextFile file = new($"{dir}\\test123.txt", "12345");

            // act
            var result = await storage.GetFileAsync(file.FileName);

            // assert
            Assert.IsNull(result);

            // cleaning
            Directory.Delete(dir);
        }
        #endregion
    }
}
