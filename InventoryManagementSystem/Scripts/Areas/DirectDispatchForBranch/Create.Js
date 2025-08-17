$(document).ready(function () {
    $('#branchname').change();
    $('#prodname').change();
    $('#forwardedto').change();
    $('#departname').change();
})

$('#branchname').change(function () {
    $('#departname').empty();
    $('#prodname').empty();
    $.ajax({
        url: '/DirectDispatchForBranch/GetDepartmentForBranchList',
        type: 'POST',
        data: { branchid: $('#branchname').val() },
        success: function (data) {
            $.each(data, function (i, item) {
                $('#departname').append(new Option(item.Text, item.Value));
            })
            $('#departname').change();
            getProductNameForBranch();
        }
    });
});

function getProductNameForBranch() {
    $.ajax({
        url: '/DirectDispatchForBranch/GetProductNameForBranchList',
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
    $('#recommendby').empty();
    $.ajax({
        url: '/DirectDispatchForBranch/GetEmployeeForBranchList',
        type: 'POST',
        data: { branchid: $('#forwardedto').val() },
        success: function (data) {
            $.each(data, function (i, item) {
                $('#recommendby').append(new Option(item.Text, item.Value));
            })
        }
    });
});

$('#departname').change(function () {
    $('#reqby').empty();
    $.ajax({
        url: '/DirectDispatchForBranch/GetEmployeeNameForBranchAndDepartmentList',
        type: 'POST',
        data: { branchid: $('#branchname').val(), deptid: $('#departname').val() },
        success: function (data) {
            $.each(data, function (i, item) {
                $('#reqby').append(new Option(item.Text, item.Value));
            })
        }
    });
});

var temppur = [];
var tpname = "";
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
});

$("#saveorder").click(function () {
    swal({
        titel: "", text: "Are You Sure ?", icon: "info",
        closeOnClickOutside: false,
        closeOnEsc: false,
    }
    ).then(okay => {
        if (okay) {
            directdispatchdata();
        }
    });
});

function directdispatchdata() {

    var userRole = $("#assignedRole").val();
    var userBranchId = $("#branchId").val();
    const allowedRoles = [
        "Admin_User",
        "StoreKeeper(HeadOffice)",
        "Administrator"
    ];

    const isHeadOfficeBranch = userBranchId === "12";
    const isAllowedRole = allowedRoles.includes(userRole);

    if (isHeadOfficeBranch && !isAllowedRole) {
        alert("You are not allowed to perform this action to HeadOffice(999) with your current role.");
        return;
    }


    var priority = $('#priority :selected').text();
    var branchid = $('#branchname :selected').val();
    var deptid = $('#departname :selected').val();
    var Requby = $('#reqby :selected').val();
    var recommendby = $('#recommendby :selected').val();
    var forwardedto = $('#forwardedto :selected').val();
    var reqmessage = $('#reqmessage').val();;
    var inreqmes = {};
    var inreqList = [];
    $("table tr:not(:first)").each(function () {
        var tdlist = $(this).find("td");
        var pItem = {
            item: $(tdlist[0]).text(),
            //detailid: $(tdlist[1]).text(),
            ProductName: $(tdlist[1]).text(),
            quantity: $(tdlist[2]).html(),
            unit: $(tdlist[3]).html(),
        };
        inreqList.push(pItem);
    })
    inreqmes.inreqList = inreqList;
    inreqmes.Requ_Message = reqmessage;
    inreqmes.priority = priority;
    inreqmes.branch_id = branchid;
    inreqmes.dept_id = deptid;
    inreqmes.Requ_by = Requby;
    inreqmes.recommed_by = recommendby;
    inreqmes.Forwarded_To = forwardedto;
    $.ajax({
        url: "/api/InvMovementApi/DirectDispatchCreate",
        type: "Post",
        contentType: "application/json",
        data: JSON.stringify(inreqmes),
        success: function (data) {
            swal({
                titel: "", text: "successfully dispatched.", icon: "success",
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