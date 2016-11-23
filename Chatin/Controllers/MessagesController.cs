using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace Chatin.Controllers
{	
	
    public class MessagesController : ApiController
    {
		List<Message> messages = new List<Message>();

		public MessagesController() {
			int idMsg = 0;
			Message msg = new Message { Id = idMsg++, Author= "Alex",Text="Hey Miro, how are you doing?", Timestamp= new DateTime(), Room = "general"};
			messages.Add(msg);
		}

		public IEnumerable<Message> GetAllMessages()
		{
			return messages;
		}

		public IHttpActionResult GetMessage(int id)
		{
			var message = messages.FirstOrDefault((msg) => msg.Id == id);
			if (message == null)
			{
				return NotFound();
			}
			return Ok(message);
		}

		[HttpPost]
		public IHttpActionResult Add([FromBody] Message message)
		{

			messages.Add(message);
			return Ok(messages);
		}
    }
}