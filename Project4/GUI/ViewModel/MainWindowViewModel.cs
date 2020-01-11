using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using Data;
using GUI.Model;
using Logic;

namespace GUI.ViewModel {
    public class MainWindowViewModel : ViewModelBase {

        public MainWindowViewModel() {
            //HiButtonCommand = new RelayCommand(ShowMessage, param => this.canExecute);
            //toggleExecuteCommand = new RelayCommand(ChangeCanExecute
            GetCatalogs();
            rentCommand = new RelayCommand(o => rentMethod(o), o => { return true; });
        }

        private void GetCatalogs() {
            DataRepository dataRepository = new DataRepository();
            dataRepository = Fill(dataRepository);
            Library library = new Library(dataRepository);

            List <CatalogModel> s = new List<CatalogModel>();
            foreach (Catalog x in dataRepository.GetAllCatalogs()) {
                s.Add(new CatalogModel(x.Author, x.Title, x.Books.Count));
            }
            this.catalogs = s;
            Console.Out.WriteLine(Catalogs.Count + " adwdawdwadwadwadwadwad");

        }

        public ICommand rentCommand { get; set; }
        private void rentMethod(Object o) {
            MessageBox.Show("test");
        }

        private CatalogModel currentCatalog;
        public CatalogModel CurrentCatalog {
            get {
                return this.currentCatalog;
            }
            set {
                this.currentCatalog = value;
                this.OnPropertyChanged(nameof(CurrentCatalog));
            }
        }
        private List<CatalogModel> catalogs;
        public List<CatalogModel> Catalogs {
            get {
                return this.catalogs;
            }
            set {
                this.catalogs = value;
                this.OnPropertyChanged("Catalogs");
            }
        }


        private List<ReaderModel> readers;
        public List<ReaderModel> Readers {
            get {
                return this.readers;
            }
            set {
                this.readers = value;
                this.OnPropertyChanged("Readers");
            }
        }













        private string m_ActionText;
        public string ActionText {
            get => m_ActionText;
            set {
                m_ActionText = value;
            }
        }
        public string ButtonContent {
            get {
                return "Click Me";
            }
        }

        //public MainWindowViewModel() {
        //    m_ActionText = "Text to be displayed on the popup";





     








        //private void ReturnButtonClick(object sender, RoutedEventArgs e) {

        //}

        //private void RentButtonClick(object sender, RoutedEventArgs e) {

        //}


        public DataRepository Fill(DataRepository dataRepository) {
            Catalog c1 = new Catalog("Shakespeare", "Sonnet 116");
            c1.Books.Add(new Book(c1, 1));
            c1.Books.Add(new Book(c1, 2));
            c1.Books.Add(new Book(c1, 3));
            dataRepository.AddCatalog(c1);

            Catalog c2 = new Catalog("Shakespeare", "Sonnet 130");
            c2.Books.Add(new Book(c2, 4));
            dataRepository.AddCatalog(c2);

            Catalog c3 = new Catalog("Hemingway", "The Old Man and the Sea");
            c3.Books.Add(new Book(c3, 5));
            c3.Books.Add(new Book(c3, 6));
            dataRepository.AddCatalog(c3);

            Catalog c4 = new Catalog("Hemingway", "The Sun Also Rises");
            c4.Books.Add(new Book(c4, 7));
            c4.Books.Add(new Book(c4, 8));
            dataRepository.AddCatalog(c4);

            Catalog c5 = new Catalog("King", "It");
            c5.Books.Add(new Book(c5, 9));
            c5.Books.Add(new Book(c5, 10));
            dataRepository.AddCatalog(c5);

            Catalog c6 = new Catalog("King", "The Shining");
            c6.Books.Add(new Book(c6, 11));
            dataRepository.AddCatalog(c6);

            Catalog c7 = new Catalog("J. K. Rowling", "Harry Potter and the Deathly Hallows");
            c7.Books.Add(new Book(c7, 12));
            dataRepository.AddCatalog(c7);

            Catalog c8 = new Catalog("J. K. Rowling", "Harry Potter and the Goblet of Fire");
            c8.Books.Add(new Book(c8, 13));
            c8.Books.Add(new Book(c8, 14));
            dataRepository.AddCatalog(c8);

            Catalog c9 = new Catalog("Twain", "Adventures of Huckleberry Finn");
            c9.Books.Add(new Book(c9, 15));
            c9.Books.Add(new Book(c9, 16));
            dataRepository.AddCatalog(c9);

            Catalog c10 = new Catalog("Twain", "Adventures of Tom Sawyer");
            c10.Books.Add(new Book(c10, 17));
            c10.Books.Add(new Book(c10, 18));
            c10.Books.Add(new Book(c10, 19));
            dataRepository.AddCatalog(c10);

            Catalog c11 = new Catalog("Author1", "Book1");
            c11.Books.Add(new Book(c11, 20));
            c11.Books.Add(new Book(c11, 21));
            c11.Books.Add(new Book(c11, 22));
            dataRepository.AddCatalog(c11);

            Catalog c12 = new Catalog("Author2", "Book2");
            c12.Books.Add(new Book(c12, 23));
            c12.Books.Add(new Book(c12, 24));
            c12.Books.Add(new Book(c12, 25));
            dataRepository.AddCatalog(c12);


            Reader r1 = new Reader(1, "John", "Kowalsky");
            r1.Books.Add(new Book(c5, 26));
            r1.Books.Add(new Book(c9, 27));
            r1.Books.Add(new Book(c10, 28));
            dataRepository.AddReader(r1);

            Reader r2 = new Reader(2, "Adam", "Nowak");
            r2.Books.Add(new Book(c2, 29));
            r2.Books.Add(new Book(c9, 30));
            dataRepository.AddReader(r2);

            Reader r3 = new Reader(3, "Reader", "Reader");
            r3.Books.Add(new Book(c11, 31));
            r3.Books.Add(new Book(c12, 31));
            dataRepository.AddReader(r3);

            return dataRepository;
        }
    
    }

}