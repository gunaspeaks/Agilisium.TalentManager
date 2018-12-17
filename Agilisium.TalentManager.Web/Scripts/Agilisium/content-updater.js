

formatJsonDateString = function (value) {
    var a;
    if (typeof value == 'string') {
        a = /\/Date\((\d*)\)\//.exec(value);
        if (a) {
            var dateVal = new Date(+a[1]);
            var month = dateVal.getMonth() + 1;
            var day = dateVal.getDate();
            var year = dateVal.getFullYear();
            return month + "/" + day + "/" + year;
        }
    }
    return value;
}

function getEmployeePercentageOfAllocation() {
    var v_data = {};
    v_data.empID = $("#EmployeeID").val();
    v_data.prjID = $("#ProjectID").val();

    $.ajax({
        url: "/Allocation/GetEmploymentDetails",
        type: "POST",
        data: JSON.stringify(v_data),
        contentType: "application/json",
        success: function (data) {
            $("#howMuchOccupied").text(data);
        },
        error: (function () {
            alert("Error has occured while loading employee details");
        })
    });
}

function getEmployeeOtherProjectAllocations() {
    var v_data = {};
    v_data.empID = $("#EmployeeID").val();
    v_data.prjID = $("#ProjectID").val();

    $.ajax({
        url: "/Allocation/GetEmployeeOtherAllocations",
        type: "POST",
        data: JSON.stringify(v_data),
        contentType: "application/json",
        success: function (data) {
            $("#otherAllocationItems").empty();
            $.each(data, function () {
                var text = $("#otherAllocations").text();
                text = text + " " + this;
                $("#otherAllocations").text(text.trim());
            });
        },
        error: (function () {
            alert("Error has occured while loading employee allocation details");
        })
    });
}

function loadEmployeeDetailsForAllocationEditPage() {
    $.ajax({
        url: "/Employee/GetEmployeeDetails",
        type: "POST",
        data: { id: $("#EmployeeID").val() },
        success: function (data) {
            $("#employeeID").text(data["EmployeeID"]);
            $("#employeeType").text(data["EmploymentTypeName"]);
            $("#primarySkills").text(data["PrimarySkills"]);
            $("#secondarySkills").text(data["SecondarySkills"]);
        },
        error: function () {
            alert("Error has occured while loading project details");
        }
    });
}

function loadProjectDetailsForAllocationEditPage() {
    $.ajax({
        url: "/Allocation/GetProjectDetails",
        type: "POST",
        data: { projectID: $("#ProjectID").val() },
        success: function (data) {
            var sDate = formatJsonDateString(data["StartDate"]);
            var eDate = formatJsonDateString(data["EndDate"]);

            // updating project details section
            $("#pmName").text(data["ProjectManagerName"]);
            $("#projectStartDate").text(sDate);
            $("#projectEndDate").text(eDate);

            // updating fields in new/edit allocation details
            //$("#AllocationStartDate").val(sDate);
            //$("#AllocationEndDate").val(eDate);
        },
        error: function () {
            alert("Error has occured while loading project details");
        }
    });
}