'use strict'

var tokenData;

function deleteEvent(id) {
    $.ajax({
        url: url_base + '/oauth/deleteevent/' + id,
        headers: { 'access_token': tokenData.access_token },
        type: 'POST',
        complete: function () {
            location.href = '/AllEvents';
        }
    });
}

var Home_AllEvents = function () {
    var homeAllEvents = function () {
        let _tokenCoda = getCookie('_tokenCoda');

        if (!_tokenCoda) {
            location.href = '/';
        }

        tokenData = $.parseJSON(_tokenCoda);

        $.ajax({
            url: url_base + '/oauth/allevents',
            headers: { 'access_token': tokenData.access_token},
            type: 'GET',
            success: function (data) {
                $.each(data, function (i, row) {
                    $('#tblEvents tbody').append('<tr>' +
                        '<td>' + (i + 1) + '</td>' +
                        '<td>' + row.subject + '</td>' +
                        '<td>' + row.body.content + '</td>' +
                        '<td>' + row.start.dateTime + ' (' + row.start.timeZone + ')' + '</td>' +
                        '<td>' + row.end.dateTime + ' (' + row.end.timeZone + ')' +  '</td>' +
                        '<td><a href="/Edit?eventId=' + row.id + '">Editar</a> <a href="javascript:;" onclick="deleteEvent(\'' + row.id + '\')">Borrar</a></td>' +
                        '</tr > ');
                });
            },
            error: function (data) {
                alert(data);
            },
            failure: function (data) {
                alert(data);
            }
        });
    };

    return {
        init: function () {
            homeAllEvents();
        }
    };
}();

jQuery(document).ready(function () {
    Home_AllEvents.init();
});