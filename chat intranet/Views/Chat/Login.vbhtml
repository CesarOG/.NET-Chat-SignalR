@Code
    ViewData("Title") = "Login"
End Code
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>SignalR Chat : Login</title>
    <link href="~/Content/bootstrap.css" rel="stylesheet" />
    <link href="~/Content/style.css" rel="stylesheet" />
    <link href="~/Content/font-awesome.css" rel="stylesheet" />
    <link href="~/Content/icheck-bootstrap.css" rel="stylesheet" />
</head>
<body class="hold-transition login-page">
    -
    <form id="form1" runat="server" action="/Chat/Login" method="post">
        <div class="login-box">
            <div class="login-logo">
                <a href="Login.aspx"><b>SignalR </b>Chat App</a>
            </div>
            <!-- /.login-logo -->
            <div class="login-box-body">
                <p class="login-box-msg">Please Login to Proceed</p>
                <div class="form-group has-feedback">
                    <input type="text" id="NombreUsuario" name="NombreUsuario" class="form-control" placeholder="Email" required="required" />
                    <span class="glyphicon glyphicon-envelope form-control-feedback"></span>
                </div>
                <div class="form-group has-feedback">
                    <input type="password" id="Contraseña" name="Contraseña" class="form-control" placeholder="Password" required="required" autocomplete="off" />
                    <span class="fa fa-lock form-control-feedback"></span>
                </div>
                <div class="row">
                    <!-- /.col -->
                    <div class="col-xs-4">
                        <Button ID="btnSignIn" CssClass="btn btn-success btn-block btn-flat">Login</Button>
                    </div>

                </div>


            </div>
            <!-- /.login-box-body -->
        </div>
    </form>
    <script src="~/Scripts/jquery-3.2.1.min.js"></script>
    <script src="~/Scripts/bootstrap.min.js"></script>

</body>
</html>