'use strict'

var Home_Create = function () {
    var homeCreate = function () {
        let _tokenCoda = getCookie('_tokenCoda');

        if (!_tokenCoda) {
            location.href = '/';
        }

        let tokenData = $.parseJSON(_tokenCoda);

        $('#btnGuardar').on('click', function () {
            let data = {
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
                url: url_base + '/oauth/createevent',
                headers: {'access_token': tokenData.access_token},
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
            homeCreate();
        }
    }
}();

jQuery(document).ready(function () {
    Home_Create.init();
});