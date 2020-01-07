namespace Data {
    
    public partial class Catalog {

        public Catalog(int id, string author, string title) : this() {
            Id = id;
            Author = author;
            Title = title;
        }


        public Catalog(string author, string title) : this() {
            Author = author;
            Title = title;
        }
    }


}
