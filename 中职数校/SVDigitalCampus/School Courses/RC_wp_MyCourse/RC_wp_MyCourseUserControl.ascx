<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RC_wp_MyCourseUserControl.ascx.cs" Inherits="SVDigitalCampus.School_Courses.RC_wp_MyCourse.RC_wp_MyCourseUserControl" %>
<link href="../../../../_layouts/15/SVDigitalCampus/CSS/common.css" rel="stylesheet" />
<link href="../../../../_layouts/15/SVDigitalCampus/CSS/style.css" rel="stylesheet" />
<link href="../../../../_layouts/15/SVDigitalCampus/CSS/iconfont.css" rel="stylesheet" />
<link href="../../../../_layouts/15/SVDigitalCampus/CSS/animate.css" rel="stylesheet" />
<script src="../../../../_layouts/15/SVDigitalCampus/Script/jquery-1.8.2.min.js"></script>
<script src="../../../../_layouts/15/SVDigitalCampus/Script/Enter/tz_slider.js"></script>
<script src="../../../../_layouts/15/SVDigitalCampus/Script/Enter/uploadFile.js"></script>
<script src="../../../../_layouts/15/SVDigitalCampus/Script/JSBase.js"></script>
<script src="../../../../_layouts/15/SVDigitalCampus/Script/Enter/popwin.js"></script>

<style>
    .Title {text-decoration:underline; color:blue;}
</style>
<div class="School_library">
    <!--页面名称-->
    <h1 class="Page_name">我的课程</h1>
    <!--整个展示区域-->
    <div class="Whole_display_area">
        <div>
            <!--操作区域-->

            <div class="clear"></div>
            
            <!--展示区域-->
            <div class="Display_form">
                <div class="Resources_tab">                   
                    <div class="content">
                        <div class="tc">
                            <div class="Order_form">
                                <div class="Food_order">
                                    <asp:ListView ID="LV_TermList" runat="server" OnItemCommand="LV_TermList_ItemCommand" OnItemEditing="LV_TermList_ItemEditing" OnItemDataBound="LV_TermList_ItemDataBound">
                                        <EmptyDataTemplate>
                                            <table style="width: 100%">
                                                <tr>
                                                    <td>
                                                        <div class="kc_create">
                                                            <h1>尚未选择任何课程</h1>
                                                            <h2>进入"校本课程"节点选择要学习的课程吧！</h2>
                                                        </div>
                                                    </td>
                                                </tr>
                                            </table>
                                        </EmptyDataTemplate>
                                        <LayoutTemplate>
                                            <table id="itemPlaceholderContainer" style="width: 100%;" class="">

                                                <tr id="itemPlaceholder" runat="server"></tr>
                                            </table>
                                        </LayoutTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td>

                                                    <div class="kc_yylc">
                                                        <div class="kc_yylc_xq">
                                                            <img src='<%#Eval("ImageUrl") %>' alt="">
                                                            <h2>
                                                                <asp:LinkButton ID="LinkButton1" runat="server" CommandName="View" CommandArgument='<%#Eval("ID") %>' CssClass="Title"><%#Eval("Title") %></asp:LinkButton>
                                                            </h2>
                                                            <div class="tit_course"><span>主讲教师：<%#Eval("TeacherName")%></span></div>
                                                            <p><%#Eval("Introduc") %></p>
                                                        </div>
                                                        
                                                    </div>

                                                </td>
                                            </tr>
                                        </ItemTemplate>

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
