using System;
using System.Collections.Generic;
using Data;


namespace Logic {
    public class Library {
        private DataRepository dataRepository;

        public Library(DataRepository dataRepository) {
            this.dataRepository = dataRepository;
        }


        #region Book
        public Book RentBook(string author, string title) {
            Catalog catalog = dataRepository.GetCatalog(author, title);
            Book book = dataRepository.GetBook(catalog);
            if(book != null) {
                catalog.Books.Remove(book);
                dataRepository.AddEvent(new RentBook(DateTime.Now, book));
            }
            return book;
        }

        public void ReturnBook(Book book) {
            book.Catalog.Books.Add(book);
            dataRepository.AddEvent(new ReturnBook(DateTime.Now, book));
        }

        public void DeleteBook(Book book) {
            book.Catalog.Books.Remove(book);
            book.Catalog = null;
            dataRepository.AddEvent(new DeleteBook(DateTime.Now, book));
        }
        #endregion

        #region Reader
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
                    ievent.GetEventType() == EventType.DeleteBook) {
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
