var tsum = 0.00;
var tqty = 0;
var tpname = "";
var vatamt = 0.00;
var topurid;
var temppur = [];
var serialnototalqty = 0;
var sntq = 0;

$(document).ready(function () {
    $('#vendorname').change();
    $('#tadd').hide();
    $('#datalist').empty();
    $.ajax({
        url: '/PurchaseOrderMessage/GetTempPurchaseData',
        type: 'POST',
        success: function (data) {
            if (data.length > 0) {
                $('#tadd').show();
                $.each(data, function (i, item) {
                    var Id = item.product_code;
                    if (item.SerialStatus == true) {
                        $('#datalist').append('<tr>' +
                            '<td id="pname">' + item.productname + '</td >' +
                            '<td>' + item.Unit + '</td >' +
                            '<td id="pqty">' + item.qty + '</td >' +
                            '<td>' + item.rate + '</td>' +
                            '<td class="TotalAmount">' + item.amount + '</td >' +
                            '<td id="tpurid" class="hidden" >' + item.id + '</td >' +
                            '<td>' + '<a class="btn btn-xs btnDelete" id=' + item.id + '>Delete</a>' + '&nbsp' + '&nbsp' + '&nbsp' + '<a class="btn btn-xs btnAdd" data-toggle="modal" data-target="#myModal">Add</a>' + '</td>' +
                            '</tr >');
                    }
                    else {
                        $('#datalist').append('<tr>' +
                            '<td id="pname">' + item.productname + '</td >' +
                            '<td>' + item.Unit + '</td >' +
                            '<td id="pqty">' + item.qty + '</td >' +
                            '<td>' + item.rate + '</td>' +
                            '<td class="TotalAmount">' + item.amount + '</td >' +
                            '<td id="tpurid" class="hidden" >' + item.id + '</td >' +
                            '<td>' + '<a class="btn btn-xs btnDelete" id=' + item.id + '>Delete</a>' + '</td>' +
                            '</tr >');
                    }
                    temppur.push({ Id });
                })
                footer();
            }
        }
    });
})

$('#vendorname').change(function () {
    $('#prodname').empty();
    $.ajax({
        url: '/PurchaseOrderMessage/GetProductNameForVendor',
        type: 'POST',
        data: { vendorname: $('#vendorname').val() },
        success: function (data) {
            $.each(data, function (i, item) {
                $('#prodname').append(new Option(item.Text, item.Value));
                $('#prodname').change();
            })
        }
    });
});

$('#prodname').change(function () {
    var errpname = $('#prodname :selected').text();
    $('#qty').val("");
    $('#amt').val("");
    if (errpname.trim() != "--Select--") {
        $.ajax({
            url: '/PurchaseOrderMessage/GetUnitandRateForProduct',
            type: 'POST',
            data: { prodname: $('#prodname').val(), vendorname: $('#vendorname').val() },
            success: function (data) {
                $('#unit').val(data.Item1)
                $('#unit').text(data.Item1)
                $('#rate').val(data.Item2)
                $('#rate').text(data.Item2)
            }
        });
    }
});

$('#qty').on('input', function (e) {
    var input = $(this);
    var qty = input.val();
    var rate = $('#rate').val();
    $('#amt').val((qty * rate).toFixed(2));
});

$("#adddata").click(function () {
    var Id = $('#prodname :selected').val();
    if (temppur.find(x => x.Id == Id)) {
        swal("", "This Product has been already added!", "warning")
    }
    else {
        temppurchase();
    }
});

