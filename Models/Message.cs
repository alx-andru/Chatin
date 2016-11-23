using System;
using System.ComponentModel.DataAnnotations;

namespace Chatin
{
	public class Message
	{
		[Key]
		public int Id { get; set; }
		public String Text { get; set; }
		public String Author { get; set; }
		public String Room { get; set; }
		public DateTime Timestamp { get; set; }
	}
}
