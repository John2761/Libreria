using System;
using System.Collections.Generic;
using Libreria.Infraestructure.Models;
using Microsoft.EntityFrameworkCore;

namespace Libreria.Infraestructure.Data;

public partial class LibreriaContext : DbContext
{
    public LibreriaContext(DbContextOptions<LibreriaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Autor> Autor { get; set; }

    public virtual DbSet<Categoria> Categoria { get; set; }

    public virtual DbSet<Cliente> Cliente { get; set; }

    public virtual DbSet<Libro> Libro { get; set; }

    public virtual DbSet<Orden> Orden { get; set; }

    public virtual DbSet<OrdenDetalle> OrdenDetalle { get; set; }

    public virtual DbSet<Rol> Rol { get; set; }

    public virtual DbSet<Usuario> Usuario { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Autor>(entity =>
        {
            entity.HasKey(e => e.IdAutor);

            entity.Property(e => e.Nombre)
                .HasMaxLength(70)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Categoria>(entity =>
        {
            entity.HasKey(e => e.IdCategoria).HasName("PK__Categori__A3C02A10598B746F");

            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasMany(d => d.IdLibro).WithMany(p => p.IdCategoria)
                .UsingEntity<Dictionary<string, object>>(
                    "CategoriaLibro",
                    r => r.HasOne<Libro>().WithMany()
                        .HasForeignKey("IdLibro")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_CategoriaLibro_Libro"),
                    l => l.HasOne<Categoria>().WithMany()
                        .HasForeignKey("IdCategoria")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_CategoriaLibro_Categoria"),
                    j =>
                    {
                        j.HasKey("IdCategoria", "IdLibro").IsClustered(false);
                    });
        });

        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.IdCliente);

            entity.Property(e => e.IdCliente)
                .HasMaxLength(15)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Apellido1)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Apellido2)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.FechaNacimiento).HasColumnType("datetime");
            entity.Property(e => e.Nombre)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Sexo)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
        });

        modelBuilder.Entity<Libro>(entity =>
        {
            entity.HasKey(e => e.IdLibro);

            entity.Property(e => e.Isbn)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Precio).HasColumnType("numeric(18, 2)");

            entity.HasOne(d => d.IdAutorNavigation).WithMany(p => p.Libro)
                .HasForeignKey(d => d.IdAutor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Libro_Autor");
        });

        modelBuilder.Entity<Orden>(entity =>
        {
            entity.HasKey(e => e.IdOrden).HasName("PK_IdOrden");

            entity.Property(e => e.FechaOrden).HasColumnType("datetime");
            entity.Property(e => e.IdCliente)
                .HasMaxLength(15)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Total).HasColumnType("numeric(18, 2)");

            entity.HasOne(d => d.IdClienteNavigation).WithMany(p => p.Orden)
                .HasForeignKey(d => d.IdCliente)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Orden_Cliente");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Orden)
                .HasForeignKey(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Orden_Usuario");
        });

        modelBuilder.Entity<OrdenDetalle>(entity =>
        {
            entity.HasKey(e => new { e.IdOrden, e.IdLibro }).IsClustered(false);

            entity.Property(e => e.Precio).HasColumnType("numeric(18, 2)");
            entity.Property(e => e.Subtotal).HasColumnType("numeric(18, 2)");

            entity.HasOne(d => d.IdLibroNavigation).WithMany(p => p.OrdenDetalle)
                .HasForeignKey(d => d.IdLibro)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OrdenDetalle_Libro");

            entity.HasOne(d => d.IdOrdenNavigation).WithMany(p => p.OrdenDetalle)
                .HasForeignKey(d => d.IdOrden)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OrdenDetalle_Orden");
        });

        modelBuilder.Entity<Rol>(entity =>
        {
            entity.HasKey(e => e.IdRol).HasName("PK_Roles");

            entity.Property(e => e.Descripcion)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario);

            entity.HasIndex(e => e.Email, "UQ__Usuario__A9D1053407DC7F0C").IsUnique();

            entity.HasIndex(e => e.Email, "UQ__Usuario__A9D105346E7C1DD4").IsUnique();

            entity.Property(e => e.Apellidos)
                .HasMaxLength(40)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(40)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .IsUnicode(false)
                .IsFixedLength();

            entity.HasOne(d => d.IdRolNavigation).WithMany(p => p.Usuario)
                .HasForeignKey(d => d.IdRol)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Usuario_Rol");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
