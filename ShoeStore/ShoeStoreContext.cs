using Microsoft.EntityFrameworkCore;
using ShoeStore.Models;
using System.Collections.Generic;

namespace ShoeStore.Data
{
    public class ShoeStoreContext : DbContext
    {
        public ShoeStoreContext(DbContextOptions<ShoeStoreContext> options) : base(options)
        {
        }

        public DbSet<Shoe> Shoes { get; set; }
    }
}
