using System;

namespace Data {
    public abstract class EventReader : IEvent {
        protected DateTime dateTime;
        public Reader Reader { get; set; }

        public EventReader(DateTime dt, Reader r) {
            dateTime = dt;
            Reader = r;
        }

        public DateTime GetDateTime() {
            return dateTime;
        }

        public abstract EventType GetEventType();
    }

    public class AddReader : EventReader {
        public AddReader(DateTime dt, Reader r) : base(dt, r) { }

        public override EventType GetEventType() {
            return EventType.AddReader;
        }
    }

    public class UpdateReader : EventReader {
        public UpdateReader(DateTime dt, Reader r) : base(dt, r) { }

        public override EventType GetEventType() {
            return EventType.UpdateReader;
        }
    }

    public class DeleteReader : EventReader {
        public DeleteReader(DateTime dt, Reader r) : base(dt, r) { }

        public override EventType GetEventType() {
            return EventType.DeleteReader;
        }
    }
}
