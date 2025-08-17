$(document).ready(function () {
    $('#branchname').change();
    $('#prodname').change();
    $('#forwardedto').change();
})

$('#branchname').change(function () {
    $('#recommendby').empty();
    $.ajax({
        url: '/ReturnDispatchForBranch/GetEmployeeForBranchList',
        type: 'POST',
        data: { branchid: $('#branchname').val() },
        success: function (data) {
            $.each(data, function (i, item) {
                $('#recommendby').append(new Option(item.Text, item.Value));
            })
        }
    });
    $('#prodname').empty();
    getProductNameForBranch();
});

function getProductNameForBranch() {
    $.ajax({
        url: '/ReturnDispatchForBranch/GetProductNameForBranchList',
        type: 'POST',
        data: { branchid: $('#branchname').val() },
        success: function (data) {
            $.each(data, function (i, item) {
                $('#prodname').append(new Option(item.Text, item.Value));
            })
            $('#prodname').change();
        }
    });
}

$('#forwardedto').change(function () {
    $('#reqby').empty();
    $.ajax({
        url: '/ReturnDispatchForBranch/GetEmployeeForBranchList',
        type: 'POST',
        data: { branchid: $('#forwardedto').val() },
        success: function (data) {
            $.each(data, function (i, item) {
                $('#reqby').append(new Option(item.Text, item.Value));
            })
        }
    });
});


var temppur = [];
var tpname = "";
var tpId = 0;
var tqty = 0;
var sntq = 0;
var serialnototalqty = 0;
var TemReqId;

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
    if (tppname.trim() != "--Select--") {
        if (tpqty != "") {
            $.ajax({
                url: '/ReturnDispatchForBranch/GetSerialStatusForProduct',
                type: 'POST',
                data: { pid: Id },
                success: function (data) {
                    var serialstatus = data;
                    if (serialstatus == true) {
                        $('#tadd').show();
                        $('#datalist').append('<tr>' +
                            '<td id="pid">' + Id + '</td >' +
                            '<td id="pname">' + tppname + '</td >' +
                            '<td id="pqty">' + tpqty + '</td >' +
                            '<td>' + tpunit + '</td >' +
                            '<td>' + '<a class="btn btn-xs btnDelete" id=' + Id + '>Delete</a>' + '&nbsp' + '&nbsp' + '&nbsp' + '<a class="btn btn-xs btnAdd" data-toggle="modal" data-target="#myModal">Add</a>' + '</td>' +
                            '</tr >');
                    }
                    else {
                        $('#datalist').append('<tr>' +
                            '<td id="pid">' + Id + '</td >' +
                            '<td id="pname">' + tppname + '</td >' +
                            '<td id="pqty">' + tpqty + '</td >' +
                            '<td>' + tpunit + '</td >' +
                            '<td>' + '<a class="btn btn-xs btnDelete" id=' + Id + '>Delete</a>' + '</td>' +
                            '</tr >');
                    }
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

$("#datalist").on('click', '.btnDelete', function (event) {
    $(this).closest('tr').remove();
    temppur.splice(temppur.findIndex(x => x.Id == event.target.id), 1);
    $.ajax({
        url: '/ReturnDispatchForBranch/RemoveTempReturn',
        type: 'POST',
        data: { id: event.target.id }
    });
});

$("#datalist").on('click', '.btnAdd', function () {
    $('#prodinfo').empty();
    serialnototalqty = 0;
    tpname = $(this).closest('tr').find("#pname").text();
    tqty = $(this).closest('tr').find("#pqty").text();
    tpId = $(this).closest('tr').find("#pid").text();

    $('#myModal').show();

    $.ajax({
        url: "/ReturnDispatchForBranch/GetTempReturnProduct",
        type: "Post",
        data: { pid: tpId },
        success: function (data) {
            console.log(data);
            if (data.productid > 0) {
                    var rows = 'Product Name : ' + tpname + '&nbsp' + '&nbsp' + 'Total Qty : ' + data.qty +
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
                        '<td>' + data.sn_from + '</td >' +
                        '<td>' + data.sn_to + '</td >' +
                        '<td id="sqty">' + data.qty + '</td>' +
                        '</tr >'
                    '</tbody>' +
                        '</table >'
                    '</fieldset >'
                    $('#prodinfo').append(rows);
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
            alert("error occured");
        }
    });
});

$("#prodinfo").on('click', '.addsnlist', function () {
    var snf = $('#snf').val();
    var snt = $('#snt').val();
    var qtysn = $('#qtysn').val();
    var branchid = $('#branchname').val();
    $.ajax({
        url: "/PurchaseVoucher/CheckIfSerailNoExistsInReturnedBranch",
        type: "Post",
        data: { snf: snf, snt: snt, branchid: branchid, tpname: tpname },
        success: function (data) {
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
                            '</tr >');
                        var tro = {};
                        tro.qty = qtysn;
                        tro.sn_from = snf;
                        tro.sn_to = snt;
                        tro.productid = tpId;
                        $.ajax({
                            url: "/api/InvMovementApi/TempReturnProductCreate",
                            type: "Post",
                            contentType: "application/json",
                            data: JSON.stringify(tro),
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
            alert("Error Occured. Please Try Again Later!!");
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

$("#saveorder").click(function () {
    swal({
        titel: "", text: "Are You Sure ?", icon: "info",
        closeOnClickOutside: false,
        closeOnEsc: false,
    }
    ).then(okay => {
        if (okay) {
            returndispatchdata();
        }
    });
});

function returndispatchdata() {
    var branchid = $('#branchname :selected').val();
    var forwardedto = $('#forwardedto :selected').val();
    var reqby = $('#recommendby :selected').val();
    var dispatchby = $('#reqby :selected').val();

    var inreqmes = {};
    var inreqList = [];
    $("table tr:not(:first)").each(function () {
        var tdlist = $(this).find("td");
        var pItem = {
            item: $(tdlist[0]).text(),
            ProductName: $(tdlist[1]).text(),
            quantity: $(tdlist[2]).html(),
            unit: $(tdlist[3]).html(),
        };
        inreqList.push(pItem);
    })
    inreqmes.inreqList = inreqList;
    inreqmes.branch_id = branchid;
    inreqmes.Forwarded_To = forwardedto;
    inreqmes.Requ_Message = $('#reqmessage').val();
    inreqmes.Delivered_By = dispatchby;
    inreqmes.Requ_by = reqby;
    $.ajax({
        url: "/api/InvMovementApi/ProductReturnCreate",
        type: "Post",
        contentType: "application/json",
        data: JSON.stringify(inreqmes),
        success: function (data) {
            swal({
                titel: "", text: "Product Returned Successfully.", icon: "success",
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

$("#myModal").on('click', '.serialnosave', function () {
    alert("successfull saved");
    $("#myModal").modal("hide");
});