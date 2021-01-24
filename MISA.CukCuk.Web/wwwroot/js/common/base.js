class BaseJS {
    constructor() {
        this.host = "http://localhost:15369/api/v1";
        this.apiRouter = null;
        this.setApiRouter();
        this.initEvents();
        this.loadData();
    }

    setApiRouter() {

    }

    initEvents() {
        var me = this;
        // Sự kiện click khi nhấn thêm mới:

        $('#btnAdd').click(function () {
            me.FormMode = 'Add';
            // hiển thị dialog thêm
            $('input').val('');
            dialogDetail.dialog('open');
            //hiển thị combobox
            me.loadCombobox();
        })

        // Load lại dữ liệu khi nhấn button nạp:
        // Sự kiện click khi refresh thêm mới:
        $('#btnRefresh').click(function () {
            me.loadData();
        })
        // Ẩn form chi tiết khi nhấn hủy:
        $('#btnCancel').click(function () {
            dialogDetail.dialog('close');
        })
        // Thực hiện lưu dữ liệu khi nhấn button [Lưu] trên form chi tiết:
        $('#btnSave').click(function () {
            try {
                //validate dữ liệu
                var inputValidates = $('input[required], input[type="email"]');
                $.each(inputValidates, function (index, input) {
                    $(input).trigger('blur');
                })
                var inputNotValidate = $('input[validate = "false"]');
                if (inputNotValidate && inputNotValidate.length > 0) {
                    alert("Dữ liệu không hợp lện xin vui lòng kiểm tra lại");
                    inputNotValidate[0].focus();
                    return;
                }
                //thu thập thông tin dữ liệu được nhập cho vào object
                //lấy tất cả các controll nhập liệu
                var entity = {};
                var inputs = $('input[fieldName], select[fieldName]');
                $.each(inputs, function (index, input) {
                    var propertyName = $(this).attr('fieldName');
                    var value = $(this).val();

                    //check với trường hợp input là radio, thì chỉ lấy value của input có atrribu là checked
                    entity[propertyName] = value;
                })
                //xác định phương thức cho nút lưu
                var method = "POST";
                var id = '';
                if (me.FormMode == 'Edit') {
                    method = "PUT";
                    id = '/' + me.recordId;
                    //entity.CustomerId = me.recordId;
                }
                //gọi service tương tác thực hiện lưu dữ liệu
                $.ajax({
                    url: me.host + me.apiRouter + id,
                    method: method,
                    data: JSON.stringify(entity),
                    contentType: 'application/json'
                }).done(function (res) {
                    //sau khi lưu thàng công
                    //+ đưa ra thông báo cho người dùng
                    alert("Thêm Thành công");
                    //+ Ẩn form chi tiết
                    dialogDetail.dialog('close');
                    //+ load lại dữ liệu
                    me.loadData();
                }).fail(function (res) {
                    alert("Thêm không thành công vui lòng thử lại hoặc liên hệ Misa");

                })
            } catch (e) {
                console.log(e);
            }
        })

        // Hiển thị thông tin chi tiết khi nhấn đúp chuột chọn 1 bản ghi trên danh sách dữ liệu
        // CreatedBy: PQKHANH (26/12/2020)
        $('table tbody').on('dblclick', 'tr', function () {
            try {
                me.FormMode = 'Edit';
                // lấy khóa chính của bản ghi
                var recordId = $(this).data('recordId');
                me.recordId = recordId;
                //gọi service lấy thông tin chi tiết theo id
                $.ajax({
                    url: me.host + me.apiRouter + `/${recordId}`,
                    method: "GET"
                }).done(function (res) {
                    //build lên form
                    // lấy tất cả các controll nhập liệu
                    var entity = {};
                    var inputs = $('input[fieldName], select[fieldName]');
                    $.each(inputs, function (index, input) {
                        var propertyName = $(this).attr('fieldName');
                        var value = res[propertyName];
                        if (propertyName == "DateOfBirth" || propertyName == "JoinDate" || propertyName == "IdentityDate") {
                            var dateValue = formatDate(value);
                            $(this).val(dateValue);
                        } else {
                            $(this).val(value);
                        }
                    })
                }).fail(function (res) {

                })
            } catch (e) {
                console.log(e);
            }
            dialogDetail.dialog('open');
        })

        /*
         * validate bắt buộc nhập:
         * CreatedBy: PQKHANH (27/12/2020)
         */
        $('input[required]').blur(function () {
            // kiểm tra dữ liệu đã nhập , nếu để trống thì cảnh báo:
            var value = $(this).val();
            if (!value) {
                $(this).addClass('border-red');
                $(this).attr('title', 'trường này không được phép để trống');
                $(this).attr("validate", false)
            } else {
                $(this).removeClass('border-red');
                $(this).attr("validate", true)
            }

        })

        /*
       * validate email đúng định dạng
       * CreatedBy: PQKHANH (27/12/2020)
       */
        $('input[type="email"]').blur(function () {
            var value = $(this).val();
            var testEmail = /^[A-Z0-9._%+-]+@([A-Z0-9-]+\.)+[A-Z]{2,4}$/i;
            if (!testEmail.test(value)) {
                $(this).addClass('border-red');
                $(this).attr('title', 'Email không đúng định dạng.');
                $(this).attr("validate", false);
            } else {
                $(this).removeClass('border-red');
                $(this).attr("validate", true);
            }
        })
    }


    loadCombobox() {
        var me = this;
        // Hiển thị combo box
        var selects = $('select#cbxPosition , select#cbxDepartment');
        selects.empty();
        $.each(selects, function (index, select) {
            var api = $(select).attr('api');
            var fieldName = $(select).attr('fieldName');
            var fieldValue = $(select).attr('fieldValue');
            if (fieldValue == "DepartmentName") {
                var optionDeault = $(`<option value="">Tất cả phòng ban</option>`);
                $(select).append(optionDeault);
            }
            if (fieldValue == "PositionName") {
                var optionDeault = $(`<option value="">Tất cả chức vụ</option>`);
                $(select).append(optionDeault);
            }
            $.ajax({
                url: me.host + '/' + api,
                method: "GET"
            }).done(function (res) {
                if (res) {
                    $.each(res, function (index, obj) {
                        var option = $(`<option value="${obj[fieldName]}">${obj[fieldValue]}</option>`)
                        $(select).append(option);
                    })
                }
            }).fail(function (res) {

            })
        })

    }


    /**
     * Load dữ liệu
     * CreatedBy: PQKHANH (25/12/2020)
     * */
    loadData() {
        var me = this;
        try {
            $('table tbody').empty();
            //Hiển thị comboBox
            me.loadCombobox();
            // Lấy thông tin các cột dữ liệu
            var columns = $('table thead th');
            $('.loading').show();
            //Lấy dữ liệu về:
            $.ajax({
                url: me.host + me.apiRouter,
                method: "GET",
                async: true,
            }).done(function (res) {
                $.each(res, function (index, obj) {
                    var tr = $(`<tr></tr>`);
                    $(tr).data('recordId', obj.EmployeeId);
                    // Lấy thông tin dữ liệu sẽ map tương ứng với các cột:
                    $.each(columns, function (index, th) {
                        var td = $(`<td><div><span></span></div></td>`);
                        var fieldName = $(th).attr('fieldname');
                        var value = obj[fieldName];
                        var formatType = $(th).attr('formatType');
                        switch (formatType) {
                            case "ddmmyyyy":
                                td.addClass("text-align-center");
                                value = formatDate(value);
                                break;
                            case "MoneyVND":
                                td.addClass("text-align-right");
                                value = formatMoney(value);
                                break;
                            case "Gender":
                                value = value == 1 ? "Nam" : (value == 0 ? "Nữ" : (value == 2 ? "Khác" : ""));
                                break;
                            case "WorkStatus":
                                value = value == 1 ? "Đang làm việc" : (value == 0 ? "Đã nghỉ việc" : (value == 2 ? "Đang thử việc" : ""));
                                break;
                            default:
                                break;
                        }
                        td.append(value)
                        $(tr).append(td);
                    })
                    $('table tbody').append(tr);
                    $('.loading').hide();
                })

            }).fail(function (res) {

            })
        } catch (e) {
            //Ghi log lỗi:
            console.log(e);
        }


    }
}
    //loadComboboxDepartment() {
    //    var me = this;
    //    var select = $('select#cbxDepartmentName');
    //    select.empty();
    //    //Lấy dữ liệu nhóm khách hàng
    //    $.ajax({
    //        url: me.host + "/Departments",
    //        method: 'GET'
    //    }).done(function (res) {
    //        if (res) {
    //            $.each(res, function (index, obj) {
    //                var option = $(`<option value="${obj.DepartmentId}">${obj.DepartmentName}</option>`);
    //                select.append(option);
    //            })
    //        }
    //    }).fail(function (res) {
    //    });
    //}

    //loadComboboxPosition() {
    //    var me = this;
    //    var select = $('select#cbxPositionName')
    //    select.empty();
    //    //Lấy dữ liệu nhóm khách hàng
    //    $.ajax({
    //        url: me.host + "/Positions",
    //        method: 'GET'
    //    }).done(function (res) {
    //        if (res) {
    //            $.each(res, function (index, obj) {
    //                var option = $(`<option value="${obj.PositionId}">${obj.PositionName}</option>`);
    //                select.append(option);
    //            })
    //        }
    //    }).fail(function (res) {
    //    });
    //}
