$(function () {
    TacticCampaignBindGrid($(this));
});

function TacticCampaignBindGrid(panel) {
    var sdata = {
    };

    $.ajax({
        type: 'get',
        contentType: "application/json",
        url: "/TacticCampaign/GetTacticCampaignList",
        data: JSON.stringify(sdata),
        success: function (dataset) {
            var table = panel.find('#tacticCampaignGrid').DataTable({
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
                "order": [[0, "desc"]],
                columns: [
                    { title: "Campaign Id", data: "Id" },
                    { title: "Name", data: "Name" },
                    { title: "Tactic Description & Goals", data: "TacticDescription" },
                    { title: "Status", data: "InheritStatus" },
                    { title: "Start date", data: "StartDate" },
                    { title: "End date", data: "EndDate" },
                    { title: "Action" },
                ],
                columnDefs: [
                        {
                            "targets": 6, //Action
                            "data": null,
                            "render": function (data, type, full, meta) {
                                if (data.Status && data.Status.toLowerCase() === 'active')
                                    return '<a href="/TacticCampaign/TacticCampaign?id=' + parseInt(data.Id.slice(1)) + '"  title="View/Edit Campaign" class="btn-mc-action"><span class="glyphicon glyphicon-pencil"></span></a> &nbsp;&nbsp;<button type="submit" title="Delete" campaignId=' + parseInt(data.Id.slice(1)) + ' class="btn btn-danger btn-xs btn-mc-action" value="Delete" disabled ><span class="glyphicon glyphicon-trash"></span></button>';
                                else
                                    return '<a href="/TacticCampaign/TacticCampaign?id=' + parseInt(data.Id.slice(1)) + '"  title="View/Edit Campaign" class="btn-mc-action"><span class="glyphicon glyphicon-pencil"></span></a> &nbsp;&nbsp;<button type="submit" title="Delete" campaignId=' + parseInt(data.Id.slice(1)) + ' class="btn btn-danger btn-xs btn-mc-action" value="Delete" data-toggle="modal"><span class="glyphicon glyphicon-trash"></span></button>';
                            }
                        },
                ]
            });

            panel.find('#tacticCampaignGrid tbody').on('click', '.btn-mc-action', function () {
                var tacticCampaignId = parseInt($(this).attr('campaignId'))
                var action = $(this).attr('title');
                sdata = {};
                sdata.Id = tacticCampaignId;
                var actionUrl = "";
                if (action === "Delete") {
                    actionUrl = "/TacticCampaign/DeleteTacticCampaign";
                    ConfigurationModel.ConfirmationDialog('Confirmation', 'Are you sure you want to delete?', function () {
                        $.ajax({
                            type: 'post',
                            contentType: "application/json",
                            url: actionUrl,
                            data: JSON.stringify(sdata),
                            success: function (result) {
                                if (panel.find('#tacticCampaignGrid')[0].childNodes.length > 0) {
                                    panel.find('#tacticCampaignGrid').dataTable().fnDestroy();
                                    TacticCampaignBindGrid(panel);
                                }
                            },
                            error: function (jqxhr, textStatus, error) {

                            }
                        });
                        $("#btnNoConfirmYesNo").find(".modal-footer").find(".btn-default").click();
                    });
                }
                //else {
                //    actionUrl = "/ChildCampaign/MasterCampaign?id=" + campaignId;
                //    window.location = actionUrl
                //}
            });
        },
        error: function (jqxhr, textStatus, error) {
            //panel.find('#loadingSpinner').hide();
        }
    });
}