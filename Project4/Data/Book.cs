namespace Data {

    public partial class Book {
        
        public Book(int idNumber, int catalogId, int readerId) : this() {
            IdNumber = idNumber;
            CatalogId = catalogId;
            ReaderId = readerId;
        }

        public Book(int catalogId, int readerId) : this() {
            CatalogId = catalogId;
            ReaderId = readerId;
        }
    }
}
