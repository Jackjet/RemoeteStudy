<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RC_wp_CourseCheckDetailUserControl.ascx.cs" Inherits="SVDigitalCampus.School_Courses.RC_wp_CourseCheckDetail.RC_wp_CourseCheckDetailUserControl" %>
<script src="../../../../_layouts/15/SVDigitalCampus/Script/jquery-1.8.2.min.js"></script>
<link type="text/css" rel="stylesheet" href="../../../../_layouts/15/SVDigitalCampus/CSS/common.css" />
<link type="text/css" rel="stylesheet" href="../../../../_layouts/15/SVDigitalCampus/CSS/style.css" />
<link type="text/css" rel="stylesheet" href="../../../../_layouts/15/SVDigitalCampus/CSS/iconfont.css" />
<link type="text/css" rel="stylesheet" href="../../../../_layouts/15/SVDigitalCampus/CSS/animate.css" />

<script type="text/javascript">
    $(function () {
        var CourseID = GetRequest().CourseID;
        var Type = GetRequest().Type;
        if (Type == null || Type == "") {
            $("#Check").hide();
        }
        $("[id$='Hidden1']").val(CourseID);
        CourseDetail();
        BindNave();
        BindData();
        BindTask();
    })
    //绑定上部分-该学生已经选修的课程

    function CourseDetail() {
        var FirstUrl = window.location.href;
        FirstUrl = FirstUrl.substring(0, FirstUrl.indexOf("SitePages"))
        var CourseID = GetRequest().CourseID;
        $.ajax({
            type: "Post",
            url: FirstUrl + "/_layouts/15/SVDigitalCampus/hander/Caurse.aspx",
            data: { Func: "GetCourseList", "CourseID": CourseID },
            dataType: "text",
            success: function (returnVal) {
                returnVal = $.parseJSON(returnVal);
                if (returnVal != null) {
                    $(".music_kc").empty();
                    $($.parseJSON(returnVal.Data)).each(function () {
                        var WeekName = "";
                        if (this.WeekID == "3") {
                            WeekName = "周三上课";
                        }
                        else {
                            WeekName = "周二上课";
                        }

                        var ul = "<img src=" + this.Attachments + " alt=''><div class='music_nr'><h2>" + this.Title + "</h2><div class='tit_de'><span>主讲教师：" + this.TeacherXM
                            + "</span><span>上课时间：" + WeekName + " </span><span>上课地点：教学楼103</span></div><p>" + this.Introduc
                            + "</p><div></div></div>";
                        $(".music_kc").append(ul);
                    });
                }
            },
            error: function (errMsg) {
                alert('数据加载失败！');
            }
        });
    }
    function BindData() {
        var FirstUrl = window.location.href;
        FirstUrl = FirstUrl.substring(0, FirstUrl.indexOf("SitePages"));
        var CatagoryID = $("#CatagoryID").val();
        var CourseID = GetRequest().CourseID;
        $.ajax({
            type: "Post",
            url: FirstUrl + "/_layouts/15/SVDigitalCampus/hander/Caurse.aspx",
            data: { Func: "GetData", CourseID: CourseID, ContentID: CatagoryID },
            dataType: "text",
            success: function (returnVal) {
                returnVal = $.parseJSON(returnVal);
                if (returnVal != null) {
                    $("#Data").empty();
                    $($.parseJSON(returnVal.Data)).each(function () {
                        var li = "<li><a href=''><i class='iconfont'><img src='" + this.Image + "'></i>" + this.Title + "</a></li>";
                        $("#Data").append(li);

                    });
                }
            },
            error: function (errMsg) {
                alert('数据加载失败！');
            }
        });
    }
    function BindTask() {
        var FirstUrl = window.location.href;
        FirstUrl = FirstUrl.substring(0, FirstUrl.indexOf("SitePages"));
        var CatagoryID = $("#CatagoryID").val();
        var CourseID = GetRequest().CourseID;
        $.ajax({
            type: "Post",
            url: FirstUrl + "/_layouts/15/SVDigitalCampus/hander/Caurse.aspx",
            data: { Func: "GetTask", CourseID: CourseID, ContentID: CatagoryID },
            dataType: "text",
            success: function (returnVal) {
                returnVal = $.parseJSON(returnVal);
                if (returnVal != null) {
                    $("#Task").empty();
                    $($.parseJSON(returnVal.Data)).each(function () {
                        var li = "<li><a href=''><i class='iconfont'></i>" + this.Title + "</a></li>";
                        $("#Task").append(li);

                    });
                }
            },
            error: function (errMsg) {
                alert('数据加载失败！');
            }
        });
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
        BindData();
        BindTask();
    }
    //绑定课程目录
    function BindNave() {
        $("#Tree").empty();
        var CourseID = GetRequest().CourseID;

        var FirstUrl = window.location.href;
        FirstUrl = FirstUrl.substring(0, FirstUrl.indexOf("SitePages"))
        $.ajax({
            type: "Post",
            url: FirstUrl + "/_layouts/15/SVDigitalCampus/hander/CourseLibrary.aspx",
            data: { CMD: "Tree", CourseID: CourseID, Edit: "no" },
            dataType: "text",
            success: function (returnVal) {
                $("#Tree").append(returnVal);
                initTree();
            },
            error: function (errMsg) {
            }
        });

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
    function SelData() {
        $("#SelData").attr("class", "active");
        $("#SelTask").removeClass();
        $("#ddData").show();
        $("#ddTask").hide();
    }
    function SelTask() {
        $("#SelTask").attr("class", "active");
        $("#SelData").removeClass();
        $("#ddData").hide();
        $("#ddTask").show();
    }
    function Check() { }
</script>

<div class="course_check">
    <div class="my_kecheng" style="margin: 0">
        <div class="music_kc">
        </div>
        <!-- 开始 -->
        <div class="left_navcon fl">
            <div class="ty_biaoti">
                <span class="active">目录</span>
            </div>
            <h3 class="tit"><i class="icon"></i>全部</h3>
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
                <dd id="ddTask" style="display: none;">
                    <ul id="Task">
                    </ul>
                </dd>
            </dl>
        </div>

    </div>
    <div class="audit_opinion" id="Check">
        <p class="result">
            <span style="width: 50%; text-align: right;">审批结果：</span>
            <span class="lableinput">
                <asp:RadioButtonList ID="rbStatus" runat="server" RepeatColumns="2" RepeatDirection="Horizontal" RepeatLayout="Flow" Width="240px" TextAlign="Right">
                    <asp:ListItem Value="3"><span class="span_l">审核失败</span></asp:ListItem>
                    <asp:ListItem Value="4" Selected="True"><span class="span_r">审核通过</span></asp:ListItem>
                </asp:RadioButtonList>
            </span>
            <input id="Hidden1" type="hidden" runat="server" />
        </p>
        <p class="Opinion">
            <span style="width: 50%; text-align: right;">审批意见：</span>
            <span class="textarea">
                <textarea id="txtIdear" cols="20" rows="2" runat="server"></textarea></span>
        </p>
        <asp:Button ID="btCheck" runat="server" Text="确定" CssClass="surebtn" OnClick="btCheck_Click" />
    </div>
</div>
