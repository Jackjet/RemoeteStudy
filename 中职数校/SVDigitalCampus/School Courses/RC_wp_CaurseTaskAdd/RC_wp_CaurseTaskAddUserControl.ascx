<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RC_wp_CaurseTaskAddUserControl.ascx.cs" Inherits="SVDigitalCampus.School_Courses.RC_wp_CaurseTaskAdd.RC_wp_CaurseTaskAddUserControl" %>
<link href="../../../../_layouts/15/SVDigitalCampus/CSS/common.css" rel="stylesheet" />
<link href="../../../../_layouts/15/SVDigitalCampus/CSS/style.css" rel="stylesheet" />
<link href="../../../../_layouts/15/SVDigitalCampus/CSS/iconfont.css" rel="stylesheet" />
<link href="../../../../_layouts/15/SVDigitalCampus/CSS/animate.css" rel="stylesheet" />
<script src="../../../../_layouts/15/SVDigitalCampus/Script/jquery-1.8.2.min.js"></script>
<link href="../../../../_layouts/15/SVDigitalCampus/Script/Enter/jquery-webox.css" rel="stylesheet" />
<script src="../../../../_layouts/15/SVDigitalCampus/Script/Enter/jquery-webox.js"></script>
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

            var tr = "<tr class='J-item Single'><td class='Check'><input type='checkbox' value='" + this.ID
                + "' class='Check_box' name='Check_box'  id='subcheck' onclick='setSelectAll()'/></td><td class='name' style='text-align: left;'><span style=\"cursor:pointer\">" + this.Title +
                "</span><div class=\"Operation\" style=\"display:none;\">" +
                "<span class=\"more\"><a href=\"#\" class=\"more_m\"><i class=\"iconfont\">&#xe61a;</i></a>" +
                "<ul class=\"M_con\" style=\"display:none;\"><li><a href=\"#\" onclick=\"DelTask('" + this.ID + "')\">删除</a></li>"
                + "</ul></span></div>"
                + "</td><td class='Change_date'>" + this.Created + "</td></tr>";
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
        var postData = { Func: "GetTask", PageSize: pSize, PageIndex: index, CourseID: CourseID, ContentID: CatagoryID };
        var FirstUrl = window.location.href;
        FirstUrl = FirstUrl.substring(0, FirstUrl.indexOf("SitePages"))

        $.ajax({
            type: "Post",
            url: FirstUrl + "/_layouts/15/SVDigitalCampus/hander/Caurse.aspx",
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
            url: FirstUrl + "/_layouts/15/SVDigitalCampus/hander/Caurse.aspx",
            data: { Func: "GetTask", PageSize: pSize, PageIndex: pIndex, CourseID: CourseID, ContentID: CatagoryID },
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
        BindNave();
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
        if (Status == "2") {
            //$("#Task").attr("class", "kc_yylc_spzt kc_yylc_spoff");
            $("#Check").html("待审核");
        }
        //else {
        //    $("#Task").attr("class", "kc_yylc_spzt kc_yylc_spon");
        //    $("#Check").attr("class", "kc_yylc_spzt kc_yylc_spon");
        //}
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
    function DelTask(id) {
        var FirstUrl = window.location.href;
        FirstUrl = FirstUrl.substring(0, FirstUrl.indexOf("SitePages"))
        if (confirm("确认删除任务吗")) {
            $.ajax({
                type: "Post",
                url: FirstUrl + "/_layouts/15/SVDigitalCampus/hander/Caurse.aspx",
                data: { Func: "DelTask", "DelID": id },
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
        Bind();
        $("#CatagoryID").val(id);
    }
    //左侧导航点击、鼠标经过事件
    function initTree() {
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


    function Del() {
        $("#div0").remove();
        ////获取当前节点对象
        //var divNode2 = document.getElementById("Tree");
        ////获取文本节点
        //var textNode = divNode2.childNodes[0];
        ////删除文本节点
        //divNode2.removeChild(textNode);
    }
    function EditMajor() {
        var FirstUrl = window.location.href;
        FirstUrl = FirstUrl.substring(0, FirstUrl.indexOf("SitePages"))

        popWin.showWin("600", "500", "修改专业学科", FirstUrl + "/SitePages/EditMajor.aspx", "no");
    }
    function shaixuan() {
        var dis = $(".File_type").css("display");
        if (dis == "none") {
            $(".File_type").show();
        }
        else {
            $(".File_type").hide();
        }
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
    //选题组卷
    function SelTask() {

        var CourseID = GetRequest().CourseID;
        var CatagoryID = $("#CatagoryID").val();
        $.webox({ height: 550, width: 760, bgvisibel: true, title: '选题组卷', iframe: '<%=FirstUrl%>' + '/TaskBase/SitePages/QuestionChoice.aspx?CourseID=' + CourseID + "&CatagoryID=" + CatagoryID });

        //popWin.showWin('760', '550', '选题组卷', '<%=FirstUrl%>' + '/TaskBase/SitePages/QuestionChoice.aspx?CourseID=' + CourseID + "&CatagoryID=" + CatagoryID, "auto");
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

        popWin.showWin("800", "570", "修改课程信息", FirstUrl + "/SitePages/CouseEdit.aspx?CourseID=" + CourseID, "auto");
    }
    //提交审核
    function SubMit() {
        if ($("#Check").html() == "提交审核") {

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
                        $("#Status").val("2");
                        $("#Check").val("待审核");
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
        else { alert("等待审核") }
    }
    function Return() {
        var FirstUrl = window.location.href;
        FirstUrl = FirstUrl.substring(0, FirstUrl.indexOf("SitePages"));

        window.location.href = FirstUrl + "/SitePages/CaurseManage.aspx";
    }
    function SelData() {
        var FirstUrl = window.location.href;
        FirstUrl = FirstUrl.substring(0, FirstUrl.indexOf("SitePages"));
        var CourseID = GetRequest().CourseID;
        var Title = GetRequest().Title
        window.location.href = FirstUrl + "/SitePages/CauseData.aspx?CourseID=" + CourseID + "&Title=" + Title + "&Status=" + $("#Status").val();
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

                        <td  class="kc_yylc_sptd"><a onclick="Return()" class="kc_yylc_spzt kc_yylc_spon"><<返回</a></td>
                        <td class="kc_yylc_sptd">
                            <a id="BaseData" class="kc_yylc_spzt kc_yylc_spon" onclick="BaseData()">基础资料</a>
                        </td>
                        <td class="kc_yylc_sptd">
                            <a id="SelData" class="kc_yylc_spzt kc_yylc_spon" onclick="SelData()">选取资源</a>
                        </td>
                        <td class="kc_yylc_sptd">
                            <a id="Task" class="kc_yylc_spzt kc_yylc_spon" onclick="">创建任务</a>
                        </td>
                        <td class="kc_yylc_sptd">
                            <a id="Check" class="kc_yylc_spzt kc_yylc_spon" onclick="SubMit()">提交审核</a>
                            <div class="jujue" visible='false' id="weitongguo" style="display:none"><span></span></div>

                            <%--<div class="weitongguo" runat="server" visible='<%#Eval("Status").ToString()=="2"?true:false %>' id="weitongguo"><span></span></div>--%>
                        </td>
                        <%-- <td class="kc_yylc_sptd"><a class="kc_yylc_spzt kc_yylc_spcc" href="">待审核</a></td>
                                                                            <td class="kc_yylc_sptd"><a class="kc_yylc_spzt kc_yylc_spoff" href="">审核通过</a></td>--%>
                        <td class="kc_yylc_sptd">
                            <a id="Room" class="kc_yylc_spzt kc_yylc_spoff">预约教室</a>
                            
                        </td>
                        <td class="kc_yylc_sptd">
                            <a id="CheckStu" class="kc_yylc_spzt kc_yylc_spoff">审核报名</a>
                        </td>
                        <td class="kc_yylc_sptd">
                            <a id="OpenClass" class="kc_yylc_spzt kc_yylc_spoff">开课</a>
                        </td>
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
                    </h3>
                <div class="select-box" id="Tree">
                </div>
            </div>
            <div class="right_dcon fr">
                <!--操作区域-->
                <div class="Operation_area">

                    <div class="right_add fr" id="opertion" style="display: block">
                        <%--<a href="#" class="add" onclick="SelTask()"><i class="iconfont">&#xe65a;</i>选择试卷</a>--%>
                        <a href="#" class="add" onclick="SelTask()"><i class="iconfont">&#xe65a;</i>选题组卷</a>
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
                                        <h1>您尚未添加任何试题</h1>
                                        <h2>点击<a onclick="SelTask()">+选题组卷</a>进行添加吧！</h2>
                                    </div>
                                    <table class="O_form" id="tab" style="display: none">
                                        <thead>
                                            <tr class="O_trth">
                                                <!--表头tr名称-->
                                                <th class="Check">
                                                    <input type="checkbox" class="Check_box" id="checkAll" /></th>
                                                <th class="name">试卷名称 </th>
                                                <th class="Change_date">创建时间 </th>
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
