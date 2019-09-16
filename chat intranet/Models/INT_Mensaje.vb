Imports System
Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema
Imports System.Data.Entity.Spatial

Partial Public Class EX_MensajeChat
    <Key>
    <DatabaseGenerated(DatabaseGeneratedOption.None)>
    Public Property IdMensaje As Integer

    Public Property IdUsuarioEnvia As Integer

    Public Property IdUsuarioRecepciona As Integer

    <Required>
    Public Property Mensaje As String

    <Required>
    <StringLength(20)>
    Public Property FechaRegistro As String

    Public Overridable Property INT_Usuario As EX_Usuario

    Public Overridable Property INT_Usuario1 As EX_Usuario
End Class
