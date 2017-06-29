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

        dialog.find("#btnConfirmNo").on('click', function () {
            dialog.find(".modal-footer").find(".btn-default").click();
        });
        AddUpdateDetailsPopUp(dialog);
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
        CachedAsCookies: CachedAsCookies
    };

})();
