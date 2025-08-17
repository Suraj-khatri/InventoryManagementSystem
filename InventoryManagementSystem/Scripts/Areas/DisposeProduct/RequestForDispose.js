var tsum = 0.00;
var tqty = 0;
var tpname = "";
var vatamt = 0.00;
var todisposeid;
var tempdispose = [];
var serialnototalqty = 0;
var sntq = 0;

$(document).ready(function () {
    $('#prodname').change();
    $('#tadd').hide();
    $('#datalist').empty();
    $.ajax({
        url: '/DisposeProduct/GetTempDisposeData',
        type: 'POST',
        success: function (data) {
            if (data.length > 0) {
                $('#tadd').show();
                $.each(data, function (i, item) {
                    var Id = item.ProductId;
                    if (item.SerialStatus == true) {
                        $('#datalist').append('<tr>' +
                            '<td id="pId" class="hidden">' + Id + '</td >' +
                            '<td id="pname">' + item.productname + '</td >' +
                            '<td>' + item.Unit + '</td >' +
                            '<td id="pqty">' + item.Qty + '</td >' +
                            '<td>' + item.Rate + '</td>' +
                            '<td class="TotalAmount">' + item.Amount + '</td >' +
                            '<td id="tpurid" class="hidden" >' + item.Id + '</td >' +
                            '<td>' + '<a class="btn btn-xs btnDelete" id=' + item.Id + '>Delete</a>' + '&nbsp' + '&nbsp' + '&nbsp' + '<a class="btn btn-xs btnAdd" data-toggle="modal" data-target="#myModal">Add</a>' + '</td>' +
                            '</tr >');
                    }
                    else {
                        $('#datalist').append('<tr>' +
                            '<td id="pId" class="hidden">' + Id + '</td >' +
                            '<td id="pname">' + item.productname + '</td >' +
                            '<td>' + item.Unit + '</td >' +
                            '<td id="pqty">' + item.Qty + '</td >' +
                            '<td>' + item.Rate + '</td>' +
                            '<td class="TotalAmount">' + item.Amount + '</td >' +
                            '<td id="tpurid" class="hidden" >' + item.Id + '</td >' +
                            '<td>' + '<a class="btn btn-xs btnDelete" id=' + item.Id + '>Delete</a>' + '</td>' +
                            '</tr >');
                    }
                    tempdispose.push({ Id });
                })
                footer();
            }
        }
    });
})

