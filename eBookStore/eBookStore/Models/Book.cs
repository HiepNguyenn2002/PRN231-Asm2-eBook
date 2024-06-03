using System;
using System.Collections.Generic;

namespace eBookStore.Models
{
    public partial class Book
    {
     

        public int BookId { get; set; }
        public string? BookName { get; set; }
        public string? Type { get; set; }
        public int? PubId { get; set; }
        public decimal? Price { get; set; }
        public decimal? Advance { get; set; }
        public decimal? Royalty { get; set; }
        public int? YtdSales { get; set; }
        public string? Notes { get; set; }
        public DateTime? PublisherDate { get; set; }
        public string? PubName { get; set; }


    }
}
