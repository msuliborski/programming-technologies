using System;
using System.Collections.Generic;
using System.Text;

namespace Data {
    public enum EventType {
        AddCatalog, DeleteCatalog,
        AddReader, DeleteReader
    }
    public interface IEvent {
        EventType GetEventType();
        DateTime GetDateTime();
    }
}
