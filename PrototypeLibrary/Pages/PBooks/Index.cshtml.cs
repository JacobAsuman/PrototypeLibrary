using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using PrototypeLibrary.Models;

namespace PrototypeLibrary.Pages.PBooks
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public List<PersonalList> PublicBooks { get; set; }
        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; }

        [BindProperty]
        public List<bool> isLoan { get; set; }
        
        public List<PersonalList> BookToLoan { get; set; }  //!! PersonalList is actually referring to my PUBLIC list of books that will be selected so please use your public books list table in place of PersonalList

        public void OnGet()
        {
            // Connect to Database
            DBConnection dbstring = new DBConnection();
            string DBConnection = dbstring.DbString();
            SqlConnection conn = new SqlConnection(DBConnection);
            conn.Open();
            using (SqlCommand command = new SqlCommand())
            {



                command.Connection = conn;

                PublicBooks = new List<PersonalList>();
                command.CommandText = @"SELECT * FROM PersonalList ORDER BY BookTitle ASC"; //Uses all books in PersonalList table to show them to the screen
                if (!string.IsNullOrEmpty(SearchTerm))
                {
                    command.CommandText += " WHERE (BookTitle LIKE '%' + @BKTitle) OR (BookTitle LIKE @BKTitle + '%') OR (BookTitle LIKE '%' + @BKTitle + '%') ORDER BY BookTitle ASC";
                    command.Parameters.AddWithValue("@BKTitle", (SearchTerm));
                }

                
                isLoan = new List<bool> {false};


                SqlDataReader reader = command.ExecuteReader();

                PublicBooks = new List<PersonalList>();

                while (reader.Read())
                {
                    PersonalList rec = new PersonalList();
                    rec.BookID = reader.GetInt32(1);
                    Console.WriteLine(rec.BookID);
                    rec.BookTitle = reader.GetString(2);
                    rec.Author = reader.GetString(3);
                    rec.Genre = reader.GetString(4);
                    rec.Price = reader.GetFloat(5);

                    PublicBooks.Add(rec);
                }

                reader.Close();

               
            }
            //return Page();
            isLoan = new List<bool> {  };
            for (int i = 0; i<PublicBooks.Count; i++)
            {
                isLoan.Add(false);
            }
            
        }
        public IActionResult OnPost()
        {
            Console.WriteLine("OnPost");

            BookToLoan = new List<PersonalList>();
            for(int i=0; i<PublicBooks.Count; i++)
            {
                if (isLoan[i] == true)
                {
                    BookToLoan.Add(PublicBooks[i]);
                    
                }
            }


            for (int i = 0; i < BookToLoan.Count(); i++)
            {
                //Test code to make sure the correct values are retrieved, you can omit this entire for loop
                Console.WriteLine("BookID: " + BookToLoan[i].BookID); 
                Console.WriteLine(BookToLoan[i].BookTitle); 
                Console.WriteLine(BookToLoan[i].Author);
                Console.WriteLine(BookToLoan[i].Price);
            }

            DBConnection dbstring = new DBConnection();
            string DBConnection = dbstring.DbString();
            SqlConnection conn = new SqlConnection(DBConnection);
            conn.Open();
            int Username = 123; //dummy username, you'll need to use the username from the login stuff 
            
                for (int i = 0; i < BookToLoan.Count; i++)
                {
                    using (SqlCommand command = new SqlCommand())
                        {
                            command.Connection = conn;
                            command.Parameters.AddWithValue("@UserID", Username);
                            command.CommandText = @"INSERT INTO MemberBook (UsernameId, LoanedBookId, LoanedDate) VALUES (@UserID, @LBookID, @Date)";
                    
                            command.Parameters.AddWithValue("@LBookID", BookToLoan[i].BookID);
                            command.Parameters.AddWithValue("@Date", DateTime.Now.ToString());
                            command.ExecuteNonQuery();
                        }
                }


            return RedirectToPage("/Index");
        }


    }
}
