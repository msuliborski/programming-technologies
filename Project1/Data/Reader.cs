using System;
using System.Collections.Generic;
using System.Text;

namespace Data {
    public class Reader {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public int Id { get; set; }

        public Reader(int id, string firstName, string lastName) {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
        }
    }
}
