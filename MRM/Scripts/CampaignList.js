$(function () {
    MasterCampaignBindGrid($(this));
});

function MasterCampaignBindGrid(panel) {
    var sdata = {
    };

             panel.find('#masterCampaignGrid').DataTable({
                paging: true,
                responsive: true,
                serverSide: true,
                ordering: true,
                filter : false,
                info: false,
                //data: dataset,
                "autoWidth": false,
                "lengthChange": false,
                initComplete: function (settings, json) {
                    //$('#loadingSpinner').hide();
                },
                "order": [[0, "desc"]],
                "ajax": {
                    "url": "/MasterCampaign/GetMasterCampaignListByPage",
                    "type": "POST",
                    "datatype": "json"
                },
                columns: [
                    { title: "Campaign Id", data: "Id" },
                    { title: "Name", data: "Name" },
                    { title: "Master Campaign Description & Goals", data: "CampaignDescription" },
                    { title: "Campaign Manager", data: "CampaignManager" },
                    { title: "Status", data: "InheritStatus" },
                    { title: "Start date", data: "StartDate" },
                    { title: "End date", data: "EndDate" },
                    { title: "Created By", data: "CreatedBy" },
                    { title: "Action" },
                ],
                columnDefs: [
                        {
                            "targets": 8, //Action
                            "data": null,
                            "render": function (data, type, full, meta) {
                                if (data.Status && data.Status.toLowerCase() === 'active')
                                    return '<a href="/MasterCampaign/MasterCampaign?id=' + parseInt(data.Id.slice(1)) + '"  title="View/Edit Campaign" class="btn-mc-action"><span class="glyphicon glyphicon-pencil"></span></a> &nbsp;&nbsp;<button type="submit" title="Delete" campaignId=' + parseInt(data.Id.slice(1)) + ' class="btn btn-danger btn-xs btn-mc-action" value="Delete" disabled><span class="glyphicon glyphicon-trash"></span></button>';
                                else
                                    return '<a href="/MasterCampaign/MasterCampaign?id=' + parseInt(data.Id.slice(1)) + '"  title="View/Edit Campaign" class="btn-mc-action"><span class="glyphicon glyphicon-pencil"></span></a> &nbsp;&nbsp;<button type="submit" title="Delete" campaignId=' + parseInt(data.Id.slice(1)) + ' class="btn btn-danger btn-xs btn-mc-action" value="Delete" data-toggle="modal"><span class="glyphicon glyphicon-trash"></span></button>';
                            }
                        },
                ]
            });

            panel.find('#masterCampaignGrid tbody').on('click', '.btn-mc-action', function () {               
                var campaignId = parseInt($(this).attr('campaignId'))
                var action = $(this).attr('title');
                sdata = {};
                sdata.Id = campaignId;
                var actionUrl = "";
                if (action === "Delete") {
                    actionUrl = "/MasterCampaign/DeleteCampaign";
                    ConfigurationModel.ConfirmationDialog('Confirmation', 'Are you sure you want to delete?', function () {
                        $.ajax({
                            type: 'post',
                            contentType: "application/json",
                            url: actionUrl,
                            data: JSON.stringify(sdata),
                            success: function (result) {
                                if (panel.find('#masterCampaignGrid')[0].childNodes.length > 0) {
                                    panel.find('#masterCampaignGrid').dataTable().fnDestroy();                                   
                                    MasterCampaignBindGrid(panel);
                                }
                            },
                            error: function (jqxhr, textStatus, error) {

                            }
                        });
                        $("#btnNoConfirmYesNo").find(".modal-footer").find(".btn-default").click();
                    });
                }
                //else {
                //    actionUrl = "/MasterCampaign/MasterCampaign?id=" + campaignId;
                //    window.location = actionUrl
                //}
            });

    //$.ajax({
    //    type: 'get',
    //    contentType: "application/json",
    //    url: "/MasterCampaign/GetMasterCampaignList",
    //    data: JSON.stringify(sdata),
    //    success: function (dataset) {

    //    },
    //    error: function (jqxhr, textStatus, error) {
    //        //panel.find('#loadingSpinner').hide();
    //    }
    //});
}