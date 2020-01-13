using Data;
using Logic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests {
    [TestClass]
    public class LogicTest {

        private DataRepository dataRepository;
        private Library library;

        [TestInitialize]
        public void Fill() {
            dataRepository = new DataRepository(new DynamicFiller());
            library = new Library(dataRepository);
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
        public void CatalogTest() {
            Assert.IsTrue(dataRepository.GetAllCatalogs().ToList().Count == 12);
            dataRepository.AddCatalog(new Catalog("test1", "test2"));
            Assert.IsTrue(dataRepository.GetAllCatalogs().ToList().Count == 13);

            Assert.IsTrue(dataRepository.GetCatalog(12).Author.Equals("test1"));
            Assert.IsTrue(dataRepository.GetCatalog(12).Title.Equals("test2"));

            dataRepository.DeleteCatalog(1);
            Assert.IsTrue(dataRepository.GetAllCatalogs().ToList().Count == 12);
        }

        [TestMethod]
        public void ReaderTest() {
            Assert.IsTrue(library.GetAllReaders().ToList().Count == 3);
            library.AddReader(new Reader(90, "test10", "test20"));
            Assert.IsTrue(library.GetAllReaders().ToList().Count == 4);
            library.AddReader(new Reader(91, "test11", "test21"));
            Assert.IsTrue(library.GetAllReaders().ToList().Count == 5);

            Assert.IsTrue(library.GetReader(91).FirstName.Equals("test11"));
            Assert.IsTrue(library.GetReader(91).LastName.Equals("test21"));
            Assert.IsTrue(library.GetReader(91).Id == 91);

            Catalog c = library.GetCatalog("Shakespeare", "Sonnet 116");
            Assert.IsTrue(library.GetBook("Shakespeare", "Sonnet 116").Catalog.Author.Equals("Shakespeare"));
            Assert.IsTrue(library.GetBook("Shakespeare", "Sonnet 116").Catalog.Title.Equals("Sonnet 116"));
            Assert.IsTrue(library.GetBook("Shakespeare", "Sonnet 116").Catalog.Books.Count == 3);
            Assert.IsTrue(library.GetBook("Shakespeare", "Sonnet 116").Catalog.Books[0].IdNumber == 1);


            library.DeleteReader(91);
            Assert.IsTrue(library.GetAllReaders().ToList().Count == 4);

            Reader r = new Reader(99, "test99a", "test99b");
            library.UpdateReader(90, r);

            Assert.IsTrue(library.GetReader(99).FirstName.Equals("test99a"));
            Assert.IsTrue(library.GetReader(99).LastName.Equals("test99b"));
            Assert.IsTrue(library.GetReader(99).Id == 99);
            Assert.IsTrue(library.GetAllReaders().ToList().Count == 4);
        }

        [TestMethod]
        public void BookTest() {
            Reader reader = library.GetReader(3);
            Catalog catalog = library.GetCatalog("Twain", "Adventures of Huckleberry Finn");
            int catalogCount = catalog.Books.Count;
            int readerCount = reader.Books.Count;
            Book book = library.RentBook("Twain", "Adventures of Huckleberry Finn", reader);
            Assert.AreEqual(reader.Books.Count, readerCount + 1);
            Assert.AreEqual(catalog.Books.Count, catalogCount - 1);
            library.ReturnBook(book, reader);
            Assert.AreEqual(catalog.Books.Count, catalogCount);
            Assert.AreEqual(reader.Books.Count, readerCount);
            library.DeleteBook(book);
            Assert.AreEqual(catalog.Books.Count, catalogCount - 1);
            library.AddBook(book);
            Assert.AreEqual(library.GetEventsForBook(book).ToList().Count, 4);
        }


        [TestMethod]
        public void EventTest() {
            Assert.AreEqual(library.GetAllEvents().ToList().Count, 0);

            library.AddReader(new Reader(99, "test1", "test1"));
            Assert.AreEqual(library.GetAllEvents().ToList().Count, 1);

            library.DeleteReader(99);
            Assert.AreEqual(library.GetAllEvents().ToList().Count, 2);

            library.AddCatalog(new Catalog("test", "test"));
            Assert.AreEqual(library.GetAllEvents().ToList().Count, 3);

            library.DeleteCatalog(1);
            Assert.AreEqual(library.GetAllEvents().ToList().Count, 4);

            Reader reader = library.GetReader(2);
            Book book = library.GetBook("Shakespeare", "Sonnet 116");
            library.RentBook("Shakespeare", "Sonnet 116", reader);
            Assert.AreEqual(library.GetAllEvents().ToList().Count, 5);

            library.ReturnBook(book, reader);
            Assert.AreEqual(library.GetAllEvents().ToList().Count, 6);

            library.DeleteReader(2);

            Assert.AreEqual(library.GetEventsForReader(reader).ToList().Count, 3);

            library.DeleteBook(new Book(new Catalog("test", "test"), 99));
            Assert.AreEqual(library.GetAllEvents().ToList().Count, 8);
        }

    }
}
