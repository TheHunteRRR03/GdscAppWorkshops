using App_GDSC_workshops.Features.Assignments.Models;
using Microsoft.EntityFrameworkCore;

namespace App_GDSC_workshops.Database;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions options) : base(options) { }

    public DbSet<AssignmentModel> Assignments { get; set; }
}