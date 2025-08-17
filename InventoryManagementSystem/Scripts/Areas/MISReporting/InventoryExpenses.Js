$('#branchid').change(function () {
    $('#departmentid').empty();
    $.ajax({
        url: '/MISReporting/GetDepartmentNameFromBranchName',
        type: 'POST',
        data: { branchid: $('#branchid').val() },
        success: function (data) {
            $.each(data, function (i, item) {
                $('#departmentid').append(new Option(item.Text, item.Value));
            })
        },
        async: false
    });
});