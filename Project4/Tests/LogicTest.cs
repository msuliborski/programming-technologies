using Microsoft.VisualStudio.TestTools.UnitTesting;
using Data;
using System.IO;
using System.Linq;
using System;
using System.Collections;
using Services;

namespace Tests {
    [TestClass]
    [DeploymentItem(@"Lib.mdf")]
    public class LogicTest {

        private static Library library;
        private static string connectionString;

        [ClassInitialize]
        public static void ClassInitializeMethod(TestContext context) {
            string DBRelativePath = "Lib.mdf";
            string testingWorkingFolder = Environment.CurrentDirectory;
            string DBPath = Path.Combine(testingWorkingFolder, DBRelativePath);
            FileInfo databaseFile = new FileInfo(DBPath);
            Assert.IsTrue(databaseFile.Exists, $"{Environment.CurrentDirectory}");
            library = new Library();
        }


        [TestMethod]
        public void Test() {

            using (LibDataContext lib = new LibDataContext(connectionString)) {
                Assert.IsNotNull(lib.Connection);
                Assert.AreEqual<int>(2, lib.Readers.Count());
                //IEnumerable filtered = lib.FilterReadersByLastName_ForEach("Reader");           
            }
        }

        [TestMethod]
        public void FillTest() {
            Assert.IsTrue(library.GetAllCatalogs().ToList().Count == 12);
            Assert.IsTrue(library.GetAllReaders().ToList().Count == 3);


            Assert.IsTrue(library.GetCatalog(0).Author.Equals("Shakespeare"));
            Assert.IsTrue(library.GetCatalog(0).Title.Equals("Sonnet 116"));
            Assert.IsTrue(library.GetCatalog(0).Books == 3);
            Assert.IsTrue(library.GetBook(0).IdNumber == 1);


            Assert.IsTrue(library.GetReader(1).FirstName.Equals("John"));
            Assert.IsTrue(library.GetReader(1).LastName.Equals("Kowalsky"));
            Assert.IsTrue(library.GetReader(1).Id == 1);
            Assert.IsTrue(library.GetReader(1).Books == 3);
            Assert.IsTrue(library.GetBook(1).IdNumber == 26);
        }

        [TestMethod]
        public void BookTest() {
            Services.Model.Catalog c = library.GetCatalog(0);

            Assert.IsTrue(library.GetCatalog(0).Books == 3);
            library.AddBook(new Services.Model.Book(0, 100));
            Assert.IsTrue(library.GetCatalog(0).Books == 4);
            library.DeleteBook(100);
            Assert.IsTrue(library.GetCatalog(0).Books == 3);
        }

        //[TestMethod]
        //public void ReaderTest() {

        //    Assert.IsTrue(dataRepository.GetAllReaders().ToList().Count == 3);
        //    dataRepository.AddReader(new Reader(90, "test10", "test20"));
        //    Assert.IsTrue(dataRepository.GetAllReaders().ToList().Count == 4);
        //    dataRepository.AddReader(new Reader(91, "test11", "test21"));
        //    Assert.IsTrue(dataRepository.GetAllReaders().ToList().Count == 5);

        //    Assert.IsTrue(dataRepository.GetReader(91).FirstName.Equals("test11"));
        //    Assert.IsTrue(dataRepository.GetReader(91).LastName.Equals("test21"));
        //    Assert.IsTrue(dataRepository.GetReader(91).Id == 91);

        //    Catalog c = dataRepository.GetCatalog(0);
        //    Assert.IsTrue(dataRepository.GetBook(c).Catalog.Author.Equals("Shakespeare"));
        //    Assert.IsTrue(dataRepository.GetBook(c).Catalog.Title.Equals("Sonnet 116"));
        //    Assert.IsTrue(dataRepository.GetBook(c).Catalog.Books.Count == 3);
        //    Assert.IsTrue(dataRepository.GetBook(c).Catalog.Books[0].IdNumber == 1);


        //    dataRepository.DeleteReader(91);
        //    Assert.IsTrue(dataRepository.GetAllReaders().ToList().Count == 4);


        //    Reader r = new Reader(99, "test99a", "test99b");
        //    dataRepository.UpdateReader(90, r);

        //    Assert.IsTrue(dataRepository.GetReader(99).FirstName.Equals("test99a"));
        //    Assert.IsTrue(dataRepository.GetReader(99).LastName.Equals("test99b"));
        //    Assert.IsTrue(dataRepository.GetReader(99).Id == 99);
        //    Assert.IsTrue(dataRepository.GetAllReaders().ToList().Count == 4);
        //}

        //[TestMethod]
        //public void CatalogTest() {

        //    Assert.IsTrue(dataRepository.GetAllCatalogs().ToList().Count == 12);
        //    dataRepository.AddCatalog(new Catalog("test1", "test2"));
        //    Assert.IsTrue(dataRepository.GetAllCatalogs().ToList().Count == 13);

        //    Assert.IsTrue(dataRepository.GetCatalog(12).Author.Equals("test1"));
        //    Assert.IsTrue(dataRepository.GetCatalog(12).Title.Equals("test2"));

        //    dataRepository.DeleteCatalog(1);
        //    Assert.IsTrue(dataRepository.GetAllCatalogs().ToList().Count == 12);

        //}

    }
}
