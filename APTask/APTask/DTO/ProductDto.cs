using System;

namespace APTask.DTO
{
    public class ProductDto
    {
        public string ProductName { get; set; }
        public string Description { get; set; }
        public string ProductCode { get; set; }
        public Guid CategoryId { get; set; }
        public bool IsActive { get; set; }
    }
}
