using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace LudoGameEngine.Database
{
    public class LudoContext : DbContext
    {
        public DbSet<Game> Game { get; set; }
        public DbSet<Player> Player { get; set; }
        public DbSet<GamePiece> GamePiece { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            IConfiguration config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", true, true)
                .AddJsonFile($"appsettings.dev.json", true, true)
                .Build();

            var defaultConnection = config.GetConnectionString("DefaultConnection");

            optionsBuilder.UseSqlServer(defaultConnection);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            var colorConverter = new ValueConverter<Color, string>(
             v => v.ToString(),
             v => (Color)Enum.Parse(typeof(Color), v));

            var positionTypeConverter = new ValueConverter<PositionType, string>(
             v => v.ToString(),
             v => (PositionType)Enum.Parse(typeof(PositionType), v));

            builder.Entity<Player>()
                .Property(x => x.Name)
                .HasMaxLength(50);

            builder.Entity<Player>()
                 .Property(x => x.Color)
                 .HasConversion(colorConverter);

            builder.Entity<Player>()
                .Property(x => x.ID)
                .ValueGeneratedNever();

            builder.Entity<GamePiece>()
                 .Property(x => x.PositionType)
                 .HasConversion(positionTypeConverter);

            //builder.Entity<GamePiece>()
            //    .Property(x => x.PlayerGamePiece)
            //    .ValueGeneratedNever();

        }
    }
}