using System;
using System.Collections.Generic;
using System.Text;

namespace Data
{
    public class DataRepository
    {
        public class DataContext
        {
            private DataSupplier dataSupplier;
            public List<Book> Books { get; set; }
        }
    }
}
