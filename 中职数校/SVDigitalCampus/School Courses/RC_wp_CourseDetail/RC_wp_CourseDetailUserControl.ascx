<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RC_wp_CourseDetailUserControl.ascx.cs" Inherits="SVDigitalCampus.School_Courses.RC_wp_CourseDetail.RC_wp_CourseDetailUserControl" %>
<script src="../../../../_layouts/15/SVDigitalCampus/Script/jquery-1.8.2.min.js"></script>
<link href="../../../../_layouts/15/SVDigitalCampus/Script/Enter/jquery-webox.css" rel="stylesheet" />
<script src="../../../../_layouts/15/SVDigitalCampus/Script/Enter/jquery-webox.js"></script>
<script src="../../../../_layouts/15/SVDigitalCampus/Script/Enter/popwin.js"></script>
<link href="../../../../_layouts/15/SVDigitalCampus/CSS/common.css" rel="stylesheet" type="text/css" />
<link type="text/css" rel="stylesheet" href="../../../../_layouts/15/SVDigitalCampus/CSS/style.css" />
<link type="text/css" rel="stylesheet" href="../../../../_layouts/15/SVDigitalCampus/CSS/iconfont.css" />
<link type="text/css" rel="stylesheet" href="../../../../_layouts/15/SVDigitalCampus/CSS/animate.css" />

