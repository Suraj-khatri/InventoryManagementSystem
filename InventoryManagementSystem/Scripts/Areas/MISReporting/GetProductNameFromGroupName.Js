$('#prodgroup').change(function () {
    $('#prodname').empty();
    $.ajax({
        url: '/MISReporting/GetProductNameFromProductGroupName',
        type: 'POST',
        data: { groupid: $('#prodgroup').val(), branchid: $('#BranchId').val() },
        success: function (data) {
            $.each(data, function (i, item) {
                $('#prodname').append(new Option(item.Text, item.Value));
            })
        },
        async: false
    });
});