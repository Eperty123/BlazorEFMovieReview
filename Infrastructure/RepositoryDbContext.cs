using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public class RepositoryDbContext : Microsoft.EntityFrameworkCore.DbContext
{

    public RepositoryDbContext(DbContextOptions<RepositoryDbContext> options, ServiceLifetime serviceLifetime) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // TODO: Make relations.
        //modelBuilder.Entity<Review>()
        //    .HasOne(ol => ol.Movie)
        //    .HasForeignKey(ol => ol.TournamentId);
    }

    public DbSet<Movie> MovieTable { get; set; }
    public DbSet<Review> ReviewTable { get; set; }
}