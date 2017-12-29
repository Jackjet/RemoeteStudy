<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SE_wp_SelectQuestionUserControl.ascx.cs" Inherits="Sinp_StudentWP.WebParts.SE.SE_wp_SelectQuestion.SE_wp_SelectQuestionUserControl" %>
<script src="/_layouts/15/Script/jquery-1.8.0.js"></script>
<link href="/_layouts/15/Style/common.css" rel="stylesheet" />
<link href="/_layouts/15/Style/tablelist.css" rel="stylesheet" />
<style type="text/css">
.btn {border-radius:3px;border:none;width:80px;height:35px;margin-right:20px; font-size:14px;font-family:"微软雅黑";cursor: pointer;}
.btn_sure{color:#fff;background:#0da6ec;}
.btn_sure:hover{background:#0072c6;outline:none;}
/*分页*/
.page {text-align: center;height:35px;line-height:35px; font-size:12px;clear:both;margin-top:10px;}
.page span { margin:0 5px;}
.page span.number a { margin: 0 4px; padding:2px 8px; background:#5493d7; border-radius:3px; color:#fff; }
.page span a{ cursor:pointer;padding:0 4px;}
.page span .number { background:#fff; border: 1px solid #d7d7d7; color:#757575;padding:1px 6px; border-radius:3px; margin:0px 2px;}
.page span .now {  background:#5593d7; border: 1px solid #0494d6; color:#fff;}
</style>
<script type="text/javascript">
    function selectAll(chk) {
        var chked = $(chk).attr('checked');
        if (chked == undefined) {
            $('input[type=checkbox]').removeAttr("checked");
        }
        $('input[type=checkbox]').attr('checked', chked);
    }
    function getItem() {
        var arrChk = $("input[name='ck_list']:checked");
        if (arrChk.length > 0) {
            var ids = "<%=this._type %>#";
            for (var i = 0; i < arrChk.length; i++) {
                ids += arrChk[i].value + ",";
            }
            parent.refleshData(ids);
        }
        else {
            alert("您没有选择项!");
        }
    }
</script>
<div class="writing_form">
    <asp:ListView ID="lvQuestion" runat="server">
        <EmptyDataTemplate>
            <table class="W_form">
                <tr class="trth">
                    <td colspan="3" style="text-align: center">暂无该类题目</td>
                </tr>
            </table>
        </EmptyDataTemplate>
        <LayoutTemplate>
            <table class="W_form" style="margin:10px auto">
                <tr class="trth">
                    <th><input type="checkbox" onclick="selectAll(this);" /></th>
                    <th>组别
                    </th>
                    <th>题目
                    </th>
                    <th>内容
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
                <td><input type="checkbox" name="ck_list" value='<%#Eval("ID")%>' /></td>
                <td>
                    <%#Eval("Group")%>
                </td>
                <td>
                    <%#Eval("Title")%>
                </td>
                <td>
                    <%#Eval("Content")%>
                </td>
            </tr>
        </ItemTemplate>
    </asp:ListView>
    <div style="margin: 10px auto; width: 80px">
        <input type="button" class="btn btn_sure" value="确定" onclick="getItem()" />
    </div>
</div>