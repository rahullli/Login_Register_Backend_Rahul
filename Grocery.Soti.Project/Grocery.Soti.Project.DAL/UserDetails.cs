﻿using Grocery.Soti.Project.DAL.Interfaces;
using Grocery.Soti.Project.DAL.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grocery.Soti.Project.DAL
{
    public class UserDetails : IAccount
    {
        public Task<User> ValidateUserAsync(string emailId, string password)
        {
            return Task.Run(() =>
            {
                using (SqlConnection connection = new SqlConnection(SqlConnectionStrings.GetConnectionString))
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter("Select * from Users", connection))
                    {
                        using (DataSet ds = new DataSet())
                        {
                            adapter.Fill(ds, "Users");
                            return ds.Tables[0].AsEnumerable()
                                .Select(u => new User
                                {
                                    FirstName = u.Field<string>("FirstName"),
                                    LastName = u.Field<string>("LastName"),
                                    EmailId = u.Field<string>("EmailId"),
                                    Password = u.Field<string>("Password"),
                                    MobileNumber = u.Field<string>("MobileNumber")

                                })
                                .FirstOrDefault(x => x.EmailId == emailId && x.Password == password);
                        }
                    }
                }
            });
        }
    }
}
