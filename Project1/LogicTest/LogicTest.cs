using Data;
using Logic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LogicTest {
    [TestClass]
    public class LogicTest {

        private DataRepository dataRepository;
        private Library library;

        [TestInitialize]
        public void Fill() {

            dataRepository = new DataRepository();

            Catalog c1 = new Catalog("Shakespeare", "Sonnet 116");
            c1.Books.Add(new Book(c1, 1));
            c1.Books.Add(new Book(c1, 2));
            c1.Books.Add(new Book(c1, 3));
            dataRepository.AddCatalog(c1);

            Catalog c2 = new Catalog("Shakespeare", "Sonnet 130");
            c2.Books.Add(new Book(c2, 4));
            dataRepository.AddCatalog(c2);

            Catalog c3 = new Catalog("Hemingway", "The Old Man and the Sea");
            c3.Books.Add(new Book(c3, 5));
            c3.Books.Add(new Book(c3, 6));
            dataRepository.AddCatalog(c3);

            Catalog c4 = new Catalog("Hemingway", "The Sun Also Rises");
            c4.Books.Add(new Book(c4, 7));
            c4.Books.Add(new Book(c4, 8));
            dataRepository.AddCatalog(c4);

            Catalog c5 = new Catalog("King", "It");
            c5.Books.Add(new Book(c5, 9));
            c5.Books.Add(new Book(c5, 10));
            dataRepository.AddCatalog(c5);

            Catalog c6 = new Catalog("King", "The Shining");
            c6.Books.Add(new Book(c6, 11));
            dataRepository.AddCatalog(c6);

            Catalog c7 = new Catalog("J. K. Rowling", "Harry Potter and the Deathly Hallows");
            c7.Books.Add(new Book(c7, 12));
            dataRepository.AddCatalog(c7);

            Catalog c8 = new Catalog("J. K. Rowling", "Harry Potter and the Goblet of Fire");
            c8.Books.Add(new Book(c8, 13));
            c8.Books.Add(new Book(c8, 14));
            dataRepository.AddCatalog(c8);

            Catalog c9 = new Catalog("Twain", "Adventures of Huckleberry Finn");
            c9.Books.Add(new Book(c9, 15));
            c9.Books.Add(new Book(c9, 16));
            dataRepository.AddCatalog(c9);

            Catalog c10 = new Catalog("Twain", "Adventures of Tom Sawyer");
            c10.Books.Add(new Book(c10, 17));
            c10.Books.Add(new Book(c10, 18));
            c10.Books.Add(new Book(c10, 19));
            dataRepository.AddCatalog(c10);

            Catalog c11 = new Catalog("Author1", "Book1");
            c11.Books.Add(new Book(c11, 20));
            c11.Books.Add(new Book(c11, 21));
            c11.Books.Add(new Book(c11, 22));
            dataRepository.AddCatalog(c11);

            Catalog c12 = new Catalog("Author2", "Book2");
            c12.Books.Add(new Book(c12, 23));
            c12.Books.Add(new Book(c12, 24));
            c12.Books.Add(new Book(c12, 25));
            dataRepository.AddCatalog(c12);


            Reader r1 = new Reader(1, "John", "Kowalsky");
            r1.Books.Add(new Book(c5, 26));
            r1.Books.Add(new Book(c9, 27));
            r1.Books.Add(new Book(c10, 28));
            dataRepository.AddReader(r1);

            Reader r2 = new Reader(2, "Adam", "Nowak");
            r2.Books.Add(new Book(c2, 29));
            r2.Books.Add(new Book(c9, 30));
            dataRepository.AddReader(r2);

            Reader r3 = new Reader(3, "Reader", "Reader");
            r3.Books.Add(new Book(c11, 31));
            r3.Books.Add(new Book(c12, 31));
            dataRepository.AddReader(r3);

            library = new Library(dataRepository);
        }


        [TestMethod]
        public void FillTest() {
            Assert.IsTrue(library.GetAllCatalogs().ToList().Count == 12);
            Assert.IsTrue(library.GetAllReaders().ToList().Count == 3);
            Assert.IsTrue(library.GetAllEvents().ToList().Count == 0);


            Assert.IsTrue(library.GetCatalog("Shakespeare", "Sonnet 116").Author.Equals("Shakespeare"));
            Assert.IsTrue(library.GetCatalog("Shakespeare", "Sonnet 116").Title.Equals("Sonnet 116"));
            Assert.IsTrue(library.GetCatalog("Shakespeare", "Sonnet 116").Books.Count == 3);
            Assert.IsTrue(library.GetCatalog("Shakespeare", "Sonnet 116").Books[0].IdNumber == 1);


            Assert.IsTrue(library.GetReader(1).FirstName.Equals("John"));
            Assert.IsTrue(library.GetReader(1).LastName.Equals("Kowalsky"));
            Assert.IsTrue(library.GetReader(1).Id == 1);
            Assert.IsTrue(library.GetReader(1).Books.Count == 3);
            Assert.IsTrue(library.GetReader(1).Books[0].IdNumber == 26);
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
            Catalog catalog = library.GetCatalog("Twain", "Adventures of Huckleberry Finn");
            int count = catalog.Books.Count;
            Book book = library.RentBook("Twain", "Adventures of Huckleberry Finn");
            Assert.AreEqual(catalog.Books.Count, count - 1);
            library.ReturnBook(book);
            Assert.AreEqual(catalog.Books.Count, count);
            library.DeleteBook(book);
            Assert.AreEqual(catalog.Books.Count, count - 1);
            library.AddBook(book);
            Assert.AreEqual(library.GetEventsForBook(book).ToList().Count, 4);
        }


        [TestMethod]
        public void EventTest() {
            Assert.IsTrue(library.GetAllEvents().ToList().Count == 0);

            library.AddReader(new Reader(99, "test1", "test1"));
            Assert.IsTrue(library.GetAllEvents().ToList().Count == 1);

            library.DeleteReader(99);
            Assert.IsTrue(library.GetAllEvents().ToList().Count == 2);

            library.AddCatalog(new Catalog("test", "test"));
            Assert.IsTrue(library.GetAllEvents().ToList().Count == 3);

            library.DeleteCatalog(1);
            Assert.IsTrue(library.GetAllEvents().ToList().Count == 4);

            library.RentBook("Shakespeare", "Sonnet 116");
            Assert.IsTrue(library.GetAllEvents().ToList().Count == 5);

            library.ReturnBook(new Book(new Catalog("test", "test"), 99));
            Assert.IsTrue(library.GetAllEvents().ToList().Count == 6);

            library.DeleteBook(new Book(new Catalog("test", "test"), 99));
            Assert.IsTrue(library.GetAllEvents().ToList().Count == 7);
        }

    }
}
