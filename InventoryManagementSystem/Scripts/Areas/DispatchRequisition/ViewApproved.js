var pname = "";
var tqty = 0;
var serialnototalqty = 0;
var sntq = 0;
var ReqMesId = 0;

$(document).on('click', '#addserialno', function () {
    $('#prodinfo').empty();
    serialnototalqty = 0;
    pname = $(this).closest('tr').find("#pname").text().trim();
    tqty = $(this).closest('tr').find("#dispqty :input[type='number']").val();/*$(this).closest('tr').find("#qty").text();*/
    var val1 = $('#reqmesid').val();
    var val2 = $(this).closest('tr').find("#pname").text();
    $.ajax({
        url: "/DispatchRequisition/GetDetailIdandPurIdFromInRequisitionDetailOther",
        type: "Post",
        data: { inreqmesid: val1, 'prodname': val2 },
        success: function (data) {
            if (data.length > 0) {
                $.each(data, function (i, item) {
                    var rows = 'Product Name : ' + pname + '&nbsp' + '&nbsp' + 'Total Qty : ' + item.qty +
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
                        '</tr >'
                        '</tbody>' +
                        '</table >'
                    '</fieldset >'
                    $('#prodinfo').append(rows);
                })
            }
            else {
                var rows = 'Product Name : ' + pname + '&nbsp' + '&nbsp' + 'Total Qty : ' + tqty +
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
            alert("error occured");
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
        data: { snf: snf, snt: snt, tpname: pname },
        success: function (data, status, jqXHR) {
            console.log(status, jqXHR);
            if (jqXHR.status === 302) {
                // Extract the redirect URL from the response headers
                let redirectUrl = jqXHR.getResponseHeader("Location");
                if (redirectUrl) {
                    // Redirect the browser to the new URL
                    window.location.href = redirectUrl;
                } else {
                    swal("", "Redirection occurred, but no URL was provided!", "warning");
                }
            }
            if (data != parseInt(qtysn)) {
                swal("", "This Range of Serial No. For This Product does not Exists in this branch!!", "warning")
            }
            else {
                serialnototalqty += parseFloat(qtysn);

                if (parseInt(serialnototalqty) <= parseInt(tqty)) {
                    if (snf != "" && snt > 0) {
                        $('#datalistsn').append('<tr>' +
                            '<td>' + snf + '</td >' +
                            '<td>' + snt + '</td >' +
                            '<td id="sqty">' + qtysn + '</td>' +
                            //'<td>' + '<a class="btn btn-xs btnsnDelete">Delete</a>' + '</td>' +
                            '</tr >');
                        var irdo = {};
                        irdo.qty = qtysn;
                        irdo.sn_from = snf;
                        irdo.sn_to = snt;
                        irdo.detail_id = $('#reqmesid').val();
                        irdo.productname = pname;
                        $.ajax({
                            url: "/api/InvMovementApi/InRequisitionDetailOtherCreate",
                            type: "Post",
                            contentType: "application/json",
                            data: JSON.stringify(irdo),
                            success: function (data) {
                            },
                            error: function (xhr) {
                                alert("error occured");
                            }
                        });
                        sntq = parseFloat(serialnototalqty);

                        $('#qtysn').val('');
                        $('#snf').val('');
                        $('#snt').val('');
                    }
                    else {
                        alert("Please enter serial start from and serial number end!")
                    }
                }
                else {
                    alert("Qty out of range!")
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
        alert("Qty out of range!")
        $('#qtysn').val('');
        $('#snf').val('');
        $('#snt').val('');
    }
});

$("#savedispatch").click(function () {
    var $btn = $(this);
    $btn.button('loading');
    var disptmessage = $('#dispatchmessage').val();
    var dispatchdate = $('#Delivered_Date').val();
    ReqMesId = $('#reqmesid').val();
    var inreqmes = {};
    var indispList = [];
    $("table tr:not(:first)").each(function () {
        var tdlist = $(this).find("td");
        var pItem = {
            product_id: $(tdlist[0]).text(),
            productname: $(tdlist[1]).text(),
            IsRowCheck: $(tdlist[6]).find("input:checkbox").is(':checked'),
            dispatched_qty: $(this).closest('tr').find("#dispqty :input[type='number']").val(),// $(tdlist[5]).val(),
            remain: $(this).closest('tr').find("#remaintodisp :input[type='number']").val(),// $(tdlist[6]).text(),
            p_rate: $(tdlist[10]).text(),
        };
        indispList.push(pItem);
    })
    inreqmes.id = ReqMesId;
    inreqmes.indispList = indispList;
    inreqmes.Delivery_Message = disptmessage;
    inreqmes.Delivered_Date = dispatchdate;
    $.ajax({
        url: "/api/InvMovementApi/DispatchRequisition",
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

$("#myModal").on('click', '.serialnosave', function () {
    alert("successfull saved");
    $("#myModal").modal("hide");
});

$("#tbl_create").on("input", '#dispqty', function (e) {
    var approveqty = $(this).closest('tr').find('#qty').text();
    var dispatchqty = $(this).closest('tr').find("#dispqty :input[type='number']").val();
    if (dispatchqty == "") {
        $(this).closest('tr').find("#remaintodisp :input[type=number]").val(parseInt(approveqty));
        return 0;
    }
    if (parseInt(dispatchqty) <= parseInt(approveqty)) {
        var remaindispatchqty = parseInt(approveqty) - parseInt(dispatchqty);
        $(this).closest('tr').find("#remaintodisp :input[type=number]").val(parseInt(remaindispatchqty));
    }
    else {
        alert("Dispatched Qty must be smaller or equal to Approved Qty.");
        $(this).closest('tr').find("#dispqty :input[type=number]").val(parseInt(approveqty));
        $(this).closest('tr').find("#remaintodisp :input[type=number]").val('0');
    }
});
