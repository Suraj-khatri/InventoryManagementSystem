$(document).ready(function () {
    $('#ToBranchName').change();
    /* $('#FromBranchName').change();*/
    $('#ToDeptName').change();
    $('#prodgroup').change();
    $('#FromDeptName').change();

})

$('#ToBranchName').change(function () {
    $('#ToDeptName').empty();

    $.ajax({
        url: '/InterBranchTransfer/GetDepartmentForBranchList',
        type: 'POST',
        data: { branchid: $('#ToBranchName').val() },
        success: function (data) {
            $.each(data, function (i, item) {
                $('#ToDeptName').append(new Option(item.Text, item.Value));
            })
            $('#ToDeptName').change();
        }
    });
});
$("#adddata").click(function () {
    getProductStockData();
});

function getProductNameForBranch() {
    $.ajax({
        url: '/InterBranchTransfer/GetProductNameForBranchList',
        type: 'POST',
        data: { branchid: $('#FromBranchName').val() },
        success: function (data) {
            $.each(data, function (i, item) {
                $('#ProductName').append(new Option(item.Text, item.Value));
            })
        }
    });
}

$('#FromBranchName').change(function () {
    $('#FromDeptName').empty();
    $('#SenderFrom').empty();
    $.ajax({
        url: '/InterBranchTransfer/GetDepartmentForBranchList',
        type: 'POST',
        data: { branchid: $('#FromBranchName').val() },
        success: function (data) {
            $.each(data, function (i, item) {
                $('#FromDeptName').append(new Option(item.Text, item.Value));
            })
            $('#FromDeptName').change();
        }
    });
});


$('#ToDeptName').change(function () {
    $('#SendTo').empty();
    $.ajax({
        url: '/InterBranchTransfer/GetEmployeeNameForBranchAndDepartmentList',
        type: 'POST',
        data: { branchid: $('#ToBranchName').val(), deptid: $('#ToDeptName').val() },
        success: function (data) {
            $.each(data, function (i, item) {
                $('#SendTo').append(new Option(item.Text, item.Value));
            })
        }
    });
});
$('#FromDeptName').change(function () {
    $('#SenderFrom').empty();
    $.ajax({
        url: '/InterBranchTransfer/GetEmployeeNameForBranchAndDepartmentList',
        type: 'POST',
        data: { branchid: $('#FromBranchName').val(), deptid: $('#FromDeptName').val() },
        success: function (data) {
            $.each(data, function (i, item) {
                $('#SenderFrom').append(new Option(item.Text, item.Value));
            })
        }
    });
});



$('#prodgroup').change(function () {
    $('#ProductName').empty();
    $.ajax({
        url: '/InterBranchTransfer/GetProductNameFromGroupName',
        type: 'POST',
        data: { groupid: $('#prodgroup').val() },
        success: function (data) {
            $.each(data, function (i, item) {
                $('#ProductName').append(new Option(item.Text, item.Value));
            })
        },
    });
});

$('#transfer').click(function (event) {
    event.preventDefault();
    const toBranchName = validateSelect('ToBranchName', 'branch in the "To Branch" field');
    if (!toBranchName) return;

    const fromBranchName = validateSelect('FromBranchName', 'branch in the "From Branch" field');
    if (!fromBranchName) return;

    //const prodgrp = validateSelect('prodgroup', 'group in the "Group" field');
    //if (!prodgrp) return;

    if (toBranchName === fromBranchName) {
        alert("'To Branch' and 'From Branch' cannot be the same.");
        $('#ToBranchName').focus();
        return;
    }

    const narration = $('#Narration').val().trim();  // Changed to match ID case
    if (narration === '') {
        alert("Narration cannot be empty");
        $('#Narration').focus();  // Changed to match ID case
        return;
    }
    tempData = [];
    transferData();
});
function validateSelect(elementId, fieldName) {
    const value = $(`#${elementId} option:selected`).text().trim();
    if (value === "--SELECT--" || value === "") {
        alert(`Please choose a valid ${fieldName}.`);
        $(`#${elementId}`).focus();
        return false;
    }
    return value;
}

