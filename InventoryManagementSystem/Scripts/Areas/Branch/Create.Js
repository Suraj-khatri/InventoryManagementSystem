$('#provincename').change(function () {
    $('#districtname').empty();
    $.ajax({
        url: '/Branch/GetDistrictforProvinceName',
        type: 'POST',
        data: { provincename: $('#provincename').val() },
        success: function (data) {
            $.each(data, function (i, item) {
                $('#districtname').append(new Option(item.Text, item.Value));
            })
        },
        async: false
    });
});

$(document).ready(function () {
    $('#provincename').change();
    if (obj.BRANCH_ID > 0) {
        $('#districtname').val(obj.BRANCH_DISTRICT);
    }
});
