var temppur = [];

$("#adddata").click(function () {
        tempproduct();
});

function tempproduct() {
    var Id = $('#prodname :selected').val();
    var tppname = $('#prodname :selected').text();
    var tpqty = $('#qty').val();
    var price = $('#price').val();
    if (tpqty != "") {
        $('#tadd').show();
        var tp = {};
        tp.qty = tpqty;
        tp.product_code = Id;
        tp.productname = tppname;

        $('#datalist').append('<tr>' +
            '<td id="pcode">' + Id + '</td >' +
            '<td id="pname">' + tppname + '</td >' +
            '<td id="pqty">' + tpqty + '</td >' +
            '<td id="">' + price + '</td >' +
            '<td>' + '<a class="btn btn-xs btnDelete" id=' + Id + '>Delete</a>' + '</td>' +
            '</tr >');
        temppur.push({ Id });
    }
    else {
        alert("Please enter quantity!")
    }
}

$("#datalist").on('click', '.btnDelete', function (event) {
    $(this).closest('tr').remove();
    temppur.splice(temppur.findIndex(x => x.Id == event.target.id), 1);
});

$("#saveorder").click(function () {
    var priority = $('#priority :selected').text();
    var forwardedbranch = $('#forwardedbranch :selected').val();
    var narration = $('#message').val();
    var approvedby = $('#approvedby :selected').val();
    var inreqmes = {};
    var inreqList = [];
    $("table tr:not(:first)").each(function () {
        var tdlist = $(this).find("td");
        var pItem = {
            asset_id: $(tdlist[0]).text(),
            qty: $(tdlist[2]).html(),
            price: $(tdlist[3]).html(),
        };
        inreqList.push(pItem);
    })
    inreqmes.inreqList = inreqList;
    inreqmes.narration = narration;
    inreqmes.priority = priority;
    inreqmes.forwarded_branch = forwardedbranch;
    inreqmes.approved_by = approvedby;
    $.ajax({
        url: "/api/AssetAquisitionApi/Create",
        type: "Post",
        contentType: "application/json",
        data: JSON.stringify(inreqmes),
        success: function (data) {
            window.location.replace(data.RedirectUrl, true)
            alert("successfully requested purchase order");
        },
        error: function (err) {
            alert(err.responseJSON.Message);
        }
    });
});

