@Code
    ViewData("Title") = "Chat"
End Code
<link href="~/Content/chatIntranet.css" rel="stylesheet" />
<div class="hide">
    <div id="divIdUsuario">@Model.IdUsuario</div>
    <div id="divNombreUsuario">@Model.NombreUsuario</div>
    <div id="divDescripcion">@Model.Descripcion</div>
    <div id="divIdRol">@Model.IdRol</div>
    <div id="divFoto">@Model.Foto</div>
    <div id="divConnectionId"></div>
</div>
<div class="contentChat">
    <div class="contentChat__header">        
        <h5 class="contentChat__header__title">Usuarios Online</h5>
        <button id="btnMinimize" type="button" class="btn  btn-sm" data-toggle="collapse" data-target=".contentChat__body">
            <i class="fa fa-minus"></i>
        </button>
    </div>
    <div class="contentChat__body collapse in" >
        <div class="boxUsers" id="divusers">
            @* <div Class="boxUsers__user">
                <img src="~/images/DP/dummy.png" Class="boxUsers__user__img" alt="User Image" />
                <div Class="boxUsers__user__data">
                    <span Class="boxUsers__user__data__name">Ortega Godoy, César Marcelo </span>
                    <span Class="boxUsers__user__data__date">03/04/2019 12:30:10</span>
                    <span Class="boxUsers__user__data__date"><i class="fa fa-circle text-success"></i></span>
                </div>
                        </div>*@
        </div>
        <div class="searchUser" id="searchUser">
            <input type="text" id="inpSearchUser" class="searchUser__input" placeholder="Buscar Estudiante o Docente Online">
            <button class="searchUser__btn" id="btnSearch"><i class="fa fa-search"></i></button>
        </div>
    </div>
</div>
<div class="content_msj"></div>

@*<div class="messageChat hide">
    <div class="messageChat__header">
        <h5 class="messageChat__header__title">Ortega Godoy, César Marcelo</h5>
        <div class="messageChat__header__tools">
            <span id="MsgCountP" title="0 Nuevos Mensajes" class="badge bg- PWClass ">0</span>
            <button id="btnMinimize" type="button" class="btn  btn-sm">
                <i class="fa fa-minus"></i>
            </button>
            <button id="btnClose" type="button" class="btn  btn-sm"><i class="fa fa-times"></i></button>
        </div>
    </div>
    <div class="messageChat__body">
        <div id="divMessage" class="messageChat__body__contenMessages">
            @For index = 1 To 10

                @<div class="contentMessages__message--side-left">
                    <div class="contentMessages__message__info">
                        <img class="contentMessages__message__img" src="~/images/DP/dummy.png" alt="Message User Image">
                        <div class="contentMessages__message__text"> message </div>
                    </div>
                    <div class="contentMessages__message__info">
                        <span class="contentMessages__message__info__user">userName</span>
                        <span class="contentMessages__message__info__date">04/04/2019 10:30:15</span>
                    </div>
                </div>
                @<div class="contentMessages__message--side-right">
                    <div class="contentMessages__message__info">
                        <img class="contentMessages__message__img" src="~/images/DP/dummy.png" alt="Message User Image">
                        <div class="contentMessages__message__text"> message </div>
                    </div>
                    <div class="contentMessages__message__info">
                        <span class="contentMessages__message__info__user">userName</span>
                        <span class="contentMessages__message__info__date">04/04/2019 10:30:15</span>
                    </div>
                </div>
            Next
        </div>
    </div>
    <div class="messageChat__footer">
        <div class="messageChat__footer__tools">
            <textarea rows="1" id="txtPrivateMessage" placeholder="Type Message ..." class="messageChat__footer__tools__text"></textarea>
            <button type="button" id="btnSendMessage" class="messageChat__footer__tools__btn">Enviar</button>
        </div>
    </div>
</div>*@
<Script src="~/js/Chat/Chat.js"></Script>