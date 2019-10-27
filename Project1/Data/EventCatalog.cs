using System;

namespace Data {
    public abstract class EventCatalog : IEvent {
        protected DateTime dateTime;
        public Catalog Catalog { get; set; }

        public EventCatalog(DateTime dt, Catalog c) {
            dateTime = dt;
            Catalog = c;
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

    public class UpdateCatalog : EventCatalog {

        public UpdateCatalog(DateTime dt, Catalog c) : base(dt, c) {

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
