<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ChooseStudentUserControl.ascx.cs" Inherits="SVDigitalCampus.School_Courses.ChooseStudent.ChooseStudentUserControl" %>
<link href="../../../../_layouts/15/SVDigitalCampus/CSS/common.css" rel="stylesheet" />
<link href="../../../../_layouts/15/SVDigitalCampus/CSS/style.css" rel="stylesheet" />
<link href="../../../../_layouts/15/SVDigitalCampus/CSS/iconfont.css" rel="stylesheet" />
<link href="../../../../_layouts/15/SVDigitalCampus/CSS/animate.css" rel="stylesheet" />
<script src="../../../../_layouts/15/SVDigitalCampus/Script/jquery-1.8.2.min.js"></script>
<script src="../../../../_layouts/15/SVDigitalCampus/Script/Enter/tz_slider.js"></script>
<script src="../../../../_layouts/15/SVDigitalCampus/Script/Enter/uploadFile.js"></script>
<script src="../../../../_layouts/15/SVDigitalCampus/Script/JSBase.js"></script>
<script src="../../../../_layouts/15/SVDigitalCampus/Script/Enter/popwin.js"></script>

<div class="School_library">
    <!--页面名称-->
    <h1 class="Page_name">报名筛选</h1>
    <!--整个展示区域-->
    <div class="Whole_display_area">


        <div>
            <!--操作区域-->

            <div class="clear"></div>

            <div class="clear"></div>
            <!--展示区域-->
            <div class="Display_form">
                <div class="Resources_tab">

                    <div class="Operation_area">
                        <div class="left_choice fl">
                            <ul>
                                <li class="Sear"></li>
                                <li class="Sear">
                                    <input type="text" placeholder=" 请输入学生姓名" class="search" name="search" id="searchName" runat="server" /><i class="iconfont">&#xe62d;</i>
                                </li>
                            </ul>
                        </div>
                        <div class="right_add fr" id="opertion">
                        </div>
                    </div>
                    <div class="content">
                        <div class="tc">
                            <div class="Order_form">
                                <div class="Food_order">
                                    <asp:ListView ID="LV_TermList" runat="server" OnItemCommand="LV_TermList_ItemCommand" OnItemEditing="LV_TermList_ItemEditing" OnItemDataBound="LV_TermList_ItemDataBound">
                                        <EmptyDataTemplate>
                                            <table>
                                                <tr>
                                                    <td colspan="3">暂无报名信息。</td>
                                                </tr>
                                            </table>
                                        </EmptyDataTemplate>
                                        <LayoutTemplate>
                                            <table id="itemPlaceholderContainer" style="width: 100%;" class="D_form">
                                                <tr class="trth">
                                                    <th>序号</th>
                                                    <th>学生姓名</th>
                                                    <th>学生专业</th>
                                                    <th>报名课程</th>
                                                    <th>审核状态</th>
                                                    <th>报名时间</th>
                                                    <th>操作</th>
                                                </tr>
                                                <tr id="itemPlaceholder" runat="server"></tr>
                                            </table>
                                        </LayoutTemplate>
                                        <ItemTemplate>
                                            <tr class="Single">
                                                <td><%# Container.DataItemIndex + 1%></td>
                                                <td><%#Eval("XM")%></td>
                                                <td><%#Eval("ZYMC")%></td>
                                                <td><%#Eval("KC")%></td>
                                                <td>
                                                    <asp:Label ID="lbStatus" runat="server" Text='<%#Eval("Status").ToString()=="0"?"待审核":Eval("Status").ToString()=="1"?"审核通过":"审核失败"%>'></asp:Label>
                                                </td>

                                                <td><%#Eval("Created")%></td>
                                                <td>
                                                    <asp:LinkButton ID="lbCheckY" runat="server" CommandArgument='<%#Eval("ID") %>' CommandName="CheckY">通过</asp:LinkButton>
                                                    <asp:LinkButton ID="lbCheckN" runat="server" CommandArgument='<%#Eval("ID") %>' CommandName="CheckN">拒绝</asp:LinkButton>

                                                </td>

                                            </tr>
                                        </ItemTemplate>
                                        <AlternatingItemTemplate>
                                            <tr class="Double">
                                                <td><%# Container.DataItemIndex + 1%></td>
                                                <td><%#Eval("XM")%></td>
                                                <td><%#Eval("ZYMC")%></td>
                                                <td><%#Eval("KC")%></td>
                                                <td>
                                                    <asp:Label ID="lbStatus" runat="server" Text='<%#Eval("Status").ToString()=="0"?"待审核":Eval("Status").ToString()=="1"?"审核通过":"审核失败"%>'></asp:Label>
                                                </td>

                                                <td><%#Eval("Created")%></td>
                                                <td>
                                                    <asp:LinkButton ID="lbCheckY" runat="server" CommandArgument='<%#Eval("ID") %>' CommandName="CheckY">通过</asp:LinkButton>
                                                    <asp:LinkButton ID="lbCheckN" runat="server" CommandArgument='<%#Eval("ID") %>' CommandName="CheckN">拒绝</asp:LinkButton>
                                                </td>
                                            </tr>
                                        </AlternatingItemTemplate>
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
<div id="Messagediv" class="blk" style="display: none; position: absolute; z-index: 1000; background: #67a3d9;">
    <div class="heaMessaged">
        <div class="head-right"></div>
    </div>
    <div class="main" style="width: 300px; background-color: white; border: 1px solid #374760">
        <div style="height: 40px; background: #5493D7; line-height: 50px; font-size: 15px;">
            <span style="float: left; color: white; margin-left: 20px;">失败原因</span>
            <span style="float: right; margin-right: 20px;">
                <input type="button" value="X" onclick="closeDiv('Messagediv')" style="border-color: #5493D7; background-color: #5493D7; height: 30px" />
            </span>
        </div>
        <div id="Message_head" style="z-index: 1001; position: absolute;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</div>
        <div style="margin: 20px; border: 1px solid #374760">
            <table style="width: 100%; margin: 10px;">
                <tr style="border-bottom: 0">
                    <td class="lftd">拒绝原因：<asp:Label ID="lbID" runat="server" Text="" Visible="false"></asp:Label></td>
                    <td>
                        <input type="text" id="Message" class="search" runat="server" /></td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:Button ID="CheckN" runat="server" Text="确定" CssClass="b_add" OnClick="CheckN_Click" />
                </tr>
            </table>

        </div>
    </div>
</div>
