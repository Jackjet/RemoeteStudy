$(function () {
    // GetNewTime();
});
function GetNewTime() {
    jQuery.ajax({
        url: "OrderIndex.aspx?action=GetNowTime&" + Math.random(),   // 提交的页面
        type: "POST",                   // 设置请求类型为"POST"，默认为"GET"
        beforeSend: function ()          // 设置表单提交前方法
        {
            //alert("准备提交数据");
        },
        error: function (request) {      // 设置表单提交出错
            //alert("表单提交出错，请稍候再试");
            //rebool = false;
        },
        success: function (data) {
            var ss = data.split("|");
            if (ss[0] != "1") {
                $("#status").html("休息中");
            } else if (ss[0] == "1") {
                $("#status").html(ss[1]);
            }
            setInterval("GetNewTime()", 1000);
        }

    });

}
function CheckNumber(obj, id, price, date, type) {
    if ($(obj).val().length > 0) {
        if (isNaN($(obj).val())) {
            alert("请正确输入！");
            $(obj).val($(obj).next().val());
            return;
        }
        if ($(obj).val().indexOf('.') >= 0 || $(obj).val() <= 0) {
            alert("请输入正数！");
            $(obj).val($(obj).next().val());
            return;
        }
    } else {
        alert("请输入数量!");
        $(obj).val($(obj).next().val());
        return;
    }
    if ($(obj).val() == $(obj).next().val()) {
        return;
    }
    var num = $(obj).val();
    if (num.length > 2) { alert('请输入长度小于100的数量！'); $(obj).val($(obj).next().val()); }
    //alert("id:" + id + " type:" + type + " num:" + num);
    if (id != null && id != "" && type != null && type != "" && num != null && num != "") {
        jQuery.ajax({
            url: "OrderIndex.aspx?action=ChangeNum&" + Math.random(),   // 提交的页面
            type: "POST",                   // 设置请求类型为"POST"，默认为"GET"
            data: { "id": id, "type": type, "num": num, "date": date },
            beforeSend: function ()          // 设置表单提交前方法
            {
                //alert("准备提交数据");


            },
            error: function (request) {      // 设置表单提交出错
                //alert("表单提交出错，请稍候再试");
                //rebool = false;
            },
            success: function (result) {
                var ss = result.split("|");
                if (ss[0] == "1") {
                    //修改显示（数量/金额）
                    //修改显示（数量/金额）
                    var addnum = parseInt($(obj).val()) - parseInt($(obj).next().val());
                    editNumber(obj, addnum, price, type);
                } else {
                    $(obj).val($(obj).next().val());
                }
            }

        });
    }
}
function AddNum(obj, typeid, foodId, price, date) {
    var prevobj = $(obj).prev().find("[id$='txtNumber']");
    var num = parseInt($(prevobj).val()) + parseInt(1);
    if (num.length > 2) alert('请输入长度小于100的数量！');
    jQuery.ajax({
        url: "OrderIndex.aspx?action=AddNum&" + Math.random(),   // 提交的页面
        type: "POST",                   // 设置请求类型为"POST"，默认为"GET"
        data: { "id": foodId, "type": typeid, "num": 1, "date": date },
        beforeSend: function ()          // 设置表单提交前方法
        {
            //alert("准备提交数据");


        },
        error: function (request) {      // 设置表单提交出错
            //alert("表单提交出错，请稍候再试");
            //rebool = false;
        },
        success: function (result) {
            var ss = result.split("|");
            if (ss[0] == "1") {
                //修改显示（数量/金额）
        
                $(prevobj).val(parseInt($(prevobj).val()) + parseInt(1));
                editNumber(prevobj, 1, price, typeid);

            }
        }

    });
}
function ReduceNum(obj, typeid, foodId, price, date) {
    var nextvobj = $(obj).next().find("[id$='txtNumber']");
    var newcount = parseInt($(nextvobj).val()) + parseInt(-1);
    if (parseInt(newcount) > 0) {
        jQuery.ajax({
            url: "OrderIndex.aspx?action=ReduceNum&" + Math.random(),   // 提交的页面
            type: "POST",                   // 设置请求类型为"POST"，默认为"GET"
            data: { "id": foodId, "type": typeid, "num": -1, "date": date },
            beforeSend: function ()          // 设置表单提交前方法
            {
                //alert("准备提交数据");


            },
            error: function (request) {      // 设置表单提交出错
                //alert("表单提交出错，请稍候再试");
                //rebool = false;
            },
            success: function (result) {
                var ss = result.split("|");
                if (ss[0] == "1") {
                    //修改显示（数量/金额）
                    $(nextvobj).val(parseInt($(nextvobj).val()) + parseInt(-1));
                    editNumber(nextvobj, '-1', price, typeid);

                }
            }

        });
    }
}
function addFood(foodid, date) {
    if (foodid != null && foodid != "") {
        jQuery.ajax({
            url: "OrderIndex.aspx?action=AddCart&" + Math.random(),   // 提交的页面
            type: "POST",                   // 设置请求类型为"POST"，默认为"GET"
            data: { "id": foodid, "date": date },
            beforeSend: function ()          // 设置表单提交前方法
            {
                //alert("准备提交数据");


            },
            error: function (request) {      // 设置表单提交出错
                //alert("表单提交出错，请稍候再试");
                //rebool = false;
            },
            success: function (result) {
                var ss = result.split("|");
                if (ss[0] == "1") {
                    window.location.reload();
                }
            }

        });
    }
}
function editNumber(obj, addnum, price, type) {
    var newcount = parseInt($("[id$='Totalcount']").text()) + parseInt(addnum);
    if (newcount > 0) {
        $("[id$='Totalcount']").text(newcount);
    } else {
        $(obj).val($(obj).next().val());
    }
    var newtotalmoney = parseFloat(price) * parseFloat(addnum);
    var newmoney = parseFloat(newtotalmoney) + parseFloat($("[id$='totalmoney']").html());
    if (newmoney > 0) {
        $("[id$='totalmoney']").text(newmoney);
    } else {
        $(obj).val($(obj).next().val());
    }
    $(obj).next().val($(obj).val());
    if (type == "1") {
        $("[id$='Morningcount']").text(parseInt($("[id$='Morningcount']").text()) + parseInt(addnum));
        var addmmoney = parseFloat(price) * parseFloat(addnum);
        var newmmoney = parseFloat(addmmoney) + parseFloat($("[id$='Morningmoney']").text());
        if (newmmoney > 0) {
            $("[id$='Morningmoney']").text(newmmoney);
            var nowsubtotal = parseFloat($(obj).val()) * parseFloat(price);
            $(obj).parent().parent().next().html("￥" + nowsubtotal);
        } else {
            $(obj).val($(obj).next().val());
        }
    } else if (type == "2") {
        $("[id$='Lunchcount']").text(parseInt($("[id$='Lunchcount']").text()) + parseInt(addnum));
        var addlmoney = parseFloat(price) * parseFloat(addnum);
        var newlmoney = parseFloat(addlmoney) + parseFloat($("[id$='Lunchmoney']").text());
        if (newlmoney > 0) {
            $("[id$='Lunchmoney']").text(newlmoney);
            var nowsubtotal = parseFloat($(obj).val()) * parseFloat(price);
            $(obj).parent().parent().next().html("￥" + nowsubtotal);
        } else {
            $(obj).val($(obj).next().val());
        }
    } else if (type == "3") {
        $("[id$='Dinnercount']").text(parseInt($("[id$='Dinnercount']").text()) + parseInt(addnum));
        var addDmoney = parseFloat(price) * parseFloat(addnum);
        var newDmoney = parseFloat(addDmoney) + parseFloat($("[id$='Dinnermoney']").text());
        if (newDmoney > 0) {
            $("[id$='Dinnermoney']").text(newDmoney);
            var nowsubtotal = parseFloat($(obj).val()) * parseFloat(price);
            $(obj).parent().parent().next().html("￥" + nowsubtotal);
        } else {
            $(obj).val($(obj).next().val());
        }
    }
}