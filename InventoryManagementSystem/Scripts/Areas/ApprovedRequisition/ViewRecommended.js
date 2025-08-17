$("#tbl_create").on("input", '#approveqty', function (e) {
    var reqqty = $(this).closest('tr').find('#requqty').text();
    var approveqty = $(this).closest('tr').find("#approveqty :input[type='number']").val();
    if (approveqty == "") {
        return 0;
    }
    if (parseInt(approveqty) >= parseInt(reqqty)) {
        alert("Approved Qty must be smaller or equal to Requested Qty.");
        $(this).closest('tr').find("#approveqty :input[type=number]").val(parseInt(reqqty));
    }
});

$("#saveapprove").click(function () {
    var $btn = $(this);
    $btn.button('loading');
    var approvemessage = $('#approvemessage').val();
    ReqMesId = $('#reqmesid').val();
    var inreqmes = {};
    var inreqList = [];
    $("table tr:not(:first)").each(function () {
        var tdlist = $(this).find("td");
        var pItem = {
            item: $(tdlist[0]).text(),
            Approved_Quantity: $(this).closest('tr').find("#approveqty :input[type='number']").val(),// $(tdlist[5]).val(),
        };
        inreqList.push(pItem);
    })
    inreqmes.id = ReqMesId;
    inreqmes.inreqList = inreqList;
    inreqmes.Approver_message = approvemessage;
    $.ajax({
        url: "/api/InvMovementApi/ApproveRequisition",
        type: "Post",
        contentType: "application/json",
        data: JSON.stringify(inreqmes),
        success: function (data) {
            $btn.button('reset');
            window.location.replace(data.RedirectUrl, true)
            alert("Successfully Approved");
        },
        error: function (err) {
            $btn.button('reset');
            alert(err.responseJSON.Message);
        }
    });
});

