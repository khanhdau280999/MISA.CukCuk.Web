
$(document).ready(function () {
    new CustomerJS();
    dialogDetail = $(".m-dialog").dialog ({
        autoOpen: false,
        fluid: true,
        minWidth: 700,
        resizable: true,
        position: ({ my: "center", at: "center", of: window }),
        modal: true
    });
})

/**
 * Class quản lý sự kiện cho trang Customer
 * CreatedBy: PQKHANH(25/12/2020)
 * */
class CustomerJS extends BaseJS {
    constructor() {
        super();
    }
    setApiRouter() {
        this.apiRouter = "/customers";
    }
/**
 * Load dữ liệu
 * CreatedBy: PQKhanh (25/12/2020)
 * */
    //loadData() {
    //    //Lấy dữ liệu về:
    //    $.ajax({
    //        url: "http://api.manhnv.net/api/customers",
    //        method: "GET",
    //    }).done(function (res) {
    //        var data = res;
    //        $.each(data, function (index, item) {
    //            var dateOfBirth = item["DateOfBirth"];
    //            //var amount = item.DebitAmount;
    //            //amount = formatMoney(amount);
    //            dateOfBirth = formatDate(dateOfBirth);
    //            var checkbox = `<input type="checkbox" />`;
    //            if (item.Gender > 0) {
    //                var checkbox = `<input type="checkbox" checked />`;
    //            }
    //            var tr = $(`<tr>
    //                    <td><div><span>`+ item.CustomerCode + `</span></div></td>
    //                    <td><div><span>`+ item['FullName'] + `</span></div></td>
    //                    <td><div class="text-align-center">`+ checkbox + `</div></td>
    //                    <td><div><span>`+ dateOfBirth + `</span></div></td>
    //                    <td><div><span>`+ item['CustomerGroupName'] + `</span></div></td>
    //                    <td><div><span>`+ item['PhoneNumber'] + `</span></div></td>
    //                    <td><div><span>`+ item['Email'] + `</span></div></td>
    //                    <td><div style="max-width:250px"><span>`+ item['CompanyName'] + `</span></div></td>
    //                    <td><div><span>`+ item['DebitAmount'] + `</span></div></td>
    //                    <td><div><span>`+ item['CompanyTaxCode'] + `</span></div></td>
    //                </tr>`);
    //            $('table tbody').append(tr);
    //        });

    //    }).fail(function (res) {

    //    })
    ////binding dữ liệu lên table:
    //}

   
}


///**
// * Load dữ liệu
// * CreatedBy: PQKhanh (25/12/2020)
// * */
//function loadData() {
   
//}

///**
// * CreatedBy: PQKHANH (25/12/2020)
// * Format dữ liệu ngày tháng sang ngày/tháng/năm
// * @param {any} date tham số có kiểu dữ liệu bất kỳ
// * 
// */
//function formatDate(date) {
//    var date = new Date(date);
//    if (Number.isNaN(date.getTime())) {
//        return "";
//    } else {
//        var day = date.getDate(),
//            month = date.getMonth() + 1,
//            year = date.getFullYear();
//        day = day < 10 ? '0' + day : day;
//        month = month < 10 ? '0' + month : month;
//        return day + '/' + month + '/' + year;
//    }

//}

///**
// * Hàm định dạng hiển thị tiền tệ
// * @param {Number} money Số tiền
// * CreatedBy: PQKHANH (25/12/2020)
// */
//function formatMoney(money) {
//    var num = money.toFixed(0).replace(/(\d)(?=(\d{3})+(?!\d))/g, '$1.');
//    return num;
//}