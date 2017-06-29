﻿var CampaignTypeval;
$(document).ready(function () {

    MasterCampaignBindGrid($(this));

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


    if ($("#StartDate").find("input").val() == "") {
        $('.validmsgSdate').text("Please select Start Date").css("color", "#b94a48");
        $('.validmsgSdate').show();
        flag = false;

    }
    else {
        $('.validmsgSdate').hide();
    }

    if ($("#EndDate").find("input").val() == "") {
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

    if ($('#Name').val() == "") {
        $('.validmsgMaster').text("Please enter Master Campaign Name").css("color", "#b94a48");
        $('.validmsgMaster').show();
        flag = false;
    }
    else {
        $('.validmsgMaster').hide();
    }

    if ($('#CampaignDescription').val() === "") {
        $('.validmsgMasterDesc').text("Please enter Master Campaign Description").css("color", "#b94a48");
        $('.validmsgMasterDesc').show();
        flag = false;
    }
    else {
        $('.validmsgMasterDesc').hide();
    }
    return flag;
}

function MasterCampaignBindGrid(panel) {
    //$('#loadingSpinner').show();
    var sdata = {
    };

    $.ajax({
        type: 'get',
        contentType: "application/json",
        url: "/MasterCampaign/GetMasterCampaignList",
        data: JSON.stringify(sdata),
        success: function (dataset) {
            debugger;
            var table = panel.find('#masterCampaignGrid').DataTable({
                paging: true,
                responsive: true,
                ordering: true,
                info: false,
                data: dataset,
                "autoWidth": false,
                "lengthChange": false,
                initComplete: function (settings, json) {
                    //$('#loadingSpinner').hide();
                },
                columns: [
                    { title: "Campaign Id", data: "Id" },
                    { title: "Name", data: "Name" },
                    { title: "Description", data: "CampaignDescription" },
                    { title: "Status", data: "Status" },
                    { title: "Start date", data: "StartDate" },
                    { title: "End date", data: "EndDate" },
                    { title: "Action" },
                ],
                columnDefs: [
                        {
                            "targets": 6, //Action
                            "data": null,
                            "render": function (data, type, full, meta) {
                                if (data.Status && data.Status.toLowerCase() != 'complete')
                                    return '<a href="#" title="View Campaign">View/Edit</a> &nbsp;&nbsp;<input type="button" title="Delete" class="btn btn-block btn-primary btn-sm btn-delete" value="Delete"  />';
                                else
                                    return '<a href="#" title="View Campaign">View/Edit</a> &nbsp;&nbsp;<input type="button" title="Delete" class="btn btn-block btn-sm btn-delete" value="Delete" disabled />';
                            }
                        },
                ]
            });

            panel.find('#masterCampaignGrid tbody').on('click', '.btn-delete', function () {
                debugger;
                var row = table.row($(this).parents('tr'));
                var data = row.data();
                var campaignId = parseInt(data.Id.slice(1))
                var action = $(this).attr('title');
                var actionUrl = "";
                if (action == "Delete") {
                    actionUrl = "";
                }
                else {
                    actionUrl = "";
                }
                sdata = {};
                sdata.Id = campaignId;
                $.ajax({
                    type: 'post',
                    contentType: "application/json",
                    url: actionUrl,
                    data: JSON.stringify(sdata),
                    success: function (result) {
                       
                    },
                    error: function (jqxhr, textStatus, error) {

                    }
                });
            });
        },
        error: function (jqxhr, textStatus, error) {
            //panel.find('#loadingSpinner').hide();
        }
    });
}
