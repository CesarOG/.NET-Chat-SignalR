Public Class ChatRepository
    Dim ctx As New ContextIntranet
    Public Function ListUserState() As List(Of EX_Usuario)

    End Function
    Public Function ListMessage(ByVal IdUsuarioEnvia As Integer, ByVal IdUsuarioRecepciona As Integer) As List(Of EX_MensajeChat)
        Using cn As New ContextIntranet
            Dim query As IQueryable(Of EX_MensajeChat) = From x In ctx.EX_MensajeChat
                                                         Where x.IdUsuarioEnvia = IdUsuarioEnvia And x.IdUsuarioRecepciona = IdUsuarioRecepciona Or
                                                            x.IdUsuarioEnvia = IdUsuarioRecepciona And x.IdUsuarioRecepciona = IdUsuarioEnvia
                                                         Select x

            Dim lista As List(Of EX_MensajeChat) = query.ToList
            Return lista
        End Using
    End Function
    Public Sub addMessage(ByVal mensaje As EX_MensajeChat)
        Using ctx As New ContextIntranet
            Dim id = 0
            If ctx.EX_MensajeChat.Count > 0 Then
                id = ctx.EX_MensajeChat.OrderByDescending(Function(o) o.IdMensaje).Select(Function(s) s.IdMensaje).First()
            End If
            mensaje.IdMensaje = id + 1
            ctx.EX_MensajeChat.Add(mensaje)
            ctx.SaveChanges()
        End Using
    End Sub
End Class
