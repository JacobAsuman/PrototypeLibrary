using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using PrototypeLibrary.Models;

namespace PrototypeLibrary.Pages.Details
{
    public class MoneyModel : PageModel
    {
        public List<Member> Users { get; set; }
        [BindProperty]
        public int InputCredit { get; set; }
        [BindProperty]
        public float existingCredit { get; set; }
        public void OnGet()
        {
            DBConnection dbstring = new DBConnection();
            string DBConnection = dbstring.DbString();
            SqlConnection conn = new SqlConnection(DBConnection);
            conn.Open();
           /* using (SqlCommand command = new SqlCommand())
            {
                command.Connection = conn;

                command.CommandText = @"SELECT Credit FROM Member WHERE Username = 123";

                SqlDataReader reader = command.ExecuteReader();
                Member rec = new Member();
                while (reader.Read())
                {

                    rec.Credit = reader.GetFloat(0);
                }
                existingCredit = rec.Credit;
                Console.WriteLine(rec.Credit);
                Console.WriteLine(existingCredit);
            }
           */

        }

        public IActionResult OnPost()
        {
            DBConnection dbstring = new DBConnection();
            string DBConnection = dbstring.DbString();
            SqlConnection conn = new SqlConnection(DBConnection);
            conn.Open();
            Users = new List<Member>();

            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = conn;
                command.CommandText = @"SELECT Credit FROM Member WHERE Username = 123";

                SqlDataReader reader = command.ExecuteReader();
                Member rec = new Member();
                while (reader.Read())
                {

                    rec.Credit = reader.GetFloat(0);
                }
                existingCredit = rec.Credit;
                Console.WriteLine("recorded credit from db: " + rec.Credit);
                Console.WriteLine("existing credit (should be same as recorded): " + existingCredit);


                Users = new List<Member>();
                Console.WriteLine("existing credit:" + existingCredit);
                Console.WriteLine("Input Credit: " + InputCredit);
                if (InputCredit > 0)
                {
                    Console.WriteLine("Input credit: " + InputCredit);
                    existingCredit += InputCredit;
                    Console.WriteLine("Input credit + existing credit: " + existingCredit);
                    
                    command.Parameters.AddWithValue("@Credit", (existingCredit));
                    command.CommandText = @"UPDATE Member SET (@Credit = Credit) WHERE Username = 123";
                }

            }

            return RedirectToPage("/Index");
        }
    }
}

        

