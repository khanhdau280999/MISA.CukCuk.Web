$(document).ready(function () {
    new CustomerJS();
})

/**
 * Class used to manage events for Customer
 * createdBy: pqkhanh (28/12/2020)
 * */
class CustomerJS extends BaseJS {
    constructor() {
        super();
        this.objectName = "Customer";
    }
    setApiRouter() {
        this.apiRouter = "/api/v1/customers";
    }

}

