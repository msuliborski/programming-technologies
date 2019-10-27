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
