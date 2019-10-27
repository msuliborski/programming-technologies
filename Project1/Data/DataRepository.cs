using System;
using System.Collections.Generic;
using System.Text;

namespace Data {
    public class DataRepository {
        public class DataContext {
            public List<Reader> Readers { get; set; }
            public List<Catalog> Catalogs { get; set; }
            public List<IEvent> EventHistory { get; set; }
            public List<Book> Books { get; set; }

            public void Fill(DataRepository dr) {

            }
        }

        private DataContext data { get; set; }

        public DataRepository() {
            data = new DataContext();
            data.Fill(this);
        }

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


        public void AddBook(Reader r) {
            data.Books.Add(r);
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

    }
}
