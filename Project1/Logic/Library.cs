using System;
using System.Collections.Generic;
using System.Text;
using Data;


namespace Logic {
    public class Library {
        private DataRepository dataRepository;

        public Library(DataRepository dataRepository) {
            this.dataRepository = dataRepository;
        }


        #region Book
        public Book RentBook(string author, string title) {
            Catalog catalog = dataRepository.GetCatalog(author, title);
            Book book = dataRepository.GetBook(catalog);
            if (book != null) {
                catalog.Books.Remove(book);
                dataRepository.AddEvent(new RentBook(DateTime.Now, book));
            }
            return book;
        }

        public void ReturnBook(Book book) {
            book.Catalog.Books.Add(book);
            dataRepository.AddEvent(new ReturnBook(DateTime.Now, book));
        }

        public void DeleteBook(Book book) {
            book.Catalog.Books.Remove(book);
            book.Catalog = null;
            dataRepository.AddEvent(new DeleteBook(DateTime.Now, book));
        } 
          #endregion

        #region Reader
        public void AddReader(Reader reader) {
            dataRepository.AddReader(reader);
            dataRepository.AddEvent(new AddReader(DateTime.Now, reader));
        }

        public void DeleteReader(Reader reader) {
            dataRepository.DeleteReader(reader.Id);
            dataRepository.AddEvent(new DeleteReader(DateTime.Now, reader));
        }

        public void DeleteReader(int id) {
            Reader reader = dataRepository.GetReader(id);
            if (reader != null) {
                dataRepository.DeleteReader(id);
                dataRepository.AddEvent(new DeleteReader(DateTime.Now, reader));
            }
        }

        public void UpdateReader(int id, Reader reader) {
            Reader r = dataRepository.GetReader(id);
            if (r != null) {
                dataRepository.UpdateReader(id, reader);
                dataRepository.AddEvent(new UpdateReader(DateTime.Now, reader));
            }
        }
        #endregion
    }
}
