using System;
using System.Collections.Generic;
using System.Text;

namespace Data {
    public abstract class EventBook : IEvent {
        protected DateTime dateTime;
        public Book Book { get; set; }

        public EventBook(DateTime dt, Book b) {
            dateTime = dt;
            Book = b;
        }

        public DateTime GetDateTime() {
            return dateTime;
        }

        public abstract EventType GetEventType();
    }

    public class AddBook : EventBook {
        public AddBook(DateTime dt, Book b) : base(dt, b) { }

        public override EventType GetEventType() {
            return EventType.AddBook;
        }
    }

    public class RentBook : EventBook {
        public Reader Reader { get; set; }

        public RentBook(DateTime dt, Book b, Reader r) : base(dt, b) {
            Reader = r;
        }

        public override EventType GetEventType() {
            return EventType.RentBook;
        }
    }

    public class DeleteBook : EventBook {
        public DeleteBook(DateTime dt, Book b) : base(dt, b) { }

        public override EventType GetEventType() {
            return EventType.DeleteBook;
        }
    }

    public class ReturnBook : EventBook {
        public Reader Reader { get; set; }

        public ReturnBook(DateTime dt, Book b, Reader r) : base(dt, b) {
            Reader = r;
        }

        public override EventType GetEventType() {
            return EventType.ReturnBook;
        }
    }
}
