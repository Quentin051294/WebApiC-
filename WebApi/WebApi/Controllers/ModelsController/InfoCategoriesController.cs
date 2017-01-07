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

namespace WebApi.Controllers.ModelsController
{
    public class InfoCategoriesController : ApiController
    {
        private JavaContext db = new JavaContext();

        // GET: api/InfoCategories
        public IQueryable<InfoCategory> GetInfoCategories()
        {
            return db.InfoCategories.Include(b => b.Category).Include(b => b.Language);
        }

        // GET: api/InfoCategories/5
        [ResponseType(typeof(InfoCategory))]
        public IHttpActionResult GetInfoCategory(int id)
        {
            InfoCategory infoCategory = db.InfoCategories.Find(id);
            if (infoCategory == null)
            {
                return NotFound();
            }

            return Ok(infoCategory);
        }

        // PUT: api/InfoCategories/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutInfoCategory(int id, InfoCategory infoCategory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != infoCategory.InfoCategoryID)
            {
                return BadRequest();
            }

            db.Entry(infoCategory).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InfoCategoryExists(id))
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

        // POST: api/InfoCategories
        [ResponseType(typeof(InfoCategory))]
        public IHttpActionResult PostInfoCategory(InfoCategory infoCategory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.InfoCategories.Add(infoCategory);
            try
            {
                db.SaveChanges();
            }
            catch
            {
                return BadRequest("Il y a un problème avec la base de donnée");
            }

            return CreatedAtRoute("DefaultApi", new { id = infoCategory.InfoCategoryID }, infoCategory);
        }

        // DELETE: api/InfoCategories/5
        [ResponseType(typeof(InfoCategory))]
        public IHttpActionResult DeleteInfoCategory(int id)
        {
            InfoCategory infoCategory = db.InfoCategories.Find(id);
            if (infoCategory == null)
            {
                return NotFound();
            }

            db.InfoCategories.Remove(infoCategory);
            try
            {
                db.SaveChanges();
            }
            catch
            {
                return BadRequest("Il y a un problème avec la base de donnée");
            }

            return Ok(infoCategory);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool InfoCategoryExists(int id)
        {
            return db.InfoCategories.Count(e => e.InfoCategoryID == id) > 0;
        }
    }
}