using Microsoft.EntityFrameworkCore;

namespace DatabaseAssignment;

internal class AppDbContext : DbContext {
	public DbSet<Student> Students { get; set; }
	private const string CONNECTION_STRING = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=DatabaseAssignment;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
		optionsBuilder.UseSqlServer(CONNECTION_STRING);
	}
}
