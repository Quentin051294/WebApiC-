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
    public class LanguagesController : ApiController
    {
        private JavaContext db = new JavaContext();

        // GET: api/Languages
        public IQueryable<Language> GetLanguages()
        {
            return db.Languages;
        }

        // GET: api/Languages/5
        [ResponseType(typeof(Language))]
        public IHttpActionResult GetLanguage(int id)
        {
            Language language = db.Languages.Find(id);
            if (language == null)
            {
                return NotFound();
            }

            return Ok(language);
        }

        // PUT: api/Languages/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutLanguage(int id, Language language)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != language.LanguageID)
            {
                return BadRequest();
            }

            db.Entry(language).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LanguageExists(id))
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

        // POST: api/Languages
        [ResponseType(typeof(Language))]
        public IHttpActionResult PostLanguage(Language language)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Languages.Add(language);
            try
            {
                db.SaveChanges();
            }
            catch
            {
                return BadRequest("Il y a un problème avec la base de donnée");
            }

            return CreatedAtRoute("DefaultApi", new { id = language.LanguageID }, language);
        }

        // DELETE: api/Languages/5
        [ResponseType(typeof(Language))]
        public IHttpActionResult DeleteLanguage(int id)
        {
            Language language = db.Languages.Find(id);
            if (language == null)
            {
                return NotFound();
            }

            db.Languages.Remove(language);
            try
            {
                db.SaveChanges();
            }
            catch
            {
                return BadRequest("Il y a un problème avec la base de donnée");
            }

            return Ok(language);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool LanguageExists(int id)
        {
            return db.Languages.Count(e => e.LanguageID == id) > 0;
        }
    }
}