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

namespace WebApi.Controllers.ModelsController
{
    public class ProductsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Products
        [Authorize]
        public IQueryable<Product> GetProducts()
        {
            return db.Products.Include(b => b.Category);
        }

        // GET: api/Products/5
        [Authorize]
        [ResponseType(typeof(Product))]
        public IHttpActionResult GetProduct(int id)
        {
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        // PUT: api/Products/5
        [Authorize]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutProduct(int id, Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != product.ProductID)
            {
                return BadRequest();
            }

            db.Entry(product).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Products
        [Authorize]
        [ResponseType(typeof(Product))]
        public IHttpActionResult PostProduct(Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (product.UnitPrice < 0)
            {
                return BadRequest("Le prix doit être plus grand que 0");
            }
            db.Products.Add(product);
            try
            {
                db.SaveChanges();
            }
            catch
            {
                return BadRequest("Il y a un problème avec la base de donnée");
            }

            return CreatedAtRoute("DefaultApi", new { id = product.ProductID }, product);
        }

        // DELETE: api/Products/5
        [Authorize]
        [ResponseType(typeof(Product))]
        public IHttpActionResult DeleteProduct(int id)
        {
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return NotFound();
            }

            db.Products.Remove(product);
            try
            {
                db.SaveChanges();
            }
            catch
            {
                return BadRequest("Il y a un problème avec la base de donnée");
            }

            return Ok(product);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProductExists(int id)
        {
            return db.Products.Count(e => e.ProductID == id) > 0;
        }
    }
}