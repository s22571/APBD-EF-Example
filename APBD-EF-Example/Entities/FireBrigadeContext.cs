using System.Reflection;
using APBD_EF_Example.Entities.Configurations;
using Microsoft.EntityFrameworkCore;

namespace APBD_EF_Example.Entities;

public class FireBrigadeContext : DbContext
{
    public virtual DbSet<Action> Actions { get; set; }
    public virtual DbSet<FireTruckAction> FireTruckActions { get; set; }
    public virtual DbSet<FireTruck> FireTrucks { get; set; }

    public FireBrigadeContext()
    {
    }
    
    public FireBrigadeContext(DbContextOptions<FireBrigadeContext> options) : base(options){}
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(FireTruckEfConfiguration).GetTypeInfo().Assembly);
    }
}