let tempData = [];
let ids = [];
let tId = 0;
function transferData() {
    let isValid = true;
    let Serial = true;

    StoreData();

    console.log("before", tempData);

    // Check if there are no products in tempData
    if (tempData.length < 1) {
        alert("Please select a valid product and click on add");
        return false;
    }

    tempData.forEach(item => {
        // Validate quantity
        if (Number(item.Quantity) < 1) {
            isValid = false;
            console.log("Invalid quantity:", item.Quantity);
            return false;
        }

        // Validate serial numbers for serial products
        if (item.IsSerial === 'true') {
            if (!item.SerialNoFrom || !item.SerialNoTo || item.SerialNoFrom.trim() === '' || item.SerialNoTo.trim() === '') {
                Serial = false;
                console.log("Missing serial numbers:", item.SerialNoFrom, item.SerialNoTo);
            }
        }

        // Check if Quantity exceeds StockInBranch
        if (parseInt(item.Quantity) > parseInt(item.StockInBranch)) {
            isValid = false;
            console.log("Quantity exceeds stock:", item.Quantity, item.StockInBranch);
        }
    });

    if (!isValid) {
        alert("Quantity is either 0 or Quantity exceeds stock");
        return false;
    }

    if (!Serial) {
        alert("Serial Number Missing");
        return false;
    }

    let detail = {
        FromBranchId: $('#FromBranchName :selected').val(),
        ToBranchId: $('#ToBranchName :selected').val(),
        ProdGroupId: 0,
        ProductId: $('#ProductName :selected').val(),
        SenderTo: $('#SendTo :selected').val(),
        SenderFrom: $('#SenderFrom :selected').val(),
        Narration: $('#Narration').val(),
        ToDeptId: $('#ToDeptName :selected').val(),
        FromDeptId: $('#FromDeptName :selected').val()
    };

    let finalData = {
        "Flag": 'i',
        "details": detail,
        "data": tempData
    };

    const jsonData = JSON.stringify(finalData);

    console.log('metaData to post:', detail);
    console.log('DataDetails to post:', tempData);
    console.log('Data to post:', finalData);

    // Confirm action with SweetAlert
    swal({
        title: "",
        text: "Are You Sure?",
        icon: "info",
        closeOnClickOutside: false,
        closeOnEsc: false,
    }).then(okay => {
        if (okay) {
            $.ajax({
                url: '/InterBranchTransfer/Create',
                type: 'POST',
                contentType: "application/json",
                data: JSON.stringify({ MvJson: jsonData }),
                success: function (response) {
                    if (response.status === "success") {
                        tId = response.data;
                        $('#datalist').empty();

                        swal({
                            title: "",
                            text: "Successfully Transferred.",
                            icon: "success",
                            closeOnClickOutside: false,
                            closeOnEsc: false,
                        }).then(okay => {
                            if (okay) {
                                if (response.RedirectUrl) {
                                    window.location.href = response.RedirectUrl;
                                } else {
                                    window.location.href = '/InterBranchTransfer/Create';
                                }
                            }
                        });
                        SendNotification();
                        SendEmail();

                        ClearFields();
                    } else {
                        swal({
                            title: "Error",
                            text: response.message || "An unknown error occurred.",
                            icon: "error"
                        });
                    }
                },
                error: function () {
                    swal({
                        title: "Error",
                        text: "An error occurred while processing your request.",
                        icon: "error"
                    });
                }
            });
        }
    });
}

function SendNotification() {
    $.ajax({
        url: '/InterBranchTransfer/SendNotification',
        type: 'POST',
        contentType: 'application/json',
        data: JSON.stringify({
            Forwardedby: $('#SenderFrom :selected').val(),
            Forwardedto: $('#SendTo :selected').val(),
            Tid: tId

        }),

        success: function (response) {
            console.log(Forwardedto, Forwardedby)
            if (response.status === 'success') {
                alert(response.message);
            } else {
                alert('Error: ' + response.message);
            }
        },
        error: function (xhr, status, error) {
            console.log('Error:', error);
            alert('Something went wrong!');
        }
    });
}


