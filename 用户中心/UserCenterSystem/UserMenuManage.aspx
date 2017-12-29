<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserMenuManage.aspx.cs" Inherits="UserCenterSystem.UserMenuManage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>信息中心</title>
    <link rel="stylesheet" type="text/css" href="css/base.css" />
    <link rel="stylesheet" type="text/css" href="css/frame.css" />
    <script src="Scripts/jquery-1.8.3.min.js"></script>
    <script src="Scripts/layer/layer.min.js"></script>
    <!--子切换标签 -->
    <script type="text/javascript" src="Scripts/ui.core.js"></script>
    <script type="text/javascript" src="Scripts/ui.tabs.js"></script>
    <!--menu.js是网页设计书写的页面效果方面的动作，使用jquery书写 -->
    <script type="text/javascript" src="Scripts/menu.js"></script>
    <script type="text/javascript" src="Scripts/table.js"></script>

    <script type="text/javascript">
        $(document).ready(function () {
            $('#content').attr("src", '/Index.aspx');
            //关闭网站
            //$("#shutdown").click(function () {
            //    if (confirm("确定要关闭网站吗？")) {
            //        window.opener = null;
            //        window.open('', '_self');
            //        window.close();
            //    }
            //})
        })//ERP-PersonCenter.html

        function LoadMenu(pId) {
            $('#content').attr("src", '/Index.aspx');
            $("#OneMenu").text("");
            $("#TwoMenu").text("");
        }

        function fraload(uid, url, fid, id) {

            if (fid == undefined || id == undefined) {
            } else {

                $("#OneMenu").text($("#One" + fid).text());
                $("#TwoMenu").text($("#Two" + id).text());
            }

            if (url.indexOf("?") == -1)
                $("#" + uid).attr("src", encodeURI(url) + "?_" + (new Date().getTime()));
            else
                $("#" + uid).attr("src", encodeURI(url) + "&_" + (new Date().getTime()));

        }
        //$.ajax({
        //    url: "UserMenuManage.aspx",
        //    type: "post",
        //    contentType: "application/json;charset=utf-8",
        //    success: function(msg) {
        //        if(msg)
        //        {
        //            document.getElementById("InfoManage").style.visibility="hidden";
        //        }
        //    } 

        //});


        function alertWin() {

            $.layer({
                type: 1,
                offset: ['100px', ''],
                title: '教师导入',
                area: ['auto', 'auto'],
                shadeClose: true,
                moveOut: false,
                border: [0], //去掉默认边框
                shift: "top", //从左动画弹出
                page: { dom: '#winDiv' }
            });

        }
        function iFrameHeight() {
            var iframe = document.getElementById("content");
            var iframeDocument = null;
            //safari和chrome都是webkit内核的浏览器，但是webkit可以,chrome不可以
            if (iframe.contentDocument) {
                //ie 8,ff,opera,safari
                iframeDocument = iframe.contentDocument;
            }
            else if (iframe.contentWindow) {
                // for IE, 6 and 7:
                iframeDocument = iframe.contentWindow.document;
            }
            if (!!iframeDocument) {
                iframe.width = iframeDocument.documentElement.scrollWidth + "px";
                iframe.height = iframeDocument.documentElement.scrollHeight + "px";
            } else {
                alert("this browser doesn't seem to support the iframe document object");
            }
        }

    </script>
