Imports System
Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema
Imports System.Data.Entity.Spatial
Imports System.Runtime.Serialization
Imports System.Web.Script.Serialization
Imports Newtonsoft.Json

Partial Public Class EX_Usuario
    Public Sub New()
        INT_Mensaje = New HashSet(Of EX_MensajeChat)()
        INT_Mensaje1 = New HashSet(Of EX_MensajeChat)()
    End Sub

    <Key>
    <DatabaseGenerated(DatabaseGeneratedOption.None)>
    Public Property IdUsuario As Integer

    Public Property IdRol As Integer
    Public Property IdAgenda As String

    <StringLength(100)>
    Public Property Descripcion As String

    <Required>
    <StringLength(15)>
    Public Property NombreUsuario As String

    <Required>
    <StringLength(15)>
    Public Property Contraseña As String

    <StringLength(20)>
    Public Property Foto As String

    Public Property JefeTransversal As Boolean?

    Public Property EstadoCn As Boolean?

    Public Property Estado As Boolean?
    <JsonIgnore>
    <IgnoreDataMember>
    Public Overridable Property INT_Mensaje As ICollection(Of EX_MensajeChat)
    <JsonIgnore>
    <IgnoreDataMember>
    Public Overridable Property INT_Mensaje1 As ICollection(Of EX_MensajeChat)
End Class
