Imports System.Web.Mvc

Namespace Controllers
    Public Class ChatController
        Inherits Controller

        Dim objUsuarioRepository As New UsuarioRepository
        Dim usuario As New EX_Usuario

        Protected UploadFolderPath As String = "~/Uploads/"

        Function Login() As ActionResult
            Return View()
        End Function
        <HttpPost>
        Public Function Login(ByVal user As EX_Usuario) As ActionResult
            Dim result = objUsuarioRepository.Login(user)
            If Not result Is Nothing Then
                Session("IdUsuario") = result.IdUsuario
                Session("Usuario") = result.NombreUsuario
                Session("Descripcion") = result.Descripcion
                Session("Rol") = result.IdRol
                Return RedirectToAction("Chat")
            End If

            Return View()
        End Function
        Function Chat() As ActionResult
            If Session("Usuario") Is Nothing Then
                Return View("Login")
            End If
            usuario.IdUsuario = Session("IdUsuario")
            usuario.NombreUsuario = Session("Usuario")
            usuario.Descripcion = Session("Descripcion")
            usuario.IdRol = Session("Rol")
            usuario.Foto = objUsuarioRepository.GetUserImage(Session("Usuario"))
            Return View(usuario)
        End Function

    End Class
End Namespace