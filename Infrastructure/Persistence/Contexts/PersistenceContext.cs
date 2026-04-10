using Domain.Entities;
using GymPortal.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Contexts;

public class PersistenceContext : IdentityDbContext<ApplicationUser>
{
    public PersistenceContext(DbContextOptions<PersistenceContext> options)
        : base(options) { }

    public DbSet<GymClass> GymClasses => Set<GymClass>();
    public DbSet<Booking> Bookings => Set<Booking>();
    public DbSet<Membership> Memberships => Set<Membership>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(typeof(PersistenceContext).Assembly);
    }
}