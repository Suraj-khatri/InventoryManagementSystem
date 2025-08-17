var temppur = [];
var j = 0;

$(document).ready(function () {
    $('#branchname').change();
    $('#prodname').change();
    $('#forwardedto').change();
    $('#nonprintingItem').click();
})

$('#branchname').change(function () {
    $('#reqby').empty();
    $.ajax({
        url: '/ReturnDispatchForBranch/GetEmployeeForBranchList',
        type: 'POST',
        data: { branchid: $('#branchname').val() },
        success: function (data) {
            $.each(data, function (i, item) {
                $('#reqby').append(new Option(item.Text, item.Value));
            })
        }
    });
});

$('#forwardedto').change(function () {
    $('#dispatchby').empty();
    $.ajax({
        url: '/ReturnDispatchForBranch/GetEmployeeForBranchList',
        type: 'POST',
        data: { branchid: $('#forwardedto').val() },
        success: function (data) {
            $.each(data, function (i, item) {
                $('#dispatchby').append(new Option(item.Text, item.Value));
            })
        }
    });
});

$('#prodname').change(function () {
    var errpname = $('#prodname :selected').text();
    $('#qty').val("");
    if (errpname.trim() != "--Select--") {
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
    var StockInHand = 0;
    if (tppname.trim() != "--Select--") {
        if (tpqty != "") {

            $.ajax({
                url: "/DirectDispatchForNewBranch/GetStockInHandForProductId",
                type: "Post",
                data: { pid: Id },
                success: function (data) {
                    StockInHand = data;
                },
                error: function (xhr) {
                    alert("error occured")
                }
            });

            var std = {};
            std.Qty = tpqty;
            std.ProductId = Id;
            $.ajax({
                url: "/api/InvMovementApi/TempStaticTempDispatchCreate",
                type: "Post",
                contentType: "application/json",
                data: JSON.stringify(std),
                success: function (data) {
                    $('#datalist').append('<tr>' +
                        '<td>' + j + '</td>' +
                        '<td id="tpurid">' + Id + '</td >' +
                        '<td id="pname">' + tppname + '</td >' +
                        '<td>' + tpunit + '</td >' +
                        '<td id="dispqty">' + '<input type="number" value="' + tpqty + '" size="2" />' + '</td >' +
                        '<td>' + '<input type="checkbox" name="row-check"/>' + '</td>' +
                        '<td>' + StockInHand + '</td >' +
                        '<td>' + '<a class="btn btn-xs btnDelete" id=' + Id + '>Remove</a>' + '</td>' +
                        '</tr >');
                    j++;
                    temppur.push({ Id });
                    $("#prodname").val("--Select--").change();
                    $("#unit").val('');
                    $("#prodname").focus();
                },
                error: function (xhr) {
                    alert("error occured");
                }
            });
        }
        else {
            alert("Qty  Field is required!!")
            $('#qty').focus();
        }
    }
    else {
        alert("Please Select Product Name!!")
        $("#prodname").focus();
    }
}



$("#saveorder").click(function () {
    var $btn = $(this);
    $btn.button('loading');
    var dispatchmessage = $('#dispatchmessage').val();
    var reqby = $('#reqby :selected').val();
    var dispatchby = $('#dispatchby :selected').val();
    var branchid = $('#branchname :selected').val();
    var forwardedto = $('#forwardedto :selected').val();

    var inreqmes = {};
    var indispList = [];
    $("table tr:not(:first)").each(function () {
        var tdlist = $(this).find("td");
        var pItem = {
            product_id: $(tdlist[1]).text(),
            productname: $(tdlist[2]).html(),
            unit: $(tdlist[3]).html(),
            dispatched_qty: $(this).closest('tr').find("#dispqty :input[type='number']").val(),// $(tdlist[5]).val(),
            IsRowCheck: $(tdlist[5]).find("input:checkbox").is(':checked'),
        };
        indispList.push(pItem);
    })
    inreqmes.indispList = indispList;
    inreqmes.Delivery_Message = dispatchmessage;
    inreqmes.Delivered_By = dispatchby;
    inreqmes.Requ_by = reqby;
    inreqmes.branch_id = branchid;
    inreqmes.Forwarded_To = forwardedto;

    $.ajax({
        url: "/api/InvMovementApi/DispatchRequisitionForNewBranch",
        type: "Post",
        contentType: "application/json",
        data: JSON.stringify(inreqmes),
        success: function (data) {
            $btn.button('reset');
            window.location.replace(data.RedirectUrl, true)
            alert("Successfully Dispatched");
        },
        error: function (err) {
            $btn.button('reset');
            alert(err.responseJSON.Message);
        }
    });
});

$('#nonprintingItem').click(function () {
    if ($('#nonprintingItem').prop('checked', true)) {
        $('#printingItem').prop('checked', false);
        $('#btnPurchase').show();

        $('#prodname').empty();
        $.ajax({
            url: '/DirectDispatchForNewBranch/GetNonPrintingProductNameForBranchList',
            type: 'POST',
            data: { branchid: $('#forwardedto').val() },
            success: function (data) {
                $.each(data, function (i, item) {
                    $('#prodname').append(new Option(item.Text, item.Value));
                })
                $('#prodname').change();
            }
        });

        $('#datalist').empty();
        j = 1;
        $.ajax({
            url: '/DirectDispatchForNewBranch/GetNonPrintingInStaticTempDispatchData',
            type: 'POST',
            success: function (data) {
                if (data.length > 0) {
                    $('#tadd').show();
                    $.each(data, function (i, item) {
                        var Id = item.ProductId;
                        $('#datalist').append('<tr>' +
                            '<td>' + j + '</td>' +
                            '<td id="tpurid">' + Id + '</td >' +
                            '<td id="pname">' + item.ProductName + '|' + Id + '</td >' +
                            '<td>' + item.Unit + '</td >' +
                            '<td id="dispqty">' + '<input type="number" value="' + item.Qty + '" size="2" />' + '</td >' +
                            '<td>' + '<input type="checkbox" name="row-check"/>' + '</td>' +
                            '<td>' + item.StockInHand + '</td >' +
                            '<td>' + '<a class="btn btn-xs btnDelete" id=' + Id + '>Remove</a>' + '</td>' +
                            '</tr >');
                        j++;
                        temppur.push({ Id });
                    })
                }
            }
        });
    }
});

$('#printingItem').click(function () {
    if ($('#printingItem').prop('checked', true)) {
        $('#nonprintingItem').prop('checked', false);
        $('#btnPurchase').hide();
        $('#prodname').empty();
        $.ajax({
            url: '/DirectDispatchForNewBranch/GetPrintingProductNameForBranchList',
            type: 'POST',
            data: { branchid: $('#forwardedto').val() },
            success: function (data) {
                $.each(data, function (i, item) {
                    $('#prodname').append(new Option(item.Text, item.Value));
                })
                $('#prodname').change();
            }
        });

        $('#datalist').empty();
        j = 1;
        $.ajax({
            url: '/DirectDispatchForNewBranch/GetPrintingInStaticTempDispatchData',
            type: 'POST',
            success: function (data) {
                if (data.length > 0) {
                    $('#tadd').show();

                    $.each(data, function (i, item) {
                        var Id = item.ProductId;
                        $('#datalist').append('<tr>' +
                            '<td>' + j + '</td>' +
                            '<td id="tpurid">' + Id + '</td >' +
                            '<td id="pname">' + item.ProductName + '|' + Id + '</td >' +
                            '<td>' + item.Unit + '</td >' +
                            '<td id="dispqty">' + '<input type="number" value="' + item.Qty + '" size="2" />' + '</td >' +
                            '<td>' + '<input type="checkbox" name="row-check"/>' + '</td>' +
                            '<td>' + item.StockInHand + '</td >' +
                            '<td>' + '<a class="btn btn-xs btnDelete" id=' + Id + '>Remove</a>' + '</td>' +
                            '</tr >');
                        j++;
                        temppur.push({ Id });
                    })
                }
            }
        });
    }
});

$("#datalist").on('click', '.btnDelete', function (event) {
    $(this).closest('tr').remove();
    temppur.splice(temppur.findIndex(x => x.Id == event.target.id), 1);
    $.ajax({
        url: '/DirectDispatchForNewBranch/RemoveProduct',
        type: 'POST',
        contentType: "application/json",
        data: JSON.stringify({ pId: event.target.id }),
        success: function (data) {
            console.log("Product removed successfully");
        },
        error: function (xhr, status, error) {
            console.error("Error removing product:", error);
        }
    });
});
$(function () {
    $("#masterCheck").on("click", function () {
        if ($("input:checkbox[name='master']").prop("checked")) {
            $("input:checkbox[name='row-check']").prop("checked", true);
        } else {
            $("input:checkbox[name='row-check']").prop("checked", false);
        }
    });
    $("#datalist").on("input:checkbox[name='row-check']").on("change", function () {
        var total_check_boxes = $("input:checkbox[name='row-check']").length;
        var total_checked_boxes = $("input:checkbox[name='row-check']:checked").length;

        if (total_check_boxes === total_checked_boxes) {
            $("#masterCheck").prop("checked", true);
        }
        else {
            $("#masterCheck").prop("checked", false);
        }
    });
});
