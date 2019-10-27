using System;
using System.Collections.Generic;
using System.Text;

namespace Data {
    public class DataRepository {
        public class DataContext {
            public List<Reader> Readers { get; set; }
            public List<Catalog> Catalogs { get; set; }
            public List<IEvent> Events { get; set; }

            public void Fill(DataRepository dr) {
                //Catalog c1 = new Catalog("Shakespeare", "Sonnet 116");
                //Catalog c1 = new Catalog("Shakespeare", "Sonnet 130");
                //Catalog c2 = new Catalog("Hemingway", "");
                //Catalog c3 = new Catalog("King", "");
                //Catalog c4 = new Catalog("J. K. Rowling", "");
                //Catalog c5 = new Catalog("Twain", "");
            }
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

        public Catalog GetCatalog(int id) {
            return data.Catalogs[id];
        }
        public IEnumerable<Catalog> GetAllCatalogs() {
            return data.Catalogs;
        }

        public void UpdateCatolog(int id, Catalog c) {
            data.Catalogs[id] = c;
        }

        public void DeleteCatalog(int id) {
            data.Catalogs.RemoveAt(id);
        }

        //Reader
        public void AddReader(Reader r) {
            data.Readers.Add(r);
        }

        public Reader GetReader(int id) {
            return data.Readers[id];
        }

        public IEnumerable<Reader> GetAllReaders() {
            return data.Readers;
        }

        public void UpdateReader(int id, Reader r) {
            data.Readers[id] = r;
        }

        public void DeleteReader(int id) {
            data.Readers.RemoveAt(id);
        }

        public void AddBook(Book b) {
            b.Catalog.Books.Add(b);
        }

        public void DeleteBook(Book b) {
            b.Catalog.Books.Remove(b);
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