function getProductStockData() {
    // Disable the button before making the AJAX request
    $("#adddata").prop('disabled', true);

    $.ajax({
        url: '/InterBranchTransfer/GetStockData',
        type: 'POST',
        data: {
            branchid: $('#FromBranchName :selected').val(),
            groupid: $('#prodgroup :selected').val(),
            productid: $('#ProductName :selected').val()
        },
        success: function (data) {
            if (data && data.length > 0) {
                $('#tadd').show();

                data.forEach(function (product) {
                    const productId = product.PRODUCT_ID.toString();

                    if (!ids.includes(productId)) {
                        $('#datalist').append(`
                            <tr>
                                <td class="pid">${product.PRODUCT_ID}</td>
                                <td class="pname">${product.ProductName}</td>
                                <td><input type="number" value="0" class="form-control qty-input" data-max="${product.qty}" /></td>
                                <td>${product.rate}</td>
                                <td>${product.qty}</td>
                                <td>${product.package_unit}</td>
                                <td><a class="btn btn-xs btnDelete" data-id="${product.PRODUCT_ID}">Delete</a></td>
                                ${product.isserial
                                ? `<td><a class="btn btn-xs btnADD" data-id="${product.PRODUCT_ID}">Add</a></td>`
                                : `<td></td>`
                            }
                                <td><input type="hidden" class="hidden-serial" value="${product.isserial}" /></td>
                                <td><input type="hidden" id="hidden-serialFrom" value="" /></td>
                                <td><input type="hidden" id="hidden-serialTo" value="" /></td>
                            </tr>
                        `);
                        ids.push(productId);
                    } else {
                        alert("Product already exists");
                        console.log('Product ID already exists in ids:', productId);
                    }
                });

                $('#ProductName').empty();
                $('#prodgroup').val("--SELECT--");
                console.log("Current product IDs:", ids);

            } else {
                alert("Product doesn't exist in branch");
            }
        },
        error: function () {
            alert('Failed to fetch product list. Please try again.');
        },
        complete: function () {
            // Re-enable the button after the operation is complete
            $("#adddata").prop('disabled', false);
        }
    });
}




$('#datalist').on('change', '.qty-input', function (event) {
    const maxQty = parseInt($(this).data('max'), 10);
    const currentValue = parseInt($(this).val(), 10);
    var productId = $(this).data('id');
    var productIndex = tempData.findIndex(item => item.PRODUCT_ID == productId);

    console.log('Max allowed:', maxQty, 'Current value:', currentValue);
    if (isNaN(currentValue) || currentValue < 1) {

        alert('Quantity cannot be less than 1.');
        $(this).val(0);
        return false;
    }

    if (currentValue > maxQty) {
        alert(`Quantity cannot exceed available stock: ${maxQty}`);
        $(this).val(maxQty);
        return false;
    }
    $('#hidden-serialFrom').val('');
    $('#hidden-serialTo').val('');

});

function StoreData() {
    let data = [];

    // Traverse through each row in the datalist table
    $('#datalist tr').each(function () {
        let row = $(this);

        // Extract values
        let productId = row.find('.pid').text().trim();
        let productName = row.find('.pname').text().trim();
        let qty = row.find('.qty-input').val().trim();
        let rate = row.find('td:nth-child(4)').text().trim();
        let stockInBranch = row.find('td:nth-child(5)').text().trim();
        let unit = row.find('td:nth-child(6)').text().trim();
        let isSerial = row.find('.hidden-serial').val();
        let serialNoFrom = row.find('#hidden-serialFrom').val().trim();
        let serialNoTo = row.find('#hidden-serialTo').val().trim();

        // Build the object
        let productData = {
            PRODUCT_ID: productId,
            ProductName: productName,
            Quantity: qty,
            Rate: rate,
            StockInBranch: stockInBranch,
            Unit: unit,
            IsSerial: isSerial,
            SerialNoFrom: serialNoFrom,
            SerialNoTo: serialNoTo
        };
        data.push(productData);
        tempData.push(productData);
        console.log("qty", tempData)
    });
}


// Delete functionality
$("#datalist").on('click', '.btnDelete', function (event) {
    var productId = $(this).data('id').toString();
    console.log('Product ID to remove:', productId);

    $(this).closest('tr').remove();

    var productIndex = ids.indexOf(productId);
    console.log("Current ids array:", ids);


    if (productIndex < 0) {
        console.log('Product ID not found in ids array.');
    } else {
        ids.splice(productIndex, 1);
        console.log('Updated ids array:', ids);
    }
});





