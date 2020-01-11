using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Model {
    public class Catalog {
        public string Author { get; set; }
        public string Title { get; set; }
        public List<Book> Books { get; set; }

        public Catalog(string author, string title) {
            Author = author;
            Title = title;
            Books = new List<Book>();
        }
    }
}
