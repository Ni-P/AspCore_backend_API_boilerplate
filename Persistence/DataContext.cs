using System;
using Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Persistence
{
    public class DataContext : IdentityDbContext<AppUser>
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Parent> Parents { get; set; }
        public DbSet<Child> Children { get; set; }
        public DbSet<FamilyRelationship> FamilyMembers { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<FamilyRelationship>(x =>
                x.HasKey(k => new {k.ParentId, k.ChildId}));
            builder.Entity<FamilyRelationship>()
                .HasOne(p => p.Parent)
                .WithMany(p => p.FamilyRelationships)
                .HasForeignKey(k => k.ParentId);

            builder.Entity<FamilyRelationship>()
                .HasOne(c => c.Child)
                .WithMany(c => c.FamilyRelationships)
                .HasForeignKey(k => k.ChildId);


            // builder.Entity<UserActivity>(x => x.HasKey(ua =>
            //     new {ua.AppUserId, ua.ActivityId}));
            // builder.Entity<UserActivity>()
            //     .HasOne(u => u.AppUser)
            //     .WithMany((u => u.UserActivities))
            //     .HasForeignKey(a => a.AppUserId);
            // builder.Entity<UserActivity>()
            //     .HasOne(a => a.Activity)
            //     .WithMany((u => u.UserActivities))
            //     .HasForeignKey(a => a.ActivityId);
        }
    }
}