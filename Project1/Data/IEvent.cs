using System;
using System.Collections.Generic;
using System.Text;

namespace Data {
    public enum EventType {
        AddCatalog, UpdateCatalog, DeleteCatalog,
        AddReader, UpdateReader, DeleteReader,
        AddBook, RentBook, DeleteBook, ReturnBook
    }
    public interface IEvent {
        EventType GetEventType();
        DateTime GetDateTime();
    }
}




