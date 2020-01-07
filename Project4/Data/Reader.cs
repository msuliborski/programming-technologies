namespace Data {
    
    public partial class Reader {
       
        public Reader(int id, string firstName, string lastName) : this() {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
        }


        public Reader(string firstName, string lastName) : this() {
            FirstName = firstName;
            LastName = lastName;
        }
    }
}
