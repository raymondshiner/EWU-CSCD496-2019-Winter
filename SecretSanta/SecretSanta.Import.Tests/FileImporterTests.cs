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

        [TestCleanup]
        public void CleanUpTests()
        {
            string path = System.Environment.CurrentDirectory;
            File.Delete(path + @"\ValidFile1_RayFNF.txt");
            File.Delete(path + @"\ValidFile2_MarkFNF.txt");
            File.Delete(path + @"\ValidFile3_MikeLNF.txt");
            File.Delete(path + @"\InvalidFile1_GarbageHeader.txt");
        }

        // ==============START OF TESTS==================

        [TestMethod]
        public void ReadInUser_ValidTestFile1_ReturnsValidUserObject()
        {
            (User user, List<Gift> gifts) = FileImporter.ReadInUser("ValidFile1_RayFNF.txt");

            Assert.AreEqual("Raymond", user.FirstName);
            Assert.AreEqual("Shiner", user.LastName);

            Assert.AreEqual("PS4", gifts[0].Title);
            Assert.AreEqual(1, gifts[0].Importance);
            Assert.AreEqual("Money", gifts[1].Title);
            Assert.AreEqual(2, gifts[1].Importance);
            Assert.AreEqual("Peace on Earth", gifts[2].Title);
            Assert.AreEqual(3, gifts[2].Importance);
        }

        [TestMethod]
        public void ReadInUser_ValidTestFile2_ReturnsValidUserObject()
        {
            (User user, List<Gift> gifts)  = FileImporter.ReadInUser("ValidFile2_MarkFNF.txt");

            Assert.AreEqual("Mark", user.FirstName);
            Assert.AreEqual("Michaelis", user.LastName);

            Assert.AreEqual("Fame", gifts[0].Title);
            Assert.AreEqual(1, gifts[0].Importance);
            Assert.AreEqual("Fortune", gifts[1].Title);
            Assert.AreEqual(2, gifts[1].Importance);
            Assert.AreEqual("Abs", gifts[2].Title);
            Assert.AreEqual(3, gifts[2].Importance);
        }

        [TestMethod]
        public void ReadInUser_ValidTestFile3_ReturnsValidUserObject()
        {
            (User user, List<Gift> gifts) = FileImporter.ReadInUser("ValidFile3_MikeLNF.txt");

            Assert.AreEqual("Mike", user.FirstName);
            Assert.AreEqual("Stokes", user.LastName);

            Assert.AreEqual("Money", gifts[0].Title);
            Assert.AreEqual(1, gifts[0].Importance);
            Assert.AreEqual("More Money", gifts[1].Title);
            Assert.AreEqual(2, gifts[1].Importance);
            Assert.AreEqual("Pizza", gifts[2].Title);
            Assert.AreEqual(3, gifts[2].Importance);
        }

        [TestMethod]
        public void ReadInUser_InvalidTestFile1_ReturnsNullTuple()
        {
            (User user, List<Gift> gifts) = FileImporter.ReadInUser("InvalidFile1_GarbageHeader.txt");

            Assert.IsNull(user);
            Assert.IsNull(gifts);
        }

        [TestMethod]
        public void ReadInUser_NonExistentTestFile_ReturnsNullTuple()
        {
            (User user, List<Gift> gifts) = FileImporter.ReadInUser("NonExistentFile.txt");

            Assert.IsNull(user);
            Assert.IsNull(gifts);
        }

        [TestMethod]
        public void FileHeaderIsFormattedCorrectly_CorrectFormat_ReturnsTrue()
        {
            string header = "Name: Raymond Shiner";

            bool res = FileImporter.FileHeaderIsFormattedCorrectly(header);
            Assert.IsTrue(res);
        }

        [TestMethod]
        public void FileHeaderIsFormattedCorrectly_CorrectFormatExtraWhiteSpaceOnEitherEnd_ReturnsTrue()
        {
            string header = "Name: Raymond Shiner    ";

            bool res = FileImporter.FileHeaderIsFormattedCorrectly(header);
            Assert.IsTrue(res);
        }

        [TestMethod]
        public void FileHeaderIsFormattedCorrectly_NullEntry_ReturnsFalse()
        {
            bool res = FileImporter.FileHeaderIsFormattedCorrectly(null);
            Assert.IsFalse(res);
        }

        [TestMethod]
        public void FileHeaderIsFormattedCorrectly_GarbageHeaderLine_ReturnsFalse()
        {
            string header = "LEEROOOOOY JENKINS";
            bool res = FileImporter.FileHeaderIsFormattedCorrectly(header);
            Assert.IsFalse(res);
        }

        [TestMethod]
        public void FileHeaderIsFormattedCorrectly_2CommasInHeader_ReturnsFalse()
        {
            string header = "Name: Shiner, Ray, Something";

            bool res = FileImporter.FileHeaderIsFormattedCorrectly(header);
            Assert.IsFalse(res);
        }

        [TestMethod]
        public void FileHeaderIsFormattedCorrectly_3NamesInHeader_ReturnsFalse()
        {
            string header = "Name: Raymond Lawrence Shiner";

            bool res = FileImporter.FileHeaderIsFormattedCorrectly(header);
            Assert.IsFalse(res);
        }

        [TestMethod]
        public void FileHeaderIsFormattedCorrectly_HeaderFirstPartCorrectNoName_ReturnsFalse()
        {
            string header = "Name: ";

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
