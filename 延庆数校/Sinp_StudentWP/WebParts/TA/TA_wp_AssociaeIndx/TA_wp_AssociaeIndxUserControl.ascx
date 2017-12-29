<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TA_wp_AssociaeIndxUserControl.ascx.cs" Inherits="Sinp_StudentWP.WebParts.TA.TA_wp_AssociaeIndx.TA_wp_AssociaeIndxUserControl" %>
<link rel="stylesheet" href="/_layouts/15/Stu_css/st_index.css">
<link href="/_layouts/15/Style/iconfont.css" rel="stylesheet" />
<link rel="stylesheet" href="/_layouts/15/Stu_css/ico/iconfont.css">
<link href="/_layouts/15/Stu_js/skitter/css/skitter.styles.css" rel="stylesheet" />
<script src="/_layouts/15/Script/jquery-1.8.0.js"></script>
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
</style>
<div class="st_join">
    <img src="/_layouts/15/Stu_images/zs28.jpg" alt="">
    <div class="st_join_bt">
        速度加入我们吧，小伙伴们都在等你呢……
				<a href="Association.aspx">加入社团</a>
    </div>
</div>
<div class="stindex">
    <div class="stindex_rflf" style="height: 270px; margin-bottom: 10px;">
        <dl>
            <dt>
                <span class="active">社团资讯</span>
                <span style="float: right;">更多+</span>
            </dt>
            <dd>
                <div class="div_newdoc">
                    <div style="float: left; width: 40%;">
                        <div class="box_skitter box_skitter_normal" style="border: solid 1px #ededed; box-shadow: 0 0 3px #dedede;">
                            <ul>
                                <asp:ListView ID="lv_PictureShow" runat="server">
                                    <EmptyDataTemplate>
                                        <table class="W_form">
                                            <tr>
                                                <td>暂无社团资讯</td>
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
                            <asp:ListView ID="lv_AssociNews" runat="server">
                                <EmptyDataTemplate>
                                    <table class="W_form">
                                        <tr>
                                            <td>暂无社团资讯</td>
                                        </tr>
                                    </table>
                                </EmptyDataTemplate>
                                <ItemTemplate>
                                    <li id='<%# Eval("ID") %>' style="clear:both;;line-height:28px;">                                        
                                        <span style="float: left; margin-left: 10px;">
                                            <span class="number <%# Eval("numclass")%>"><%# Eval("num")%></span> 
                                            <a href="#"><%# Eval("Title") %></a>   
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
                    <asp:ListView ID="lv_ActivityDoc" runat="server">
                        <EmptyDataTemplate>
                            <table class="W_form">
                                <tr>
                                    <td>暂无活动资料</td>
                                </tr>
                            </table>
                        </EmptyDataTemplate>
                        <ItemTemplate>
                            <li id='<%# Eval("ID") %>' style="clear:both;line-height:28px;">
                                <i class="iconfont" style="float:left;color:#cccccc;">&#xe640;</i><span style="float: left; margin-left: 10px;"><a href='<%# Eval("DocPath") %>'><%# Eval("Title") %></a></span><span style="float: right; margin-right: 10px;"><%# Eval("Date") %></span></li>
                        </ItemTemplate>
                    </asp:ListView>
                </ul>
            </dd>
        </dl>
    </div>
</div>
<div class="stindex">
    <div class="stindex_rflf">
        <dl>
            <dt>
                <span class="active">热门活动</span>
                <p><a href="javascript:void(0)">></a></p>
                <p><a href="javascript:void(0)"><</a></p>
            </dt>
            <dd>
                <asp:ListView ID="LV_TermList" runat="server">
                    <EmptyDataTemplate>
                        <table class="W_form">
                            <tr>
                                <td>暂无活动,赶紧筹备吧</td>
                            </tr>
                        </table>
                    </EmptyDataTemplate>
                    <ItemTemplate>
                        <div class="div_act">
                            <ul>
                                <li>
                                    <img src='<%# Eval("Activity_Pic") %>' alt=""></li>
                                <li>
                                    <h2><a href='ActivityShow.aspx?itemid=<%# Eval("ID") %>'><%# Eval("Title") %></a></h2>
                                </li>
                                <li><i class="iconfont">&#xe60c;</i><%# Eval("Associae") %></li>
                                <li><i class="iconfont">&#xe604;</i><%# Eval("Date") %></li>
                                <li><i class="iconfont">&#xe607;</i><%# Eval("Address") %></li>
                                <li><i class="iconfont">&#xe60b;</i><%# Eval("Count") %>人参加</li>
                            </ul>
                        </div>
                    </ItemTemplate>
                </asp:ListView>
            </dd>
        </dl>

    </div>
    <div class="stindex_rfrf">
        <dl>
            <dt>
                <span class="active">社团活跃度排名</span>
                <p><%--更多+--%></p>
            </dt>
            <dd>
                <ol>
                    <asp:ListView ID="SB_TermList" runat="server">
                        <EmptyDataTemplate>
                            <table class="W_form">
                                <tr>
                                    <td>无活跃度排名</td>
                                </tr>
                            </table>
                        </EmptyDataTemplate>
                        <ItemTemplate>
                            <li id='<%# Eval("ID") %>'>
                                <img src='<%# Eval("Ass_Pic") %>' alt="">
                                <div>
                                    <h3><a href='AssociationShow.aspx?itemid=<%# Eval("ID") %>'><%# Eval("Title") %></a></h3>
                                    <i class="iconfont">&#xe60b;</i><span><%# Eval("Count") %>人</span>
                                </div>
                                <p><%# Eval("Introduce") %></p>
                            </li>
                        </ItemTemplate>
                    </asp:ListView>
                </ol>
            </dd>
        </dl>
    </div>
</div>
