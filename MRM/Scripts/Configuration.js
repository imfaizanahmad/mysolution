var ConfigurationModel = (function () {

    var CachedAsCookies = {
        searchedADUsers: []
    };   

    var AddUpdateDetailsPopUp = function (response) {
        response.modal({
            "backdrop": "static",
            "keyboard": true,
            "show": true
        });
    };

    var OpenPopUp = function (response) {
        response.modal({
            "backdrop": "static",
            "keyboard": false,
            "show": true
        });
        $('div').not('.fade').remove('.modal-backdrop');
    }

    var ConfirmationDialog = function (title, message, callback) {
        $('div').remove('.modal-backdrop');
        $('div').remove('#myModal');
        var dialog = $('<div id="myModal" class="modal fade">\
                    <div class="modal-dialog">\
                    <div class="modal-content">\
                    <div class="modal-header">\
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">\
                    <span aria-hidden="true">&times;</span>\
                    </button>\
                    <h4 id="lblTitleConfirmYesNo" class="modal-title">Confirmation</h4>\
                    </div>\
                    <div class="modal-body">\
                    <p id="lblMsgConfirmYesNo"></p>\
                    </div>\
                    <div class="modal-footer">\
                    <button id="btnYesConfirmYesNo" type="button" data-dismiss="modal" class="btn btn-primary">Yes</button>\
                    <button id="btnNoConfirmYesNo" type="button" data-dismiss="modal" class="btn btn-default">No</button>\
                    </div>\
                    </div>\
                    </div>\
                    </div>');

        dialog.find("#lblTitleConfirmYesNo").html(title);
        dialog.find("#lblMsgConfirmYesNo").html(message);

        dialog.find("#btnYesConfirmYesNo").on('click', function () {
            callback();
        });

        dialog.find("#btnNoConfirmYesNo").on('click', function () {
            dialog.find(".modal-footer").find(".btn-default").click();
        });
        AddUpdateDetailsPopUp(dialog);
    };

    var AlertDialog = function (title, message) {
        var dialog = $('<div id="AlertModal" class="modal fade">\
                    <div class="modal-dialog">\
                    <div class="modal-content">\
                    <div class="modal-header">\
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">\
                    <span aria-hidden="true">&times;</span>\
                    </button>\
                    <h4 id="lblTitle" class="modal-title">Confirmation</h4>\
                    </div>\
                    <div class="modal-body">\
                    <p id="lblMsg"></p>\
                    </div>\
                    <div class="modal-footer">\
                    <button id="btnConfirmNo" type="button" data-dismiss="modal" class="btn btn-default">Close</button>\
                    </div>\
                    </div>\
                    </div>\
                    </div>');

        dialog.find("#lblTitle").html(title);
        dialog.find("#lblMsg").html(message);

        //dialog.find("#btnConfirmNo").on('click', function () {
        //    dialog.find(".modal-footer").find(".btn-default").click();
        //});
        AddUpdateDetailsPopUp(dialog);
    };   

    var emailvalidate = function (text) {
        ///^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/
        var checkmail = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;
        if (!checkmail.test(text))
            return false;
        else
            return true;
    };
    var decimalvalidate = function decimalvalidate(event) {
        if (event.shiftKey == true) {
            event.preventDefault();
        }

        if ((event.keyCode >= 48 && event.keyCode <= 57) ||
            (event.keyCode >= 96 && event.keyCode <= 105) ||
            event.keyCode == 8 || event.keyCode == 9 || event.keyCode == 37 ||
            event.keyCode == 39 || event.keyCode == 46 || event.keyCode == 190) {

        } else {
            event.preventDefault();
        }

        if ($("#" + event.target.id).val().indexOf('.') !== -1 && event.keyCode == 190)
            event.preventDefault();
    };

    var numericvalidate = function numericvalidate(e) {
        if ($.inArray(e.keyCode, [8, 9, 27, 13, 110, 190]) !== -1 ||
            (e.keyCode === 65 && (e.ctrlKey === true || e.metaKey === true)) ||
            (e.keyCode >= 35 && e.keyCode <= 40)) {
            return;
        }
        if ((e.shiftKey || e.keyCode === 46 || (e.keyCode < 48 || e.keyCode > 57)) && (e.keyCode < 96 || e.keyCode > 105)) {
            e.preventDefault();
        }
    }; 
 



    return {
        ConfirmationDialog: function (title, message, callback) {
            ConfirmationDialog(title, message, callback);
        },
        AlertDialog: function (title, message) {
            AlertDialog(title, message);
        },       
        OpenPopUp: function (response) {
            OpenPopUp(response);
        },
        emailvalidate: function (text) {
            return emailvalidate(text);
        },
        decimalvalidate: function (event) {
            return decimalvalidate(event);
        },
        numericvalidate: function (event) {
            return numericvalidate(event);
        },
        CachedAsCookies: CachedAsCookies
    };

})();
