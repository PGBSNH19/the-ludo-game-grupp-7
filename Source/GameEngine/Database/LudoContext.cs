using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace LudoGameEngine.Database
{
    public class DataContext : DbContext
    {
        public DbSet<Player> Player { get; set; }
        public DbSet<Game> Game { get; set; }
        public DbSet<Session> Session { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            IConfiguration config = new ConfigurationBuilder()
                .AddJsonFile("AppSettings.Json")
                .Build();

            var defaultConnection = config.GetConnectionString("DefaultConnection");

            optionsBuilder.UseSqlServer(defaultConnection);
        }

        //protected override void OnModelCreating(ModelBuilder Builder)
        //{
        //    Builder.Entity<PlayerSession>().HasKey(c => new { c.SessionId, c.PlayerId });
        //    Builder.Entity<PlayerSession>()
        //        .HasOne<Player>(c => c.PlayerRef)
        //        .WithMany(s => s.PlayerSession)
        //        .HasForeignKey(x => x.PlayerId);

        //    Builder.Entity<PlayerSession>()
        //        .HasOne<Session>(x => x.SessionRef)
        //        .WithMany(c => c.PlayerPiece)
        //        .HasForeignKey(x => x.SessionId);

        //    Builder.Entity<PlayerPiece>().HasKey(c => new { c.PlayerId, c.PieceId });
        //    Builder.Entity<PlayerPiece>()
        //        .HasOne<Player>(c => c.PlayerRef)
        //        .WithMany(c => c.PlayerPiece)
        //        .HasForeignKey(x => x.PlayerId);

        //    Builder.Entity<PlayerPiece>()
        //        .HasOne<Piece>(c => c.PieceRef)
        //        .WithMany(c => c.PlayerPiece)
        //        .HasForeignKey(x => x.PieceId);
        //}
    }
}