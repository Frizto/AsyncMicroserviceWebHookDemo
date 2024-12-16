using Microsoft.EntityFrameworkCore;
using Shared.Entities;

namespace OrderWebAPI.Data;

public class OrderDbContext(DbContextOptions<OrderDbContext> options) 
    : DbContext(options)
{
    public DbSet<Order> Orders { get; set; }
    public DbSet<Product> Products { get; set; }
}
