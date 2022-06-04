using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace APBD_EF_Example.Entities.Configurations;

public class ActionEfConfiguration : IEntityTypeConfiguration<Action>
{
    public void Configure(EntityTypeBuilder<Action> builder)
    {
        builder.HasKey(e => e.IdAction).HasName("Action_pk");
        builder.Property(e => e.IdAction).UseIdentityColumn();

        builder.Property(e => e.StartTime).IsRequired();
        builder.Property(e => e.NeedSpecialEquipment).IsRequired();

        builder.ToTable("Action");

        builder.HasData(new Action()
        {
            IdAction = 1,
            StartTime = DateTime.Today,
            EndTime = DateTime.Today.AddDays(10),
            NeedSpecialEquipment = false
        });
        
        builder.HasData(new Action()
        {
            IdAction = 2,
            StartTime = DateTime.Today,
            EndTime = DateTime.Today.AddDays(10),
            NeedSpecialEquipment = true
        });
        
        builder.HasData(new Action()
        {
            IdAction = 3,
            StartTime = DateTime.Today,
            EndTime = DateTime.Today.AddDays(10),
            NeedSpecialEquipment = true
        });


    }
}