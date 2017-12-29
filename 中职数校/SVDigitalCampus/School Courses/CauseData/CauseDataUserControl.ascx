<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CauseDataUserControl.ascx.cs" Inherits="SVDigitalCampus.School_Courses.CauseData.CauseDataUserControl" %>
<link href="../../../../_layouts/15/SVDigitalCampus/CSS/common.css" rel="stylesheet" />
<link href="../../../../_layouts/15/SVDigitalCampus/CSS/style.css" rel="stylesheet" />
<link href="../../../../_layouts/15/SVDigitalCampus/CSS/iconfont.css" rel="stylesheet" />
<link href="../../../../_layouts/15/SVDigitalCampus/CSS/animate.css" rel="stylesheet" />
<script src="../../../../_layouts/15/SVDigitalCampus/Script/jquery-1.8.2.min.js"></script>
<link href="../../_layouts/15/SVDigitalCampus/Script/Enter/jquery-webox.css" rel="stylesheet" />
<script src="../../_layouts/15/SVDigitalCampus/Script/Enter/jquery-webox.js"></script>
<script src="../../../../_layouts/15/SVDigitalCampus/Script/Enter/tz_slider.js"></script>
<script src="../../../../_layouts/15/SVDigitalCampus/Script/Enter/uploadFile.js"></script>
<script src="../../../../_layouts/15/SVDigitalCampus/Script/JSBase.js"></script>
<script src="../../../../_layouts/15/SVDigitalCampus/Script/Enter/popwin.js"></script>

