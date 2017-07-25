$(document).ready(function () {
    if ($('#Status').val() == "Complete") {
        $('a[data-select-all="selectunselect"]').hide();
        $('a[data-target-id="BusinessGroups_Id"]').hide();
        $('a[data-target-id="Segments_Id"]').hide();
    }

    if ($('#CampaignTypes').val() == 0)
    $('a[data-target-id="BusinessGroups_Id"]').hide();



    PreventSpecialChar();

    //child camapaign post
    $(document).on('click', '#btnSubmitChild', function () {

        if (ValidateChildForm() === true) {
            fFormatCurrency();
            $.ajax({
                type: "POST",
                url: '/ChildCampaign/save?button=' + "Submit",
                data: $("#frmChildCampaign").serialize(), // serializes the form's elements.
                success: function (data) {
                    if (data === "True") window.location = "/ChildCampaign/ChildCampaignList";
                }
            });
        }
        else {
            var pos = $(validationFocusId).offset().top;
            // animated top scrolling
            $('body, html').animate({ scrollTop: pos - 70 });
        }
    });

    $(document).on('click', '#btnSaveDraftChild', function() {
        if (ValidateChildSaveasDraft() === true) {
            fFormatCurrency();
            $.ajax({
                type: "POST",
                url: '/ChildCampaign/save?button=' + "Draft",
                data: $("#frmChildCampaign").serialize(), // serializes the form's elements.
                success: function(data) {
                    if (data === "True") window.location = "/ChildCampaign/ChildCampaignList";
                }
            });
        }
        else {
            var pos = $(validationFocusId).offset().top;
            // animated top scrolling
            $('body, html').animate({ scrollTop: pos - 70 });
        }
    });

    $(document).on('click', '#btnDeleteChild', function () {
        ConfigurationModel.ConfirmationDialog('Confirmation', 'Are you sure you want to delete?', function () {
            $.ajax({
                type: "POST",
                url: '/ChildCampaign/Delete?childId=' + $('#Id').val() + '&_=' + (new Date()).getTime(),
                success: function (data) {
                    if (data === "True") window.location = "/ChildCampaign/ChildCampaignList";

                }
            });
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
                PreventSpecialChar();
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
                PreventSpecialChar();
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
                PreventSpecialChar();

            }
        });
});



$(document).on("change", "#CampaignTypes", function () {
        $.ajax({
            type: "POST",
            url: "/ChildCampaign/OnChangeCampaignTypes",
            data: $("#frmChildCampaign").serialize(),
            success: function (data) {
                $("#dvFormChildCampaign").html(data);
                PreventSpecialChar();
            }
        });
    });


    //Click BusinessGroup for select -unselect
    $(document).on('click', 'a[data-target-id="BusinessGroups_Id"]', function () {

        var getselectedval = $("#BusinessGroups_Id").closest('.form-group').find('a[data-select-all="BGselectunselect"]');
        var selectallElement = getselectedval.attr('data-target-id');
        var nextStage = getselectedval.attr('data-next-stage');

        if (nextStage === "bgselect") {
            $('#' + selectallElement + ' option').prop('selected', true);
            getselectedval.attr('data-next-stage', "bgunselect");
            getselectedval.text("Select None");

        } else {
            $('#' + selectallElement + ' option').prop('selected', false);
            getselectedval.attr('data-next-stage', "bgselect");
            getselectedval.text("Select All");
        }
        $('#' + selectallElement).trigger('chosen:updated');

        //Load Business Line
        funcLoadBusinessLine();
    });
    //Click Segment for select -unselect
    $(document).on('click', 'a[data-target-id="Segments_Id"]', function () {
        var getselectedval = $("#Segments_Id").closest('.form-group').find('a[data-select-all="Segselectunselect"]');
        var selectallElement = getselectedval.attr('data-target-id');
        var nextStage = getselectedval.attr('data-next-stage');

        if (nextStage === "Segselect") {
            $('#' + selectallElement + ' option').prop('selected', true);
            getselectedval.attr('data-next-stage', "Segunselect");
            getselectedval.text("Select None");

        } else {
            $('#' + selectallElement + ' option').prop('selected', false);
            getselectedval.attr('data-next-stage', "Segselect");
            getselectedval.text("Select All");
        }
        $('#' + selectallElement).trigger('chosen:updated');

        //Load Industry
        funcLoadIndustry();
    });
});

