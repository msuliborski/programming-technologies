using System;
using System.Collections.Generic;
using System.Text;

namespace Data {

    [Serializable]
    public class Book {
        public Catalog Catalog;
        public int IdNumber { get; set; }

        public Book(Catalog catalog, int idNumber) {
            Catalog = catalog;
            IdNumber = idNumber;
        }
    }
}
