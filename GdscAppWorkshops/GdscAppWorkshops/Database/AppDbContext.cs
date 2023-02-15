using App_GDSC_workshops.Features.Assignments.Models;
using App_GDSC_workshops.Features.Subjects.Models;
using App_GDSC_workshops.Features.Tests.Models;
using Microsoft.EntityFrameworkCore;

namespace App_GDSC_workshops.Database;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions options) : base(options) { }

    public DbSet<AssignmentModel> Assignments { get; set; } = null!;

    public DbSet<SubjectModel> Subjects { get; set; } = null!;

    public DbSet<TestModel> Tests { get; set; } = null!;
}