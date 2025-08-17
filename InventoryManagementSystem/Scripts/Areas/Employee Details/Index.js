function ViewEmpDet(data) {
    $('#datalist').empty();
    var rows = '<div>\
    <dl class="dl-horizontal">\
       <dt>\
            Emp. Code\
        </dt>\
        <dd>\
          <strong>:&nbsp;</strong>' + data.EMP_CODE+ '\
        </dd>\
        <dt>\
            Name\
        </dt>\
        <dd>\
          <strong>:&nbsp;</strong>' + data.FullName + '\
        </dd>\
        <dt>\
            Province No.\
        </dt>\
        <dd>\
          <strong>:&nbsp;</strong>' + data.TEMP_PROVINCE + '\
        </dd>\
        <dt>\
            Branch Name\
        </dt>\
        <dd>\
          <strong>:&nbsp;</strong>' + data.BranchName + '\
        </dd>\
        <dt>\
            Department Name\
        </dt>\
        <dd>\
          <strong>:&nbsp;</strong>' + data.DepartmentName + '\
        </dd>\
        <dt>\
            Designation\
        </dt>\
        <dd>\
          <strong>:&nbsp;</strong>' + data.Designation + '\
        </dd>\
        <dt>\
            Mobile No.\
        </dt>\
        <dd>\
          <strong>:&nbsp;</strong>' + data.PERSONAL_MOBILE + '\
        </dd>\
        <dt>\
            Date of Birth\
        </dt>\
        <dd>\
          <strong>:&nbsp;</strong>' + data.DateOfBirthString + '\
        </dd>\
        <dt>\
            Email\
        </dt>\
        <dd>\
         <strong>:&nbsp;</strong>' + data.PERSONAL_EMAIL + '\
        </dd>\
    </dl>\
</div>'
    $('#datalist').append(rows);
}