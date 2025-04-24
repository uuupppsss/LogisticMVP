using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace ApiMvp.Model;

public partial class LogisticMvpContext : DbContext
{
    public LogisticMvpContext()
    {
    }

    public LogisticMvpContext(DbContextOptions<LogisticMvpContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderStatus> OrderStatuses { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductsInOrder> ProductsInOrders { get; set; }

    public virtual DbSet<Route> Routes { get; set; }

    public virtual DbSet<Transport> Transports { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=192.168.200.13;user=student;password=student;database=LogisticMVP", Microsoft.EntityFrameworkCore.ServerVersion.Parse("10.3.39-mariadb"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_general_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("Order");

            entity.HasIndex(e => e.StatusId, "FK_Order_OrderStatus_Id");

            entity.Property(e => e.Id).HasColumnType("int(11)");
            entity.Property(e => e.CustomerName).HasMaxLength(255);
            entity.Property(e => e.OrderDate)
                .HasDefaultValueSql("'0000-00-00 00:00:00'")
                .HasColumnType("datetime");
            entity.Property(e => e.StatusId).HasColumnType("int(11)");

            entity.HasOne(d => d.Status).WithMany(p => p.Orders)
                .HasForeignKey(d => d.StatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Order_OrderStatus_Id");
        });

        modelBuilder.Entity<OrderStatus>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("OrderStatus");

            entity.Property(e => e.Id).HasColumnType("int(11)");
            entity.Property(e => e.Title)
                .HasMaxLength(255)
                .HasDefaultValueSql("''");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("Product");

            entity.Property(e => e.Id).HasColumnType("int(11)");
            entity.Property(e => e.Price).HasPrecision(19, 2);
            entity.Property(e => e.ProductName)
                .HasMaxLength(255)
                .HasDefaultValueSql("''");
            entity.Property(e => e.Quantity).HasColumnType("int(11)");
        });

        modelBuilder.Entity<ProductsInOrder>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("ProductsInOrder");

            entity.HasIndex(e => e.IdOrder, "FK_ProductsInOrder_Order_Id");

            entity.HasIndex(e => e.IdProduct, "FK_ProductsInOrder_Product_Id");

            entity.Property(e => e.Id).HasColumnType("int(11)");
            entity.Property(e => e.IdOrder).HasColumnType("int(11)");
            entity.Property(e => e.IdProduct).HasColumnType("int(11)");

            entity.HasOne(d => d.IdOrderNavigation).WithMany(p => p.ProductsInOrders)
                .HasForeignKey(d => d.IdOrder)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ProductsInOrder_Order_Id");

            entity.HasOne(d => d.IdProductNavigation).WithMany(p => p.ProductsInOrders)
                .HasForeignKey(d => d.IdProduct)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ProductsInOrder_Product_Id");
        });

        modelBuilder.Entity<Route>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("Route");

            entity.HasIndex(e => e.IdOrder, "FK_Route_Order_Id");

            entity.HasIndex(e => e.IdTransport, "FK_Route_Transport_Id");

            entity.Property(e => e.Id).HasColumnType("int(11)");
            entity.Property(e => e.EndPoint)
                .HasMaxLength(255)
                .HasDefaultValueSql("''");
            entity.Property(e => e.IdOrder).HasColumnType("int(11)");
            entity.Property(e => e.IdTransport).HasColumnType("int(11)");
            entity.Property(e => e.StartPoint)
                .HasMaxLength(255)
                .HasDefaultValueSql("''");

            entity.HasOne(d => d.IdOrderNavigation).WithMany(p => p.Routes)
                .HasForeignKey(d => d.IdOrder)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Route_Order_Id");

            entity.HasOne(d => d.IdTransportNavigation).WithMany(p => p.Routes)
                .HasForeignKey(d => d.IdTransport)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Route_Transport_Id");
        });

        modelBuilder.Entity<Transport>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("Transport");

            entity.Property(e => e.Id).HasColumnType("int(11)");
            entity.Property(e => e.Brand)
                .HasMaxLength(255)
                .HasDefaultValueSql("''");
            entity.Property(e => e.LicensePlate)
                .HasMaxLength(255)
                .HasDefaultValueSql("''");
            entity.Property(e => e.Model)
                .HasMaxLength(255)
                .HasDefaultValueSql("''");
            entity.Property(e => e.Year).HasColumnType("int(11)");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("User");

            entity.Property(e => e.Id).HasColumnType("int(11)");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasDefaultValueSql("''");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .HasDefaultValueSql("''");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
