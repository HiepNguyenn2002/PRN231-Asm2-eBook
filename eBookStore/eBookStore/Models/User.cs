using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace eBookStore.Models
{
    public partial class User
    {
        public int UserId { get; set; }
        [Required(ErrorMessage = "Email is required")]
        public string EmailAddress { get; set; } = null!;
        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;
        public string? Source { get; set; }
        public string? FullName { get; set; }
        public int? RoleId { get; set; }
        public DateTime? HireDate { get; set; }
        public int? PubId { get; set; }

      
    }
}
