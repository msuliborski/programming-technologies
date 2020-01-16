using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using GUI.Model;
using Services;

namespace GUI.ViewModel {
    public class MainWindowViewModel : ViewModelBase {

        private Library library;
        private bool canRent;
        private bool canReturn;

        public MainWindowViewModel() {
            library = new Library();
            GetCatalogs();
            GetReaders();
            rentCommand = new RelayCommand(o => rentMethod(o), o => canRent);
            returnCommand = new RelayCommand(o => returnMethod(o), o => canReturn);
        }


        private void GetCatalogs() {
            List<CatalogModel> s = new List<CatalogModel>();
            foreach (Services.Model.Catalog x in library.GetAllCatalogs()) {
                s.Add(new CatalogModel(x.Author, x.Title, x.Books));
            }
            this.catalogs = s;
            this.OnPropertyChanged(nameof(Catalogs));
        }
        private void GetReaders() {
            List <ReaderModel> s = new List<ReaderModel>();
            foreach (Services.Model.Reader x in library.GetAllReaders()) {
                s.Add(new ReaderModel(x.Id, x.FirstName, x.LastName, x.Books));
            }
            this.readers = s;
            this.OnPropertyChanged(nameof(Readers));
        }
        private void GetReadersCatalogs() {
            if (currentReader != null) {
                List<CatalogModel> s = new List<CatalogModel>();
                foreach (Services.Model.Catalog x in library.GetReadersCatalogs(currentReader.Id)) {
                    s.Add(new CatalogModel(x.Author, x.Title, x.Books));
                }
                this.readersCatalogs = s;
            }
            this.OnPropertyChanged(nameof(ReadersCatalogs));
        }


        public ICommand rentCommand { get; set; }
        private void rentMethod(Object o) {
            canRent = false;
            canReturn = false;
            Task task = Task.Factory
               .StartNew(() => {
                   library.RentBook(currentCatalog.Author, currentCatalog.Title, currentReader.Id);
                   GetReadersCatalogs();
                   GetCatalogs();
                   GetReaders();
               });
        }

        private void updateCanReturnAndCanRent() {
            
                    if (currentCatalog != null && currentReader != null && library.UserCanRentBook(currentCatalog.Author, currentCatalog.Title)) canRent = true;
                    else canRent = false;
                    if (currentCatalog != null && currentReader != null && library.UserCanReturnBook(currentCatalog.Author, currentCatalog.Title, currentReader.Id)) canReturn = true;
                    else canReturn = false;
                
        }
        
        public ICommand returnCommand { get; set; }
        private void returnMethod(Object o) {
            canRent = false;
            canReturn = false;
            Task task = Task.Factory
                .StartNew(() => {
                    library.ReturnBook(currentCatalog.Author, currentCatalog.Title, currentReader.Id);
                    GetReadersCatalogs();
                    GetCatalogs();
                    GetReaders();
                });
        }


        private List<CatalogModel> catalogs;
        public List<CatalogModel> Catalogs {
            get {
                return this.catalogs;
            }
            set {
                this.catalogs = value;
                this.OnPropertyChanged(nameof(Catalogs));
            }
        }

        private List<CatalogModel> readersCatalogs;
        public List<CatalogModel> ReadersCatalogs {
            get {
                return this.readersCatalogs;
            }
            set {
                this.readersCatalogs = value;
                this.OnPropertyChanged(nameof(ReadersCatalogs));
            }
        }

        private CatalogModel currentCatalog;
        public CatalogModel CurrentCatalog {
            get {
                return this.currentCatalog;
            }
            set {
                this.currentCatalog = value;
                Task task = Task.Factory
                .StartNew(() => {
                    updateCanReturnAndCanRent();
                    this.OnPropertyChanged(nameof(CurrentCatalog));
                });
            }
        }


        private List<ReaderModel> readers;
        public List<ReaderModel> Readers {
            get {
                return this.readers;
            }
            set {
                this.readers = value;
                this.OnPropertyChanged(nameof(Readers));
            }
        }

        private ReaderModel currentReader;
        public ReaderModel CurrentReader {
            get {
                return this.currentReader;
            }
            set {
                this.currentReader = value;
                Task task = Task.Factory
                .StartNew(() => {
                    GetReadersCatalogs();
                    updateCanReturnAndCanRent();
                    this.OnPropertyChanged(nameof(CurrentReader));
                });
            }
        }
    }
}