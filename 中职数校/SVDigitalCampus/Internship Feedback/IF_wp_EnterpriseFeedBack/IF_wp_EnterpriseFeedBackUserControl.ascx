<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="IF_wp_EnterpriseFeedBackUserControl.ascx.cs" Inherits="SVDigitalCampus.Internship_Feedback.IF_wp_EnterpriseFeedBack.IF_wp_EnterpriseFeedBackUserControl" %>
<script src="../../../../_layouts/15/SVDigitalCampus/Script/jquery-1.8.2.min.js"></script>
<script src="../../../../_layouts/15/SVDigitalCampus/Script/Enter/popwin.js"></script>
<link href="../../../../_layouts/15/SVDigitalCampus/CSS/common.css" rel="stylesheet" />
<link type="text/css" rel="stylesheet" href="../../../../_layouts/15/SVDigitalCampus/CSS/style.css" />
<link type="text/css" rel="stylesheet" href="../../../../_layouts/15/SVDigitalCampus/CSS/iconfont.css" />
<link type="text/css" rel="stylesheet" href="../../../../_layouts/15/SVDigitalCampus/CSS/animate.css" />
<link href="../../../../_layouts/15/SVDigitalCampus/Script/Enter/jquery-webox.css" rel="stylesheet" />
<script src="../../../../_layouts/15/SVDigitalCampus/Script/Enter/jquery-webox.js"></script>
<script src="../../../../_layouts/15/SVDigitalCampus/Script/Enter/tz_slider.js"></script>
<style type="text/css">
    .State span {
        border-radius: 12px;
        background: #dcdcdc;
        text-align: center;
        padding: 2px 0px;
        font-size: 12px;
    }

    .t_btn {
        color: #fff;
        width: 100px;
        border: none;
        border-radius: 3px;
        padding: 6px 0;
        font-size: 14px;
        font-family: "微软雅黑";
        background: #96cc66;
        cursor: pointer;
    }

    .feedback input {
        padding: 0 2px;
        margin-left: 5px;
        border-radius: 3px;
        line-height: 10px;
    }

    .feedback textarea {
        padding: 0 2px;
        margin-left: 2px;
        border-radius: 3px;
    }

    .feedback tr {
        height: 30px;
        line-height: 30px;
    }

    .feedback .btn {
        color: #fff;
        width: 100px;
        border: none;
        border-radius: 3px;
        padding: 6px 0;
        font-size: 14px;
        font-family: "微软雅黑";
        background: #96cc66;
        cursor: pointer;
    }
