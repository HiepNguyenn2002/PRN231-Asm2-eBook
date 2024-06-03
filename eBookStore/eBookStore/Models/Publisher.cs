using System;
using System.Collections.Generic;

namespace eBookStore.Models
{
    public partial class Publisher
    {
       

        public int PubId { get; set; }
        public string? PublisherName { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? Country { get; set; }

     
    }
}
