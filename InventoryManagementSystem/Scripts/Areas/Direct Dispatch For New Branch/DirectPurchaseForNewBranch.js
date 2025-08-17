var tsum = 0.00;
var tqty = 0;
var vatamt = 0.00;
var j = 1;
var k = 1;
$(document).ready(function () {
    $('#tadd').hide();
    $('#datalist').empty();
    $.ajax({
        url: '/DirectDispatchForNewBranch/GetNonPrintingInStaticTempDispatchData',
        type: 'POST',
        success: function (data) {
            if (data.length > 0) {
                $('#tadd').show();
                $.each(data, function (i, item) {
                    var TotalAmount = k + "TotalAmount";
                    let Qty = i + "Qty";
                    let Rate = i + "Rate";
                    let Amount = i + "Amount";
                    $('#datalist').append('<tr class="checkQty">' +
                        '<td>' + j + '</td>' +
                        '<td>' + item.ProductId + '</td >' +
                        '<td>' + item.ProductName + '|' + item.ProductId + '</td >' +
                        '<td>' + item.Unit + '</td >' +
                        '<td class="pqty " id="purqty">' + '<input type="number" oninput="calculateAmount(' + i + ')" id ="' + Qty + '"  value="' + item.Qty + '" size="2" />' + '</td >' +
                        '<td class="prate" id="' + Rate + '">' + item.Rate.toFixed(2) + '</td>' +
                        '<td class=' + TotalAmount + ' id="' + Amount + '">' + (item.Qty * item.Rate).toFixed(2) + '</td >' +
                        '<td>' + '<input type="checkbox" id=' + k + ' class="checkeditem" name="row-check"/>' + '</td>' +
                        '</tr >');
                    j++;
                    k++;
                })
                footer();
            }
        }
    });
})

$("#saveorder").click(function () {
    var $btn = $(this);
    $btn.button('loading');
    var vId = $('#vendorname :selected').val();
    var bi = {};
    var polist = [];
    $("table tr:not(:first)").each(function () {
        var tdlist = $(this).find("td");
        var Item = {
            product_code: $(tdlist[1]).text(),
            qty: $(this).closest('tr').find("#purqty :input[type='number']").val(),
            rate: $(tdlist[5]).html(),
            IsRowCheck: $(tdlist[7]).find("input:checkbox").is(':checked')
        };
        polist.push(Item);
    })
    bi.party_code = vId;
    bi.vendor_code = vId;
    bi.bill_amount = tsum + vatamt;
    bi.vat_amt = vatamt;
    bi.polist = polist;
    bi.bill_notes = $('#billnotes').val();
    bi.billno = $('#billno').val();
    bi.bill_date = $('#billdate').val()
    $.ajax({
        url: "/api/PurchaseApi/DirectPurchaseFromDirectDispatch",
        type: "Post",
        contentType: "application/json",
        data: JSON.stringify(bi),
        success: function (data) {
            $btn.button('reset');
            window.location.replace(data.RedirectUrl, true)
            alert(data.VoucherNo);
        },
        error: function (err) {
            $btn.button('reset');
            alert(err.responseJSON.Message);
        }
    });
});

//calculate row amount
function calculateAmount(i) {
    let Qty = parseFloat($('#' + i + 'Qty').val()).toFixed(2);
    let Rate = parseFloat($('#' + i + 'Rate').text()).toFixed(2);
    let Amount = Qty * Rate;
    $('#' + i + 'Amount').text(Amount.toFixed(2));
}

function footer() {
    var total = 0
    $('#tfooter').empty();
    $('.checkeditem:checked').each(function () {
        total += parseFloat($("." + $(this).attr("id") + "TotalAmount").text());
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
        '<th>' + '</th >' +
        '<th>' + '</th >' +
        '<th>' + 'Sub Total :' + '</br>' + '13% VAT :' + '</br>' + 'Total Amount: ' + '</th > ' +
        '<th id="sumtot">' + (tsum).toFixed(2) + '</br>' + vatamt.toFixed(2) + '</br>' + (parseFloat(tsum) + parseFloat(vatamt)).toFixed(2) + '</th' +
        '<th>' + '</th >');
}

$(document).on('change', '#defaultUnchecked', function () {
    footer();
});

$(function () {
    $("#masterCheck").on("click", function () {
        if ($("input:checkbox[name='master']").prop("checked")) {
            $("input:checkbox[name='row-check']").prop("checked", true);
            footer();
        } else {
            $("input:checkbox[name='row-check']").prop("checked", false);
            $('#tfooter').empty();
        }
    });
    $("#datalist").on("input:checkbox[name='row-check']").on("change", function () {
        var total_check_boxes = $("input:checkbox[name='row-check']").length;
        var total_checked_boxes = $("input:checkbox[name='row-check']:checked").length;

        if (total_check_boxes === total_checked_boxes) {
            $("#masterCheck").prop("checked", true);
            footer();
        }
        else {
            $("#masterCheck").prop("checked", false);
            footer();
        }
    });
});