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
    public class CommandLinesController : ApiController
    {
        private JavaContext db = new JavaContext();

        // GET: api/CommandLines
        public IQueryable<CommandLine> GetCommandLines()
        {
            return db.CommandLines.Include(b => b.Command).Include(b => b.Product);
        }

        // GET: api/CommandLines/5
        [ResponseType(typeof(CommandLine))]
        public IHttpActionResult GetCommandLine(int id)
        {
            CommandLine commandLine = db.CommandLines.Find(id);
            if (commandLine == null)
            {
                return NotFound();
            }

            return Ok(commandLine);
        }

        // PUT: api/CommandLines/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCommandLine(int id, CommandLine commandLine)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != commandLine.CommandLineID)
            {
                return BadRequest();
            }

            db.Entry(commandLine).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CommandLineExists(id))
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

        // POST: api/CommandLines
        [ResponseType(typeof(CommandLine))]
        public IHttpActionResult PostCommandLine(CommandLine commandLine)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if(commandLine.Quantity <= 0)
            {
                return BadRequest("La quantité doit être plus grande que 0");
            }

            if(commandLine.RealPrice <0)
            {
                return BadRequest("Le prix doit être plus grand que 0");
            }
            db.CommandLines.Add(commandLine);
            try
            {
                db.SaveChanges();
            }
            catch
            {
                return BadRequest("Il y a un problème avec la base de donnée");
            }

            return CreatedAtRoute("DefaultApi", new { id = commandLine.CommandLineID }, commandLine);
        }

        // DELETE: api/CommandLines/5
        [ResponseType(typeof(CommandLine))]
        public IHttpActionResult DeleteCommandLine(int id)
        {
            CommandLine commandLine = db.CommandLines.Find(id);
            if (commandLine == null)
            {
                return NotFound();
            }

            db.CommandLines.Remove(commandLine);
            try
            {
                db.SaveChanges();
            }
            catch
            {
                return BadRequest("Il y a un problème avec la base de donnée");
            }

            return Ok(commandLine);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CommandLineExists(int id)
        {
            return db.CommandLines.Count(e => e.CommandLineID == id) > 0;
        }
    }
}