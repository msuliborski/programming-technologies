using System;
using System.Collections.Generic;
using Data;
using System.Linq;

namespace Logic {
    public class Library {
        private DataRepository dataRepository;

        public Library(DataRepository dataRepository) {
            this.dataRepository = dataRepository;
        }

        #region Book
        public Book GetBook(string author, string title) {
            return dataRepository.GetBook(dataRepository.GetCatalog(author, title));
        }

        public Book RentBook(string author, string title, Reader reader) {
            Catalog catalog = dataRepository.GetCatalog(author, title);
            Book book = dataRepository.GetBook(catalog);
            if(book != null) {
                catalog.Books.Remove(book);
                reader.Books.Add(book);
                dataRepository.AddEvent(new RentBook(DateTime.Now, book, reader));
            }
            return book;
        }

        public bool UserCanRentBook(string author, string title, int readerId) {
            Catalog catalog = dataRepository.GetCatalog(author, title);
            Book book = dataRepository.GetBook(catalog);
            Reader reader = dataRepository.GetReader(readerId);
            if (book != null && !reader.Books.Contains(book)) return true; 
            return false;
        }

        public bool UserCanReturnBook(string author, string title, int readerId) {
            Catalog catalog = dataRepository.GetCatalog(author, title);
            Reader reader = dataRepository.GetReader(readerId);
            Book book = reader.Books.FirstOrDefault(c => c.Catalog == catalog);
            if (book != null && reader.Books.Contains(book)) return true; 
            return false;
        }

        public void ReturnBook(string author, string title, int readerId) { 
            Catalog catalog = dataRepository.GetCatalog(author, title);
            Reader reader = dataRepository.GetReader(readerId);
            Book book = reader.Books.FirstOrDefault(c => c.Catalog == catalog);

            if (book != null) {
                book.Catalog.Books.Add(book);
                reader.Books.Remove(book);
                dataRepository.AddEvent(new ReturnBook(DateTime.Now, book, reader));
            }
        }

        public void ReturnBook(Book book, Reader reader) {
            if(reader.Books.Contains(book)) {
                book.Catalog.Books.Add(book);
                reader.Books.Remove(book);
                dataRepository.AddEvent(new ReturnBook(DateTime.Now, book, reader));
            }
        }

        public void DeleteBook(Book book) {
            book.Catalog.Books.Remove(book);
            dataRepository.AddEvent(new DeleteBook(DateTime.Now, book));
        }

        public void AddBook(Book book) {
            if(book.Catalog != null) {
                dataRepository.AddBook(book);
                dataRepository.AddEvent(new AddBook(DateTime.Now, book));
            }

        }

        public void AddBook(string author, string title) {
            Catalog catalog = dataRepository.GetCatalog(author, title);
            if(catalog != null) {
                Book book = new Book(catalog, dataRepository.CurrentBookId);
                dataRepository.AddBook(book);
                dataRepository.AddEvent(new AddBook(DateTime.Now, book));
            }
        }
        #endregion


        #region Reader
        public Reader GetReader(int id) {
            return dataRepository.GetReader(id);
        }

        public void AddReader(Reader reader) {
            dataRepository.AddReader(reader);
            dataRepository.AddEvent(new AddReader(DateTime.Now, reader));
        }

        public void AddReader(int id, string fistName, string lastName) {
            Reader reader = new Reader(id, fistName, lastName);
            dataRepository.AddReader(reader);
            dataRepository.AddEvent(new AddReader(DateTime.Now, reader));
        }

        public void DeleteReader(Reader reader) {
            dataRepository.DeleteReader(reader.Id);
            dataRepository.AddEvent(new DeleteReader(DateTime.Now, reader));
        }

        public void DeleteReader(int id) {
            Reader reader = dataRepository.GetReader(id);
            if(reader != null) {
                dataRepository.DeleteReader(id);
                dataRepository.AddEvent(new DeleteReader(DateTime.Now, reader));
            }
        }

        public void UpdateReader(int id, Reader reader) {
            Reader r = dataRepository.GetReader(id);
            if(r != null) {
                dataRepository.UpdateReader(id, reader);
                dataRepository.AddEvent(new UpdateReader(DateTime.Now, reader));
            }
        }

        public IEnumerable<Reader> GetAllReaders() {
            return dataRepository.GetAllReaders();
        }
        #endregion


