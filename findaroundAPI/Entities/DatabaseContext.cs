using System;
using findaroundAPI.Models;
using findaroundAPI.Utilities;
using Microsoft.EntityFrameworkCore;

namespace findaroundAPI.Entities
{
	public class DatabaseContext : DbContext
	{
		static DbConnectionConfig config = DbConnectionUtilities.GetDbConnectionConfig();

		readonly string connectionString = $"server={config.Address};port={config.Port};user={config.Username};password={config.Password};database={config.Database}";

        // Tables

        public DbSet<UserEntity> Users { get; set; }
        public DbSet<PostEntity> Posts { get; set; }
        public DbSet<CommentEntity> PostsComments { get; set; }
        public DbSet<PostsImagesEntity> PostsImages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Relationships between tables

            modelBuilder.Entity<UserEntity>().HasMany(u => u.Comments).WithOne(c => c.Author).HasForeignKey(u => u.AuthorId);
            modelBuilder.Entity<PostEntity>().HasMany(p => p.Comments).WithOne(c => c.Post).HasForeignKey(p => p.PostId);
            modelBuilder.Entity<PostEntity>().HasMany(p => p.Images).WithOne(i => i.Post).HasForeignKey(p => p.PostId);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
			optionsBuilder.UseMySql(connectionString, new MySqlServerVersion(new Version(8,0,21))).UseLoggerFactory(LoggerFactory.Create(b => b.AddConsole().AddFilter(level => level >= LogLevel.Information)))
                .EnableSensitiveDataLogging().EnableDetailedErrors();
        }
    }
}

