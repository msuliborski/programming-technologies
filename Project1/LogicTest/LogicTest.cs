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
        }

        [TestMethod]
        public void CatalogTest() {
        }

        [TestMethod]
        public void ReaderTest() {
        }

        
        [TestMethod]
        public void BookTest() {
            //int count = library.ge
            Book book = library.RentBook("Twain", "Adventures of Huckleberry Finn");
        }

        [TestMethod]
        public void EventsTest() {
        }


    }
}
