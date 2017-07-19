var CampaignTypeval;
$(document).ready(function () {
    if ($('#Status').val() === "Complete") {
        $('a[data-select-all="selectunselect"]').hide();
        $('a[data-target-id="BusinessGroups_Id"]').hide();
        $('a[data-target-id="Segments_Id"]').hide();
    }

    PreventSpecialChar();

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
        else {
            $("html, body").animate({ scrollTop: 0 }, "slow");
        }
    });

    $(document).on('click', '#btnSaveDraft', function () {
        if (ValidateSaveMasterForm() === true) {
            $.ajax({
                type: "POST",
                url: '/MasterCampaign/save?button=' + "Draft",
                data: $("#frmMasterCampaign").serialize(), // serializes the form's elements.
                success: function(data) {
                    if (data === "True") window.location = "/MasterCampaign/CampaignList";
                }
            });
        }
        else {
            $("html, body").animate({ scrollTop: 0 }, "slow");
        }
    });



    $(document).on('click', '#btnDelete', function () {
       
        ConfigurationModel.ConfirmationDialog('Confirmation', 'Are you sure you want to delete?', function () {
        $.ajax({
            type: "POST",
            url: '/MasterCampaign/Delete?masterId=' + $('#Id').val() + '&_=' + (new Date()).getTime(),
            success: function (data)
            {
                if (data === "True") window.location = "/MasterCampaign/CampaignList";
                
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

    $(document).on("change", "#BusinessGroups_Id", function () {
        $.ajax({
            type: "POST",
            url: '/MasterCampaign/LoadBusinessLine',
            data: $("#frmMasterCampaign").serialize(),
            success: function (data) {
                $("#dvFormMasterCampaign").html(data);
                PreventSpecialChar();
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
                PreventSpecialChar();
            }
        });
    });

});

//Load BusinessLine 
function funcLoadBusinessLine() {
    if ($("#BusinessGroups_Id").val() != null) {
        $.ajax({
            type: "POST",
            url: '/MasterCampaign/LoadBusinessLine',
            data: $("#frmMasterCampaign").serialize(),
            success: function (data) {
                $("#dvFormMasterCampaign").html(data);
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
            url: '/MasterCampaign/LoadIndustry',
            data: $("#frmMasterCampaign").serialize(),
            success: function (data) {
                $("#dvFormMasterCampaign").html(data);
                PreventSpecialChar();
            }
        });
    }
}

function ValidateSaveMasterForm() {
    var flag = true;
    if ($('#Name').val().trim() == "") {
        $('.validmsgMaster').text("Please enter Master Campaign Name").css("color", "#b94a48");
        $('.validmsgMaster').show();
        flag = false;
    }
    else {
        $('.validmsgMaster').hide();
    }

    if ($("#StartDate").val() != "") { $('.validmsgSdate').hide(); }
    if ($("#EndDate").val() != "") { $('.validmsgEdate').hide(); }

    if ($("#StartDate").val() != "" && $("#EndDate").val() != "") {
        var startdate = new Date($("#StartDate").datepicker("getDate"));
        var enddate = new Date($("#EndDate").datepicker("getDate"));

        if (startdate > enddate) {
            $('.validmsgDatecompare').text("End Date cannot be less than Start Date").css("color", "#b94a48");
            $('.validmsgDatecompare').show();
            flag = false;
        } else {
            $('.validmsgDatecompare').hide();
        }
    }
    //Hide valid messages
    $('.HideOnsave').hide();

    return flag;
}

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

        $('.validmsgbusinesIndustry').text("Please select Industry").css("color", "#b94a48");
        $('.validmsgbusinesIndustry').show();
        flag = false;

    }
    else {
        $('.validmsgbusinesIndustry').hide();
    }


    if ($('#Geographys_Id').val() === null) {

        $('.validmsgbusinesGeography').text("Please select Markets").css("color", "#b94a48");
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

    var startdate = new Date($("#StartDate").datepicker("getDate"));
    var enddate = new Date($("#EndDate").datepicker("getDate"));

    if (startdate > enddate) {
        $('.validmsgDatecompare').text("End Date cannot be less than Start Date").css("color", "#b94a48");
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
        $('.validmsgMasterDesc').text("Please enter Master Campaign Description & Goals").css("color", "#b94a48");
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

function PreventSpecialChar() {
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
}
