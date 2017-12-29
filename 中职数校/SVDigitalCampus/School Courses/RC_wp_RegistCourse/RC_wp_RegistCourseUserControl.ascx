<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RC_wp_RegistCourseUserControl.ascx.cs" Inherits="SVDigitalCampus.School_Courses.RC_wp_RegistCourse.RC_wp_RegistCourseUserControl" %>
<script src="../../../../_layouts/15/SVDigitalCampus/Script/jquery-1.8.2.min.js"></script>
<link href="../../../../_layouts/15/SVDigitalCampus/CSS/common.css" rel="stylesheet" />
<link href="../../../../_layouts/15/SVDigitalCampus/CSS/style.css" rel="stylesheet" />
<link href="../../../../_layouts/15/SVDigitalCampus/CSS/animate.css" rel="stylesheet" />
<script type="text/javascript">
    $(function () {
        CourceList();
        GetMyCourse();
    })
    //绑定上部分-该学生已经选修的课程
    function GetMyCourse() {
        //参数为0只判断身份不判断选修几门课程
        IsStu(0);
        var FirstUrl = window.location.href;
        FirstUrl = FirstUrl.substring(0, FirstUrl.indexOf("SitePages"))
        var PostTtyp = $("#hdType").val();
        if (PostTtyp == "0") {
            $("#WeekTwo").hide();
            $("#WeekThird").hide();
        }
        else {
            $.ajax({
                type: "Post",
                url: FirstUrl + "/_layouts/15/SVDigitalCampus/hander/Caurse.aspx",
                data: { Func: "GetMyCourse" },
                dataType: "text",
                success: function (returnVal) {
                    returnVal = $.parseJSON(returnVal);
                    if (returnVal != null) {

                        $($.parseJSON(returnVal.Data)).each(function () {

                            var html = " <img src=" + this.Attachments + " alt=''><ul class='Register_detial'><li class='ali'><h2>" + this.Title + "</h2><a onclick='quxiao(" + this.ID + ")'  style='cursor:pointer;'>取消报名</a></li><li><h3>" +
                               this.TeacherXM + "</h3><span>" + this.WeekName + "</span></li></ul>";
                            if (this.WeekName == "周三") {
                                $("#WeekThird").attr("class", "kc_quxiao");
                                $("#WeekThird").html("");
                                $("#WeekThird").append(html);
                            }
                            else {
                                $("#WeekTwo").attr("class", "kc_quxiao");
                                $("#WeekTwo").html("");
                                $("#WeekTwo").append(html);
                            }
                            $("MyCourseID").val($("MyCourseID").val() + this.ID + ",")
                        });
                    }
                },
                error: function (errMsg) {
                    alert('数据加载失败！');
                }
            });
        }
    }
    //所有课程
    function CourceList() {
        var FirstUrl = window.location.href;
        FirstUrl = FirstUrl.substring(0, FirstUrl.indexOf("SitePages"))

        $.ajax({
            type: "Post",
            url: FirstUrl + "/_layouts/15/SVDigitalCampus/hander/Caurse.aspx",
            data: { Func: "GetCourseList", Status: 5, weekName: $("#hdweek").val() },
            dataType: "text",
            success: function (returnVal) {
                returnVal = $.parseJSON(returnVal);
                if (returnVal != null) {
                    $("#CourceList").empty();

                    $($.parseJSON(returnVal.Data)).each(function () {
                        var ShowName = ""
                        IsStu(this.ID);
                        var returnResult = $("#hdType").val();
                        if (returnResult == "1") {
                            if (this.MaxNum > this.ApplyNum) {
                                ShowName = "<span><a onclick=\"SingUp(" + this.ID + ",'" + this.WeekName + "')\"  style='cursor:pointer'>报名</a></span>";
                            }
                        }
                        else if (returnResult == "2") {
                            ShowName = "<span><a>已报名</a></span>";
                        }
                        else {
                            ShowName = ""
                        }
                        var url = FirstUrl + "/SitePages/RC_wp_CourseDetail.aspx?CourseID=" + this.ID + "&PostTtyp=" + returnResult;
                        var ul = "<ul class='course'><li class='sb_fz01'><a  href='" + url + "'><img src=" + this.Attachments + " alt=''></a></li><li class='sb_fz02' style='cursor:pointer' onclick='Detail(\"" + url + "\")'>'<div class='lf_tp'>" +
                        "<img src='" + this.TeacherZP + "' alt=''><h3>" + this.TeacherXM + "</h3></div><div class='rf_xq'>" +
                        "<h3>" + this.Title + "</h3><p>" + this.Introduc + "</p></div></li>" +
               " <li class='sb_fz03'> <h2>" + this.Title + "</h2>" + ShowName + "</li><li class='sb_fz04'>" + this.WeekName + "<div style='width: 110px; float: right;'>报名人数" + this.MaxNum + "/" + this.ApplyNum + "</div></li></ul>";
                        $("#CourceList").append(ul);
                    });
                }
            },
            error: function (errMsg) {
                alert('数据加载失败！');
            }
        });
    }
    function Detail(href) {
        window.location.href = href;
    }
    function IsStu(CourseID) {
        var FirstUrl = window.location.href;
        FirstUrl = FirstUrl.substring(0, FirstUrl.indexOf("SitePages"))
        //var CourseID = GetRequest().CourseID;

        $.ajax({
            type: "Post",
            url: FirstUrl + "/_layouts/15/SVDigitalCampus/hander/Caurse.aspx",
            data: { Func: "IsStu", CourseID: CourseID },
            async: false,
            dataType: "text",
            success: function (returnVal) {
                $("#hdType").val(returnVal);
            },
            error: function (errMsg) {
                alert('数据加载失败！');
            }
        });
    }
    //课程报名
    function SingUp(CourseID, WeekName) {

        //var CourseID = GetRequest().CourseID;
        var FirstUrl = window.location.href;
        FirstUrl = FirstUrl.substring(0, FirstUrl.indexOf("SitePages"))
        $.ajax({
            type: "Post",
            url: FirstUrl + "/_layouts/15/SVDigitalCampus/hander/Caurse.aspx",
            data: { Func: "SingUp", CourseID: CourseID, week: WeekName },
            dataType: "text",
            success: function (returnVal) {
                if (returnVal == "1") {
                    alert("报名成功");
                    GetMyCourse();
                    CourceList();
                }
                else if (returnVal == "2") {
                    alert("超过做多选修课限制数！");
                }
                else if (returnVal == "3") {
                    alert("选修课上课时间冲突！");
                }
                else {
                    alert("报名失败");
                }
            },
            error: function (errMsg) {
            }
        });

    }
    function WeekChange() {
        var week = $("select[id$='Week']").val();
        $("#hdweek").val(week);
        CourceList();
    }
</script>
<div class="course_lists">
    <div class="kc_choose">
        <input id="hdType" type="hidden" />
        <input id="hdweek" type="hidden" value="" />

        <div class="navgition">
            <a href="http://192.168.1.47/sites/ZZSZXY/CouseManage/SitePages/RC_wp_CourseHome.aspx">校本课程</a> > 
        <a href="http://192.168.1.47/sites/ZZSZXY/CouseManage/SitePages/RC_wp_RegistCourse.aspx">报名选课</a>
        </div>
        <%--<div class="kc_quxiao">
       
    </div>--%>
        <div class="kc_day" id="WeekTwo">周二/周四课程</div>
        <div class="kc_day" id="WeekThird">周三课程</div>
    </div>
    <div class="allxxk_list">
        <div class="Tips">
            <select name="education" id="Week" style="width: 220px;" onchange="WeekChange()">
                <option value="" selected="selected">全部课程</option>
                <option value="周二">周二课程</option>
                <option value="周三">周三课程</option>
                <option value="周四">周四课程</option>

            </select>提醒：每位同学周二或周四必须选一节，周三必须选一节课哦~

        </div>

        <div id="CourceList"></div>
    </div>
</div>
