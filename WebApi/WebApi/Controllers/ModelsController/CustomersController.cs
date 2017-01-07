using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Models;
using WebApi.Models;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace WebApi.Controllers.ModelsController
{
    public class CustomersController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Customers
        [Authorize]
        public IQueryable<Customer> GetCustomers()
        {
            return db.Customers;
        }

        // GET: api/Customers/5
        [Authorize]
        [ResponseType(typeof(Customer))]
        public IHttpActionResult GetCustomer(int id)
        {
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return BadRequest("Le client recherché avec cet ID n'existe pas");
            }

            return Ok(customer);
        }

        // PUT: api/Customers/5
        [Authorize]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCustomer(int id, Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != customer.CustomerID)
            {
                return BadRequest("L'ID dans le chemin n'est pas le même que dans les données que vous donnez");
            }

            try
            {
                MailAddress m = new MailAddress(customer.Email);
            }
            catch
            {
                return BadRequest("l'email n'est pas valide");
            }

            Regex regexFirstName = new Regex(@"^[A-Z]{1}[a-z]{1,30}([-]{0,1}[A-Z]{1}[a-z]{1,20})?$");
            if (customer.FirstName.Length < 4 || customer.FirstName.Length > 30 || !regexFirstName.IsMatch(customer.FirstName))
            {
                return BadRequest("Le prénom n'est pas valide");
            }



            Regex regexName = new Regex(@"^([a-zA-z]{1,}'?-?[a-zA-Z]{2,}\s?([a-zA-Z]{1,})?)");
            if (customer.Name.Length < 4 || !regexName.IsMatch(customer.Name))
            {
                return BadRequest("Le nom n'est pas valide");
            }

            if (customer.PhoneNumber != null)
            {
                Regex regexPhoneNumber = new Regex(@"^[0]{1}[0-9]{9}$");
                if (!regexPhoneNumber.IsMatch(customer.PhoneNumber))
                {
                    return BadRequest("Le numéro de téléphone n'est pas valide");
                }

            }
            if (customer.CodePostal <= 1000 || customer.CodePostal >= 9999)
            {
                return BadRequest("le code postal n'est pas valide");
            }

            db.Entry(customer).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerExists(id))
                {
                    return BadRequest("Le client n'existe pas (Mauvais ID)");
                }
                else
                {
                    return BadRequest(("Il y a un problème de concurrence"));
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Customers
        [Authorize]
        [ResponseType(typeof(Customer))]
        public IHttpActionResult PostCustomer(Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                MailAddress m = new MailAddress(customer.Email);
            }
            catch
            {
                return BadRequest("l'email n'est pas valide");
            }

            Regex regexFirstName = new Regex(@"^[A-Z]{1}[a-z]{1,30}([-]{0,1}[A-Z]{1}[a-z]{1,20})?$");
            if (customer.FirstName.Length < 4 || customer.FirstName.Length > 30 || !regexFirstName.IsMatch(customer.FirstName))
            {
                return BadRequest("Le prénom n'est pas valide");
            }



            Regex regexName = new Regex(@"^([a-zA-z]{1,}'?-?[a-zA-Z]{2,}\s?([a-zA-Z]{1,})?)");
            if (customer.Name.Length < 4 || !regexName.IsMatch(customer.Name))
            {
                return BadRequest("Le nom n'est pas valide");
            }

            if (customer.PhoneNumber != null)
            {
                Regex regexPhoneNumber = new Regex(@"^[0]{1}[0-9]{9}$");
                if (!regexPhoneNumber.IsMatch(customer.PhoneNumber))
                {
                    return BadRequest("Le numéro de téléphone n'est pas valide");
                }

            }
            if (customer.CodePostal <= 1000 || customer.CodePostal >= 9999)
            {
                return BadRequest("le code postal n'est pas valide");
            }

            db.Customers.Add(customer);
            try
            {
                db.SaveChanges();
            }
            catch
            {
                return BadRequest("Il y a un problème avec la base de donnée");
            }

            return CreatedAtRoute("DefaultApi", new { id = customer.CustomerID }, customer);
        }

        // DELETE: api/Customers/5
        [Authorize]
        [ResponseType(typeof(Customer))]
        public IHttpActionResult DeleteCustomer(int id)
        {
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return NotFound();
            }

            db.Customers.Remove(customer);
            try
            {
                db.SaveChanges();
            }
            catch
            {
                return BadRequest("Il y a un problème avec la base de donnée");
            }

            return Ok(customer);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CustomerExists(int id)
        {
            return db.Customers.Count(e => e.CustomerID == id) > 0;
        }
    }
}