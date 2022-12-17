using System.Collections.Generic;

namespace APTask.Entities
{
    public class Category : Entity
    {
        public string CategoryName { get; set; }
        public bool IsActive { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