// Replace string format from currency
function fFormatCurrency() {
    $('#Budget').val($('#Budget').val().replace(/\,/g, ''));
    $('#Spend').val($('#Spend').val().replace(/\,/g, ''));
}
//Load BusinessLine 
function funcLoadBusinessLine() {
    if ($("#BusinessGroups_Id").val() != null) {
        $.ajax({
            type: "POST",
            url: '/ChildCampaign/LoadBusinessLine',
            data: $("#frmChildCampaign").serialize(),
            success: function (data) {
                $("#dvFormChildCampaign").html(data);
                PreventSpecialChar();
            }
        });
    }
}
//Load Industry
function funcLoadIndustry() {
    if ($("#Segments_Id").val() != null) {
        $.ajax({
            type: "POST",
            url: '/ChildCampaign/LoadIndustry',
            data: $("#frmChildCampaign").serialize(),
            success: function (data) {
                $("#dvFormChildCampaign").html(data);
                PreventSpecialChar();
            }
        });
    }
}



//Numeric validation
function numericvalidate(evt) {
    var theEvent = evt || window.event;
    var key = theEvent.keyCode || theEvent.which;
    key = String.fromCharCode(key);
    var regex = /[0-9]|\./;
    if (!regex.test(key)) {
        theEvent.returnValue = false;
        if(theEvent.preventDefault) theEvent.preventDefault();
    }
}

var validationFocusId = null;
var validationFocusFlag = 0;


function ValidateChildSaveasDraft() {
    var flag = true;
    validationFocusId = null;
    validationFocusFlag = 0;

    if ($('#MasterCampaignId').val() == null || $('#MasterCampaignId').val() == "") {

        $('.validmsgMastercampaign').text("Please select Master Campaign.").css("color", "#b94a48");
        $('.validmsgMastercampaign').show();
        if (validationFocusFlag == 0) { validationFocusId = "#MC"; validationFocusFlag = 1; }
        flag = false;

    } else {
        $('.validmsgMastercampaign').hide();
    }

    if ($("#StartDate").val() != "") { $('.validmsgSdate').hide();}
    if ($("#EndDate").val() != "") { $('.validmsgEdate').hide(); }

    if ($("#StartDate").val() != "" && $("#EndDate").val() != "") {

        var startdate = new Date($("#StartDate").datepicker("getDate"));
        var enddate = new Date($("#EndDate").datepicker("getDate"));
        if (startdate > enddate) {
            $('.validmsgDatecompare').text("End Date cannot be less than Start Date").css("color", "#b94a48");
            $('.validmsgDatecompare').show();
            if (validationFocusFlag == 0) { validationFocusId = "#ED"; validationFocusFlag = 1; }
            flag = false;
        } else {
            $('.validmsgDatecompare').hide();
        }

        var MCStartdate = new Date($("#MCStartDate").val());
        var MCEnddate = new Date($("#MCEndDate").val());

        //var DisMCStartdate = (MCStartdate.getDate() +'/' +(MCStartdate.getMonth() + 1) +'/' +MCStartdate.getFullYear());
        //var DisMCEnddate = (MCEnddate.getDate() + '/' + (MCEnddate.getMonth() + 1) + '/' + MCEnddate.getFullYear());

        var DisMCStartdate = DateAsNokiaFormat(MCStartdate);
        var DisMCEnddate = DateAsNokiaFormat(MCEnddate);

        if ($("#StartDate").val() !== "" && $("#EndDate").val() !== "") {

            if ((startdate < MCStartdate)) {
                var msg =
                    $('.validmsgDateMCcompare')
                        .text("Sub Campaign start and end date should be between Master Campaign date: " +
                            DisMCStartdate +
                            " to " +
                            DisMCEnddate +
                            "").css("color", "#b94a48");
                $('.validmsgDateMCcompare').show();
                if (validationFocusFlag == 0) { validationFocusId = "#ED"; validationFocusFlag = 1; }
                flag = false;

            } else if (enddate > MCEnddate) {
                var msg =
                    $('.validmsgDateMCcompare')
                        .text("Sub Campaign start and end date should be between Master Campaign date: " +
                            DisMCStartdate +
                            " to " +
                            DisMCEnddate +
                            "").css("color", "#b94a48");
                $('.validmsgDateMCcompare').show();
                if (validationFocusFlag == 0) { validationFocusId = "#ED"; validationFocusFlag = 1; }
                flag = false;
            } else {
                $('.validmsgDateMCcompare').hide();
            }
        }
    }
    //Hide valid messages
    $('.HideOnsave').hide();
    CheckMasterAvailable();
    return flag;
}

