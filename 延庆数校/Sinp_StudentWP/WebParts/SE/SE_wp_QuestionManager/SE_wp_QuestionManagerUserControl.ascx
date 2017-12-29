<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SE_wp_QuestionManagerUserControl.ascx.cs" Inherits="Sinp_StudentWP.WebParts.SE.SE_wp_QuestionManager.SE_wp_QuestionManagerUserControl" %>
<script src="/_layouts/15/Script/jquery-1.8.0.js"></script>
<script src="/_layouts/15/Script/popwin.js"></script>
<link href="/_layouts/15/Style/common.css" rel="stylesheet" />
<link rel="stylesheet" href="/_layouts/15/Stu_css/allst.css">
<link href="/_layouts/15/Style/layout.css" rel="stylesheet" />
<link href="/_layouts/15/Style/tablelist.css" rel="stylesheet" />
<style type="text/css">
    .selected { background-color:#0da6ec;color:#fff;}
    .line { line-height:21px}
</style>
<script type="text/javascript">
    $(function () {
        $("#group").click(function () {
            $("#group").removeClass("selected");
            $(this).toggleClass("selected");
        });
    });
    function openPage(pageTitle, pageName, pageWidth, pageHeight) {
        var webUrl = _spPageContextInfo.webAbsoluteUrl;
        popWin.showWin(pageWidth, pageHeight, pageTitle, webUrl + pageName);
    }
    function closePages() {
        $("#mask,#maskTop").fadeOut();
    }
    function deleteItem(id) {
        if (confirm("你确定要删除吗？")) {
            $("#ItemId").val(id);
            $("#Delete").click();
        }
    }
</script>
<div class="writingform">
    <div class="writing_title">
        <h2>
            <span class="title_left fl">题库管理</span>
        </h2>
    </div>
    <div class="writing_form">
        <div class="screenBox" style="margin:10px;border:0px">
        <dl style="height:38px;border:1px solid #e5e5e5">
            <dt>组别</dt>
            <dd>
                <asp:ListView ID="lvGroup" runat="server">
                    <ItemTemplate>
                        <li><a id="group"><%#Eval("Grade")%></a></li>
                    </ItemTemplate>
                </asp:ListView>
            </dd>
        </dl></div>
        <div style="margin: 10px; height: 30px; line-height: 30px">
            <span style="float: left">
                <asp:DropDownList CssClass="option" ID="DDL_Type" runat="server" Height="21px" AutoPostBack="true" OnSelectedIndexChanged="DDL_Type_SelectedIndexChanged" />
                <asp:TextBox CssClass="line" ID="Tb_searchTitle" placeholder="请输入关键字" runat="server" Height="21px" />
                <asp:Button CssClass="line" ID="Btn_Search" runat="server" Text="查询" Height="24px"  OnClick="Btn_Search_Click"/>
            </span>
            <span style="float: right">
                <a class="add_GL" onclick="openPage('添加新题','/SitePages/EditQuestion.aspx','550','460');return false;"><i class="iconfont"></i>添加新题</a>&nbsp;&nbsp;
                <a class="add_GL" onclick="openPage('组别管理','/SitePages/Group.aspx','550','420');return false;"><i class="iconfont"></i>组别管理</a>
            </span>
        </div>
        <div class="content_list">
            <asp:ListView ID="lvQuestion" runat="server">
                <EmptyDataTemplate>
                    <table class="W_form">
                        <tr class="trth">
                            <td style="text-align: center">暂无调查题目</td>
                        </tr>
                    </table>
                </EmptyDataTemplate>
                <LayoutTemplate>
                    <table class="W_form" id="itemPlaceholderContainer">
                        <tr class="trth">
                            <th>组别
                            </th>
                            <th>题目
                            </th>
                            <th>内容
                            </th>
                            <th>类型
                            </th>
                            <th>发布时间
                            </th>
                            <th>操作
                            </th>
                        </tr>
                        <tr id="itemPlaceholder" runat="server"></tr>
                    </table>
                    <div class="page">
                        <asp:DataPager ID="QuestionPager" PageSize="10" runat="server">
                            <Fields>
                                <asp:NextPreviousPagerField ButtonType="Link" FirstPageText="首页" PreviousPageText="上一页"
                                    ShowFirstPageButton="true" ShowPreviousPageButton="true" ShowNextPageButton="false" />
                                <asp:NumericPagerField CurrentPageLabelCssClass="number now" NumericButtonCssClass="number" />
                                <asp:NextPreviousPagerField ButtonType="Link" NextPageText="下一页" LastPageText="末页"
                                    ShowLastPageButton="true" ShowPreviousPageButton="false" ShowNextPageButton="true" />
                                <asp:TemplatePagerField>
                                    <PagerTemplate>
                                        <span class="page">| <%#Container.StartRowIndex/Container.PageSize+1%> /
                                   <%#(Container.TotalRowCount%Container.MaximumRows)>0?Container.TotalRowCount/Container.MaximumRows+1:Container.TotalRowCount/Container.MaximumRows %>页
                                   (共<%#Container.TotalRowCount%>项)
                                        </span>
                                    </PagerTemplate>
                                </asp:TemplatePagerField>
                            </Fields>
                        </asp:DataPager>
                    </div>
                </LayoutTemplate>
                <ItemTemplate>
                    <tr class="Single">
                        <td>
                            <%#Eval("Group")%>
                        </td>
                        <td>
                            <%#Eval("Title")%>
                        </td>
                        <td>
                            <%#Eval("Content")%>
                        </td>
                        <td>
                            <%#Eval("Type")%>
                        </td>
                        <td>
                            <%#Eval("Date")%>
                        </td>
                        <td>
                            <a href="javascript:void(0)" class="btnpass" onclick="openPage('编辑','/SitePages/EditQuestion.aspx?itemid=<%#Eval("ID")%>&type=<%#Eval("Type")%>','550','460');return false">编辑</a>
                            <a href="javascript:void(0)" class="btnrefuse" onclick="deleteItem('<%#Eval("ID")%>')">删除</a>
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:ListView>
            <div style="display:none">
                <asp:HiddenField runat="server" ClientIDMode="Static" ID="ItemId" />
                <asp:Button runat="server" ClientIDMode="Static" ID="Delete" OnClick="Delete_Click" />
            </div>
        </div>
    </div>
</div>