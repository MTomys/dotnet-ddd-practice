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
        ConfigureMenuDinnerIdsTable(builder);
        ConfigureMenuReviewIdsTable(builder);
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
            menuSectionBuilder =>
            {
                menuSectionBuilder.ToTable("MenuSections");

                menuSectionBuilder.WithOwner().HasForeignKey("MenuId");

                menuSectionBuilder.HasKey("Id", "MenuId");

                menuSectionBuilder.Property(s => s.Id)
                    .HasColumnName("MenuSectionId")
                    .ValueGeneratedNever()
                    .HasConversion(
                        id => id.Value, value => MenuSectionId.CreateUnique(value));

                menuSectionBuilder.Property(s => s.Name)
                    .HasMaxLength(100);

                menuSectionBuilder.Property(s => s.Description)
                    .HasMaxLength(100);

                menuSectionBuilder.OwnsMany(s => s.Items, itemBuilder =>
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

                menuSectionBuilder.Navigation(s => s.Items).Metadata.SetField("_items");
                menuSectionBuilder.Navigation(s => s.Items).UsePropertyAccessMode(PropertyAccessMode.Field);
            });

        builder.Metadata.FindNavigation(nameof(Menu.Sections))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);
    }

    private void ConfigureMenuDinnerIdsTable(EntityTypeBuilder<Menu> builder)
    {
        builder.OwnsMany(m => m.DinnerIds, dinnerIdBuilder =>
        {
            dinnerIdBuilder.ToTable("MenuDinnerIds");
            dinnerIdBuilder.WithOwner().HasForeignKey("MenuId");
            dinnerIdBuilder.HasKey("Id");
            dinnerIdBuilder.Property(d => d.Value)
                .HasColumnName("DinnerId")
                .ValueGeneratedNever();
        });
    }

    private void ConfigureMenuReviewIdsTable(EntityTypeBuilder<Menu> builder)
    {
        builder.OwnsMany(m => m.MenuReviewIds, dinnerIdBuilder =>
        {
            dinnerIdBuilder.ToTable("MenuReviewIds");
            dinnerIdBuilder.WithOwner().HasForeignKey("MenuId");
            dinnerIdBuilder.HasKey("Id");
            dinnerIdBuilder.Property(d => d.Value)
                .HasColumnName("ReviewId")
                .ValueGeneratedNever();
        });
    }
}