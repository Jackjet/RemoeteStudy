<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="IF_wp_EnterpriseListUserControl.ascx.cs" Inherits="SVDigitalCampus.Internship_Feedback.IF_wp_EnterpriseList.IF_wp_EnterpriseListUserControl" %>
<script src="../../../../_layouts/15/SVDigitalCampus/Script/jquery-1.8.2.min.js"></script>
<link href="../../../../_layouts/15/SVDigitalCampus/CSS/common.css" rel="stylesheet" />
<link type="text/css" rel="stylesheet" href="../../../../_layouts/15/SVDigitalCampus/CSS/style.css" />
<link type="text/css" rel="stylesheet" href="../../../../_layouts/15/SVDigitalCampus/CSS/iconfont.css" />
<link type="text/css" rel="stylesheet" href="../../../../_layouts/15/SVDigitalCampus/CSS/animate.css" />
<link href="../../../../_layouts/15/SVDigitalCampus/Script/Enter/jquery-webox.css" rel="stylesheet" />
<script src="../../../../_layouts/15/SVDigitalCampus/Script/enter/tz_slider.js"></script>
<script src="../../../../_layouts/15/SVDigitalCampus/Script/enter/popwin.js"></script>
<script src="../../../../_layouts/15/SVDigitalCampus/Script/Enter/jquery-webox.js"></script>
<script src="../../../../_layouts/15/SVDigitalCampus/Script/F5.js"></script>
<style type="text/css">
    .tip {
        color: red;
        display: block;
        position: absolute;
        left: 120px;
    }
</style>
<script type="text/javascript">
    function add(j) {
        var txtAttr = "bigcol";
        var textid = txtAttr + '' + j;
        var text = document.getElementById(textid).value;
        if (text.length > 0) {
            document.getElementById("bt" + j).style.display = "none";
            document.getElementById("del" + j).style.display = "inline";
            j++;
            // $("#addinfo").append("<input type='text' name='bigcol' id=" + 'bigcol' + j + " value=''  class='hu' placeholder=' 请输入实习岗位' />&nbsp;<input id=" + 'bt' + j + " type='button' value='+' onclick='add(" + j + ")' /><input type='button' id=" + 'del' + j + " value='-' class='del'  style='display:none'/><br/>");
            $("#addinfo").append("<input type='text' name='bigcol' id=" + 'bigcol' + j + " value=''  class='hu' placeholder=' 请输入实习岗位' />&nbsp;<a href='#' id=" + 'bt' + j + "  onclick='add(" + j + ")'><i class='iconfont tishi jia_t'>&#xe6cd;</i></a><a href='#' id=" + 'del' + j + " class='del'  style='display:none'><i class='iconfont tishi jian_t'>&#xe9c6;</i></a><br/>");
        }
        else {
            alert("文本框有空值");
        }
    }
    $(document).ready(function () {
        //删除大列
        $('.del').live('click', function () {
            $(this).parent().remove();
        });

    });
    function add() {
        $.webox({
            height: 540, width: 600, bgvisibel: true, title: '添加企业信息', iframe: '<%=rootUrl%>' + "/SitePages/EnterAdd.aspx?" + Math.random
        });
        //popWin.showWin("600", "520", "添加企业信息", '<%=rootUrl%>' + "/SitePages/EnterAdd.aspx", "no");

    }
    function job(id) {
        $.webox({
            width: 600,
            height: 437,
            bgvisibel: true,
            title: '编辑岗位信息',
            iframe: '<%=rootUrl%>' + "/SitePages/EnterJob.aspx?EnterID=" + id + "&" + Math.random
        });
        //popWin.showWin("600", "437", "编辑岗位信息", '<%=rootUrl%>' + "/SitePages/EnterJob.aspx?EnterID=" + id, "no");
    }
    function Edit(id) {
        $.webox({
            width: 600,
            height: 470,
            bgvisibel: true,
            title: '修改企业信息',
            iframe: '<%=rootUrl%>' + "/SitePages/EnterEdit.aspx?EnterID=" + id + "&" + Math.random
        });
        //popWin.showWin("600", "470", "修改企业信息", '<%=rootUrl%>' + "/SitePages/EnterEdit.aspx?EnterID=" + id, "no");
    }
