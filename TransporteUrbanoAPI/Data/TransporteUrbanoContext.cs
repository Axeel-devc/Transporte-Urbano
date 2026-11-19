using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using TransporteUrbanoAPI.Models;

namespace TransporteUrbanoAPI.Data;

public partial class TransporteUrbanoContext : DbContext
{
    public TransporteUrbanoContext(DbContextOptions<TransporteUrbanoContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Asignacione> Asignaciones { get; set; }

    public virtual DbSet<Boleto> Boletos { get; set; }

    public virtual DbSet<Calle> Calles { get; set; }

    public virtual DbSet<Chofere> Choferes { get; set; }

    public virtual DbSet<Colectivo> Colectivos { get; set; }

    public virtual DbSet<DetalleMantenimiento> DetalleMantenimientos { get; set; }

    public virtual DbSet<EstadosColectivo> EstadosColectivos { get; set; }

    public virtual DbSet<FormasPago> FormasPagos { get; set; }

    public virtual DbSet<Linea> Lineas { get; set; }

    public virtual DbSet<Mantenimiento> Mantenimientos { get; set; }

    public virtual DbSet<MovimientosStock> MovimientosStocks { get; set; }

    public virtual DbSet<Parada> Paradas { get; set; }

    public virtual DbSet<Proveedore> Proveedores { get; set; }

    public virtual DbSet<Recorrido> Recorridos { get; set; }

    public virtual DbSet<RecorridosParada> RecorridosParadas { get; set; }

    public virtual DbSet<Repuesto> Repuestos { get; set; }

    public virtual DbSet<SentidosViaje> SentidosViajes { get; set; }

    public virtual DbSet<Servicio> Servicios { get; set; }

    public virtual DbSet<TiposBoleto> TiposBoletos { get; set; }

    public virtual DbSet<TiposMantenimiento> TiposMantenimientos { get; set; }

    public virtual DbSet<TiposRepuesto> TiposRepuestos { get; set; }

    public virtual DbSet<UbicacionCoche> UbicacionCoches { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    public virtual DbSet<VistaEstadoAsignacionesChofere> VistaEstadoAsignacionesChoferes { get; set; }

    public virtual DbSet<VistaRecaudacionLineaTipoBoleto> VistaRecaudacionLineaTipoBoletos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Asignacione>(entity =>
        {
            entity.HasKey(e => e.CodigoAsignacion);

            entity.Property(e => e.CodigoAsignacion).HasColumnName("Codigo_Asignacion");
            entity.Property(e => e.CodigoChofer).HasColumnName("Codigo_Chofer");
            entity.Property(e => e.HoraFin)
                .HasPrecision(0)
                .HasColumnName("Hora_Fin");
            entity.Property(e => e.HoraInicio)
                .HasPrecision(0)
                .HasColumnName("Hora_inicio");
            entity.Property(e => e.NroCoche).HasColumnName("Nro_Coche");

            entity.HasOne(d => d.CodigoChoferNavigation).WithMany(p => p.Asignaciones)
                .HasForeignKey(d => d.CodigoChofer)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Asignaciones_Choferes");

            entity.HasOne(d => d.NroCocheNavigation).WithMany(p => p.Asignaciones)
                .HasForeignKey(d => d.NroCoche)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Asignaciones_Colectivos");
        });

        modelBuilder.Entity<Boleto>(entity =>
        {
            entity.HasKey(e => e.CodigoBoleto);

            entity.Property(e => e.CodigoBoleto).HasColumnName("Codigo_Boleto");
            entity.Property(e => e.CodigoFormaPago).HasColumnName("Codigo_Forma_Pago");
            entity.Property(e => e.CodigoServicio).HasColumnName("Codigo_Servicio");
            entity.Property(e => e.CodigoTipoBoleto).HasColumnName("Codigo_Tipo_Boleto");
            entity.Property(e => e.FechaHora)
                .HasPrecision(0)
                .HasColumnName("Fecha_Hora");
            entity.Property(e => e.Importe).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.CodigoFormaPagoNavigation).WithMany(p => p.Boletos)
                .HasForeignKey(d => d.CodigoFormaPago)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Boletos_Formas_Pago");

            entity.HasOne(d => d.CodigoServicioNavigation).WithMany(p => p.Boletos)
                .HasForeignKey(d => d.CodigoServicio)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Boletos_Servicios");

            entity.HasOne(d => d.CodigoTipoBoletoNavigation).WithMany(p => p.Boletos)
                .HasForeignKey(d => d.CodigoTipoBoleto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Boletos_Tipos_Boleto");
        });

        modelBuilder.Entity<Calle>(entity =>
        {
            entity.HasKey(e => e.CodigoCalle);

            entity.Property(e => e.CodigoCalle).HasColumnName("Codigo_Calle");
            entity.Property(e => e.NombreCalle)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Nombre_Calle");
        });

        modelBuilder.Entity<Chofere>(entity =>
        {
            entity.HasKey(e => e.CodigoChofer);

            entity.HasIndex(e => e.Dni, "UQ_Choferes_DNI").IsUnique();

            entity.Property(e => e.CodigoChofer).HasColumnName("Codigo_Chofer");
            entity.Property(e => e.Apellido)
                .HasMaxLength(60)
                .IsUnicode(false);
            entity.Property(e => e.Dni)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("DNI");
            entity.Property(e => e.Nombre)
                .HasMaxLength(60)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Colectivo>(entity =>
        {
            entity.HasKey(e => e.NroCoche);

            entity.HasIndex(e => e.Patente, "UQ_Colectivos_Patente").IsUnique();

            entity.Property(e => e.NroCoche).HasColumnName("Nro_Coche");
            entity.Property(e => e.CodigoEstado).HasColumnName("Codigo_Estado");
            entity.Property(e => e.CodigoLinea).HasColumnName("Codigo_Linea");
            entity.Property(e => e.Marca)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Modelo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Patente)
                .HasMaxLength(10)
                .IsUnicode(false);

            entity.HasOne(d => d.CodigoEstadoNavigation).WithMany(p => p.Colectivos)
                .HasForeignKey(d => d.CodigoEstado)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Colectivos_Estados");

            entity.HasOne(d => d.CodigoLineaNavigation).WithMany(p => p.Colectivos)
                .HasForeignKey(d => d.CodigoLinea)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Colectivos_Lineas");
        });

        modelBuilder.Entity<DetalleMantenimiento>(entity =>
        {
            entity.HasKey(e => e.CodigoDetalleMantenimiento);

            entity.ToTable("Detalle_Mantenimiento");

            entity.Property(e => e.CodigoDetalleMantenimiento).HasColumnName("Codigo_Detalle_Mantenimiento");
            entity.Property(e => e.CodigoMantenimiento).HasColumnName("Codigo_Mantenimiento");
            entity.Property(e => e.CodigoRepuesto).HasColumnName("Codigo_Repuesto");
            entity.Property(e => e.CostoUnitario)
                .HasColumnType("decimal(12, 2)")
                .HasColumnName("Costo_Unitario");

            entity.HasOne(d => d.CodigoMantenimientoNavigation).WithMany(p => p.DetalleMantenimientos)
                .HasForeignKey(d => d.CodigoMantenimiento)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Detalle_Mantenimiento_Mantenimientos");

            entity.HasOne(d => d.CodigoRepuestoNavigation).WithMany(p => p.DetalleMantenimientos)
                .HasForeignKey(d => d.CodigoRepuesto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Detalle_Mantenimiento_Repuestos");
        });

        modelBuilder.Entity<EstadosColectivo>(entity =>
        {
            entity.HasKey(e => e.CodigoEstado);

            entity.ToTable("Estados_Colectivo");

            entity.HasIndex(e => e.Descripcion, "UQ_Estados_Colectivo_Descripcion").IsUnique();

            entity.Property(e => e.CodigoEstado).HasColumnName("Codigo_Estado");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<FormasPago>(entity =>
        {
            entity.HasKey(e => e.CodigoFormaPago);

            entity.ToTable("Formas_Pago");

            entity.HasIndex(e => e.Descripcion, "UQ_Formas_Pago_Descripcion").IsUnique();

            entity.Property(e => e.CodigoFormaPago).HasColumnName("Codigo_Forma_Pago");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Linea>(entity =>
        {
            entity.HasKey(e => e.CodigoLinea);

            entity.Property(e => e.CodigoLinea).HasColumnName("Codigo_Linea");
            entity.Property(e => e.NumeroLinea)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("Numero_Linea");
        });

        modelBuilder.Entity<Mantenimiento>(entity =>
        {
            entity.HasKey(e => e.CodigoMantenimiento);

            entity.Property(e => e.CodigoMantenimiento).HasColumnName("Codigo_Mantenimiento");
            entity.Property(e => e.CodigoTipoMantenimiento).HasColumnName("Codigo_Tipo_Mantenimiento");
            entity.Property(e => e.Costo).HasColumnType("decimal(12, 2)");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.NroCoche).HasColumnName("Nro_Coche");

            entity.HasOne(d => d.CodigoTipoMantenimientoNavigation).WithMany(p => p.Mantenimientos)
                .HasForeignKey(d => d.CodigoTipoMantenimiento)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Mantenimientos_Tipos");

            entity.HasOne(d => d.NroCocheNavigation).WithMany(p => p.Mantenimientos)
                .HasForeignKey(d => d.NroCoche)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Mantenimientos_Colectivos");
        });

        modelBuilder.Entity<MovimientosStock>(entity =>
        {
            entity.HasKey(e => e.CodigoMovimiento);

            entity.ToTable("Movimientos_Stock");

            entity.Property(e => e.CodigoMovimiento).HasColumnName("Codigo_Movimiento");
            entity.Property(e => e.CodigoRepuesto).HasColumnName("Codigo_Repuesto");
            entity.Property(e => e.Fecha).HasPrecision(0);
            entity.Property(e => e.Observacion)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.TipoMovimiento)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("Tipo_Movimiento");

            entity.HasOne(d => d.CodigoRepuestoNavigation).WithMany(p => p.MovimientosStocks)
                .HasForeignKey(d => d.CodigoRepuesto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Movimientos_Stock_Repuestos");
        });

        modelBuilder.Entity<Parada>(entity =>
        {
            entity.HasKey(e => e.CodigoParada);

            entity.Property(e => e.CodigoParada).HasColumnName("Codigo_Parada");
            entity.Property(e => e.CodigoCalle).HasColumnName("Codigo_Calle");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.NumeroCalle)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("Numero_Calle");

            entity.HasOne(d => d.CodigoCalleNavigation).WithMany(p => p.Parada)
                .HasForeignKey(d => d.CodigoCalle)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Paradas_Calles");
        });

        modelBuilder.Entity<Proveedore>(entity =>
        {
            entity.HasKey(e => e.CodigoProveedor);

            entity.Property(e => e.CodigoProveedor).HasColumnName("Codigo_Proveedor");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Telefono)
                .HasMaxLength(25)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Recorrido>(entity =>
        {
            entity.HasKey(e => e.CodigoRecorrido);

            entity.Property(e => e.CodigoRecorrido).HasColumnName("Codigo_Recorrido");
            entity.Property(e => e.CodigoLinea).HasColumnName("Codigo_Linea");
            entity.Property(e => e.CodigoSentido).HasColumnName("Codigo_Sentido");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(150)
                .IsUnicode(false);

            entity.HasOne(d => d.CodigoLineaNavigation).WithMany(p => p.Recorridos)
                .HasForeignKey(d => d.CodigoLinea)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Recorridos_Lineas");

            entity.HasOne(d => d.CodigoSentidoNavigation).WithMany(p => p.Recorridos)
                .HasForeignKey(d => d.CodigoSentido)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Recorridos_Sentidos");
        });

        modelBuilder.Entity<RecorridosParada>(entity =>
        {
            entity.HasKey(e => new { e.CodigoRecorrido, e.CodigoParada });

            entity.ToTable("Recorridos_Paradas");

            entity.HasIndex(e => new { e.CodigoRecorrido, e.Orden }, "UQ_Recorridos_Paradas_Orden").IsUnique();

            entity.Property(e => e.CodigoRecorrido).HasColumnName("Codigo_Recorrido");
            entity.Property(e => e.CodigoParada).HasColumnName("Codigo_Parada");

            entity.HasOne(d => d.CodigoParadaNavigation).WithMany(p => p.RecorridosParada)
                .HasForeignKey(d => d.CodigoParada)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Recorridos_Paradas_Paradas");

            entity.HasOne(d => d.CodigoRecorridoNavigation).WithMany(p => p.RecorridosParada)
                .HasForeignKey(d => d.CodigoRecorrido)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Recorridos_Paradas_Recorridos");
        });

        modelBuilder.Entity<Repuesto>(entity =>
        {
            entity.HasKey(e => e.CodigoRepuesto);

            entity.Property(e => e.CodigoRepuesto).HasColumnName("Codigo_Repuesto");
            entity.Property(e => e.CodigoProveedor).HasColumnName("Codigo_Proveedor");
            entity.Property(e => e.CodigoTipoRepuesto).HasColumnName("Codigo_Tipo_Repuesto");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(120)
                .IsUnicode(false);
            entity.Property(e => e.PrecioUnitario)
                .HasColumnType("decimal(12, 2)")
                .HasColumnName("Precio_Unitario");
            entity.Property(e => e.StockMinimo).HasColumnName("Stock_Minimo");

            entity.HasOne(d => d.CodigoProveedorNavigation).WithMany(p => p.Repuestos)
                .HasForeignKey(d => d.CodigoProveedor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Repuestos_Proveedores");

            entity.HasOne(d => d.CodigoTipoRepuestoNavigation).WithMany(p => p.Repuestos)
                .HasForeignKey(d => d.CodigoTipoRepuesto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Repuestos_Tipos");
        });

        modelBuilder.Entity<SentidosViaje>(entity =>
        {
            entity.HasKey(e => e.CodigoSentido);

            entity.ToTable("Sentidos_Viaje");

            entity.Property(e => e.CodigoSentido).HasColumnName("Codigo_Sentido");
            entity.Property(e => e.Sentido)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Servicio>(entity =>
        {
            entity.HasKey(e => e.CodigoServicio);

            entity.Property(e => e.CodigoServicio).HasColumnName("Codigo_Servicio");
            entity.Property(e => e.CodigoChofer).HasColumnName("Codigo_Chofer");
            entity.Property(e => e.CodigoRecorrido).HasColumnName("Codigo_Recorrido");
            entity.Property(e => e.HoraFin)
                .HasPrecision(0)
                .HasColumnName("Hora_Fin");
            entity.Property(e => e.HoraInicio)
                .HasPrecision(0)
                .HasColumnName("Hora_inicio");
            entity.Property(e => e.NroCoche).HasColumnName("Nro_Coche");

            entity.HasOne(d => d.CodigoChoferNavigation).WithMany(p => p.Servicios)
                .HasForeignKey(d => d.CodigoChofer)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Servicios_Choferes");

            entity.HasOne(d => d.CodigoRecorridoNavigation).WithMany(p => p.Servicios)
                .HasForeignKey(d => d.CodigoRecorrido)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Servicios_Recorridos");

            entity.HasOne(d => d.NroCocheNavigation).WithMany(p => p.Servicios)
                .HasForeignKey(d => d.NroCoche)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Servicios_Colectivos");
        });

        modelBuilder.Entity<TiposBoleto>(entity =>
        {
            entity.HasKey(e => e.CodigoTipoBoleto);

            entity.ToTable("Tipos_Boleto");

            entity.Property(e => e.CodigoTipoBoleto).HasColumnName("Codigo_Tipo_Boleto");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Precio).HasColumnType("decimal(10, 2)");
        });

        modelBuilder.Entity<TiposMantenimiento>(entity =>
        {
            entity.HasKey(e => e.CodigoTipoMantenimiento);

            entity.ToTable("Tipos_Mantenimiento");

            entity.Property(e => e.CodigoTipoMantenimiento).HasColumnName("Codigo_Tipo_Mantenimiento");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TiposRepuesto>(entity =>
        {
            entity.HasKey(e => e.CodigoTipoRepuesto);

            entity.ToTable("Tipos_Repuesto");

            entity.Property(e => e.CodigoTipoRepuesto).HasColumnName("Codigo_Tipo_Repuesto");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<UbicacionCoche>(entity =>
        {
            entity.HasKey(e => e.CodigoUbicacionCoche);

            entity.ToTable("Ubicacion_Coche");

            entity.Property(e => e.CodigoUbicacionCoche).HasColumnName("Codigo_Ubicacion_Coche");
            entity.Property(e => e.CodigoServicio).HasColumnName("Codigo_Servicio");
            entity.Property(e => e.FechaHora)
                .HasPrecision(0)
                .HasColumnName("Fecha_Hora");
            entity.Property(e => e.Latitud).HasColumnType("decimal(9, 6)");
            entity.Property(e => e.Longitud).HasColumnType("decimal(9, 6)");
            entity.Property(e => e.VelocidadKmxH)
                .HasColumnType("decimal(7, 2)")
                .HasColumnName("Velocidad_KmxH");

            entity.HasOne(d => d.CodigoServicioNavigation).WithMany(p => p.UbicacionCoches)
                .HasForeignKey(d => d.CodigoServicio)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Ubicacion_Coche_Servicios");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.CodigoUsuario);

            entity.HasIndex(e => e.NombreUsuario, "UQ_Usuarios_Nombre").IsUnique();

            entity.Property(e => e.CodigoUsuario).HasColumnName("Codigo_Usuario");
            entity.Property(e => e.NombreUsuario)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Nombre_Usuario");
            entity.Property(e => e.PasswordHash)
                .HasMaxLength(64)
                .IsUnicode(false)
                .HasColumnName("Password_Hash");
            entity.Property(e => e.Rol)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasDefaultValue("Admin", "DF_Usuarios_Rol");
        });

        modelBuilder.Entity<VistaEstadoAsignacionesChofere>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("Vista_Estado_Asignaciones_Choferes");

            entity.Property(e => e.Apellido)
                .HasMaxLength(60)
                .IsUnicode(false);
            entity.Property(e => e.CantidadAsignaciones).HasColumnName("Cantidad_Asignaciones");
            entity.Property(e => e.CodigoChofer).HasColumnName("Codigo_Chofer");
            entity.Property(e => e.Estado)
                .HasMaxLength(16)
                .IsUnicode(false);
            entity.Property(e => e.MinutosAsignados).HasColumnName("Minutos_Asignados");
            entity.Property(e => e.Nombre)
                .HasMaxLength(60)
                .IsUnicode(false);
        });

        modelBuilder.Entity<VistaRecaudacionLineaTipoBoleto>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("Vista_Recaudacion_Linea_Tipo_Boleto");

            entity.Property(e => e.CantidadBoletos).HasColumnName("Cantidad_Boletos");
            entity.Property(e => e.NumeroLinea)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("Numero_Linea");
            entity.Property(e => e.RecaudacionTotal)
                .HasColumnType("decimal(38, 2)")
                .HasColumnName("Recaudacion_Total");
            entity.Property(e => e.TipoBoleto)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Tipo_Boleto");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
