<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SA_wp_AllActivityUserControl.ascx.cs" Inherits="Sinp_StudentWP.WebParts.SA.SA_wp_AllActivity.SA_wp_AllActivityUserControl" %>
<link rel="stylesheet" href="/_layouts/15/Style/common.css" />
<link rel="stylesheet" href="/_layouts/15/Stu_css/allst.css">
<link rel="stylesheet" href="/_layouts/15/Stu_css/ico/iconfont.css">
<link href="/_layouts/15/Stu_css/StuAssociate.css" rel="stylesheet" />
<link href="/_layouts/15/Stu_css/st_index.css" rel="stylesheet">
<script src="/_layouts/15/Script/jquery-1.8.0.js"></script>
<script src="/_layouts/15/Script/popwin.js"></script>
<script type="text/javascript">
    $(function () {
        var intvalue = $("Input[id*='HF_TabFlag']").val();
        var loadindex = $("." + intvalue).parent().index();
        $("." + intvalue).parent().addClass("active").siblings().removeClass("active");
        $("." + intvalue).parents(".rflf").find(".td").eq(loadindex).show().siblings().hide();

        TabChange(".rflf .st_nav", 'active', '.rflf', '.td');
    })
    function TabChange(navcss, selectedcss, parcls, childcls) { //选项卡切换方法，navcss选项卡父级，selectedcss选中样式，parcls内容的父级样式，childcls内容样式
        $(navcss).find("a").click(function () {
            var index = $(this).parent().index();
            $(this).parent().addClass(selectedcss).siblings().removeClass(selectedcss);//为单击的选项卡添加选中样式，同时与当前选项卡同级的其他选项卡移除选中样式
            $(this).parents(parcls).find(childcls).eq(index).show().siblings().hide();//找到与选中选项卡相同索引的内容，使其展示，同时设置其他同级内容隐藏。

            $("Input[id*='HF_TabFlag']").val($(this).attr("class"));
        });
    }
    function openPage(pageTitle, pageName, pageWidth, pageHeight) {
        var webUrl = _spPageContextInfo.webAbsoluteUrl;
        popWin.showWin(pageWidth, pageHeight, pageTitle, webUrl + pageName);
    }
    function closePages() {
        $("#mask,#maskTop").fadeOut(function () {
            $(this).remove();
        });
    }
</script>
<style type="text/css">
    .sthd_list dl dd div ul li h2 {
        float: left;
        width: 140px;
        margin: 0 0 0 0;
        font-size: 14px;
        /*font-weight: normal;*/
        color: #0da6ec;
    }

    .sthd_list div ul li .sp_regist {
        width: 55px;
        height: 22px;
        margin: 3px 5px 0 0;
        line-height: 22px;
        text-align: center;
        border: solid 1px #00a1ea;
        border-radius: 3px;
        background: #11aaf0;
        float: right;
    }

    .sthd_list ul li .sp_regist a {
        color: #fff;
    }
</style>
<asp:HiddenField ID="HF_TabFlag" Value="first" runat="server" />
<div class="rflf" style="width: 100%">
    <div class="st_nav" style="width: 100%">
        <ul style="width: 100%;">
            <li class="active"><a href="#" class="first">全部活动</a></li>
            <li><a href="#" class="second" id="a_Regist">活动报名</a></li>
        </ul>
    </div>
    <div style="width: 100%; background: #fff;">
        <div class="td" id="first" style="display: none;">
            <div class="tabcontent">
                <div class="sthd_list" style="width: 100%;">
                    <dl>
                        <dd class="allxxk_list">
                            <asp:ListView ID="LV_AllActivity" runat="server" OnPagePropertiesChanging="LV_AllActivity_PagePropertiesChanging">
                                <EmptyDataTemplate>
                                    <table class="W_form">
                                        <tr>
                                            <td>暂无活动。</td>
                                        </tr>
                                    </table>
                                </EmptyDataTemplate>
                                <ItemTemplate>
                                    <ul>
                                        <li class="sb_fz01">
                                            <img src='<%# Eval("Activity_Pic") %>' alt="">
                                            <img class="zhuangtai" src='<%# Eval("StatusPic") %>' alt=""></li>
                                        <li class="sb_fz02">
                                            <div class="lf_tp">
                                                <img src='<%# Eval("Activity_Pic") %>' alt="">
                                                <h3><%# Eval("OrganizeUser") %></h3>
                                            </div>
                                            <div class="rf_xq">
                                                <h3><%# Eval("Title") %></h3>
                                                <p><%# Eval("Introduction") %></p>
                                            </div>
                                        </li>
                                        <li class="sb_fz03">
                                            <h2><%# Eval("Title") %></h2>
                                            <span><a href='ActivityDetailShow.aspx?itemid=<%# Eval("ID") %>'>报名</a></span>
                                        </li>
                                        <li class="sb_fz04"><%# Eval("OrganizeUser") %></li>
                                    </ul>
                                </ItemTemplate>
                            </asp:ListView>
                        </dd>
                    </dl>
                </div>
                <div class="page">
                    <asp:DataPager ID="DP_AllActivity" runat="server" PageSize="8" PagedControlID="LV_AllActivity">
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
        <div class="td" id="second" style="display: none;">
            <div class="Query">
                <ul>
                    <li>
                        <asp:DropDownList CssClass="option" ID="DDL_LearnYear" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DDL_LearnYear_SelectedIndexChanged"></asp:DropDownList>
                    </li>
                    <li>
                        <asp:DropDownList CssClass="option" ID="DDL_Activity" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DDL_Activity_SelectedIndexChanged"></asp:DropDownList>
                    </li>
                </ul>
            </div>
            <div class="content" style="padding: 0px 50px;">
                <asp:ListView ID="LV_ActivityRegist" runat="server" OnPagePropertiesChanging="LV_ActivityRegist_PagePropertiesChanging">
                    <EmptyDataTemplate>
                        <table class="W_form">
                            <tr class="trth">
                                <td colspan="3" style="text-align: center">暂无报名信息！
                                </td>
                            </tr>
                        </table>
                    </EmptyDataTemplate>
                    <ItemTemplate>
                        <div style="padding: 20px 0px;">
                            <h2 style="font-size: 16px; text-align: center;"><%# Eval("Title") %><asp:HiddenField ID="DetailID" runat="server" Value='<%# Eval("ID") %>' />
                                <asp:HiddenField ID="Hid_type" runat="server" Value='<%# Eval("Type") %>' />
                            </h2>
                            <p style="text-indent: 2em; line-height: 28px;">
                                <%# Eval("Introduction") %>
                            </p>
                        </div>

                        <div class="Topic_tcon">
                            <div id="slide">
                                <ul>
                                    <asp:ListView ID="LV_ProjectList" runat="server">
                                        <EmptyDataTemplate>
                                            <table class="W_form">
                                                <tr>
                                                    <td>暂无活动项目</td>
                                                </tr>
                                            </table>
                                        </EmptyDataTemplate>
                                        <ItemTemplate>
                                            <li>
                                                <div class="Topic_click" style="background: #eff8fd;">
                                                    <span class="Topic_tit fl"><span class="tit_name"><%# Eval("ProTitle") %></span></span>
                                                </div>
                                                <div class="Topic_con" style="background: #fff;">
                                                    <%# Eval("JoinMembers") %>
                                                </div>
                                            </li>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </ul>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:ListView>
                <div class="page">
                    <asp:DataPager ID="DP_ActivityRegist" PageSize="10" runat="server" PagedControlID="LV_ActivityRegist">
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
            </div>
        </div>
    </div>
</div>
