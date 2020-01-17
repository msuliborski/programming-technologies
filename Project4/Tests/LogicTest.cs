using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System;
using Services;

namespace Tests {
    [TestClass]
    [DeploymentItem(@"Lib.mdf")]
    public class LogicTest {

        private static Library library;
        

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
        public void BookTest() {
            Services.Model.Catalog c = library.GetCatalog(1);
            int count = library.GetCatalog(1).Books;
            library.AddBook(new Services.Model.Book(1, 1, 999999));
            Assert.IsTrue(library.GetCatalog(1).Books == count + 1);
            library.DeleteMaxBook();
            Assert.IsTrue(library.GetCatalog(1).Books == count);
        }

        [TestMethod]
        public void ReaderTest() {
            Services.Model.Reader reader = library.GetReader(1);
            string oldName = reader.FirstName;
            reader.FirstName = "A";
            library.UpdateReader(reader);
            Assert.IsTrue(library.GetReader(1).FirstName.Equals(reader.FirstName));
            reader.FirstName = oldName;
            library.UpdateReader(reader);
            Assert.IsTrue(library.GetReader(1).FirstName.Equals(oldName));
    }

        [TestMethod]
        public void CatalogTest() {

            Services.Model.Catalog catalog = library.GetCatalog(1);
            string oldTitle = catalog.Title;
            catalog.Title = "A";
            library.UpdateCatalog(catalog);
            Assert.IsTrue(library.GetCatalog(1).Title.Equals(catalog.Title));
            catalog.Title = oldTitle;
            library.UpdateCatalog(catalog);
            Assert.IsTrue(library.GetCatalog(1).Title.Equals(oldTitle));
        } 
        
        [TestMethod]
        public void RentAndReturnTest() {
            int count = library.GetReader(1).Books;
            Assert.IsTrue(library.GetReader(1).Books == count);
            library.RentBook("King", "It", 1);
            Assert.IsTrue(library.GetReader(1).Books == count + 1);
            library.ReturnBook("King", "It", 1);
            Assert.IsTrue(library.GetReader(1).Books == count);
        }
    }
}
