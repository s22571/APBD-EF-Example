using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace APBD_EF_Example.Entities.Configurations;

public class FireTruckEfConfiguration : IEntityTypeConfiguration<FireTruck>
{
    public void Configure(EntityTypeBuilder<FireTruck> builder)
    {
        builder.HasKey(e => e.IdFiretruck).HasName("FireTruck_pk");
        builder.Property(e => e.IdFiretruck).UseIdentityColumn();

        builder.Property(e => e.OperationNumber).IsRequired().HasMaxLength(10);
        builder.Property(e => e.SpecialEquipment).IsRequired();

        builder.ToTable("FireTruck");

        builder.HasData(new FireTruck()
        {
            IdFiretruck = 1,
            OperationNumber = "1",
            SpecialEquipment = true
        });
        builder.HasData(new FireTruck()
        {
            IdFiretruck = 2,
            OperationNumber = "2",
            SpecialEquipment = true
        });
        builder.HasData(new FireTruck()
        {
            IdFiretruck = 3,
            OperationNumber = "3",
            SpecialEquipment = true
        });
        builder.HasData(new FireTruck()
        {
            IdFiretruck = 4,
            OperationNumber = "4",
            SpecialEquipment = true
        });
        builder.HasData(new FireTruck()
        {
            IdFiretruck = 5,
            OperationNumber = "5",
            SpecialEquipment = false
        });
        builder.HasData(new FireTruck()
        {
            IdFiretruck = 6,
            OperationNumber = "6",
            SpecialEquipment = false
        });
    }
}