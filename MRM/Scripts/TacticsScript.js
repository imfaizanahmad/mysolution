$(document).ready(function () {

    if ($('#Status').val() == "Complete") {
        $('a[data-select-all="selectunselect"]').hide();
    }

    //tactic camapaign post
    $(document).on('click', '#btnSubmitTactic', function () {
        if (ValidateTacticForm() === true) {
            $.ajax({
                type: "POST",
                url: '/TacticCampaign/save?button=' + "Submit",
                data: $("#frmTacticCampaign").serialize(), // serializes the form's elements.
                success: function (data) {
                    if (data === "True") window.location = "/TacticList/TacticList";
                }
            });
        }
    });

    $(document).on('click', '#btnSaveDrafttactic', function () {
        if (ValidateTacticSaveasDraft() === true)
            {
        $.ajax({
            type: "POST",
            url: '/TacticCampaign/save?button=' + "Save Draft",
            data: $("#frmTacticCampaign").serialize(), // serializes the form's elements.
            success: function (data) {
                if (data === "True") window.location = "/TacticList/TacticList";
           }
        });
     }
    });

    $(document).on('click', '#btnDeleteTactic', function () {
        $.ajax({
            type: "POST",
            url: '/TacticCampaign/Delete?tacticId=' + $('#Id').val() + '&_=' + (new Date()).getTime(),
            success: function (data) {
                if (data === "True") window.location = "/TacticList/TacticList";

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
            url: "/TacticCampaign/LoadBusinessLine",
            data: $("#frmTacticCampaign").serialize(),
            success: function (data) {
                $("#dvFormTacticCampaign").html(data);
            }
        });
    });

    $(document).on("change", "#Segments_Id", function () {
        $.ajax({
            type: "POST",
            url: "/TacticCampaign/LoadIndustry",
            data: $("#frmTacticCampaign").serialize(),
            success: function (data) {
                $("#dvFormTacticCampaign").html(data);
            }
        });
    });

    $(document).on("change", "#MasterCampaign_Id", function () {
        $.ajax({
            type: "POST",
            url: "/TacticCampaign/LoadMasterCampaign",
            data: $("#frmTacticCampaign").serialize(),
            success: function (data) {
                $("#dvFormTacticCampaign").html(data);
            }
        });
    });

    $(document).on("change", "#ChildCampaign_Id", function () {
        $.ajax({
            type: "POST",
            url: "/TacticCampaign/LoadChildCampaign",
            data: $("#frmTacticCampaign").serialize(),
            success: function (data) {
                $("#dvFormTacticCampaign").html(data);
            }
        });
    });

});

function ValidateTacticSaveasDraft() {
    var flag = true;
    if ($('#MasterCampaign_Id').val() == null || $('#MasterCampaign_Id').val() == 0) {

        $('.validmsgMastercampaign').text("Please select master campaign.").css("color", "#b94a48");
        $('.validmsgMastercampaign').show();
        flag = false;

    }
    else {
        $('.validmsgMastercampaign').hide();
    }

    if ($('#ChildCampaign_Id').val() == null || $('#ChildCampaign_Id').val() == 0) {

        $('.validmsgSubcampaign').text("Please select child Campaign").css("color", "#b94a48");
        $('.validmsgSubcampaign').show();
        flag = false;

    } else {
        $('.validmsgSubcampaign').hide();
    }

    return flag;
}

    function ValidateTacticForm() {
        var flag = true;

        if ($('#MasterCampaign_Id').val() == null || $('#MasterCampaign_Id').val() == 0) {

            $('.validmsgMastercampaign').text("Please select master campaign.").css("color", "#b94a48");
            $('.validmsgMastercampaign').show();
            flag = false;

        }
        else {
            $('.validmsgMastercampaign').hide();
        }

        if ($('#ChildCampaign_Id').val() == null || $('#ChildCampaign_Id').val() == 0) {

            $('.validmsgSubcampaign').text("Please select child Campaign").css("color", "#b94a48");
            $('.validmsgSubcampaign').show();
            flag = false;

        } else {
            $('.validmsgSubcampaign').hide();
        }

        if ($('#Vendor').val() === "") {

            $('.validmsgvendor').text("Please enter Vendor").css("color", "#b94a48");
            $('.validmsgvendor').show();
            flag = false;

        } else {
            $('.validmsgvendor').hide();
        }

        if ($('#Tactic_Id').val() == null) {

            $('.validmsgTactictype').text("Please select Vendor").css("color", "#b94a48");
            $('.validmsgTactictype').show();
            flag = false;

        } else {
            $('.validmsgTactictype').hide();
        }

        if ($('#ChildCampaign_Id').val() == null) {

            $('.validmsgSubcampaign').text("Please select Sub Campaign").css("color", "#b94a48");
            $('.validmsgSubcampaign').show();
            flag = false;

        } else {
            $('.validmsgSubcampaign').hide();
        }

        if ($('#BusinessGroups_Id').val() == null) {

            $('.validmsgbusinesGp').text("Please select Business Group").css("color", "#b94a48");
            $('.validmsgbusinesGp').show();
            flag = false;

        } else {
            $('.validmsgbusinesGp').hide();
        }

        if ($('#BusinessLines_Id').val() == null) {

            $('.validmsgbusinesLine').text("Please select business line from drop-down").css("color", "#b94a48");
            $('.validmsgbusinesLine').show();
            flag = false;

        } else {
            $('.validmsgbusinesLine').hide();
        }

        if ($('#Segments_Id').val() == null) {

            $('.validmsgbusinesSegment').text("Please select segment from drop-down").css("color", "#b94a48");
            $('.validmsgbusinesSegment').show();
            flag = false;

        } else {
            $('.validmsgbusinesSegment').hide();
        }

        if ($('#Industries_Id').val() == null) {

            $('.validmsgbusinesIndustry').text("Please select business line from drop-down").css("color", "#b94a48");
            $('.validmsgbusinesIndustry').show();
            flag = false;

        } else {
            $('.validmsgbusinesIndustry').hide();
        }

        if ($('#Geographys_Id').val() == null) {
            $('.validmsggeography').text("Please select geography from drop-down").css("color", "#b94a48");
            $('.validmsggeography').show();
            flag = false;

        } else {
            $('.validmsggeography').hide();
        }

        if ($('#subcampaigntype').val() == "") {

            $('.validmsgsubcampaigntype').text("Please select Type (Sub Campaign)").css("color", "#b94a48");
            $('.validmsgsubcampaigntype').show();
            flag = false;

        } else {
            $('.validmsgsubcampaigntype').hide();
        }


        if ($("#StartDate").find("input").val() == "") {
            $('.validmsgSdate').text("Please select start date").css("color", "#b94a48");
            $('.validmsgSdate').show();
            flag = false;

        } else {
            $('.validmsgSdate').hide();
        }

        if ($("#EndDate").find("input").val() == "") {
            $('.validmsgEdate').text("Please select end date").css("color", "#b94a48");
            $('.validmsgEdate').show();
            flag = false;
        } else {
            $('.validmsgEdate').hide();
        }

        var startdate = new Date($("#StartDate").find("input").val());
        var enddate = new Date($("#EndDate").find("input").val());
        if (startdate > enddate) {
            $('.validmsgDatecompare').text("End date can not less than start date").css("color", "#b94a48");
            $('.validmsgDatecompare').show();
            flag = false;
        } else {
            $('.validmsgDatecompare').hide();
        }

        if ($('#Name').val() == "") {
            $('.validmsgtacticname').text("Please enter sub campaign name").css("color", "#b94a48");
            $('.validmsgtacticname').show();
            flag = false;

        } else {

            $('.validmsgtacticname').hide();
        }

        if ($('#TacticDescription').val() == "") {
            $('.validmsgtacticdesc').text("Please enter sub campaign description name").css("color", "#b94a48");
            $('.validmsgtacticdesc').show();
            flag = false;
        } else {

            $('.validmsgSubCampDesc').hide();
        }

        if ($('#Year').val() == "") {
            $('.validmsgyear').text("Please enter Year").css("color", "#b94a48");
            $('.validmsgyear').show();
            flag = false;
        } else {

            $('.validmsgyear').hide();
        }
           
            return flag;
}
