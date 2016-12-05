using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Chatin.Models;
using Chatin.DAL;

namespace Chatin.Controllers
{
    [RoutePrefix("api/message")]
    public class MessageController : ApiController
    {
        public ChatinContext db = new ChatinContext();
     

        public MessageController()
        {
           
        }
        [Route("")]
        public IEnumerable<Message> GetAllMessages()
        {
            return db.Messages.ToList();
        }
        [HttpPost]
        [Route("")]
        public IHttpActionResult Add([FromBody] Message message)
        {
            message.Timestamp = DateTime.Now;
            db.Messages.Add(message);
            db.SaveChanges();
            return Ok(db.Messages.ToList());
        }

        [Route("{id:int}")]
        public IHttpActionResult GetMessage(int id)
        {
            var message = db.Messages.FirstOrDefault((msg) => msg.Id == id);
            if (message == null)
            {
                return NotFound();
            }
            return Ok(message);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }




    }
}