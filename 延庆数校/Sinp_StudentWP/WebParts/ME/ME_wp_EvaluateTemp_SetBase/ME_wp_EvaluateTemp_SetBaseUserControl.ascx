<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ME_wp_EvaluateTemp_SetBaseUserControl.ascx.cs" Inherits="Sinp_StudentWP.WebParts.ME.ME_wp_EvaluateTemp_SetBase.ME_wp_EvaluateTemp_SetBaseUserControl" %>
<script src="/_layouts/15/Script/jquery-1.8.0.js"></script>
<link href="/_layouts/15/Style/common.css" rel="stylesheet" />
<link rel="stylesheet" href="/_layouts/15/Stu_css/allst.css">
<link href="/_layouts/15/Style/layout.css" rel="stylesheet" />
<link href="/_layouts/15/Style/tablelist.css" rel="stylesheet" />
<script type="text/javascript">
    function selectAll(chk)
    {
        var chked = $(chk).attr('checked');
        if (chked == undefined)
        {
            $('input[type=checkbox]').removeAttr("checked");
        }
        $('input[type=checkbox]').attr('checked', chked);
    }
    function getItem() {
        var arrChk = $("input[name='ck_list']:checked");
        if (arrChk.length > 0) {
            var ids = "";
            for (var i = 0; i < arrChk.length; i++) {
                ids += arrChk[i].value+",";
            }
            $("#itemIds").val(ids);
            $("#btnOK").click();
        }
        else{
            alert("您没有选择项!");
        }
    }
</script>
<div class="writing_form">
    <div class="searchtit" style="margin:0px">
        <asp:Label runat="server" Text="搜索条件：" />
        <asp:DropDownList CssClass="option" ID="DDL_Target" runat="server" />
        <asp:Button CssClass="sear" ID="Btn_Search" runat="server" Text="查询" Height="24px" OnClick="Btn_Search_Click" />
        <span style="float: right">
            <a class="add_GL" onclick="getItem();"><i class="iconfont"></i>添加</a>
        </span>
    </div>
    <div class="content_list">
        <asp:ListView ID="lvEvaluateBase" runat="server">
            <EmptyDataTemplate>
                <table class="W_form">
                    <tr class="trth">
                        <td colspan="3" style="text-align: center">暂无考评基础项</td>
                    </tr>
                </table>
            </EmptyDataTemplate>
            <LayoutTemplate>
                <table class="W_form" id="itemPlaceholderContainer">
                    <tr class="trth">
                        <th><input type="checkbox" onclick="selectAll(this);" /></th>
                        <th>名称
                        </th>
                        <th>内容
                        </th>
                        <th>周期
                        </th>
                        <th>分值
                        </th>
                        <th>对象
                        </th>
                    </tr>
                    <tr id="itemPlaceholder" runat="server"></tr>
                </table>
                <div class="page">
                    <asp:DataPager ID="EvaluateBasePager" PageSize="10" runat="server">
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
                        <input type="checkbox" name="ck_list" value='<%#Eval("ID")%>' />
                    </td>
                    <td>
                        <%#Eval("Title")%>
                    </td>
                    <td>
                        <%#Eval("Content")%>
                    </td>
                    <td>
                        <%#Eval("Cycle")%>
                    </td>
                    <td>
                        <%#Eval("Score")%>
                    </td>
                    <td>
                        <%#Eval("Target")%>
                    </td>
                </tr>
            </ItemTemplate>
        </asp:ListView>
        <div style="display: none">
            <asp:HiddenField runat="server" ClientIDMode="Static" ID="itemIds"/>
            <asp:Button runat="server" ClientIDMode="Static" ID="btnOK" OnClick="btnOK_Click" />
        </div>
    </div>
</div>