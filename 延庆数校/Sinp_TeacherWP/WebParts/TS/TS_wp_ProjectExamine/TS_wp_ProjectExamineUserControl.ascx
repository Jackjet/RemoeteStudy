<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TS_wp_ProjectExamineUserControl.ascx.cs" Inherits="Sinp_TeacherWP.WebParts.TS.TS_wp_ProjectExamine.TS_wp_ProjectExamineUserControl" %>
<script src="/_layouts/15/Script/jquery-1.8.0.js"></script>
<link href="/_layouts/15/Style/layout.css" rel="stylesheet" />
<link href="/_layouts/15/Style/common.css" rel="stylesheet" />
<link href="/_layouts/15/Style/tablelist.css" rel="stylesheet" />
<script src="/_layouts/15/Script/popwin.js"></script>
<script src="/_layouts/15/Script/uploadFile.js"></script>
<script type="text/javascript">
    $(function () {
        $(".exphasetit").click(function () {
            $(this).next().toggle();
            var a = $(this).find('a');
            var updown = $(a).attr("class");
            if (updown == "up") {
                $(a).attr("class","down");
            }
            else {
                $(a).attr("class", "up");
            }
        });
    })
    function closePages() {
        $("#mask,#maskTop").fadeOut();
    }
</script>

<div class="PE_wrap">
   <div class="PE_con W_main">
<div class="projecttit">课题信息</div>
<asp:ListView ID="LV_Project" runat="server" OnItemDataBound="LV_Project_ItemDataBound">
    <EmptyDataTemplate>
        <table>
            <tr>
                <td colspan="3">暂无送审通知。</td>
            </tr>
        </table>
    </EmptyDataTemplate>
    <LayoutTemplate>

        <div id="itemPlaceholder" runat="server"></div>

    </LayoutTemplate>
    <ItemTemplate>
        <asp:HiddenField ID="Hid_ProId" runat="server" Value='<%#Eval("ID") %>' />
        <div class="exphasetit"><a href="#" class='<%#Eval("UpDown") %>' ></a><span><%#Eval("Title") %></span><span class="right"><%#Eval("ProjectPhase") %></span></div>
        
        <asp:ListView ID="LV_ProRecord" runat="server">
            <EmptyDataTemplate>
                <table style="display:none;" class="W_form">
                    <tr>
                        <td colspan="3">暂无送审通知。</td>
                    </tr>
                </table>
            </EmptyDataTemplate>
            <LayoutTemplate>
                <table class="W_form">
                    <tr id="itemPlaceholder" runat="server"></tr>
                </table>
            </LayoutTemplate>
            <ItemTemplate>
                <tr class="Single">
                    <%--<td><%#Eval("Title") %></td>--%>
                    <td>申请人：<%#Eval("CreateUser") %></td>
                    <td><%#Eval("ProjectName") %></td>
                    <td><%#Eval("Created") %></td>
                    <td class="Operations">
                        <asp:HiddenField ID="DetailID" runat="server" Value='<%# Eval("ID") %>' />
                        <a href="#" onclick="editpdetail(this, '课题阶段审核', '/SitePages/ExamineItem.aspx?itemid=', '800', '600');">审核</a></td>
                </tr>
            </ItemTemplate>

        </asp:ListView>
        <div class="paging">
            <asp:DataPager ID="DP_ProRecord" runat="server" PageSize="10" PagedControlID="LV_ProRecord">
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
    </ItemTemplate>

</asp:ListView>

<div class="paging">
    <asp:DataPager ID="DPProject" runat="server" PageSize="10" PagedControlID="LV_Project">
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