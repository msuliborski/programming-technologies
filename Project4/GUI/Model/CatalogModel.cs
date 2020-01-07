namespace GUI.Model {
    public class CatalogModel {
        public CatalogModel(string a, string t, int b) {
            this.Author = a;
            this.Title = t;
            this.Books = b;
        }
        public string Author { get; set; }
        public string Title { get; set; }
        public int Books { get; set; }
    }
}
