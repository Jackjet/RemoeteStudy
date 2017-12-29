<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TS_wp_ManagerProjectListUserControl.ascx.cs" Inherits="Sinp_TeacherWP.WebParts.TS.TS_wp_ManagerProjectList.TS_wp_ManagerProjectListUserControl" %>
<script src="/_layouts/15/Script/jquery-1.8.0.js"></script>
<script src="/_layouts/15/Script/uploadFile.js"></script>
<script src="/_layouts/15/Script/popwin.js"></script>
<link href="/_layouts/15/Style/common.css" rel="stylesheet" />
<link href="/_layouts/15/Style/iconfont.css" rel="stylesheet" />
<link href="/_layouts/15/Style/layout.css" rel="stylesheet" />
<link href="/_layouts/15/Style/tablelist.css" rel="stylesheet" />
<script type="text/javascript">
    function closePages() {
        $("#mask,#maskTop").fadeOut();
    }

</script>
<div class="writingform">
    <div class="writing_title">
        <h2>
            <span class="title_left fl">管理课题信息</span>
            <span class="title_right fr">
                <a id="addCourse" class="add_GL" onclick="openPage('添加课题','/SitePages/AddProject.aspx','700','500');return false;">
                    <i class="iconfont"></i>添加课题</a>
                <%--<a id="addCourse" style="cursor:pointer;padding-right:20px;" href="javascript:void(0)">添加课题</a>--%>
            </span>
        </h2>
    </div>
    <div class="writing_form">
        <div class="searchtit">
            课题名称：
        <asp:TextBox ID="TB_Title" runat="server" CssClass="searinput"></asp:TextBox>
            <asp:Button ID="Btn_Search" runat="server" Text="查询" OnClick="Btn_Search_Click" CssClass="sear" />

        </div>
        <div class="content_list">
            <asp:ListView ID="ProjectList" runat="server" OnPagePropertiesChanging="ProjectList_PagePropertiesChanging" OnItemCommand="ProjectList_ItemCommand" OnItemDataBound="ProjectList_ItemDataBound">
                <EmptyDataTemplate>
                    <table class="emptydate">
                        <tr>
                            <td>
                                <img src="/_layouts/15/TeacherImages/expression.png" /><p>没有找到相关内容</p>
                            </td>
                        </tr>
                    </table>
                </EmptyDataTemplate>
                <LayoutTemplate>
                    <table id="itemPlaceholderContainer" class="W_form">
                        <tr class="trth">
                            <th class="theleft">课题名称
                            </th>
                            <th>课题级别
                            </th>

                            <th>开始时间
                            </th>
                            <th>结束时间
                            </th>
                            <th>课题状态
                            </th>

                            <th>操作
                            </th>
                        </tr>
                        <tr id="itemPlaceholder" runat="server"></tr>
                    </table>
                </LayoutTemplate>
                <ItemTemplate>
                    <tr class="Single">
                        <td class="theleft">
                            <%#Eval("Title") %>
                        </td>
                        <td>
                            <%#Eval("ProjectLevel") %>
                        </td>
                        <td>
                            <%#Eval("StartDate") %>
                        </td>
                        <td>
                            <%#Eval("EndDate") %>
                        </td>
                        <td class="contenttd">
                            <%#Eval("ReleaseStatus") %>
                        </td>
                        <td>
                            <asp:HiddenField ID="DetailID" runat="server" Value='<%# Eval("ID") %>' />
                            <%--<asp:Button ID="look" runat="server" Text="查看" OnClientClick='lookpdetail(this);return false;' CssClass="btn_query" />--%>
                            <asp:Button ID="edit" runat="server" Text="编辑" CommandName="edit" OnClientClick="editpdetail(this,'添加课题','/SitePages/AddProject.aspx?itemid=','700','500');return false;" CssClass="btn_editor" />
                            <asp:Button ID="BtnDelete" runat="server" Text="删除" OnClientClick="return confirm('你确定删除吗？');" CssClass="btn_delete" CommandName="del" CommandArgument='<%# Eval("ID") %>' />
                            <asp:Button ID="ReleaseBtn" runat="server" Text="发布" OnClientClick="return confirm('你确定发布吗？');" CssClass="btn_query" CommandName="release" CommandArgument='<%#Eval("ID") %>' />
                        </td>
                    </tr>
                </ItemTemplate>
                <AlternatingItemTemplate>
                    <tr class="Double">
                        <td class="theleft">
                            <%#Eval("Title") %>
                        </td>
                        <td>
                            <%#Eval("ProjectLevel") %>
                        </td>
                        <td>
                            <%#Eval("StartDate") %>
                        </td>
                        <td>
                            <%#Eval("EndDate") %>
                        </td>
                        <td>
                            <%#Eval("ReleaseStatus") %>
                        </td>
                        <td>
                            <asp:HiddenField ID="DetailID" runat="server" Value='<%# Eval("ID") %>' />
                            <%--<asp:Button ID="look" runat="server" Text="查看" OnClientClick='lookpdetail(this);return false;' CssClass="btn_query" />--%>
                            <asp:Button ID="edit" runat="server" Text="编辑" OnClientClick="editpdetail(this,'添加课题','/SitePages/AddProject.aspx?itemid=','700','500');return false;" CssClass="btn_editor" />
                            <asp:Button ID="BtnDelete" runat="server" Text="删除" OnClientClick="return confirm('你确定删除吗？');" CssClass="btn_delete" CommandName="del" CommandArgument='<%# Eval("ID") %>' />
                            <asp:Button ID="ReleaseBtn" runat="server" Text="发布" OnClientClick="return confirm('你确定发布吗？');" CssClass="btn_query" CommandName="release" CommandArgument='<%#Eval("ID") %>' />
                        </td>
                    </tr>
                </AlternatingItemTemplate>
            </asp:ListView>
        </div>
        <div class="paging">
            <asp:DataPager ID="DPProject" runat="server" PageSize="10" PagedControlID="ProjectList">
                <Fields>
                    <asp:NextPreviousPagerField
                        ButtonType="Link" ShowNextPageButton="False" ShowPreviousPageButton="true"
                        ShowFirstPageButton="true" FirstPageText="首页" PreviousPageText="上一页" />

                    <asp:NumericPagerField CurrentPageLabelCssClass="number now" NumericButtonCssClass="number" />

                    <asp:NextPreviousPagerField
                        ButtonType="Link" ShowPreviousPageButton="False" ShowNextPageButton="true"
                        ShowLastPageButton="true" LastPageText="末页" NextPageText="下一页" />

                    <asp:TemplatePagerField>
                        <PagerTemplate>
                            <span class="page">| <%# Container.StartRowIndex / Container.PageSize + 1%> / 
                            <%# (Container.TotalRowCount % Container.MaximumRows) > 0 ? Convert.ToInt16(Container.TotalRowCount / Container.MaximumRows) + 1 : Container.TotalRowCount / Container.MaximumRows%>  页
                            (共<%# Container.TotalRowCount %>项)
                            </span>
                        </PagerTemplate>
                    </asp:TemplatePagerField>
                </Fields>
            </asp:DataPager>
        </div>
    </div>
</div>
