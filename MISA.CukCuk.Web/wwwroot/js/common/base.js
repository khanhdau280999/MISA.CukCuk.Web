class BaseJS {
    constructor() {
        this.host = "";
        this.apiRouter = null;
        this.subApi = "";
        this.objectName = null;
        this.setApiRouter();
        this.setSubApi();
        this.loadData();
        this.loadCombobox();
        this.initEvents();
    }

    //set api router
    setApiRouter() {

    }

    //set sub api
    setSubApi() {

    }

    //base event
    initEvents() {
        var me = this;

        //Event click on button add new
        $('.button-default').click(me.btnAddOnClick.bind(me));

        //Event reload data when click button load
        $('#button-refresh').click(function () {
            $.jnoty("Cập nhật dữ liệu thành công!", {
                header: 'Thành Công',
                theme: 'jnoty-success',
                icon: 'fa fa-check-circle fa-2x'
            });
            me.subApi = "";
            this.loadData();
            $('#txtSearchEmployee, .cbxFilter1 , .cbxFilter2').val('');
        }.bind(this));

        //Hide form dialog when click button "Huy" and "X"
        $('#button-cancel').click(function () {
            $('.dialog-detail').removeClass('show-dialog');
            $('.dialog-detail').addClass('hide-dialog');
        });
        $('#button-x').click(function () {
            $('.dialog-detail').removeClass('show-dialog');
            $('.dialog-detail').addClass('hide-dialog');
        });

        //Event click on button "Xoa"
        $('#button-delete').click(me.btnDeleteOnClick.bind(me));

        //Excute save data when click button "Luu"
        $('#button-save').click(me.btnSaveOnClick.bind(me));

        //Show detail infor when double click on object in table(dynamic event definitions)
        $('table tbody').on('dblclick', 'tr', function () {
            try {
                me.FormMode = 'Edit';
                //Get primary key from table
                var recordId = $(this).data('recordId');
                me.recordId = recordId;
                //Get object id from table
                var objectId = $(this).data('objectId');
                me.objectId = objectId;
                //Call service to get detail infor by Id
                $.ajax({
                    url: me.host + me.apiRouter + `/${recordId}`,
                    method: "GET"
                }).done(function (res) {
                    //data collection has been entered -> build to object
                    var inputs = $('input[fieldName], select[id]');
                    $.each(inputs, function (index, input) {
                        var propertyName = $(this).attr('fieldName');
                        var value = res[propertyName];

                        //for customer group combobox
                        if ($(this).attr('id') == 'cbxPosition') {
                            var propValueName = $(this).attr('fieldValue');
                            value = res[propValueName];
                            $(this).val(value);
                        }
                        //for postion combobox
                        else if ($(this).attr('id') == 'cbxCustomerGroup') {
                            var propValueName = $(this).attr('fieldValue');
                            value = res[propValueName];
                            $(this).val(value);
                        }
                        //for gender combobox
                        else if ($(this).attr('id') == 'cbxGender') {
                            var propValueName = $(this).attr('fieldValue');
                            value = res[propValueName];
                            $(this).val(value);
                        }
                        //for work status combobox
                        else if ($(this).attr('id') == 'cbxWorkStatus') {
                            var propValueName = $(this).attr('fieldValue');
                            value = res[propValueName];
                            $(this).val(value);
                        }
                        //for department combobox
                        else if ($(this).attr('id') == 'cbxDepartment') {
                            var propValueName = $(this).attr('fieldValue');
                            value = res[propValueName];
                            $(this).val(value);
                        }
                        //for base salary input
                        else if ($(this).attr('id') == 'txtSalary') {
                            $(this).val(formatMoney(value));
                        }
                        //for input date type
                        else if ($(this).attr('type') == 'date') {
                            $(this).val(formatStringDate(value));
                        }
                        //for input radio type
                        else if ($(this).attr('type') == "radio") {
                            var inputValue = this.value;

                            if (value == inputValue) {
                                this.checked = true;
                            } else {
                                this.checked = false;
                            }
                        } else {
                            $(this).val(value);
                        }

                    })


                }).fail(function (res) {

                })
                //show dialog
                $('.dialog-detail').addClass('show-dialog');
                $('.dialog-detail').removeClass('hide-dialog');
            } catch (e) {
                console.log(e);
            }
        });

        /*
         * validate empty input
         * createdBy: pqkhanh (30/12/2020)
         */
        $('input[required]').blur(function () {
            //check value input; if value is empty, web will give caution
            var value = $(this).val();
            if (!value) {
                $(this).addClass('border-red');
                $(this).attr('title', 'Trường này không được phép để trống!');
                $(this).attr("validate", false);
            } else {
                $(this).removeClass('border-red');
                $(this).attr("validate", true);
            }
        });

        /*
         * validate email
         * createdBy: pqkhanh (30/12/2020)
         */
        $('input[type="email"]').blur(function () {
            var value = $(this).val();
            var testEmail = /^[A-Z0-9._%+-]+@([A-Z0-9-]+\.)+[A-Z]{2,4}$/i;
            if (!testEmail.test(value)) {
                $(this).addClass('border-red');
                $(this).attr('title', 'Email không đúng định dạng!');
                $(this).attr("validate", false);
            } else {
                $(this).removeClass('border-red');
                $(this).attr("validate", true);
            }
        })
    }
    /**
    * Load data
    * createdBy: pqkhanh (28/12/2020)
    * */
    loadData() {
        var me = this;
        try {
            $('.loading').show();
            $('table tbody').empty();
            //get value for column
            var columns = $('table thead th');
            //get data
            $.ajax({
                url: me.host + me.apiRouter + me.subApi,
                method: "GET",
            }).done(function (res) {
                var data = res;
                $.each(data, function (index, obj) {
                    var check = me.objectName;
                    var tr = $(`<tr></tr>`);
                    if (check == "Customer") {
                        $(tr).data('recordId', obj.CustomerId);
                        $(tr).data('objectId', obj.CustomerCode);
                    }
                    else {
                        $(tr).data('recordId', obj.EmployeeId);
                        $(tr).data('objectId', obj.EmployeeCode);
                    }
                    //get value use for mapping with the corresponding columns
                    $.each(columns, function (index, th) {
                        var td = $(`<td></td>`);

                        var fieldName = $(th).attr('fieldName');
                        var value = obj[fieldName];
                        var formatType = $(th).attr('formatType');
                        switch (formatType) {
                            case "ddmmyyyy":
                                td.addClass("text-align-center");
                                $(th).addClass("text-align-center");
                                value = formatDate(value);
                                break;
                            case "Money":
                                td.addClass("text-align-right");
                                $(th).addClass("text-align-right");
                                value = formatMoney(value);
                                break;
                            case "Gender":
                                value = formatGender(value);
                                break;
                            case "WorkStatus":
                                value = formatWorkStatus(value);
                            default:
                                break;
                        }
                        td.append(value);
                        $(tr).append(td);
                    })
                    $('table tbody').append(tr);

                })
                $('.loading').hide();
            }).fail(function (res) {
                $('.loading').hide();
            })
        } catch (e) {
            //log error
            console.log(e);
        }
    }

    /**
     * load combobox
     * createdBy: pqkhanh (4/1/2021)
     * */
    loadCombobox() {
        var me = this;
        try {
            $('.loading').show();
            //load data for combobox
            var selects = $('select[fieldName]');
            $.each(selects, function (index, select) {
                //get value of group customers
                var api = $(select).attr('api');
                var fieldName = $(select).attr('fieldName');
                var fieldValue = $(select).attr('fieldValue');
                $.ajax({
                    url: me.host + api,
                    method: "GET",
                    async: true
                }).done(function (res) {
                    if (res) {
                        $.each(res, function (index, obj) {
                            var option = $(`<option value="${obj[fieldValue]}">${obj[fieldName]}</option>`);
                            $(select).append(option);
                        })
                    }
                    $('.loading').hide();
                }).fail(function (res) {
                    $('.loading').hide();
                })
            })
        } catch (e) {
            console.log(e);
        }
        
    }

    /* Function excute event when click on "Thêm mới khách hàng" button 
    * createdBy: pqkhanh (4/1/2021)
    * */
    btnAddOnClick() {
        try {
            var me = this;
            me.FormMode = 'Add';
            //show dialog
            $('.dialog-detail').addClass('show-dialog');
            $('.dialog-detail').removeClass('hide-dialog');
            //Reset and format value for input
            $('input[type !="radio"]').val(null);
            $('input[type="radio"]').prop('checked', false);
            $(`#txt${me.objectName}Code`).focus();
            $("option:selected").prop("selected", false);
            $('input').removeClass('border-red');
            //Getting and auto binding objectCode
            $.ajax({
                url: me.host + me.apiRouter + `/maxcode`,
                method: "GET",
            }).done(function (res) {
                var newObjectCode = parseInt(res) + 1;
                $('#txtEmployeeCode').val(`NV${newObjectCode}`);
            }).fail(function (res) {

            })
        } catch (e) {
            console.log(e);
        }
    }

    /**
     * Function excute event when click on "Lưu" button
     * createdBy: pqkhanh (4/1/2021)
     * */
    btnSaveOnClick() {
        var me = this;
        try {
            //validate data
            var inputValidates = $('.input-required, input[type="email"]');
            $.each(inputValidates, function (index, item) {
                $(item).trigger('blur');
            })

            var inputNotValids = $('input[validate="false"]');
            if (inputNotValids && inputNotValids.length > 0) {
                $.jnoty("Dữ liệu không hợp lệ vui lòng kiểm tra lại!", {
                    header: 'Cảnh báo',
                    theme: 'jnoty-danger',
                    icon: 'fa fa-info-circle fa-2x'
                });
                $('input[tabindex="1"]').focus();
                return;
            }
            //data collection has been entered -> build to object
            var inputs = $('input[fieldName], select[id]');
            var entity = {};
            $.each(inputs, function (index, input) {
                var propertyName = $(this).attr('fieldName');
                var value = $(this).val();

                //Check value of radio, Give only checked
                if ($(this).attr('type') == "radio") {
                    if (this.checked) {
                        entity[propertyName] = value;
                    }
                } else {
                    entity[propertyName] = value;
                }
                //Get value from select
                if (this.tagName == "SELECT") {
                    var propertyName = $(this).attr('fieldValue');
                    entity[propertyName] = value;
                }
                //Replace string for input
                if ($(this).attr('id') == 'txtSalary') {
                    entity[propertyName] = value.split(".").join("");
                }
            })
            var method = "POST";
            var id = ``;
            if (me.FormMode == 'Edit') {
                method = "PUT";
                id = `/${me.recordId}`;
            }
            //call service and save data
            $.ajax({
                url: me.host + me.apiRouter + id,
                method: method,
                data: JSON.stringify(entity),
                contentType: 'application/json',
            }).done(function (res) {
                //After save success -> Give a message, hide form dialog, reload data
                console.log(entity);
                var text = '';
                if (me.FormMode == 'Add') {
                    text = 'Thêm thành công!';
                } if (me.FormMode == 'Edit') {
                    text = 'Cập nhật thành công!';
                }

                $.jnoty(text, {
                    header: 'Thành Công',
                    theme: 'jnoty-success',
                    icon: 'fa fa-check-circle fa-2x'
                });

                $('.dialog-detail').removeClass('show-dialog');
                $('.dialog-detail').addClass('hide-dialog');
                me.loadData();

            }).fail(function (res) {
                var msg = JSON.parse(res.responseText);

                $.jnoty(`${msg.Data}`, {
                    header: 'Cảnh báo',
                    theme: 'jnoty-danger',
                    icon: 'fa fa-info-circle fa-2x'
                });
            })
        } catch (e) {
            console.log(e);
        }
    }

    /**
     * Function used to process event when click on "Xóa" button
     * createdBy: pqkhanh (8/1/2021)
     * */
    btnDeleteOnClick() {
        var me = this;
        try {
            $('.dialog-detail').removeClass('show-dialog');
            $('.dialog-detail').addClass('hide-dialog');
            var name = '';
            var check = me.objectName;
            if (check == "Customer")
                name = "khách hàng";
            else
                name = "nhân viên";

            lnv.confirm({
                title: 'Thông báo xác nhận',
                content: `Bạn có chắc chắn muốn xóa ${name} ${me.objectId} không?`,
                confirmBtnText: 'Có',
                confirmHandler: function () {
                    $.ajax({
                        url: me.host + me.apiRouter + `/${me.recordId}`,
                        method: "DELETE"
                    }).done(function (res) {
                        me.loadData();
                    }).fail(function (res) {

                    })

                    lnv.alert({
                        title: 'Thông báo xác nhận',
                        content: 'Xóa thành công!',
                        alertBtnText: 'xác nhận'
                    })
                },
                cancelBtnText: 'Không',
                cancelHandler: function () {
                    $('.dialog-detail').addClass('show-dialog');
                    $('.dialog-detail').removeClass('hide-dialog');
                }
            })
        } catch (e) {
            console.log(e);
        }
    }
}
