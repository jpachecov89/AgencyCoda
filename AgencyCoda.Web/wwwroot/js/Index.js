'use strict'

var Home_Index = function () {
    var homeIndex = function () {
        let _tokenCoda = getCookie('_tokenCoda');

        if (_tokenCoda) {
            location.href = '/AllEvents';
        }

        $('#btnSignIn').on('click', function () {
            $.ajax({
                type: 'GET',
                url: url_base + '/oauth/redirecturl',
                success: function (data) {
                    location.href = data;
                }
            });
        });
    };

    return {
        init: function () {
            homeIndex();
        }
    };
}();

jQuery(document).ready(function () {
    Home_Index.init();
});