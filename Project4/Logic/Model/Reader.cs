using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Model {
    public class Reader {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Books { get; set; }

        public Reader(int id, string firstName, string lastName, int books) {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Books = books;
        }
    }
}
