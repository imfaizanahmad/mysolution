﻿$(document).ready(function () {

    if ($('#Status').val() == "Complete") {
        $('a[data-select-all="selectunselect"]').hide();
    }

    //child camapaign post
    $(document).on('click', '#btnSubmitChild', function () {
        if (ValidateChildForm() === true) {
            $.ajax({
                type: "POST",
                url: '/ChildCampaign/save?button=' + "Submit",
                data: $("#frmChildCampaign").serialize(), // serializes the form's elements.
                success: function (data) {
                    if (data === "True") window.location = "/ChildList/ChildList";
                }
            });
        }
    });

    $(document).on('click', '#btnSaveDraftChild', function () {
        $.ajax({
            type: "POST",
            url: '/ChildCampaign/save?button=' + "Save Draft",
            data: $("#frmChildCampaign").serialize(), // serializes the form's elements.
            success: function (data) {
                if (data === "True") window.location = "/ChildList/ChildList";
            }
        });
    });

    $(document).on('click', '#btnDeleteChild', function () {
        $.ajax({
            type: "POST",
            url: '/ChildCampaign/Delete?childId=' + $('#Id').val() + '&_=' + (new Date()).getTime(),
            success: function (data) {
                if (data === "True") window.location = "/ChildList/ChildList";

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
            url: "/ChildCampaign/LoadBusinessLine",
            data: $("#frmChildCampaign").serialize(),
            success: function (data) {
                $("#dvFormChildCampaign").html(data);
            }
        });
    });

    $(document).on("change", "#Segments_Id", function () {
        $.ajax({
            type: "POST",
            url: "/ChildCampaign/LoadIndustry",
            data: $("#frmChildCampaign").serialize(),
            success: function (data) {
                $("#dvFormChildCampaign").html(data);
            }
        });
    });

    $(document).on("change", "#MasterCampaignId", function () {
        $.ajax({
            type: "POST",
            url: "/ChildCampaign/LoadMasterCampaign",
            data: $("#frmChildCampaign").serialize(),
            success: function (data) {
                $("#dvFormChildCampaign").html(data);
            }
        });
    });
});

function ValidateChildForm() {
    var flag = true;

    if ($('#MasterCampaignId').val() == "" || $('#MasterCampaignId').val() == "None selected.") {

        $('.validmsgMastercampaign').text("Please select Master Campaign").css("color", "#b94a48");
        $('.validmsgMastercampaign').show();
        flag = false;

    }
    else {
        $('.validmsgMastercampaign').hide();
    }

    if ($('#BusinessGroups_Id').val() == null) {

        $('.validmsgbusinesGp').text("Please select Business Group").css("color", "#b94a48");
        $('.validmsgbusinesGp').show();
        flag = false;

    }
    else {
        $('.validmsgbusinesGp').hide();
    }

    if ($('#Industries_Id').val() == null) {

        $('.validmsgbusinesIndustry').text("Please select Business Group").css("color", "#b94a48");
        $('.validmsgbusinesIndustry').show();
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

    if ($('#CampaignType').val() == "") {

        $('.validmsgsubcampaigntype').text("Please select Campaign Type").css("color", "#b94a48");
        $('.validmsgsubcampaigntype').show();
        flag = false;

    }
    else {
        $('.validmsgsubcampaigntype').hide();
    }


    if ($('#CampaignType').val() == "1") {
        if ($('#Industries_Id').val() == null) {

            $('.validmsgbusinesIndustry').text("Please select Industry").css("color", "#b94a48");
            $('.validmsgbusinesIndustry').show();
            flag = false;

        }
        else {
            $('.validmsgbusinesIndustry').hide();
        }
    }

    if ($('#CampaignType').val() == 0) {

        CampaignTypeval = "0";//$("#CampaignType option:selected").val();

        var BGArr = [];
        $('#BusinessGroups_Id :selected').each(function (i, selected) {
            BGArr.push($(selected).val());
        });

        if (BGArr.length != 1) {
            $('.validmsgSingleBGselect').text("You can not select multiple Business Group").css("color", "#b94a48");
            $('.validmsgSingleBGselect').show();
            flag = false;
        }
        else { $('.validmsgSingleBGselect').hide(); }
    }
    else {
        CampaignTypeval = $("#CampaignType option:selected").val();

        var SegArr = [];
        $('#Segments_Id :selected').each(function (i, selected) {
            SegArr.push($(selected).val());
        });

        if (SegArr.length !== 1) {
            $(".validmsgSingleSegment").text("You can not select multiple Segment").css("color", "#b94a48");
            $('.validmsgSingleSegment').show();
            flag = false;
        }
        else { $('.validmsgSingleSegment').hide(); }

    }



    if ($("#StartDate").find("input").val() == "") {
        $('.validmsgSdate').text("Please select start date").css("color", "#b94a48");
        $('.validmsgSdate').show();
        flag = false;

    }
    else {
        $('.validmsgSdate').hide();
    }

    if ($("#EndDate").find("input").val() == "") {
        $('.validmsgEdate').text("Please select end date").css("color", "#b94a48");
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

    if ($('#Name').val() == "") {
        $('.validmsgSubCamp').text("Please enter Sub Campaign Name").css("color", "#b94a48");
        $('.validmsgSubCamp').show();
        flag = false;

    }
    else {

        $('.validmsgSubCamp').hide();
    }

    if ($('#CampaignDescription').val() == "") {
        $('.validmsgSubCampDesc').text("Please enter Sub Campaign Description").css("color", "#b94a48");
        $('.validmsgSubCampDesc').show();
        flag = false;
    }
    else {

        $('.validmsgSubCampDesc').hide();
    }

    if ($('#Budget').val() == "") {
        $('.validmsgBudget').text("Please enter Budget").css("color", "#b94a48");
        $('.validmsgBudget').show();
        flag = false;
    }
    else {

        $('.validmsgBudget').hide();
    }
    return flag;
}