namespace GUI.Model {
    public class ReaderModel {
        public ReaderModel(int i, string f, string l, int b) {
            this.Id = i;
            this.FirstName = f;
            this.LastName = l;
            this.Books = b;
        }
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Books { get; set; }
    }
}