<link href="../../../../_layouts/15/SVDigitalCampus/test/type.css" rel="stylesheet" />
<link href="../../../../_layouts/15/SVDigitalCampus/test/Pager.css" rel="stylesheet" />
<script src="../../../../_layouts/15/SVDigitalCampus/test/Pager.js"></script>
<script src="../../../../_layouts/15/SVDigitalCampus/test/json.js"></script>
<script src="../../../../_layouts/15/SVDigitalCampus/test/VocationalEducation.js"></script>
<script src="../../../../_layouts/15/SVDigitalCampus/test/FormatUtil.js"></script>
<script type="text/javascript">
    var pIndex = 1;
    var pCount = 0;
    var pSize = 10;
    //绑定文件内容
    function BindData(data) {
        $("#tab tbody").empty();

        $($.parseJSON(data)).each(function () {
            var QuoteName = "";
            if (this.QuoteID == "0") {
                QuoteName = "本地文件";
            }
            else {
                QuoteName = "校本资源库";
            }
            var tr = "<tr class='J-item Single'><td class='Check'><input type='checkbox' value='" + this.ID
                + "' class='Check_box' name='Check_box'  id='subcheck' onclick='setSelectAll()'/></td><td class='name' style='text-align: left;'><i class='file' style='float: left;cursor:pointer'><img src='" +
                this.Image + "'></i><span style=\"cursor:pointer\" onclick=\" window.open(" + this.Url + ")\">" + this.Title +
                "</span><div class=\"Operation\" style=\"display:none;\">" +
                "<span class=\"more\"><a href=\"#\" class=\"more_m\"><i class=\"iconfont\">&#xe61a;</i></a>" +
                "<ul class=\"M_con\" style=\"display:none;\"><li><a href=\"#\" onclick=\"cl(this,'" + this.ID + "','" + this.Title + "')\">重命名</a></li><li><a href=\"#\" onclick=\"DelFile('" + this.ID + "')\">删除</a></li>"
                + "</ul></span></div>"
                + "</td><td class='File_size'>" + this.Size + "</td><td>" + QuoteName + "</td><td class='Change_date'>" + this.Modified + "</td></tr>";
            $("#tab tbody").append(tr);
        })

        $(".J-item").hover(function () {
            $(this).find(".Operation").show().end().siblings().find('.M_con').hide();
        }, function () {
            $(this).find(".Operation").hide();
        });
        //分享下载
        $('.more_m').click(function () {
            $(this).siblings('.M_con').show();
        });

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

    //绑定文件内容（分页）
    function ShowData(index) {
        var CourseID = GetRequest().CourseID;

        var CatagoryID = $("#CatagoryID").val();
        var postData = { CMD: "FullTab", PageSize: pSize, PageIndex: index, CourseID: CourseID, ContentID: CatagoryID };
        var FirstUrl = window.location.href;
        FirstUrl = FirstUrl.substring(0, FirstUrl.indexOf("SitePages"))

        $.ajax({
            type: "Post",
            url: FirstUrl + "/_layouts/15/SVDigitalCampus/hander/CourseLibrary.aspx",
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
    function Bind() {
        var FirstUrl = window.location.href;
        FirstUrl = FirstUrl.substring(0, FirstUrl.indexOf("SitePages"));
        var CatagoryID = $("#CatagoryID").val();
        var CourseID = GetRequest().CourseID;
        $.ajax({
            type: "Post",
            url: FirstUrl + "/_layouts/15/SVDigitalCampus/hander/CourseLibrary.aspx",
            data: { CMD: "FullTab", PageSize: pSize, PageIndex: pIndex, CourseID: CourseID, ContentID: CatagoryID },
            dataType: "text",
            success: function (returnVal) {
                returnVal = $.parseJSON(returnVal);
                if (returnVal != null) {
                    pCount = returnVal.PageCount;
                    LoadPageControl(ShowData, "pageDiv", pIndex, pSize, Math.ceil(pCount / pSize), pCount);
                    if (pCount > pSize) {
                        $("#pageDiv").show();
                    }
                    else {
                        $("#pageDiv").hide();
                    }
                    if (pCount > 0) {
                        $("#tab").show();
                        $("#kc_create").hide();
                    }
                    if (pCount <= 0) {
                        $("#tab").hide();
                        $("#kc_create").show();
                    }
                }
            },
            error: function (errMsg) {
                alert('数据加载失败！');
            }
        });

    }
    $(function () {
        $("#Status").val(GetRequest().Status);
        BindNave('');
        Bind();
        $("#checkAll").click(function () {
            var flag = $(this).attr("checked") == undefined ? false : true;
            $("input[name='Check_box']:checkbox").each(function () {
                $(this).attr("checked", flag);
            })
        })
        InitBar();
    });
    function InitBar() {
        var Status = $("#Status").val();
        if (Status == "0") {
            $("#Task").attr("class", "kc_yylc_spzt kc_yylc_spoff");
            $("#Check").attr("class", "kc_yylc_spzt kc_yylc_spoff");
        }
        else {
            $("#Task").attr("class", "kc_yylc_spzt kc_yylc_spon");
            $("#Check").attr("class", "kc_yylc_spzt kc_yylc_spon");
            $("#Task").attr("onclick", "AddTask()");
            $("#Check").attr("onclick", "SubMit()");
        }
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
    //删除
    function DelFile(id) {
        var FirstUrl = window.location.href;
        FirstUrl = FirstUrl.substring(0, FirstUrl.indexOf("SitePages"))
        if (confirm("确认删除文件吗")) {
            $.ajax({
                type: "Post",
                url: FirstUrl + "/_layouts/15/SVDigitalCampus/hander/CourseLibrary.aspx",
                data: { CMD: "Del", "DelID": id },
                dataType: "text",
                success: function (returnVal) {
                    if (returnVal == "1") {
                        alert("删除成功");
                        Bind();
                    }
                },
                error: function (errMsg) {
                    alert('删除失败！');
                }
            });
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
                url: FirstUrl + "/_layouts/15/SVDigitalCampus/hander/CourseLibrary.aspx",
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
            url: FirstUrl + "/_layouts/15/SVDigitalCampus/hander/CourseLibrary.aspx",
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
    //修改文件名称
    function cl(em, id, title) {

        var name = $(em).parents("td").children("span").html();
        if (name = title) {
            var v = "<input type='text' value=\"" + name + "\" style='float:left;line-height:10px;margin-top:5px' id=\"txt" + id
                        + "\"/> <i class=\"iconfont tishi true_t\" style=\"margin: 2px; color: #87c352; cursor:pointer;\" onclick=\"EditName(this,'" + id
                        + "','" + name + "')\">&#xe61d;</i> <i class=\"iconfont tishi fault_t\" style=\"margin: 2px; color: #ff6d72;cursor:pointer; \" onclick=\"EditNameQ(this,'" + name
                        + "')\">&#xe61e;</i>";

            $(em).parents("td").children("span").html(v);
            $(em).parents("td").children("span").removeAttr("onclick");
        }
    }
    //绑定左侧导航
    function BindNave(id) {
        $("#Tree").empty();
        var CourseID = GetRequest().CourseID;
        var Title = GetRequest().Title;
        $("#Title").html(decodeURIComponent(Title));

        var FirstUrl = window.location.href;
        FirstUrl = FirstUrl.substring(0, FirstUrl.indexOf("SitePages"))
        $.ajax({
            type: "Post",
            url: FirstUrl + "/_layouts/15/SVDigitalCampus/hander/CourseLibrary.aspx",
            data: { CMD: "Tree", CourseID: CourseID },
            dataType: "text",
            success: function (returnVal) {
                $("#Tree").append(returnVal);
                initTree();
                $("#div" + id).find(".i-con").show();

                Bind();
            },
            error: function (errMsg) {
            }
        });

    }

    function navTopClick() {
        $("[id$='HFoldUrl']").val("");
        $(".navgition").html("全部文件");

        $("#quanbu").css("background", "#F6F6F6");
        $("#quanbu").children(".icon").eq(0).css("background", "#0da6ea");
        $("#quanbu").children(".icon").eq(0).css("border", "1px solid #0da6ea");
        $("[id$='hContent']").val("");

        Bind();

    }
    //左侧导航点击事件
    function NavClick(id, em) {

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
        $("#CatagoryID").val(id);
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
        var length = $('#Tree input').length;

        if (length == 0) {
            var name = $(em).parent().prev().html();
            if (name = title) {
                var v = "<input type='text' value=\"" + name + "\" style='float:left;line-height:10px; width:100px; margin-top:8px;' id=\"Menu" + id
                    + "\"/> <i class=\"iconfont tishi true_t\" style=\"margin: 2px; color: #87c352; cursor:pointer;\" onclick=\"EditMenuName(this,'" + id
                    + "','" + name + "')\">&#xe61d;</i> <i class=\"iconfont tishi fault_t\" style=\"margin: 2px; color: #ff6d72;cursor:pointer; \" onclick=\"EditMenuNameQ(this,'" + name
                    + "')\">&#xe61e;</i>";
                $(em).parent().prev().html(v);
            }
        }
        else {
            alert("有未完成操作");
            $('#Tree input').focus();
        }
    }
    //修改目录名称
    function EditMenuName(em, id, oldname) {
        var FirstUrl = window.location.href;
        FirstUrl = FirstUrl.substring(0, FirstUrl.indexOf("SitePages"))

        var name = $("#Menu" + id).val();
        $.ajax({
            type: "Post",
            url: FirstUrl + "/_layouts/15/SVDigitalCampus/hander/CourseLibrary.aspx",
            data: {
                CMD: "EditMenuName", "EditMenuID": id, "MenuNewName": name
            },
            dataType: "text",
            success: function (returnVal) {
                if (returnVal == "1") {
                    alert("修改成功");
                    BindNave(id);
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
    function delLeftMenu(id) {
        var FirstUrl = window.location.href;
        FirstUrl = FirstUrl.substring(0, FirstUrl.indexOf("SitePages"))

        $.ajax({
            type: "Post",
            url: FirstUrl + "/_layouts/15/SVDigitalCampus/hander/CourseLibrary.aspx",
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
                    $("#div" + id).remove();
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
        // var divNode = document.getElementById("div0");
        var length = $('#Tree input').length

        if (length == 0) {
            var v = "<input type='text' value='' style='float:left;line-height:10px; width:100px; margin-top:8px;' id=\"Menu" + id
               + "\"/> <i class=\"iconfont tishi true_t\" style=\"margin: 2px; color: #87c352;cursor:pointer; \" onclick=\"AddMenu(this,'" + id
               + "','')\">&#xe61d;</i> <i class=\"iconfont tishi fault_t\" style=\"margin: 2px; color: #ff6d72; cursor:pointer;\" onclick=\"Del(this)\">&#xe61e;</i>";
            var html = "<div id='div0' class=\"item\"><div class=\"i-menu cf\"> <span class=\"fl icon\"></span><span class=\"tit fl\">" + v + "</span></div></div>";
            //$("#" + divid).parent().prepend(html);
            var em1 = $("#" + divid).find('.i-con').html();
            var em2 = $("#" + divid).find('.ici-con').html();
            if (em1 == undefined && em2 == undefined) {
                var html = "<div clas\"i-con\"><div id='div0' class=\"item\"><div class=\"i-menu cf\"> <span class=\"fl icon\"></span><span class=\"tit fl\">" + v + "</span></div></div></div>";
                $("#" + divid).append(html);
            }
            if (em1 != undefined) {
                var html = "<div id='div0' class=\"ic-item\"><div class=\"ici-menu cf\"> <span class=\"fl icon\"></span><span class=\"tit fl\">" + v + "</span></div></div>";
                $("#" + divid).find('.i-con').prepend(html)
            }
            if (em1 == undefined && em != undefined) {
                var html = "<div id='div0' class=\"icic-item\"><span class=\"fl icon\"></span><span class=\"tit fl\">" + v + "</span></div>";
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
        var CourseID = GetRequest().CourseID;

        var FileName = $("#Menu" + id + "").val();
        $.ajax({
            type: "Post",
            url: FirstUrl + "/_layouts/15/SVDigitalCampus/hander/CourseLibrary.aspx",

            data: { CMD: "AddMenu", id: id, MenuName: FileName, CourseID: CourseID },
            dataType: "text",
            success: function (returnVal) {
                if (returnVal == "1") {
                    alert("添加成功");
                    BindNave(id);
                    $("#Status").val("1");
                    InitBar();
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
    function AddMenuTop() {
        var length = $('#Tree input').length

        //var divNode = document.getElementById("div0");
        if (length == 0) {
            var v = "<input type='text' value='' style='float:left;line-height:10px; width:100px; margin-top:8px;' id=\"Menu0\"/> <i class=\"iconfont tishi true_t\" style=\"margin: 2px; color: #87c352;cursor:pointer; \" onclick=\"AddMenu(this,0)\">&#xe61d;</i> <i class=\"iconfont tishi fault_t\" style=\"margin: 2px; color: #ff6d72;cursor:pointer; \" onclick=\"Del()\">&#xe61e;</i>";
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
    //    $.webox({ height: 550, width: 600, bgvisibel: true, title: '修改专业学科', iframe: FirstUrl + '/SitePages/EditMajor.aspx?CourseID=' + CourseID + "&CatagoryID=" + CatagoryID });

    //    popWin.showWin("600", "500", "修改专业学科", FirstUrl + "/SitePages/EditMajor.aspx", "no");
    //}
    //function shaixuan() {
    //    var dis = $(".File_type").css("display");
    //    if (dis == "none") {
    //        $(".File_type").show();
    //    }
    //    else {
    //        $(".File_type").hide();
    //    }
    //}
    //文件上传
    function uplFile() {
        var FirstUrl = window.location.href;
        FirstUrl = FirstUrl.substring(0, FirstUrl.indexOf("SitePages"));
        var CourseID = GetRequest().CourseID;

        var CatagoryID = $("#CatagoryID").val();
        $.webox({ height: 550, width: 600, bgvisibel: true, title: '文件上传', iframe: FirstUrl + '/SitePages/FileUpload.aspx?CourseID=' + CourseID + "&CatagoryID=" + CatagoryID });

        //popWin.showWin('600', '550', '文件上传', FirstUrl + '/SitePages/FileUpload.aspx?CourseID=' + CourseID + "&CatagoryID=" + CatagoryID, "no");
    }
    //文件选择
    function SelFile() {
        var FirstUrl = window.location.href;
        FirstUrl = FirstUrl.substring(0, FirstUrl.indexOf("SitePages"));
        var CourseID = GetRequest().CourseID;
        var CatagoryID = $("#CatagoryID").val();
        $.webox({ height: 550, width: 600, bgvisibel: true, title: '选择文件', iframe: FirstUrl + '/SitePages/RC_wp_SelNav.aspx?CourseID=' + CourseID + "&Title=" + encodeURIComponent($("#Title").html()) });
        //;popWin.showWin('600', '550', '选择文件', FirstUrl + '/SitePages/RC_wp_SelNav.aspx?CourseID=' + CourseID + "&Title=" + encodeURIComponent($("#Title").html()), "auto");
    }
    function GetRequest() {
        var url = location.search; //获取url中"?"符后的字串

        var theRequest = new Object();

        if (url.indexOf("?") != -1) {

            var str = url.substr(1);

            strs = str.split("&");

            for (var i = 0; i < strs.length; i++) {

                theRequest[strs[i].split("=")[0]] = (strs[i].split("=")[1]);

            }

        }
        return theRequest;
    }
    //修改基本信息
    function BaseData() {
        var FirstUrl = window.location.href;
        FirstUrl = FirstUrl.substring(0, FirstUrl.indexOf("SitePages"));
        var CourseID = GetRequest().CourseID;
        $.webox({ width: 800, height: 570, bgvisibel: true, title: '修改课程信息', iframe: FirstUrl + '/SitePages/CouseEdit.aspx?CourseID=' + CourseID });

        //popWin.showWin("800", "570", "修改课程信息", FirstUrl + "/SitePages/CouseEdit.aspx?CourseID=" + CourseID, "auto");
    }
    //提交审核
    function SubMit() {
        var FirstUrl = window.location.href;
        FirstUrl = FirstUrl.substring(0, FirstUrl.indexOf("SitePages"));
        var CourseID = GetRequest().CourseID;

        $.ajax({
            type: "Post",
            url: FirstUrl + "/_layouts/15/SVDigitalCampus/hander/CourseLibrary.aspx",

            data: { CMD: "SubMit", CourseID: CourseID },
            dataType: "text",
            success: function (returnVal) {
                if (returnVal == "1") {
                    alert("提交成功");
                    $("#Status").val("1");
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
    function Return() {
        var FirstUrl = window.location.href;
        FirstUrl = FirstUrl.substring(0, FirstUrl.indexOf("SitePages"));

        window.location.href = FirstUrl + "/SitePages/CaurseManage.aspx";
    }
    function AddTask() {
        var FirstUrl = window.location.href;
        FirstUrl = FirstUrl.substring(0, FirstUrl.indexOf("SitePages"));
        var CourseID = GetRequest().CourseID;
        var Title= GetRequest().Title
        window.location.href = FirstUrl + "/SitePages/RC_wp_CaurseTaskAdd.aspx?CourseID=" + CourseID + "&Title=" + Title + "&Status=" + $("#Status").val();
    }
</script>
<!--校本资源库-->

<div class="School_library">
    <input id="CatagoryID" type="hidden" />
    <input id="Status" type="hidden" value="0" />

    <div class="kc_yylc_sp">
        <div class="kc_yylc_spxian"></div>
        <div class="kc_yylc_spbiao">
            <table>
                <tbody>
                    <tr>

                        <td  class="kc_yylc_sptd"><a onclick="Return()" class="kc_yylc_spzt kc_yylc_spon" style="cursor:pointer;"><<返回</a></td>

                        <td class="kc_yylc_sptd">
                            <a id="BaseData" class="kc_yylc_spzt kc_yylc_spon" onclick="BaseData()">基础资料</a>
                        </td>
                        <td class="kc_yylc_sptd">
                            <a id="SelData" class="kc_yylc_spzt kc_yylc_spon">选取资源</a>
                        </td>
                        <td class="kc_yylc_sptd">
                            <a id="Task" class="kc_yylc_spzt kc_yylc_spoff">创建任务</a>
                        </td>
                        <td class="kc_yylc_sptd">
                            <a id="Check" class="kc_yylc_spzt kc_yylc_spoff">提交审核</a>
                            <div class="weitongguo" style="display: none" id="weitongguo"><span></span></div>

                            <%--<div class="weitongguo" runat="server" visible='<%#Eval("Status").ToString()=="2"?true:false %>' id="weitongguo"><span></span></div>--%>
                        </td>
                        <%-- <td class="kc_yylc_sptd"><a class="kc_yylc_spzt kc_yylc_spcc" href="">待审核</a></td>
                                                                            <td class="kc_yylc_sptd"><a class="kc_yylc_spzt kc_yylc_spoff" href="">审核通过</a></td>--%>
                        <td class="kc_yylc_sptd">
                            <a id="Room" class="kc_yylc_spzt kc_yylc_spoff">预约教室</a>
                            <div class="jujue" style="display: none">
                                <span></span>
                                <p>拒绝理由拒绝理由</p>
                            </div>
                        </td>
                        <td class="kc_yylc_sptd">
                            <a id="CheckStu" class="kc_yylc_spzt kc_yylc_spoff">审核报名</a>
                        </td>
                       <%-- <td class="kc_yylc_sptd">
                            <a id="OpenClass" class="kc_yylc_spzt kc_yylc_spoff">开课</a>
                        </td>--%>
                    </tr>
                </tbody>
            </table>
        </div>
    </div>
    <!--页面名称-->
    <div class="Whole_display_area">

        <div class="clear"></div>
        <div class="Schoolcon_wrap">
            <div class="left_navcon fl">
                <h1 id="Title"></h1>
                <h3 class="tit" style="background: #F6F6F6" id="quanbu" onclick="navTopClick()">
                    <i class="icon" style="background: #0da6ea; border: 1px solid #0da6ea"></i>全部
                    <i class="iconfont" style="cursor: pointer;" id="toptianjia" onclick="AddMenuTop()">&#xe630;</i></h3>
                <div class="select-box" id="Tree">
                </div>
            </div>
            <div class="right_dcon fr">
                <!--操作区域-->
                <div class="Operation_area">

                    <div class="right_add fr" id="opertion" style="display: block">
                        <a href="#" class="add" onclick="SelFile()"><i class="iconfont">&#xe65a;</i>选择文件</a>
                        <a href="#" class="add" onclick="uplFile()"><i class="iconfont">&#xe65a;</i>本地文件</a>
                    </div>
                </div>
                <div class="clear"></div>

                <!--展示区域-->
                <div class="Display_form">
                    <div class="content">
                        <div class="tc">
                            <div class="Order_form">
                                <div class="Food_order">
                                    <div class="kc_create" id="kc_create" style="display: none; width: 95%;">
                                        <h1>您尚未添加任何资料</h1>
                                        <h2>点击<a onclick="SelFile()">+选择文件</a>进行添加吧！</h2>
                                    </div>
                                    <table class="O_form" id="tab" style="display: none">
                                        <thead>
                                            <tr class="O_trth">
                                                <!--表头tr名称-->
                                                <th class="Check">
                                                    <input type="checkbox" class="Check_box" id="checkAll" /></th>
                                                <th class="name">文件名称 </th>
                                                <th class="File_size">文件大小 </th>
                                                <th class="name">文件来源 </th>
                                                <th class="Change_date">修改日期 </th>
                                            </tr>
                                        </thead>
                                        <tbody></tbody>

                                    </table>
                                    <div id="pageDiv" class="pageDiv" style="display: none">
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
