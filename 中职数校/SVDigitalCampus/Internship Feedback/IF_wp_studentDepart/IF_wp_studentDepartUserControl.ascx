<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="IF_wp_studentDepartUserControl.ascx.cs" Inherits="SVDigitalCampus.Internship_Feedback.IF_wp_studentDepart.IF_wp_studentDepartUserControl" %>

<script src="../../_layouts/15/SVDigitalCampus/Script/JSBase.js"></script>
<script src="../../../../_layouts/15/SVDigitalCampus/Script/jquery-1.8.2.min.js"></script>
<script src="../../../../_layouts/15/SVDigitalCampus/Script/Enter/popwin.js"></script>
<link href="../../../../_layouts/15/SVDigitalCampus/CSS/common.css" rel="stylesheet" />
<link type="text/css" rel="stylesheet" href="../../../../_layouts/15/SVDigitalCampus/CSS/style.css" />
<link type="text/css" rel="stylesheet" href="../../../../_layouts/15/SVDigitalCampus/CSS/iconfont.css" />
<link type="text/css" rel="stylesheet" href="../../../../_layouts/15/SVDigitalCampus/CSS/animate.css" />
<script src="../../../../_layouts/15/SVDigitalCampus/Script/Enter/tz_slider.js"></script>
<style type="text/css">
    .first_a {
        background: #5493D7;
        color: #FFFFFF;
    }
</style>
<script type="text/javascript">

    function SelectAllCheckBox() {
        var form = document.forms[0];
        for (i = 0; i < form.elements.length; i++) {
            if (form.elements[i].type == "checkbox") {
                form.elements[i].checked = true;
            }
        }
    }
    function FormSelectAll(EleName, e) //formID：目标复选框组所在的form表单的ID属性；Elename：目标复选框组共同的Name属性；e：用于标识是否全选的复选框自身，用户判断是“全选”还是“全不选”  
    {
        var Elements = document.forms[0].elements; //获取目标复选框组所在的Form表单  
        for (var i = 0; i < Elements.length; i++) {
            if (Elements[i].type == "checkbox" && Elements[i].name.indexOf(EleName) >= 0) //根据对象类型和对象的name属性判断是否为目标复选框  
            {
                Elements[i].checked = e.checked; //根据用于控制的复选框的选中情况判断是否选中目标复选框  
            }
        }
    }
</script>
<script type="text/javascript">
    function StuM() {
        var FirstUrl = window.location.href;
        FirstUrl = FirstUrl.substring(0, FirstUrl.indexOf("SitePages"));

        window.location.href = FirstUrl + "/SitePages/StudentManage.aspx";
    }
