using System;
using System.Collections.Generic;
using System.Text;

namespace Data {
    public abstract class EventReader : IEvent {
        protected DateTime dateTime;
        protected Reader reader;

        public EventReader(DateTime dt, Reader r) {
            dateTime = dt;
            reader = r;
        }

        public DateTime GetDateTime() {
            return dateTime;
        }

        public abstract EventType GetEventType();
    }

    public class AddReader : EventReader {

        public AddReader(DateTime dt, Reader r) : base(dt, r) {

        }

        public override EventType GetEventType() {
            return EventType.AddReader;
        }
    }
    public class UpdateReader : EventReader {

        public UpdateReader(DateTime dt, Reader r) : base(dt, r) {

        }

        public override EventType GetEventType() {
            return EventType.UpdateReader;
        }
    }
    public class DeleteReader : EventReader {

        public DeleteReader(DateTime dt, Reader r) : base(dt, r) {

        }

        public override EventType GetEventType() {
            return EventType.DeleteReader;
        }
    }
}
