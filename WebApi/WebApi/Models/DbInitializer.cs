using Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;


namespace WebApi.Models
{
    public class DbInitializer : DropCreateDatabaseAlways<JavaContext>
    {

        protected override void Seed(JavaContext context)
        {
            Customer cust = new Customer()
            {
                CustomerID = 1,
                Email = "quentin_nico@hotmail.com",
                Password = "abcd",
                FirstName = "Quentin",
                Name = "Nicolay",
                Rue = "Rue David, n°1",
                CodePostal = 5190,
                Localite = "Moustier/Sur/Sambre"

            };
            context.Customers.Add(cust);
            context.SaveChanges();
        }
    }
}