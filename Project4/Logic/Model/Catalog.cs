using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Model {
    public class Catalog {
        public string Author { get; set; }
        public string Title { get; set; }
        public int Books { get; set; }

        public Catalog(string author, string title, int books) {
            Author = author;
            Title = title;
            Books = books;
        }
    }
}
