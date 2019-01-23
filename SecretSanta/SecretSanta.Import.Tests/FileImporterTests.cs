using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecretSanta.Domain.Models;

namespace SecretSanta.Import.Tests
{
    [TestClass]
    public class FileImporterTests
    {
        //For the purpose of this testing class, let abbreviations FNF and LNF be
        //"FirstName First" and "LastName First" accordingly.

        [TestInitialize]
        public void InitializeTests()
        {
            DeleteFiles();
            Initialize_ValidTestFile1_RayFNF();
            Initialize_ValidTestFile2_MarkFNF();
            Initialize_ValidTestFile3_MikeLNF();
            Initialize_InvalidTestFile1_GarbageHeader();
        }

        private void Initialize_ValidTestFile1_RayFNF()
        {
            string path = System.Environment.CurrentDirectory;
            path += @"\ValidFile1_RayFNF.txt";

            if (!File.Exists(path))
            {
                using (StreamWriter sw = File.CreateText(path))
                {
                    sw.WriteLine("Name: Raymond Shiner");
                    sw.WriteLine("PS4");
                    sw.WriteLine("Money");
                    sw.WriteLine("Peace on Earth");
                }
            }
        }
        
        private void Initialize_ValidTestFile2_MarkFNF()
        {
            string path = System.Environment.CurrentDirectory;
            path += @"\ValidFile2_MarkFNF.txt";

            if (!File.Exists(path))
            {
                using (StreamWriter sw = File.CreateText(path))
                {
                    sw.WriteLine("Name: Mark Michaelis");
                    sw.WriteLine("Fame");
                    sw.WriteLine("Fortune");
                    sw.WriteLine("Abs");
                }
            }
        }

        private void Initialize_ValidTestFile3_MikeLNF()
        {
            string path = System.Environment.CurrentDirectory;
            path += @"\ValidFile3_MikeLNF.txt";

            if (!File.Exists(path))
            {
                using (StreamWriter sw = File.CreateText(path))
                {
                    sw.WriteLine("Name: Stokes, Mike");
                    sw.WriteLine("Money");
                    sw.WriteLine("More Money");
                    sw.WriteLine("Pizza");
                }
            }
        }

        private void Initialize_InvalidTestFile1_GarbageHeader()
        {
            string path = System.Environment.CurrentDirectory;
            path += @"\InvalidFile1_GarbageHeader.txt";

            if (!File.Exists(path))
            {
                using (StreamWriter sw = File.CreateText(path))
                {
                    sw.WriteLine("GARBAGE HEADER");
                    sw.WriteLine("Garbage Content");
                    sw.WriteLine("Garbage Content");
                    sw.WriteLine("Garbage Content");
                }
            }
        }

        private void DeleteFiles()
        {
            string path = System.Environment.CurrentDirectory;
            File.Delete(path + @"\ValidFile1_RayFNF.txt");
            File.Delete(path + @"\ValidFile2_MarkFNF.txt");
            File.Delete(path + @"\ValidFile3_MikeLNF.txt");
            File.Delete(path + @"\InvalidFile1_GarbageHeader.txt");
        }

        [TestCleanup]
        public void CleanUpTests()
        {
            DeleteFiles();
        }

        

        // ==============START OF TESTS==================

        [DataTestMethod]
        [DataRow("ValidFile1_RayFNF.txt", "Raymond", "Shiner", "PS4", "Money", "Peace on Earth")]
        [DataRow("ValidFile2_MarkFNF.txt", "Mark", "Michaelis", "Fame", "Fortune", "Abs")]
        [DataRow("ValidFile3_MikeLNF.txt", "Mike", "Stokes", "Money", "More Money", "Pizza")]
        public void ReadInUser_ValidTestFiles_ReturnsValidUserObject(string filename, string firstname, string lastname, string gift1, string gift2, string gift3)
        {
            (User user, List<Gift> gifts) = FileImporter.ReadInUser(filename);

            Assert.AreEqual(firstname, user.FirstName);
            Assert.AreEqual(lastname, user.LastName);

            Assert.AreEqual(gift1, gifts[0].Title);
            Assert.AreEqual(1, gifts[0].Importance);

            Assert.AreEqual(gift2, gifts[1].Title);
            Assert.AreEqual(2, gifts[1].Importance);

            Assert.AreEqual(gift3, gifts[2].Title);
            Assert.AreEqual(3, gifts[2].Importance);
        }

        [DataTestMethod]
        [DataRow("InvalidFile1_GarbageHeader.txt")]
        [DataRow("NonExistentFile.txt")]
        public void ReadInUser_InvalidOrNonExistentTestFiles_ReturnsNullTuple(string filename)
        {
            (User user, List<Gift> gifts) = FileImporter.ReadInUser(filename);

            Assert.IsNull(user);
            Assert.IsNull(gifts);
        }

        [DataTestMethod]
        [DataRow("Name: Raymond Shiner")]
        [DataRow("Name: Shiner, Raymond")]
        [DataRow("Name: Mike Stokes    ")]
        public void FileHeaderIsFormattedCorrectly_CorrectFormat_ReturnsTrue(string header)
        {
            bool res = FileImporter.FileHeaderIsFormattedCorrectly(header);
            Assert.IsTrue(res);
        }

        [DataTestMethod]
        [DataRow(null)]
        [DataRow("LEEROOOOOY JENKINS")]
        [DataRow("Name: Shiner, Ray, Something")]
        [DataRow("Name: Raymond Lawrence Shiner")]
        [DataRow("Name: ")]
        public void FileHeaderIsFormattedCorrectly_InvalidHeaders_ReturnsFalse(string header)
        {
            bool res = FileImporter.FileHeaderIsFormattedCorrectly(header);
            Assert.IsFalse(res);
        }

        [DataTestMethod] // Header, expectedFirstName, expectedLastName
        [DataRow("Name: Raymond Shiner", "Raymond", "Shiner")]
        [DataRow("Name: Mike Stokes", "Mike", "Stokes")]
        [DataRow("Name: Michaelis, Mark", "Mark", "Michaelis")]
        [DataRow("Name: Michaelis, Mark    ", "Mark", "Michaelis")]
        public void ExtractNamesFromHeader_ValidHeaders_CorrectOutput(string header, string expectedFirstName, string expectedLastName)
        {
            (string firstName, string lastName) = FileImporter.ExtractNamesFromHeader(header);

            Assert.AreEqual(expectedFirstName, firstName);
            Assert.AreEqual(expectedLastName, lastName);
        }

        [DataTestMethod]
        [DataRow("GARBAGE HEADER")]
        [DataRow("Name: something, else,")]
        public void ExtractNamesFromHeader_InvalidHeaders_ReturnsNullTuple(string header)
        {
            (string firstName, string lastName) = FileImporter.ExtractNamesFromHeader(header);

            Assert.IsNull(firstName);
            Assert.IsNull(lastName);
        }
    }
}
