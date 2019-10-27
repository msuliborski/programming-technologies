using System;
using System.Collections.Generic;
using System.Text;

namespace Data {
    public abstract class EventCatalog : IEvent {
        protected DateTime dateTime;
        protected Catalog catalog;

        public EventCatalog(DateTime dt, Catalog c) {
            dateTime = dt;
            catalog = c;
        }

        public DateTime GetDateTime() {
            return dateTime;
        }

        public abstract EventType GetEventType();
    }

    public class AddCatalog : EventCatalog {

        public AddCatalog(DateTime dt, Catalog c) : base(dt, c) {

        }

        public override EventType GetEventType() {
            return EventType.AddCatalog;
        }
    }

    public class UpdateCatolog : EventCatalog {

        public UpdateCatolog(DateTime dt, Catalog c) : base(dt, c) {

        }

        public override EventType GetEventType() {
            return EventType.UpdateCatalog;
        }
    }

    public class DeleteCatalog : EventCatalog {

        public DeleteCatalog(DateTime dt, Catalog c) : base(dt, c) {

        }

        public override EventType GetEventType() {
            return EventType.DeleteCatalog;
        }
    }
}
