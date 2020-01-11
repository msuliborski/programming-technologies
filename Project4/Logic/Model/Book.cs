using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Model {
    public class Book {
        public Catalog Catalog;
        public int IdNumber { get; set; }

        public Book(Catalog catalog, int idNumber) {
            Catalog = catalog;
            IdNumber = idNumber;
        }
    }
}
