using Microsoft.VisualStudio.TestTools.UnitTesting;
using Data;
using System.Data.Linq.Mapping;
using System.IO;
using System.Linq;

namespace Tests {
    [TestClass]
    [DeploymentItem(@"Lib.mdf")]
    public class DataTest {
        private DataRepository dataRepository;


        [ClassInitialize]
        public static void ClassInitializeMethod(TestContext context) {
            string DBRelativePath = @"Lib.mdf";
            string testingWorkingFolder = Environment.CurrentDirectory;
            string DBPath = Path.Combine(testingWorkingFolder, DBRelativePath);
            FileInfo databaseFile = new FileInfo(DBPath);
            Assert.IsTrue(databaseFile.Exists, $"{Environment.CurrentDirectory}");
            connectionString = $@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={_DBPath};Integrated Security = True; Connect Timeout = 30;";
        }



        [TestInitialize]
        public void Fill() {
            dataRepository = new DataRepository(new StaticFiller());
        }

        [TestMethod]
        public void FillTest() {
            Assert.IsTrue(dataRepository.GetAllCatalogs().ToList().Count == 12);
            Assert.IsTrue(dataRepository.GetAllReaders().ToList().Count == 3);
            Assert.IsTrue(dataRepository.GetAllEvents().ToList().Count == 0);


            Assert.IsTrue(dataRepository.GetCatalog(0).Author.Equals("Shakespeare"));
            Assert.IsTrue(dataRepository.GetCatalog(0).Title.Equals("Sonnet 116"));
            Assert.IsTrue(dataRepository.GetCatalog(0).Books.Count == 3);
            Assert.IsTrue(dataRepository.GetCatalog(0).Books[0].IdNumber == 1);


            Assert.IsTrue(dataRepository.GetReader(1).FirstName.Equals("John"));
            Assert.IsTrue(dataRepository.GetReader(1).LastName.Equals("Kowalsky"));
            Assert.IsTrue(dataRepository.GetReader(1).Id == 1);
            Assert.IsTrue(dataRepository.GetReader(1).Books.Count == 3);
            Assert.IsTrue(dataRepository.GetReader(1).Books[0].IdNumber == 26);
        }

        [TestMethod]
        public void BookTest() {
            Catalog c = dataRepository.GetCatalog(0);

            Assert.IsTrue(dataRepository.GetBook(c).Catalog.Author.Equals("Shakespeare"));
            Assert.IsTrue(dataRepository.GetBook(c).Catalog.Title.Equals("Sonnet 116"));
            Assert.IsTrue(dataRepository.GetBook(c).Catalog.Books.Count == 3);
            Assert.IsTrue(dataRepository.GetBook(c).Catalog.Books[0].IdNumber == 1);
        }

        [TestMethod]
        public void ReaderTest() {

            Assert.IsTrue(dataRepository.GetAllReaders().ToList().Count == 3);
            dataRepository.AddReader(new Reader(90, "test10", "test20"));
            Assert.IsTrue(dataRepository.GetAllReaders().ToList().Count == 4);
            dataRepository.AddReader(new Reader(91, "test11", "test21"));
            Assert.IsTrue(dataRepository.GetAllReaders().ToList().Count == 5);

            Assert.IsTrue(dataRepository.GetReader(91).FirstName.Equals("test11"));
            Assert.IsTrue(dataRepository.GetReader(91).LastName.Equals("test21"));
            Assert.IsTrue(dataRepository.GetReader(91).Id == 91);

            Catalog c = dataRepository.GetCatalog(0);
            Assert.IsTrue(dataRepository.GetBook(c).Catalog.Author.Equals("Shakespeare"));
            Assert.IsTrue(dataRepository.GetBook(c).Catalog.Title.Equals("Sonnet 116"));
            Assert.IsTrue(dataRepository.GetBook(c).Catalog.Books.Count == 3);
            Assert.IsTrue(dataRepository.GetBook(c).Catalog.Books[0].IdNumber == 1);


            dataRepository.DeleteReader(91);
            Assert.IsTrue(dataRepository.GetAllReaders().ToList().Count == 4);


            Reader r = new Reader(99, "test99a", "test99b");
            dataRepository.UpdateReader(90, r);

            Assert.IsTrue(dataRepository.GetReader(99).FirstName.Equals("test99a"));
            Assert.IsTrue(dataRepository.GetReader(99).LastName.Equals("test99b"));
            Assert.IsTrue(dataRepository.GetReader(99).Id == 99);
            Assert.IsTrue(dataRepository.GetAllReaders().ToList().Count == 4);
        }

        [TestMethod]
        public void CatalogTest() {

            Assert.IsTrue(dataRepository.GetAllCatalogs().ToList().Count == 12);
            dataRepository.AddCatalog(new Catalog("test1", "test2"));
            Assert.IsTrue(dataRepository.GetAllCatalogs().ToList().Count == 13);

            Assert.IsTrue(dataRepository.GetCatalog(12).Author.Equals("test1"));
            Assert.IsTrue(dataRepository.GetCatalog(12).Title.Equals("test2"));

            dataRepository.DeleteCatalog(1);
            Assert.IsTrue(dataRepository.GetAllCatalogs().ToList().Count == 12);

        }
    }
}