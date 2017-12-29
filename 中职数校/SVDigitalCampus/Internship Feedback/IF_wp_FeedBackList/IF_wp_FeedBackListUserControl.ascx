<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="IF_wp_FeedBackListUserControl.ascx.cs" Inherits="SVDigitalCampus.Internship_Feedback.IF_wp_FeedBackList.IF_wp_FeedBackListUserControl" %>
<script src="../../../../_layouts/15/SVDigitalCampus/Script/jquery-1.8.2.min.js"></script>
<script src="../../../../_layouts/15/SVDigitalCampus/Script/Enter/popwin.js"></script>
<link href="../../../../_layouts/15/SVDigitalCampus/CSS/common.css" rel="stylesheet" />
<link type="text/css" rel="stylesheet" href="../../../../_layouts/15/SVDigitalCampus/CSS/style.css" />
<link type="text/css" rel="stylesheet" href="../../../../_layouts/15/SVDigitalCampus/CSS/iconfont.css" />
<link type="text/css" rel="stylesheet" href="../../../../_layouts/15/SVDigitalCampus/CSS/animate.css" />
<link href="../../../../_layouts/15/SVDigitalCampus/Script/Enter/jquery-webox.css" rel="stylesheet" />
<script src="../../../../_layouts/15/SVDigitalCampus/Script/Enter/jquery-webox.js"></script>

<script src="../../../../_layouts/15/SVDigitalCampus/Script/Enter/tz_slider.js"></script>
<script type="text/javascript">
    function set() {
        $.webox({
            width: 600, height: 450, bgvisibel: true, title: '反馈表设置', iframe: '<%=rootUrl%>' + "/SitePages/FeedItem.aspx"
         });
       // popWin.showWin("600", "450", "反馈表设置", '<%=rootUrl%>' + "/SitePages/FeedItem.aspx", "no");
    }