</script>

<!--企业信息管理-->
<div class="Enterprise_information_feedback">
    <!--页面名称-->
    <h1 class="Page_name">企业信息管理</h1>
    <!--整个展示区域-->
    <div class="Whole_display_area">
        <!--操作区域-->
        <div class="Operation_area">
            <div class="left_choice fl">
                <ul>
                    <li class="Select">
                        <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
                            <asp:ListItem Value="">全部状态</asp:ListItem>
                            <asp:ListItem Value="1">启用</asp:ListItem>
                            <asp:ListItem Value="0">禁用</asp:ListItem>
                        </asp:DropDownList>

                    </li>
                    <li class="Sear">
                        <input type="text" id="EnterName" placeholder=" 请输入公司名称" class="search" name="search" runat="server" /><i class="iconfont"><asp:LinkButton ID="LinkButton2" runat="server" OnClick="Button1_Click">&#xe62d;</asp:LinkButton></i>
                    </li>

                </ul>
            </div>
            <div class="right_add fr">
                <%--<asp:LinkButton ID="lbAdd" CssClass="add" runat="server" OnClick="lbAdd_Click"><i class="iconfont">&#xf000a;</i>添加企业</asp:LinkButton>--%>
                <a href="#" class="add" onclick="add()"><i class="iconfont">&#xe630;</i>添加企业</a>
            </div>
        </div>
        <div class="clear"></div>

        <div class="Display_form">
            <asp:ListView ID="LV_TermList" runat="server" OnPagePropertiesChanging="LV_TermList_PagePropertiesChanging" OnItemCommand="LV_TermList_ItemCommand" OnItemEditing="LV_TermList_ItemEditing" OnItemDataBound="LV_TermList_ItemDataBound">
                <EmptyDataTemplate>
                    <table>
                        <tr>
                            <td colspan="3">暂无企业信息。</td>
                        </tr>
                    </table>
                </EmptyDataTemplate>
                <LayoutTemplate>
                    <table id="itemPlaceholderContainer" style="width: 100%;" class="D_form">
                        <tr class="trth">
                            <%--<th class="Check">
                                <input type="checkbox" class="Check_box" name="Check_box" /></th>--%>
                            <th class="name">企业名称 </th>
                            <th class="Account">企业账号 </th>
                            <th class="Head">负责人 </th>
                            <th class="Contact">联系方式 </th>
                            <th class="Post">岗位 </th>
                            <th class="State">状态 </th>
                            <th class="Operation">操作 </th>

                        </tr>
                        <tr id="itemPlaceholder" runat="server"></tr>
                    </table>
                </LayoutTemplate>
                <ItemTemplate>
                    <tr class="Single">
                        <asp:Label ID="lbID" runat="server" Text='<%#Eval("ID") %>' Visible="false"></asp:Label>
                        <%--<td class="Check">
                            <asp:CheckBox ID="ckNew" runat="server" CssClass="Check_box" /></td>--%>
                        <td class="name"><%#Eval("Title")%></td>
                        <td class="Account"><%#Eval("UserID")%></td>
                        <td class="Head"><%#Eval("RelationName")%></td>
                        <td class="Contact"><%#Eval("RelationPhone")%></td>
                        <td class="Post">
                            <span class="Postname"><%#Eval("fistJob") %></span>
                            <span class="Post_post">
                                <a href="#"><i class="Drop_down iconfont">&#xe640;(<%#Eval("JobN") %>)</i></a>
                                <div class="more_info" style="display: none;">
                                    <ul class="info">
                                        <%#Eval("info") %>
                                    </ul>
                                </div>
                            </span>
                            <span class="Postadd">
                                <a href="#" class="add" onclick="job('<%#Eval("ID")%>')"><i class="Plus iconfont">&#xe630;</i></a>

                                <%--<asp:LinkButton ID="lbjob" runat="server" CommandName="JEdit" CommandArgument='<%#Eval("ID")%>'><i class="Plus iconfont">&#xf000a;</i></asp:LinkButton>--%></i></a>
                        </td>
                        <td class="State">
                            <asp:Label ID="lbStatus" runat="server" Text='<%#Eval("Status") %>' Visible="false"></asp:Label>
                            <span class="qijin">
                                <asp:LinkButton ID="lbon" runat="server" CommandArgument='<%#Eval("ID")%>' CommandName="Estatus">启用</asp:LinkButton><asp:LinkButton ID="lboff" runat="server" CommandArgument='<%#Eval("ID")%>' CommandName="Estatus">禁用</asp:LinkButton>
                            </span></td>
                        <td class="Operation">
                            <a href="#" class="add" onclick="Edit('<%#Eval("ID")%>')"><i class="iconfont">&#xe629;</i></a>
                            </i></a></td>
                    </tr>

                    </tr>
                </ItemTemplate>
                <AlternatingItemTemplate>
                    <tr class="Double">
                        <%--<td class="Check">
                            <asp:CheckBox ID="ckNew" runat="server" CssClass="Check_box" />
                        </td>--%>
                        <asp:Label ID="lbID" runat="server" Text='<%#Eval("ID")%>' Visible="false"></asp:Label>

                        <td class="name"><%#Eval("Title")%></td>
                        <td class="Account"><%#Eval("UserID")%></td>
                        <td class="Head"><%#Eval("RelationName")%></td>
                        <td class="Contact"><%#Eval("RelationPhone")%></td>
                        <td class="Post">
                            <span class="Postname"><%#Eval("fistJob") %></span>
                            <span class="Post_post">
                                <a href="#"><i class="Drop_down iconfont">&#xe640;(<%#Eval("JobN") %>)</i></a>
                                <div class="more_info" style="display: none;">
                                    <ul class="info">
                                        <%#Eval("info") %>
                                    </ul>
                                </div>
                            </span>
                            <span class="Postadd">
                                <a href="#" class="add" onclick="job('<%#Eval("ID")%>')"><i class="Plus iconfont">&#xe630;</i></a>
                            </span>
                        </td>
                        <td class="State">
                            <asp:Label ID="lbStatus" runat="server" Text='<%#Eval("Status") %>' Visible="false"></asp:Label>
                            <span class="qijin">
                                <asp:LinkButton ID="lbon" runat="server" CommandArgument='<%#Eval("ID")%>' CommandName="Estatus">启用</asp:LinkButton><asp:LinkButton ID="lboff" runat="server" CommandArgument='<%#Eval("ID")%>' CommandName="Estatus">禁用</asp:LinkButton>
                            </span></td>
                        <td class="Operation">
                            <a href="#" class="add" onclick="Edit('<%#Eval("ID")%>')"><i class="iconfont">&#xe629;</i></a>

                            <%--<asp:LinkButton ID="LinkButton1" runat="server" CommandName="Edit" CommandArgument='<%#Eval("ID") %>'><i class="iconfont">&#xe62f;</i></asp:LinkButton>--%></td>
                    </tr>
                </AlternatingItemTemplate>
            </asp:ListView>


        </div>
    </div>
</div>
<div class="page">
    <asp:DataPager ID="DPTeacher" runat="server" PageSize="10" PagedControlID="LV_TermList">
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
                    <span class="pageup">| <%# Container.StartRowIndex / Container.PageSize + 1%> / 
                            <%# (Container.TotalRowCount % Container.MaximumRows) > 0 ? Convert.ToInt16(Container.TotalRowCount / Container.MaximumRows) + 1 : Container.TotalRowCount / Container.MaximumRows%>  页
                            (共<%# Container.TotalRowCount %>项)
                    </span>
                </PagerTemplate>
            </asp:TemplatePagerField>
        </Fields>
    </asp:DataPager>
</div>
