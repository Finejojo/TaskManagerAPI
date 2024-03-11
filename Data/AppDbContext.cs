using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using TaskManagerAPI.Models;

namespace TaskManagerAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<TaskModel> Tasks { get; set; }
    }
}
