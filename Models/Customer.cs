using System;

namespace GreenLifeStore.Models
{
    internal class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool ActiveStatus { get; set; }
    }
}
