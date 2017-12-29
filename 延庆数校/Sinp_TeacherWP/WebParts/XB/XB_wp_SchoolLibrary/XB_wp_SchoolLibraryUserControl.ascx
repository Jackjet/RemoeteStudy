<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="XB_wp_SchoolLibraryUserControl.ascx.cs" Inherits="Sinp_TeacherWP.WebParts.XB.XB_wp_SchoolLibrary.XB_wp_SchoolLibraryUserControl" %>
<link href="../../../../_layouts/15/Style/common.css" rel="stylesheet" />
<link href="../../../../_layouts/15/Style/layout.css" rel="stylesheet" />
<link href="../../../../_layouts/15/Style/animate.css" rel="stylesheet" />
<link href="../../../../_layouts/15/Style/iconfont.css" rel="stylesheet" />
<link href="../../../../_layouts/15/ajax/type.css" rel="stylesheet" />
<link href="../../../../_layouts/15/ajax/Pager.css" rel="stylesheet" />
<link href="../../../../_layouts/15/Style/tablelist.css" rel="stylesheet" />
<script src="../../../../_layouts/15/Script/jquery-1.8.0.js"></script>
<script src="../../../../_layouts/15/Script/xb/tz_slider.js"></script>
<script src="../../../../_layouts/15/Script/xb/uploadFile.js"></script>
<script src="../../../../_layouts/15/Script/JSBase.js"></script>
<script src="../../../../_layouts/15/Script/xb/popwin.js"></script>
<script src="../../../../_layouts/15/ajax/Pager.js"></script>
<script src="../../../../_layouts/15/ajax/json.js"></script>
<script src="../../../../_layouts/15/ajax/VocationalEducation.js"></script>
<script src="../../../../_layouts/15/ajax/FormatUtil.js"></script>
<style type="text/css">
    .hid {
        display: none;
    }

    .fileup {
        border-radius: 3px;
        background-color: #0DA6EC;
        color: white;
        border: 0px;
    }

    .Operations a.shenhe {
        display: block;
        padding: 0px 4px;
        background: #0da6ec;
        border-radius: 3px;
        color: #fff;
        float: left;
        margin: 0 4px;
        height: 20px;
        line-height: 20px;
    }

    .selected {
        display: inline-block;
        background: #0da6ec;
        color: #fff;
    }

        .selected a {
            color: #fff;
        }

    .star {
        padding: 0;
        margin: 0;
        list-style: none;
        float: left;
    }

        .star li {
            float: left;
            height: 20px;
            width: 20px;
            margin-right: 4px;
        }

            .star li.on {
                color: #f60;
            }

            .star li.off {
                color: #ccc;
            }
