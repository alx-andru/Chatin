using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Chatin.Models;
using Chatin.DAL;


namespace Chatin.Controllers
{
    [RoutePrefix("api/user")]
    public class UserController : ApiController
    {
        
        public ChatinContext db = new ChatinContext();

        public UserController()
        {
            
        }

        [Route("")]
        public IEnumerable<User> GetAllUsers()
        {
            return db.Users.ToList();
        }

        [HttpPost]
        [Route("")]
        public IHttpActionResult Add([FromBody] User user)
        {
            db.Users.Add(user);
            db.SaveChanges();
            return Ok(db.Users.ToList());
        }

        [Route("{id:int}")]
        public IHttpActionResult GetUser(int id)
        {
            var user = db.Users.FirstOrDefault((usr) => usr.Id == id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpDelete]
        [Route("{id:int}")]
        public IHttpActionResult Delete(int id)
        {
            var user = db.Users.FirstOrDefault((usr) => usr.Id == id);
            if (user == null)
            {
                return NotFound();
            }
            db.Users.Remove(user);
            db.SaveChanges();
            return Ok(); 
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }



    }
}