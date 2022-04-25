using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LAB.Models
{
    public class LABAppContext : DbContext
    {
        public DbSet<Budget> Budgets { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<FinishedProducts> FinishedProducts { get; set; }
        public DbSet<Ingredients> Ingredients { get; set; }
        public DbSet<Measurement> Measurements { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<Production> Productions { get; set; }
        public DbSet<PurchaseRaw> PurchaseRaws { get; set; }
        public DbSet<Raw> Raws { get; set; }
        public DbSet<Sell> Sells { get; set; }
        public DbSet<Salary> Salaries { get; set; }
        public LABAppContext(DbContextOptions<LABAppContext> options)

            : base(options)
        {
        }
    }
}
