using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using PrototypeLibrary.Models;

namespace PrototypeLibrary.Pages.Balance
{
    public class IndexModel : PageModel
    {
        public List<Member> Users { get; set; }
        public void OnGet()
        {


            DBConnection dbstring = new DBConnection();
            string DBConnection = dbstring.DbString();
            SqlConnection conn = new SqlConnection(DBConnection);
            conn.Open();

            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = conn;

                //SQL Query
                
                command.CommandText = @"SELECT * FROM Member WHERE Username = 123";
                Users = new List<Member>();
                SqlDataReader reader = command.ExecuteReader(); //read records
                while (reader.Read())
                {
                    Member record = new Member();
                    record.Id = reader.GetInt32(0);
                    record.Username = reader.GetString(1);
                    record.Email = reader.GetString(4);
                    record.Credit = (float)reader.GetFloat(6);
                    Users.Add(record);
                }
                reader.Close();



            }

            }
    }
}
