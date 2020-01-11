using System;
using System.Collections.Generic;
using Data;
using System.Linq;


namespace Services {
    public class Library {
        
        private string ConnectionString { get; }

        public Library() { 
            ConnectionString = $@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=[TestsDirectory]\Lib.mdf;Integrated Security=True;Connect Timeout=30;";
        }

        public Library(string connectionString) {
            ConnectionString = connectionString;
        }

        #region Book
        public Model.Book GetBook(string author, string title) {
            using (LibDataContext lib = new LibDataContext(ConnectionString)) {
                Catalog catalogEntity = (from _catalog in lib.Catalogs
                                   where _catalog.Author == author && _catalog.Title == title
                                   select _catalog).SingleOrDefault();
                if (catalogEntity != null) {
                    Book bookEntity = catalogEntity.Books.FirstOrDefault();
                    return bookEntity != null ? new Model.Book(bookEntity.CatalogId, bookEntity.IdNumber) : null;
                } else {
                    return null;
                }
            }
        }
        
        public bool UserCanRentBook(string author, string title, int readerId) {
            using (LibDataContext lib = new LibDataContext(ConnectionString)) {
                Catalog catalogEntity = (from _catalog in lib.Catalogs
                                         where _catalog.Author == author && _catalog.Title == title
                                         select _catalog).SingleOrDefault();
                if (catalogEntity != null) {
                    Book bookEntity = catalogEntity.Books.FirstOrDefault();
                    Reader readerEntity = (from _reader in lib.Readers
                                           where _reader.Id == readerId
                                           select _reader).SingleOrDefault();
                    if (bookEntity != null) return true;
                }
                return false;
            }
        }

        public bool UserCanReturnBook(string author, string title, int readerId) {
            using (LibDataContext lib = new LibDataContext(ConnectionString)) {
                Catalog catalogEntity = (from _catalog in lib.Catalogs
                                         where _catalog.Author == author && _catalog.Title == title
                                         select _catalog).SingleOrDefault();
                Reader readerEntity = (from _reader in lib.Readers
                                       where _reader.Id == readerId
                                       select _reader).SingleOrDefault();
                if (catalogEntity != null && readerEntity != null) {
                    Book bookEntity = readerEntity.Books.FirstOrDefault(b => b.Catalog == catalogEntity);
                    if (bookEntity != null) return true;
                }
                return false;
            }
        }

        public void ReturnBook(string author, string title, int readerId) { 
            Catalog catalog = dataRepository.GetCatalog(author, title);
            Reader reader = dataRepository.GetReader(readerId);
            Book book = reader.Books.FirstOrDefault(c => c.Catalog == catalog);

            if (book != null) {
                book.Catalog.Books.Add(book);
                reader.Books.Remove(book);
                dataRepository.AddEvent(new ReturnBook(DateTime.Now, book, reader));
            }
        }

        public Book RentBook(string author, string title, int readerId) {
            return null;
        }

        #endregion


        #region Reader
        public IEnumerable<Reader> GetAllReaders() {
            return null;
        }
        #endregion

        #region Catalog
        public IEnumerable<Model.Catalog> GetReadersCatalogs(int readerId) {
            using (LibDataContext lib = new LibDataContext(ConnectionString)) {
                IEnumerable<Catalog> catalogEntities = (from _book in lib.Books
                                       where _book.ReaderId == readerId
                                       select _book.Catalog).Distinct();
                List<Model.Catalog> catalogModels = new List<Model.Catalog>();
                foreach (Catalog catalogEntity in catalogEntities) {
                    catalogModels.Add(new Model.Catalog(catalogEntity.Author, catalogEntity.Title, catalogEntity.Books.Count));
                }
                return catalogModels;
            }
        }

        public IEnumerable<Model.Catalog> GetAllCatalogs() {
            using (LibDataContext lib = new LibDataContext(ConnectionString)) {

                List<Catalog> catalogEntities = new List<Catalog>(); 
                List<Model.Catalog> catalogModels = new List<Model.Catalog>();

                foreach (Catalog catalogEntity in catalogEntities) {
                    catalogModels.Add(new Model.Catalog(catalogEntity.Author, catalogEntity.Title, catalogEntity.Books.Count));
                }
                return catalogModels;
            }
        }
        #endregion

       
      
    }
}
