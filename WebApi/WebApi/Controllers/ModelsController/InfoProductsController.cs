﻿using System;
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

namespace WebApi.Controllers.ModelsController
{
    public class InfoProductsController : ApiController
    {
        private JavaContext db = new JavaContext();

        // GET: api/InfoProducts
        public IQueryable<InfoProduct> GetInfoProducts()
        {
            return db.InfoProducts;
        }

        // GET: api/InfoProducts/5
        [ResponseType(typeof(InfoProduct))]
        public IHttpActionResult GetInfoProduct(int id)
        {
            InfoProduct infoProduct = db.InfoProducts.Find(id);
            if (infoProduct == null)
            {
                return NotFound();
            }

            return Ok(infoProduct);
        }

        // PUT: api/InfoProducts/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutInfoProduct(int id, InfoProduct infoProduct)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != infoProduct.InfoProductID)
            {
                return BadRequest();
            }

            db.Entry(infoProduct).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InfoProductExists(id))
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

        // POST: api/InfoProducts
        [ResponseType(typeof(InfoProduct))]
        public IHttpActionResult PostInfoProduct(InfoProduct infoProduct)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.InfoProducts.Add(infoProduct);
            try
            {
                db.SaveChanges();
            }
            catch
            {
                return BadRequest("Il y a un problème avec la base de donnée");
            }

            return CreatedAtRoute("DefaultApi", new { id = infoProduct.InfoProductID }, infoProduct);
        }

        // DELETE: api/InfoProducts/5
        [ResponseType(typeof(InfoProduct))]
        public IHttpActionResult DeleteInfoProduct(int id)
        {
            InfoProduct infoProduct = db.InfoProducts.Find(id);
            if (infoProduct == null)
            {
                return NotFound();
            }

            db.InfoProducts.Remove(infoProduct);
            try
            {
                db.SaveChanges();
            }
            catch
            {
                return BadRequest("Il y a un problème avec la base de donnée");
            }

            return Ok(infoProduct);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool InfoProductExists(int id)
        {
            return db.InfoProducts.Count(e => e.InfoProductID == id) > 0;
        }
    }
}