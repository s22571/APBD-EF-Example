using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace APBD_EF_Example.Entities.Configurations;

public class FireTruckActionEfConfiguration : IEntityTypeConfiguration<FireTruckAction>
{
    public void Configure(EntityTypeBuilder<FireTruckAction> builder)
    {
        builder.HasKey(e =>
            new {e.IdFiretruck, e.IdAction}).HasName("FiretruckAction_pk");

        builder.Property(e => e.AssignmentDate).IsRequired();
        
        builder.HasOne(e => e.IdFireTruckNavigation)
            .WithMany(e => e.FireTruckActions)
            .HasForeignKey(e => e.IdFiretruck)
            .HasConstraintName("FiretruckAction_Firetruck")
            .OnDelete(DeleteBehavior.ClientSetNull);
        
        builder.HasOne(e => e.IdActionNavigation)
            .WithMany(e => e.FireTruckActions)
            .HasForeignKey(e => e.IdAction)
            .HasConstraintName("FiretruckAction_Action")
            .OnDelete(DeleteBehavior.ClientSetNull);

        builder.ToTable("Firetruck_Action");
        
        builder.HasData(new FireTruckAction()
        {
            IdFiretruck = 6,
            IdAction = 1,
            AssignmentDate = DateTime.Now
        });
        
        builder.HasData(new FireTruckAction()
        {
            IdFiretruck = 2,
            IdAction = 2,
            AssignmentDate = DateTime.Now
        });
    }
}