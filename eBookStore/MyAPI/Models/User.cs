using System;
using System.Collections.Generic;

namespace MyAPI.Models
{
    public partial class User
    {
        public int UserId { get; set; }
        public string EmailAddress { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string? Source { get; set; }
        public string? FullName { get; set; }
        public int? RoleId { get; set; }
        public DateTime? HireDate { get; set; }
        public int? PubId { get; set; }

        public virtual Publisher? Pub { get; set; }
        public virtual Role? Role { get; set; }
    }
}
