using System.Collections.Generic;

namespace CakesWebApp.Models
{
    public class Product : BaseModel<int>
    {
        public Product()
        {
            this.Orders = new HashSet<OrdersProducts>();
        }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public string ImageUrl { get; set; }

        public virtual ICollection<OrdersProducts> Orders { get; set; }
    }
}
