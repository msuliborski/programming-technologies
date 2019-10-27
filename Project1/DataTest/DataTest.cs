using Microsoft.VisualStudio.TestTools.UnitTesting;
using Data;
using System.Linq;

namespace DataTest {
    [TestClass]
    public class DataTest {


        private DataRepository dataRepository;

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
        public void BookHandleTest() {

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