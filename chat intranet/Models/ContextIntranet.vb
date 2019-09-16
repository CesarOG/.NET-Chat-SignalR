Imports System
Imports System.Data.Entity
Imports System.ComponentModel.DataAnnotations.Schema
Imports System.Linq

Partial Public Class ContextIntranet
    Inherits DbContext

    Public Sub New()
        MyBase.New("name=ContextIntranet")
    End Sub

    Public Overridable Property EX_MensajeChat As DbSet(Of EX_MensajeChat)
    Public Overridable Property EX_Usuario As DbSet(Of EX_Usuario)

    Protected Overrides Sub OnModelCreating(ByVal modelBuilder As DbModelBuilder)
        modelBuilder.Entity(Of EX_MensajeChat)() _
            .Property(Function(e) e.Mensaje) _
            .IsUnicode(False)

        modelBuilder.Entity(Of EX_MensajeChat)() _
            .Property(Function(e) e.FechaRegistro) _
            .IsUnicode(False)

        modelBuilder.Entity(Of EX_Usuario)() _
            .Property(Function(e) e.Descripcion) _
            .IsUnicode(False)

        modelBuilder.Entity(Of EX_Usuario)() _
            .Property(Function(e) e.NombreUsuario) _
            .IsUnicode(False)

        modelBuilder.Entity(Of EX_Usuario)() _
            .Property(Function(e) e.Contraseña) _
            .IsUnicode(False)

        modelBuilder.Entity(Of EX_Usuario)() _
            .Property(Function(e) e.Foto) _
            .IsUnicode(False)

        modelBuilder.Entity(Of EX_Usuario)() _
            .HasMany(Function(e) e.INT_Mensaje) _
            .WithRequired(Function(e) e.INT_Usuario) _
            .HasForeignKey(Function(e) e.IdUsuarioEnvia) _
            .WillCascadeOnDelete(False)

        modelBuilder.Entity(Of EX_Usuario)() _
            .HasMany(Function(e) e.INT_Mensaje1) _
            .WithRequired(Function(e) e.INT_Usuario1) _
            .HasForeignKey(Function(e) e.IdUsuarioRecepciona) _
            .WillCascadeOnDelete(False)
    End Sub
End Class
