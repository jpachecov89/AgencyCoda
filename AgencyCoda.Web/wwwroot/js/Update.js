'use strict'

var Home_Update = function () {
    var homeUpdate = function () {
        let _tokenCoda = getCookie('_tokenCoda');

        if (!_tokenCoda) {
            location.href = '/';
        }

        let tokenData = $.parseJSON(_tokenCoda);

        let urlParams = new URLSearchParams(window.location.search);
        let eventId = urlParams.get('eventId');

        if (eventId) {
            $.ajax({
                url: url_base + '/oauth/getevent?eventId=' + eventId,
                headers: { 'access_token': tokenData.access_token },
                type: 'GET',
                success: function (data) {
                    $('#subject').val(data.subject);
                    $('#bodyContent').val(data.body.content);
                    $('#startDatetime').val(data.start.dateTime);
                    $('#endDatetime').val(data.end.dateTime);
                }
            });
        }
        else {
            location.href = '/AllEvents';
        }

        $('#btnGuardar').on('click', function () {
            let data = {
                Id: eventId,
                Subject: $('#subject').val(),
                Body: {
                    Content: $('#bodyContent').val()
                },
                Start: {
                    DateTime: $('#startDatetime').val()
                },
                End: {
                    DateTime: $('#endDatetime').val()
                }
            };

            $.ajax({
                url: url_base + '/oauth/updateevent',
                headers: { 'access_token': tokenData.access_token },
                type: 'POST',
                dataType: 'json',
                contentType: "application/json;charset=UTF-8",
                data: JSON.stringify(data),
                complete: function () {
                    location.href = '/AllEvents';
                }
            });
        });
    };

    return {
        init: function () {
            homeUpdate();
        }
    };
}();

jQuery(document).ready(function () {
    Home_Update.init();
});