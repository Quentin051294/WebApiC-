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
    public class CommandsController : ApiController
    {
        private JavaContext db = new JavaContext();

        // GET: api/Commands
        public IQueryable<Command> GetCommands()
        {
            return db.Commands.Include(b => b.Customer);
        }

        // GET: api/Commands/5
        [ResponseType(typeof(Command))]
        public IHttpActionResult GetCommand(int id)
        {
            Command command = db.Commands.Find(id);
            if (command == null)
            {
                return NotFound();
            } 

            return Ok(command);
        }

        // PUT: api/Commands/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCommand(int id, Command command)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != command.CommandID)
            {
                return BadRequest();
            }

            db.Entry(command).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CommandExists(id))
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

        // POST: api/Commands
        [ResponseType(typeof(Command))]
        public IHttpActionResult PostCommand(Command command)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Commands.Add(command);
            try
            {
                db.SaveChanges();
            }
            catch
            {
                return BadRequest("Il y a un problème avec la base de donnée");
            }

            return CreatedAtRoute("DefaultApi", new { id = command.CommandID }, command);
        }

        // DELETE: api/Commands/5
        [ResponseType(typeof(Command))]
        public IHttpActionResult DeleteCommand(int id)
        {
            Command command = db.Commands.Find(id);
            if (command == null)
            {
                return NotFound();
            }

            db.Commands.Remove(command);
            try
            {
                db.SaveChanges();
            }
            catch
            {
                return BadRequest("Il y a un problème avec la base de donnée");
            }

            return Ok(command);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CommandExists(int id)
        {
            return db.Commands.Count(e => e.CommandID == id) > 0;
        }
    }
}