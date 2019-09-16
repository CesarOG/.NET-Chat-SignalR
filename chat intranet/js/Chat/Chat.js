

let IntervalVal;
$(function () {
    // Declare a proxy to reference the hub.
    let chatHub = $.connection.chatHub;
    registerClientMethods(chatHub);
    // Start Hub
    $.connection.hub.start().done(function () {
        registerEvents(chatHub);
    });
});
function registerEvents(chatHub) {
    let IdUsuario = $("#divIdUsuario").text();
    if (IdUsuario.length > 0) {
        chatHub.server.connect(IdUsuario);
    }
}
// Show Title Alert
function ShowTitleAlert(newMessageTitle, pageTitle) {
    if (document.title == pageTitle) {
        document.title = newMessageTitle;
    }
    else {
        document.title = pageTitle;
    }
}

function registerClientMethods(chatHub) {
    // Calls when user successfully logged in
    chatHub.client.onConnected = function (connectionId, UsuariosConectados) {
        $('#divConnectionId').text(connectionId);
        // Add All Users
        for (i = 0; i < UsuariosConectados.length; i++) {
            AddUser(chatHub, UsuariosConectados[i].ConnectionId, UsuariosConectados[i].IdUsuario, UsuariosConectados[i].UserName, UsuariosConectados[i].UserImage, UsuariosConectados[i].LoginTime);
        }
    };

    // On New User Connected
    chatHub.client.onNewUserConnected = function (connectionId, IdUsuario, userName, UserImg, logintime) {
        AddUser(chatHub, connectionId, IdUsuario, userName, UserImg, logintime);
    }

    // On User Disconnected
    chatHub.client.onUserDisconnected = function (id, userName) {
        $('#Div' + id).remove();
        let ctrId = 'private_' + id;
        //$('#' + ctrId).remove();
        let disc = $('<div class="disconnect">"' + userName + '" se desconecto.</div>');
        $(disc).hide();
        $('#divusers').prepend(disc);
        $(disc).fadeIn(200).delay(2000).fadeOut(200);
    }


    chatHub.client.sendPrivateMessage = function (windowId, fromUserIdUsuario, fromUserName, message, userimg, CurrentDateTime) {
        let ctrId = 'private_' + windowId;
        let CurrUser = $('#divIdUsuario').text();
        if ($('#' + ctrId).length == 0) {
            //OpenPrivateChatBox(chatHub, windowId, ctrId, fromUserName, userimg);
            chatHub.server.openPrivateChat(CurrUser, windowId, ctrId, fromUserName);
        }
        let Side = 'left';
        if (CurrUser == fromUserIdUsuario) {
            Side = 'right';
        }
        else {
            let Notification = 'Nuevo Mensaje de ' + fromUserName;
            IntervalVal = setInterval("ShowTitleAlert('Intranet Chat App', '" + Notification + "')", 800);
        }

        let divChatP = '<div class="contentMessages__message--side-' + Side + '">' +
            '<div class="contentMessages__message__info">' +
            ' <img class="contentMessages__message__img" src="' + userimg + '" alt="Message User Image">' +
            ' <div class="contentMessages__message__text" >' + message + '</div>' +
            '</div>' +

            '<div class="contentMessages__message__info">' +
            '<span class="contentMessages__message__info__user">' + fromUserName + '</span>' +
            '<span class="contentMessages__message__info__date">' + CurrentDateTime + '</span>' +
            '</div>' +

            '</div > ';

        $('#' + ctrId).find('#divMessage').append(divChatP);

        // Apply Slim Scroll Bar in Private Chat Box
        let ScrollHeight = $('#' + ctrId).find('#divMessage')[0].scrollHeight;
        $('#' + ctrId).find('#divMessage').slimScroll({
            height: 350,
            scrollTo: ScrollHeight
        });
    }

    chatHub.client.OpenPrivateChat = function (IdUserTo, ctrId, userName, ListaMensajes) {
        let div1 = `<div class="messageChat" id="${ctrId}">
                    <div class="messageChat__header">
                        <h5 class="messageChat__header__title">${userName}</h5>
                        <div class="messageChat__header__tools">           
                            <button id="btnMinimize" type="button" class="btn  btn-sm" data-toggle="collapse" data-target=".msj_${ctrId}">
                                <i class="fa fa-minus"></i>
                            </button>
                            <button id="btnClose" type="button" class="btn  btn-sm"><i class="fa fa-times"></i></button>
                        </div>
                    </div>
                    <div class="messageChat__body msj_${ctrId} collapse in">
                        <div id="divMessage" class="messageChat__body__contenMessages">`;
        Array.from(ListaMensajes).map(msj => {
            let Side = 'left';
            if (IdUserTo != msj.IdUsuarioEnvia) {
                Side = 'right';
            }
            div1 += '<div class="contentMessages__message--side-' + Side + '">' +
                '<div class="contentMessages__message__info">' +
                ' <img class="contentMessages__message__img" src="/images/dummy.png" alt="Message User Image">' +
                ' <div class="contentMessages__message__text" >' + msj.Mensaje + '</div>' +
                '</div>' +
                '<div class="contentMessages__message__info">' +
                '<span class="contentMessages__message__info__user">' + msj.INT_Usuario.Descripcion + '</span>' +
                '<span class="contentMessages__message__info__date">' + msj.FechaRegistro + '</span>' +
                '</div>' +
                '</div > ';
        });
        div1 += `</div>
                    </div>
                    <div class="messageChat__footer msj_${ctrId} collapse in">
                        <div class="messageChat__footer__tools">
                            <textarea rows="1" id="txtPrivateMessage" placeholder="Escribir mensaje ..." class="messageChat__footer__tools__text"></textarea>
                            <button type="button" id="btnSendMessage" class="messageChat__footer__tools__btn">Enviar</button>
                        </div>
                    </div>
                </div>`;
        let $div = $(div1);
        // Closing Private Chat Box
        $div.find('#btnClose').click(function () {
            $('#' + ctrId).remove();
        });
        // Send Button event in Private Chat
        $div.find("#btnSendMessage").click(function () {
            $textBox = $div.find("#txtPrivateMessage");
            let msg = $textBox.val();
            if (msg.length > 0) {
                $textBox.val('');
                chatHub.server.sendPrivateMessage(IdUserTo, msg);
            }
        });
        // Text Box event on Enter Button
        $div.find("#txtPrivateMessage").keypress(function (e) {
            if (e.which == 13) {
                e.preventDefault();
                $div.find("#btnSendMessage").click();
            }
        });
        // Append private chat div inside the main div 
        if ($(".messageChat").length === 3) {
            $(".messageChat:first-child").remove();
        }
        $('.content_msj').append($div);
        $div.find("#txtPrivateMessage").focus();
        // Apply Slim Scroll Bar in Private Chat Box
        let ScrollHeight = $('#' + ctrId).find('#divMessage')[0].scrollHeight;
        $('#' + ctrId).find('#divMessage').slimScroll({ height: 350 });
        $('#' + ctrId).find('#divMessage').slimScroll({ scrollTo: ScrollHeight });
    };

}

