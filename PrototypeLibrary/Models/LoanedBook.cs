using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PrototypeLibrary.Models
{
    public class LoanedBook
    {
        public int Id { get; set; }
        [Display(Name = "First Name")]
        public string CusFirstName { get; set;}

        [Display(Name = "Last Name")]
        public string CusLastName { get; set; }

        [Display(Name = "Book Title")]
        public string BookTitle { get; set; }

        [Display(Name = "Date Loaned")]
        public DateTime Date { get; set; }
        public List<bool> isLoaned { get; set; }

        
    }
}