        #region Catalog
        public Catalog GetCatalog(string author, string title) {
            return dataRepository.GetCatalog(author, title);
        }

        public void AddCatalog(Catalog catalog) {
            dataRepository.AddCatalog(catalog);
            dataRepository.AddEvent(new AddCatalog(DateTime.Now, catalog));
        }

        public void AddCatalog(string author, string title) {
            Catalog catalog = new Catalog(author, title);
            dataRepository.AddCatalog(catalog);
            dataRepository.AddEvent(new AddCatalog(DateTime.Now, catalog));
        }

        public void UpdateCatalog(int index, Catalog catalog) {
            Catalog c = dataRepository.GetCatalog(index);
            if(c != null) {
                dataRepository.UpdateCatalog(index, catalog);
                dataRepository.AddEvent(new UpdateCatalog(DateTime.Now, catalog));
            }
        }

        public void DeleteCatalog(int index) {
            Catalog catalog = dataRepository.GetCatalog(index);
            if(catalog != null) {
                dataRepository.DeleteCatalog(index);
                dataRepository.AddEvent(new DeleteCatalog(DateTime.Now, catalog));
            }
        }

        public void DeleteCatalog(string author, string title) {
            Catalog catalog = dataRepository.GetCatalog(author, title);
            if(catalog != null) {
                dataRepository.DeleteCatalog(author, title);
                dataRepository.AddEvent(new DeleteCatalog(DateTime.Now, catalog));
            }
        }

        public IEnumerable<Catalog> GetAllCatalogs() {
            return dataRepository.GetAllCatalogs();
        }
        #endregion


        #region Events
        public IEnumerable<IEvent> GetAllEvents() {
            return dataRepository.GetAllEvents();
        }

        public IEnumerable<IEvent> GetEventsForReader(Reader reader) {
            List<IEvent> userEvents = new List<IEvent>();
            foreach(IEvent ievent in GetAllEvents()) {
                if(ievent.GetEventType() == EventType.AddReader ||
                    ievent.GetEventType() == EventType.UpdateReader ||
                    ievent.GetEventType() == EventType.DeleteReader) {
                    EventReader eventReader = ievent as EventReader;
                    if(eventReader.Reader == reader) {
                        userEvents.Add(eventReader);
                    }
                } else if(ievent.GetEventType() == EventType.RentBook) {
                    RentBook rentBook = ievent as RentBook;
                    if(rentBook.Reader == reader) {
                        userEvents.Add(rentBook);
                    }
                } else if(ievent.GetEventType() == EventType.ReturnBook) {
                    ReturnBook returnBook = ievent as ReturnBook;
                    if(returnBook.Reader == reader) {
                        userEvents.Add(returnBook);
                    }
                }
            }
            return userEvents;
        }

        public IEnumerable<IEvent> GetEventsForCatalog(Catalog catalog) {
            List<IEvent> catalogEvents = new List<IEvent>();
            foreach(IEvent ievent in GetAllEvents()) {
                if(ievent.GetEventType() == EventType.AddCatalog ||
                    ievent.GetEventType() == EventType.UpdateCatalog ||
                    ievent.GetEventType() == EventType.DeleteCatalog) {
                    EventCatalog eventCatalog = ievent as EventCatalog;
                    if(eventCatalog.Catalog == catalog) {
                        catalogEvents.Add(eventCatalog);
                    }
                }
            }
            return catalogEvents;
        }


        public IEnumerable<IEvent> GetEventsForBook(Book book) {
            List<IEvent> bookEvents = new List<IEvent>();
            foreach(IEvent ievent in GetAllEvents()) {
                if(ievent.GetEventType() == EventType.AddBook ||
                    ievent.GetEventType() == EventType.DeleteBook ||
                    ievent.GetEventType() == EventType.RentBook ||
                    ievent.GetEventType() == EventType.ReturnBook) {
                    EventBook eventBook = ievent as EventBook;
                    if(eventBook.Book == book) {
                        bookEvents.Add(eventBook);
                    }
                }
            }
            return bookEvents;
        }

        public IEnumerable<IEvent> GetEventsBetweenDates(DateTime startDate, DateTime endDate) {
            List<IEvent> events = new List<IEvent>();
            foreach(IEvent ievent in GetAllEvents()) {
                if(ievent.GetDateTime() >= startDate && ievent.GetDateTime() <= endDate) {
                    events.Add(ievent);
                }
            }
            return events;
        }
        #endregion
    }
}
