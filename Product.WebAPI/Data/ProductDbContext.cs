﻿using Microsoft.EntityFrameworkCore;
using Shared.Entities;

namespace ProductWebAPI.Data;

public class ProductDbContext(DbContextOptions<ProductDbContext> options)
    : DbContext(options)
{
    public DbSet<Product> Products { get; set; }
}
