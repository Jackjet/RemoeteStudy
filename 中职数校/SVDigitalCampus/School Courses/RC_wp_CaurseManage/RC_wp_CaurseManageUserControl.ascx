<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RC_wp_CaurseManageUserControl.ascx.cs" Inherits="SVDigitalCampus.School_Courses.RC_wp_CaurseManage.RC_wp_CaurseManageUserControl" %>
<link href="../../../../_layouts/15/SVDigitalCampus/CSS/common.css" rel="stylesheet" />
<link href="../../../../_layouts/15/SVDigitalCampus/CSS/style.css" rel="stylesheet" />
<link href="../../../../_layouts/15/SVDigitalCampus/CSS/iconfont.css" rel="stylesheet" />
<link href="../../../../_layouts/15/SVDigitalCampus/CSS/animate.css" rel="stylesheet" />
<script src="../../../../_layouts/15/SVDigitalCampus/Script/jquery-1.8.2.min.js"></script>
<link href="../../../../_layouts/15/SVDigitalCampus/Script/Enter/jquery-webox.css" rel="stylesheet" />
<script src="../../../../_layouts/15/SVDigitalCampus/Script/Enter/jquery-webox.js"></script>
<script src="../../../../_layouts/15/SVDigitalCampus/Script/Enter/tz_slider.js"></script>
<script src="../../../../_layouts/15/SVDigitalCampus/Script/Enter/uploadFile.js"></script>

<script src="../../../../_layouts/15/SVDigitalCampus/Script/Enter/popwin.js"></script>
<script type="text/javascript">
    function Add() {
        $.webox({
            height: 620,
            width: 800,
            bgvisibel: true,
            title: '新建课程',
            iframe: '<%=rootUrl%>' + "/SitePages/CaurseAdd.aspx?" + Math.random
        });
        //popWin.showWin('800', '720', '新建课程', '<%=rootUrl%>' + "/SitePages/CaurseAdd.aspx", 'no')
    }
</script>

