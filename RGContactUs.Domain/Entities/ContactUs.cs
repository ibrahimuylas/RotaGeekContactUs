using System;
namespace RGContactUs.Domain.Entities
{
    public class ContactUs : EntityBase
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Message { get; set; }
    }
}
