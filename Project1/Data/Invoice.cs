using System;
using System.Collections.Generic;
using System.Text;

namespace Data
{
    public class addCatalog : IEvent
    {
        public Catalog Catalog { get; set; }        
        
        public Invoice(int price, IUser user) {
            this.price = price;
            this.user = user;
        }

        public void execute()
        {
            throw new NotImplementedException();
        }
    }
}
