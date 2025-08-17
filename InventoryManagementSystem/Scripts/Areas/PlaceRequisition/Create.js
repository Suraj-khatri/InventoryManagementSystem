var temppur = [];
$(document).ready(function () {
    $('#prodgroup').change();
})

$('#prodgroup').change(function () {
    $('#prodname').empty();
    $.ajax({
        url: '/PlaceRequisition/GetProductNameFromProductGroupName',
        type: 'POST',
        data: { groupid: $('#prodgroup').val() },
        success: function (data) {
            $.each(data, function (i, item) {
                $('#prodname').append(new Option(item.Text, item.Value));
            })
            $('#prodname').change();
        },
        async: false
    });
});

$('#prodname').change(function () {
    var tppname = $('#prodname :selected').text();
    $('#qty').val("");
    if (tppname.trim() != "--Select--") {
        $.ajax({
            url: '/PlaceRequisition/GetUnitForProduct',
            type: 'POST',
            data: { prodname: $('#prodname').val() },
            success: function (data) {
                $('#unit').val(data)
                $('#unit').text(data)
            }
        });
    }
});


$("#adddata").click(function () {
    var Id = $('#prodname :selected').val();
    if (temppur.find(x => x.Id == Id)) {
        alert("This Product has been already added!");
        $("#prodname").focus();
    }
    else if (temppur.length > 24) {
        alert("Sorry, 25 products only per requisition!");
    }
    else {
        tempproduct();
    }
});

function tempproduct() {
    var Id = $('#prodname :selected').val();
    var tppname = $('#prodname :selected').text();
    var tpunit = $('#unit').val();
    var tpqty = $('#qty').val();
    if (tppname.trim() !="--Select--") {
        if (tpqty != "") {
            $.ajax({
                url: "/PurchaseOrderMessage/GetReOrderQtyFromInBranch",
                type: "Post",
                data: { id: Id },
                success: function (data) {
                    if (tpqty > data) {
                        alert("Requisition not acceptable because Branch stock found high quantity for this item. Please reduce used stock before placing requisition!!")
                        $('#qty').focus();
                    }
                    else {
                        $.ajax({
                            url: "/PurchaseOrderMessage/GetMaxHoldingQtyFromInBranch",
                            type: "Post",
                            data: { id: Id },
                            success: function (data) {
                                if (tpqty > data) {
                                    alert("Maximum Holding Qty For This Product Is Greater Than Purchase Qty!!")
                                    $('#qty').focus();
                                }
                                else {
                                    $('#tadd').show();
                                    $('#datalist').append('<tr>' +
                                        '<td id="pid">' + Id + '</td >' +
                                        '<td id="pname">' + tppname + '</td >' +
                                        '<td id="pqty">' + tpqty + '</td >' +
                                        '<td>' + tpunit + '</td >' +
                                        '<td>' + '<a class="btn btn-xs btnDelete" id=' + Id + '>Delete</a>' + '</td>' +
                                        '</tr >');
                                    temppur.push({ Id });
                                    $("#prodname").val("--Select--").change();
                                    $("#unit").val('');
                                   $("#prodname").focus();
                                }
                            },
                            error: function (xhr) {
                                swal("", "error occured", "warning")
                            }
                        });
                    }
                },
                error: function (xhr) {
                    swal("", "error occured", "warning")
                }
            });
        }
        else {
            alert("Qty  Field is required!!")
            $('#qty').focus();
        }
    }
    else {
        alert("Please Select Product Name.")
        $("#prodname").focus();
    }
}

$("#datalist").on('click', '.btnDelete', function (event) {
    $(this).closest('tr').remove();
    temppur.splice(temppur.findIndex(x => x.Id == event.target.id), 1);
});

$("#saveorder").click(function () {
    var $btn = $(this);
    $btn.button('loading');
    var priority = $('#priority :selected').text();
    var forwardedto = $('#forwardedto :selected').val(); 
    var recommendedby = $('#recommendby :selected').val(); 
    var reqmessage = $('#reqmessage').val();
    var prodGroupId = $('#prodgroup').val();
    var inreqmes = {};
    var inreqList = [];
    $("table tr:not(:first)").each(function () {
        var tdlist = $(this).find("td");
        var pItem = {
            item: $(tdlist[0]).text(),
            quantity: $(tdlist[2]).html(),
            unit: $(tdlist[3]).html(),
        };
        inreqList.push(pItem);
    })
    inreqmes.inreqList = inreqList;
    inreqmes.Requ_Message = reqmessage; 
    inreqmes.priority = priority;
    inreqmes.Forwarded_To = forwardedto;
    inreqmes.prodGroupId = prodGroupId;
    inreqmes.recommed_by = recommendedby
    $.ajax({
        url: "/api/InvMovementApi/Create",
        type: "Post",
        contentType: "application/json",
        data: JSON.stringify(inreqmes),
        success: function (data) {
            $btn.button('reset');
            window.location.replace(data.RedirectUrl, true)
            alert("successfully requested purchase order");
        },
        error: function (err) {
            $btn.button('reset');
            alert(err.responseJSON.Message);
        }
    });
});

