$(document).ready(function () {
    new EmployeeJS();
    dialogDetail = $(".m-dialog").dialog({
        autoOpen: false,
        fluid: true,
        minWidth: 700,
        resizable: true,
        position: ({ my: "center", at: "center", of: window }),
        modal: true
    });
})

/**
 * Class quản lý sự kiện cho trang Employee
 * CreatedBy: PQKHANH(25/12/2020)
 * */

class EmployeeJS extends BaseJS {
    constructor() {
        super();
        //this.loadData();
    }

    setApiRouter() {
        this.apiRouter = "/employees";
    }

}
