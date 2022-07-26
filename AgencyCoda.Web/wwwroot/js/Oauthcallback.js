'use strict'

function setCookie(name, value) {
    let date = new Date();
    date.setTime(date.getTime() + 3600000);
    let expires = date.toUTCString();
    document.cookie = name + "=" + (value || "") + ";expires=" + expires + ";path=/";
}

var Oauth_Callback = function () {
    var oauthCallback = function () {
        let urlParams = new URLSearchParams(window.location.search);
        let code = urlParams.get('code');

        if (code) {
            $.ajax({
                url: url_base + '/oauth/callback?code=' + code,
                type: 'GET',
                success: function (data) {
                    setCookie('_tokenCoda', data);
                    location.href = '/AllEvents';
                }
            });
        }
        else {
            location.href = '/Index';
        }
    };

    return {
        init: function () {
            oauthCallback();
        }
    };
}();

jQuery(document).ready(function () {
    Oauth_Callback.init();
});