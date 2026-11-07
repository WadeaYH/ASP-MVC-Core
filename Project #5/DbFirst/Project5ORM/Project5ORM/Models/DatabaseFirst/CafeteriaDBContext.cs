using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Project5ORM.Models.DatabaseFirst;

public partial class CafeteriaDBContext : DbContext
{
    public CafeteriaDBContext(DbContextOptions<CafeteriaDBContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