function AddUser(chatHub, connectionId, IdUsuario, userName, UserImg, logintime) {
    let userId = $('#divConnectionId').text();
    let fromIdUsuario = $("#divIdUsuario").text();
    let code;
    if (userId !== connectionId) {
        code = $('<div class="boxUsers__user" id="Div' + IdUsuario + '">' +
            '<img class="boxUsers__user__img" src="' + UserImg + '" alt="User Image" />' +
            '<div class="boxUsers__user__data">' +
            '<span class="boxUsers__user__data__name">' + userName + '</span>' +
            '<span class="boxUsers__user__data__date"><i class="fa fa-circle text-success"></i></span>' +
            '</div>' +
            '</div>');

        let UserLink = $('<a id="' + IdUsuario + '" class="user" >' + userName + '<a>');
        $(code).click(function () {
            let toIdUsuario = $(UserLink).attr('id');
            if (fromIdUsuario !== toIdUsuario) {
                let ctrId = 'private_' + toIdUsuario;
                //OpenPrivateChatBox(chatHub, toIdUsuario, ctrId, userName);
                if ($(`#${ctrId}`).length === 0) {
                    chatHub.server.openPrivateChat(fromIdUsuario, toIdUsuario, ctrId, userName);
                } else {
                    $(`#${ctrId}`).focus();
                }
            }
        });
    }
    $("#divusers").append(code);
}


btnSearch.addEventListener("click", e => {

});

inpSearchUser.addEventListener("keyup", e => {
    let value = e.target.value.toLowerCase();
    $(".boxUsers__user__data__name").each(function () {
        let name = $(this).text().toLowerCase().split(value).length;
        if (name > 1) {
            $(this).parents(".boxUsers__user").show();
        } else {
            $(this).parents(".boxUsers__user").hide();
        }
    });
    if (e.which === 13) {
        e.preventDefault();
        btnSearch.click();
    }
});

