using BuberDinner.Domain.Host.ValueObjects;
using BuberDinner.Domain.Menu;
using BuberDinner.Domain.Menu.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BuberDinner.Infrastructure.Persistence.Configurations;

public class MenuConfigurations : IEntityTypeConfiguration<Menu>
{
    public void Configure(EntityTypeBuilder<Menu> builder)
    {
        ConfigureMenusTable(builder);
        ConfigureMenuSections(builder);
    }

    private void ConfigureMenusTable(EntityTypeBuilder<Menu> builder)
    {
        builder.ToTable("Menus");

        builder.HasKey(m => m.Id);
        builder.Property(m => m.Id)
            .ValueGeneratedNever()
            .HasConversion(
                id => id.Value,
                value => MenuId.CreateUnique(value));

        builder.Property(m => m.Name)
            .HasMaxLength(100);

        builder.Property(m => m.Description)
            .HasMaxLength(100);

        builder.OwnsOne(m => m.AverageRating);

        builder.Property(m => m.HostId)
            .ValueGeneratedNever()
            .HasConversion(
                id => id.Value,
                value => HostId.CreateUnique(value));
    }

    private void ConfigureMenuSections(EntityTypeBuilder<Menu> builder)
    {
        builder.OwnsMany(m => m.Sections,
            sectionBuilder =>
            {
                sectionBuilder.ToTable("MenuSections");
                
                sectionBuilder.WithOwner().HasForeignKey("MenuId");

                sectionBuilder.HasKey("Id", "MenuId");
                
                sectionBuilder.Property(s => s.Id)
                    .HasColumnName("MenuSectionId")
                    .ValueGeneratedNever()
                    .HasConversion(
                        id => id.Value, value => MenuSectionId.CreateUnique(value));
                
                sectionBuilder.Property(s => s.Name)
                    .HasMaxLength(100);
                
                sectionBuilder.Property(s => s.Description)
                    .HasMaxLength(100);

                sectionBuilder.OwnsMany(s => s.Items, itemBuilder =>
                {
                    itemBuilder.ToTable("MenuItems");
                    itemBuilder.WithOwner().HasForeignKey("MenuSectionId", "MenuId");

                    itemBuilder.HasKey(nameof(MenuItem.Id), "MenuSectionId", "MenuId");
                    
                    itemBuilder.Property(i => i.Id)
                        .HasColumnName("MenuItemId")
                        .ValueGeneratedNever()
                        .HasConversion(id => id.Value, value => MenuItemId.CreateUnique(value));
                    
                    itemBuilder.Property(s => s.Name)
                        .HasMaxLength(100);
                
                    itemBuilder.Property(s => s.Description)
                        .HasMaxLength(100);
                });
            });
    }
}