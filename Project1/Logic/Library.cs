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

        public Book RentBook(string author, string title) {
            Catalog catalog = dataRepository.GetCatalog(author, title);
            if (catalog.Books.Count != 0) {
                Book book = catalog.Books[catalog.Books.Count - 1];
                catalog.Books.RemoveAt(catalog.Books.Count - 1);
                return book;
            }
            return null;
        }

        public void ReturnBook(Book book) {
            book.Catalog.Books.Add(book);
            dataRepository.
        }


        void zwroc(Book b)
        {
            b.Catalog.Books.Add(b);
        }
    }
}
