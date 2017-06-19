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
using WebServerAndClient;
using WebServerAndClient.Models;

namespace WebServerAndClient.Controllers
{
    public class ClientController : ApiController
    {
        private WebServerAndClientContext db = new WebServerAndClientContext();

        // GET: api/Client
        public IQueryable<ClientModel> GetClientModels()
        {
            return db.ClientModels;
        }

        // GET: api/Client/5
        [ResponseType(typeof(ClientModel))]
        public IHttpActionResult GetClientModel(int id)
        {
            ClientModel clientModel = db.ClientModels.Find(id);
            if (clientModel == null)
            {
                return NotFound();
            }

            return Ok(clientModel);
        }

        // PUT: api/Client/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutClientModel(int id, ClientModel clientModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != clientModel.B)
            {
                return BadRequest();
            }

            db.Entry(clientModel).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClientModelExists(id))
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

        // POST: api/Client
        [ResponseType(typeof(ClientModel))]
        public IHttpActionResult PostClientModel(ClientModel clientModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ClientModels.Add(clientModel);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = clientModel.B }, clientModel);
        }

        // DELETE: api/Client/5
        [ResponseType(typeof(ClientModel))]
        public IHttpActionResult DeleteClientModel(int id)
        {
            ClientModel clientModel = db.ClientModels.Find(id);
            if (clientModel == null)
            {
                return NotFound();
            }

            db.ClientModels.Remove(clientModel);
            db.SaveChanges();

            return Ok(clientModel);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ClientModelExists(int id)
        {
            return db.ClientModels.Count(e => e.B == id) > 0;
        }
    }
}