<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="XXK_wp_CourceHomeUserControl.ascx.cs" Inherits="Sinp_TeacherWP.WebParts.XXK.XXK_wp_CourceHome.XXK_wp_CourceHomeUserControl" %>
<script src="../../../../_layouts/15/Script/jquery-1.8.2.min.js"></script>
<link href="../../../../_layouts/15/Style/common.css" rel="stylesheet" />
<link href="../../../../_layouts/15/Style/iconfont.css" rel="stylesheet" />
<link href="../../../../_layouts/15/Style/style1.css" rel="stylesheet" />
<script src="../../../../_layouts/15/Script/popwin.js"></script>

<script type="text/javascript">
    $(function () {
        notice();
        CourceList();
        TeacherList();
    })
    //公告信息
    function notice() {
        var FirstUrl = window.location.href;
        FirstUrl = FirstUrl.substring(0, FirstUrl.indexOf("SitePages"))

        $.ajax({
            type: "Post",
            url: FirstUrl + "/_layouts/15/hander/Caurse.aspx",
            data: { Func: "GetNotice" },
            dataType: "text",
            success: function (returnVal) {
                returnVal = $.parseJSON(returnVal);
                if (returnVal != null) {
                    $("#notic").empty();
                    $($.parseJSON(returnVal.Data)).each(function () {
                        var li = "<li class='ali'><i>" + this.Num + "</i><a href=\"\">" + this.Name + "</a><span>" + this.Created + "</span></li>";
                        $("#notic").append(li);
                    });
                }
            },
            error: function (errMsg) {
                alert('数据加载失败！');
            }
        });
    }
    //课程信息
    function CourceList() {

        var FirstUrl = window.location.href;
        FirstUrl = FirstUrl.substring(0, FirstUrl.indexOf("SitePages"))

        $.ajax({
            type: "Post",
            url: FirstUrl + "/_layouts/15/hander/Caurse.aspx",
            data: { Func: "GetCourseList", Status: 5 },
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
                            if (this.ApplyNum < this.MaxNum) {
                                ShowName = "<span><a onclick='SingUp(" + this.ID + ")'  style='cursor:pointer'>报名</a></span>";
                            }
                        }
                        else if (returnResult == "2") {
                            ShowName = "<span><a>已报名</a></span>";
                        }
                        else {
                            ShowName = ""
                        }
                        var href = FirstUrl + "/SitePages/RC_wp_CourseDetail.aspx?CourseID=" + this.ID + "&PostTtyp=" + returnResult;
                        var ul = "<ul class='course'><li class='sb_fz01'><a  href='" + href + "'><img src=" + this.Attachments + "></a></li><li class='sb_fz02'  style='cursor:pointer' onclick='Detail(\"" + href + "\")'>'<div class='lf_tp'>" +
                        "<a><img src='" + this.TeacherZP + "' alt=''></a><h3>" + this.TeacherXM + "</h3></div><div class='rf_xq'>" +
                        "<h3>" + this.Title + "</h3><p>" + this.Introduc + "</p></div></li>" +
               " <li class='sb_fz03'> <h2>" + this.Title + "</h2>" + ShowName + "</li><li class='sb_fz04'>" + this.WeekName + "<div style='width: 110px; float: right;'>报名人数" + this.ApplyNum + "/" + this.MaxNum + "</div></li></ul>";
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
    //课程报名
    function SingUp(CourseID) {

        var FirstUrl = window.location.href;
        FirstUrl = FirstUrl.substring(0, FirstUrl.indexOf("SitePages"))
        $.ajax({
            type: "Post",
            url: FirstUrl + "/_layouts/15/hander/Caurse.aspx",
            data: { Func: "SingUp", CourseID: CourseID },
            dataType: "text",
            success: function (returnVal) {
                if (returnVal == "1") {
                    alert("报名成功");
                    GetMyCourse();
                }
                else if (returnVal == "2") {
                    alert("每个学生只能选秀两门课程！");
                }
                else {
                    alert("报名失败");
                }
            },
            error: function (errMsg) {
            }
        });

    }
    function IsStu(CourseID) {
        var FirstUrl = window.location.href;
        FirstUrl = FirstUrl.substring(0, FirstUrl.indexOf("SitePages"))
        //var CourseID = GetRequest().CourseID;

        $.ajax({
            type: "Post",
            url: FirstUrl + "/_layouts/15/hander/Caurse.aspx",
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
    //老师信息
    function TeacherList() {
        var FirstUrl = window.location.href;
        FirstUrl = FirstUrl.substring(0, FirstUrl.indexOf("SitePages"))

        $.ajax({
            type: "Post",
            url: FirstUrl + "/_layouts/15/hander/Caurse.aspx",
            data: { Func: "GetAllTeacher" },
            dataType: "text",
            success: function (returnVal) {
                returnVal = $.parseJSON(returnVal);
                if (returnVal != null) {
                    $(".gd").empty();
                    $($.parseJSON(returnVal.Data)).each(function () {

                        var li = "<li><img src='http://192.168.1.47:9001" + "/" + this.ZP + "'><div class='con fr'><h3>" + this.XM + "</h3><p>" + this.BZ + "</p></div></li>";

                        $(".gd").append(li);
                    });
                }
            },
            error: function (errMsg) {
                alert('数据加载失败！');
            }
        });
    }
    function MoreCourse() {
        var FirstUrl = window.location.href;
        FirstUrl = FirstUrl.substring(0, FirstUrl.indexOf("SitePages"))

        window.location.href = FirstUrl + "/SitePages/RC_wp_RegistCourse.aspx";
    }
</script>
<script type="text/javascript">

    // 0-1校本课程 页面 中优秀教师向上滚动
    $(function () {
        var liH = -$('.gd li').height(),   															// 每条数据的高度
				gdT = 2000;																									// 每条数据滚动的时间
        function g() {																									//定义并执行函数
            $('.gd').animate({ 'top': liH }, gdT, 'linear', function () {				//定义动画的  效果、时间、是否匀速、动画执行完毕执行的函数
                $(this).css({ 'top': 0 });																			//停止滚动
                var liF = $('.gd li').eq(0);																//第一条数据  先删除，然后增加到最后 并重复执行 函数a
                liF.remove();
                $(this).append(liF);
                g();
            });
        }; g();
        $('.gd').hover(function () {																				//鼠标放上去执行函数
            $(this).stop();					//停止动画
        }, function () {																											//鼠标离开执行的函数
            var t = $(this).css('top'),
					a = parseInt(t.replace('px', '')),															//获取当前已经滚动的距离此时 肯定是负数
					d = gdT - gdT * (a / liH);
            //再次滚动执行的时间 防止速度不一致
            $(this).animate({ 'top': liH }, d, 'linear', function () {				//定义动画的  效果、时间、是否匀速、动画执行完毕执行的函数
                $(this).css({ 'top': 0 });																			//停止滚动
                var liF = $('.gd li').eq(0);																//第一条数据  先删除，然后增加到最后 并重复执行 函数a
                liF.remove();
                $(this).append(liF);
                g();
            });
        });
        // $('.header').mouseover(function(){
        // 	console.log(1);
        // })
    })
</script>

<!--校本课程首页-->
<div class="Course_home">

    <!--页面上半部分 新闻和优秀教师-->
    <div class="xbkc">
        <input id="hdType" type="hidden" />
        <div class="xxk_gonggao fl">
            <div class="biaoti">
                <h2><span class="title_left fl"><a href="#">选修课公告</a></span><span class="title_right fr"><a class="more" href="#"></a></span></h2>
            </div>
            <div class="content">
                <div class="xxk_tp">
                    <img src="../../../../_layouts/15/Image/bg_login.jpg" alt="">
                    <p>新一轮的选修课正式开课了</p>
                </div>
                <ul id="notic">
                </ul>
            </div>
        </div>
        <div class="yx_tea fr">
            <div class="biaoti">
                <h2><span class="title_left fl"><a href="#">优秀教师</a></span><span class="title_right fr"><a class="more" href="#"></a></span></h2>
            </div>
            <div class="scroll_top">
                <ol class="gd">
                </ol>
            </div>
        </div>

    </div>
    <div class="allxxk_list">
        <div class="biaoti">
            <h2><span class="title_left fl"><a href="#">报名选课</a></span><span class="title_right fr"><a class="more" href="#" onclick="MoreCourse()">更多+</a></span></h2>
        </div>
        <div id="CourceList"></div>

    </div>
</div>
