using System;
using System.Collections.Generic;
using System.Text;

namespace Data {
    public class AddCatalog : IEvent {

        public override EventType GetEventType() {
            return EventType.AddCatalog;
        }


        private Type type; 

        public AddCatalog() {
            this.price = price;
            this.user = user;
        }


        public void execute() {
            throw new NotImplementedException();
        }
    }
}
