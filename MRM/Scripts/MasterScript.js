var CampaignTypeval;
$(document).ready(function () {
    if ($('#Status').val() === "Complete") {
        $('a[data-select-all="selectunselect"]').hide();
    }

    //Master camapaign post
    $(document).on('click', '#btnSubmit', function () {
        if (ValidateMasterForm() === true) {
            $.ajax({
                type: "POST",
                url: '/MasterCampaign/save?button=' + "Submit",
                data: $("#frmMasterCampaign").serialize(), // serializes the form's elements.
                success: function (data) {
                    if (data === "True") window.location = "/MasterCampaign/CampaignList";
                }
            });
        }
    });

    $(document).on('click', '#btnSaveDraft', function () {

        $.ajax({
            type: "POST",
            url: '/MasterCampaign/save?button=' + "Save Draft",
            data: $("#frmMasterCampaign").serialize(), // serializes the form's elements.
            success: function (data) {
                if (data === "True") window.location = "/MasterCampaign/CampaignList";
            }
        });

    });



    $(document).on('click', '#btnDelete', function () {
        $.ajax({
            type: "POST",
            url: '/MasterCampaign/Delete?masterId=' + $('#Id').val() + '&_=' + (new Date()).getTime(),
            success: function (data) {
                if (data === "True") window.location = "/MasterCampaign/CampaignList";

            }
        });
    });

    $(document).on('click', 'a[data-select-all="selectunselect"]', function () {
        var selectallElement = $(this).attr('data-target-id');
        var nextStage = $(this).attr('data-next-stage');

        if (nextStage === "select") {
            $('#' + selectallElement + ' option').prop('selected', true);
            $(this).attr('data-next-stage', "unselect");
            $(this).text("Select None");

        } else {
            $('#' + selectallElement + ' option').prop('selected', false);
            $(this).attr('data-next-stage', "select");
            $(this).text("Select All");
        }

        $('#' + selectallElement).trigger('chosen:updated');
    });

    $(document).on("change", "#BusinessGroups_Id", function () {
        $.ajax({
            type: "POST",
            url: '/MasterCampaign/LoadBusinessLine',
            data: $("#frmMasterCampaign").serialize(),
            success: function (data) {
                $("#dvFormMasterCampaign").html(data);
            }
        });
    });

    $(document).on("change", "#Segments_Id", function () {
        $.ajax({
            type: "POST",
            url: '/MasterCampaign/LoadIndustry',
            data: $("#frmMasterCampaign").serialize(),
            success: function (data) {
                $("#dvFormMasterCampaign").html(data);
            }
        });
    });

});


function ValidateMasterForm() {
    var flag = true;


    if ($('#BusinessGroups_Id').val() == null) {

        $('.validmsgbusinesGp').text("Please select Business Group.").css("color", "#b94a48");
        $('.validmsgbusinesGp').show();
        flag = false;

    }
    else {
        $('.validmsgbusinesGp').hide();
    }

    if ($('#BusinessLines_Id').val() == null) {

        $('.validmsgbusinesLine').text("Please select Business Line").css("color", "#b94a48");
        $('.validmsgbusinesLine').show();
        flag = false;

    }
    else {
        $('.validmsgbusinesLine').hide();
    }

    if ($('#Segments_Id').val() == null) {

        $('.validmsgbusinesSegment').text("Please select Segment").css("color", "#b94a48");
        $('.validmsgbusinesSegment').show();
        flag = false;

    }
    else {
        $('.validmsgbusinesSegment').hide();
    }

    if ($('#Industries_Id').val() == null) {

        $('.validmsgbusinesIndustry').text("Please select industry from drop-down").css("color", "#b94a48");
        $('.validmsgbusinesIndustry').show();
        flag = false;

    }
    else {
        $('.validmsgbusinesIndustry').hide();
    }


    if ($('#Geographys_Id').val() === null) {

        $('.validmsgbusinesGeography').text("Please select geography from drop-down").css("color", "#b94a48");
        $('.validmsgbusinesGeography').show();
        flag = false;

    }
    else {
        $('.validmsgbusinesGeography').hide();
    }


    if ($("#StartDate").val() == "") {
        $('.validmsgSdate').text("Please select Start Date").css("color", "#b94a48");
        $('.validmsgSdate').show();
        flag = false;

    }
    else {
        $('.validmsgSdate').hide();
    }

    if ($("#EndDate").val() == "") {
        $('.validmsgEdate').text("Please select End Date").css("color", "#b94a48");
        $('.validmsgEdate').show();
        flag = false;
    }
    else {
        $('.validmsgEdate').hide();
    }

    var startdate = new Date($("#StartDate").find("input").val());
    var enddate = new Date($("#EndDate").find("input").val());

    if (startdate > enddate) {
        $('.validmsgDatecompare').text("End Date can not less than Start Date").css("color", "#b94a48");
        $('.validmsgDatecompare').show();
        flag = false;
    }
    else {
        $('.validmsgDatecompare').hide();
    }

    if ($('#Name').val().trim() == "") {
        $('.validmsgMaster').text("Please enter Master Campaign Name").css("color", "#b94a48");
        $('.validmsgMaster').show();
        flag = false;
    }
    else {
        $('.validmsgMaster').hide();
    }

    if ($('#CampaignDescription').val().trim() === "") {
        $('.validmsgMasterDesc').text("Please enter Master Campaign Description").css("color", "#b94a48");
        $('.validmsgMasterDesc').show();
        flag = false;
    }
    else {
        $('.validmsgMasterDesc').hide();
    }
    return flag;
}
//Prevent to user enter special character in Description Area.
function alpha(e) {

    if (document.getElementById("CampaignDescription").value.length < 500) {
        var k;
        document.all ? k = e.keyCode : k = e.which;
        return ((k > 64 && k < 91) || (k > 96 && k < 123) || k == 8 || k == 32 || (k >= 48 && k <= 57));
    }
    else {
        alert("You can't enter more then 500 character in description field!")
        return false;
    }
}

$(function () {
    $("#CampaignDescription").bind('paste', function () {
        setTimeout(function () {
            //get the value of the input text
            var data = $('#CampaignDescription').val();
            //replace the special characters to '' 
            var dataFull = data.replace(/[^\w\s]/gi, '');
            //set the new value of the input text without special characters
            $('#CampaignDescription').val(dataFull);
        });

    });
});
