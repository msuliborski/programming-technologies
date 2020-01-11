
using System;
using System.Collections.Generic;
using System.Text;

namespace Data {

    [Serializable]
    public partial class Book {
        public Catalog Catalog;
        public int IdNumber { get; set; }
        //public string Author { get; set; }
        //public string Title { get; set; }

        public Book(Catalog catalog, int idNumber) { //, string author, string title) {
            Catalog = catalog;
            IdNumber = idNumber;
           // Author = author;
          //  Title = title;
        }
    }
}
