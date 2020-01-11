using System;
using System.Collections.Generic;
using Data;


namespace Logic {
    public class Library {
        //private DataRepository dataRepository;

        private string ConnectionString { get; }

        public Library() { 
            ConnectionString = $@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=[TestsDirectory]\Lib.mdf;Integrated Security=True;Connect Timeout=30;";
        }

        public Library(string connectionString) {
            ConnectionString = connectionString;
        }

        #region Book
        public Book GetBook(string author, string title) {
            return null;
        }

        public Book RentBook(string author, string title, Reader reader) {
            return null;
        }

        public void ReturnBook(Book book, Reader reader) {
            
        }

        public void DeleteBook(Book book) {
            
        }

        public void AddBook(Book book) {
           
        }

        public void AddBook(string author, string title) {
         
        }
        #endregion


        #region Reader
        public Reader GetReader(int id) {
            return null;
        }

        public void AddReader(Reader reader) {
           
        }

        public void AddReader(int id, string fistName, string lastName) {
            
        }

        public void DeleteReader(Reader reader) {
            
        }

        public void DeleteReader(int id) {
            
        }

        public void UpdateReader(int id, Reader reader) {
            
        }

        public IEnumerable<Reader> GetAllReaders() {
            return null;
        }
        #endregion


        #region Catalog
        public Catalog GetCatalog(string author, string title) {
            return null;
        }

        public void AddCatalog(Catalog catalog) {
            
        }

        public void AddCatalog(string author, string title) {
            
        }

        public void UpdateCatalog(int index, Catalog catalog) {
            
        }

        public void DeleteCatalog(int index) {
            
        }

        public void DeleteCatalog(string author, string title) {
            
        }

        public IEnumerable<Catalog> GetAllCatalogs() {
            using (LibDataContext lib = new LibDataContext(ConnectionString)) {

                List<Catalog> bookEntities = new List<Book>(); 
                List<Model.Book> bookModels = new List<Model.Book>();

                foreach (Book bookEntity in bookEntities) { 
                    bookModels.Add(new Model.Book(bookEntity.))
                }
            }
            return null;
            
        }
        #endregion


      
    }
}
