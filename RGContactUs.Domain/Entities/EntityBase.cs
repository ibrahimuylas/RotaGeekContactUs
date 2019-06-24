using System;
using RGContactUs.Toolkit;

namespace RGContactUs.Domain.Entities
{
    public abstract class EntityBase
    {
        public int Id { get; set; }
        public Nullable<DateTime> ModifiedDate { get; set; }
        public Nullable<int> ModifierUserId { get; set; }
        public Nullable<DateTime> CanceledDate { get; set; }
        public Nullable<int> CancelUserId { get; set; }
        public EntityStatus State { get; set; }
    }
}
