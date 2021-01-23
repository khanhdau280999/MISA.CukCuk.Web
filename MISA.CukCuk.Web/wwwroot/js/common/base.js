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
        $('#btnAdd').click(me.btnAddOnClick.bind(me));
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
        $('#btnSave').click(me.btnSaveOnClick.bind(me));

        $("table").on("click", "tbody tr", this.rowOnClick);

        $("#btnDelete").on("click", me.btnDeleteOnClick.bind(me));

        // Hiển thị thông tin chi tiết khi nhấn đúp chuột chọn 1 bản ghi trên danh sách dữ liệu
        // CreatedBy: PQKHANH (26/12/2020)
        $('table tbody').on('dblclick', 'tr', function () {
            $(this).find('td').addClass('row-selected');
            //Load form:
            // load dữ liệu cho các combobox:
            //me.FormMode = 'Edit';
            //var recordId = $(this).data('recordId');
            //me.recordId = recordId;
            var selects = $('select[fieldName]');
            selects.empty();
            $.each(selects, function (index, select) {
                // lấy dữ liệu nhóm nhân viên:
                var api = $(select).attr('api');
                var fieldName = $(select).attr('fieldName');
                var fieldValue = $(select).attr('fieldValue');
                $('.loading').show();
                $.ajax({
                    url: me.host + api,
                    method: "GET"
                }).done(function (res) {
                    if (res) {
                        console.log(res);
                        $.each(res, function (index, obj) {
                            var option = $(`<option value="${obj[fieldValue]}">${obj[fieldName]}</option>`);

                            select.append(option);
                            console.log(option);
                        })
                    }
                    $('.loading').hide();
                }).fail(function (res) {
                    $('.loading').hide();
                })
            })

            me.FormMode = 'Edit';
            // Lấy khóa chính của bản ghi:
            var recordId = $(this).data('recordId');
            me.recordId = recordId;
            // Gọi service lấy thông tin chi tiết qua Id:
            $.ajax({
                url: me.host + me.apiRouter + `/${recordId}`,
                method: "GET"
            }).done(function (res) {
                // Binding dữ liệu lên form chi tiết:
                console.log(res);

                // Lấy tất cả các control nhập liệu:
                var inputs = $('input[fieldName], select[fieldName]');
                var entity = {};
                $.each(inputs, function (index, input) {
                    var propertyName = $(this).attr('fieldName');
                    var value = res[propertyName];
                    $(this).val(value);
                    entity[propertyName] = value;
                })
            }).fail(function (res) {

            })
            // Build lên form chi tiết :
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


    /**
     * Load dữ liệu
     * CreatedBy: PQKHANH (25/12/2020)
     * */
    loadData() {
        var me = this;
        try {
            $('table tbody').empty();
            // Lấy thông tin các cột dữ liệu
            var columns = $('table thead th');
            var getDataUrl = this.getDataUrl;
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

    /**
     * Hàm xử lý khi nhấn button thêm mới
     * CreatedBy: PQKHANH (28/12/2020)
     * 
     * */
    btnAddOnClick() {
        try {
            var me = this;
            me.FormMode = 'Add';
            //Hiển thị dialog thông tin chi tiết:
            dialogDetail.dialog('open');
            $('input').val(null);
            // load dữ liệu cho các combobox:
            //var select = $('select#cbxPosition');
            var select = $('select#cbxDepartment');
            //var select = $('select#cbxWorkStatus');
            select.empty();
            // lấy dữ liệu vị trí:

            $('.loading').show();
            $.ajax({
                url: me.host + "/api/employees",
                method: "GET"
            }).done(function (res) {
                if (res) {
                    console.log(res);
                    $.each(res, function (index, obj) {
                        var option = $(`<option value="${obj.DepartmentId}">${obj.DepartmentName}</option>`);

                        select.append(option);
                        console.log(option);
                    })
                }
                $('.loading').hide();
            }).fail(function (res) {
                $('.loading').hide();
            })
        } catch (e) {
            console.log(e);
        }
    }


    /**
     * Hàm xử lý khi nhấn button lưu
     * CreatedBy: PQKHANH (28/12/2020)
     *
     * */
    btnSaveOnClick() {
        var me = this;
        // validate dữ liệu:
        var inputValidates = $('input[required], input[type="email"]');
        $.each(inputValidates, function (index, input) {
            $(input).trigger('blur');
        })
        var inputNotValids = $('input[validate="false"]');
        if (inputNotValids && inputNotValids.length > 0) {
            alert("Dữ liệu không hợp lệ vui lòng kiểm tra lại");
            inputNotValids[0].focus();
            return;
        }
        // thu thập thông tin dữ liệu -> build thành object
        // Lấy tất cả các control nhập liệu:
        var inputs = $('input[fieldName], select[fieldName]');
        var entity = {};
        $.each(inputs, function (index, input) {
            var propertyName = $(this).attr('fieldName');
            var value = $(this).val();
            entity[propertyName] = value;
            // Check với trường hợp input là radio, thì chỉ lấy value của input có attribute là checked
            if ($(this).attr('type') == "radio") {
                if (this.checked) {
                    entity[propertyName] = value;
                }
            } else {
                entity[propertyName] = value;
            }
        })
        var method = "POST";
        if (me.FormMode == 'Edit') {
            var method = "PUT";
            entity.CustomerId = me.recordId;
        } else if (me.FormMode == 'Delete') {
            var method = "DELETE";
        }

        // gọi service tương ứng thực hiện lưu dữ liệu:
        $.ajax({
            url: me.host + me.apiRouter,
            method: method,
            data: JSON.stringify(entity),
            contentType: 'application/json'
        }).done(function (res) {
            // Sau khi lưu thành công thì: 
            //đưa ra thông báo thành công 
            //ẩn form chi tiết 
            //load lại dữ liệu
            alert('thêm thành công');
            dialogDetail.dialog('close');
            me.loadData();
        }).fail(function (res) {

        })
    }
    /**
     * sự kiện khi nhấn vào 1 hàng
     * CreatedBy : PQKHANH (30/12/2020)
     * @param {any} sender
     */
    rowOnClick(sender) {
        this.classList.add("row-selected");
        $(this).siblings().removeClass("row-selected");
    }

    /**
     * hàm sử lý khi nhấn button xóa
     * CreatedBy : PQKHANH (30/12/2020)
     * @param {any} sender
     */
    btnDeleteOnClick(sender) {
        var me = this;
        //me.FormMode = 'Delete';
        var formMode = sender.data;
        // Lấy khóa chính của bản ghi:
        //var recordId = $(this).data('recordId');
        //me.recordId = recordId;
        var rowSelected = $('tr.row-selected');
        if (rowSelected && rowSelected.length == 1) {
            var recordId = $('tr.row-selected').data('id');
            $.ajax({
                url: me.host + me.apiRouter + `/${recordId}`,
                method: "DELETE",
            }).done(function (res) {
                // Thực hiện binding dữ liệu lên form chi tiết:
                me.loadData();
            }).fail(function () {
                alert("Lỗi");
            });
        }
    }
}