$(document).ready(function () {

    RemoveZeroFromMetric();
    
    //if ($("#SubCampaignType").val == 1) {
    //    $('.Industrymanage-mandate').show();
    //} else {
    //    $('.Industrymanage-mandate').hide();
    //}

    ($("#SubCampaignType").val == 1) ? $('.Industrymanage-mandate').show() : $('.Industrymanage-mandate').hide();

    //To make tactic type list single selection
    $('#TacticType_Id').removeAttr('multiple');

    if ($('#Status').val() == "Complete") {
        $('a[data-select-all="selectunselect"]').hide();
        $('a[data-target-id="BusinessGroups_Id"]').hide();
        $('a[data-target-id="Segments_Id"]').hide();
    }

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
                    if (data === "True") window.location = "/TacticCampaign/TacticCampaignList";
                }
            });
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
                data: { "jsonModel": JSON.stringify(sdata), "button": "Draft" },
                success: function (data) {
                    if (data === "True") window.location = "/TacticCampaign/TacticCampaignList";
                }
            });
        }
    });

    $(document).on('click', '#btnDeleteTactic', function () {
        ConfigurationModel.ConfirmationDialog('Confirmation !', 'Are you sure you want to delete?', function () {
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
                PreventSpecialChar();
                RemoveZeroFromMetric();
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

    $('#tblBenchmark tbody').on("click", ".removeRow", function () {
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
    if ($("#BusinessGroups_Id").val() != null) {
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
    }
}
//Load Industry
function funcLoadIndustry() {
    if ($("#Segments_Id").val() != null) {
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
    }
}



//Special character not allowed
function blockSpecialChar(e) {
    var k;
    document.all ? k = e.keyCode : k = e.which;
    return ((k > 64 && k < 91) || (k > 96 && k < 123) || k == 8 || k == 32 || (k >= 48 && k <= 57));
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
    if ($('#MasterCampaign_Id').val() == null || $('#MasterCampaign_Id').val() == 0 || $('#MasterCampaign_Id').val() == "") {

        $('.validmsgMastercampaign').text("Please select Master Campaign.").css("color", "#b94a48");
        $('.validmsgMastercampaign').show();
        flag = false;

    }
    else {
        $('.validmsgMastercampaign').hide();
    }

    if ($('#ChildCampaign_Id').val() == null || $('#ChildCampaign_Id').val() == 0 || $('#ChildCampaign_Id').val() == "") {

        $('.validmsgSubcampaign').text("Please select Sub Campaign").css("color", "#b94a48");
        $('.validmsgSubcampaign').show();
        flag = false;

    } else {
        $('.validmsgSubcampaign').hide();
    }

    var startdate = new Date($("#StartDate").datepicker("getDate"));
    var enddate = new Date($("#EndDate").datepicker("getDate"));

    var MCStartdate = new Date($("#MCStartDate").val());
    var MCEnddate = new Date($("#MCEndDate").val());

    var DisMCStartdate = (MCStartdate.getDate() + '/'+ (MCStartdate.getMonth() + 1) + '/' + MCStartdate.getFullYear());
    var DisMCEnddate = (MCEnddate.getDate() + '/' + (MCEnddate.getMonth() + 1) + '/' + MCEnddate.getFullYear());
    if ($("#StartDate").val() !== "" && $("#EndDate").val() !== "") {
        if (startdate < MCStartdate) {
            var msg =
                $('.validmsgDateMCcompare')
                    .text("Sub Campaign start and end date should be between Master Campaign date: " +
                        DisMCStartdate +
                        " to " +
                        DisMCEnddate +
                        "").css("color", "#b94a48");
            $('.validmsgDateMCcompare').show();
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
            flag = false;
        }
        else {
            $('.validmsgDateMCcompare').hide();
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

    //var trReach = $('#frmTacticCampaign').find('#tblBenchmark tbody tr.trReach');
    //var rachGoal = trReach.find('.goal').val();
    //var rachLow = trReach.find('.low').val();
    //var rachHigh = trReach.find('.high').val();

    //var trResponse = $('#frmTacticCampaign').find('#tblBenchmark tbody tr.trResponse');
    //var responseGoal = trResponse.find('.goal').val();
    //var responseLow = trResponse.find('.low').val();
    //var responseHigh = trResponse.find('.high').val();

    //if (rachGoal == 0 || rachLow == 0 || rachHigh == 0) {
    //    $('.validmsgReachMetric').text("Please fill atleast one Reach Metric").css("color", "#b94a48");
    //    $('.validmsgReachMetric').show();
    //    flag = false;
    //}
    //else {
    //    $('.validmsgReachMetric').hide();
    //}

    //if (responseGoal == 0 || responseLow == 0 || responseHigh == 0) {
    //    $('.validmsgResponseMetric').text("Please fill atleast one Response Metric").css("color", "#b94a48");
    //    $('.validmsgResponseMetric').show();
    //    flag = false;
    //}
    //else {
    //    $('.validmsgResponseMetric').hide();
    //}
    return flag;
}

function ValidateSubmitTacticForm() {
    var flag = true;
    if ($('#MasterCampaign_Id').val() == null || $('#MasterCampaign_Id').val() == 0 || $('#MasterCampaign_Id').val() == "") {

        $('.validmsgMastercampaign').text("Please select Master Campaign.").css("color", "#b94a48");
        $('.validmsgMastercampaign').show();
        flag = false;

    }
    else {
        $('.validmsgMastercampaign').hide();
    }

    if ($('#ChildCampaign_Id').val() == null || $('#ChildCampaign_Id').val() == 0 || $('#ChildCampaign_Id').val() == "") {

        $('.validmsgSubcampaign').text("Please select Sub Campaign").css("color", "#b94a48");
        $('.validmsgSubcampaign').show();
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

    if ($('#TacticType_Id').val() == null) {

        $('.validmsgTactictype').text("Please select Tactic Type").css("color", "#b94a48");
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

        $('.validmsgbusinesLine').text("Please select Business Line").css("color", "#b94a48");
        $('.validmsgbusinesLine').show();
        flag = false;

    } else {
        $('.validmsgbusinesLine').hide();
    }

    if ($('#Segments_Id').val() == null) {

        $('.validmsgbusinesSegment').text("Please select Segment").css("color", "#b94a48");
        $('.validmsgbusinesSegment').show();
        flag = false;

    } else {
        $('.validmsgbusinesSegment').hide();
    }

    if ($("#SubCampaignType").val == 1) {
        if ($('#Industries_Id').val() == null) {

            $('.validmsgbusinesIndustry').text("Please select Industry").css("color", "#b94a48");
            $('.validmsgbusinesIndustry').show();
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

    if ($('#subcampaigntype').val() == "") {

        $('.validmsgsubcampaigntype').text("Please select Type (Sub Campaign)").css("color", "#b94a48");
        $('.validmsgsubcampaigntype').show();
        flag = false;

    } else {
        $('.validmsgsubcampaigntype').hide();
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

    //var startdate = new Date($("#StartDate").find("input").val());
    //var enddate = new Date($("#EndDate").find("input").val());

    var startdate = new Date($("#StartDate").datepicker("getDate"));
    var enddate = new Date($("#EndDate").datepicker("getDate"));

    var MCStartdate = new Date($("#MCStartDate").val());
    var MCEnddate = new Date($("#MCEndDate").val());

    var DisMCStartdate = (MCStartdate.getDate() + '/' + (MCStartdate.getMonth() + 1) + '/' + MCStartdate.getFullYear());
    var DisMCEnddate = (MCEnddate.getDate() + '/' + (MCEnddate.getMonth() + 1) + '/' + MCEnddate.getFullYear());
    if ($("#StartDate").val() !== "" && $("#EndDate").val() !== "") {
        if (startdate < MCStartdate) {
            var msg =
                $('.validmsgDateMCcompare')
                    .text("Sub Campaign start and end date should be between Master Campaign date: " +
                        DisMCStartdate +
                        " to " +
                        DisMCEnddate +
                        "").css("color", "#b94a48");
            $('.validmsgDateMCcompare').show();
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
            flag = false;
        }
        else {
            $('.validmsgDateMCcompare').hide();
        }
    }

    if (startdate > enddate) {
        $('.validmsgDatecompare').text("End date can not less than start date").css("color", "#b94a48");
        $('.validmsgDatecompare').show();
        flag = false;
    } else {
        $('.validmsgDatecompare').hide();
    }

    if ($('#Name').val().trim() == "") {
        $('.validmsgtacticname').text("Please enter Tactic Name").css("color", "#b94a48");
        $('.validmsgtacticname').show();
        flag = false;

    } else {

        $('.validmsgtacticname').hide();
    }

    if ($('#TacticDescription').val().trim() == "") {
        $('.validmsgtacticdesc').text("Please enter Tactic Description & Goals").css("color", "#b94a48");
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
        flag = false;
    }
    else {
        $('.validmsgReachMetric').hide();
    }

    if (responseGoal == "" || responseLow == "" || responseHigh == "") {
        $('.validmsgResponseMetric').text("Please fill atleast one Response Metric").css("color", "#b94a48");
        $('.validmsgResponseMetric').show();
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
        alert("You can't enter more then 500 character in description field!")
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
    data.Id = $('#frmTacticCampaign').find('input[name="Id"]').val()
    data.MasterCampaign_Id = $('#frmTacticCampaign').find('#MasterCampaign_Id option:selected').val();
    data.ChildCampaign_Id = $('#frmTacticCampaign').find('#ChildCampaign_Id option:selected').val();

    data.TacticType_Id = [];
    data.TacticType_Id.push(parseInt($('#frmTacticCampaign').find('#TacticType_Id option:selected').val()));

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



    
    data.StartDate = $('#frmTacticCampaign').find("#StartDate").datepicker("getDate");
    data.EndDate = $('#frmTacticCampaign').find("#EndDate").datepicker("getDate");

    data.BusinessGroups_Id = [];
    $('#frmTacticCampaign').find('#BusinessGroups_Id').closest('.form-group').find('ul li.search-choice').each(function () {
        data.BusinessGroups_Id.push($('#frmTacticCampaign').find('#BusinessGroups_Id option').eq(parseInt($(this).find('a').attr('data-option-array-index'))).val());
    });

    data.Segments_Id = [];
    $('#frmTacticCampaign').find('#Segments_Id').closest('.form-group').find('ul li.search-choice').each(function () {
        data.Segments_Id.push($('#frmTacticCampaign').find('#Segments_Id option').eq(parseInt($(this).find('a').attr('data-option-array-index'))).val());
    });

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
function alpha(e) {
    if (document.getElementById("TacticDescription").value.length < 500) {
        var k;
        document.all ? k = e.keyCode : k = e.which;
        return ((k > 64 && k < 91) || (k > 96 && k < 123) || k == 8 || k == 32 || (k >= 48 && k <= 57));
    }
    else {
        alert("You can't enter more then 500 character in description field!")
        return false;
    }
}

function PreventSpecialChar() {
    $("#TacticDescription").bind('paste', function () {
        setTimeout(function () {
            //get the value of the input text
            var data = $('#TacticDescription').val();
            //replace the special characters to '' 
            var dataFull = data.replace(/[^\w\s]/gi, '');
            //set the new value of the input text without special characters
            $('#TacticDescription').val(dataFull);
        });

    });
}

function RemoveZeroFromMetric() {
    $('.mrminttostring').each(function () { if ($(this).val() == 0) { $(this).val(''); } });
}