<script type="text/javascript">
    $(function () {
        //头部面包屑
        Navtip();
        //我的课程
        GetMyCourse();
        //课程详细信息
        CourseDetail();
        //章节绑定
        BindNave();
        //资料
        BindData();
        //任务
        BindTask();
    })
    //绑定上部分-该学生已经选修的课程
    function GetMyCourse() {
        var FirstUrl = window.location.href;
        FirstUrl = FirstUrl.substring(0, FirstUrl.indexOf("SitePages"))
        var PostTtyp = GetRequest().PostTtyp;
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
                        $("#CourceList").empty();
                        $($.parseJSON(returnVal.Data)).each(function () {

                            var html = " <img src=" + this.Attachments + " alt=''><ul class='Register_detial'><li class='ali'><h2>" + this.Title + "</h2><a onclick='quxiao(" + this.ID
                                + ")'  style='cursor:pointer;'>取消报名</a></li><li><h3>" +
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
    function CourseDetail() {
        var FirstUrl = window.location.href;
        FirstUrl = FirstUrl.substring(0, FirstUrl.indexOf("SitePages"))
        var CourseID = GetRequest().CourseID;
        var PostTtyp = GetRequest().PostTtyp;
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

                        if (PostTtyp == "1") {
                            a = "<a onclick='SingUp(" + this.ID + ",'" + this.WeekName + "')' class='Cancel_registration'>报名课程</a>";
                        }
                        if (PostTtyp == "2") {
                            a = "<a onclick='quxiao(" + this.ID + ")' class='Cancel_registration'>取消课程</a>";
                        }
                        if (PostTtyp == "0") { a = ""; }
                        //}
                        var ul = "<img src=" + this.Attachments + " alt=''><div class='music_nr'><h2>" + this.Title + "</h2><div><span>主讲教师：" + this.TeacherXM
                            + "</span><span>上课时间：" + this.WeekName + " </span><span>上课地点：" + this.AddressID + "</span></div><p>" + this.Introduc
                            + "</p><div>" + a + "</div></div>";
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
        var PostTtyp = GetRequest().PostTtyp;

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
                        var li = "<li><a onclick='Down(\"" + this.Url + "\")'><i class='iconfont'><img src='" + this.Image + "'></i>" + this.Title + "</a></li>";
                        $("#Data").append(li);

                    });
                }
            },
            error: function (errMsg) {
                alert('数据加载失败！');
            }
        });
    }
    function Down(url) {
        window.open(url);
    }
    //绑定任务
    function BindTask() {
        var FirstUrl = window.location.href;
        FirstUrl = FirstUrl.substring(0, FirstUrl.indexOf("SitePages"));
        var CatagoryID = $("#CatagoryID").val();
        var CourseID = GetRequest().CourseID;
        var PostTtyp = GetRequest().PostTtyp;

        $.ajax({
            type: "Post",
            url: FirstUrl + "/_layouts/15/SVDigitalCampus/hander/Caurse.aspx",
            data: { Func: "GetTask", CourseID: CourseID, ContentID: CatagoryID, PostTtyp: PostTtyp },
            dataType: "text",
            success: function (returnVal) {
                returnVal = $.parseJSON(returnVal);
                if (returnVal != null) {
                    $("#Task").empty();
                    $($.parseJSON(returnVal.Data)).each(function () {
                        var a = "";
                        if (PostTtyp == "2") {
                            IsExam(this.ID);
                            if ($("#hdExampaperID").val().length > 0) {
                                a = "<a class='task' onclick='ViewCheck(" + $("#hdExampaperID").val() + ")' style='cursor: pointer;'>查看批阅</a>";

                            }
                            else {
                                a = "<a class='task' onclick='SumitTask(" + this.ID + ")' style='cursor: pointer;'>提交作业</a>";
                            }
                        }
                        var i = "";
                        if (PostTtyp == "0") {
                            i = "<i onclick='ShowSubmitTask(this)'>+</i>";
                        }
                        var li = "<li><div class='list_kctitle'><p>" + this.Title
                        + "</p>" + i + "<span class='rightbtn fr'>" + a + "</span></div><ol> " + this.TaskSubMit
                        "</ol></li>";

                        $("#Task").append(li);
                    });
                }
            },
            error: function (errMsg) {
                alert('数据加载失败！');
            }
        });
    }
    function IsExam(ExampaperID) {
        var FirstUrl = window.location.href;
        FirstUrl = FirstUrl.substring(0, FirstUrl.indexOf("SitePages"))
        //var CourseID = GetRequest().CourseID;

        $.ajax({
            type: "Post",
            url: FirstUrl + "/_layouts/15/SVDigitalCampus/hander/Caurse.aspx",
            data: { Func: "IsSubmintTask", ExampaperID: ExampaperID },
            async: false,
            dataType: "text",
            success: function (returnVal) {
                $("#hdExampaperID").val(returnVal);
            },
            error: function (errMsg) {
                alert('数据加载失败！');
            }
        });
    }
    function ShowSubmitTask(em) {
        $(".kc_zuoye").find("ul li ol").hide();
        $(".kc_zuoye").find("ul li div i").html("+");
        $(em).html($(em).html() == "+" ? "-" : "+");
        if ($(em).html() == "-") {
            $(em).parent().next("ol").show();
        }
        else {
            $(em).parent().next("ol").hide();
        }
    }
    //提交作业
    function SumitTask(id) {
        GetStuID();
        if ($("#CurrenID").val().length > 0) {
            var url = "<%=rootUrl%>" + "/TaskBase/SitePages/AnswerQuestion.aspx?ExamID=" + id + "&StuID=" + $("#CurrenID").val() + "&" + Math.random;
            $.webox({
                height: 700, width: 600, bgvisibel: true, title: '提交作业', iframe: url
            });
            //popWin.showWin("700", "700", "提交作业", url, "auto");
        }
    }
    function GetStuID() {
        var FirstUrl = window.location.href;
        FirstUrl = FirstUrl.substring(0, FirstUrl.indexOf("SitePages"))
        $.ajax({
            type: "Post",
            url: FirstUrl + "/_layouts/15/SVDigitalCampus/hander/Caurse.aspx",
            data: { Func: "GetCurrenID" },
            dataType: "text",
            async: false,
            success: function (returnVal) {
                $("#CurrenID").val(returnVal);
            },
            error: function (errMsg) {
            }
        });
    }
    function ViewCheck(id) {
        var url = "<%=rootUrl%>" + "/TaskBase/SitePages/ExamAnalysis.aspx?ExamID=" + id + "&" + Math.random;
        $.webox({
            height: 700, width: 700, bgvisibel: true, title: '试卷分析', iframe: url
        });
    }
    //检查作业
    function CheckTask(id) {
        var url = "<%=rootUrl%>" + "/TaskBase/SitePages/MarkSubQ.aspx?ExamID=" + id + "&" + Math.random;
        $.webox({
            height: 700, width: 700, bgvisibel: true, title: '检查作业', iframe: url
        });
        //popWin.showWin("700", "700", "检查作业", url, "auto");
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
    //资料、任务切换
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
    //课程报名
    function SingUp(WeekName) {

        var CourseID = GetRequest().CourseID;
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
                }
                else {
                    if (returnVal == "2") {
                        alert("每个学生只能选修两门课程！");
                    }
                    else {
                        alert("报名失败");
                    }
                }
            },
            error: function (errMsg) {
            }
        });

    }
    //取消报名
    function quxiao(id) {
        var FirstUrl = window.location.href;
        FirstUrl = FirstUrl.substring(0, FirstUrl.indexOf("SitePages"))
        $.ajax({
            type: "Post",
            url: FirstUrl + "/_layouts/15/SVDigitalCampus/hander/Caurse.aspx",
            data: { Func: "SingOut", CourseID: id },
            dataType: "text",
            success: function (returnVal) {
                if (returnVal == "1") {
                    alert("取消成功");
                    GetMyCourse();
                }
                else if (returnVal == "2") {
                    alert("已开课不能取消");
                }
                else {
                    alert("取消失败");
                }
            },
            error: function (errMsg) {
            }
        });

    }
    //头部导航数据绑定
    function Navtip() {
        var PostUrl = GetRequest().PostUrl;
        var FirstUrl = window.location.href;
        var CourseID = GetRequest().CourseID;
        var PostTtyp = GetRequest().PostTtyp;

        FirstUrl = FirstUrl.substring(0, FirstUrl.indexOf("SitePages"))

        if (PostUrl == "Mycource") {
            var html = "<a href='" + FirstUrl + "/SitePages/RC_wp_MyCourse.aspx'>我的课程</a> > <a href='" + FirstUrl + "/SitePages/RC_wp_CourseDetail.aspx?CourseID=" + CourseID + "&PostTtyp=" + PostTtyp + "'>课程详细信息</a>";
            $(".navgition").append(html);
        }
        else {
            var html = "<a href='" + FirstUrl + "/SitePages/RC_wp_CourseHome.aspx'>校本课程</a> > <a href='" + FirstUrl + "/SitePages/RC_wp_RegistCourse.aspx'>报名选课</a> > <a href='" + FirstUrl + "/SitePages/RC_wp_CourseDetail.aspx?CourseID=" + CourseID + "&PostTtyp=" + PostTtyp + "'>课程详细信息</a>";
            $(".navgition").append(html);
        }
    }

</script>
<div class="course_detial">
    <div class="kc_choose">
        <input id="CatagoryID" type="hidden" />
        <input id="MyCourseID" type="hidden" value="," />
        <input id="CurrenID" type="hidden" />

        <input id="hdExampaperID" type="hidden" />

        <div class="navgition"></div>

        <div class="kc_day" id="WeekTwo">周二/周四课程</div>
        <div class="kc_day" id="WeekThird">周三课程</div>
    </div>
    <div class="my_kecheng">
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
                <dd id="ddTask" style="display: none;" class="kc_zuoye">
                    <ul id="Task">
                    </ul>
                </dd>
            </dl>
        </div>

    </div>
</div>
