﻿using GiftRegistryService.Data.Helpers;
using GiftRegistryService.Data.Model;
using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Threading.Tasks;

namespace GiftRegistryService.Data
{
    public interface IGiftRegistryServiceContext
    {
        DbSet<User> Users { get; set; }
        DbSet<Role> Roles { get; set; }
        DbSet<Tenant> Tenants { get; set; }
        DbSet<DigitalAsset> DigitalAssets { get; set; }        
        DbSet<Account> Accounts { get; set; }
        DbSet<Profile> Profiles { get; set; }
        DbSet<Feature> Features { get; set; }
        DbSet<Subscription> Subscriptions { get; set; }
        DbSet<Product> Products { get; set; }
        DbSet<Guest> Guests { get; set; }
        DbSet<Event> Events { get; set; }
        DbSet<Contact> Contacts { get; set; }
        DbSet<PhotoGallery> PhotoGalleries { get; set; }
        DbSet<PhotoGallerySlide> PhotoGallerySlides { get; set; }
        DbSet<ContentBlock> ContentBlocks { get; set; }
        DbSet<Personality> Personalities { get; set; }
        Task<int> SaveChangesAsync();
    }

    public class GiftRegistryServiceContext: DbContext, IGiftRegistryServiceContext
    {
        public GiftRegistryServiceContext()
            :base("GiftRegistryServiceContext")
        {
            Configuration.ProxyCreationEnabled = false;
            Configuration.LazyLoadingEnabled = false;
            Configuration.AutoDetectChangesEnabled = true;
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Tenant> Tenants { get; set; }
        public DbSet<DigitalAsset> DigitalAssets { get; set; }        
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<Feature> Features { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Guest> Guests { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<PhotoGallery> PhotoGalleries { get; set; }
        public DbSet<PhotoGallerySlide> PhotoGallerySlides { get; set; }
        public DbSet<ContentBlock> ContentBlocks { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<MenuItem> MenuItems { get; set; }
        public DbSet<Personality> Personalities { get; set; }

        public override int SaveChanges()
        {
            UpdateLoggableEntries();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync()
        {
            UpdateLoggableEntries();
            return base.SaveChangesAsync();
        }

        public void UpdateLoggableEntries()
        {
            foreach (var entity in ChangeTracker.Entries()
                .Where(e => e.Entity is ILoggable && ((e.State == EntityState.Added || (e.State == EntityState.Modified))))
                .Select(x => x.Entity as ILoggable))
            {
                entity.CreatedOn = entity.CreatedOn == default(DateTime) ? DateTime.UtcNow : entity.CreatedOn;
                entity.LastModifiedOn = DateTime.UtcNow;
            }
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().
                HasMany(u => u.Roles).
                WithMany(r => r.Users).
                Map(
                    m =>
                    {
                        m.MapLeftKey("User_Id");
                        m.MapRightKey("Role_Id");
                        m.ToTable("UserRoles");
                    });


            var convention = new AttributeToTableAnnotationConvention<SoftDeleteAttribute, string>(
                "SoftDeleteColumnName",
                (type, attributes) => attributes.Single().ColumnName);

            modelBuilder.Conventions.Add(convention);
        }
    }
}