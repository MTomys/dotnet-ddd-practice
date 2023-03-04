﻿using BuberDinner.Domain.Menu;
using Microsoft.EntityFrameworkCore;

namespace BuberDinner.Infrastructure.Persistence;

public class BuberDinnerDbContext : DbContext
{
    public BuberDinnerDbContext(DbContextOptions<BuberDinnerDbContext> options) : base(options)
    {
        
    }

    public DbSet<Menu> Menus { get; set; } = null!;
    

}

