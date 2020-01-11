

namespace Services.Model {
    public class Book {
        public int CatalogId;
        public int IdNumber { get; set; }

        public Book(int catalogId, int idNumber) {
            CatalogId = catalogId;
            IdNumber = idNumber;
        }
    }
}
