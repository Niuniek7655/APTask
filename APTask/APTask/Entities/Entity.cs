using System;

namespace APTask.Entities
{
    public abstract class Entity
    {
        public Guid Id { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? ModificationDate { get; set; }
        public Guid CreatingUserId { get; set; }
        public Guid? ModifyingUserId { get; set; }
    }
}
