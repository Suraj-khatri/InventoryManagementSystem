
$("#saveacknowledge").click(function () {
    var $btn = $(this);
    $btn.button('loading');
    var acknowlmessage = $('#acknowlmessage').val();
    var ReqMesId = $('#reqmesid').val();
    var DeptId = $('#deptid').val();

    var inreqmes = {};
    var inrecList = [];
    $("table tr:not(:first)").each(function () {
        var tdlist = $(this).find("td");
        var pItem = {
            product_id: $(tdlist[0]).text(),
            p_rate: $(tdlist[3]).text(),
            received_qty: $(tdlist[5]).html(),
        };
        inrecList.push(pItem);
    })
    inreqmes.id = ReqMesId;
    inreqmes.deptid = DeptId;
    inreqmes.inrecList = inrecList;
    inreqmes.Acknowledged_Message = acknowlmessage;
    $.ajax({
        url: "/api/InvMovementApi/AcknowledgeRequisition",
        type: "Post",
        contentType: "application/json",
        data: JSON.stringify(inreqmes),
        success: function (data) {
            $btn.button('reset');
            window.location.replace(data.RedirectUrl, true)
            alert("Successfully Acknowledge");
        },
        error: function (err) {
            $btn.button('reset');
            alert(err.responseJSON.Message);
        }
    });
});