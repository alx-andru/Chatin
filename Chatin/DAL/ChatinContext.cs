
using System.Data.Entity;
using SQLite.CodeFirst;

namespace Chatin
{
	public class ChatinContext : DbContext
	{
		public ChatinContext() : base("chatin.sqlite") { }

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			var sqliteConnectionInitializer = new SqliteCreateDatabaseIfNotExists<ChatinContext>(modelBuilder);
			Database.SetInitializer(sqliteConnectionInitializer);
		}

		public DbSet<Message> Messages { get; set; }


	}
}
