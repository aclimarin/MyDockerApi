using Microsoft.EntityFrameworkCore;

namespace MyDockerApi.Infra;

public class AppDbContext : DbContext
{
  public DbSet<Models.Task> Task { get; set; }

  public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

  // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
  // {
  //   optionsBuilder.UseMySQL("server=localhost;database=TaskDb;user=root;password=root");
  // }

  // protected override void OnModelCreating(ModelBuilder modelBuilder)
  // {
  //   base.OnModelCreating(modelBuilder);

  //   modelBuilder.Entity<Models.Task>(entity =>
  //   {
  //     entity.HasKey(e => e.Id);
  //     entity.Property(e => e.Name).IsRequired();
  //   });
  // }
}
