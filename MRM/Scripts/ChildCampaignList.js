$(function () {
    ChildCampaignBindGrid($(this));
});

function ChildCampaignBindGrid(panel) {
    var sdata = {
    };

    $.ajax({
        type: 'get',
        contentType: "application/json",
        url: "/ChildCampaign/GetChildCampaignList",
        data: JSON.stringify(sdata),
        success: function (dataset) {
            var table = panel.find('#childCampaignGrid').DataTable({
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
                                if (data.Status && data.Status.toLowerCase() === 'active')
                                    return '<a href="/ChildCampaign/ChildCampaign?id=' + parseInt(data.Id.slice(1)) + '"  title="View/Edit Campaign" class="btn-mc-action"><span class="glyphicon glyphicon-pencil"></span></a> &nbsp;&nbsp;<button type="submit" title="Delete" campaignId=' + parseInt(data.Id.slice(1)) + ' class="btn btn-danger btn-xs btn-mc-action" value="Delete" disabled><span class="glyphicon glyphicon-trash"></span></button>';
                                else
                                    return '<a href="/ChildCampaign/ChildCampaign?id=' + parseInt(data.Id.slice(1)) + '"  title="View/Edit Campaign" class="btn-mc-action"><span class="glyphicon glyphicon-pencil"></span></a> &nbsp;&nbsp;<button type="submit" title="Delete" campaignId=' + parseInt(data.Id.slice(1)) + ' class="btn btn-danger btn-xs btn-mc-action" value="Delete" data-toggle="modal"><span class="glyphicon glyphicon-trash"></span></button>';
                            }
                        },
                ]
            });

            panel.find('#childCampaignGrid tbody').on('click', '.btn-mc-action', function () {
                var childCampaignId = parseInt($(this).attr('campaignId'))
                var action = $(this).attr('title');
                sdata = {};
                sdata.Id = childCampaignId;
                var actionUrl = "";
                if (action === "Delete") {
                    actionUrl = "/ChildCampaign/DeleteChildCampaign";
                    ConfigurationModel.ConfirmationDialog('Confirmation !', 'Are you sure you want to delete?', function () {
                        $.ajax({
                            type: 'post',
                            contentType: "application/json",
                            url: actionUrl,
                            data: JSON.stringify(sdata),
                            success: function (result) {
                                if (panel.find('#childCampaignGrid')[0].childNodes.length > 0) {
                                    panel.find('#childCampaignGrid').dataTable().fnDestroy();
                                    ChildCampaignBindGrid(panel);
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