function ValidateChildForm() {
    var flag = true;
    validationFocusId = null;
    validationFocusFlag = 0;

    if ($('#MasterCampaignId').val() == null || $('#MasterCampaignId').val() == "") {

        $('.validmsgMastercampaign').text("Please select Master Campaign").css("color", "#b94a48");
        $('.validmsgMastercampaign').show();
        if (validationFocusFlag == 0) { validationFocusId = "#MC"; validationFocusFlag = 1; }
        flag = false;
    }
    else {
        $('.validmsgMastercampaign').hide();
    }

    if ($('#Name').val().trim() == "") {
        $('.validmsgSubCamp').text("Please enter Sub Campaign Name").css("color", "#b94a48");
        $('.validmsgSubCamp').show();
        if (validationFocusFlag == 0) { validationFocusId = "#SCN"; validationFocusFlag = 1; }
        flag = false;

    }
    else {

        $('.validmsgSubCamp').hide();
    }

    if ($('#CampaignManager').val().trim() == "") {
        $('.validmsgSubCampmanager').text("Please enter Sub Campaign Manager").css("color", "#b94a48");
        $('.validmsgSubCampmanager').show();
        if (validationFocusFlag == 0) { validationFocusId = "#MCN"; validationFocusFlag = 1; }
        flag = false;
    }
    else {
        if (ConfigurationModel.emailvalidate($('#CampaignManager').val())) {
            $('.validmsgSubCampmanager').hide();
        }
        else {
            $('.validmsgSubCampmanager').text("Please enter valid Sub Campaign Manager").css("color", "#b94a48");
            $('.validmsgSubCampmanager').show();
            if (validationFocusFlag == 0) { validationFocusId = "#MCN"; validationFocusFlag = 1; }
            flag = false;
        }
    }

    if ($('#CampaignDescription').val().trim() == "") {
        $('.validmsgSubCampDesc').text("Please enter Sub Campaign Description & Goals").css("color", "#b94a48");
        $('.validmsgSubCampDesc').show();
        if (validationFocusFlag == 0) { validationFocusId = "#SDC"; validationFocusFlag = 1; }
        flag = false;
    }
    else {

        $('.validmsgSubCampDesc').hide();
    }



    if ($("#StartDate").val() == "") {
        $('.validmsgSdate').text("Please select Start Date").css("color", "#b94a48");
        $('.validmsgSdate').show();
        if (validationFocusFlag == 0) { validationFocusId = "#SD"; validationFocusFlag = 1; }
        flag = false;

    }
    else {
        $('.validmsgSdate').hide();
    }

    if ($("#EndDate").val() == "") {
        $('.validmsgEdate').text("Please select End Date").css("color", "#b94a48");
        $('.validmsgEdate').show();
        if (validationFocusFlag == 0) { validationFocusId = "#ED"; validationFocusFlag = 1; }
        flag = false;
    }
    else {
        $('.validmsgEdate').hide();
    }

    //var startdate = new Date($("#StartDate").find("input").val());
    //var enddate = new Date($("#EndDate").find("input").val());

    if ($("#EndDate").val() != "" && $("#StartDate").val() != "") {
        var startdate = new Date($("#StartDate").datepicker("getDate"));
        var enddate = new Date($("#EndDate").datepicker("getDate"));

        var MCStartdate = new Date($("#MCStartDate").val());
        var MCEnddate = new Date($("#MCEndDate").val());

     

        //var DisMCStartdate = (MCStartdate.getDate() +'/' +(MCStartdate.getMonth() + 1) +'/' +MCStartdate.getFullYear());
        //var DisMCEnddate = (MCEnddate.getDate() + '/' + (MCEnddate.getMonth() + 1) + '/' + MCEnddate.getFullYear());
        var DisMCStartdate = DateAsNokiaFormat(MCStartdate);
        var DisMCEnddate = DateAsNokiaFormat(MCEnddate);

        if ($("#StartDate").val() !== "" && $("#EndDate").val() !== "") {
            //if (startdate < MCStartdate || enddate > MCEnddate) {
            //    $('.validmsgDateMCcompare').text("Sub Campaign start and end date should be between Master Campaign date: " + DisMCStartdate + " to " + DisMCEnddate + "").css("color", "#b94a48");
            //    $('.validmsgDateMCcompare').show();
            //    flag = false;
            //}

            if (startdate < MCStartdate) {
                var msg =
                    $('.validmsgDateMCcompare')
                        .text("Sub Campaign start and end date should be between Master Campaign date: " +
                            DisMCStartdate +
                            " to " +
                            DisMCEnddate +
                            "").css("color", "#b94a48");
                $('.validmsgDateMCcompare').show();
                if (validationFocusFlag == 0) {
                    validationFocusId = "#ED";
                    validationFocusFlag = 1;
                }
                flag = false;

            } else if (enddate > MCEnddate) {
                var msg =
                    $('.validmsgDateMCcompare')
                        .text("Sub Campaign start and end date should be between Master Campaign date: " +
                            DisMCStartdate +
                            " to " +
                            DisMCEnddate +
                            "").css("color", "#b94a48");
                $('.validmsgDateMCcompare').show();
                if (validationFocusFlag == 0) {
                    validationFocusId = "#ED";
                    validationFocusFlag = 1;
                }
                flag = false;
            } else {
                $('.validmsgDateMCcompare').hide();
            }
        }

        if (startdate > enddate) {
            $('.validmsgDatecompare').text("End Date cannot be less than Start Date").css("color", "#b94a48");
            $('.validmsgDatecompare').show();
            if (validationFocusFlag == 0) {
                validationFocusId = "#ED";
                validationFocusFlag = 1;
            }
            flag = false;
        } else {
            $('.validmsgDatecompare').hide();
        }
    }
    if ($('#CampaignType').val() == "") {

        $('.validmsgsubcampaigntype').text("Please select Campaign Lead").css("color", "#b94a48");
        $('.validmsgsubcampaigntype').show();
        if (validationFocusFlag == 0) { validationFocusId = "#CL"; validationFocusFlag = 1; }
        flag = false;

    }
    else {
        $('.validmsgsubcampaigntype').hide();
    }


    if ($('#BusinessGroups_Id').val() == null || $('#BusinessGroups_Id').val()=="-1") {

        $('.validmsgbusinesGp').text("Please select Business Group").css("color", "#b94a48");
        $('.validmsgbusinesGp').show();
        if (validationFocusFlag == 0) { validationFocusId = "#BG"; validationFocusFlag = 1; }
        flag = false;

    }
    else {
        $('.validmsgbusinesGp').hide();
    }


    if ($('#CampaignTypes').val() == 0) {
        $('.validmsgSingleSegment').hide();
        CampaignTypeval = $("#CampaignType option:selected").val();

        var BGArr = [];
        $('#BusinessGroups_Id :selected').each(function (i, selected) {
            BGArr.push($(selected).val());
        });

        if (BGArr.length > 1) {
            $('.validmsgSingleBGselect').text("You can not select multiple Business Group").css("color", "#b94a48");
            $('.validmsgSingleBGselect').show();
            if (validationFocusFlag == 0) { validationFocusId = "#BG"; validationFocusFlag = 1; }
            flag = false;
        }
        else { $('.validmsgSingleBGselect').hide(); }
    }
    else {
        CampaignTypeval = $("#CampaignType option:selected").val();
        $('.validmsgSingleBGselect').hide();
        var SegArr = [];
        $('#Segments_Id :selected').each(function (i, selected) {
            SegArr.push($(selected).val());
        });

        if (SegArr.length > 1) {
            $(".validmsgSingleSegment").text("You can not select multiple Segment").css("color", "#b94a48");
            $('.validmsgSingleSegment').show();
            if (validationFocusFlag == 0) { validationFocusId = "#Seg"; validationFocusFlag = 1; }
            flag = false;
        }
        else { $('.validmsgSingleSegment').hide(); }

    }

    if ($('#Segments_Id').val() == null || $('#Segments_Id').val() == "-1") {

        $('.validmsgbusinesSegment').text("Please select Segment").css("color", "#b94a48");
        $('.validmsgbusinesSegment').show();
        if (validationFocusFlag == 0) { validationFocusId = "#Seg"; validationFocusFlag = 1; }
        flag = false;
    }
    else {
        $('.validmsgbusinesSegment').hide();
    }

    if ($('#BusinessLines_Id').val() == null) {

        $('.validmsgbusinesLine').text("Please select Business Line").css("color", "#b94a48");
        $('.validmsgbusinesLine').show();
        if (validationFocusFlag == 0) { validationFocusId = "#BL"; validationFocusFlag = 1; }
        flag = false;

    }
    else {
        $('.validmsgbusinesLine').hide();
    }

    if ($('#CampaignTypes').val() == 1) {
        if ($('#Industries_Id').val() == null) {

            $('.validmsgbusinesIndustry').text("Please select Industry").css("color", "#b94a48");
            $('.validmsgbusinesIndustry').show();
            if (validationFocusFlag == 0) { validationFocusId = "#Ind"; validationFocusFlag = 1; }
            flag = false;
        }
    }
    else {
        $('.validmsgbusinesIndustry').hide();
    }

   
    if ($('#Budget').val().trim() == "") {
        $('.validmsgBudget').text("Please enter Budget").css("color", "#b94a48");
        $('.validmsgBudget').show();
        if (validationFocusFlag == 0) { validationFocusId = "#Bud"; validationFocusFlag = 1; }
        flag = false;
    }
    else {

        $('.validmsgBudget').hide();
    }
   
    //if ($('#Spend').val() == "") {
    //    $('.validmsgSpend').text("Please enter spend.").css("color", "#b94a48");
    //    $('.validmsgSpend').show();
    //    flag = false;
    //}
    //else {

    //    $('.validmsgSpend').hide();
    //}
    CheckMasterAvailable();
    return flag;
}


