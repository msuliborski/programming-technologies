

namespace Services.Model {
    public class Book {
        public int? CatalogId { get; set; }
        public int IdNumber { get; set; }
        public int? ReaderId { get; set; }

        public Book(int? catalogId, int idNumber) {
            CatalogId = catalogId;
            IdNumber = idNumber;
            ReaderId = null;
        }

        public Book(int? catalogId, int? readerId, int idNumber) {
            CatalogId = catalogId;
            IdNumber = idNumber;
            ReaderId = readerId;
        }
    }
}
