using Microsoft.EntityFrameworkCore;
using NLayerArchitecture.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace NLayerArchitecture.Infrastructure.Data
{
    public class NLayerArchitectureDbContext:DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        

        public NLayerArchitectureDbContext(DbContextOptions<NLayerArchitectureDbContext> options)
      : base(options)
        { }
    }
}