</style>
<script type="text/javascript">

    var pIndex = 1;
    var pCount = 0;
    var pSize = 10;
    function BindData(data) {
        $("#tab tbody").empty();
        var FirstUrl = window.location.href;
        FirstUrl = FirstUrl.substring(0, FirstUrl.indexOf("SitePages"))
        var Hadmin = encodeURIComponent($("[id$='HAdmin']").val());
        $.ajax({
            type: "Post",
            url: FirstUrl + "/_layouts/15/hander/SchoolLibrary.aspx",
            data: { CMD: "IsAdmin", Hadmin: Hadmin },
            dataType: "text",
            success: function (returnVal) {
                if (returnVal == "True") {
                    $($.parseJSON(data)).each(function () {
                        var operater = "";
                        if ($("[id$='HStatus']").val() == "-1") {
                            operater = "<a href=\"#\" class=\"shenhe\" onclick=\"Check('" + this.ID + "','1')\";>通过</a><a href=\"#\" class=\"shenhe\" onclick=\"showshenhe('" + this.ID + "')\";>拒绝</a>";
                            document.getElementById("Operations").style.display = "block";
                        }
                        else { document.getElementById("Operations").style.display = "none"; }
                        var star = "";
                        if (this.EvalNum > 0) {
                            star = "<ul class='star'>";
                            for (var i = 0; i < 5; i++) {
                                if (i < this.EvalNum) {
                                    star += "<li class='iconfont on'>&#xe620;</li>";
                                }
                                else {
                                    star += "<li class='iconfont off'>&#xe620;</li>";
                                }
                            }
                        }
                        else {
                            star = "<ul class='star'><li class='iconfont off' onmousemove='over(this)' onclick=\"star('1','" + this.ID +
                                       "')\">&#xe620;</li><li class='iconfont off' onmousemove='over(this)' onclick=\"star('2','" +
                                       this.ID + "')\">&#xe620;</li><li class='iconfont off' onmousemove='over(this)' onclick=\"star('3','" +
                                       this.ID + "')\">&#xe620;</li><li class='iconfont off' onmousemove='over(this)' onclick=\"star('4','" +
                                       this.ID + "')\">&#xe620;</li><li class='iconfont off' onmousemove='over(this)' onclick=\"star('5','" + this.ID + "')\">&#xe620;</li></ul>";
                        }
                        var tr = "<tr class='J-item Single'><td class='Check'><input type='checkbox' value='" + this.ID + "' class='Check_box' name='Check_box'  id='subcheck' onclick='setSelectAll()'/></td><td class='name'><i class='file' style='float: left;cursor:pointer' onclick=\"folder('" + this.Name
                            + "','" + this.Url + "','" + this.Type + "','" + this.ID + "')\"><img src='" + this.Image + "'></i><span style=\"cursor:pointer\" onclick=\"folder('" + this.Name
                            + "','" + this.Url + "','" + this.Type + "','" + this.ID + "')\">" + this.Title +
                            "</span><div class=\"Operation\" style=\"display:none;\">" +
                            "<span class=\"Download\" ><a href=\"#\" onclick=\"Down('" + this.ID + "')\"><i class=\"iconfont\">&#xe616;</i></a></span><span class=\"more\"><a href=\"#\" class=\"more_m\"><i class=\"iconfont\">&#xe611;</i></a>" +
                            "<ul class=\"M_con\" style=\"display:none;\"><li><a href=\"#\" onclick=\"Move('" + this.ID + "','move')\">移动到</a></li><li><a href=\"#\"  onclick=\"Move('" + this.ID +
                            "','copy')\">复制到</a></li><li><a href=\"#\" onclick=\"cl(this,'" + this.ID + "','" + this.Title + "')\">重命名</a></li><li><a href=\"#\" onclick=\"DelFile('" + this.ID + "')\">删除</a></li>"
                            + "</ul></span></div>"
                            + "</td><td class='File_size'>" + this.Size + "</td><td class='Change_date'>" + this.Created + "</td><td>" + this.Eval + "</td><td>" + star + "</td><td class='Operations'>" + operater + "</td></tr>";
                        $("#tab tbody").append(tr);
                    })
                }
                else {
                    $($.parseJSON(data)).each(function () {
                        var operater = "";
                        if ($("[id$='HStatus']").val() == "-1") {
                            operater = "<a href=\"#\" class=\"shenhe\" onclick=\"Check('" + this.ID + "','1')\";>通过</a><a href=\"#\" class=\"shenhe\" onclick=\"showshenhe('" + this.ID + "')\";>拒绝</a>";
                        }
                        var star = "";
                        if (this.EvalNum > 0) {
                            star = "<ul class='star'>";
                            for (var i = 0; i < 5; i++) {
                                if (i < this.EvalNum) {
                                    star += "<li class='iconfont on'>&#xe620;</li>";
                                }
                                else {
                                    star += "<li class='iconfont off'>&#xe620;</li>";
                                }
                            }
                        }
                        else {
                            star = "<ul class='star'><li class='iconfont off' onmousemove='over(this)' onclick=\"star('1','" + this.ID +
                                       "')\">&#xe620;</li><li class='iconfont off' onmousemove='over(this)' onclick=\"star('2','" +
                                       this.ID + "')\">&#xe620;</li><li class='iconfont off' onmousemove='over(this)' onclick=\"star('3','" +
                                       this.ID + "')\">&#xe620;</li><li class='iconfont off' onmousemove='over(this)' onclick=\"star('4','" +
                                       this.ID + "')\">&#xe620;</li><li class='iconfont off' onmousemove='over(this)' onclick=\"star('5','" + this.ID + "')\">&#xe620;</li></ul>";
                        }
                        var tr = "<tr class='J-item Single'><td class='Check'><input type='checkbox' value='" + this.ID + "' class='Check_box' name='Check_box' /></td><td class='name'><i class='file' style='float: left; cursor:pointer' onclick=\"folder('" + this.Name
                            + "','" + this.Url + "','" + this.Type + "','" + this.ID + "')\"><img src='" + this.Image + "'></i><span onclick=\"folder('" + this.Name
                            + "','" + this.Url + "','" + this.Type + "','" + this.ID + "')\">" + this.Title +
                            "</span></td><td class='File_size'>" + this.Size + "</td><td class='Change_date'>" + this.Created + "</td><td>" + this.Eval + "</td><td>" + star + "</td><td class='Operations'>" + operater + "</td></tr>";
                        $("#tab tbody").append(tr);
                    })
                }
                $(".J-item").hover(function () {
                    $(this).find(".Operation").show().end().siblings().find('.M_con').hide();
                }, function () {
                    $(this).find(".Operation").hide();
                });
                //分享下载
                $('.more_m').click(function () {
                    $(this).siblings('.M_con').show();
                })
            },
            error: function (errMsg) {
                alert('数据加载失败！');
            }
        });

        // });

    }
    function ShowData(index) {
        var Hadmin = encodeURIComponent($("[id$='HAdmin']").val());

        var postData = {
            CMD: "FullTab",
            PageSize: pSize,
            PageIndex: index,
            Hadmin: Hadmin,
            Name: $("#txtName").val(),
            TypeSel: $("#Hidden2").val(),
            TimeSel: $("#Hidden3").val(),
            FoldUrl: $("[id$='HFoldUrl']").val(),
            SeleStatu: $("[id$='HStatus']").val(),
            hSubject: $("[id$='hSubject']").val(),
            hContent: $("[id$='hContent']").val()
        };
        var FirstUrl = window.location.href;
        FirstUrl = FirstUrl.substring(0, FirstUrl.indexOf("SitePages"))

        $.ajax({
            type: "Post",
            url: FirstUrl + "/_layouts/15/hander/SchoolLibrary.aspx",
            data: postData,
            dataType: "text",
            success: function (returnVal) {
                returnVal = $.parseJSON(returnVal);
                if (returnVal != null) {
                    SetPageCount(Math.ceil(returnVal.PageCount / pSize));
                    BindData(returnVal.Data);
                }
            },
            error: function (errMsg) {
                alert('数据加载失败！');
            }
        });
    }
    //资源评价
    function star(eval, id) {
        var FirstUrl = window.location.href;
        FirstUrl = FirstUrl.substring(0, FirstUrl.indexOf("SitePages"))

        $.ajax({
            type: "Post",
            url: FirstUrl + "/_layouts/15/hander/SchoolLibrary.aspx",
            data: { CMD: "Evalue", "eval": eval, "EvalID": id },
            dataType: "text",
            success: function (returnVal) {
                if (returnVal == "1") {
                    alert('操作成功！');
                    ShowData(1);
                }
            },
            error: function (errMsg) {
                alert('操作失败！');
            }
        });
    }
    function over(em) {
        $(em).parent().find("li").removeClass("off");
        $(em).parent().find("li").addClass("on");
        $(em).addClass("on");
        $(em).nextAll().removeClass("on").addClass("off");
    }
    $(function () {
        var FirstUrl = window.location.href;
        FirstUrl = FirstUrl.substring(0, FirstUrl.indexOf("SitePages"))

        GetFileType();
        IsAdmin(FirstUrl);
        BindMajor(FirstUrl);

        $("#checkAll").click(function () {
            var flag = $(this).attr("checked") == undefined ? false : true;
            $("input[name='Check_box']:checkbox").each(function () {
                $(this).attr("checked", flag);
            })
        });
        $(document).bind("keydown", function (e) {
            e = window.event || e;
            if (e.keyCode == 13) {
                Bind();
            }
        });
    });
    //子复选框的事件  
    function setSelectAll() {
        //当没有选中某个子复选框时，SelectAll取消选中  
        if (!$("#subcheck").checked) {
            $("#checkAll").attr("checked", false);
        }
        var chsub = $("input[type='checkbox'][id='subcheck']").length; //获取subcheck的个数  
        var checkedsub = $("input[type='checkbox'][id='subcheck']:checked").length; //获取选中的subcheck的个数  
        if (checkedsub > 0) {
            $("#checkAll").attr("checked", true);
        }
    }
    function showshenhe(id) {
        showDiv('Messagediv', 'Message_head');
        $("#hdS").val(id);
    }

    function Bind() {
        var FirstUrl = window.location.href;
        FirstUrl = FirstUrl.substring(0, FirstUrl.indexOf("SitePages"))
        var Hadmin = encodeURIComponent($("[id$='HAdmin']").val());


        $.ajax({
            type: "Post",
            url: FirstUrl + "/_layouts/15/hander/SchoolLibrary.aspx",
            data: { CMD: "FullTab", PageSize: pSize, PageIndex: pIndex, Hadmin: Hadmin, hSubject: $("[id$='hSubject']").val(), hContent: $("[id$='hContent']").val(), SeleStatu: $("[id$='HStatus']").val() },
            dataType: "text",
            success: function (returnVal) {
                returnVal = $.parseJSON(returnVal);
                if (returnVal != null) {
                    pCount = returnVal.PageCount;
                    LoadPageControl(ShowData, "pageDiv", pIndex, pSize, Math.ceil(pCount / pSize), pCount);
                }
            },
            error: function (errMsg) {
                alert('数据加载失败！');
            }
        });

    }
    function btnQuery() {
        ShowData(1);
    }
    //审核成功
    function Check(id, status) {
        var FirstUrl = window.location.href;
        FirstUrl = FirstUrl.substring(0, FirstUrl.indexOf("SitePages"))

        $.ajax({
            type: "Post",
            url: FirstUrl + "/_layouts/15/hander/SchoolLibrary.aspx",
            data: { CMD: "Check", "CheckID": id, "Status": status },
            dataType: "text",
            success: function (returnVal) {
                if (returnVal == "1") {
                    alert('操作成功！');
                    ShowData(1);
                }
            },
            error: function (errMsg) {
                alert('操作失败！');
            }
        });
    }
    //审核失败
    function shenhe() {
        var id = $("#hdS").val();
        var FirstUrl = window.location.href;
        FirstUrl = FirstUrl.substring(0, FirstUrl.indexOf("SitePages"))


        $.ajax({
            type: "Post",
            url: FirstUrl + "/_layouts/15/hander/SchoolLibrary.aspx",
            data: { CMD: "Shenhe", ShenheID: id, Message: $("#Message").val() },
            dataType: "text",
            success: function (returnVal) {
                if (returnVal == "1") {
                    alert("操作成功");
                    ShowData(1);
                }
                else
                    alert("操作失败");
            },
            error: function (errMsg) {
                alert('操作失败！');
            }
        });
    }
    //管理员可以查看审核页面
    function IsAdmin(FirstUrl) {
        var Hadmin = encodeURIComponent($("[id$='HAdmin']").val());

        $.ajax({
            type: "Post",
            url: FirstUrl + "/_layouts/15/hander/SchoolLibrary.aspx",
            data: { CMD: "IsAdmin", Hadmin: Hadmin },
            dataType: "text",
            success: function (returnVal) {
                if (returnVal == "True") {

                    document.getElementById("daishenhe").style.display = "block";
                    document.getElementById("plcz").style.display = "block";
                    document.getElementById("xjwjj").style.display = "block";
                    document.getElementById("toptianjia").style.display = "inline";
                    //document.getElementById("zyxkxg").style.display = "block";

                }
            },
            error: function (errMsg) {
                alert('数据加载失败！');
            }
        });
    }
    //绑定文件类型
    function GetFileType() {
        var FirstUrl = window.location.href;
        FirstUrl = FirstUrl.substring(0, FirstUrl.indexOf("SitePages"))
        $("#F_type").empty();
        $("#F_type").append("<dt>文件类型：</dt><dd class=\"selected\"><a href=\"#\" onclick=\"serchType('',this)\">不限</a></dd>");

        $.ajax({
            type: "Post",
            url: FirstUrl + "/_layouts/15/hander/SchoolLibrary.aspx",
            data: { CMD: "FileType" },
            dataType: "text",
            success: function (returnVal) {
                returnVal = $.parseJSON(returnVal);
                $(returnVal).each(function () {
                    var result = "<dd><a href='#' onclick=\"serchType('" + this.Attr + "',this)\">" + this.Title + "</a></dd>";
                    $("#F_type").append(result);
                });
                $("#F_type").append("<dd><i class=\"iconfont\" style=\"cursor:pointer;\" onclick=\"EditType()\")>&#xe60a;</i></dd>");
            },
            error: function (errMsg) {
            }
        });
    }
    function EditType() {
        var FirstUrl = window.location.href;
        FirstUrl = FirstUrl.substring(0, FirstUrl.indexOf("SitePages"))

        popWin.showWin("600", "467", "修改文件类型", FirstUrl + "/SitePages/DriveType.aspx", 'auto');
    }
    //新增文件夹
    function AddFold(em) {
        var Hadmin = encodeURIComponent($("[id$='HAdmin']").val());

        var FirstUrl = window.location.href;
        FirstUrl = FirstUrl.substring(0, FirstUrl.indexOf("SitePages"));

        var FileName = $("#txt" + em + "").val();
        $.ajax({
            type: "Post",
            url: FirstUrl + "/_layouts/15/hander/SchoolLibrary.aspx",

            data: { CMD: "AddFolder", "FileName": FileName, FoldUrl: $("[id$='HFoldUrl']").val(), Hadmin: Hadmin, SubJectID: $("[id$='hSubject']").val(), CatagoryID: $("[id$='hContent']").val(), TypeName: $("[id$='HStatus']").val() },
            dataType: "text",
            success: function (returnVal) {
                if (returnVal == "1") {
                    alert("添加成功");
                    ShowData(1);
                }
                else
                    alert("添加失败");
            },
            error: function (errMsg) {
                alert('数据加载失败！');
            }
        });
    }
    //追加一行（新建文件夹）

    function appendAfterRow(tableID, RowIndex) {
        var txt1 = document.getElementById("txt1");
        if (txt1 != undefined) {
            txt1.onfocus();
        }
        else {
            //FUNCTION: 向指定行后面增加一行,列数和第一行的列数一样
            var o = document.getElementById(tableID);
            var refRow = RowIndex;
            // var cells = o.rows[0].cells.length;
            if (refRow == "") refRow = getO("nRow").value;
            var v = "<img src='/_layouts/15/images/folder.gif?rev=23' style='float:left; margin-top:11px;'>" +
                "<input type='text' value='新建文件夹' style='float:left;line-height:10px;margin-top:9px' id=\"txt" + RowIndex +
                "\"/> <i class=\"iconfont tishi true_t\" style=\"margin: 2px; color: #87c352; float: left; height:24px; cursor:pointer;\" onclick=\"AddFold('" + RowIndex
                + "')\">&#xe65a;</i> <i class=\"iconfont tishi fault_t\" style=\"margin: 2px; color: #ff6d72; float: left; height:24px; cursor:pointer;\" onclick=\"DelRow()\">&#xe65c;</i>";
            var newRefRow = o.insertRow(refRow);
            //for (var i = 0; i < cells; i++) {
            newRefRow.insertCell(0).innerHTML = "";
            //if (i == RowIndex) {

            newRefRow.insertCell(1).innerHTML = v;
            //}
            newRefRow.insertCell(2).innerHTML = "--";
            var now = new Date();
            var nowStr = now.Format("yyyy-MM-dd");
            newRefRow.insertCell(3).innerHTML = nowStr;
            newRefRow.insertCell(4).innerHTML = "";
            newRefRow.insertCell(5).innerHTML = "";
            // else { newRefRow.insertCell(i).innerHTML = ""; }
            // }
        }
    }
    function getO(id) {
        if (typeof (id) == "string")
            return document.getElementById(id);
    }
    //删除一行（table）
    function DelRow() {
        $('table tr:eq(1)').remove();
    }
    //文件类型查询点击
    function serchType(attr, em) {
        $(em).parent().addClass("selected").siblings().removeClass("selected");
        $("#Hidden2").val(attr);
        ShowData(1);
        $(".File_type").hide();
    }
    //时间查询条件点击事件
    function serchTime(tim, em) {
        $("#Hidden3").val(tim);
        $(em).parent().addClass("selected").siblings().removeClass("selected");
        $(".File_type").hide();

        ShowData(1);
    }
    //新建文件夹
    function folder(name, url, type, id) {
        $(".navgition").html("全部文件");

        if (type == "文件夹") {
            var html = "<a onclick=\"folder2('','')\">全部文件<a/>";
            $("[id$='HFoldUrl']").val($("[id$='HFoldUrl']").val() + "/" + name);
            var allUrl = $("[id$='HFoldUrl']").val();
            var names = '';
            for (var i = 1; i < allUrl.split('/').length; i++) {
                names += "/" + allUrl.split('/')[i];
                if (names != "" && allUrl.split('/')[i].length > 0) {
                    $("[id$='HFoldUrl']").val(names);

                    html += "<a onclick=\"folder2('" + names + "','" + id + "')\">>" + allUrl.split('/')[i].substring(0, allUrl.split('/')[i].length - id.length) + "<a/>";
                }
            }
            $(".navgition").html(html);

            ShowData(1);
        }
        else {
            window.open(url);
        }
    }
    function folder2(name, id) {
        $(".navgition").html("");

        var html = "<a onclick=\"folder2('','')\">全部文件<a/>";
        $("[id$='HFoldUrl']").val(name);
        var allUrl = $("[id$='HFoldUrl']").val();
        var names = '';
        for (var i = 1; i < allUrl.split('/').length; i++) {
            names += "/" + allUrl.split('/')[i];
            if (names != "") {
                $("[id$='HFoldUrl']").val(names);
                html += "<a onclick=\"folder2('" + names + "','" + id + "')\">>" + allUrl.split('/')[i].substring(0, allUrl.split('/')[i].length - id.length) + "<a/>";
            }
        }
        $(".navgition").html(html);

        ShowData(1);
    }
    //格式化时间
    Date.prototype.Format = function (fmt) { //author: meizz 
        var o = {
            "M+": this.getMonth() + 1, //月份 
            "d+": this.getDate(), //日 
            "h+": this.getHours(), //小时 
            "m+": this.getMinutes(), //分 
            "s+": this.getSeconds(), //秒 
            "q+": Math.floor((this.getMonth() + 3) / 3), //季度 
            "S": this.getMilliseconds() //毫秒 
        };
        if (/(y+)/.test(fmt)) fmt = fmt.replace(RegExp.$1, (this.getFullYear() + "").substr(4 - RegExp.$1.length));
        for (var k in o)
            if (new RegExp("(" + k + ")").test(fmt)) fmt = fmt.replace(RegExp.$1, (RegExp.$1.length == 1) ? (o[k]) : (("00" + o[k]).substr(("" + o[k]).length)));
        return fmt;
    }
    //移动文件
    function FileMoveMore(url, id, type) {
        $("#maskTop").remove();
        $("#mask").remove();
        var Hadmin = encodeURIComponent($("[id$='HAdmin']").val());

        var FirstUrl = window.location.href;
        FirstUrl = FirstUrl.substring(0, FirstUrl.indexOf("SitePages"));
        var ids = "";
        if (id == "") {
            $("input[name='Check_box']:checkbox").each(function () {
                if ($(this).attr("checked") == "checked") {
                    ids += $(this).attr('value') + ",";
                }
            });
        }
        else {
            ids = id;
        }
        if (ids == "") {
            alert("请选择要移动的文件");
        }
        else {
            var hSubject = $("[id$='hSubject']").val();
            var hContent = $("[id$='hContent']").val();
            $.ajax({
                type: "Post",
                url: FirstUrl + "/_layouts/15/hander/SchoolLibrary.aspx",
                data: { CMD: "MoveMore", "MoveIDs": ids, "Url": url, "Type": type, hSubject: hSubject, hContent: hContent, Hadmin: Hadmin },
                dataType: "text",
                success: function (returnVal) {
                    if (returnVal == "1") {
                        alert("操作成功");
                        ShowData(1);
                    }
                    else { alert("文件已存在") }
                },
                error: function (errMsg) {
                    alert('操作失败！');
                }
            });
        }
    }
    function Move(id, type) {
        var FirstUrl = window.location.href;
        FirstUrl = FirstUrl.substring(0, FirstUrl.indexOf("SitePages"));
        if (type == "copy") {
            popWin.showWin('400', '300', '复制到', FirstUrl + '/SitePages/MySchoolLibraryTree.aspx?ID=' + id + '&Type=copy', 'no');
            window.location.href = window.location.href;

        }
        else {
            popWin.showWin('400', '300', '移动到', FirstUrl + '/SitePages/MySchoolLibraryTree.aspx?ID=' + id + '&Type=move', 'no');
            window.location.href = window.location.href;

        }
    }
    //批量删除
    function DelMore() {
        var FirstUrl = window.location.href;
        FirstUrl = FirstUrl.substring(0, FirstUrl.indexOf("SitePages"))

        var ids = "";
        $("input[name='Check_box']:checkbox").each(function () {
            if ($(this).attr("checked") == "checked") {
                ids += $(this).attr('value') + ",";
                //Del($(this).attr('value'));
            }
        });
        if (confirm("确认删除文件吗")) {
            $.ajax({
                type: "Post",
                url: FirstUrl + "/_layouts/15/hander/SchoolLibrary.aspx",
                data: { CMD: "DelMore", "DelIDs": ids },
                dataType: "text",
                success: function (returnVal) {
                    if (returnVal == "1") {
                        alert("删除成功");
                    }
                    ShowData(1);
                },
                error: function (errMsg) {
                    alert('删除失败！');
                }
            });
        }
    }
    //删除
    function DelFile(id) {
        var FirstUrl = window.location.href;
        FirstUrl = FirstUrl.substring(0, FirstUrl.indexOf("SitePages"))
        if (confirm("确认删除文件吗")) {
            $.ajax({
                type: "Post",
                url: FirstUrl + "/_layouts/15/hander/SchoolLibrary.aspx",
                data: { CMD: "Del", "DelID": id },
                dataType: "text",
                success: function (returnVal) {
                    if (returnVal == "1") {
                        alert("删除成功");
                    }
                    ShowData(1);
                },
                error: function (errMsg) {
                    alert('删除失败！');
                }
            });
        }
    }
    //取消修改文件名称
    function EditNameQ(em, name) {
        $(em).parents("td").children("span").html(name);
    }
    //修改文件名称
    function EditName(em, id, oldname) {
        var FirstUrl = window.location.href;
        FirstUrl = FirstUrl.substring(0, FirstUrl.indexOf("SitePages"))

        var name = $("#txt" + id).val();
        $.ajax({
            type: "Post",
            url: FirstUrl + "/_layouts/15/hander/SchoolLibrary.aspx",
            data: {
                CMD: "EditName", "NameID": id, "NewName": name
            },
            dataType: "text",
            success: function (returnVal) {
                if (returnVal == "1") {
                    $(em).parents("td").children("span").html(name);
                }
                else {

                    $(em).parents("td").children("span").html(oldname);
                }
            },
            error: function (errMsg) {
                $(em).parents("td").children("span").html(oldname);
            }
        });
    }
    function cl(em, id, Title) {

        var name = $(em).parents("td").children("span").html();
        if (name = Title) {
            var v = "<input type='text' value=\"" + name + "\" style='float:left;line-height:10px;margin-top:5px' id=\"txt" + id
                        + "\"/> <i class=\"iconfont tishi true_t\" style=\"margin: 2px; color: #87c352; \" onclick=\"EditName(this,'" + id
                        + "','" + name + "')\">&#xe65a;</i> <i class=\"iconfont tishi fault_t\" style=\"margin: 2px; color: #ff6d72; \" onclick=\"EditNameQ(this,'" + name
                        + "')\">&#xe65c;</i>";

            $(em).parents("td").children("span").html(v);
        }
    }
    //文档下载
    function Down(id) {
        var FirstUrl = window.location.href;
        FirstUrl = FirstUrl.substring(0, FirstUrl.indexOf("SitePages"));
        var ids = "";
        if (id == "") {
            $("input[name='Check_box']:checkbox").each(function () {
                if ($(this).attr("checked") == "checked") {
                    ids += $(this).attr('value') + ",";
                }
            });
        }
        else {
            ids = id;
        }
        if (ids == "") {
            alert("请选择要下载的文件");
        }
        else {
            $.ajax({
                type: "Post",
                url: FirstUrl + "/_layouts/15/hander/SchoolLibrary.aspx",
                data: { CMD: "Down", downID: ids },
                dataType: "text",
                success: function (url) {
                    if (url != "") {
                        window.open(url);
                    }
                    else {
                        alert('没有选择任何文件！');
                    }
                },
                error: function (errMsg) {
                    alert('下载失败！');
                }
            });
        }
    }
    function TabSel(SelType) {
        if (SelType == "0" || SelType == "-1") {
            document.getElementById("opertion").style.display = "none";
        }
        else {
            document.getElementById("opertion").style.display = "block";
        }
        // opertion
        $("[id$='HFoldUrl']").val("");
        $(".navgition").html("全部文件");

        $("#Hidden2").val("");//文件类型
        $("#Hidden3").val("");//上传时间
        $("[id$='HStatus']").val(SelType);//tab条件
        $(".F_time dd:first").addClass("selected").siblings().removeClass("selected");
        $(".F_type dd:first").addClass("selected").siblings().removeClass("selected");

        Bind();
    }
    $(window).resize(function () {
        var height = $("#Major").height();
        if (height > 40) {
            $("#Major").append("<span class=\"more\">展开</span>");
            //筛选条件经过展开
            $("#selectList").find(".more").toggle(function () {
                $(this).parent().parent().removeClass("dlHeight");
            }, function () {
                $(this).parent().parent().addClass("dlHeight");
            });
        }
        else {
            $("#Major").find(".more").remove();
        }
    });
    //绑定学科
    function BindMajor() {
        var FirstUrl = window.location.href;
        FirstUrl = FirstUrl.substring(0, FirstUrl.indexOf("SitePages"));
        var Hadmin = encodeURIComponent($("[id$='HAdmin']").val());
        var GradID = $("[id$='GradID']").val();


        var i = 0;
        $.ajax({
            type: "Post",
            url: FirstUrl + "/_layouts/15/hander/SchoolLibrary.aspx",
            data: { CMD: "Major", Hadmin: Hadmin, GradID: GradID },
            dataType: "text",
            success: function (returnVal) {
                returnVal = $.parseJSON(returnVal);
                $(returnVal).each(function () {
                    var result = "";
                    if (i == 0) {
                        result = "<li><a href='#'  class='selected' onclick=\"serchMajor('" + this.NJ + "',this)\">" + this.NJMC + "</a></li>";
                        serchMajor(this.NJ, "");
                        $("#hMajor").val(this.NJ);
                    }
                    else {
                        result = "<li><a href='#' onclick=\"serchMajor('" + this.NJ + "',this)\">" + this.NJMC + "</a></li>";
                    }
                    $("#Major").append(result);
                    i++;
                });
                var height = $("#Major").height();
                if (height > 40) {
                    $("#Major").append("<span class=\"more\">展开</span>");
                    //筛选条件经过展开
                    $("#selectList").find(".more").toggle(function () {
                        $(this).parent().parent().removeClass("dlHeight");
                    }, function () {
                        $(this).parent().parent().addClass("dlHeight");
                    });
                }

            },
            error: function (errMsg) {
            }
        });

    }
    //绑定专业
    function serchMajor(id, em) {
        $(".navgition").html("全部文件");
        $("#Major li a").removeClass("selected");
        $(em).toggleClass("selected");
        $("#hMajor").val(id);
        var GradID = $("[id$='GradID']").val();

        $("#SubJect").empty();
        var FirstUrl = window.location.href;
        FirstUrl = FirstUrl.substring(0, FirstUrl.indexOf("SitePages"))
        var j = 0;
        $.ajax({
            type: "Post",
            url: FirstUrl + "/_layouts/15/hander/SchoolLibrary.aspx",
            data: { CMD: "SubJect", "MajorID": id, GradID: GradID },
            dataType: "text",
            success: function (returnVal) {
                returnVal = $.parseJSON(returnVal);
                if (returnVal == null) {
                    $("[id$='hSubject']").val("")
                }
                $(returnVal).each(function () {
                    var result = "";
                    if (j == 0) {
                        result = "<li><a  class='selected' href='#' onclick=\"serchSubNev('" + this.ID + "',this)\">" + this.Title + "</a></li>";
                        serchSubNev(this.ID, "");
                        $("[id$='hSubject']").val(this.ID)
                        $("[id$='hContent']").val("");
                        $("#Hidden2").val("");
                        $("#Hidden3").val("");

                        Bind();
                    }
                    else {
                        result = "<li><a href='#' onclick=\"serchSubNev('" + this.ID + "',this)\">" + this.Title + "</a></li>";
                    }
                    $("#SubJect").append(result);

                    j++;
                });
            },
            error: function (errMsg) {
            }
        });
    }
    //绑定左侧导航
    function serchSubNev(id, em) {
        $(".navgition").html("全部文件");
        $(".navgition").html("全部文件");
        $("#SubJect li a").removeClass("selected");
        $(em).toggleClass("selected");

        $("[id$='hSubject']").val(id);//学科
        $("[id$='hContent']").val("");//
        $("#Hidden2").val("");//
        $("#Hidden3").val("");
        $("[id$='HStatus']").val("0");
        $(".resourses li:first").addClass("selected").siblings().removeClass("selected");
        $(".F_time dd:first").addClass("selected").siblings().removeClass("selected");
        $(".F_type dd:first").addClass("selected").siblings().removeClass("selected");
        var Hadmin = encodeURIComponent($("[id$='HAdmin']").val());
        $("#Tree").empty();
        var FirstUrl = window.location.href;
        FirstUrl = FirstUrl.substring(0, FirstUrl.indexOf("SitePages"))
        var div = "";
        $.ajax({
            type: "Post",
            url: FirstUrl + "/_layouts/15/hander/SchoolLibrary.aspx",
            data: { CMD: "Tree", "SubJectID": id, Hadmin: Hadmin },
            dataType: "text",
            success: function (returnVal) {
                $("#Tree").append(returnVal);

                initTree();
                Bind();
            },
            error: function (errMsg) {
            }
        });

    }
    //左侧导航点击绑定内容

    function navTopClick() {
        $(".navgition").html("全部文件");
        $("#quanbu").css("background", "#F6F6F6");
        $("#quanbu").children(".icon").eq(0).css("background", "#0da6ea");
        $("#quanbu").children(".icon").eq(0).css("border", "1px solid #0da6ea");
        $("[id$='hContent']").val("");

        Bind();

    }
    //左侧导航点击绑定内容
    function NavClick(id, em) {
        $(".navgition").html("全部文件");
        $("#quanbu").css("background", "#fff");
        $("#quanbu").children(".icon").eq(0).css("background", "#fff");
        $("#quanbu").children(".icon").eq(0).css("border", "1px solid #666");

        $(".select-box").find(".i-menu").css("background", "#fff");

        $(".select-box").find(".ici-menu").css("background", "#fff");

        $(".select-box").find(".icic-item").css("background", "#fff");
        $(".select-box").find(".icon").css("background", "#fff");
        $(".select-box").find(".icon").css("border", "1px solid #666");

        $(em).css("background", "#F6F6F6");
        $(em).children(".icon").eq(0).css("background", "#0da6ea");
        $(em).children(".icon").eq(0).css("border", "1px solid #0da6ea");

        $("[id$='hContent']").val(id);
        $("#Hidden2").val("");
        $("#Hidden3").val("");

        $(".F_time dd:first").addClass("selected").siblings().removeClass("selected");
        $(".F_type dd:first").addClass("selected").siblings().removeClass("selected");

        Bind();
    }
    //左侧导航点击、鼠标经过事件
    function initTree() {

        $(".i-menu").each(function () {
            var _this = $(this);
            $(this).hover(function () {
                _this.find(".btn-area").show();
            }, function () {
                _this.find(".btn-area").hide();
            });
            var i_con = $(this).next(".ici-con")[0];
            if (i_con != null) {
                i_con.style.display = "none";
            }
        });
        $(".ici-menu").each(function () {
            var _this = $(this);
            $(this).hover(function () {
                _this.find(".btn-area").show();
            }, function () {
                _this.find(".btn-area").hide();
            });
            var i_con = $(this).next(".ici-con")[0];
            if (i_con != null) {
                i_con.style.display = "none";
            }

        });
        $(".icic-item").each(function () {
            var _this = $(this);
            $(this).hover(function () {
                _this.find(".btn-area").show();
            }, function () {
                _this.find(".btn-area").hide();
            });
        });
        $(".i-menu").click(function () {
            $(".i-menu").next().hide();
            var dis = $(this).next().css("display");
            if (dis == "none") {
                $(this).next().show();
            }
            else {
                $(this).next().hide();
            }

        });
        $(".ici-menu").click(function () {
            $(".ici-menu").next().hide();
            var dis = $(this).next().css("display");
            if (dis == "none") {
                $(this).next().show();
            }
            else {
                $(this).next().hide();
            }
        });
    }

    function editLeftMenu(id, em, title) {
        var name = $(em).parent().prev().html();
        if (name = title) {

            var v = "<input type='text' value=\"" + name + "\" style='float:left;line-height:10px; width:100px; margin-top:8px;' id=\"Menu" + id
                + "\"/> <i class=\"iconfont tishi true_t\" style=\"margin: 2px; color: #87c352; \" onclick=\"EditMenuName(this,'" + id
                + "','" + name + "')\">&#xe65a;</i> <i class=\"iconfont tishi fault_t\" style=\"margin: 2px; color: #ff6d72; \" onclick=\"EditMenuNameQ(this,'" + name
                + "')\">&#xe65c;</i>";
            $(em).parent().prev().html(v);
        }
    }
    //修改目录名称
    function EditMenuName(em, id, oldname) {
        var FirstUrl = window.location.href;
        FirstUrl = FirstUrl.substring(0, FirstUrl.indexOf("SitePages"))

        var name = $("#Menu" + id).val();
        $.ajax({
            type: "Post",
            url: FirstUrl + "/_layouts/15/hander/SchoolLibrary.aspx",
            data: {
                CMD: "EditMenuName", "EditMenuID": id, "MenuNewName": name
            },
            dataType: "text",
            success: function (returnVal) {
                if (returnVal == "1") {
                    alert("修改成功");
                    $(em).parent().html(name);
                }
                else {
                    alert("修改失败");
                    $(em).parent().html(oldname);
                }
            },
            error: function (errMsg) {
                alert(errMsg);
                $(em).parent().html(oldname);
            }
        });
    }
    function EditMenuNameQ(em, name) {
        var name = $(em).parent().html(name);
    }
    //左侧导航相关方法
    function delLeftMenu(id) {
        var FirstUrl = window.location.href;
        FirstUrl = FirstUrl.substring(0, FirstUrl.indexOf("SitePages"))

        $.ajax({
            type: "Post",
            url: FirstUrl + "/_layouts/15/hander/SchoolLibrary.aspx",
            data: {
                CMD: "DelContent", "delMenuID": id, "DelMenuName": name
            },
            dataType: "text",
            success: function (returnVal) {
                if (returnVal == "0") {
                    alert("删除失败");
                }
                if (returnVal == "1") {
                    alert("删除成功");
                    serchSubNev($("[id$='hSubject']").val());
                }
                if (returnVal == "2") {
                    alert("目录不为空");
                }
                if (returnVal == "3") {
                    alert("请先删除子节点");
                }
            },
            error: function (errMsg) {
                alert(errMsg);
            }
        });
    }
    function addLeftMenu(id, em, divid) {
        var length = $('#Tree input').length
        //var divNode = document.getElementById("div0");
        if (length == 0) {
            var v = "<input type='text' value='' style='float:left;line-height:10px; width:100px; margin-top:8px;' id=\"Menu" + id
               + "\"/> <i class=\"iconfont tishi true_t\" style=\"margin: 2px; color: #87c352; \" onclick=\"AddMenu(this,'" + id
               + "','')\">&#xe65a;</i> <i class=\"iconfont tishi fault_t\" style=\"margin: 2px; color: #ff6d72; \" onclick=\"Del(this)\">&#xe65c;</i>";
            var html = "<div id='div0' class=\"item\"><div class=\"i-menu cf\"> <span class=\"fl icon\"></span><span class=\"tit fl\">" + v + "</span></div></div>";
            //$("#" + divid).parent().prepend(html);
            var em1 = $("#" + divid).find('.i-con').html();
            var em2 = $("#" + divid).find('.ici-con').html();
            if (em1 == undefined && em2 == undefined) {
                var html = "<div clas\"i-con\"><div id='div0' class=\"item\"><div class=\"i-menu cf\"> <span class=\"fl icon\"></span><span class=\"tit fl\">" + v + "</span></div></div></div>";
                $("#" + divid).append(html);
            }
            if (em1 != undefined) {
                var html = "<div id='div'" + id + " class=\"ic-item\"><div class=\"ici-menu cf\"> <span class=\"fl icon\"></span><span class=\"tit fl\">" + v + "</span></div></div>";
                $("#" + divid).find('.i-con').prepend(html)
            }
            if (em1 == undefined && em != undefined) {
                var html = "<div id='div'" + id + " class=\"icic-item\"><span class=\"fl icon\"></span><span class=\"tit fl\">" + v + "</span></div>";
                $("#" + divid).find('.ici-con').prepend(html)

            }
        }
        else {
            alert("有未完成操作");
            $('#Tree input').focus();
        }
    }
    function AddMenu(em, id) {
        var FirstUrl = window.location.href;
        FirstUrl = FirstUrl.substring(0, FirstUrl.indexOf("SitePages"));
        var hSubject = $("[id$='hSubject']").val();
        var FileName = $("#Menu" + id + "").val();
        $.ajax({
            type: "Post",
            url: FirstUrl + "/_layouts/15/hander/SchoolLibrary.aspx",

            data: { CMD: "AddMenu", "hSubject": hSubject, id: id, MenuName: FileName },
            dataType: "text",
            success: function (returnVal) {
                if (returnVal == "1") {
                    alert("添加成功");
                    $(em).parent().html(FileName);
                }
                else {
                    alert("名称重复");
                }
            },
            error: function (errMsg) {
                alert('添加失败！');
            }
        });
    }
    //绑定左侧导航所需方法
    function AddMenuTop(em) {
        var length = $('#Tree input').length

        var id = $("[id$='hSubject']").val();
        // var divNode = document.getElementById("div0");
        if (length == 0) {
            var v = "<input type='text' value='' style='float:left;line-height:10px; width:100px; margin-top:8px;' id=\"Menu" + id
               + "\"/> <i class=\"iconfont tishi true_t\" style=\"margin: 2px; color: #87c352; \" onclick=\"AddMenu(this," + id + ")\">&#xe65a;</i> <i class=\"iconfont tishi fault_t\" style=\"margin: 2px; color: #ff6d72; \" onclick=\"Del()\">&#xe65c;</i>";
            var html = "<div id='div0' class=\"item\"><div class=\"i-menu cf\"> <span class=\"fl icon\"></span><span class=\"tit fl\">" + v + "</span></div></div>";
            $("#Tree").prepend(html);
        }
        else {
            alert("有未完成操作");
            $('#Tree input').focus();
        }
    }
    function Del() {
        $("#div0").remove();
        ////获取当前节点对象
        //var divNode2 = document.getElementById("Tree");
        ////获取文本节点
        //var textNode = divNode2.childNodes[0];
        ////删除文本节点
        //divNode2.removeChild(textNode);
    }
    //function EditMajor() {
    //    var FirstUrl = window.location.href;
    //    FirstUrl = FirstUrl.substring(0, FirstUrl.indexOf("SitePages"))

    //    popWin.showWin("600", "500", "修改专业学科", FirstUrl + "/SitePages/EditMajor.aspx", "no");
    //}
    function shaixuan() {
        var dis = $(".File_type").css("display");
        if (dis == "none") {
            $(".File_type").show();
        }
        else {
            $(".File_type").hide();
        }
    }
    function uploader() {
        var FirstUrl = window.location.href;
        FirstUrl = FirstUrl.substring(0, FirstUrl.indexOf("SitePages"));
        var HFoldUrl = $("[id$='HFoldUrl']").val();
        var hSubject = $("[id$='hSubject']").val();
        var HStatus = $("[id$='HStatus']").val();
        var hContent = $("[id$='hContent']").val();
        var urlName = encodeURIComponent("校本资源库");

        popWin.showWin('600', '550', '文件上传', FirstUrl + '/SitePages/PND_wp_DriveUpload.aspx?UrlName=' + urlName + '&hSubject=' + hSubject + '&HStatus=' + HStatus + '&hContent=' + hContent + '&HFoldUrl=' + HFoldUrl, "no");
    }