</head>
<body class="b_index">
    <form runat="server">
        <!--头部开始 -->
        <div class="erp-top">
            <div class="logo-block fr"></div>
            <div style="float: right; width: 350px; height: 100%;">
                <div class="info-div fl" style="padding: 20px; width: 290px;">
                    <p>欢迎您：<asp:Literal ID="lblDepartment" runat="server"></asp:Literal>&nbsp;<asp:Literal ID="lblUserName" runat="server"></asp:Literal></p>
                </div>
            </div>
        </div>
        <!--头部结束 -->
        <div class="breadcrumb-block">
            <!--左侧面包屑导航开始 -->
            <div class="fl block-l" id="js-menu-sub2"><a class="breadcrumb-a" href="javascript:void(0);" onclick="LoadMenu('')"><span></span>首页公告</a> <a class="breadcrumb-a" id="OneMenu"></a><a id="TwoMenu"></a></div>
            <!--左侧面包屑导航结束 -->
            <!--右侧相关链接开始 -->
            <div class="fr block-r">
                <asp:Button ID="LogOff" runat="server" Text="注销" Style="border: none; line-height: 28px; margin-right: 10px; cursor: pointer;" OnClick="LogOff_Click" OnClientClick="return confirm('确定要注销吗?')" />
            </div>
            <!--右侧相关链接结束 -->
        </div>
        <div class="content clearfix">
            <!--左侧菜单开始 -->
            <div class="con-l" id="js-menu">
                <!--用户信息开始 -->
                <div class="uesr-info-block">
                    <!--用户信息-s -->
                    <div class="ui-con pr" id="js-userinfo" style="height: 20px;">
                        <div class="box-pad">
                        </div>
                        <a href="javascript:void(0);" class="control-user-info" id="js-userinfo-control"></a>
                    </div>
                    <!--用户信息-e -->
                </div>
                <!--用户信息结束 -->
                <div class="sidebar">
                    <ul class="side-menu">
                        <li class="haschild"><a href="javascript:void(0);" id="OneYYPTSysMenu01">用户管理 </a>
                            <ul class="childmenu none">
                                <li><a href="javascript:void(0);" onclick="fraload('content','TeacherList.aspx','YYPTSysMenu01','YYPTUserAListMenu')"
                                    id="TwoYYPTUserAListMenu">培训师资管理</a> </li>
                                <li><a href="javascript:void(0);" onclick="fraload('content','StudentList.aspx','YYPTSysMenu01','YYPTUserReviewListMenu')"
                                    id="TwoYYPTUserReviewListMenu">学员档案管理</a> </li>
                                <%--<li><a href="javascript:void(0);" onclick="fraload('content','ParentList.aspx','YYPTSysMenu01','YYPTRoleUserMenu')"
                                    id="TwoYYPTRoleUserMenu">家长信息管理</a> </li>--%>
                                <li><a href="javascript:void(0);" onclick="fraload('content','AccountManage.aspx','YYPTSysMenu01','YYPTTAMMenu')"
                                    id="TwoYYPTTAMListMenu">账号管理</a> </li>
                                <li><a href="javascript:void(0);" onclick="fraload('content','TeaStuList.aspx','YYPTSysMenu01','YYPTTAMMenu1111')"
                                    id="TwoYYPTTAMListMenu111">师生名单</a> </li>
                            </ul>

                        </li>
                        <li class="haschild"><a href="javascript:void(0);" id="OneYYPTSysMenu04">初始配置 </a>
                            <ul class="childmenu none">
                                <li><a href="javascript:void(0);" onclick="fraload('content','DepartmentManage.aspx','YYPTSysMenu04','YYPTDepMenuListMenu')"
                                    id="TwoYYPTDepMenuListMenu">组织机构管理</a> </li>
                                <li><a href="javascript:void(0);" onclick="fraload('content','GradeManage.aspx','YYPTSysMenu04','YYPTRoleMenuListMenu')"
                                    id="TwoYYPTRoleMenuListMenu">专业管理</a> </li>
                                <li><a href="javascript:void(0);" onclick="fraload('content','ClassManage.aspx','YYPTSysMenu04','YYPTClassMenuListMenu')"
                                    id="TwoYYPTClassMenuListMenu">班级管理</a> </li>
                                <li><a href="javascript:void(0);" onclick="fraload('content','SubjectManage.aspx','YYPTSysMenu04','YYPTSubInfoMenuListMenu')"
                                    id="TwoYYPTSubInfoMenuListMenu">学科信息</a> </li>
                                <li><a href="javascript:void(0);" onclick="fraload('content','SubjectInfoManage.aspx','YYPTSysMenu04','YYPTSubjectMenuListMenu')"
                                    id="TwoYYPTSubjectMenuListMenu">学科管理</a> </li>

                                <%--<li class="col_b"><a href="javascript:void(0);" onclick="fraload('content','AcademicSemesterManage.aspx','YYPTSysMenu04','YYPTAcademicSemesterMenuListMenu')"
                                    id="TwoYYPTAcademicSemesterMenuListMenu">学年学期管理</a> </li>--%>
                                <%--  <li><a href="javascript:void(0);" onclick="fraload('content','RoleManageList.aspx','YYPTSysMenu04','YYPTNewRoleMenuListMenu')"
                                id="TwoYYPTNewRoleMenuListMenu">角色管理</a> </li>--%>
                                <%--<li><a href="javascript:void(0);" onclick="fraload('content','TeamManage.aspx','YYPTSysMenu04','YYPTTeamMenuListMenu')"
                                    id="TwoYYPTTeamMenuListMenu">教研组管理</a> </li>--%>
                                <%--<li><a href="javascript:void(0);" onclick="fraload('content','SpecialtyManage.aspx','YYPTSysMenu04','YYPTSpecialtyMenuListMenu')"
                                    id="TwoYYPTSpecialtyMenuListMenu">专业信息</a> </li>--%>
                                <li><a href="javascript:void(0);" onclick="fraload('content','CultivateArchivesManage.aspx','YYPTSysMenu04','YYPTSubjectMenuListMenu11')"
                                    id="TwoYYPTSubjectMenuListMenu1">培训档案管理</a> </li>
                                <li><a href="javascript:void(0);" onclick="fraload('content','TrainingFacilities.aspx','YYPTSysMenu04','YYPTSubjectMenuListMenu1122')"
                                    id="TwoYYPTSubjectMenuListMenu2">培训设施管理</a> </li>
                            </ul>
                        </li>

                        <%--<li class="haschild" runat="server" id="InfoManage" visible="true"><a href="javascript:void(0);" id="OneYYPTSysMenu05">接口管理 </a>
                            <ul class="childmenu none">
                                <li><a href="javascript:void(0);" onclick="fraload('content','SystemConfigManager.aspx','YYPTSysMenu05','SystemAccountManager')"
                                    id="SystemAccountManager">系统账号管理</a> </li>
                                <li><a href="javascript:void(0);" onclick="fraload('content','InterfaceManager.aspx','YYPTSysMenu05','InterfaceData')"
                                    id="InterfaceData">接口信息管理</a> </li>
                                <li><a href="javascript:void(0);" onclick="fraload('content','InterfaceConfigManager.aspx','YYPTSysMenu05','InterfaceCompetence')"
                                    id="InterfaceCompetence">接口权限管理</a> </li>
                            </ul>
                        </li>--%>


                        <li class="haschild" runat="server" id="logManage" visible="true"><a href="javascript:void(0);" id="OneYYPTSysMenu06">系统管理 </a>
                            <ul class="childmenu none">
                                <li><a href="javascript:void(0);" onclick="fraload('content','authorize_ChangePassWord.aspx?flag=del','YYPTSysMenu06','pwdManager')"
                                    id="pwdManager"><span>修改密码管理
                                    </span></a></li>
                                <li><a href="javascript:void(0);" onclick="fraload('content','operationLogManager.aspx','YYPTSysMenu06','logManager')"
                                    id="logManager">操作日志管理</a> </li>
                                <li><a href="javascript:void(0);" onclick="fraload('content','RegistDetail.aspx','YYPTSysMenu06','regditManager')"
                                    id="regditManager">教师注册统计</a> </li>
                                <%--         <li><a href="javascript:void(0);" onclick="fraload('content','StatisticAnalysis.aspx','YYPTSysMenu06','StatisticalManager')"
                                id="StatisticalManager">统计分析</a> </li>--%>
                            </ul>
                        </li>

                    </ul>
                </div>
            </div>
            <!--左侧菜单结束 -->
            <div class="con-r" id="js-menu-sub1">
                <%--<iframe src="" name="content" id="content" width="100%" onload="iFrameHeight()" scrolling="no" marginheight="0" marginwidth="0" frameborder="0" allowtransparency="true" ></iframe>--%>
                <iframe src="" name="content" id="content" width="100%" marginheight="0" marginwidth="0" frameborder="0" allowtransparency="true" height="99%" style="overflow-x: hidden; overflow-y: auto;"></iframe>
            </div>
        </div>
        <!--底部网站信息开始 -->
        <div class="bottom clearfix">
            <!--菜单的快捷操作：全部展开，全部合并，登录人信息打开关闭，隐藏菜单 -->
            <div class="control-menu fl">
                <a href="javascript:;" class="ct-menu-bt" id="js-menu-control"></a>
                <a href="javascript:;" title="展开全部菜单" class="open-all fl" id="js-openmenu"></a>
                <a href="javascript:;" title="合并全部菜单" class="close-all fl" id="js-closemenu"></a>
                <!--
<a href="javascript:;" title="打开登录人信息" class="userinfo-open userinfo-open-un fl" id="js-open-userinfo"></a>
-->

            </div>
            信息中心
        </div>
        <!--底部网站信息结束 -->
        <iframe name="iframe_data" id="iframe_data" style="display: none;"></iframe>
        <input type="button" style="display: none; width: 0px; height: 0px;" id="btnfancybox"
            value="fancybox" />

    </form>
</body>
</html>
