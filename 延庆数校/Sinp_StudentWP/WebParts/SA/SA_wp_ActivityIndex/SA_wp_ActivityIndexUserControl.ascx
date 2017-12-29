<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SA_wp_ActivityIndexUserControl.ascx.cs" Inherits="Sinp_StudentWP.WebParts.SA.SA_wp_ActivityIndex.SA_wp_ActivityIndexUserControl" %>
<link href="/_layouts/15/Stu_css/st_index.css" rel="stylesheet">
<link href="/_layouts/15/Style/iconfont.css" rel="stylesheet" />
<link rel="stylesheet" href="/_layouts/15/Stu_css/ico/iconfont.css">
<link href="/_layouts/15/Stu_js/skitter/css/skitter.styles.css" rel="stylesheet" />
<script src="/_layouts/15/Script/jquery-1.8.0.js"></script>
<script src="/_layouts/15/Script/popwin.js"></script>
<script src="/_layouts/15/Stu_js/skitter/jquery.easing.1.3.js"></script>
<script src="/_layouts/15/Stu_js/skitter/jquery.skitter.min.js"></script>
<!-- Init Skitter -->
<script type="text/javascript">
    $(document).ready(function () {
        $('.box_skitter_normal').skitter({
            theme: 'clean',
            numbers_align: 'center',
            progressbar: false,
            hideTools: true
        });
    });
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
    .box_skitter_normal {
        width: 100%;
        height: 201px;
    }

    .box_skitter .prev_button {
        left: 0px;
    }

    .box_skitter .next_button {
        right: 0px;
    }

    .box_skitter .image img {
        display: none;
        width: 100%;
        height: 201px;
    }

    .box_skitter .label_skitter {
        opacity: 0.6;
    }

        .box_skitter .label_skitter p {
            color: #fff;
            text-align: center;
            padding: 5px;
        }

    .btn_addnews {
        width: 87px;
        height: 29px;
        line-height: 29px;
        text-align: center;
        color: #fff;
        background: #0da6ec;
        border-radius: 5px;
        display: block;
        float: right;
    }
</style>
<div class="stindex">
    <div class="stindex_rflf" style="height: 270px; margin-bottom: 10px;">
        <dl>
            <dt>
                <span class="active">最新导读</span>
                <span style="float: right;">更多+</span>
            </dt>
            <dd>
                <div class="div_newdoc">
                    <div style="float: left; width: 40%;">
                        <div class="box_skitter box_skitter_normal" style="border: solid 1px #ededed; box-shadow: 0 0 3px #dedede;">
                            <ul>
                                <asp:ListView ID="LV_NewPicture" runat="server">
                                    <EmptyDataTemplate>
                                        <table class="W_form">
                                            <tr>
                                                <td>暂无部门新闻</td>
                                            </tr>
                                        </table>
                                    </EmptyDataTemplate>
                                    <ItemTemplate>
                                        <li>
                                            <a href="#">
                                                <img src='<%# Eval("New_Pic") %>' class="cubeRandom" style="width: 100%; height: 201px;" /></a>
                                            <div class="label_text">
                                                <p><%# Eval("Title") %></p>
                                            </div>
                                        </li>
                                    </ItemTemplate>
                                </asp:ListView>
                            </ul>
                        </div>
                    </div>
                    <div style="float: left; width: 60%;">
                        <ol>
                            <asp:ListView ID="LV_ActivityNews" runat="server">
                                <EmptyDataTemplate>
                                    <table class="W_form">
                                        <tr>
                                            <td>暂无部门新闻</td>
                                        </tr>
                                    </table>
                                </EmptyDataTemplate>
                                <ItemTemplate>
                                    <li id='<%# Eval("ID") %>' style="clear: both; line-height: 28px;">
                                        <span style="float: left; margin-left: 10px;">
                                            <span class="number <%# Eval("numclass")%>"><%# Eval("num")%></span>
                                            <a href='ActivityNewsShow.aspx?itemid=<%# Eval("ID") %>'><%# Eval("Title") %></a>
                                        </span>
                                        <span style="float: right; margin-right: 10px;"><%# Eval("Date") %></span>
                                    </li>
                                </ItemTemplate>
                            </asp:ListView>
                        </ol>
                    </div>
                </div>
            </dd>
        </dl>

    </div>
    <div class="stindex_rfrf" style="height: 270px; margin-bottom: 10px;">
        <dl>
            <dt>
                <span class="active">活动资料</span>
                <span style="float: right;">更多+</span>
            </dt>
            <dd>
                <ul>
                    <asp:ListView ID="LV_ActivityDoc" runat="server">
                        <EmptyDataTemplate>
                            <table class="W_form">
                                <tr>
                                    <td>暂无活动资料</td>
                                </tr>
                            </table>
                        </EmptyDataTemplate>
                        <ItemTemplate>
                            <li id='<%# Eval("ID") %>' style="clear: both; line-height: 28px;">
                                <i class="iconfont" style="float: left; color: #cccccc;">&#xe640;</i><span style="float: left; margin-left: 10px;"><a target='_blank' href='<%# Eval("DocPath") %>'><%# Eval("Title") %></a></span><span style="float: right; margin-right: 10px;"><%# Eval("Date") %></span></li>
                        </ItemTemplate>
                    </asp:ListView>
                </ul>
            </dd>
        </dl>
    </div>
</div>
<div class="stindex">
    <div class="stindex_rflf">
        <dl class="hot_kc">
            <dt class="ty_biaoti_hd">
                <span class="active">活动</span>
                <span style="float: right;">更多+</span>
            </dt>
            <dd class="allxxk_list">
                <asp:ListView ID="LV_HotActivity" runat="server">
                    <EmptyDataTemplate>
                        <table class="W_form">
                            <tr>
                                <td>暂无活动,赶紧筹备吧</td>
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
    <div class="stindex_rfrf">
        <dl>
            <dt>
                <span class="active">部门通知</span>
                <span style="float: right;">更多+</span>
            </dt>
            <dd>
                <ol>
                </ol>
            </dd>
        </dl>
    </div>
</div>
