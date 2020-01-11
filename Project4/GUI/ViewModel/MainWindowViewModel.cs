using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using GUI.Model;
using Services;

namespace GUI.ViewModel {
    public class MainWindowViewModel : ViewModelBase {

        private Library library;

        public MainWindowViewModel() {
            library = new Library();

            GetCatalogs();
            GetReaders();
            GetReadersCatalogs();
            rentCommand = new RelayCommand(o => rentMethod(o), o => canRent(o));
            returnCommand = new RelayCommand(o => returnMethod(o), o => canReturn(o));
        }

        private void GetCatalogs() {
            List<CatalogModel> s = new List<CatalogModel>();
            foreach (Services.Model.Catalog x in library.GetAllCatalogs()) {
                s.Add(new CatalogModel(x.Author, x.Title, x.Books));
            }
            this.catalogs = s;
        }
        private void GetReaders() {
            List <ReaderModel> s = new List<ReaderModel>();
            foreach (Services.Model.Reader x in library.GetAllReaders()) {
                s.Add(new ReaderModel(x.Id, x.FirstName, x.LastName, x.Books));
            }
            this.readers = s;
        }
        private void GetReadersCatalogs() {
            List<CatalogModel> s = new List<CatalogModel>();
            foreach (Services.Model.Catalog x in library.GetReadersCatalogs(currentReader.Id)) {
                s.Add(new CatalogModel(x.Author, x.Title, x.Books));
            }
            this.readersCatalogs = s;
        }


        public ICommand rentCommand { get; set; }
        private void rentMethod(Object o) {
            library.RentBook(currentCatalog.Author, currentCatalog.Title, currentReader.Id);
            GetCatalogs();
            GetReaders();
            this.OnPropertyChanged(nameof(Readers));
            this.OnPropertyChanged(nameof(Catalogs));
        }
        private bool canRent(Object o) {
            if (currentCatalog != null && currentReader != null && library.UserCanRentBook(currentCatalog.Author, currentCatalog.Title)) return true;
            return false;
        }
        
        public ICommand returnCommand { get; set; }
        private void returnMethod(Object o) {
            library.ReturnBook(currentCatalog.Author, currentCatalog.Title, currentReader.Id);
            GetCatalogs();
            GetReaders();
            this.OnPropertyChanged(nameof(Readers));
            this.OnPropertyChanged(nameof(Catalogs));
        }
        private bool canReturn(Object o) {
            if (currentCatalog != null && currentReader != null && library.UserCanReturnBook(currentCatalog.Author, currentCatalog.Title, currentReader.Id)) return true;
            return false;
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
                this.OnPropertyChanged(nameof(CurrentCatalog));
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
                this.OnPropertyChanged(nameof(CurrentReader));
            }
        }
    }
}