using Microsoft.EntityFrameworkCore;
using MovieApi.Models;
using System.Collections.Generic;

namespace MovieApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Watchlist> WatchLists { get; set; }
        public DbSet<Watchedlist> WatchedLists { get; set; }
    }
}
