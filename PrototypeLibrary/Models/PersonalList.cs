using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PrototypeLibrary.Models
{
    public class PersonalList
    {
        public int Id { get; set; }

        [Display(Name = "Book ID")]
        public int BookID { get; set; }

        [Display(Name = "Title")]
        public string BookTitle { get; set; }

        [Display(Name = "Author")]
        public string Author { get; set; }

        [Display(Name = "Genre")]
        public string Genre { get; set; }

        [Display(Name = "Price")]
        public float Price { get; set; }
        public string DateLoaned { get; set; }

    }
}
