using Microsoft.EntityFrameworkCore;

namespace RestaurantRaterMVC.Data
{
    public class RestaurantDbContext : DbContext
    {   
        public RestaurantDbContext(DbContextOptions<RestaurantDbContext> options) : base (options) { }
        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<Rating> Rating { get; set; }
    }
}