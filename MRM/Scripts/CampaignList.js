$(function () {
    MasterCampaignBindGrid($(this));
    mySearchfunction($(this));
});

function mySearchfunction(panels)
{
    panels.find(".dataTables_filter input")
                .unbind() // Unbind previous default bindings
                .bind("input", function (e) { // Bind our desired behavior
                    // If the length is 3 or more characters, or the user pressed ENTER, search
                    debugger;
                    var dtable = panels.find('#masterCampaignGrid').dataTable().api();
                    if (this.value.length >= 3 || e.keyCode == 13) {
                        // Call the API search function
                       dtable.search(this.value).draw();
                }
                    // Ensure we clear the search if they backspace far enough
                    if (this.value == "") {
                       dtable.search(this.value).draw();
                    }
                    return;
                });

}

function MasterCampaignBindGrid(panel) {
    var sdata = {
    };

             panel.find('#masterCampaignGrid').DataTable({
                paging: true,
                responsive: true,
                serverSide: true,
                ordering: true,
                filter: true,                
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
                    { title: "Campaign Id", data: "DigitalID" },
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
                                if (data.Status && (data.Status.toLowerCase() === 'active' || data.Status.toLowerCase() === 'complete'))
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
            });
}