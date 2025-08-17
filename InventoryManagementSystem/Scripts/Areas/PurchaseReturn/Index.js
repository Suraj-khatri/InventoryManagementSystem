$("#saveorder").click(function (e) {
    var inpurreturn = {};
    var inpurreturnlist = [];
    $("table tr:not(:first)").each(function () {
        var tdlist = $(this).find("td");
        var Item = {
            ProductName: $(tdlist[0]).text(),
            Qty: $(tdlist[1]).text(),
            Rate: $(tdlist[2]).html(),
            Amount: $(tdlist[3]).html(),
            ReturnQty: $(tdlist[4]).find("input:input").val(),
        };
        inpurreturnlist.push(Item);
    })
    inpurreturn.InPurRetDetailList = inpurreturnlist;
    inpurreturn.Bill_No = $('#billno').html();
    inpurreturn.Bill_Id = $('#billid').html();
    inpurreturn.VendorName = $('#vendorname').text();
    inpurreturn.ReturnAll = $('#defaultUnchecked').is(':checked');
    inpurreturn.Narration = $('#narr').val();
    $.ajax({
        url: "/api/PurchaseApi/PurchaseReturnCreate",
        type: "Post",
        contentType: "application/json",
        data: JSON.stringify(inpurreturn),
        success: function (data) {
            window.location.replace(data.RedirectUrl, true)
            alert("successfully return purchase order");
        },
        error: function (err) {
            alert(err.responseJSON.Message);
        }
    });
});

//$('#tbldata').on('input', function (e) {
//    var input = $("#tbldata :input").val();
//    alert(input);
//});
$(document).ready(function () {
    $('td:nth-child(6),th:nth-child(6)').hide();
})

$("#defaultUnchecked").change(function () {
    if (this.checked) {
        $("#tbldata :input").val(' ');
        $('td:nth-child(5),th:nth-child(5)').hide();
        $('td:nth-child(6),th:nth-child(6)').show();
    }
    else {
        $('td:nth-child(5),th:nth-child(5)').show();
        $('td:nth-child(6),th:nth-child(6)').hide();
    }
});
