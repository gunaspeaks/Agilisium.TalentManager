
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
    if ($("#EmployeeID").val().length > 0 && $("#ProjectID").val().length > 0) {
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
}

function getEmployeeOtherProjectAllocations() {
    if ($("#EmployeeID").val().length > 0 && $("#ProjectID").val().length > 0) {
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
}

function loadEmployeeDetailsForAllocationEditPage() {
    if ($("#EmployeeID").val().length > 0) {
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
}

function loadProjectDetailsForAllocationEditPage() {
    if ($("#ProjectID").val().length > 0) {
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
                $("#projectType").text(data["ProjectTypeName"]);

                // updating fields in new/edit allocation details
                //$("#AllocationStartDate").val(sDate);
                //$("#AllocationEndDate").val(eDate);
            },
            error: function () {
                alert("Error has occured while loading project details");
            }
        });
    }
}

function getPracticeManagerNameForSubPracticePage() {
    if ($("#PracticeID").val().length > 0) {
        $.ajax({
            url: "/SubPractice/GetPracticeName",
            type: "POST",
            data: { id: $("#PracticeID").val() },
            success: function (data) {
                if (data.length == 0) {
                    $("#practiceManagerName").text("  Practice Manager: None");
                }
                else {
                    $("#practiceManagerName").text("  Practice Manager: " + data);
                }
            },
            error: function () {
                alert("Unable to retrieve the Pratice Manager Name");
            }
        });
    }
}

function generateEmployeeIDForEmpPage() {
    if ($("#EmploymentTypeID").val().length > 0) {
        $.ajax({
            url: "/Employee/GenerateNewEmployeeID",
            type: "POST",
            data: { id: $("#EmploymentTypeID").val() },
            success: function (data) {
                $("#EmployeeID").val(data);
                $("#empID").val(data);
            },
            error: function (eid) { alert("Error while generating new Employee ID"); }
        });
    }
}

function loadSubPracticeDropDownForEmpPage() {
    if ($("#PracticeID").val().length > 0) {
        $.ajax({
            url: "/Employee/GetSubPracticeList",
            type: 'POST',
            data: { id: $("#PracticeID").val() },
            success: function (data) {
                $('#SubPracticeID').empty();
                $.each(data, function () {
                    $("#SubPracticeID").append($("<option></option>").val(parseInt(this['Value'])).text(this['Text']));
                });
                ddlSubPractice.prop('disabled', false);
            },
            error: function (xhr) { alert('Error while loading the Sub Practice list'); }
        });
    }
}


function getPracticeManagerNameForEmployeePage() {
    if ($("#PracticeID").val().length > 0) {
        $.ajax({
            url: "/SubPractice/GetPracticeName",
            type: "POST",
            data: { id: $("#PracticeID").val() },
            success: function (data) {
                if (data.length == 0) {
                    $("#practiceManager").text("None");
                }
                else {
                    $("#practiceManager").text(data);
                }
            },
            error: function () {
                alert("Unable to retrieve the Pratice Manager Name");
            }
        });
    }
    else {
        $("#practiceManager").text("");
        $("#subPracticeManager").text("");
    }
}

function getSubPracticeManagerNameForEmployeePage() {
    if ($("#SubPracticeID").val().length > 0) {
        $.ajax({
            url: "/SubPractice/GetSubPracticeManagerName",
            type: "POST",
            data: { id: $("#SubPracticeID").val() },
            success: function (data) {
                if (data.length == 0) {
                    $("#subPracticeManager").text("None");
                }
                else {
                    $("#subPracticeManager").text(data);
                }
            },
            error: function () {
                alert("Unable to retrieve the Pratice Manager Name");
            }
        });
    }
    else {
        $("#subPracticeManager").text("");
    }
}

