<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="XXK_wp_MyCourceManageUserControl.ascx.cs" Inherits="Sinp_TeacherWP.WebParts.XXK.XXK_wp_MyCourceManage.XXK_wp_MyCourceManageUserControl" %>
<link href="../../../_layouts/15/Style/iconfont.css" rel="stylesheet" />
<link href="../../../_layouts/15/Style/common.css" rel="stylesheet" />
<link href="../../../_layouts/15/Style/layout.css" rel="stylesheet" />
<link href="../../../_layouts/15/Style/style.css" rel="stylesheet" />
<link href="../../../_layouts/15/Style/tablelist.css" rel="stylesheet" />
<link href="../../../../_layouts/15/Style/style1.css" rel="stylesheet" />
<script src="/_layouts/15/Script/jquery-1.8.0.js"></script>
<script src="../../../../_layouts/15/Script/xb/popwin.js"></script>
<style type="text/css">
    .music_kc .ZT_img {
        width: 100px;
        height: 46px;
    }
</style>
<script type="text/javascript">
    $(function () {
        $("#Title").val("校本课程");
        BindNave('');
    });
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
    }
   
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
</script>
<div class="yy_tab">
    <div class="yy_tabheader">
        <ul class="tab_tit">
            <li>
                <asp:LinkButton ID="lbMain" runat="server" OnClick="lbMain_Click">选修课信息</asp:LinkButton></li>
            <li class="selected">
                <asp:LinkButton ID="lbMy" runat="server">我的选修课</asp:LinkButton></li>
        </ul>

    </div>
    <%--<div class="content" style="width: 99%">--%>
    <div class="my_kecheng">
        <div class="music_kc">
            <span class="zt fr">
                <asp:Image ID="Img_Status" ImageUrl="/_layouts/15/TeacherImages/wait.png" runat="server" CssClass="ZT_img" /></span>
            <img src="../../../../_layouts/15/Style/choosen/chosen-sprite.png" alt="">
            <div class="music_nr">
                <h2>音乐选修课</h2>
                <div>
                    <span>课程类别：专业选修课</span>
                    <span>上课场地：107场地</span>
                    <span>选课人数上线：50</span>
                    <span>硬件要求：音响、多媒体</span>
                </div>
                <p style="max-height: 100px;">在高等学校中学习某一专业的学生可以有选择地修习的课程。有些选修课是为介绍先进科学技术和最新科学成果；有些选修课是为扩大学生知识面（如中国语言文学专业的学生选修通史，化学专业的学生选修生物学，会计专业的学生选修法学概论等）；还有些选修课是为满足学生的兴趣爱好，发展他们某一方面的才能(如专业的学生选修文学、音乐、绘画、戏剧等课程)。选修课可分为限制性选修课与非限制性选修课。限制性选修课也称指定选修课，指学生须在某一学科门类的领域或一组课程中选修；如有的专业教学计划规定高年级学生须在某一专门组或选修组中选修若干门课程...</p>
                <%--<div>附件：<a href="">课程评价标准.doc</a></div>--%>
            </div>
        </div>
        <!-- 开始 -->
        <div class="left_navcon fl">
            <h1 id="Title"></h1>

            <h3 class="tit" style="background: #F6F6F6" id="quanbu" onclick="navTopClick()">
                <i class="icon" style="background: #0da6ea; border: 1px solid #0da6ea"></i>全部
                    <i class="iconfont" style="cursor: pointer;" id="toptianjia" onclick="AddMenuTop()">&#xe630;</i></h3>
            <div class="select-box" id="Tree">
            </div>
        </div>
        <!-- 结束 -->
        <div class="kc_ziyuan">
            <dl class="js_jianjie">
                <dt class="ty_biaoti">
                    <span class="active" onclick="SelData()" id="SelData" style="cursor: pointer">课程资源</span>
                    <span onclick="SelTask()" id="SelTask" style="cursor: pointer">课程作业</span>
                </dt>
                <dd id="ddData" style="display: block:">
                    <ul id="Data">
                    </ul>
                </dd>
                <dd id="ddTask" style="display: none;" class="kc_zuoye">
                    <ul id="Task">
                    </ul>
                </dd>
            </dl>
        </div>

    </div>
    <%-- </div>--%>
</div>