$("#myModal").on('click', '.serialnosave', function () {
    saveserial();
    alert("successfull saved");
    $("#myModal").modal("hide");
});


function ClearFields() {
    $('#FromBranchName').val('');
    $('#ProductName').val('');
    $('#prodgroup').val('');
    $('#SendTo').val('');
    $('#narration').val('');
    $('#ToDeptName').val('');
}
let pQty = 0;
$("#datalist").on('click', '.btnADD', function (e) {
    e.preventDefault();
    console.log("temp", tempData);
    let row = $(this).closest('tr');
    let productQty = row.find('.qty-input').val().trim();


    if (productQty == 0) {
        alert("Please enter Quantity");
        return;
    }


    $("#myModal").modal("show");
    var productId = $(this).data('id');

    console.log("qty", productQty)

    console.log(productId)

    console.log(productQty)

    var a = ($('#hidden-serialFrom').val().trim() !== '') ? $('#hidden-serialFrom').val() : '';
    var b = ($('#hidden-serialTo').val().trim() !== '') ? $('#hidden-serialTo').val() : '';

    $('#prodinfo').off('change', '#serialFrom');

    $("#prodinfo").html(`
        <input type="hidden" id="productId" value="${productId}" /> 
        <div class="form-group">
            <label for="f1">Serial No(From)</label>
            <input type="text" id="serialFrom" class="form-control" placeholder="Enter Serial Number From" value="${a}" />
            <span id="f1-error" class="text-danger" style="display: none;">Field 1 is required.</span>
        </div>
        <div class="form-group">
            <label for="f2">Serial No(To)</label>
            <input type="text" id="serialTo" class="form-control" disabled = "disabled" placeholder="Enter Serial Number To" value="${b}" />
            <span id="f2-error" class="text-danger" style="display: none;">Field 2 is required.</span>
        </div>
    `);
    let alertShown = false;
    $('#prodinfo').on('change', '#serialFrom', function (e) {
        e.preventDefault();

        var serialNoFrom = $("#serialFrom").val().trim();
        var fromBranch = $('#FromBranchName :selected').val();
        console.log(serialNoFrom, fromBranch, productQty);

        let finalData = {
            "SerialNumber": serialNoFrom,
            "BranchId": fromBranch,
            "ProductId": productId,
            "ProductQty": productQty
        };

        const Data = JSON.stringify(finalData);

        $.ajax({
            url: '/InterBranchTransfer/CheckSerial',
            type: 'POST',
            contentType: "application/json",
            data: JSON.stringify({ model: Data }),
            success: function (response) {
                console.log(result)
                var result = 0;
                var parsedResponse = JSON.parse(response.data);
                result = parsedResponse[0];
                console.log('Response:', result);
                console.log(result.Header.Message);
                if (result.Header.Message == "Success") {
                    finalData = [];
                    $("#serialTo").val(result.Data.SerialNoTo);
                    $("#hidden-serialFrom").val(result.Data.SerialNoFrom);
                    $("#hidden-serialTo").val(result.Data.SerialNoTo);
                    alert("Success")
                    alertShown = false;
                } else {
                    alert('Error: ' + result.Header.Message);
                    alertShown = true;
                    $("#serialFrom").focus();
                }
            },
            error: function () {
                alert('An error occurred while processing your request.');
            }
        });


    });
});
function SendEmail() {

    $.ajax({
        url: '/InterBranchTransfer/SendEmail',
        type: 'POST',
        contentType: "application/json",
        data: JSON.stringify({
            Forwardedby: $('#SenderFrom :selected').val(),
            Forwardedto: $('#SendTo :selected').val(),
            Tid: tId,
            fromBranch: $('#FromBranchName :selected').val(),

        }),
        success: function (response) {
            if (response.status == "Success") {
            } else {
                alert('Error: ' + result.Header.Message);
            }
        },
        error: function () {
            alert('An error occurred while processing your request.');
        }
    });

}


