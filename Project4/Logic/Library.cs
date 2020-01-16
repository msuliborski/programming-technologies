using System;
using System.Collections.Generic;
using Data;
using System.Linq;
using System.IO;

namespace Services {
    public class Library {
        
        private string ConnectionString { get; }

        public Library() {
            
            string workingFolder = Environment.CurrentDirectory + "\\..\\..\\..\\Tests\\Lib_old.mdf";
            string DBPath = Path.GetFullPath(workingFolder);
            ConnectionString = $@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={DBPath};Integrated Security=True;Connect Timeout=30;";
        }

        public Library(string connectionString) {
            ConnectionString = connectionString;
        }

        #region Book

        public void AddBook(Model.Book book) { 
        }

        public void UpdateBook(Model.Book book) {
        }

        public void DeleteBook(int bookId) { 
        }

        public Model.Book GetBook(int bookId) {
            return null;
        }

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
        
        public bool UserCanRentBook(string author, string title) {
            using (LibDataContext lib = new LibDataContext(ConnectionString)) {
                Catalog catalogEntity = (from _catalog in lib.Catalogs
                                         where _catalog.Author == author && _catalog.Title == title
                                         select _catalog).SingleOrDefault();
                if (catalogEntity != null) {
                    Book bookEntity = catalogEntity.Books.Where(b => b.ReaderId == null).FirstOrDefault();
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
            using (LibDataContext lib = new LibDataContext(ConnectionString)) {

                Catalog catalogEntity = (from _catalog in lib.Catalogs
                                         where _catalog.Author == author && _catalog.Title == title
                                         select _catalog).SingleOrDefault();
                Reader readerEntity = (from _reader in lib.Readers
                                       where _reader.Id == readerId
                                       select _reader).SingleOrDefault();

                if (catalogEntity != null && readerEntity != null) { 
                    Book bookEntity = readerEntity.Books.FirstOrDefault(b => b.CatalogId == catalogEntity.Id);
                    if (bookEntity != null) {
                        bookEntity.Reader = null;
                        lib.SubmitChanges();
                    }
                }
            }
        }

        public void RentBook(string author, string title, int readerId) {
            using (LibDataContext lib = new LibDataContext(ConnectionString)) {

                Catalog catalogEntity = (from _catalog in lib.Catalogs
                                         where _catalog.Author == author && _catalog.Title == title
                                         select _catalog).SingleOrDefault();

                Reader readerEntity = lib.Readers.Where(r => r.Id == readerId).SingleOrDefault();

                if (catalogEntity != null && readerEntity != null) {
                    Book bookEntity = catalogEntity.Books.Where(b => b.ReaderId == null && b.CatalogId == catalogEntity.Id).FirstOrDefault();
                    if (bookEntity != null) {
                        bookEntity.ReaderId = readerEntity.Id;
                        lib.SubmitChanges();
                    }
                }
            }
        }

        #endregion


        #region Reader

        public void AddReader(Model.Reader reader) {
        }

        public void UpdateReader(Model.Reader reader) {
        }

        public void DeleteReader(int readerId) {
        }

        public Model.Reader GetReader(int readerId) {
            return null;
        }

        public IEnumerable<Model.Reader> GetAllReaders() {
            using (LibDataContext lib = new LibDataContext(ConnectionString)) {
                List<Reader> readerEntities = lib.Readers.ToList();
                List<Model.Reader> readerModels = new List<Model.Reader>();
                
                foreach (Reader readerEntity in readerEntities) {
                    readerModels.Add(new Model.Reader(readerEntity.Id, readerEntity.FirstName, readerEntity.LastName, readerEntity.Books.Count));
                }
                return readerModels;
            }
        }
        #endregion

        #region Catalog

        public void AddCatalog(Model.Catalog catalog) {
        }

        public void UpdateCatalog(Model.Catalog catalog) {
        }

        public void DeleteCatalog(int catalogId) {
        }

        public Model.Catalog GetCatalog(int catalogId) {
            return null;
        }
        public IEnumerable<Model.Catalog> GetReadersCatalogs(int readerId) {
            using (LibDataContext lib = new LibDataContext(ConnectionString)) {
                IEnumerable<Catalog> catalogEntities = (from _book in lib.Books
                                       where _book.ReaderId == readerId
                                       select _book.Catalog).Distinct();
                Reader readerEntity = (from _reader in lib.Readers
                                       where _reader.Id == readerId
                                       select _reader).SingleOrDefault();
                List<Model.Catalog> catalogModels = new List<Model.Catalog>();
                foreach (Catalog catalogEntity in catalogEntities) {
                    List<Book> bookEntities = catalogEntity.Books.Where(b => b.ReaderId == readerEntity.Id).ToList();
                    catalogModels.Add(new Model.Catalog(catalogEntity.Author, catalogEntity.Title, bookEntities.Count));
                }
                return catalogModels;
            }
        }

        public IEnumerable<Model.Catalog> GetAllCatalogs() {
            using (LibDataContext lib = new LibDataContext(ConnectionString)) {

                List<Catalog> catalogEntities = lib.Catalogs.ToList(); 
                List<Model.Catalog> catalogModels = new List<Model.Catalog>();
                foreach (Catalog catalogEntity in catalogEntities) {
                    List<Book> bookEntities = catalogEntity.Books.Where(b => b.ReaderId == null).ToList();
                    catalogModels.Add(new Model.Catalog(catalogEntity.Author, catalogEntity.Title, bookEntities.Count));
                }
                return catalogModels;
            }
        }
        #endregion

       
      
    }
}
