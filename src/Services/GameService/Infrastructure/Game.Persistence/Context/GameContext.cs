﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Game.Domain.Entities;

namespace Game.Persistence.Context
{
    public class GameContext : DbContext
    {
        public GameContext(DbContextOptions<GameContext> options) : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Domain.Entities.Game> Games { get; set; }
        public DbSet<GameImage> GameImages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Domain.Entities.Game>()
                .HasOne(x => x.Category)
                .WithMany(x => x.Games)
                .HasForeignKey(x => x.CategoryID);

            modelBuilder.Entity<GameImage>()
                .HasOne(x => x.Game)
                .WithMany(x => x.GameImages)
                .HasForeignKey(x => x.GameID);
        }
    }
}