$('#prodname').change(function () {
    var errpname = $('#prodname :selected').text();
    $('#qty').val("");
    $('#amt').val("");
    $('#rate').val("");
    if (errpname.trim() != "--Select--") {
        $.ajax({
            url: '/PurchaseOrderMessage/GetUnitandRateForProduct',
            type: 'POST',
            data: { prodname: $('#prodname').val(), vendorname: 0 },
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

$('#rate').on('input', function (e) {
    var input = $(this);
    var rate = input.val();
    var qty = $('#qty').val();
    $('#amt').val((qty * rate).toFixed(2));
});

$("#adddata").click(function () {
    var Id = $('#prodname :selected').val();
    if (tempdispose.find(x => x.Id == Id)) {
        swal("", "This Product has been already added!", "warning")
    }
    else {
        tempdisposeadd();
    }
});

function tempdisposeadd() {
    var Id = $('#prodname :selected').val();
    var tppname = $('#prodname :selected').text();
    var tpunit = $('#unit').val();
    var tpqty = $('#qty').val();
    var tprate = $('#rate').val();
    var tpamt = $('#amt').val();
    if (tppname.trim() != "--Select--") {
        if (tpqty !== "" && tprate !== "" && tpamt > 0) {
            $('#tadd').show();
            var tp = {};
            tp.Qty = tpqty;
            tp.ProductId = Id;
            tp.Rate = tprate;
            tp.Amount = tpamt;
            tp.productname = tppname;
            $.ajax({
                url: "/DisposeProduct/TempDisposeCreate",
                type: "Post",
                contentType: "application/json",
                data: JSON.stringify(tp),
                success: function (data) {
                    if (data.success == true) {
                        todisposeid = data.todisposeid;
                        var serialstatus = data.serialstatus;
                        if (serialstatus == true) {
                            $('#datalist').append('<tr>' +
                                '<td id="pId" class="hidden">' + Id + '</td >' +
                                '<td id="pname">' + tppname + '</td >' +
                                '<td>' + tpunit + '</td >' +
                                '<td id="pqty">' + tpqty + '</td >' +
                                '<td>' + tprate + '</td>' +
                                '<td class="TotalAmount">' + tpamt + '</td >' +
                                '<td id="tpurid" class="hidden" >' + todisposeid + '</td >' +
                                '<td>' + '<a class="btn btn-xs btnDelete" id=' + todisposeid + '>Delete</a>' + '&nbsp' + '&nbsp' + '&nbsp' + '<a class="btn btn-xs btnAdd" data-toggle="modal" data-target="#myModal">Add</a>' + '</td>' +
                                '</tr >');
                        }
                        else {
                            $('#datalist').append('<tr>' +
                                '<td id="pId" class="hidden">' + Id + '</td >' +
                                '<td id="pname">' + tppname + '</td >' +
                                '<td>' + tpunit + '</td >' +
                                '<td id="pqty">' + tpqty + '</td >' +
                                '<td>' + tprate + '</td>' +
                                '<td class="TotalAmount">' + tpamt + '</td >' +
                                '<td id="tpurid" class="hidden" >' + todisposeid + '</td >' +
                                '<td>' + '<a class="btn btn-xs btnDelete" id=' + todisposeid + '>Delete</a>' + '</td>' +
                                '</tr >');
                        }
                        tempdispose.push({ Id });
                        footer();
                        $("#prodname").val("--Select--").change();
                        $("#unit").val('');
                        $("#rate").val('');
                    }
                    else {
                        swal("", data.err, "warning")
                    }
                },
                error: function (xhr) {
                    swal("", "error occured", "warning")
                }
            });

        }
        else {
            swal("", "Qty, Rate and Amount Field are required!!", "warning")
        }
    }
    else {
        swal("", "Please Select Product Name!!", "warning")
    }
}

$("#datalist").on('click', '.btnDelete', function (event) {
    $(this).closest('tr').remove();
    footer();
    tempdispose.splice(tempdispose.findIndex(x => x.Id == event.target.id), 1);
    $.ajax({
        url: '/DisposeProduct/RemoveTempDispose',
        type: 'POST',
        data: { id: event.target.id }
    });
});

$("#datalist").on('click', '.btnAdd', function () {
    $('#prodinfo').empty();
    serialnototalqty = 0;
    tpname = $(this).closest('tr').find("#pname").text();
    tqty = $(this).closest('tr').find("#pqty").text();
    todisposeid = $(this).closest('tr').find("#tpurid").text();
    $('#myModal').show();

    $.ajax({
        url: "/DisposeProduct/GetTempDiposeIdFromTempDisposeOther",
        type: "Post",
        data: { tempid: todisposeid },
        success: function (data) {
            if (data.length > 0) {
                $.each(data, function (i, item) {
                    var rows = 'Product Name : ' + tpname + '&nbsp' + '&nbsp' + 'Total Qty : ' + item.Qty +
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
                        '<td id="sqty">' + item.Qty + '</td>' +
                        '</tr >'
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
        url: "/PurchaseVoucher/CheckIfSerailNoExistsInEachBranch",
        type: "Post",
        data: { snf: snf, snt: snt, tpname: tpname },
        success: function (data) {
            if (data != parseInt(qtysn)) {
                swal("", "This Range of Serial No. For This Product does not Exists in this Branch!!", "warning")
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
                        tpo.TempDisposedId = todisposeid;
                        $.ajax({
                            url: "/DisposeProduct/TempDiposeOtherCreate",
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


$("#savedispose").click(function (e) {
    var $btn = $(this);
    $btn.button('loading');
    var disposemes = {};
    var disposedetailsList = [];
    $("table tr:not(:first)").each(function () {
        var tdlist = $(this).find("td");
        var Item = {
            ProductId: $(tdlist[0]).text(),
            RequestedQty: $(tdlist[3]).html(),
            Rate: $(tdlist[4]).text(),
            Amount: $(tdlist[5]).html(),
        };
        disposedetailsList.push(Item);
    })
    disposemes.TotalAmount = (vatamt + tsum);
    disposemes.VatAmount = vatamt;
    disposemes.disposedetailsList = disposedetailsList;
    disposemes.RequestDate = $('#RequestDate').val();
    disposemes.ForwardedForApproval = $('#ForwardedForApproval').val();
    disposemes.DisposeReason = $('#DisposeReason').val();
    $.ajax({
        url: "/DisposeProduct/RequestForDispose",
        type: "Post",
        contentType: "application/json",
        data: JSON.stringify(disposemes),
        success: function (data) {
            if (data.success == true) {
                //swal({
                //    titel: "", text: data.mes, icon: "success",
                //    closeOnClickOutside: false,
                //    closeOnEsc: false,
                //}
                //).then(okay => {
                //    if (okay) {
                $btn.button('reset');
                window.location.replace("/DisposeProduct/Index", true)
                //    }
                //});
            }
            else {
                /* swal("", data.err, "warning")*/
                $btn.button('reset');
                alert(data.err);
            }
        },
        error: function (err) {
            $btn.button('reset');
            alert("Error Occured !!");
        }
    });
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
        '<th>' + '</th >');
}

$("#myModal").on('click', '.serialnosave', function () {
    alert("successfull saved");
    $("#myModal").modal("hide");
});

$(document).on('change', '#defaultUnchecked', function () {
    footer();
});