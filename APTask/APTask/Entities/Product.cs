using System;

namespace APTask.Entities
{
    public class Product : Entity
    {
        public string ProductName { get; set; }
        public string Description { get; set; }
        public string ProductCode { get; set; }
        public Guid CategoryId { get; set; }
        public Category Category { get; set; }
        public bool IsActive { get; set; }
    }
}
