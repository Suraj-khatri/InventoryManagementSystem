$("#saveproductdispose").click(function () {
    var $btn = $(this);
    $btn.button('loading');
    var disposemes = {};
    var disposedetailsList = [];
    $("table tr:not(:first)").each(function () {
        var tdlist = $(this).find("td");
        var pItem = {
            ProductId: $(tdlist[0]).text(),
            DisposeQty: $(tdlist[3]).text(),
        };
        disposedetailsList.push(pItem);
    })
    disposemes.Id = $('#disposemesid').val();
    disposemes.DisposeReason = $('#disposemessage').val();
    disposemes.disposedetailsList = disposedetailsList;
    $.ajax({
        url: "/DisposeProduct/ProductDispose",
        type: "Post",
        contentType: "application/json",
        data: JSON.stringify(disposemes),
        success: function (data) {
            if (data.success == true) {
                $btn.button('reset');
                window.location.replace("/DisposeProduct/ApproveDisposeIndex", true)
            }
            else {
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

