using System;
using System.Collections.Generic;
using Data;
using System.Linq;
using System.IO;

namespace Services {
    public class Library {
        
        private string ConnectionString { get; }

        public Library() {
            
            string workingFolder = Environment.CurrentDirectory + "\\..\\..\\..\\Tests\\Lib.mdf";
            string DBPath = Path.GetFullPath(workingFolder);
            ConnectionString = $@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={DBPath};Integrated Security=True;Connect Timeout=30;";
        }

        public Library(string connectionString) {
            ConnectionString = connectionString;
        }

        #region Book

      
        public void AddBook(Model.Book book) {
            using (LibDataContext lib = new LibDataContext(ConnectionString)) {
                lib.Books.InsertOnSubmit(new Book(book.IdNumber, (int) book.CatalogId, (int) book.ReaderId));
                lib.SubmitChanges();
            }
        }

        public void UpdateBook(Model.Book book) {
            using (LibDataContext lib = new LibDataContext(ConnectionString)) {
                Book bookEntity = lib.Books.Where(b => b.IdNumber == book.IdNumber).FirstOrDefault();
                bookEntity.CatalogId = book.CatalogId;
                bookEntity.ReaderId = book.ReaderId;
                lib.SubmitChanges();
            }
        }

        public void DeleteBook(int bookId) {
            using (LibDataContext lib = new LibDataContext(ConnectionString)) {
                Book bookEntity = lib.Books.Where(b => b.IdNumber == bookId).FirstOrDefault();
                lib.Books.DeleteOnSubmit(bookEntity);
                lib.SubmitChanges();
            }
        }

        public Model.Book GetBook(int bookId) {
            using (LibDataContext lib = new LibDataContext(ConnectionString)) {
                Book bookEntity = lib.Books.Where(b => b.IdNumber == bookId).FirstOrDefault();
                return new Model.Book(bookEntity.CatalogId, bookEntity.ReaderId, bookId);
            }
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
            using (LibDataContext lib = new LibDataContext(ConnectionString)) {
                lib.Readers.InsertOnSubmit(new Reader(reader.Id, reader.FirstName, reader.LastName));
                lib.SubmitChanges();
            }
        }

        public void UpdateReader(Model.Reader reader) {
            using (LibDataContext lib = new LibDataContext(ConnectionString)) {
                Reader readerEntity = lib.Readers.Where(r => r.Id == reader.Id).FirstOrDefault();
                readerEntity.FirstName = reader.FirstName;
                readerEntity.LastName = reader.LastName;
                lib.SubmitChanges();
            }
        }

        public void DeleteReader(int readerId) {
            using (LibDataContext lib = new LibDataContext(ConnectionString)) {
                Reader readerEntity = lib.Readers.Where(r => r.Id == readerId).FirstOrDefault();
                lib.Readers.DeleteOnSubmit(readerEntity);
                lib.SubmitChanges();
            }
        }

        public Model.Reader GetReader(int readerId) {
            using (LibDataContext lib = new LibDataContext(ConnectionString)) {
                Reader readerEntity = lib.Readers.Where(r => r.Id == readerId).FirstOrDefault();
                return new Model.Reader(readerId, readerEntity.FirstName, readerEntity.LastName);
            }
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
            using (LibDataContext lib = new LibDataContext(ConnectionString)) {
                lib.Catalogs.InsertOnSubmit(new Catalog(catalog.CatalogId, catalog.Author, catalog.Title));
                lib.SubmitChanges();
            }
        }

        public void UpdateCatalog(Model.Catalog catalog) {
            using (LibDataContext lib = new LibDataContext(ConnectionString)) {
                Catalog catalogEntity = lib.Catalogs.Where(c => c.Id == catalog.CatalogId).FirstOrDefault();
                catalogEntity.Author = catalog.Author;
                catalogEntity.Title = catalog.Title;
                lib.SubmitChanges();
            }
        }

        public void DeleteCatalog(int catalogId) {
            using (LibDataContext lib = new LibDataContext(ConnectionString)) {
                Catalog catalogEntity = lib.Catalogs.Where(c => c.Id == catalogId).FirstOrDefault();
                lib.Catalogs.DeleteOnSubmit(catalogEntity);
                lib.SubmitChanges();
            }
        }

        public Model.Catalog GetCatalog(int catalogId) {
            using (LibDataContext lib = new LibDataContext(ConnectionString)) {
                Catalog catalogEntity = lib.Catalogs.Where(c => c.Id == catalogId).FirstOrDefault();
                return new Model.Catalog(catalogEntity.Id, catalogEntity.Author, catalogEntity.Title, catalogEntity.Books.Count);
            }
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
