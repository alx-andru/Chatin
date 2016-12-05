using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Chatin.Models;
using Chatin.DAL;
using System.Diagnostics;

namespace Chatin.Controllers
{
    [RoutePrefix("api/room")]
    public class RoomController : ApiController
    {

        public ChatinContext db = new ChatinContext();

        public RoomController()
        {

        }
        [Route("")]
        public IEnumerable<Room> GetAllRooms()
        {
            return db.Rooms.ToList();
        }

        [HttpPost]
        [Route("")]
        public IHttpActionResult Add([FromBody] Room room)
        {
            db.Rooms.Add(room);
            return Ok(db.Rooms.ToList());
        }

        [Route("{id:int}")]
        public IHttpActionResult GetRoom(int id)
        {

            var room = db.Rooms.FirstOrDefault((rm) => rm.Id == id);
            if (room == null)
            {
                return NotFound();
            }
            return Ok(room);
        }

        [Route("{id:int}/messages")]
        public IHttpActionResult GetRoomMessages(int id)
        {
            Debug.WriteLine(id);
            var room = db.Rooms.FirstOrDefault((rm) => rm.Id == id);
            if (room == null)
            {
                return NotFound();
            }

            return Ok(room.Messages.ToList());
        }

        [HttpPost]
        [Route("{id:int}/messages")]
        public IHttpActionResult AddRoomMessages(int id, [FromBody] Message message)
        {
            Debug.WriteLine(id);
            var room = db.Rooms.FirstOrDefault((rm) => rm.Id == id);
            if (room == null)
            {
                return NotFound();
            }
            message.Timestamp = DateTime.Now;
            room.Messages.Add(message);
            db.SaveChanges();

            return Ok();
        }


        [Route("{id:int}/users")]
        public IHttpActionResult GetRoomUser(int id)
        {
            Debug.WriteLine(id);
            var room = db.Rooms.FirstOrDefault((rm) => rm.Id == id);
            if (room == null)
            {
                return NotFound();
            }
            
            return Ok(room.Users.ToList());
        }

        

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}