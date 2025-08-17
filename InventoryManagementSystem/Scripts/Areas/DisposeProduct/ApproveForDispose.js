$("#tbl_approvedispose").on("input", '#approveqty', function (e) {
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

$("#saveapprovedispose").click(function () {
    swal({
        titel: "", text: "Are You Sure ?", icon: "info",
        closeOnClickOutside: false,
        closeOnEsc: false,
    }
    ).then(okay => {
        if (okay) {
            approvedata();
        }
    });
});

function approvedata() {
    var disposemes = {};
    var disposedetailsList = [];
    $("table tr:not(:first)").each(function () {
        var tdlist = $(this).find("td");
        var pItem = {
            ProductId: $(tdlist[0]).text(),
            DisposeQty: $(this).closest('tr').find("#approveqty :input[type='number']").val(),
        };
        disposedetailsList.push(pItem);
    })
    disposemes.Id = $('#disposemesid').val();;
    disposemes.disposedetailsList = disposedetailsList;
    disposemes.DisposeReason = $('#disposemessage').val();
    $.ajax({
        url: "/DisposeProduct/ApproveForDispose",
        type: "Post",
        contentType: "application/json",
        data: JSON.stringify(disposemes),
        success: function (data) {
            if (data.success == true) {
                swal({
                    titel: "", text: data.mes, icon: "success",
                    closeOnClickOutside: false,
                    closeOnEsc: false,
                }
                ).then(okay => {
                    if (okay) {
                        window.location.replace("/DisposeProduct/ApproveDisposeIndex", true)
                    }
                });
            }
            else {
                swal("", data.err, "warning")
            }
        },
        error: function (err) {
            swal("", "Something Went Wrong!!", "warning")
        }
    });
}