function GetSex(val) {
    if (val == '0') {
        return '女';
    }
    else if (val == '1') {
        return '男';
    }
}

function GetTrClass(val) {
    return val % 2 == 0 ? "tab_td" : "tab_td td_bg";
}
//数字(从0开始)转换为字母
function IntegerToLetter(n) {
    return String.fromCharCode(65 + parseInt(n));
}