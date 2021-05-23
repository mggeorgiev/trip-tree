using System;
using System.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;
using trip_tree.ViewModels;

#nullable disable

namespace trip_tree.Models
{
    public partial class data_sciContext : DbContext
    {
        public data_sciContext()
        {
        }

        public data_sciContext(DbContextOptions<data_sciContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Line> Lines { get; set; }
        public virtual DbSet<Trip> Trips { get; set; }

        public virtual DbSet<v_complete_graph> V_Complete_Graphs { get; set; }

        public virtual DbSet<v_undirected_graph> V_Undirected_Graphs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                                                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                                                .AddJsonFile("appsettings.json")
                                                .Build();
                optionsBuilder.UseNpgsql(configuration.GetConnectionString("PostgresConnection"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "en_US.utf8");

            modelBuilder.Entity<Line>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("line_pk");

                entity.ToTable("line", "network");

                entity.Property(e => e.Busname)
                    .HasMaxLength(255)
                    .HasColumnName("busname");

                entity.Property(e => e.Departure)
                    .HasMaxLength(255)
                    .HasColumnName("departure");

                entity.Property(e => e.Destination)
                    .HasMaxLength(255)
                    .HasColumnName("destination");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id");
            });

            modelBuilder.Entity<Trip>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("trip_pk");

                entity.ToTable("trip", "network");

                entity.Property(e => e.Route)
                    .HasMaxLength(255)
                    .HasColumnName("route");


                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id");
            });

            modelBuilder.Entity<v_complete_graph>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("v_complete_graph", "network");

                entity.Property(e => e.node_1)
                    .HasMaxLength(255)
                    .HasColumnName("node_1");

                entity.Property(e => e.node_2)
                    .HasMaxLength(255)
                    .HasColumnName("node_2");
            });

            modelBuilder.Entity<v_undirected_graph>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("v_undirected_graph", "network");

                entity.Property(e => e.Departure)
                    .HasMaxLength(255)
                    .HasColumnName("departure");

                entity.Property(e => e.Destination)
                    .HasMaxLength(255)
                    .HasColumnName("destination");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