</script>
<div class="Distribution_of_students">
    <h1 class="Page_name"><a style="text-decoration: underline; color: blue;" onclick="StuM()">学生信息管理</a>>分配实习人员</h1>

    <div class="listswitch">
        <div class="tab_switch">
            <ul class="sw_tit">
                <li class="fl">实习企业</li>
                <li class="Sear fr">
                    <input type="text" id="txtName" placeholder=" 请输入公司名称" class="search" name="search" runat="server" /><i class="iconfont">
                        <asp:LinkButton ID="lbserch" runat="server" OnClick="lbserch_Click">&#xe62d;</asp:LinkButton></i>
            </ul>
            <div class="clear"></div>
            <div class="S_conditions">
                <div id="selectList" class="screenBox screenBackground">
                    <dl class="listIndex dlHeight" attr="terminal_brand_s">
                        <dt>企业</dt>
                        <dd id="Major">
                            <asp:Repeater ID="rptEnter" runat="server" OnItemCommand="rptEnter_ItemCommand" OnItemDataBound="rptEnter_ItemDataBound">
                                <ItemTemplate>
                                    <asp:Label ID="lbsort" runat="server" Text='<%#Eval("Sorts") %>' Visible="false"></asp:Label>

                                    <asp:Label ID="labEnter" runat="server" Text='<%#Eval("ID") %>' Visible="false"></asp:Label>
                                    <li>
                                        <asp:LinkButton ID="lbEnter" runat="server" Text='<%#Eval("Title") %>' CommandName="Click" CommandArgument='<%#Eval("ID") %>'></asp:LinkButton>
                                    </li>
                                </ItemTemplate>
                            </asp:Repeater>

                            <span class="more">展开</span>
                        </dd>
                    </dl>
                    <dl class="listIndex" attr="terminal_brand_s" id="subjectdl" runat="server">
                        <dt>岗位</dt>
                        <dd>
                            <asp:Repeater ID="rptJob" runat="server" OnItemCommand="rptJob_ItemCommand1">
                                <ItemTemplate>
                                    <asp:Label ID="labJob" runat="server" Text='<%#Eval("ID") %>' Visible="false"></asp:Label>
                                    <li>
                                        <asp:LinkButton ID="lbJob" runat="server" Text='<%#Eval("Title") %>' CommandName="Click" CommandArgument='<%#Eval("ID") %>'></asp:LinkButton>
                                    </li>

                                </ItemTemplate>
                            </asp:Repeater>

                            <dd></dd>
                    </dl>
                </div>
            </div>

        </div>
        <div class="Assignment_form">
            <div class="left_form fl">
                <h3 class="trth">已分配学员</h3>
                <div class="Display_form">
                    <asp:ListView ID="LV_TermList" runat="server" OnPagePropertiesChanging="LV_TermList_PagePropertiesChanging">
                        <EmptyDataTemplate>
                            <table>
                                <tr>
                                    <td colspan="3">暂无学员分配该企业</td>
                                </tr>
                            </table>
                        </EmptyDataTemplate>
                        <LayoutTemplate>
                            <table id="itemPlaceholderContainer" style="width: 100%;" class="PL_form">
                                <tr class="trth">
                                    <th>
                                       <input name="CheckAll" type="checkbox" value="" onclick="javascript: FormSelectAll('ckOld', this);" /></th>
                                    <th>姓名</th>
                                    <th>性别</th>
                                    <th>实习岗位</th>
                                </tr>
                                <tr id="itemPlaceholder" runat="server"></tr>
                            </table>
                        </LayoutTemplate>
                        <ItemTemplate>
                            <tr class="Single">
                                <td>
                                    <asp:CheckBox ID="ckOld" runat="server" /></td>
                                <asp:Label ID="lbStuid" runat="server" Text='<%#Eval("StuID")%>' Visible="false"></asp:Label>
                                <asp:Label ID="lbID" runat="server" Text='<%#Eval("ID")%>' Visible="false"></asp:Label>
                                <td><%#Eval("Title")%></td>
                                <td><%#Eval("Sex")%></td>
                                <td><%#Eval("EJob")%></td>
                            </tr>
                        </ItemTemplate>

                    </asp:ListView>
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
            <div class="LR_btn fl">
                <span class="btn_width">
                    <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" ForeColor="#96cc66" BorderStyle="None" CssClass="leftbtn" />
                    <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" ForeColor="#ff666b" BorderStyle="None" CssClass="rightbtn" />
                </span>
            </div>
            <div class="right_form fr">
                <h3 class="trth">未分配学员 <span class="sear fr">
                    <span class="times_input">
                        <input id="tbName" runat="server" class="time" placeholder=" 请输学生姓名" /><i class="iconfont"></i></span>
                    <span class="times_input">
                        <input id="tbzhuanye" runat="server" class="time" placeholder=" 请输学生专业" /><i class="iconfont"></i></span>
                    <asp:Button ID="btserch" runat="server" Text="查询" OnClick="btserch_Click" CssClass="b_query" /></span>
                </h3>

                <asp:Label ID="lbE" runat="server" Text="" Visible="false"></asp:Label>
                <asp:Label ID="lbJ" runat="server" Text="" Visible="false"></asp:Label>

                <div class="Display_form">
                    <asp:ListView ID="ListView1" runat="server" OnPagePropertiesChanging="ListView1_PagePropertiesChanging">
                        <EmptyDataTemplate>
                            <table>
                                <tr>
                                    <td colspan="3">所有学生都已分配</td>
                                </tr>
                            </table>
                        </EmptyDataTemplate>
                        <LayoutTemplate>
                            <table id="itemPlaceholderContainer1" class="PR_form">
                                <tr class="trth">
                                    <th><input name="CheckAll" type="checkbox" value="" onclick="javascript: FormSelectAll('ckNew', this);" /></th>
                                    <th>姓名</th>
                                    <th>性别</th>
                                    <th>专业</th>
                                </tr>
                                <tr id="itemPlaceholder" runat="server"></tr>
                            </table>
                        </LayoutTemplate>
                        <ItemTemplate>
                            <tr class="Single">
                                <td>
                                    <asp:CheckBox ID="ckNew" runat="server" /></td>
                                <asp:Label ID="lbID" runat="server" Text='<%# Eval("SFZJH") %>' Visible="false"></asp:Label>
                                <td><%#Eval("XM")%></td>
                                <td><%#Eval("XBM")%></td>
                                <td><%#Eval("ZYMC")%></td>
                            </tr>
                        </ItemTemplate>

                    </asp:ListView>
                </div>
                <div class="page">
                    <asp:DataPager ID="DataPager1" runat="server" PageSize="10" PagedControlID="ListView1">
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
</div>