<div class="School_library">
    <!--页面名称-->
    <%--<h1 class="Page_name">课程管理</h1>--%>
    <!--整个展示区域-->
    <div class="Whole_display_area">
        <div>
            <!--操作区域-->

            <div class="clear"></div>

            <div class="clear"></div>
            <div class="Operation_area">
                <div class="left_choice fl">
                    <ul>
                        <li class="State">
                            <span class="qijin">
                                <asp:LinkButton ID="Course" runat="server" CssClass="Enable" ForeColor="White">我的课程</asp:LinkButton>
                                <asp:LinkButton ID="lbLibrary" runat="server" CssClass="Disable" ForeColor="White" OnClick="lbLibrary_Click" >历史课程</asp:LinkButton>
                                <asp:LinkButton ID="lbTask" runat="server" CssClass="Disable" ForeColor="White" OnClick="lbTask_Click">任务库</asp:LinkButton>

                            </span>
                        </li>

                    </ul>
                </div>

            </div>
            <!--展示区域-->
            <div class="Display_form">
                <div class="Resources_tab">

                    <div class="Operation_area" style="margin-top: 10px;">
                        <div class="left_choice fl">
                            <%--<ul>
                                <li class="Sear">
                                    <asp:DropDownList ID="DplistStatus" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DplistStatus_SelectedIndexChanged">
                                        <asp:ListItem Value="-1">全部状态</asp:ListItem>
                                        <asp:ListItem Value="0">未传资料</asp:ListItem>
                                        <asp:ListItem Value="1">未分配时间和教室</asp:ListItem>
                                        <asp:ListItem Value="2">待审核</asp:ListItem>
                                        <asp:ListItem Value="3">审核失败</asp:ListItem>
                                        <asp:ListItem Value="4">待开课</asp:ListItem>
                                        <asp:ListItem Value="5">已开课</asp:ListItem>
                                        <asp:ListItem Value="6">已停课</asp:ListItem>
                                    </asp:DropDownList>
                                </li>
                                <li class="Sear">
                                    <input type="text" placeholder=" 请输入课程名称" class="search" name="search" id="searchName" runat="server" />
                                    <i class="iconfont">
                                        <asp:LinkButton ID="lbsearch" runat="server" OnClick="lbsearch_Click">&#xe62d;</asp:LinkButton></i>
                                </li>
                            </ul>--%>
                        </div>
                        <div class="right_add fr" id="opertion">
                            <a class="add" onclick="Add()" style="cursor: pointer;"><i class="iconfont">&#xe630;</i>创建课程</a>
                        </div>
                    </div>
                    <div class="content">
                        <div class="tc">
                            <div class="Order_form">
                                <div class="Food_order">
                                    <asp:ListView ID="LV_TermList" runat="server" OnItemCommand="LV_TermList_ItemCommand" OnItemEditing="LV_TermList_ItemEditing" OnItemDataBound="LV_TermList_ItemDataBound">
                                        <EmptyDataTemplate>
                                            <table style="width: 100%">
                                                <tr>
                                                    <td>
                                                        <div class="kc_create">
                                                            <h1>您尚未创建任何课程</h1>
                                                            <h2>点击<a onclick="Add()" style="cursor: pointer;">+创建课程</a>进行创建吧！</h2>
                                                        </div>
                                                    </td>
                                                </tr>
                                            </table>
                                        </EmptyDataTemplate>
                                        <LayoutTemplate>
                                            <table id="itemPlaceholderContainer" style="width: 100%;" class="">
                                                <%--<tr class="trth">
                                                    <th>序号</th>
                                                    <th>课程名称</th>
                                                    <th>上课时间</th>
                                                    <th>主讲老师</th>
                                                    <th>课程状态</th>
                                                    <th>操作</th>
                                                </tr>--%>
                                                <tr id="itemPlaceholder" runat="server"></tr>
                                            </table>
                                        </LayoutTemplate>
                                        <ItemTemplate>
                                            <%--<tr class="Single">
                                                <td><%# Container.DataItemIndex + 1%></td>
                                                <td><%#Eval("Title")%></td>
                                                <td><%#Eval("BeginTime")%></td>
                                                <td><%#Eval("TeacherName")%></td>
                                                <td><%#Eval("StatusName")%></td>
                                                <td>
                                                    <a href="#" class="add" onclick="Edit('<%#Eval("ID")%>')">编辑</a>
                                                    <asp:LinkButton ID="Task" runat="server" CommandArgument='<%#Eval("ID") %>' CommandName="Task">作业</asp:LinkButton>
                                                    <asp:LinkButton ID="View" runat="server" CommandArgument='<%#Eval("ID") %>' CommandName="View">审核</asp:LinkButton>

                                                </td>

                                            </tr>--%>
                                            <tr>
                                                <td>

                                                    <div class="kc_yylc">
                                                        <div class="kc_yylc_xq">
                                                            <img src='<%#Eval("ImageUrl") %>' alt="">
                                                            <h2>
                                                                <asp:Label ID="lbTitle" runat="server" Text='<%#Eval("Title") %>'></asp:Label>
                                                                <%--<asp:LinkButton ID="LinkButton2" runat="server" OnClientClick="Edit()">修改</asp:LinkButton>--%>
                                                            </h2>
                                                            <div class="tit_course"><span>主讲教师：<%#Eval("TeacherName")%></span></div>
                                                            <p><%#Eval("Introduc") %></p>
                                                        </div>
                                                        <div class="kc_yylc_sp">
                                                            <div class="kc_yylc_spxian"></div>
                                                            <div class="kc_yylc_spbiao">
                                                                <table>
                                                                    <tbody>
                                                                        <tr>
                                                                            <asp:Label ID="lbStatus" runat="server" Text='<%#Eval("Status") %>' Visible="false"></asp:Label>
                                                                            <td class="kc_yylc_sptd">
                                                                                <asp:LinkButton ID="lbBaseData" CssClass="kc_yylc_spzt kc_yylc_spon" runat="server" CommandArgument='<%#Eval("ID") %>' CommandName="BaseData">基础资料</asp:LinkButton>
                                                                            </td>
                                                                            <td class="kc_yylc_sptd">
                                                                                <asp:LinkButton ID="lbSelData" CssClass="kc_yylc_spzt kc_yylc_spon" runat="server" CommandArgument='<%#Eval("ID") %>' CommandName="SelData">选取资源</asp:LinkButton>
                                                                            </td>
                                                                            <td class="kc_yylc_sptd">
                                                                                <asp:LinkButton ID="lbTask" runat="server" CssClass="kc_yylc_spzt kc_yylc_spoff"  CommandArgument='<%#Eval("ID") %>' CommandName="AddTask">创建任务</asp:LinkButton>
                                                                            </td>
                                                                            <td class="kc_yylc_sptd">
                                                                                <asp:LinkButton ID="lbCheck" CssClass="kc_yylc_spzt kc_yylc_spoff" runat="server" CommandArgument='<%#Eval("ID") %>' CommandName="SubMit">提交审核</asp:LinkButton>
                                                                                <div class="weitongguo" runat="server" visible='<%#Eval("Status").ToString()=="3"?true:false %>' id="weitongguo"><span></span><i>未通过</i></div>
                                                                            </td>
                                                                            <%-- <td class="kc_yylc_sptd"><a class="kc_yylc_spzt kc_yylc_spcc" href="">待审核</a></td>
                                                                            <td class="kc_yylc_sptd"><a class="kc_yylc_spzt kc_yylc_spoff" href="">审核通过</a></td>--%>
                                                                            <td class="kc_yylc_sptd">
                                                                                <asp:LinkButton ID="lbRoom" runat="server" CssClass="kc_yylc_spzt kc_yylc_spoff" CommandName="SelRoom" CommandArgument='<%#Eval("ID") %>'>预约教室</asp:LinkButton>
                                                                                <div class="jujue" style="display: none">
                                                                                    <span></span>
                                                                                    <p>拒绝理由拒绝理由</p>
                                                                                </div>
                                                                            </td>
                                                                            <td class="kc_yylc_sptd">
                                                                                <asp:LinkButton ID="lbCheckStu" runat="server" CssClass="kc_yylc_spzt kc_yylc_spoff" CommandName="CheckStu" CommandArgument='<%#Eval("ID") %>'>审核报名</asp:LinkButton>
                                                                            </td>
                                                                            <%--<td class="kc_yylc_sptd">
                                                                                <asp:LinkButton ID="lbOpenClass" runat="server" CssClass="kc_yylc_spzt kc_yylc_spoff" CommandArgument='<%#Eval("ID") %>' CommandName="Kaike">开课</asp:LinkButton>
                                                                            </td>--%>
                                                                        </tr>
                                                                    </tbody>
                                                                </table>
                                                            </div>
                                                        </div>
                                                    </div>

                                                </td>
                                            </tr>
                                        </ItemTemplate>

                                    </asp:ListView>

                                </div>

                            </div>
                        </div>
                    </div>

                </div>
            </div>
        </div>
    </div>

</div>