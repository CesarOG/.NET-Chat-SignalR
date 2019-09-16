Public Class UsuarioRepository
    Dim ctx As New ContextIntranet

    Public Function Login(ByVal user As EX_Usuario) As EX_Usuario
        Using ctx As New ContextIntranet
            Dim result = ctx.EX_Usuario.Where(Function(x) x.NombreUsuario = user.NombreUsuario And x.Contraseña = user.Contraseña)
            Dim usuario As EX_Usuario
            If result.Count > 0 Then
                usuario = result.First()
            End If
            Return usuario
        End Using
    End Function
    Public Function GetUserImage(ByVal username As String) As String
        Dim RetimgName As String = "/images/dummy.png"

        Try
            Dim ImageName As String = ctx.EX_Usuario.Where(Function(x) x.NombreUsuario = username).Select(Function(s) s.Foto).First()
            If ImageName <> "" Then RetimgName = "/images/DP/" & ImageName
        Catch ex As Exception
        End Try

        Return RetimgName
    End Function
End Class
