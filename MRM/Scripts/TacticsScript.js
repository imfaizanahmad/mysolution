﻿$(document).ready(function () {

    RemoveZeroFromMetric();

    //if ($("#SubCampaignType").val == 1) {
    //    $('.Industrymanage-mandate').show();
    //} else {
    //    $('.Industrymanage-mandate').hide();
    //}

    ($("#SubCampaignType").val == 1) ? $('.Industrymanage-mandate').show() : $('.Industrymanage-mandate').hide();

    //To make tactic type list single selection
    $('#TacticType_Id').removeAttr('multiple');


    //if ($('#Status').val() == "Complete") {
    //    $('a[data-select-all="selectunselect"]').hide();
    //    $('a[data-target-id="BusinessGroups_Id"]').hide();
    //    $('a[data-target-id="Segments_Id"]').hide();
    //}

    PreventSpecialChar();

    //tactic camapaign post
    $(document).on('click', '#btnSubmitTactic', function () {
        var sdata = CollectTacticFormData();
        if (ValidateSubmitTacticForm() === true) {
            $.ajax({
                type: "POST",
                url: '/TacticCampaign/save',//?button=' + "Submit",
                data: { "jsonModel": JSON.stringify(sdata), "button": "Submit" },//$("#frmTacticCampaign").serialize(), // serializes the form's elements.
                success: function (data) {
                    if (data.Status === true) window.location = "/TacticCampaign/TacticCampaignList";
                }
            });
        }
        else {
            var pos = $(validationFocusId).offset().top;
            // animated top scrolling
            $('body, html').animate({ scrollTop: pos - 70 });
        }
    });

    $(document).on('click', '#btnSaveDrafttactic', function () {
        var sdata = CollectTacticFormData();
        if (ValidateTacticSaveasDraft() === true) {
            $.ajax({
                type: "POST",
                //url: '/TacticCampaign/save?button=' + "Save Draft",
                //data: $("#frmTacticCampaign").serialize(), //$("#frmTacticCampaign").serialize(), // serializes the form's elements.
                url: '/TacticCampaign/save',
                data: { "jsonModel": JSON.stringify(sdata), "button": "Save Draft" },
                success: function (data) {
                    if (data.Status === true) {
                        window.location = "/TacticCampaign/TacticCampaign?id=" + data.Result;
                    } //window.location = "/TacticCampaign/TacticCampaignList";
                }
            });
        }
        else {

            var pos = $(validationFocusId).offset().top;
            // animated top scrolling
            $('body, html').animate({ scrollTop: pos - 70 });
        }
    });

    $(document).on('click', '#btnDeleteTactic', function () {
        ConfigurationModel.ConfirmationDialog('Confirmation', 'Are you sure you want to delete?', function () {
            $.ajax({
                type: "POST",
                url: '/TacticCampaign/Delete?tacticId=' + $('#Id').val() + '&_=' + (new Date()).getTime(),
                success: function (data) {
                    if (data === "True") window.location = "/TacticCampaign/TacticCampaignList";

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
            url: "/TacticCampaign/LoadBusinessLine",
            data: $("#frmTacticCampaign").serialize(),
            success: function (data) {
                $("#dvFormTacticCampaign").html(data);
                $('#TacticType_Id').removeAttr('multiple');
                PreventSpecialChar();
                RemoveZeroFromMetric();
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
                $('#TacticType_Id').removeAttr('multiple');
                // PreventSpecialChar();
                // RemoveZeroFromMetric();
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
                $('#TacticType_Id').removeAttr('multiple');
                PreventSpecialChar();
                RemoveZeroFromMetric();
            }
        });
    });

    $(document).on("change", "#ChildCampaign_Id", function () {

        if ($("#MasterCampaign_Id").val() == "" || $("#MasterCampaign_Id").val() == "0") {
            $('.validmsgSubcampaign').text("Please select Master Campaign first").css("color", "#b94a48");
            $('.validmsgSubcampaign').show();
            return false;
        } else {
            $('.validmsgSubcampaign').hide();
        }

        $.ajax({
            type: "POST",
            url: "/TacticCampaign/LoadChildCampaign",
            data: $("#frmTacticCampaign").serialize(),
            success: function (data) {
                $("#dvFormTacticCampaign").html(data);
                $('#TacticType_Id').removeAttr('multiple');
                PreventSpecialChar();
                RemoveZeroFromMetric();
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

    $(document).on('click', '#btnAddReachRow', function () {
        var $options = $('#tblBenchmark tbody tr.trReach').find('.ddlMetricReach').html();
        var reachTblRow = $('<tr><td><label><input value="Reach" name="MetricType" type="hidden" /><input value="0" class="hdnMetric" type="hidden" /></label></td>\
                           <td><select id="MetricReach_Id" class="form-control ddlMetricReach chosen-single">' + $options + '</select></td>\
                           <td><input type="text" class="form-control goal" maxlength="50" name="ReachGoal" onkeypress="numericvalidate(event)" value="" /></td>\
                           <td><input type="text" class="form-control low" maxlength="50" name="ReachLow" onkeypress="numericvalidate(event)" value="" /></td>\
                           <td><input type="text" class="form-control high" maxlength="50" name="ReachHigh" onkeypress="numericvalidate(event)" value="" /></td>\
                           <td><input type="button" Id="btnRemoveReachRow" class="btn btn-primary btn-white removeRow" title="Remove Row" value="-" />\
                           <span class="validmsgReachMetric"></span></td>\
                       </tr>');

        $('#tblBenchmark tbody tr.trResponse').before(reachTblRow);
        reachTblRow.find('select').chosen().trigger('chosen:updated');

        if ($('#tblBenchmark tbody tr.trReach select.ddlMetricReach > option').length == $('#tblBenchmark tbody tr .ddlMetricReach').length) {
            $('#tblBenchmark tbody #btnAddReachRow').prop('disabled', true);
        }

        // DisableOptionBasedOnSelection('ddlMetricReach');
    });

    $(document).on('click', '#btnResponseRow', function () {
        var $options = $('#tblBenchmark tbody tr.trResponse').find('.ddlMetricResponse').html();

        var responseTblRow = $('<tr><td><label><input value="Response" name="MetricType" type="hidden" /><input value="0" class="hdnMetric" type="hidden" /></label></td>\
                           <td><select id="MetricResponse_Id" class="form-control ddlMetricResponse chosen-single">' + $options + '</select></td>\
                           <td><input type="text" class="form-control goal" maxlength="50" name="ResponseGoal" onkeypress="numericvalidate(event)" value="" /></td>\
                           <td><input type="text" class="form-control low" maxlength="50" name="ResponseLow" onkeypress="numericvalidate(event)" value="" /></td>\
                           <td><input type="text" class="form-control high" maxlength="50" name="ResponseHigh" onkeypress="numericvalidate(event)" value="" /></td>\
                           <td><input type="button" Id="btnRemoveResponseRow" class="btn btn-primary btn-white removeRow" title="Remove Row" value="-" />\
                            <span class="validmsgResponseMetric"></span></td>\
                       </tr>');

        $('#tblBenchmark tbody tr:last').after(responseTblRow);
        responseTblRow.find('select').chosen().trigger('chosen:updated');

        if ($('#tblBenchmark tbody tr.trResponse select.ddlMetricResponse > option').length == $('#tblBenchmark tbody tr .ddlMetricResponse').length) {
            $('#tblBenchmark tbody #btnResponseRow').prop('disabled', true);
        }

        // DisableOptionBasedOnSelection('ddlMetricResponse');

    });



    $(document).on('click', "#tblBenchmark tbody .removeRow", function () {
        $(this).closest("tr").remove();

        if ($('#tblBenchmark tbody tr.trReach select.ddlMetricReach > option').length >= $('#tblBenchmark tbody tr .ddlMetricReach').length) {
            $('#tblBenchmark tbody #btnAddReachRow').prop('disabled', false);

            DisableOptionBasedOnSelection('ddlMetricReach');
        }

        if ($('#tblBenchmark tbody tr.trResponse select.ddlMetricResponse > option').length >= $('#tblBenchmark tbody tr .ddlMetricResponse').length) {
            $('#tblBenchmark tbody #btnResponseRow').prop('disabled', false);

            DisableOptionBasedOnSelection('ddlMetricResponse');
        }
    });

    // BindMetricReachList($(this));

    // BindMetricResponseList($(this));

    $('#tblBenchmark tbody').on('change', 'select.ddlMetricReach', function () {
        $('#tblBenchmark tbody').find('.validmsgReachMetric').text('');
        DisableOptionBasedOnSelection('ddlMetricReach');
    });

    $('#tblBenchmark tbody').on('change', 'select.ddlMetricResponse', function () {
        $('#tblBenchmark tbody').find('.validmsgResponseMetric').text('');
        DisableOptionBasedOnSelection('ddlMetricResponse');
    });

    //Load at first time binding grid from server result
    //DisableOptionBasedOnSelection('ddlMetricReach');
    //DisableOptionBasedOnSelection('ddlMetricResponse');

});

var validationFocusId = null;
var validationFocusFlag = 0;

function DisableOptionBasedOnSelection(ddlType) {
    if (ddlType == 'ddlMetricReach') {
        var ddlMetricReach = $('#tblBenchmark tbody select.ddlMetricReach');
        ddlMetricReach.find('option').prop('disabled', false);
        ddlMetricReach.each(function () {
            ddlMetricReach.not(this).find('option[value="' + this.value + '"]').prop('disabled', true);
        });
    }
    else {
        var ddlMetricResponse = $('#tblBenchmark tbody select.ddlMetricResponse');
        ddlMetricResponse.find('option').prop('disabled', false);
        ddlMetricResponse.each(function () {
            ddlMetricResponse.not(this).find('option[value="' + this.value + '"]').prop('disabled', true);
        });
    }
}

//Load BusinessLine 
function funcLoadBusinessLine() {
    //  if ($("#BusinessGroups_Id").val() != null) {
    $.ajax({
        type: "POST",
        url: '/TacticCampaign/LoadBusinessLine',
        data: $("#frmTacticCampaign").serialize(),
        success: function (data) {
            $("#dvFormTacticCampaign").html(data);
            PreventSpecialChar();
            RemoveZeroFromMetric();
        }
    });
    // }
}
//Load Industry
function funcLoadIndustry() {
    // if ($("#Segments_Id").val() != null) {
    $.ajax({
        type: "POST",
        url: '/TacticCampaign/LoadIndustry',
        data: $("#frmTacticCampaign").serialize(),
        success: function (data) {
            $("#dvFormTacticCampaign").html(data);
            PreventSpecialChar();
            RemoveZeroFromMetric();
        }
    });
    // }
}

//Special character not allowed
function blockSpecialChar(e) {
    var k;
    document.all ? k = e.keyCode : k = e.which;
    return ((k != 44) && (k != 59) && (k != 47) && (k != 34) && (k != 38) && !(k >= 48 && k <= 57))
}

//Numeric validation
function numericvalidate(evt) {
    var theEvent = evt || window.event;
    var key = theEvent.keyCode || theEvent.which;
    key = String.fromCharCode(key);
    var regex = /[0-9]|\./;
    if (!regex.test(key)) {
        theEvent.returnValue = false;
        if (theEvent.preventDefault) theEvent.preventDefault();
    }
}

function ValidateTacticSaveasDraft() {
    var flag = true;
    validationFocusId = null;
    validationFocusFlag = 0;

    if ($('#MasterCampaign_Id').val() == null || $('#MasterCampaign_Id').val() == 0 || $('#MasterCampaign_Id').val() == "") {

        $('.validmsgMastercampaign').text("Please select Master Campaign.").css("color", "#b94a48");
        $('.validmsgMastercampaign').show();
        if (validationFocusFlag == 0) {
            validationFocusId = "#MC";
            validationFocusFlag = 1;
        }

        flag = false;

    }
    else {
        $('.validmsgMastercampaign').hide();
    }

    if ($('#ChildCampaign_Id').val() == null || $('#ChildCampaign_Id').val() == 0 || $('#ChildCampaign_Id').val() == "") {

        $('.validmsgSubcampaign').text("Please select Sub Campaign").css("color", "#b94a48");
        $('.validmsgSubcampaign').show();
        if (validationFocusFlag == 0) { validationFocusId = "#SC"; validationFocusFlag = 1; }
        flag = false;

    } else {
        $('.validmsgSubcampaign').hide();
    }

    if ($('#TacticType_Id').val() == null || $('#TacticType_Id').val() == "-1") {

        $('.validmsgTactictype').text("Please select Tactic Type").css("color", "#b94a48");
        $('.validmsgTactictype').show();
        if (validationFocusFlag == 0) { validationFocusId = "#TT"; validationFocusFlag = 1; }
        flag = false;

    } else {
        $('.validmsgTactictype').hide();
    }

    if ($("#StartDate").val() != "") { $('.validmsgSdate').hide(); }
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
            if (startdate < MCStartdate) {
                var msg =
                    $('.validmsgDateMCcompare')
                        .text("Tactic start and end date should be between Sub Campaign date: " +
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
                        .text("Tactic start and end date should be between Sub Campaign date: " +
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


    $('#frmTacticCampaign').find('#tblBenchmark tbody tr select.ddlMetricReach').each(function () {
        if ($('#tblBenchmark tbody tr select.ddlMetricReach').find('option[value="' + $(this).val() + '"]:selected').length > 1) {
            $(this).closest('tr').find('.validmsgReachMetric').text("Reach Metric selection should be different.").css("color", "#b94a48");
            flag = false;
            return flag;
        }
    });

    $('#frmTacticCampaign').find('#tblBenchmark tbody tr select.ddlMetricResponse').each(function () {
        if ($('#tblBenchmark tbody tr select.ddlMetricResponse').find('option[value="' + $(this).val() + '"]:selected').length > 1) {
            $(this).closest('tr').find('.validmsgResponseMetric').text("Response Metric selection should be different.").css("color", "#b94a48");
            flag = false;
            return flag;
        }
    });

    //Hide valid messages
    $('.HideOnsave').hide();
    return flag;
}

function ValidateSubmitTacticForm() {
    var flag = true;
    validationFocusId = null;
    validationFocusFlag = 0;

    if ($('#MasterCampaign_Id').val() == null || $('#MasterCampaign_Id').val() == 0 || $('#MasterCampaign_Id').val() == "") {

        $('.validmsgMastercampaign').text("Please select Master Campaign.").css("color", "#b94a48");
        $('.validmsgMastercampaign').show();
        if (validationFocusFlag == 0) { validationFocusId = "#MC"; validationFocusFlag = 1; }
        flag = false;


    }
    else {
        $('.validmsgMastercampaign').hide();
    }

    if ($('#ChildCampaign_Id').val() == null || $('#ChildCampaign_Id').val() == 0 || $('#ChildCampaign_Id').val() == "") {

        $('.validmsgSubcampaign').text("Please select Sub Campaign").css("color", "#b94a48");
        $('.validmsgSubcampaign').show();
        if (validationFocusFlag == 0) { validationFocusId = "#SC"; validationFocusFlag = 1; }
        flag = false;

    } else {
        $('.validmsgSubcampaign').hide();
    }

    //if ($('#Vendor').val().trim() === "") {

    //    $('.validmsgvendor').text("Please enter Vendor").css("color", "#b94a48");
    //    $('.validmsgvendor').show();
    //    flag = false;

    //} else {
    //    $('.validmsgvendor').hide();
    //}

    if ($('#TacticType_Id').val() == null || $('#TacticType_Id').val() == "-1") {

        $('.validmsgTactictype').text("Please select Tactic Type").css("color", "#b94a48");
        $('.validmsgTactictype').show();
        if (validationFocusFlag == 0) { validationFocusId = "#TT"; validationFocusFlag = 1; }
        flag = false;

    } else {
        $('.validmsgTactictype').hide();
    }

    if ($('#JournetStage_Id').val() == "") {

        $('.validmsgJourneyStage').text("Please select Journey Stage").css("color", "#b94a48");
        $('.validmsgJourneyStage').show();
        if (validationFocusFlag == 0) { validationFocusId = "#JS"; validationFocusFlag = 1; }
        flag = false;

    } else {
        $('.validmsgJourneyStage').hide();
    }
    if ($('#Name').val().trim() == "") {
        $('.validmsgtacticname').text("Please enter Tactic Name").css("color", "#b94a48");
        $('.validmsgtacticname').show();
        if (validationFocusFlag == 0) { validationFocusId = "#TN"; validationFocusFlag = 1; }
        flag = false;

    } else {

        $('.validmsgtacticname').hide();
    }

    if ($('#TacticDescription').val().trim() == "") {
        $('.validmsgtacticdesc').text("Please enter Tactic Description & Goals").css("color", "#b94a48");
        $('.validmsgtacticdesc').show();
        if (validationFocusFlag == 0) { validationFocusId = "#TDC"; validationFocusFlag = 1; }
        flag = false;
    } else {
        $('.validmsgtacticdesc').hide();
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
    if ($("#StartDate").val() != "" && $("#EndDate").val() != "") {
        var startdate = new Date($("#StartDate").datepicker("getDate"));
        var enddate = new Date($("#EndDate").datepicker("getDate"));

        var MCStartdate = new Date($("#MCStartDate").val());
        var MCEnddate = new Date($("#MCEndDate").val());

        //var DisMCStartdate = (MCStartdate.getDate() +'/' +(MCStartdate.getMonth() + 1) +'/' +MCStartdate.getFullYear());
        //var DisMCEnddate = (MCEnddate.getDate() + '/' + (MCEnddate.getMonth() + 1) + '/' + MCEnddate.getFullYear());
        var DisMCStartdate = DateAsNokiaFormat(MCStartdate);
        var DisMCEnddate = DateAsNokiaFormat(MCEnddate);

        if ($("#StartDate").val() !== "" && $("#EndDate").val() !== "") {
            if (startdate < MCStartdate) {

                $('.validmsgDateMCcompare')
                    .text("Tactic start and end date should be between Sub Campaign date: " +
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

                $('.validmsgDateMCcompare')
                    .text("Tactic start and end date should be between Sub Campaign date: " +
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
    if ($('#BusinessGroups_Id').val() == null || $('#BusinessGroups_Id').val() == "-1" || $('#BusinessGroups_Id').val() == 0) {

        $('.validmsgbusinesGp').text("Please select Business Group").css("color", "#b94a48");
        $('.validmsgbusinesGp').show();
        if (validationFocusFlag == 0) { validationFocusId = "#BG"; validationFocusFlag = 1; }
        flag = false;

    } else {
        $('.validmsgbusinesGp').hide();
    }

    if ($('#Segments_Id').val() == null || $('#Segments_Id').val() == "-1" || $('#Segments_Id').val() == 0) {

        $('.validmsgbusinesSegment').text("Please select Segment").css("color", "#b94a48");
        $('.validmsgbusinesSegment').show();
        if (validationFocusFlag == 0) { validationFocusId = "#SG"; validationFocusFlag = 1; }
        flag = false;


    } else {
        $('.validmsgbusinesSegment').hide();
    }

    if ($('#BusinessLines_Id').val() == null) {

        $('.validmsgbusinesLine').text("Please select Business Line").css("color", "#b94a48");
        $('.validmsgbusinesLine').show();
        if (validationFocusFlag == 0) { validationFocusId = "#BL"; validationFocusFlag = 1; }
        flag = false;

    } else {
        $('.validmsgbusinesLine').hide();
    }

    if ($("#SubCampaignType").val() == 1) {
        if ($('#Industries_Id').val() == null) {

            $('.validmsgbusinesIndustry').text("Please select Industry").css("color", "#b94a48");
            $('.validmsgbusinesIndustry').show();
            if (validationFocusFlag == 0) { validationFocusId = "#Ind"; validationFocusFlag = 1; }
            flag = false;

        } else {
            $('.validmsgbusinesIndustry').hide();
        }
    }

    //if ($('#Geographys_Id').val() == null) {
    //    $('.validmsggeography').text("Please select Markets").css("color", "#b94a48");
    //    $('.validmsggeography').show();
    //    flag = false;

    //} else {
    //    $('.validmsggeography').hide();
    //}


    if ($('#Year').val() == "") {
        $('.validmsgyear').text("Please enter Year").css("color", "#b94a48");
        $('.validmsgyear').show();
        flag = false;
    } else {

        $('.validmsgyear').hide();
    }

    $('#frmTacticCampaign').find('#tblBenchmark tbody tr select.ddlMetricReach').each(function () {
        if ($('#tblBenchmark tbody tr select.ddlMetricReach').find('option[value="' + $(this).val() + '"]:selected').length > 1) {
            $(this).closest('tr').find('.validmsgReachMetric').text("Reach Metric selection should be different.").css("color", "#b94a48");
            flag = false;
            if (validationFocusFlag == 0) { validationFocusId = "#Met"; validationFocusFlag = 1; }
            return flag;
        }
    });

    $('#frmTacticCampaign').find('#tblBenchmark tbody tr select.ddlMetricResponse').each(function () {
        if ($('#tblBenchmark tbody tr select.ddlMetricResponse').find('option[value="' + $(this).val() + '"]:selected').length > 1) {
            $(this).closest('tr').find('.validmsgResponseMetric').text("Response Metric selection should be different.").css("color", "#b94a48");
            flag = false;
            if (validationFocusFlag == 0) { validationFocusId = "#Met"; validationFocusFlag = 1; }
            return flag;
        }
    });

    var trReach = $('#frmTacticCampaign').find('#tblBenchmark tbody tr.trReach');
    var rachGoal = trReach.find('.goal').val();
    var rachLow = trReach.find('.low').val();
    var rachHigh = trReach.find('.high').val();

    var trResponse = $('#frmTacticCampaign').find('#tblBenchmark tbody tr.trResponse');
    var responseGoal = trResponse.find('.goal').val();
    var responseLow = trResponse.find('.low').val();
    var responseHigh = trResponse.find('.high').val();

    if (rachGoal == "" || rachLow == "" || rachHigh == "") {
        $('.validmsgReachMetric').text("Please fill atleast one Reach Metric").css("color", "#b94a48");
        $('.validmsgReachMetric').show();
        if (validationFocusFlag == 0) { validationFocusId = "#Met"; validationFocusFlag = 1; }
        flag = false;
    }
    else {
        $('.validmsgReachMetric').hide();
    }

    if (responseGoal == "" || responseLow == "" || responseHigh == "") {
        $('.validmsgResponseMetric').text("Please fill atleast one Response Metric").css("color", "#b94a48");
        $('.validmsgResponseMetric').show();
        if (validationFocusFlag == 0) { validationFocusId = "#Met"; validationFocusFlag = 1; }
        flag = false;
    }
    else {
        $('.validmsgResponseMetric').hide();
    }

    return flag;
}

//Prevent to user enter special character in Description Area.
function alpha(e) {
    if (document.getElementById("TacticDescription").value.length < 500) {
        var k;
        document.all ? k = e.keyCode : k = e.which;
        return ((k > 64 && k < 91) || (k > 96 && k < 123) || k == 8 || k == 32 || (k >= 48 && k <= 57));
    }
    else {
        alert("You can't enter more then 500 character in description field!");
        return false;
    }
}

function BindMetricReachList(panel) {
    var sdata = {
    };

    $.ajax({
        type: 'get',
        contentType: "application/json",
        url: "/Metric/GetMetricReachList",
        data: JSON.stringify(sdata),
        success: function (dataset) {
            var result = dataset.metricReachList;
            var options = '';
            if (result != null) {
                for (var i = 0; i < result.length; i++) {
                    options += '<option value="' + result[i].Id + '">' + result[i].Name + '</option>';
                }
                panel.find('#tblBenchmark').find('.ddlMetricReach').append(options);
                //panel.find('#tblBenchmark').find('.ddlMetricReach').trigger('chosen:updated');
            }
        },
        error: function (jqxhr, textStatus, error) {
            // debugger;
        }
    });
}

function BindMetricResponseList(panel) {
    var sdata = {
    };

    $.ajax({
        type: 'get',
        contentType: "application/json",
        url: "/Metric/GetMetricResponseList",
        data: JSON.stringify(sdata),
        success: function (dataset) {
            var result = dataset.metricResponseList;
            var options = '';
            if (result != null) {
                for (var i = 0; i < result.length; i++) {
                    options += '<option value="' + result[i].Id + '">' + result[i].Name + '</option>';
                }
                panel.find('#tblBenchmark').find('.ddlMetricResponse').append(options);
                //panel.find('#tblBenchmark').find('.ddlMetricResponse').trigger('chosen:updated');
            }
        },
        error: function (jqxhr, textStatus, error) {

        }
    });
}

function CollectTacticFormData() {
    var data = {
    };
    data.Id = $('#frmTacticCampaign').find('input[name="Id"]').val();
    if ($('#frmTacticCampaign').find('#JournetStage_Id option:selected').val() == "") {
        data.JournetStage_Id = 0;
    } else {
        data.JournetStage_Id = $('#frmTacticCampaign').find('#JournetStage_Id option:selected').val();
    }
    data.MasterCampaign_Id = $('#frmTacticCampaign').find('#MasterCampaign_Id option:selected').val();
    data.ChildCampaign_Id = $('#frmTacticCampaign').find('#ChildCampaign_Id option:selected').val();

    //data.TacticType_Id = [];
    //data.TacticType_Id.push(parseInt($('#frmTacticCampaign').find('#TacticType_Id option:selected').val()));

    if ($('#TacticType_Id option:selected').val() == "") {
        data.TacticType = 0;
    } else {
        data.TacticType = $('#TacticType_Id option:selected').val();
    }

    data.Name = $('#frmTacticCampaign').find('#Name').val();
    data.TacticDescription = $('#frmTacticCampaign').find('#TacticDescription').val();

    data.Themes_Id = [];
    $('#frmTacticCampaign').find('#Themes_Id').closest('.form-group').find('ul li.search-choice').each(function () {
        data.Themes_Id.push($('#frmTacticCampaign').find('#Themes_Id option').eq(parseInt($(this).find('a').attr('data-option-array-index'))).val());
    });

    data.Geographys_Id = [];
    $('#frmTacticCampaign').find('#Geographys_Id').closest('.form-group').find('ul li.search-choice').each(function () {
        data.Geographys_Id.push($('#frmTacticCampaign').find('#Geographys_Id option').eq(parseInt($(this).find('a').attr('data-option-array-index'))).val());
    });


    data.StartDate = $.datepicker.formatDate('mm/dd/yy', $("#StartDate").datepicker("getDate"));
    data.EndDate = $.datepicker.formatDate('mm/dd/yy', $("#EndDate").datepicker("getDate"));


    data.BusinessGroups_Id = [];
    if ($('#SubCampaignType').val() == 0) {
        data.BusinessGroups_Id.push($('#frmTacticCampaign').find('#BusinessGroups_Id option:selected').val());
    } else {
        $('#frmTacticCampaign').find('#BusinessGroups_Id').closest('.form-group').find('ul li.search-choice').each(function () {
            data.BusinessGroups_Id.push($('#frmTacticCampaign').find('#BusinessGroups_Id option').eq(parseInt($(this).find('a').attr('data-option-array-index'))).val());
        });
    }

    data.Segments_Id = [];
    if ($('#SubCampaignType').val() == 0) {
        $('#frmTacticCampaign').find('#Segments_Id').closest('.form-group').find('ul li.search-choice').each(
            function () {
                data.Segments_Id.push($('#frmTacticCampaign').find('#Segments_Id option')
                    .eq(parseInt($(this).find('a').attr('data-option-array-index'))).val());
            });
    } else {
        data.Segments_Id.push($('#frmTacticCampaign').find('#Segments_Id option:selected').val());
    }

    data.BusinessLines_Id = [];
    $('#frmTacticCampaign').find('#BusinessLines_Id').closest('.form-group').find('ul li.search-choice').each(function () {
        data.BusinessLines_Id.push($('#frmTacticCampaign').find('#BusinessLines_Id option').eq(parseInt($(this).find('a').attr('data-option-array-index'))).val());
    });

    data.Industries_Id = [];
    $('#frmTacticCampaign').find('#Industries_Id').closest('.form-group').find('ul li.search-choice').each(function () {
        data.Industries_Id.push($('#frmTacticCampaign').find('#Industries_Id option').eq(parseInt($(this).find('a').attr('data-option-array-index'))).val());
    });

    data.Vendor = $('#frmTacticCampaign').find('#Vendor').val();

    data.TacticCampaignReachResponseViewModels = [];
    $('#frmTacticCampaign').find('#tblBenchmark tbody tr').each(function () {
        data.TacticCampaignReachResponseViewModels.push({
            Id: $(this).find('input[class="hdnMetric"]').val(),
            MetricType: $(this).find('input[type="hidden"]').val(),
            MetricId: $(this).find('.form-control option:selected').val(),
            Goal: $(this).find('.goal').val() == "" ? 0 : $(this).find('.goal').val(),
            Low: $(this).find('.low').val() == "" ? 0 : $(this).find('.low').val(),
            High: $(this).find('.high').val() == "" ? 0 : $(this).find('.high').val()
        })
    });

    return data;
}

//Prevent to user enter special character in Description Area.
function alpha(e, type) {
    debugger;
    var k;
    document.all ? k = e.keyCode : k = e.which;
    if (type.name == "TacticDescription") {
        if (document.getElementById("TacticDescription").value.length < 500) {
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
    $("#TacticDescription").bind('paste', function () {
        setTimeout(function () {
            //get the value of the input text
            var data = $('#TacticDescription').val();
            //replace the special characters to '' 
            //var dataFull = data.replace(/[^\w\s]/gi, '');
            var dataFull = data.replace(/[&/,";“”]/g, '');
            //set the new value of the input text without special characters
            $('#TacticDescription').val(dataFull);
        });

    });

    $("#Name").bind('paste', function () {
        setTimeout(function () {
            var data = $('#Name').val();
            var dataFull = data.replace(/[&/,";“”]/g, '');
            $('#Name').val(dataFull);
        });

    });

    $("#Vendor").bind('paste', function () {
        setTimeout(function () {
            var data = $('#Vendor').val();
            var dataFull = data.replace(/[&/,";“”]/g, '');
            $('#Vendor').val(dataFull);
        });

    });

}


function RemoveZeroFromMetric() {
    $('.mrminttostring').each(function () { if ($(this).val() == 0) { $(this).val(''); } });
}

//Date Format
function DateAsNokiaFormat(date) {
    var strArray = ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'];
    var d = date.getDate();
    var m = strArray[date.getMonth()];
    var y = date.getFullYear();
    return '' + (d <= 9 ? '0' + d : d) + ' ' + m + ' ' + y;
}


//Digital Touchpoint
function ftacticData() {
    var Id = $('#frmTacticCampaign').find('input[name="Id"]').val();
    $.get("/TacticCampaign/GetDigitalTouchpoint?tacticId=" + Id + "", function (response) {
        if (response.Status) {
            $("#txtTacticID").val(response.Result.TacticCampaign_Id);
            $("#txtTacticName").val(response.Result.TacticName);
            $("#txtType").val(response.Result.TacticTypeName);
            DigitalGrid(response.Result);
            //$("#txtSource").val(response.TacticCampaign_Id);
            //$("#txtTacticName").val(response.TacticName);
            //$("#txtType").val(response.TacticTypeName);
            //$("#txtType").val(response.TacticTypeName);
        }
        else {
            ConfigurationModel.AlertDialog("Response", response.Message);
        }
    });
}
function EnableButton()
{
    $("#btnSave").removeAttr("disabled");
    $("#btnsubmit").removeAttr("disabled");
    $("#btnclose").removeAttr("disabled");
    
}
function DisableButton() {
    $("#btnSave").attr("disabled", "disabled");
    $("#btnsubmit").attr("disabled", "disabled");
    $("#btnclose").attr("disabled", "disabled");
}
function savedigitalpoint(status) {    
    var DigitalTouchPointViewModel = [];
    var tacticid = $('#frmTacticCampaign').find('input[name="Id"]').val();
    //var index = 0
    $('#gridReport tbody tr').each(function (index) {
        var obj = ($(this).context.id).split('_')[1];        
        if (obj != undefined) {
            var id = $("#hid" + obj).val();
            var gridstatus = $('#status' + obj + '').html();
       
            if (id == "0" || gridstatus === 'Draft') {
                var model = {
                    "Id": id,
                    "Source_Id": $('#hidsource' + obj + '').val(),
                    "Content": $('#content' + obj + '').html(),
                    "Medium_Id": $('#hidmedium' + obj + '').val(),
                    "Term": $('#term' + obj + '').html(),
                    "TacticType_Id": $('#TacticType_Id option:selected').val(),
                    "TacticCampaignId": tacticid,
                    "InheritStatus": status
                };
                DigitalTouchPointViewModel.push(model);
            }
        }
        
    });
    if (DigitalTouchPointViewModel != null && DigitalTouchPointViewModel.length != 0) {
        DisableButton();
        $.post("/TacticCampaign/AddDigitalTouchPoint", { model: DigitalTouchPointViewModel }, function (response) {           
            ConfigurationModel.AlertDialog("Message", response.Message);
            EnableButton();
            if (response.Status) {
                DigitalGrid(response.Result);
            }
        });
    }
    else {
        ConfigurationModel.AlertDialog("Message", "Please add atleast one digital row to save.");
    }
}
function fDigitalValidation() {
    $('#dvsource').hide();
    $('#dvMedium').hide();
    $('#dvContent').hide();
    $('#dvTerms').hide();
    var valid = false;
    if ($("#txtSource").val().trim() == "") {
        $("#dvsource").text("Please select Source").css("color", "#b94a48");
        $('#dvsource').show();
        valid = true;
    }
    if ($("#txtMedium").val().trim() == "") {
        $("#dvMedium").text("Please select Medium").css("color", "#b94a48");
        $('#dvMedium').show();
        valid = true;
    }
    if ($("#txtContent").val().trim() == "") {
        $("#dvContent").text("Please enter Content").css("color", "#b94a48");
        $('#dvContent').show();
        valid = true;
    }
    if ($("#txtTerms").val().trim() == "") {
        $("#dvTerms").text("Please enter Terms").css("color", "#b94a48");
        $('#dvTerms').show();
        valid = true;
    }
    return valid;
}
function AddRow() {
    if (fDigitalValidation()) {
        return false;
    }
    if ($('#hdnIndex').val()=="") {
        $('#hdnIndex').val("-1");
    }
    var index = Number($('#hdnIndex').val()) + 1;
    var responseTblRow = $('<tr id="tr_' + index + '">\<td id="digitalid' + index + '">0<input type="hidden" id="hid' + index + '" value="0"/></td>\
                           <td id="source' + index + '">' + $("#txtSource option:selected").text() + '<input type="hidden" id="hidsource' + index + '" value="' + $("#txtMedium").val() + '"/></td>\
                           <td id="medium' + index + '">' + $("#txtMedium option:selected").text() + '<input type="hidden" id="hidmedium' + index + '" value="' + $("#txtMedium").val() + '"/></td>\
                           <td id="content' + index + '">' + $("#txtContent").val() + '</td>\
                           <td id="term' + index + '">' + $("#txtTerms").val() + '</td>\
                           <td id="status' + index + '">Draft</td>\
                           <td><button type="button" onclick="removerow(' + index + ')" title="Delete" class="btn btn-danger btn-xs btn-mc-action" value="Delete"><span class="glyphicon glyphicon-trash"></span></button></td>\
                       </tr>');
    $('#gridReport tbody').append(responseTblRow);
    $('#hdnIndex').val(index);
    $("#txtSource").val("");
    $("#txtMedium").val("");
    $("#txtContent").val("");
    $("#txtTerms").val("");
    $('#hdnIndex').val(index);
    EnableButton();
    return false;
}
function removerow(id) {    
    var tr = $("#tr_" + id);    
    $('#gridReport tbody').find(tr).remove();
    return false;
}
function DigitalGrid(dataset) {
    $('#gridReport tbody').find('tr').remove();    
    $.each(dataset.DigitalTouchPoint, function (index,item) {
        var responseTblRow = $('<tr id="tr_' + index + '">\<td id="digitalid' + index + '">' + item.DisplayDigitalId + ' <input type="hidden" id="hid' + index + '" value="' + item.Id + '"/> </td>\
                           <td id="source' + index + '">' + item.Sources + '<input type="hidden" id="hidsource' + index + '" value="' + item.Source_Id + '"/></td>\
                           <td id="medium' + index + '">' + item.Medium + '<input type="hidden" id="hidmedium' + index + '" value="' + item.Medium_Id + '"/></td>\
                           <td id="content' + index + '">' + item.Content + '</td>\
                           <td id="term' + index + '">' + item.Term + '</td>\
                           <td id="status' + index + '">' + item.InheritStatus + '</td>\
                           <td><button ' + (item.InheritStatus == 'Complete' ? "disabled='disabled'" : "onclick='return DeleteSingleDigitalpoint(" + item.Id + ")'") + '  type="button" title="Delete" class="btn btn-danger btn-xs btn-mc-action" value="Delete"><span class="glyphicon glyphicon-trash"></span></button></td>\
                       </tr>');  
        $('#gridReport tbody').append(responseTblRow);        
        $('#hdnIndex').val(index);
    });
}

function DeleteSingleDigitalpoint(Id) {    
    var tacticid = $('#frmTacticCampaign').find('input[name="Id"]').val();
    DisableButton();
    $.get("/TacticCampaign/DeleteSingleDigitalPoint?digitalId=" + Id + "&tacticId=" + tacticid + "", function (response) {
        EnableButton();
        ConfigurationModel.AlertDialog("Response", response.Message);
        if (response.Status) {
            DigitalGrid(response.Result);
        }
    });
}
function fDeleteDigitalpoint() {
    var Id = $('#frmTacticCampaign').find('input[name="Id"]').val();
    $.get("/TacticCampaign/DeleteDigitalPoint?tacticId=" + Id + "", function (response) {
        ConfigurationModel.AlertDialog("Response", response.Message);
        EnableButton();
        if (response.Status) {
            DigitalGrid(response.Result);
        }
    });
}
//function SaveDigitalTouchpoint() {
//    data = [];
//    $('#tblBenchmark tbody tr').each(function () {
//        data.push({
//            Id: $(this).find('input[class="hdnMetric"]').val(),
//            MetricType: $(this).find('input[type="hidden"]').val(),
//            MetricId: $(this).find('.form-control option:selected').val(),
//            Goal: $(this).find('.goal').val() == "" ? 0 : $(this).find('.goal').val(),
//            Low: $(this).find('.low').val() == "" ? 0 : $(this).find('.low').val(),
//            High: $(this).find('.high').val() == "" ? 0 : $(this).find('.high').val()
//        })
//    });
//}
