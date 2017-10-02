using CoWorker.EntityFramework.ValueGenerators;
using Microsoft.EntityFrameworkCore.ValueGeneration;
namespace CoWorker.Models.Blog
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Infrastructure;
    using Microsoft.EntityFrameworkCore.Metadata;
    using Microsoft.Extensions.DependencyInjection;
    using System;

    public class BlogDbContext : DbContext
    {
        public const string CURRENT_ID = "CurrentId";
        public const string PARENT_ID = "ParentId";
        public const string USER_ID = "UserId";
        
        private IServiceProvider _provider;
        public BlogDbContext(DbContextOptions<BlogDbContext> options) : base(new DbContextOptionsBuilder(options).Options)
        {
            var ext = options.GetExtension<CoreOptionsExtension>();
            _provider = ext.ApplicationServiceProvider;
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Post>(post =>
            {
                post.HasKey(p => p.Id);
                var createdate = post.Property(p => p.CreateDate)
                    .HasValueGenerator((a, b) => _provider.GetService<DatetimeOffsetValueGenerator>())
                    .ValueGeneratedOnAdd()
                    .Metadata;
                
                var creator = post.Property(p => p.Creator)
                    .HasValueGenerator((a, b) => _provider.GetService<CurrentUserValueGenerator>())
                    .ValueGeneratedOnAdd()
                    .Metadata;

                createdate.BeforeSaveBehavior = creator.BeforeSaveBehavior = PropertySaveBehavior.Save;
                createdate.AfterSaveBehavior = creator.AfterSaveBehavior = PropertySaveBehavior.Ignore;
                var modifydate = post.Property(p => p.ModifyDate)
                    .HasValueGenerator((a, b) => _provider.GetService<DatetimeOffsetValueGenerator>())
                    .ValueGeneratedOnAddOrUpdate()
                    .Metadata;
                var modifier = post.Property(p => p.Modifier)
                    .HasValueGenerator((a, b) => _provider.GetService<CurrentUserValueGenerator>())
                    .ValueGeneratedOnAddOrUpdate()
                    .Metadata;
                modifydate.BeforeSaveBehavior = modifier.BeforeSaveBehavior = PropertySaveBehavior.Save;
                modifydate.AfterSaveBehavior = modifier.AfterSaveBehavior = PropertySaveBehavior.Save;
                post.ToTable("Post");
            });
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<PostRelated>(
                related =>
                {
                    related.HasKey(CURRENT_ID);
                    related.Property<Guid>(PARENT_ID).HasColumnName(PARENT_ID);
                    related.HasIndex(PARENT_ID);
                    related.HasMany(p => p.Posts)
                        .WithOne()
                        .HasForeignKey(PARENT_ID)
                        .OnDelete(DeleteBehavior.Restrict);
                    related.HasOne(p => p.Current)
                        .WithOne()
                        .HasForeignKey<PostRelated>(CURRENT_ID)
                        .OnDelete(DeleteBehavior.Restrict);
                    related.HasOne(p => p.Owner)
                        .WithOne()
                        .HasForeignKey<PostRelated>(USER_ID)
                        .OnDelete(DeleteBehavior.Restrict);
                    
                    related.ToTable("Related");
                });
            
            modelBuilder.Ignore<User>();
        }
    }
}