</script>

<!--校本资源库-->

<div class="School_library">
    <input id="hMajor" type="hidden" />
    <input id="hSubject" type="hidden" runat="server" />
    <input id="hContent" type="hidden" runat="server" />
    <input id="Hidden2" type="hidden" />
    <input id="Hidden3" type="hidden" />
    <input id="HFoldUrl" type="hidden" runat="server" />
    <input id="HStatus" type="hidden" value="" runat="server" />
    <input id="HAdmin" type="hidden" value="" runat="server" />
    <input id="GradID" type="hidden" value="" runat="server" />


    <div class="Whole_display_area">
        <div class="S_conditions">
            <div class="Add_list">

                <span class="add" style="padding-right: 0px;"></span>
            </div>
            <div id="selectList" class="screenBox screenBackground">
                <dl class="listIndex dlHeight" attr="terminal_brand_s">
                    <dt>年级</dt>
                    <dd id="Major"></dd>
                </dl>
                <dl class="listIndex" attr="terminal_brand_s">
                    <dt>学科</dt>
                    <dd id="SubJect"></dd>
                </dl>
            </div>
        </div>
        <div class="clear"></div>
        <div class="Schoolcon_wrap">
            <div class="left_navcon fl">
                <h1>课程</h1>
                <h3 class="tit" style="background: #F6F6F6" id="quanbu" onclick="navTopClick()"><i class="icon" style="background: #0da6ea; border: 1px solid #0da6ea"></i>全部<i class="iconfont" style="cursor: pointer; display: none" id="toptianjia" onclick="AddMenuTop('')">&#xe602;</i></h3>
                <div class="select-box" id="Tree">
                </div>
            </div>
            <div class="right_dcon">
                <!--操作区域-->
                <div class="Operation_area">
                    <div class="left_choice fl" id="opertion" style="display: none">
                        <a href="#" class="add" onclick="uploader()"><i class="iconfont">&#xe615;</i>上传文件</a>
                        <a href="#" class="add" onclick="javascript: appendAfterRow('tab', '1');" id="xjwjj" style="display: none"><i class="iconfont">&#xe62c;</i>新建文件夹</a>
                        <div class="Batch_operation fl" id="plcz" style="display: none">
                            <a href="#" class="add"><i class="iconfont">&#xe656;</i>批量操作</a>
                            <ul class="B_con" style="display: none;">
                                <li><a href="#" onclick="Move('')">移动到</a></li>
                                <li><a href="#" onclick="Move()">复制到</a></li>
                                <li><a href="#" onclick="DelMore()">删除</a></li>
                                <li><a href="#" onclick="Down('')">下载</a></li>
                            </ul>

                        </div>
                        <a href="#" class="add" onclick="shaixuan()"><i class="iconfont">&#xe658;</i>筛选</a>
                    </div>
                    <div class="right_add fr">
                        <ul>
                            <li class="Sear">
                                <input type="text" placeholder=" 请输入关键字" class="search" name="search" id="txtName" /><i class="iconfont" id="btnQuery" onclick="btnQuery()">&#xe609;</i>
                            </li>
                        </ul>
                    </div>
                </div>
                <div class="File_type" style="display: none;">
                    <dl class="F_type" id="F_type">
                        <dt>文件类型：</dt>
                        <dd class="selected"><a href="#" onclick="serchType('',this)">不限</a></dd>
                    </dl>
                    <div class="clear"></div>
                    <dl class="F_time">
                        <dt>上传时间：</dt>
                        <dd class="selected"><a href="#" onclick="serchTime('',this)">不限</a></dd>
                        <dd><a href="#" onclick="serchTime('一周内',this)">一周内</a></dd>
                        <dd><a href="#" onclick="serchTime('一月内',this)">一月内</a></dd>
                        <dd><a href="#" onclick="serchTime('半年内',this)">半年内</a></dd>
                    </dl>
                </div>
                <!--展示区域-->
                <div class="Display_form">
                    <div class="Resources_tab">
                        <div class="Resources_tabheader">
                            <ul class="resourses">
                                <li id="all" class="selected"><a href="#" onclick="TabSel('0')"><span class="zong_tit"><span class="add_li">全部</span></span></a></li>
                                <li><a href="#" onclick="TabSel('1')"><span class="zong_tit"><span class="add_li">教案</span></span></a></li>
                                <li><a href="#" onclick="TabSel('2')"><span class="zong_tit"><span class="add_li">课件</span></span></a></li>
                                <li><a href="#" onclick="TabSel('3')"><span class="zong_tit"><span class="add_li">习题</span></span></a></li>
                                <li><a href="#" id="daishenhe" onclick="TabSel('-1')" style="display: none"><span class="zong_tit"><span class="add_li">审核</span></span></a></li>
                            </ul>
                        </div>
                        <div class="navgition" style="width: 100%">全部文件</div>

                        <div class="content">
                            <div class="tc">
                                <div class="Order_form">
                                    <div class="Food_order">
                                        <table class="O_form" id="tab">
                                            <thead>
                                                <tr class="O_trth">
                                                    <!--表头tr名称-->
                                                    <th class="Check">
                                                        <input type="checkbox" class="Check_box" id="checkAll" /></th>
                                                    <th class="name">文件名称 </th>
                                                    <th class="File_size">文件大小 </th>
                                                    <th class="Change_date">修改日期 </th>
                                                    <th class="Change_date">综合评分</th>
                                                    <th class="Change_date">评价 </th>

                                                    <th class="Operations" id="Operations" style="display: none">操作 </th>
                                                </tr>
                                            </thead>
                                            <tbody></tbody>

                                        </table>
                                        <div id="pageDiv" class="pageDiv">
                                        </div>
                                    </div>
                                </div>
                            </div>

                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

</div>


<div id="Messagediv" class="blk" style="display: none; position: absolute; z-index: 1000; background: #67a3d9;">
    <div class="heaMessaged">
        <div class="head-right"></div>
    </div>
    <div class="main" style="width: 600px; background-color: white; border: 1px solid #374760">
        <div style="height: 40px; background: #5493D7; line-height: 50px; font-size: 15px;">
            <span style="float: left; color: white; margin-left: 20px;">拒绝意见</span>
            <span style="float: right; margin-right: 20px;">
                <input type="button" value="X" onclick="closeDiv('Messagediv')" style="border-color: #5493D7; background-color: #5493D7; height: 20px; line-height: 20px;" />
            </span>
        </div>
        <div id="Message_head" style="z-index: 1001; position: absolute;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</div>
        <div style="margin: 20px; border: 1px solid #374760">
            <table style="width: 100%; margin: 10px;">
                <tr style="border-bottom: 0">
                    <td class="lftd">拒绝原因：<input id="hdS" type="hidden" /></td>
                    <td>
                        <input type="text" id="Message" class="search" />
                    <td colspan="2">
                        <input type="button" value="确定" onclick="shenhe()" class="b_add" />
                </tr>
            </table>

        </div>
    </div>
</div>
