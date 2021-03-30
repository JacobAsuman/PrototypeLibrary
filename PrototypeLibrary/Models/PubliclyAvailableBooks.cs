using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PrototypeLibrary.Models
{
    public class PubliclyAvailableBooks
    {

        public int Id { get; set; }

        [Display(Name = "Book ID")]
        public int PublicBookID { get; set; }

        [Display(Name = "Title")]
        public string PublicBookTitle { get; set; }

        [Display(Name = "Author")]
        public string PublicAuthor { get; set; }

        [Display(Name = "Genre")]
        public string PublicGenre { get; set; }

        [Display(Name = "Price")]
        public float PublicPrice { get; set; }

    }
}