</style>
<div class="Enterprise_information_feedback">
    <!--页面名称-->
    <h1 class="Page_name">实习企业信息反馈管理</h1>
    <div class="Whole_display_area">
        <div class="Operation_area">
            <div class="left_choice fl">
                <ul>
                    <li class="Sear">
                        <input type="text" id="txtStudent" placeholder=" 请输入学生姓名" class="search" name="search" runat="server" /><i class="iconfont"><asp:LinkButton ID="LinkButton2" runat="server" OnClick="Button1_Click">&#xe62d;</asp:LinkButton></i>

                    </li>

                </ul>
            </div>
            <div class="right_add fr">
            </div>
        </div>

        <div class="State">
            <span class="qijin">
                <asp:LinkButton ID="btN" runat="server" OnClick="btN_Click" CssClass="Enable" ForeColor="White">未反馈学生信息</asp:LinkButton>
                <asp:LinkButton ID="btY" runat="server" OnClick="btY_Click" CssClass="Disable" ForeColor="White">已反馈学生信息</asp:LinkButton>
            </span>
        </div>

        <asp:Label ID="Label1" runat="server" Text="0" Visible="false"></asp:Label>
        <div class="Display_form">
            <asp:ListView ID="LV_TermList" runat="server" OnPagePropertiesChanging="LV_TermList_PagePropertiesChanging" OnItemCommand="LV_TermList_ItemCommand" OnItemDataBound="LV_TermList_ItemDataBound" OnItemEditing="LV_TermList_ItemEditing">
                <EmptyDataTemplate>
                    <table>
                        <tr>
                            <td colspan="3">暂无学生信息。</td>
                        </tr>
                    </table>
                </EmptyDataTemplate>
                <LayoutTemplate>
                    <table id="itemPlaceholderContainer" style="width: 100%;" class="D_form">
                        <tr class="trth">
                            <th>姓名</th>
                            <th>性别</th>
                            <th>实习岗位</th>
                            <th>创建时间</th>
                            <%--<th>反馈情况</th>--%>
                            <th>操作</th>
                        </tr>
                        <tr id="itemPlaceholder" runat="server"></tr>
                    </table>
                </LayoutTemplate>
                <ItemTemplate>
                    <tr class="Single">
                        <asp:Label ID="lbID" runat="server" Text='<%# Eval("ID") %>' Visible="false"></asp:Label>
                        <asp:Label ID="lbstuID" runat="server" Text='<%# Eval("StuID") %>' Visible="false"></asp:Label>
                        <asp:Label ID="lbEnterID" runat="server" Text='<%# Eval("EnterID") %>' Visible="false"></asp:Label>
                        <td>
                            <asp:Label ID="lbTitle" runat="server" Text='<%#Eval("Title")%>'></asp:Label></td>
                        <td>
                            <asp:Label ID="lbSex" runat="server" Text='<%#Eval("Sex")%>'></asp:Label></td>
                        <td>
                            <asp:Label ID="lbJob" runat="server" Text='<%#Eval("EJob")%>'></asp:Label></td>
                        <td><%#Eval("Created")%></td>
                        <%--<td>--%>
                        <asp:Label ID="IsCompleate" runat="server" Text='<%#Eval("IsCompleate").ToString()=="0"?"未反馈":"已反馈" %>' Visible="false"></asp:Label>
                        <%-- </td>--%>
                        <td class="Operation">
                            <asp:LinkButton ID="lbEdit" runat="server" CommandName="Edit" CommandArgument='<%#Eval("EnterID")%>'><i class="iconfont">&#xe629;</i></asp:LinkButton>
                            <asp:LinkButton ID="lbView" runat="server" CommandName="View" CommandArgument='<%#Eval("EnterID")%>' Visible="false"><i class="iconfont">&#xe651;</i></asp:LinkButton>
                        </td>
                    </tr>
                </ItemTemplate>
                <AlternatingItemTemplate>
                    <tr class="Double">
                        <asp:Label ID="lbID" runat="server" Text='<%# Eval("ID") %>' Visible="false"></asp:Label>
                        <asp:Label ID="lbstuID" runat="server" Text='<%# Eval("StuID") %>' Visible="false"></asp:Label>
                        <asp:Label ID="lbEnterID" runat="server" Text='<%# Eval("EnterID") %>' Visible="false"></asp:Label>
                        <td>
                            <asp:Label ID="lbTitle" runat="server" Text='<%#Eval("Title")%>'></asp:Label></td>
                        <td>
                            <asp:Label ID="lbSex" runat="server" Text='<%#Eval("Sex")%>'></asp:Label></td>
                        <td>
                            <asp:Label ID="lbJob" runat="server" Text='<%#Eval("EJob")%>'></asp:Label></td>
                        <td><%#Eval("Created")%></td>
                        <asp:Label ID="IsCompleate" runat="server" Text='<%#Eval("IsCompleate").ToString()=="0"?"未反馈":"已反馈" %>' Visible="false"></asp:Label>

                        <td class="Operation">
                            <asp:LinkButton ID="lbEdit" runat="server" CommandName="Edit" CommandArgument='<%#Eval("EnterID")%>'><i class="iconfont">&#xe629;</i></asp:LinkButton>
                            <asp:LinkButton ID="lbView" runat="server" CommandName="View" CommandArgument='<%#Eval("EnterID")%>' Visible="false"><i class="iconfont">&#xe651;</i></asp:LinkButton>

                        </td>
                    </tr>
                </AlternatingItemTemplate>
            </asp:ListView>


        </div>
    </div>
</div>
<div class="page">
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
<div id="Editdiv" class="blk" style="display: none; position: absolute; z-index: 1000; background: #67a3d9;">
    <div class="head">

        <div class="head-right"></div>
    </div>
    <div class="main" style="width: 600px; background-color: white;">
        <div style="height: 40px; background: #5493D7; line-height: 40px; font-size: 14px;">
            <span style="float: left; color: white; margin-left: 20px;">实习生鉴定表</span>
            <span style="float: right; margin-right: 10px;">
                <input type="button" value="X" onclick="closeDiv('Editdiv')" style="border-color: #5493D7; background-color: #5493D7; font-size: 16px; color: #fff;" /></span>
        </div>
        <div id="Edit_head" style="z-index: 1001; position: absolute;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</div>
        <div style="margin: 20px; background-color: white; border: 1px solid #5493D7; padding: 7px; margin-bottom: 1px;">
            <table class="feedback" runat="server" id="FeedBack">
                <tr>
                    <td align="center" colspan="6" style="font-weight: bold; font-size: 14px;">
                        <asp:Label ID="lbStuID" runat="server" Text="" Visible="false"></asp:Label>
                        <asp:Label ID="lbID" runat="server" Text="" Visible="false"></asp:Label>
                        <asp:Label ID="lbenter" runat="server" Text=""></asp:Label>实习生鉴定表                     
                    </td>
                </tr>
                <tr>
                    <td align="right" style="width: 21%;"></td>
                    <td style="width: 20%;"></td>
                    <td align="right" style="width: 15%;"></td>
                    <td style="width: 100px;"></td>
                    <td align="right" style="width: 15%;"></td>
                    <td></td>
                </tr>
                <tr>
                    <td align="right"></td>

                    <td colspan="5"></td>
                </tr>
                <tr>
                    <td align="right"></td>

                    <td colspan="5"></td>
                </tr>
                <tr>
                    <td align="right"></td>
                    <td colspan="5">&nbsp;</td>
                </tr>

                <tr>
                    <td align="center" colspan="6">&nbsp;</td>
                </tr>

                <tr>
                    <td colspan="6">
                        <br />

                    </td>
                </tr>
                <tr>
                    <td colspan="6" align="center">
                        <asp:Button ID="btOK" runat="server" Text="提交" OnClick="btOK_Click" CssClass="btn" /></td>
                </tr>
            </table>


        </div>
    </div>

</div>
