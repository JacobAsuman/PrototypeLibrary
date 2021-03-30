using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace PrototypeLibrary.Pages.Details
{
    public class BankCardModel : PageModel
    {
        public int CardNumber { get; set; }
        public DateTime ExpDate { get; set; }
        public int CVV { get; set; }
        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            int CardNumLength = CardNumber.ToString().Length;
            int ExpDateLength = ExpDate.ToString().Length;
            int CVVLength = CVV.ToString().Length;
            DateTime da;
            if (CardNumLength == 16 )
            {
                if(DateTime.TryParseExact(ExpDate.ToString(), "MM/yy", null, System.Globalization.DateTimeStyles.None, out da))
                {
                    Console.WriteLine("DateTime = success");
                    if(CVVLength != 3)
                    {
                        Console.WriteLine("CVV success");
                    }
                }
                else
                {
                    Console.WriteLine("Please use the correct format");
                }
            }
            else
            {
                Console.WriteLine("The Card Number must be 16 digits");
            }

            return RedirectToPage("/Index");
        }
        
    }
}
