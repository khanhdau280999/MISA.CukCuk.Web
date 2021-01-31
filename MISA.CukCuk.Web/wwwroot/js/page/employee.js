$(document).ready(function () {
    new EmployeeJS();
})

/**
 * Class used to manage events for Employee
 * createdBy: pqkhanh (28/12/2020)
 * */
class EmployeeJS extends BaseJS {
    constructor() {
        super();
        this.objectName = "Employee";
    }

    initEvents() {
        var me = this;
        super.initEvents();
        $('#txtSearchEmployee, .cbxFilter1, .cbxFilter2').on('input', function (event) {
            me.setSubApi();
            me.loadData();
        });
        $('#txtSalary, #txtIdentityNumber, #txtPersonalTaxCode').on('input keypress keyup blur', function (event) {
            if ((event.which != 46 || $(this).val().indexOf('.') != -1) && (event.which < 48 || event.which > 57)) {
                event.preventDefault();
            }
        })
    }

    setSubApi() {
        var inputValue = $('#txtSearchEmployee').val();

        var departmentId = $('select.cbxFilter1 option:selected').val();
        if (departmentId == undefined)
            departmentId = '';

        var positionId = $('select.cbxFilter2 option:selected').val();
        if (positionId == undefined)
            positionId = '';

        this.subApi = "/filter?inputValue=" + inputValue + "&departmentId=" + departmentId + "&positionId=" + positionId + "";


    }

    setApiRouter() {
        this.apiRouter = "/api/v1/employees";
    }
}
