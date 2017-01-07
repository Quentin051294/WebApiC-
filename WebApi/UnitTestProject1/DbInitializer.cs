using Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestProject1
{
    class DbInitializer : DropCreateDatabaseAlways<JavaContext>
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

            Command com = new Command()
            {
                CommandID = 1,
                CustomerId = 1
            };

            context.Commands.Add(com);
            context.SaveChanges();

            Category cat = new Category()
            {
                CategoryID = 1,
                Promotion = 10
            };

            context.Categories.Add(cat);
            context.SaveChanges();

            Language lang = new Language()
            {
                LanguageID = 1,
                Name = "Français"
            };

            context.Languages.Add(lang);
            context.SaveChanges();

            InfoCategory infoCat = new InfoCategory()
            {
                InfoCategoryID = 1,
                CategoryId = 1,
                LanguageId = 1,
                CategoryName = "DVD",
            };

            context.InfoCategories.Add(infoCat);
            context.SaveChanges();

            Product prod = new Product()
            {
                ProductID = 1,
                CategoryId = 1,
                UnitPrice = 1.5
            };

            context.Products.Add(prod);
            context.SaveChanges();

            InfoProduct infoProd = new InfoProduct()
            {
                InfoProductID = 1,
                Description = "Dvd de la série Arrow, saison 1",
                Label = "Arrow",
                ProductId = 1,
                LanguageId = 1
            };

            context.InfoProducts.Add(infoProd);
            context.SaveChanges();

            CommandLine comLine = new CommandLine()
            {
                CommandLineID = 1,
                CommandId = 1,
                ProductId = 1,
                Quantity = 3,
                RealPrice = 1.3
            };

            context.CommandLines.Add(comLine);
            context.SaveChanges();

        }
    }
}
