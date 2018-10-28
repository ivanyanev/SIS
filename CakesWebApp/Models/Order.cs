using System;
using System.Collections.Generic;

namespace CakesWebApp.Models
{
    public class Order : BaseModel<int>
    {
        public Order()
        {
            this.Products = new HashSet<OrdersProducts>();
        }
        
        public DateTime DateOfCreation { get; set; } = DateTime.UtcNow;

        public virtual ICollection<OrdersProducts> Products { get; set; }

        public int UserID { get; set; }
        public virtual User User { get; set; }
    }
}
