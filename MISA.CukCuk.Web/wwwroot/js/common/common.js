/**
 * format data of datetime to dd/mm/yyyy
 * @param {Date} date any type of data
 * createdBy: pqkhanh (27/12/2020)
 */
function formatDate(date) {
    var date = new Date(date);
    if (!Date.parse(date)) {
        return "";
    } else {
        var day = date.getDate(),
            month = date.getMonth() + 1,
            year = date.getFullYear();
        day = day < 10 ? '0' + day : day;
        month = month < 10 ? '0' + month : month;

        return day + '/' + month + '/' + year;
    }
}

/**
 * function used to show value of money after format
 * @param {Number} money number of money
 * createdBy: pqkhanh (27/12/2020)
 */
function formatMoney(money) {
    if (money) {
        return money.toString().replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$&.");
    }
    return "";
}

/**
 * function used to convert string to date dd-mm-yyyy
 * @param {Date} dateInput
 * createdBy : pqkhanh (29/12/2020)
 */
function formatStringDate(dateInput) {
    if (dateInput) {
        let year = parseInt(dateInput.substr(0, 4));
        let month = parseInt(dateInput.substr(5, 2));
        let day = parseInt(dateInput.substr(8, 2));

        day = day >= 10 ? day : '0' + day;
        month = month >= 10 ? month : '0' + month;

        var date = `${year}-${month}-${day}`;
        return date;
    }
    return "";
}

/**
 * function used to format gender
 * @param {Number} gender 
 * createdBy: pqkhanh(14/1/2021)
 */
function formatGender(gender) {
    if (gender == 1)
        return "Nam";
    else if (gender == 0)
        return "Nữ";
    else if (gender == 2)
        return "Khác";
    else
        return "";
}

/**
 * function used to format work status
 * @param {Number} workStatus
 * createdBy: pqkhanh(15/1/2021)
 */
function formatWorkStatus(workStatus) {
    if (workStatus == 1)
        return "Đang làm việc";
    else if (workStatus == 0)
        return "Đã nghỉ việc";
    else if (workStatus == 2)
        return "Đang thử việc";
    else
        return "";
}

//All this function used to add dot after 3 number
//createdBy: pqkhanh (22/01/2021) 

function reverseNumber(input) {
    return [].map.call(input, function (x) {
        return x;
    }).reverse().join('');
}

function plainNumber(number) {
    return number.split('.').join('');
}

function splitInDots(input) {

    var value = input.value,
        plain = plainNumber(value),
        reversed = reverseNumber(plain),
        reversedWithDots = reversed.match(/.{1,3}/g).join('.'),
        normal = reverseNumber(reversedWithDots);

    console.log(plain, reversed, reversedWithDots, normal);
    input.value = normal;
}