//Check master availability
function CheckMasterAvailable()
{
    if ($("#MasterCampaignId").val() == "") {
        $('.validmsgsubcampaigntype').text("Please select Master Campaign first").css("color", "#b94a48");
        $('.validmsgsubcampaigntype').show();
        $('#CampaignTypes').attr("disabled", true).trigger("chosen:updated");
        return false;
    } else {
        $('.validmsgsubcampaigntype').hide();
        $('#CampaignTypes').attr("disabled", false).trigger("chosen:updated");
    }
}


//Prevent to user enter special character in Description Area.
function alpha(e, type) {
    var k;
    document.all ? k = e.keyCode : k = e.which;
    if (type.name == "CampaignDescription") {
        if (document.getElementById("CampaignDescription").value.length < 500) {
            return ((k != 44) && (k != 59) && (k != 47) && (k != 34) && (k != 38))
        }
        else {
            alert("You can't enter more then 500 character in description field!")
            return false;
        }
    }

    else {
        return ((k != 44) && (k != 59) && (k != 47) && (k != 34) && (k != 38))
        return false;

    }
}
function PreventSpecialChar() {
    $("#CampaignDescription").bind('paste', function () {
        setTimeout(function () {
            //get the value of the input text
            var data = $('#CampaignDescription').val();
            //replace the special characters to '' 
            //var dataFull = data.replace(/[^\w\s]/gi, '');
            var dataFull = data.replace(/[&/,";“”]/g, '');
            //set the new value of the input text without special characters
            $('#CampaignDescription').val(dataFull);
        });

    });

    $("#Name").bind('paste', function () {
        setTimeout(function () {
            var data = $('#Name').val();
            var dataFull = data.replace(/[&/,";“”]/g, '');
            $('#Name').val(dataFull);
        });

    });

}

//Date Format
function DateAsNokiaFormat(date) {
    var strArray = ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'];
    var d = date.getDate();
    var m = strArray[date.getMonth()];
    var y = date.getFullYear();
    return '' + (d <= 9 ? '0' + d : d) + ' ' + m + ' ' + y;
}
        // this example uses the id selector & no options passed    
        //jQuery(function ($) {
        //    $('#Budget').autoNumeric('init');
        //    $('#Spend').autoNumeric('init');
        //});



