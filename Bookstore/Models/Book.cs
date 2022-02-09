using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Bookstore.Models
{
    public class Book
    {
        [Display(Name = "ID")]
        [Range(1, int.MaxValue, ErrorMessage = "Invalid id")]
        public int BookId { get; set; }
        [Display(Name = "Author")]
        [Required(ErrorMessage = "Author cannot be empty")]
        public string AuthorName { get; set; }
        [Display(Name = "Price")]
        [Range(0, int.MaxValue, ErrorMessage = "Invalid id")]
        public int BookPrice { get; set; }
    }
}