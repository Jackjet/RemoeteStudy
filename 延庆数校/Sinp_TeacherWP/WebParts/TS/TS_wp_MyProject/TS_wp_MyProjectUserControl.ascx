<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TS_wp_MyProjectUserControl.ascx.cs" Inherits="Sinp_TeacherWP.WebParts.TS.TS_wp_MyProject.TS_wp_MyProjectUserControl" %>
<script src="/_layouts/15/Script/jquery-1.8.0.js"></script>
<link href="/_layouts/15/Style/tablelist.css" rel="stylesheet" />
<link href="/_layouts/15/Style/layout.css" rel="stylesheet" />
<link href="/_layouts/15/Style/common.css" rel="stylesheet" />
<script src="/_layouts/15/Script/popwin.js"></script>
<script src="/_layouts/15/Script/uploadFile.js"></script>

<script type="text/javascript">
    $(function () {
        $(".Mp_tab .Mp_tabheader").find("a").click(function () {
            var index = $(this).parent().index();
            $(this).parent().addClass("selected").siblings().removeClass("selected");
            $(this).parents(".Mp_tab").find(".td").eq(index).show().siblings().hide();
        });
    })
    function closePages() {
        $("#mask,#maskTop").fadeOut();
    }
</script>
    
<div class="Scientific_management">
    <div class="Mp_tab">
        <div class="Mp_tabheader">
        <ul class="tab_tit">
            <li class="selected"><a href="#" class="first">我的课题</a></li>
            <li><a href="#" class="second">申报课题</a></li>
        </ul>
    </div>
        <div class="Mp_content">
            <div class="td" id="first">
            <div class="searchtit">我的课题：<asp:TextBox ID="TB_Search" runat="server" CssClass="searinput"></asp:TextBox><asp:Button ID="Btn_Search" runat="server" Text="搜索" OnClick="Btn_Search_Click" CssClass="sear" /></div>
            <div class="tabcontent">
                <asp:ListView ID="LV_TermList" runat="server" OnPagePropertiesChanging="LV_TermList_PagePropertiesChanging">
                    <EmptyDataTemplate>
                        <table>
                            <tr>
                                <td colspan="3">暂无课题。</td>
                            </tr>
                        </table>
                    </EmptyDataTemplate>
                    <LayoutTemplate>
                        <table cellspacing="0" cellpadding="0" class="W_form">
                            <tr class="trth">

                                <th>课题名称</th>
                                <th>课题级别</th>
                                <th>课题负责人</th>
                                <th>申请时间</th>
                                <th>课题状态</th>
                            </tr>
                            <tr id="itemPlaceholder" runat="server"></tr>
                        </table>
                    </LayoutTemplate>
                    <ItemTemplate>
                        <tr class="Single">
                            <td><a href='ProjectDetail.aspx?itemid=<%#Eval("ID") %>'><%# Eval("Title") %></a></td>
                            <td><%# Eval("ProjectLevel") %></td>
                            <td><%# Eval("ProjectDirector") %></td>
                            <td><%# Eval("DeclareDate") %></td>
                            <td><%# Eval("ProjectPhase") %></td>
                        </tr>
                    </ItemTemplate>
                    <AlternatingItemTemplate>
                        <tr class="Double">
                            <td><a href='ProjectDetail.aspx?itemid=<%#Eval("ID") %>'><%# Eval("Title") %></a></td>
                            <td><%# Eval("ProjectLevel") %></td>
                            <td><%# Eval("ProjectDirector") %></td>
                            <td><%# Eval("DeclareDate") %></td>
                            <td><%# Eval("ProjectPhase") %></td>
                        </tr>
                    </AlternatingItemTemplate>

                </asp:ListView>

            </div>
            <div class="paging">
                <asp:DataPager ID="DPTeacher" runat="server" PageSize="8" PagedControlID="LV_TermList">
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

            <div class="td" id="second" style="display: none;">
            <div class="tabcontent">
                <asp:ListView ID="SB_TermList" runat="server" OnPagePropertiesChanging="SBLV_TermList_PagePropertiesChanging">
                    <EmptyDataTemplate>
                        <table class="W_form">
                            <tr>
                                <td colspan="3">暂无课题。</td>
                            </tr>
                        </table>
                    </EmptyDataTemplate>
                    <LayoutTemplate>
                        <table cellspacing="0" cellpadding="0" class="W_form">
                            <tr class="trth">

                                <th>课题名称</th>
                                <th>课题级别</th>
                                <th>开始时间</th>
                                <th>结束时间</th>
                                <th>操作</th>

                            </tr>
                            <tr id="itemPlaceholder" runat="server"></tr>
                        </table>
                    </LayoutTemplate>
                    <ItemTemplate>
                        <tr class="Single">
                            <td><a href='ReportDetail.aspx?itemid=<%#Eval("ID") %>'><%# Eval("Title") %></a></td>
                            <td><%# Eval("ProjectLevel") %></td>
                            <td><%# Eval("StartDate") %></td>
                            <td><%# Eval("EndDate") %></td>
                            <td>
                                <asp:HiddenField ID="DetailID" runat="server" Value='<%# Eval("ID") %>' />
                                <input type="button" value="申报课题" <%# Eval("Status")%> onclick="editpdetail(this, '申报课题', '/SitePages/ReportProject.aspx?pid=', '750', '600');" class="list_btn" />
                                <%--<input type="button" value="编辑" onclick="editpdetail(this, '编辑课题', '/SitePages/ReportProject.aspx?itemid=', '800', '600');" />
                                <asp:Button ID="BtnDelete" runat="server" Text="删除" />--%>
                            </td>

                        </tr>
                    </ItemTemplate>
                    <AlternatingItemTemplate>
                        <tr class="Double">
                            <td><a href='ReportDetail.aspx?itemid=<%#Eval("ID") %>'><%# Eval("Title") %></a></td>
                            <td><%# Eval("ProjectLevel") %></td>
                            <td><%# Eval("StartDate") %></td>
                            <td><%# Eval("EndDate") %></td>
                            <td>
                                <asp:HiddenField ID="DetailID" runat="server" Value='<%# Eval("ID") %>' />
                                <input type="button" value="申报课题" <%# Eval("Status")%> onclick="editpdetail(this, '申报课题', '/SitePages/ReportProject.aspx?pid=', '750', '600');" class="list_btn" />
                                <%--<input type="button" value="编辑" onclick="editpdetail(this, '编辑课题', '/SitePages/ReportProject.aspx?itemid=', '800', '600');;" />
                                <asp:Button ID="BtnDelete" runat="server" Text="删除" />--%>
                            </td>
                        </tr>
                    </AlternatingItemTemplate>
                </asp:ListView>

            </div>
            <div class="paging">
                <asp:DataPager ID="SBDPTeacher" runat="server" PageSize="8" PagedControlID="SB_TermList">
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
                            <%# (Container.TotalRowCount % Container.MaximumRows) > 0 ? Convert.ToInt32(Container.TotalRowCount / Container.MaximumRows) + 1 : Container.TotalRowCount / Container.MaximumRows%>  页
                            (共<%# Container.TotalRowCount %>项)
                                </span>
                            </PagerTemplate>
                        </asp:TemplatePagerField>
                    </Fields>
                </asp:DataPager>
            </div>
        </div>
    </div>
</div>
    <div class="Learning_materials">
        <div class="single_tit">
            <h2><span class="title_left"><a href="#">学习资料</a></span><span class="title_right"><a href="#" class="more" onclick="openPage('添加学习资料', '/SitePages/UploadFile.aspx', '600', '200'); return false;">上传学习资料</a></span></h2>
        </div>
        <div class="content">
            <div class="tabcontent">
                <asp:ListView ID="LV_LearnData" runat="server" OnPagePropertiesChanging="LV_LearnData_PagePropertiesChanging">
                    <EmptyDataTemplate>
                        <table>
                            <tr>
                                <td colspan="3">暂无学习资料。</td>
                            </tr>
                        </table>
                    </EmptyDataTemplate>
                    <LayoutTemplate>
                        <table cellspacing="0" cellpadding="0" class="W_form">
                            <tr class="trth">

                                <th>类型</th>
                                <th>标题</th>
                                
                            </tr>
                            <tr id="itemPlaceholder" runat="server"></tr>
                        </table>
                    </LayoutTemplate>
                    <ItemTemplate>
                        <tr class="Single">
                            <td><img height="16" width="16" src='/_layouts/15/images/ic<%# Eval("Type") %>.gif'></img></td>
                            <td><a target="_blank" href='<%# Eval("Href") %>'><%# Eval("Title") %></a></td>
                        </tr>
                    </ItemTemplate>
                    <AlternatingItemTemplate>

                        <tr class="Double">
                            <td><img height="16" width="16" src='/_layouts/15/images/ic<%# Eval("Type") %>.gif'></img></td>
                            
                            <td><a target="_blank" href='<%# Eval("Href") %>'><%# Eval("Title") %></a></td>
                        </tr>
                    </AlternatingItemTemplate>

                </asp:ListView>

            </div>
            <div class="paging">
                <asp:DataPager ID="DP_LearnData" runat="server" PageSize="8" PagedControlID="LV_LearnData">
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
</div>






