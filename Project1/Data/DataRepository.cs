using System.Collections.Generic;

namespace Data {
    public class DataRepository {
        private class DataContext {

            public int CurrentBookId { get; set; }
            public List<Reader> Readers { get; set; }
            public List<Catalog> Catalogs { get; set; }
            public List<IEvent> Events { get; set; }

            public DataContext() {
                Readers = new List<Reader>();
                Catalogs = new List<Catalog>();
                Events = new List<IEvent>();
                CurrentBookId = 0;
            }
        }

        private DataContext data { get; set; }
        private IFiller filler;

        public DataRepository(IFiller filler) {
            data = new DataContext();
            this.filler = filler;
            this.filler.Fill(this);
        }

        #region Catalog
        public void AddCatalog(Catalog c) {
            data.Catalogs.Add(c);
        }

        public void SetCatalogs(List<Catalog> c) {
            data.Catalogs = c;
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
        #endregion

        #region Reader
        public void AddReader(Reader r) {
            data.Readers.Add(r);
        }

        public void SetReaders(List<Reader> r) {
            data.Readers = r;
        }

        public Reader GetReader(int id) {
            foreach(Reader r in data.Readers) {
                if(r.Id == id) {
                    return r;
                }
            }
            return null;
        }

        public IEnumerable<Reader> GetAllReaders() {
            return data.Readers;
        }

        public void DeleteReader(int id) {
            Reader reader = GetReader(id);
            if(reader != null) {
                data.Readers.Remove(reader);
            }
        }

        public void UpdateReader(int id, Reader reader) {
            for(int i = 0; i < data.Readers.Count; i++) {
                if(data.Readers[i].Id == id) {
                    data.Readers[i] = reader;
                    break;
                }
            }
        }
        #endregion

        #region Book
        public Book GetBook(Catalog catalog) {
            if(catalog.Books.Count != 0) {
                Book book = catalog.Books[catalog.Books.Count - 1];
                return book;
            }
            return null;
        }

        public void AddBook(Book book) {
            if(book.Catalog != null) {
                book.Catalog.Books.Add(book);
                data.CurrentBookId++;
            }
        }

        public int CurrentBookId {
            get { return data.CurrentBookId; }
        }

        #endregion

        #region Events
        public void AddEvent(IEvent e) {
            data.Events.Add(e);
        }

        public IEvent GetEvent(int index) {
            if(index >= 0 && index < data.Events.Count) {
                return data.Events[index];
            }
            return null;
        }

        public IEnumerable<IEvent> GetAllEvents() {
            return data.Events;
        }
        #endregion

    }
}
