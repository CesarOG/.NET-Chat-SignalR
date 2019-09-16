Imports Microsoft.AspNet.SignalR

Namespace ChatIntranet
    Public Class ChatHub
        Inherits Hub

        Shared UsuariosConectados As List(Of INT_UsuarioOnline) = New List(Of INT_UsuarioOnline)()
        Dim mensaje As New EX_MensajeChat
        Dim objMensaje As New ChatRepository
        Private ctx As ContextIntranet = New ContextIntranet()

        Public Sub Connect(ByVal IdUsuario As Integer)
            Dim connectionId = Context.ConnectionId

            If UsuariosConectados.Where(Function(x) x.IdUsuario = IdUsuario).Count = 0 Then
                Dim userName As String = ctx.EX_Usuario.Where(Function(x) x.IdUsuario = IdUsuario).Select(Function(s) s.Descripcion).First()
                Dim UserImg As String = GetUserImage(IdUsuario)
                Dim logintime As String = DateTime.Now.ToString()
                UsuariosConectados.Add(New INT_UsuarioOnline With {
                    .IdUsuario = IdUsuario,
                    .ConnectionId = connectionId,
                    .UserName = userName,
                    .UserImage = UserImg,
                    .LoginTime = logintime
                })
                Clients.Caller.onConnected(connectionId, UsuariosConectados)
                Clients.AllExcept(connectionId).onNewUserConnected(connectionId, IdUsuario, userName, UserImg, logintime)
            End If
        End Sub
        Public Function GetUserImage(ByVal IdUsuario As Integer) As String
            Dim RetimgName As String = "/images/dummy.png"

            Try
                Dim ImageName As String = ctx.EX_Usuario.Where(Function(x) x.IdUsuario = IdUsuario).Select(Function(s) s.Foto).First()
                'If ImageName <> "" Then RetimgName = "/images/DP/" & ImageName
            Catch ex As Exception
            End Try

            Return RetimgName
        End Function

        Public Overrides Function OnDisconnected(ByVal stopCalled As Boolean) As System.Threading.Tasks.Task
            Dim item = UsuariosConectados.Where(Function(x) x.ConnectionId = Context.ConnectionId).FirstOrDefault()

            If item IsNot Nothing Then
                UsuariosConectados.Remove(item)
                Dim id = Context.ConnectionId
                Clients.All.onUserDisconnected(item.IdUsuario, item.UserName)
            End If

            Return MyBase.OnDisconnected(stopCalled)
        End Function
        Public Sub OpenPrivateChat(ByVal IdUserFrom As String, ByVal IdUserTo As String, ByVal ctrId As String, ByVal userName As String)
            Dim ListaMensajes = objMensaje.ListMessage(IdUserFrom, IdUserTo)
            Clients.Caller.OpenPrivateChat(IdUserTo, ctrId, userName, ListaMensajes)
        End Sub
        Public Sub SendPrivateMessage(ByVal toUserId As String, ByVal message As String)
            Dim fromUserId As String = Context.ConnectionId
            Dim toUser = UsuariosConectados.Where(Function(x) x.IdUsuario = toUserId).FirstOrDefault()
            Dim fromUser = UsuariosConectados.Where(Function(x) x.ConnectionId = fromUserId).FirstOrDefault()

            If toUser IsNot Nothing AndAlso fromUser IsNot Nothing Then
                Dim CurrentDateTime As String = DateTime.Now.ToString()
                Dim UserImg As String = GetUserImage(fromUser.IdUsuario)
                Clients.Client(toUser.ConnectionId).sendPrivateMessage(fromUser.IdUsuario, fromUser.IdUsuario, fromUser.UserName, message, UserImg, CurrentDateTime)
                Clients.Caller.sendPrivateMessage(toUserId, fromUser.IdUsuario, fromUser.UserName, message, UserImg, CurrentDateTime)
                'Agregando en BD
                mensaje.IdUsuarioEnvia = fromUser.IdUsuario
                mensaje.IdUsuarioRecepciona = toUser.IdUsuario
                mensaje.Mensaje = message
                mensaje.FechaRegistro = CurrentDateTime
                objMensaje.addMessage(mensaje)
                '
            End If
        End Sub
    End Class
End Namespace
