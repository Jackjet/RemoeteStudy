<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ES_wp_ExaminationReleaseUserControl.ascx.cs" Inherits="SVDigitalCampus.Examination_System.ES_wp_ExaminationRelease.ES_wp_ExaminationReleaseUserControl" %>
<link type="text/css" rel="stylesheet" href="../../../../_layouts/15/SVDigitalCampus/CSS/common.css" />
<link type="text/css" rel="stylesheet" href="../../../../_layouts/15/SVDigitalCampus/CSS/style.css" />
<link type="text/css" rel="stylesheet" href="../../../../_layouts/15/SVDigitalCampus/CSS/iconfont.css" />
<link type="text/css" rel="stylesheet" href="../../../../_layouts/15/SVDigitalCampus/CSS/animate.css" />
<script src="../../../../_layouts/15/SVDigitalCampus/Script/jquery-1.8.2.min.js"></script>
<script src="../../../../_layouts/15/SVDigitalCampus/Script/tz_slider.js"></script>
<script src="../../../../_layouts/15/SVDigitalCampus/Script/popwin.js"></script>
<script src="../../../../_layouts/15/SVDigitalCampus/Script/DatePicker/WdatePicker.js"></script>
<div class="MakeExamination">
    <!--整个展示区域-->
    <div class="Whole_display_area">
        <div class="Release">
            <table class="tbDetail list_lform">
                <tr>
                    <td class="l_form mi">试卷：</td>
                    <td colspan="3">
                        <asp:Label ID="ExamPaperTit" runat="server" Text=""></asp:Label><asp:HiddenField ID="ExamPaperID" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td class="l_form mi">班级：</td>
                    <td colspan="3">
                        <div>
                            <table style="width: 100%;" class="xuanzeform">
                                <asp:ListView ID="lvClass" runat="server" ItemPlaceholderID="itemPlaceholder" GroupItemCount="3">
                                    <GroupTemplate>
                                        <tr>
                                            <asp:PlaceHolder ID="itemPlaceholder" runat="server"></asp:PlaceHolder>
                                        </tr>
                                    </GroupTemplate>
                                    <LayoutTemplate>
                                        <asp:PlaceHolder ID="groupPlaceholder" runat="server"></asp:PlaceHolder>
                                    </LayoutTemplate>
                                    <ItemTemplate>
                                        <td>
                                            <input id="ckClass" type="checkbox" runat="server" value='<%#Eval("BJBH") %>' name="ckClass"><%#Eval("BJ") %></td>
                                    </ItemTemplate>
                                </asp:ListView>
                            </table>
                        </div>
                       <%-- <div id="classs" class="classs" runat="server">

                            <input type="checkbox" id="CkAll" name="ckClass" value="0" runat="server" />所有
                        </div>--%>
                    </td>
                </tr>
                <tr>
                    <td class="l_form mi">有效开始时间：</td>
                    <td>
                        <input type="text" class="Wdate input timeheight" readonly="readonly" runat="server" id="WorkBeginTime" onclick="WdatePicker()" placeholder="请输入有效开始时间" /></td>

                    <td class="l_form">有效截止时间：</td>
                    <td>
                        <input type="text" class="Wdate input timeheight" readonly="readonly" runat="server" id="WorkEndTime" onclick="WdatePicker()" placeholder="请输入有效截止时间" /></td>
                </tr>
            </table>
            <div class="end">
                <asp:Button ID="Release" runat="server" CssClass="add" OnClick="Release_Click" Text="发布" />
            </div>
        </div>
    </div>
    <!--展示区域-->
</div>