</script>
<style type="text/css">
    .Selected {
        background: #5493D7;
        color: #FFFFFF;
    }

    .first_a {
        background: #5493D7;
        color: #FFFFFF;
    }

    .search {
        border-radius: 3px;
        border: 1px solid #5493d7;
        color: #bbb;
        height: 26px;
        line-height: 26px;
    }

    .sear i {
        margin-left: -22px;
        color: #5493d7;
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
<script type="text/javascript">
    $(window).resize(function () {
        var height = $("[id$='rptEnter']").height();
        if (height > 40) {
            $(".more").css("display", "none");
            //筛选条件经过展开
            $("#selectList").find(".more").toggle(function () {
                $(this).parent().parent().removeClass("dlHeight");
            }, function () {
                $(this).parent().parent().addClass("dlHeight");
            });
        }
        else {
            $("#Major").find(".more").remove();
        }
    });
</script>
<div class="Enterprise_information_feedback">
    <h1 class="Page_name">反馈信息管理</h1>

    <div class="Whole_display_area">
        <div class="Operation_area">
            <div class="left_choice fl">
                <ul>
                    <li class="">
                        <div class="qiehuan fl">
                            <span class="qijin"><a class="Enable" href="#">反馈详情</a>
                                <a class="Disable" href="<%=rootUrl %>/SitePages/FeedBackReport.aspx">图表统计</a></span>
                        </div>
                    </li>
                </ul>
            </div>
            <div class="right_add fr">
                <asp:LinkButton ID="lbset" runat="server" CssClass="add" OnClick="lbset_Click"><i class="iconfont">&#xe642;</i>反馈表设置</asp:LinkButton>
            </div>
        </div>
        <div class="tab_switch">
            <ul class="sw_tit">
                <li class="fl">实习企业</li>
                <li class="Sear fr">
                    <input type="text" id="txtName" placeholder=" 请输入公司名称" class="search" name="search" runat="server" /><i class="iconfont"><asp:LinkButton ID="LinkButton2" runat="server" OnClick="Button1_Click">&#xe62d;</asp:LinkButton></i>
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

                            <span class="more">
                                展开</span>
                        </dd>
                    </dl>
                    <dl class="listIndex" attr="terminal_brand_s" id="subjectdl" runat="server">
                        <dt>岗位</dt>
                        <dd>
                            <asp:Repeater ID="rptJob" runat="server" OnItemCommand="rptJob_ItemCommand1">
                                <ItemTemplate>
                                    <asp:Label ID="labJob" runat="server" Text='<%#Eval("ID") %>' Visible="false"></asp:Label>
                                    <asp:Label ID="labEnterID" runat="server" Text='<%#Eval("EnterID") %>' Visible="false"></asp:Label>

                                    <li>
                                        <asp:LinkButton ID="lbJob" runat="server" Text='<%#Eval("Title") %>' CommandName="Click" CommandArgument='<%#Eval("ID") %>'></asp:LinkButton>
                                    </li>
                                </ItemTemplate>
                            </asp:Repeater>

                        </dd>
                    </dl>
                </div>
            </div>
            <%-- <div class="sw_tabheader">
                <ul class="nav_list">
                    <asp:DataList ID="rptEnter" runat="server" OnItemCommand="rptEnter_ItemCommand" OnItemDataBound="rptEnter_ItemDataBound" RepeatColumns="3" RepeatLayout="Flow">
                        <ItemTemplate>
                            <asp:Label ID="lbsort" runat="server" Text='<%#Eval("Sorts") %>' Visible="false"></asp:Label>

                            <asp:Label ID="labEnter" runat="server" Text='<%#Eval("ID") %>' Visible="false"></asp:Label>
                            <li>
                                <asp:LinkButton ID="lbEnter" runat="server" Text='<%#Eval("Title") %>' CommandName="Click" CommandArgument='<%#Eval("ID") %>'></asp:LinkButton>
                            </li>
                        </ItemTemplate>
                    </asp:DataList>

                    <li class="last">
                        <asp:LinkButton ID="btmore" runat="server" CssClass="first_nav" OnClick="btmore_Click">查看更多<i class="iconfont"> &#xe605;</i></asp:LinkButton>

                        <div class="second_nav" style="display: none;">
                            <asp:DataList ID="DataList1" runat="server" OnItemCommand="DataList1_ItemCommand">
                                <ItemTemplate>
                                    <asp:Label ID="lbsort" runat="server" Text='<%#Eval("Sorts") %>' Visible="false"></asp:Label>

                                    <asp:Label ID="labEnter" runat="server" Text='<%#Eval("ID") %>' Visible="false"></asp:Label>
                                    <asp:LinkButton ID="lbEnter" CssClass="ng-binding ng-scope" runat="server" Text='<%#Eval("Title") %>' CommandName="Click" CommandArgument='<%#Eval("ID") %>'></asp:LinkButton>
                                </ItemTemplate>
                            </asp:DataList>

                        </div>
                    </li>
                </ul>
            </div>
            <div class="content">
                <div class="sw">
                    <dl class="Position">
                        <asp:DataList ID="rptJob" runat="server" OnItemCommand="rptJob_ItemCommand" OnItemDataBound="rptJob_ItemDataBound" Style="width: 100%; margin: 10px 20px;" RepeatColumns="10" RepeatLayout="Flow">
                            <ItemTemplate>
                                <asp:Label ID="labJob" runat="server" Text='<%#Eval("ID") %>' Visible="false"></asp:Label>
                                <asp:Label ID="labEnterID" runat="server" Text='<%#Eval("EnterID") %>' Visible="false"></asp:Label>

                                <dd>
                                    <asp:LinkButton ID="lbJob" runat="server" Text='<%#Eval("Title") %>' ForeColor="Black" CommandName="Click" CommandArgument='<%#Eval("ID") %>'></asp:LinkButton>
                                </dd>
                            </ItemTemplate>
                        </asp:DataList>
                    </dl>
                </div>

            </div>--%>
        </div>
        <div class="clear"></div>
        <div class="">

            <div class="Operation_area">
                <div class="left_choice fl">
                    <ul>
                        <li class="State">
                            <span class="qijin">
                                <asp:LinkButton ID="ncompleate" runat="server" OnClick="ncompleate_Click" CssClass="Enable" ForeColor="White">未反馈</asp:LinkButton>
                                <asp:LinkButton ID="compleate" runat="server" OnClick="compleate_Click" CssClass="Disable" ForeColor="White">已反馈</asp:LinkButton>

                            </span>
                        </li>
                        <%--<li class="Select">
                            <select class="option">
                                <option>所有专业</option>
                                <option>平面设计</option>
                                <option>UI设计</option>
                                <option>研发工程师</option>
                                <option>产品经理</option>
                            </select>
                        </li>--%>
                    </ul>
                </div>
                <div class="Sear fr">
                    <input type="text" name="search" class="search" placeholder=" 请输入姓名" runat="server" id="StuName"><i class="iconfont" style="margin-left: -22px; color: #5493d7;"><asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click">&#xe62d;</asp:LinkButton></i>
                </div>
            </div>
        </div>

        <asp:Label ID="Label1" runat="server" Text="0" Visible="false"></asp:Label>
        <asp:Label ID="lbEnter" runat="server" Text="" Visible="false"></asp:Label>
        <asp:Label ID="lbJob" runat="server" Text="" Visible="false"></asp:Label>

        <div class="Display_form">
            <asp:ListView ID="ListView1" runat="server" OnPagePropertiesChanging="ListView1_PagePropertiesChanging" Style="width: 100%;" OnItemDataBound="ListView1_ItemDataBound" OnItemCommand="ListView1_ItemCommand" OnItemEditing="ListView1_ItemEditing">
                <EmptyDataTemplate>
                    <table>
                    </table>
                </EmptyDataTemplate>
                <LayoutTemplate>
                    <table id="itemPlaceholderContainer" style="width: 100%;" class="D_form">
                        <tr class="trth">
                            <th>选择</th>
                            <th>姓名</th>
                            <th>性别</th>
                            <th>实习岗位</th>
                            <th>实习时间</th>
                            <th>操作</th>
                        </tr>
                        <tr id="itemPlaceholder" runat="server"></tr>
                    </table>
                </LayoutTemplate>
                <ItemTemplate>
                    <tr class="Single">
                        <td>
                            <asp:CheckBox ID="ckNew" runat="server" />
                            <asp:Label ID="lbID" runat="server" Text='<%# Eval("ID") %>' Visible="false"></asp:Label>
                            <asp:Label ID="lbstuID" runat="server" Text='<%# Eval("StuID") %>' Visible="false"></asp:Label>
                            <asp:Label ID="lbEnterID" runat="server" Text='<%# Eval("EnterID") %>' Visible="false"></asp:Label></td>

                        <td>
                            <asp:Label ID="lbTitle" runat="server" Text='<%#Eval("Title")%>'></asp:Label></td>
                        <td>
                            <asp:Label ID="lbSex" runat="server" Text='<%#Eval("Sex")%>'></asp:Label></td>
                        <td>
                            <asp:Label ID="lbJob" runat="server" Text='<%#Eval("EJob")%>'></asp:Label></td>
                        <td><%#Eval("Created")%></td>
                        <asp:Label ID="IsCompleate" runat="server" Text='<%#Eval("IsCompleate").ToString()=="1"?"已反馈":"未反馈"%>' Visible="false"></asp:Label>
                        </td>
                        <td class="Operation">
                            <asp:LinkButton ID="lbView" runat="server" CommandName="View" CommandArgument='<%#Eval("EnterID")%>' Visible="false"> 
                                <i class="iconfont">&#xe651;</i></asp:LinkButton>
                        </t>
                    </tr>
                </ItemTemplate>
                <AlternatingItemTemplate>
                    <tr class="Double">
                        <td>
                            <asp:CheckBox ID="ckNew" runat="server" />
                            <asp:Label ID="lbID" runat="server" Text='<%# Eval("ID") %>' Visible="false"></asp:Label>
                            <asp:Label ID="lbstuID" runat="server" Text='<%# Eval("StuID") %>' Visible="false"></asp:Label>
                            <asp:Label ID="lbEnterID" runat="server" Text='<%# Eval("EnterID") %>' Visible="false"></asp:Label></td>

                        <td>
                            <asp:Label ID="lbTitle" runat="server" Text='<%#Eval("Title")%>'></asp:Label></td>
                        <td>
                            <asp:Label ID="lbSex" runat="server" Text='<%#Eval("Sex")%>'></asp:Label></td>
                        <td>
                            <asp:Label ID="lbJob" runat="server" Text='<%#Eval("EJob")%>'></asp:Label></td>
                        <td><%#Eval("Created")%></td>
                        <asp:Label ID="IsCompleate" runat="server" Text='<%#Eval("IsCompleate").ToString()=="1"?"已反馈":"未反馈"%>' Visible="false"></asp:Label>
                        </td>
                        <td class="Operation">
                            <asp:LinkButton ID="lbView" runat="server" CommandName="View" CommandArgument='<%#Eval("EnterID")%>' Visible="false">
                            <i class="iconfont">&#xe651;</i></asp:LinkButton>
                        </td>
                    </tr>
                </AlternatingItemTemplate>
            </asp:ListView>
        </div>
    </div>
</div>
<div class="page">
    <asp:DataPager ID="DataPager1" runat="server" PageSize="8" PagedControlID="ListView1">
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

<div id="Viewdiv" class="blk" style="display: none; position: absolute; z-index: 1000; background: #5493D7;">
    <div class="head">
        <div class="head-right"></div>
    </div>
    <div class="main" style="width: 600px; background-color: white; border: 1px solid #5493D7; box-shadow: 0 0 5px #999;">
        <div style="height: 40px; background: #5493D7; line-height: 40px; font-size: 14px;">
            <span style="float: left; color: white; margin-left: 20px; font-size: 16px;">反馈表预览</span>
            <span style="float: right; margin-right: 10px; line-height:34px;">
                <input type="button" value="X" onclick="closeDiv('Viewdiv')" style="border-color: #5493D7; background-color: #5493D7; height: 40px; font-size: 16px; color:#fff;" /></span>
        </div>
        <div id="View_head" style="z-index: 1001; position: absolute;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</div>
        <div style="padding: 20px;">
            <table class="feedback" id="FeedBack" runat="server">
                <tr>
                    <td align="center" colspan="6" style="font-weight: bold; font-size: 16px;">
                        <asp:Label ID="lbresult" runat="server" Text="" Visible="false"></asp:Label>
                        <asp:Label ID="lbStuID" runat="server" Text="" Visible="false"></asp:Label>
                        <asp:Label ID="lbID" runat="server" Text="" Visible="false"></asp:Label>
                        <asp:Label ID="lben" runat="server" Text=""></asp:Label>实习生鉴定表                     
                               
                    </td>
                </tr>
                <tr>
                    <td style="width: 20%; text-align:left;"></td>
                    <td style="width: 20%;"></td>
                    <td style="width: 15%; text-align:right;"></td>
                    <td style="width: 20%"></td>
                    <td style="width: 15%; text-align:right;"></td>
                    <td></td>
                </tr>
                <tr>
                    <td style="text-align: left;"></td>
                    <td colspan="5"></td>
                </tr>
                <tr>
                    <td style="text-align: left;"></td>
                    <td colspan="5"></td>
                </tr>
                <tr>
                    <td style="text-align: left;"></td>
                    <td colspan="5"></td>
                </tr>

                <tr>
                    <td style="text-align: center;" colspan="6"></td>
                </tr>

                <tr>
                    <td colspan="6"></td>
                </tr>
                <tr>
                    <td colspan="6" style="text-align: center;">
                        <input type="button" class="btn" value="提交" />
                        <input type="reset" class="btn" value="取消重填" />
                    </td>
                </tr>
            </table>
        </div>
    </div>

</div>
<%--<div id="Editdiv" class="blk" style="display: none; position: absolute; z-index: 1000; background: #67a3d9;">
    <div class="head">

        <div class="head-right"></div>
    </div>
    <div class="main" style="width: 600px; background-color: white; border: 1px solid #374760">
        <div style="height: 40px; background: #5493D7; line-height: 40px; font-size: 14px;">
            <span style="float: left; color: white; margin-left: 20px;">实习生鉴定表</span>
            <span style="float: right; margin-right: 10px;">
                <input type="button" value="X" onclick="closeDiv('Editdiv')" style="border-color: #5493D7; background-color: #5493D7; height: 40px" /></span>
        </div>
        <div id="Edit_head" style="z-index: 1001; position: absolute;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</div>
        <div style="margin: 20px; border: 1px solid #374760">
            <table class="feedback" id="FeedBack" runat="server">
                <tr>
                    <td align="center" colspan="6" style="font-weight: bold; font-size: 14px;">
                        <asp:Label ID="lbresult" runat="server" Text="" Visible="false"></asp:Label>
                        <asp:Label ID="lbStuID" runat="server" Text="" Visible="false"></asp:Label>
                        <asp:Label ID="lbID" runat="server" Text="" Visible="false"></asp:Label>
                        <asp:Label ID="lben" runat="server" Text=""></asp:Label>实习生鉴定表                     
                    </td>
                </tr>
                <tr>
                    <td align="right" style="width: 22%;"></td>
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

            </table>


        </div>
    </div>

</div>--%>


