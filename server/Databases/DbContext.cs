using System;
using Microsoft.EntityFrameworkCore;
using vault.Entities;

namespace vault.Databases
{
    public class DbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public DbContext(DbContextOptions<DbContext> options) : base(options) {}
        public DbSet<Replay> Replays { get; set; }
        public DbSet<MostPlayedMapsAggregateRecord> AggregateDbSet { get; set; }
        public DbSet<Beatmap> Beatmaps { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Replay>().ToTable("replays").HasKey(r => r.Sha256);
            modelBuilder.Entity<Replay>()
                .HasOne(r => r.Beatmap)
                .WithOne()
                .HasForeignKey<Replay>(r => r.BeatmapHash)
                .IsRequired(false);
            modelBuilder.Entity<Beatmap>().ToTable("beatmaps").HasKey(r => r.BeatmapHash);
            modelBuilder.Entity<Beatmap>()
                .HasOne(r => r.Detail)
                .WithOne()
                .HasForeignKey<BeatmapDetail>(r => r.Md5)
                .IsRequired(false);

            modelBuilder.Entity<MostPlayedMapsAggregateRecord>()
                .HasBaseType((Type?)null)
                .HasKey(r => r.BeatmapHash);
            modelBuilder.Entity<MostPlayedMapsAggregateRecord>()
                .HasOne(r => r.Detail)
                .WithOne()
                .HasForeignKey<BeatmapDetail>(r => r.Md5)
                .IsRequired(false);
            base.OnModelCreating(modelBuilder);
        }
    }
}