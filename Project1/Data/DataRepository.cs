using System;
using System.Collections.Generic;
using System.Text;

namespace Data {
    public class DataRepository {
        public class DataContext {
            public List<Reader> Readers { get; set; }
            public List<Catalog> Catalogs { get; set; }
            public List<IEvent> Events { get; set; }

            #region fill data
            public void Fill(DataRepository dr) {

                Catalog c1 = new Catalog("Shakespeare", "Sonnet 116");
                c1.Books.Add(new Book(c1, 1));
                c1.Books.Add(new Book(c1, 2));
                c1.Books.Add(new Book(c1, 3));

                Catalog c2 = new Catalog("Shakespeare", "Sonnet 130");
                c2.Books.Add(new Book(c2, 4));

                Catalog c3 = new Catalog("Hemingway", "The Old Man and the Sea");
                c3.Books.Add(new Book(c3, 5));
                c3.Books.Add(new Book(c3, 6));

                Catalog c4 = new Catalog("Hemingway", "The Sun Also Rises");
                c4.Books.Add(new Book(c4, 7));
                c4.Books.Add(new Book(c4, 8));

                Catalog c5 = new Catalog("King", "It");
                c5.Books.Add(new Book(c5, 9));
                c5.Books.Add(new Book(c5, 10));

                Catalog c6 = new Catalog("King", "The Shining");
                c6.Books.Add(new Book(c6, 11));

                Catalog c7 = new Catalog("J. K. Rowling", "Harry Potter and the Deathly Hallows");
                c7.Books.Add(new Book(c7, 12));

                Catalog c8 = new Catalog("J. K. Rowling", "Harry Potter and the Goblet of Fire");
                c8.Books.Add(new Book(c8, 13));
                c8.Books.Add(new Book(c8, 14));

                Catalog c9 = new Catalog("Twain", "Adventures of Huckleberry Finn");
                c9.Books.Add(new Book(c9, 15));
                c9.Books.Add(new Book(c9, 16));

                Catalog c10 = new Catalog("Twain", "Adventures of Tom Sawyer");
                c10.Books.Add(new Book(c10, 17));
                c10.Books.Add(new Book(c10, 18));
                c10.Books.Add(new Book(c10, 19));

                Catalog c11 = new Catalog("Author1", "Book1");
                c11.Books.Add(new Book(c11, 20));
                c11.Books.Add(new Book(c11, 21));
                c11.Books.Add(new Book(c11, 22));

                Catalog c12 = new Catalog("Author2", "Book2");
                c12.Books.Add(new Book(c12, 23));
                c12.Books.Add(new Book(c12, 24));
                c12.Books.Add(new Book(c12, 25));


                Reader r1 = new Reader(1, "John", "Kowalsky");
                r1.Books.Add(new Book(c5, 26));
                r1.Books.Add(new Book(c9, 27));
                r1.Books.Add(new Book(c10, 28));

                Reader r2 = new Reader(2, "Adam", "Nowak");
                r2.Books.Add(new Book(c2, 29));
                r2.Books.Add(new Book(c9, 30));

                Reader r3 = new Reader(3, "Reader", "Reader");
                r3.Books.Add(new Book(c11, 31));
                r3.Books.Add(new Book(c12, 31));
            }
            #endregion
        }

        private DataContext data { get; set; }

        public DataRepository() {
            data = new DataContext();
            data.Fill(this);
        }

        //Catalog
        public void AddCatalog(Catalog c) {
            data.Catalogs.Add(c);
        }

        public void AddCatalog(string author, string title) {
            data.Catalogs.Add(new Catalog(author, title));
        }

        public void AddCatalog(string author, string title, List<Book> books) {
            data.Catalogs.Add(new Catalog(author, title, books));
        }

        public Catalog GetCatalog(string author, string title) {
            foreach(Catalog c in data.Catalogs) {
                if(c.Author.Equals(author) && c.Title.Equals(title)) {
                    return c;
                }
            }
            return null;
        }

        public Catalog GetCatalog(int index) {
            if(index >= 0 && index < data.Catalogs.Count) {
                return data.Catalogs[index];
            }
            return null;
        }

        public IEnumerable<Catalog> GetAllCatalogs() {
            return data.Catalogs;
        }

        public void UpdateCatalog(int index, Catalog c) {
            if(index >= 0 && index < data.Catalogs.Count) {
                data.Catalogs[index] = c;
            }
        }

        public void UpdateCatalog(string author, string title, Catalog c) {
            for(int i = 0; i < data.Catalogs.Count; i++) {
                if(data.Catalogs[i].Author.Equals(author) && data.Catalogs[i].Title.Equals(title)) {
                    data.Catalogs[i] = c;
                    break;
                }
            }
        }


        public void DeleteCatalog(int index) {
            if(index >= 0 && index < data.Catalogs.Count) {
                data.Catalogs.RemoveAt(index);
            }
        }

        public void DeleteCatalog(string author, string title) {
            Catalog c = GetCatalog(author, title);
            if(c != null) {
                data.Catalogs.Remove(c);
            }
        }

        //Reader
        public void AddReader(Reader r) {
            data.Readers.Add(r);
        }

        public void AddReader(int id, string fistName, string lastName) {
            data.Readers.Add(new Reader(id, fistName, lastName));
        }

        public Reader GetReader(int id) {
            foreach(Reader r in data.Readers) {
                if(r.Id == id) {
                    return r;
                }
            }
            return null;
        }

        //Events
        public void AddEvent(IEvent e) {
            data.Events.Add(e);
        }

        public IEvent GetEvent(int id) {
            return data.Events[id];
        }

        public IEnumerable<IEvent> GetAllEvents() {
            return data.Events;
        }
    }
}
