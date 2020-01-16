using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Model {
    public class Catalog {
        public int CatalogId { get; set; }
        public string Author { get; set; }
        public string Title { get; set; }
        public int Books { get; set; }

        public Catalog(string author, string title, int books) {
            Author = author;
            Title = title;
            Books = books;
        }

        public Catalog(int catalogId, string author, string title, int books) {
            CatalogId = catalogId;
            Author = author;
            Title = title;
            Books = books;
        }

        public Catalog(int catalogId, string author, string title) {
            CatalogId = catalogId;
            Author = author;
            Title = title;
        }

        public Catalog(string author, string title) {
            Author = author;
            Title = title;
        }
    }
}