function temppurchase() {
    var Id = $('#prodname :selected').val();
    var tppname = $('#prodname :selected').text();
    var tpunit = $('#unit').val();
    var tpqty = $('#qty').val();
    var tprate = $('#rate').val();
    var tpamt = $('#amt').val();
    if (tppname.trim() != "--Select--") {
        if (tpqty !== "" && tpamt > 0) {
            $.ajax({
                url: "/PurchaseOrderMessage/GetReOrderQtyFromInBranch",
                type: "Post",
                data: { id: Id },
                success: function (data) {
                    if (tpqty > data) {
                        swal("", "Re-Order Qty For This Product Is Greater Than Purchase Qty!!", "warning")
                    }
                    else {
                        $.ajax({
                            url: "/PurchaseOrderMessage/GetMaxHoldingQtyFromInBranch",
                            type: "Post",
                            data: { id: Id },
                            success: function (data) {
                                if (tpqty > data) {
                                    swal("", "Maximum Holding Qty For This Product Is Greater Than Purchase Qty!!", "warning")
                                }
                                else {
                                    $('#tadd').show();
                                    var tp = {};
                                    tp.qty = tpqty;
                                    tp.product_code = Id;
                                    tp.rate = tprate;
                                    tp.amount = tpamt;
                                    tp.productname = tppname;
                                    $.ajax({
                                        url: "/api/PurchaseApi/TempPurchaseCreate",
                                        type: "Post",
                                        contentType: "application/json",
                                        data: JSON.stringify(tp),
                                        success: function (data) {
                                            topurid = data.Item1;
                                            var serialstatus = data.Item2;
                                            if (serialstatus == true) {
                                                $('#datalist').append('<tr>' +
                                                    '<td id="pname">' + tppname + '</td >' +
                                                    '<td>' + tpunit + '</td >' +
                                                    '<td id="pqty">' + tpqty + '</td >' +
                                                    '<td>' + tprate + '</td>' +
                                                    '<td class="TotalAmount">' + tpamt + '</td >' +
                                                    '<td id="tpurid" class="hidden" >' + topurid + '</td >' +
                                                    '<td>' + '<a class="btn btn-xs btnDelete" id=' + topurid + '>Delete</a>' + '&nbsp' + '&nbsp' + '&nbsp' + '<a class="btn btn-xs btnAdd" data-toggle="modal" data-target="#myModal">Add</a>' + '</td>' +
                                                    '</tr >');
                                            }
                                            else {
                                                $('#datalist').append('<tr>' +
                                                    '<td id="pname">' + tppname + '</td >' +
                                                    '<td>' + tpunit + '</td >' +
                                                    '<td id="pqty">' + tpqty + '</td >' +
                                                    '<td>' + tprate + '</td>' +
                                                    '<td class="TotalAmount">' + tpamt + '</td >' +
                                                    '<td id="tpurid" class="hidden" >' + topurid + '</td >' +
                                                    '<td>' + '<a class="btn btn-xs btnDelete" id=' + topurid + '>Delete</a>' + '</td>' +
                                                    '</tr >');
                                            }
                                            temppur.push({ Id });
                                            footer();
                                            $("#prodname").val("--Select--").change();
                                            $("#unit").val('');
                                            $("#rate").val('');
                                            $("#prodname").focus();
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
                },
                error: function (xhr) {
                    swal("", "error occured", "warning")
                }
            });

        }
        else {
            alert("Qty and Amount Field is required!!")
            $('#qty').focus();
        }
    }
    else {
        alert("Please Select Product Name!!")
        $("#prodname").focus();
    }
}

$("#datalist").on('click', '.btnDelete', function (event) {
    $(this).closest('tr').remove();
    footer();
    temppur.splice(temppur.findIndex(x => x.Id == event.target.id), 1);
    $.ajax({
        url: '/PurchaseOrderMessage/RemoveTempPurchase',
        type: 'POST',
        data: { id: event.target.id }
    });
});

$("#datalist").on('click', '.btnAdd', function () {
    $('#prodinfo').empty();
    serialnototalqty = 0;
    tpname = $(this).closest('tr').find("#pname").text();
    tqty = $(this).closest('tr').find("#pqty").text();
    topurid = $(this).closest('tr').find("#tpurid").text();
    $('#myModal').show();

    $.ajax({
        url: "/PurchaseOrderMessage/GetTempPurIdFromTempPurchaseOther",
        type: "Post",
        data: { tempid: topurid },
        success: function (data) {
            if (data.length > 0) {
                $.each(data, function (i, item) {
                    var rows = 'Product Name : ' + tpname + '&nbsp' + '&nbsp' + 'Total Qty : ' + item.qty +
                        '<table class="table table-bordered noDataTable">' +
                        '<thead class="thead-dark" style="height:20px">' +
                        '<tr>' +
                        '<th>Serial From</th>' +
                        '<th>Serial To</th>' +
                        '<th id="tpoqty">Qty</th>' +
                        '</tr>' +
                        '</thead>' +
                        '<tbody id ="datalistsn">' +
                        '<tr>' +
                        '<td>' + item.sn_from + '</td >' +
                        '<td>' + item.sn_to + '</td >' +
                        '<td id="sqty">' + item.qty + '</td>' +
                        '</tr >'+
                    '</tbody>' +
                        '</table >'
                    '</fieldset >'
                    $('#prodinfo').append(rows);
                })
            }
            else {
                var rows = 'Product Name : ' + tpname + '&nbsp' + '&nbsp' + 'Total Qty : ' + tqty +
                    '<fieldset id="addtbllist" style="list-style: circle; list-style-type: circle; width: 100%;">' +
                    '<legend style="color:forestgreen;font-size:small;font:bold">Other Information:</legend>' +
                    '<div class="col-md-3 col-xs-12">' +
                    '<div class="form-group">' +
                    '<label class="control-label">Qty: </label>' +
                    '<input type="number" id="qtysn" name="qty">' +
                    '</div>' +
                    '</div>' +
                    '<div class="col-md-3 col-xs-12">' +
                    '<div class="form-group">' +
                    '<label class="control-label">S.N From:  </label>' +
                    '<input type="number" id="snf" name="snf">' +
                    '</div>' +
                    '</div>' +
                    '<div class="col-md-3 col-xs-12">' +
                    '<div class="form-group">' +
                    '<label class="control-label">S.N. To: </label>' +
                    '<input type="number" readonly id="snt" name="snt">' +
                    '</div>' +
                    '</div>' +
                    '<div class="col-md-3 col-xs-12">' +
                    '<a class="btn btn-xs btn-success pull-right addsnlist" title="Add">Add</a>' +
                    '</div>' +
                    '<table class="table table-bordered noDataTable">' +
                    '<thead class="thead-dark" style="height:20px">' +
                    '<tr>' +
                    '<th>Serial From</th>' +
                    '<th>Serial To</th>' +
                    '<th id="tpoqty">Qty</th>' +
                    //'<th>Action</th>' +
                    '</tr>' +
                    '</thead>' +
                    '<tbody id ="datalistsn">' +
                    '</tbody>' +
                    '</table >'
                '</fieldset >'
                $('#prodinfo').append(rows);
            }
        },
        error: function (xhr) {
            swal("", "error occured", "warning")
        }
    });
});

$("#prodinfo").on('click', '.addsnlist', function () {
    var snf = $('#snf').val();
    var snt = $('#snt').val();
    var qtysn = $('#qtysn').val();
    $.ajax({
        url: "/PurchaseVoucher/CheckIfSerailNoExists",
        type: "Post",
        data: { snf: snf, snt: snt, tpname: tpname },
        success: function (data) {
            if (data > 0) {
                swal("", "This Range of Serial No. For This Product Already Exists!!", "warning")
            }
            else {
                serialnototalqty += parseFloat(qtysn);

                if (parseInt(serialnototalqty) <= parseInt(tqty)) {
                    if (snf !== "" && snt > 0) {
                        $('#datalistsn').append('<tr>' +
                            '<td>' + snf + '</td >' +
                            '<td>' + snt + '</td >' +
                            '<td id="sqty">' + qtysn + '</td>' +
                            //'<td>' + '<a class="btn btn-xs btnsnDelete">Delete</a>' + '</td>' +
                            '</tr >');
                        var tpo = {};
                        tpo.qty = qtysn;
                        tpo.sn_from = snf;
                        tpo.sn_to = snt;
                        tpo.temp_purchase_id = topurid;
                        $.ajax({
                            url: "/api/PurchaseApi/TempPurchaseOtherCreate",
                            type: "Post",
                            contentType: "application/json",
                            data: JSON.stringify(tpo),
                            success: function (data) {
                                //alert("successfully added");
                            },
                            error: function (xhr) {
                                swal("", "error occured", "warning")
                            }
                        });
                        sntq = parseFloat(serialnototalqty);

                        $('#qtysn').val('');
                        $('#snf').val('');
                        $('#snt').val('');
                    }
                    else {
                        swal("", "Please enter serial start from and serial number end!", "warning")
                    }
                }
                else {
                    swal("", "Qty out of range!", "warning")
                    serialnototalqty = sntq;
                }
            }
        },
        error: function (err) {
            swal("", "error occured", "warning")
        }
    });
});

$("#prodinfo").on('input', '#snf', function (e) {
    var snqty = $('#qtysn').val();
    if (parseInt(snqty) <= parseInt(tqty)) {
        var input = $(this);
        var snf = input.val();
        var sntfinal = parseInt(snqty) + parseInt(snf) - 1;
        $('#snt').val(sntfinal);
    }
    else {
        swal("", "Qty out of range!", "warning")
        $('#qtysn').val('');
        $('#snf').val('');
        $('#snt').val('');
    }
});

//$("#prodinfo").on('click', '.btnsnDelete', function () {
//    $(this).closest('tr').remove();
//});

$("#saveorder").click(function (e) {
    var r = confirm("Are you sure?")
    if (r) {
        var pom = {};
        var polist = [];
        $("table tr:not(:first)").each(function () {
            var tdlist = $(this).find("td");
            var Item = {
                productname: $(tdlist[0]).text(),
                qty: $(tdlist[2]).html(),
                rate: $(tdlist[3]).html(),
                amount: $(tdlist[4]).html(),
            };
            polist.push(Item);
        })

        pom.temppurchaseid = topurid;
        pom.vat_amt = vatamt;
        pom.polist = polist;
        pom.order_date = $('#order_date').val();
        pom.forwarded_to = $('#forwarded_to').val();
        pom.delivery_date = $('#delivery_date').val();
        pom.remarks = $('#remarks').val();
        pom.prod_specfic = $('#prod_specfic').val();
        pom.vendor_code = $('#vendorname :selected').val();
        $.ajax({
            url: "/api/PurchaseApi/Create",
            type: "Post",
            contentType: "application/json",
            data: JSON.stringify(pom),
            success: function (data) {
                swal({
                    titel: "", text: "successfully requested purchase order!", icon: "success",
                    closeOnClickOutside: false,
                    closeOnEsc: false,
                }
                ).then(okay => {
                    if (okay) {
                        window.location.replace(data.RedirectUrl, true)
                    }
                });
            },
            error: function (err) {
                alert(err.responseJSON.Message);
            }
        });
    }
    else {
        return false
    }
    
});

function footer() {
    var total = 0
    $('#tfooter').empty();
    $('.TotalAmount').each(function () {
        total += parseFloat($(this).text());
    });
    tsum = total;
    if (!$('#defaultUnchecked').prop("checked")) {
        vatamt = parseFloat(13 / 100 * tsum);
    } else {
        vatamt = 0;
    }

    $('#tfooter').append('<th>' + '</th>' +
        '<th>' + '</th>' +
        '<th>' + '</th>' +
        '<th>' + 'Sub Total :' + '</br>' + '13% VAT :' + '</br>' + 'Total Amount: ' + '</th > ' +
        '<th id="sumtot">' + (tsum).toFixed(2) + '</br>' + vatamt.toFixed(2) + '</br>' + (parseFloat(tsum) + parseFloat(vatamt)).toFixed(2) + '</th' +
        '<th>' + '</th >' +
        '<th>' + '</th >' +
        '<th>' + '</th >'
    );
}

$("#myModal").on('click', '.serialnosave', function () {
    alert("successfull saved");
    $("#myModal").modal("hide");
});

$(document).on('change', '#defaultUnchecked', function () {
    footer();
});