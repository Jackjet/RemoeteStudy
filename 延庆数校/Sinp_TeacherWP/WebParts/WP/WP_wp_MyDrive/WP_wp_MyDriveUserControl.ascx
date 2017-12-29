<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WP_wp_MyDriveUserControl.ascx.cs" Inherits="Sinp_TeacherWP.WebParts.WP.WP_wp_MyDrive.WP_wp_MyDriveUserControl" %>
<link href="../../../../_layouts/15/Style/common.css" rel="stylesheet" />
<link href="../../../../_layouts/15/Style/layout.css" rel="stylesheet" />
<link href="../../../../_layouts/15/Style/animate.css" rel="stylesheet" />
<link href="../../../../_layouts/15/Style/tablelist.css" rel="stylesheet" />
<link href="../../../../_layouts/15/Style/iconfont.css" rel="stylesheet" />
<link href="../../../../_layouts/15/Style/tablelist.css" rel="stylesheet" />
<link href="../../../../_layouts/15/ajax/type.css" rel="stylesheet" />
<link href="../../../../_layouts/15/ajax/Pager.css" rel="stylesheet" />
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

    .selected {
        display: inline-block;
        background: #0da6ec;
    }

        .selected a {
            background: #0da6ec;
            color: #fff;
        }
</style>
<script type="text/javascript">
    var pIndex = 1;
    var pCount = 0;
    var pSize = 10;
    function BindData(data) {
        $("#tab tbody").empty();
        $($.parseJSON(data)).each(function () {
            var tr = "<tr class='J-item Single'><td class='Check'><input type='checkbox' value='" + this.ID + "' class='Check_box' name='Check_box'  id='subcheck' onclick='setSelectAll()'/></td><td class='name'><i class='file' style='float: left;cursor:pointer' onclick=\"folder('" + this.Name
                + "','" + this.Url + "','" + this.Type + "','" + this.ID + "')\"><img src='" + this.Image + "'></i><span  style=\"cursor:pointer\" onclick=\"folder('" + this.Name
                + "','" + this.Url + "','" + this.Type + "','" + this.ID + "')\">" + this.Title +
                "</span><div class=\"Operation\" style=\"display:none;\">" +
				"<span class=\"Download\" ><a href=\"#\" onclick=\"Down('" + this.ID + "')\"><i class=\"iconfont\">&#xe616;</i></a></span><span class=\"more\"><a href=\"#\" class=\"more_m\"><i class=\"iconfont\">&#xe611;</i></a>" +
				"<ul class=\"M_con\" style=\"display:none;\"><li><a href=\"#\" onclick=\"Move('" + this.ID + "','move')\">移动到</a></li><li><a href=\"#\"  onclick=\"Move('" + this.ID +
                "','copy')\">复制到</a></li><li><a href=\"#\" onclick=\"cl(this,'" + this.ID + "','" + this.Title + "')\">重命名</a></li><li><a href=\"#\" onclick=\"Del('" + this.ID + "')\">删除</a></li>"
				+ "</ul></span></div>"
                + "</td><td class='File_size'>" + this.Size + "</td><td class='Change_date'>" + this.Created + "</td></tr>";
            $("#tab tbody").append(tr);
        });
        $(".J-item").hover(function () {
            $(this).find(".Operation").show().end().siblings().find('.M_con').hide();
        }, function () {
            $(this).find(".Operation").hide();
        });
        //分享下载
        $('.more_m').click(function () {
            $(this).siblings('.M_con').show();
        })
    }
    function ShowData(index) {
        //var postData = { CMD: "FullTab", PageSize: pSize, PageIndex: index };
        //if ($("#txtName").val().trim() != "") {
        var postData = { CMD: "FullTab", PageSize: pSize, PageIndex: index, Name: $("#txtName").val(), TypeSel: $("#Hidden2").val(), TimeSel: $("#Hidden3").val(), FoldUrl: $("[id$='HFoldUrl']").val() };
        //}
        var FirstUrl = window.location.href;
        FirstUrl = FirstUrl.substring(0, FirstUrl.indexOf("SitePages"))
        $.ajax({
            type: "Post",
            url: FirstUrl + "/_layouts/15/hander/PersonDrive.aspx",
            data: postData,
            dataType: "text",
            success: function (returnVal) {
                returnVal = $.parseJSON(returnVal);
                SetPageCount(Math.ceil(returnVal.PageCount / pSize));
                BindData(returnVal.Data);
                //pCount = returnVal.PageCount;
                //LoadPageControl(ShowData, "pageDiv", pIndex, pSize, Math.ceil(pCount / pSize));
            },
            error: function (errMsg) {
                alert('数据加载失败！');
            }
        });
    }
    $(function () {
        Bind();
        //ShowData(1);

        GetFileType();
        $("#checkAll").click(function () {
            var flag = $(this).attr("checked") == undefined ? false : true;
            $("input[name='Check_box']:checkbox").each(function () {
                $(this).attr("checked", flag);
            })
        });
        $(document).bind("keydown", function (e) {
            e = window.event || e;
            if (e.keyCode == 13) {
                ShowData(1);
            }
        });
    });
    function btnQuery() {
        ShowData(1);
    }
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
    function Bind() {
        var FirstUrl = window.location.href;
        FirstUrl = FirstUrl.substring(0, FirstUrl.indexOf("SitePages"))

        $.ajax({
            type: "Post",
            url: FirstUrl + "/_layouts/15/hander/PersonDrive.aspx",
            data: { CMD: "FullTab", PageSize: pSize, PageIndex: pIndex, Name: $("#txtName").val(), TypeSel: $("#Hidden2").val(), TimeSel: $("#Hidden3").val(), FoldUrl: $("[id$='HFoldUrl']").val() },
            dataType: "text",
            success: function (returnVal) {
                returnVal = $.parseJSON(returnVal);
                //BindData(returnVal.Data);
                pCount = returnVal.PageCount;
                LoadPageControl(ShowData, "pageDiv", pIndex, pSize, Math.ceil(pCount / pSize), pCount);
            },
            error: function (errMsg) {
                alert('数据加载失败！');
            }
        });
    }


    //绑定文件类型
    function GetFileType() {
        //$("#F_type").empty();
        var FirstUrl = window.location.href;
        FirstUrl = FirstUrl.substring(0, FirstUrl.indexOf("SitePages"))
        $("#F_type").empty();
        $("#F_type").append("<dt>文件类型：</dt><dd class=\"selected\"><a href=\"#\" onclick=\"serchType('',this)\">不限</a></dd>");

        $.ajax({
            type: "Post",
            url: FirstUrl + "/_layouts/15/hander/PersonDrive.aspx",///BindFileType",
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
                alert('数据加载失败！');
            }
        });
    }
    function EditType() {
        var FirstUrl = window.location.href;
        FirstUrl = FirstUrl.substring(0, FirstUrl.indexOf("SitePages"))

        popWin.showWin("620", "500", "修改文件类型", FirstUrl + "/SitePages/DriveType.aspx", "auto");
    }
    //新增文件夹
    function AddFold(em) {
        var FirstUrl = window.location.href;
        FirstUrl = FirstUrl.substring(0, FirstUrl.indexOf("SitePages"))
        var FileName = $("#txt" + em + "").val();
        $.ajax({
            type: "Post",
            url: FirstUrl + "/_layouts/15/hander/PersonDrive.aspx",
            data: { CMD: "AddFolder", "FileName": FileName, FoldUrl: $("[id$='HFoldUrl']").val() },
            dataType: "text",
            success: function (returnVal) {
                Bind();
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
            var v = "<img src='/_layouts/15/images/folder.gif?rev=23' style='float:left; margin-left:10px; margin-top:11px;'>" +
                "<input type='text' value='新建文件夹' style='float:left;line-height:10px;margin-top:9px' id=\"txt" + RowIndex +
                "\"/> <i class=\"iconfont tishi true_t\" style=\"margin: 2px; color: #87c352; float: left; cursor:pointer;\" onclick=\"AddFold('" + RowIndex
                + "')\">&#xe65a;</i> <i class=\"iconfont tishi fault_t\" style=\"margin: 2px; color: #ff6d72; float: left; cursor:pointer;\" onclick=\"DelRow()\">&#xe65c;</i>";
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
    function serchType(value, em) {
        $("[id$='HFoldUrl']").val("");
        $(".navgition").html("全部文件");

        $(em).parent().addClass("selected").siblings().removeClass("selected");
        $("#Hidden2").val(value);
        Bind();

    }
    //时间查询条件点击事件
    function serchTime(value, em) {
        $("[id$='HFoldUrl']").val("");
        $(".navgition").html("全部文件");

        $(em).parent().addClass("selected").siblings().removeClass("selected");

        $("#Hidden3").val(value);
        Bind();
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
            Bind();
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

        Bind();
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

    function MoveMore(url, id, type) {
        $("#maskTop").remove();
        $("#mask").remove();

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
            $.ajax({
                type: "Post",
                url: FirstUrl + "/_layouts/15/hander/PersonDrive.aspx",
                data: { CMD: "MoveMore", "MoveIDs": ids, "Url": url, "Type": type },
                dataType: "text",
                success: function (returnVal) {
                    if (returnVal == "1") {
                        alert("操作成功")
                        ShowData(1);
                    }
                    else {
                        alert("文件已存在")
                    }
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
            popWin.showWin('400', '300', '复制到', FirstUrl + '/SitePages/MyDriveTree.aspx?ID=' + id + '&Type=copy', "no");
            window.location.href = window.location.href;

        }
        else {
            popWin.showWin('400', '300', '移动到', FirstUrl + '/SitePages/MyDriveTree.aspx?ID=' + id + '&Type=move', "no");
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
                url: FirstUrl + "/_layouts/15/hander/PersonDrive.aspx",
                data: { CMD: "DelMore", "DelIDs": ids },
                dataType: "text",
                success: function (returnVal) {
                    if (returnVal == "1") {
                        alert("删除成功");
                    }
                    Bind();
                },
                error: function (errMsg) {
                    alert('删除失败！');
                }
            });
        }
    }
    //删除
    function Del(id) {
        var FirstUrl = window.location.href;
        FirstUrl = FirstUrl.substring(0, FirstUrl.indexOf("SitePages"))
        if (confirm("确认删除文件吗")) {
            $.ajax({
                type: "Post",
                url: FirstUrl + "/_layouts/15/hander/PersonDrive.aspx",
                data: { CMD: "Del", "DelID": id },
                dataType: "text",
                success: function (returnVal) {
                    if (returnVal == "1") {
                        alert("删除成功");
                    }
                    Bind();
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
            url: FirstUrl + "/_layouts/15/hander/PersonDrive.aspx",
            data: {
                CMD: "EditName", "NameID": id, "NewName": name
            },
            dataType: "text",
            success: function (returnVal) {
                if (returnVal == "1") {
                    //$(em).parents("td").children("span").html(name);
                    Bind();
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
    function cl(em, id, title) {

        var name = $(em).parents("td").children("span").html();
        if (name = title) {
            var v = "<input type='text' value=\"" + name + "\" style='float:left;line-height:10px;margin-top:5px' id=\"txt" + id
                        + "\"/> <i class=\"iconfont tishi true_t\" style=\"margin: 2px; color: #87c352; \" onclick=\"EditName(this,'" + id
                        + "','" + name + "')\">&#xe65a;</i> <i class=\"iconfont tishi fault_t\" style=\"margin: 2px; color: #ff6d72; \" onclick=\"EditNameQ(this,'" + name
                        + "')\">&#xe65c;</i>";

            $(em).parents("td").children("span").html(v);
            $(em).parents("td").children("span").removeAttr("onclick");
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
                url: FirstUrl + "/_layouts/15/hander/PersonDrive.aspx",
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
    function uploader() {
        var FirstUrl = window.location.href;
        FirstUrl = FirstUrl.substring(0, FirstUrl.indexOf("SitePages"));

        var HFoldUrl = $("[id$='HFoldUrl']").val();
        var urlName = encodeURIComponent("个人网盘");

        popWin.showWin('600', '550', '文件上传', FirstUrl + '/SitePages/PND_wp_DriveUpload.aspx?UrlName=' + urlName + '&hSubject=\'\'&HStatus=\'\'&hContent=\'\'&HFoldUrl=' + HFoldUrl, "no");
    }
    function openShare() {
        if (isNull()) {
            var FirstUrl = window.location.href;
            FirstUrl = FirstUrl.substring(0, FirstUrl.indexOf("SitePages"));

            popWin.showWin('400', '300', '文件分享', FirstUrl + '/SitePages/PND_wp_Shrar.aspx', "no");
        }
        else {
            alert("请选择要共享的文件");
        }
    }
    function isNull() {
        var ids = '';
        $("input[name='Check_box']:checkbox").each(function () {
            if ($(this).attr("checked") == "checked") {
                ids += $(this).attr('value') + ",";
            }
        });
        if (ids == "") {
            return false;
        }
        else
            return true;
    }
    function Share(type, subjectid, id) {
        var ids = '';
        $("input[name='Check_box']:checkbox").each(function () {
            if ($(this).attr("checked") == "checked") {
                ids += $(this).attr('value') + ",";
            }
        });
        $("#maskTop").fadeOut();
        $("#mask").fadeOut();

        var FirstUrl = window.location.href;
        FirstUrl = FirstUrl.substring(0, FirstUrl.indexOf("SitePages"));

        if (ids == "") {
            alert("请选择要共享的文件");
        }
        else {
            $.ajax({
                type: "Post",
                url: FirstUrl + "/_layouts/15/hander/PersonDrive.aspx",
                data: { CMD: "Share", "ShareId": ids, "subjectid": subjectid, "type": type, "CatagoryID": id },
                dataType: "text",
                success: function (returnVal) {
                    if (returnVal == "1") {
                        alert("分享成功");
                        ShowData(1);
                    }
                    else {
                        alert('分享失败！');
                    }
                },
                error: function (errMsg) {
                    alert('操作失败！');
                }
            });
        }
    }
</script>
<div class="Personal_skyDrive">
    <!--整个展示区域-->
    <div class="Whole_display_area">
        <!--操作区域-->
        <div class="Operation_area">
            <div class="left_choice fl">
                <a href="#" class="add" onclick="uploader()"><i class="iconfont">&#xe615;</i>上传文件</a>
                <a href="#" class="add" onclick="javascript: appendAfterRow('tab', '1')"><i class="iconfont">&#xe62c;</i>新建文件夹</a>
                <div class="Batch_operation fl">
                    <a href="#" class="add"><i class="iconfont">&#xe656;</i>批量操作</a>
                    <ul class="B_con" style="display: none;" id="B_con">
                        <li><a href="#" onclick="Move('')">移动到</a></li>
                        <li><a href="#" onclick="Move()">复制到</a></li>
                        <li><a href="#" onclick="DelMore()">删除</a></li>
                        <li><a href="#" onclick="Down('')">下载</a></li>
                    </ul>
                </div>
                <a href="#" class="add" onclick="openShare()"><i class="iconfont">&#xe602;</i>分享到校本资源库</a>
            </div>

            <div class="right_add fr">
                <ul>
                    <li class="Sear">
                        <input type="text" id="txtName" placeholder=" 请输入关键字" class="search" name="search" /><i class="iconfont" id="btnQuery" onclick="btnQuery()">&#xe609;</i>
                    </li>
                </ul>
            </div>
        </div>
        <div class="clear"></div>
        <!--展示区域-->
        <div class="Display_form">
            <!--文件类型-->
            <div class="File_type">
                <input id="Hidden2" type="hidden" />
                <input id="Hidden3" type="hidden" />
                <input id="HFoldUrl" type="hidden" runat="server" />

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
            <div class="navgition" style="width: 100%">全部文件</div>
            <table class="W_form" id="tab">
                <thead>
                    <tr class="trth">
                        <!--表头tr名称-->
                        <th class="Check">
                            <input type="checkbox" class="Check_box" id="checkAll" /></th>
                        <th class="name">文件名称 </th>
                        <th class="File_size">文件大小 </th>
                        <th class="Change_date">修改日期 </th>
                    </tr>
                </thead>
                <tbody></tbody>


            </table>
            <div id="pageDiv" class="pageDiv">
            </div>


        </div>
    </div>
</div>


