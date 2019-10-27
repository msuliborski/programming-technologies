using System;
using System.Collections.Generic;
using System.Text;

namespace Data {
    public abstract class EventBook : IEvent {
        protected DateTime dateTime;
        protected Book book;

        public EventBook(DateTime dt, Book b) {
            dateTime = dt;
            book = b;
        }

        public DateTime GetDateTime() {
            return dateTime;
        }

        public abstract EventType GetEventType();
    }

    public class AddBook : EventBook {

        public AddBook(DateTime dt, Book b) : base(dt, b) {

        }

        public override EventType GetEventType() {
            return EventType.AddBook;
        }
    }

    public class RentBook : EventBook {

        public RentBook(DateTime dt, Book b) : base(dt, b) {

        }

        public override EventType GetEventType() {
            return EventType.RentBook;
        }
    }

    public class DeleteBook : EventBook {

        public DeleteBook(DateTime dt, Book b) : base(dt, b) {

        }

        public override EventType GetEventType() {
            return EventType.DeleteBook;
        }
    }

    public class ReturnBook : EventBook {

        public ReturnBook(DateTime dt, Book b) : base(dt, b) {

        }

        public override EventType GetEventType() {
            return EventType.ReturnBook;
        }
    }
}
