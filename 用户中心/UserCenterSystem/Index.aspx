<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="UserCenterSystem.Index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link type="text/css" href="css/page.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div id="main-content" style="height: 70%;">
            <!-- Main Content Section with everything -->

            <!-- Page Head -->
            <div class="nav cf">

                <h2>用户管理</h2>
                <ul class="shortcut-buttons-set">
                    <li class="col_g"><a href="TeacherList.aspx" class="shortcut-button"><span>培训师资管理
                    </span></a></li>
                    <li class="col_y"><a href="StudentList.aspx" class="shortcut-button"><span>学员档案管理
                    </span></a></li>
                    <%--<li class="col_z"><a href="ParentList.aspx" class="shortcut-button"><span>家长信息管理
                    </span></a></li>--%>
                    <li class="col_z"><a href="AccountManage.aspx" class="shortcut-button"><span>账号管理
                    </span></a></li>
                    <li class="col_b"><a href="TeaStuList.aspx" class="shortcut-button"><span>师生名单
                    </span></a></li>

                </ul>
            </div>
            <div class="nav cf">
                <h2>初始配置</h2>
                <ul class="shortcut-buttons-set">

                    <li class="col_g"><a href="DepartmentManage.aspx" class="shortcut-button"><span>组织机构管理
                    </span></a></li>
                    <li class="col_y"><a href="GradeManage.aspx" class="shortcut-button"><span>专业管理
                    </span></a></li>
                    <li class="col_z"><a href="ClassManage.aspx" class="shortcut-button"><span>班级管理
                    </span></a></li>
                    <li class="col_g"><a href="SubjectInfoManage.aspx" class="shortcut-button"><span>学科管理
                    </span></a></li>
                    <%--<li class="col_y"><a href="AcademicSemesterManage.aspx" class="shortcut-button"><span>学年学期管理 
                    </span></a></li>--%>
                    <%--   <li class="col_b"><a href="RoleManageList.aspx" class="shortcut-button"><span>角色管理
                    </span></a></li>--%>
                    <%--<li class="col_b"><a href="TeamManage.aspx" class="shortcut-button"><span>教研组管理
                    </span></a></li>--%>
                    <%--<li class="col_y"><a href="SpecialtyManage.aspx" class="shortcut-button"><span>专业信息
                    </span></a></li>--%>
                    <li class="col_g"><a href="CultivateArchivesManage.aspx" class="shortcut-button"><span>培训档案管理
                    </span></a></li>
                    <li class="col_b"><a href="TrainingFacilities.aspx" class="shortcut-button"><span>培训设施管理
                    </span></a></li>
                </ul>
            </div>
            <%--<div class="nav cf" id="InfoManage" runat="server" visible="true">
                <h2>接口管理</h2>
                <ul class="shortcut-buttons-set">
                    <li class="col_g"><a href="SystemConfigManager.aspx" class="shortcut-button"><span>系统账号管理
                    </span></a></li>
                    <li class="col_y"><a href="InterfaceManager.aspx" class="shortcut-button"><span>接口信息管理
                    </span></a></li>
                    <li class="col_z"><a href="InterfaceConfigManager.aspx" class="shortcut-button"><span>接口权限管理
                    </span></a></li>
                </ul>
            </div>--%>
            <div class="nav cf">
                <h2>系统管理</h2>
                <ul class="shortcut-buttons-set">
                    <li class="col_g"><a href="authorize_ChangePassWord.aspx" class="shortcut-button"><span>修改密码管理
                    </span></a></li>
                    <li class="col_y"><a href="operationLogManager.aspx" class="shortcut-button"><span>操作日志
                    </span></a></li>
                    <li class="col_z" runat="server"  ><a href="RegistDetail.aspx" class="shortcut-button"><span>教师注册统计
                    </span></a></li>
                </ul>
            </div>
        </div>
    </form>
</body>
</html>
