﻿using Microsoft.EntityFrameworkCore;
using WishList.Models;

namespace WishList.ApplicationDbContext;

public class WishDbContext : DbContext
{
    public WishDbContext(DbContextOptions<WishDbContext> options) : base(options) { }

    public DbSet<Wish> Wishes { get; set; }
}