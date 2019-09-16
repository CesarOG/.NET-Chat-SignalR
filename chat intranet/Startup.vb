Imports Microsoft.Owin
Imports Owin

<Assembly: OwinStartup(GetType(ChatIntranet.Startup))>
Namespace ChatIntranet
    Public Class Startup
        Public Sub Configuration(ByVal app As IAppBuilder)
            app.MapSignalR()
        End Sub
    End Class
End Namespace