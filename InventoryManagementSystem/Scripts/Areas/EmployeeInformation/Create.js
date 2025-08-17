
$('#pzonename').change(function () {
    $('#pdistrictname').empty();
    $.ajax({
        url: '/EmployeeInformation/GetPerDistrictforZoneName',
        type: 'POST',
        data: { pzonename: $('#pzonename').val() },
        success: function (data) {
            $.each(data, function (i, item) {
                $('#pdistrictname').append(new Option(item.Text, item.Value));
            })
        },
        async: false
    });
});
$('#tzonename').change(function () {
    $('#tdistrictname').empty();
    $.ajax({
        url: '/EmployeeInformation/GetTempDistrictforZoneName',
        type: 'POST',
        data: { tzonename: $('#tzonename').val() },
        success: function (data) {
            $.each(data, function (i, item) {
                $('#tdistrictname').append(new Option(item.Text, item.Value));
            })
        },
        async: false
    });
});

$('#branchname').change(function () {
    $('#departname').empty();
    $.ajax({
        url: '/EmployeeInformation/GetDepartmentForBranchList',
        type: 'POST',
        data: { branchid: $('#branchname').val() },
        success: function (data) {
            $.each(data, function (i, item) {
                $('#departname').append(new Option(item.Text, item.Value));
            })
        },
        async: false
    });
});

$('#emptype').change(function () {
    if ($('#emptype :selected').text() == "Permanent") {
        $('#fromdate').addClass("hidden");
        $('#todate').addClass("hidden");
        $('#perdate').removeClass("hidden");
    }
    else if ($('#emptype :selected').text() == "--Select--") {
        $('#fromdate').addClass("hidden");
        $('#todate').addClass("hidden");
    }
    else {
        $('#fromdate').removeClass("hidden");
        $('#todate').removeClass("hidden");
        $('#perdate').addClass("hidden");
    }
});

$(document).ready(function ()
{
    $('#perdate').addClass("hidden");
    //disable dob after today-----start-------
    var today = new Date();
    var dd = today.getDate();
    var mm = today.getMonth() + 1; //January is 0!
    var yyyy = today.getFullYear();
    if (dd < 10) {
        dd = '0' + dd
    }
    if (mm < 10) {
        mm = '0' + mm
    }
    today = yyyy + '-' + mm + '-' + dd;
    document.getElementById('dob').setAttribute("max", today);
    document.getElementById('dob').setAttribute("min", "1940-01-01");
    //----------------------------end----------

    $('#pzonename').change();
    $('#tzonename').change();
    $('#branchname').change();

    if (obj.EMPLOYEE_ID > 0) {
        $('#pdistrictname').val(obj.PER_DISTRICT);
    }

    if (obj.EMPLOYEE_ID > 0) {
        $('#tdistrictname').val(obj.TEMP_DISTRICT);
    }

    if (obj.BRANCH_ID > 0) {
        $('#departname').val(obj.DEPARTMENT_ID);
    }
})
