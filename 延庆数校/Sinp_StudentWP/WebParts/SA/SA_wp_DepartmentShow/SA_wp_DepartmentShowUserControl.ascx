<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SA_wp_DepartmentShowUserControl.ascx.cs" Inherits="Sinp_StudentWP.WebParts.SA.SA_wp_DepartmentShow.SA_wp_DepartmentShowUserControl" %>
<link href="/_layouts/15/Stu_css/st_index.css" rel="stylesheet">
<link rel="stylesheet" href="/_layouts/15/Stu_css/allst.css">
<link rel="stylesheet" href="/_layouts/15/Stu_css/ico/iconfont.css">
<script src="/_layouts/15/Script/jquery-1.8.0.js"></script>
<script src="/_layouts/15/Script/popwin.js"></script>
<script src="/_layouts/15/Stu_js/shetuan.js"></script>
<script type="text/javascript">
    $(function () {
        TabChange(".rflf .st_nav", "active", ".rflf", ".td");
        TabChange(".div_activies .activetab", "active", ".div_activies", ".twotd");
    })
    function TabChange(navcss, selectedcss, parcls, childcls) { //选项卡切换方法，navcss选项卡父级，selectedcss选中样式，parcls内容的父级样式，childcls内容样式
        $(navcss).find("a").click(function () {
            var index = $(this).parent().index();
            $(this).parent().addClass(selectedcss).siblings().removeClass(selectedcss);//为单击的选项卡添加选中样式，同时与当前选项卡同级的其他选项卡移除选中样式
            $(this).parents(parcls).find(childcls).eq(index).show().siblings().hide();//找到与选中选项卡相同索引的内容，使其展示，同时设置其他同级内容隐藏。
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
    function addAlbum(id, name) {
        $("#emptyAlbum").attr("style", "display:none");
        $("#ablum").prepend("<li id='" + id + "'><img src='/_layouts/15/Stu_images/nopic.png' alt=''/><span>0</span><h3>" + name + "</h3></li>");
    }
    function JudgeOpenWin() {
        var btntext = document.getElementById("<%=btn_apply.ClientID%>").innerHTML;
        var flag = btntext == "申请退部" ? "1" : "0";
        openPage(btntext, '/SitePages/RecruitApply.aspx?departid=<%=Department_ID %>&flag=' + flag, '650', '460');
    }
</script>
<div class="tz_sz" id="tz_nav" style='display: <%=Limit %>'>
    Hi,<a href="#"><asp:Literal ID="Lit_User" runat="server"></asp:Literal>部长</a>,这是属于你们自己的部门主页。您可以
			<a href="javascript:void(0)" onclick="openPage('设置部门首页图片','/SitePages/DepartmentHomePic.aspx?itemid=<%=Department_ID %>','700','420');return false;">设置部门首页图片</a>、
			<a href="javascript:void(0)" onclick="openPage('修改部门资料','/SitePages/AddDepartment.aspx?itemid=<%=Department_ID %>','650','420');return false;">修改部门资料</a>、
			<a href="javascript:void(0)" onclick="openPage('发布活动','/SitePages/AddActivity.aspx?departid=<%=Department_ID %>','700','660');return false;">发布活动</a>和
			<a href="javascript:void(0)" onclick="openPage('发布部门新闻','/SitePages/AddActivityNews.aspx?departid=<%=Department_ID %>','800','550');return false;">发布部门新闻</a>,把它充实起来吧！
			<a href="#" onclick="document.getElementById('tz_nav').style.display= 'none';"><i class="iconfont">&#xe601;</i></a>
</div>
<div class="st_tp">
    <img runat="server" id="headerPic" src="/_layouts/15/Stu_images/homepic.jpeg" alt="">
    <div>
        <img id="Associae_Pic" runat="server" src="/_layouts/15/Stu_images/nopic.png">
        <span>
            <asp:Literal ID="Lit_Title" runat="server"></asp:Literal></span>
    </div>
</div>
<div class="st_content">
    <div class="rfrf">
        <div class="st_js">
            <div class="st_jj">
                <h2>部门简介</h2>
                <div>
                    <asp:Literal ID="Lit_Introduce" runat="server"></asp:Literal>
                </div>
                <a href="javascript:void(0)" id="btn_apply" name="btn_apply" runat="server" class="applybtn" onclick="JudgeOpenWin();">申请加入</a>
            </div>
            <ul>
                <li>
                    <i class="iconfont">&#xe603;</i>
                    <span>部门成员</span>
                </li>
                <li><span>部长</span></li>
                <li>
                    <div>
                        <img src="/_layouts/15/TeacherImages/photo1.jpg" id="Leader_pic" runat="server">
                        <h2>
                            <asp:Literal ID="Lit_Leader" runat="server" /></h2>
                    </div>
                </li>
                <li><span>副部长</span></li>
                <li>
                    <asp:ListView ID="LV_TermList" runat="server">
                        <EmptyDataTemplate>
                            <table class="W_form">
                                <tr>
                                    <td>暂无副部长,赶紧申请吧</td>
                                </tr>
                            </table>
                        </EmptyDataTemplate>
                        <ItemTemplate>
                            <div>
                                <img src='<%# Eval("U_Pic") %>'>
                                <h2><%# Eval("Name") %></h2>
                            </div>
                        </ItemTemplate>
                    </asp:ListView>
                </li>
            </ul>
        </div>
        <div class="st_hd">
            <ol>
                <li>
                    <i class="iconfont">&#xe602;</i>
                    <span>部门活动</span>
                    <h2>更多 +</h2>
                </li>
                <asp:ListView ID="SB_TermList" runat="server">
                    <EmptyDataTemplate>
                        <table class="W_form">
                            <tr>
                                <td>您所在部门暂无活动</td>
                            </tr>
                        </table>
                    </EmptyDataTemplate>
                    <ItemTemplate>
                        <li id='<%# Eval("ID") %>'>
                            <img src='<%# Eval("Activity_Pic") %>'>
                            <h3><a href='ActivityShow.aspx?itemid=<%# Eval("ID") %>'><%# Eval("Title") %></a></h3>
                            <p>
                                <i class="iconfont">&#xe604;</i><%# Eval("Date") %>
                            </p>
                        </li>
                    </ItemTemplate>
                </asp:ListView>
            </ol>
        </div>
    </div>
    <div class="rflf">
        <div class="st_nav">
            <ul>
                <li class="active"><a href="#" class="first">首页</a></li>
                <li><a href="#" class="second">部门资料</a></li>
                <li><a href="#" class="three">部门活动</a></li>
                <li><a href="#" class="four">相册</a></li>
            </ul>
        </div>
        <div class="depart_dt">
            <div class="td" id="first">
                <div class="st_dt_title">
                    <h2>部门动态</h2>
                    <span><a href="#">全部动态</a></span>
                </div>
                <asp:ListView ID="News_TermList" runat="server">
                    <EmptyDataTemplate>
                        <table class="W_form">
                            <tr>
                                <td>暂无部门动态</td>
                            </tr>
                        </table>
                    </EmptyDataTemplate>
                    <ItemTemplate>
                        <ul>
                            <li>
                                <img class="item_img" src='<%# Eval("New_Pic") %>'>
                                <h3><%# Eval("Title") %></h3>
                                <div><%# Eval("Content") %></div>
                            </li>
                            <li id='<%# Eval("ID") %>'>
                                <p><i class="iconfont">&#xe604;</i><%# Eval("Date") %></p>
                                <span class="item_span">
                                    <i class="iconfont">&#xe605;</i><a href="">(0)赞</a>
                                    <i class="iconfont">&#xe606;</i><a href="">0条</a>
                                </span>
                            </li>
                        </ul>
                    </ItemTemplate>
                </asp:ListView>
                <asp:ListView ID="Photo_TermList" runat="server">
                    <EmptyDataTemplate>
                        <table class="W_form">
                            <tr>
                                <td>暂无部门动态</td>
                            </tr>
                        </table>
                    </EmptyDataTemplate>
                    <ItemTemplate>
                        <ol>
                            <li><a href=""><%# Eval("Editor") %></a><span>上传<%# Eval("Count") %>张照片到</span><a href=""><%# Eval("Title") %></a></li>
                            <li>
                                <%# Eval("Photo") %>
                            </li>
                            <li id='<%# Eval("Album_ID") %>'>
                                <p><i class="iconfont">&#xe604;</i><%# Eval("Date") %></p>
                                <span>
                                    <i class="iconfont">&#xe605;</i><a href="">(0)赞</a>
                                    <i class="iconfont">&#xe606;</i><a href="">0条</a>
                                </span>
                            </li>
                        </ol>
                    </ItemTemplate>
                </asp:ListView>
            </div>
            <div class="td" id="second" style="display: none;">
                <div class="st_zl">
                    <dl id="selectContent">
                        <dt>
                            <p></p>
                            <span>部门介绍</span></dt>
                        <dd class="st_zl_js ddHeight">
                            <asp:Literal ID="Literal2" runat="server"></asp:Literal><span class="more" id="more">展开</span></dd>
                    </dl>
                    <dl>
                        <dt>
                            <p></p>
                            <span>部门成员</span>(<asp:Literal ID="Literal3" runat="server"></asp:Literal>)</dt>
                        <dd>
                            <ol>
                                <asp:ListView ID="LV_MemberList" runat="server">
                                    <EmptyDataTemplate>
                                        <table class="W_form">
                                            <tr>
                                                <td>暂无成员,赶紧去招安吧</td>
                                            </tr>
                                        </table>
                                    </EmptyDataTemplate>
                                    <ItemTemplate>
                                        <li id='<%# Eval("ID") %>'>
                                            <img src='<%# Eval("Photo") %>'>
                                            <h3><%# Eval("Name") %></h3>
                                            <div>
                                                <%--<span><a class="iconfont" href="">&#xe601;</a></span>
                                                <p><a href="">任命副部长</a></p>--%>
                                            </div>
                                        </li>
                                    </ItemTemplate>
                                </asp:ListView>
                            </ol>
                        </dd>
                    </dl>
                </div>
            </div>
            <div class="td" id="three" style="display: none;">
                <div class="div_activies sthd_list">
                    <div class="activetab">
                        <dl>
                            <dt>
                                <span class="active"><a href="#">正在进行的</a></span>
                                <span><a href="#">已经结束的</a></span>
                            </dt>
                        </dl>
                    </div>
                    <div>
                        <div class="twotd">
                            <dl>
                                <dd class="allxxk_list">
                                    <asp:ListView ID="lv_Activeing" runat="server">
                                        <EmptyDataTemplate>
                                            <table class="W_form">
                                                <tr>
                                                    <td>暂无正在进行的活动</td>
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
                        <div class="twotd" style="display: none;">
                            <dl>
                                <dd>
                                    <asp:ListView ID="lv_ActiveOver" runat="server">
                                        <EmptyDataTemplate>
                                            <table class="W_form">
                                                <tr>
                                                    <td>暂无已经结束的活动</td>
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
                    </div>
                </div>
            </div>
            <div class="td" id="four" style="display: none;">
                <div class="st_xc">
                    <div class="st_xc_sc">
                        <a href="javascript:void(0)" onclick="openPage('上传照片','/SitePages/UploadDepartPhoto.aspx?itemid=<%=Department_ID %>&loginid=<%=SPContext.Current.Web.CurrentUser.ID %>&flag=2','700','570');return false;"><i class="iconfont">&#xe60a;</i>上传照片</a>
                        <a href="javascript:void(0)" onclick="openPage('创建相册','/SitePages/AddDepartmentAlbum.aspx?itemid=<%=Department_ID %>','460','200');return false;">创建相册</a>
                    </div>
                    <ul id="ablum">
                        <asp:ListView ID="Album_TermList" runat="server">
                            <EmptyDataTemplate>
                                <table class="W_form" id="emptyAlbum">
                                    <tr>
                                        <td>暂无部门相册</td>
                                    </tr>
                                </table>
                            </EmptyDataTemplate>
                            <ItemTemplate>
                                <li id='<%# Eval("Album_ID") %>' onclick="openPage('<%# Eval("Title") %>-相册详情','/SitePages/ShowDepartPhoto.aspx?itemid=<%=Department_ID %>&albumid=<%# Eval("Album_ID") %>','620','550');return false;">
                                    <img src='<%# Eval("Photo") %>' alt="">
                                    <span><%# Eval("Count") %></span>
                                    <h3><%# Eval("Title") %></h3>
                                </li>
                            </ItemTemplate>
                        </asp:ListView>
                    </ul>
                </div>
            </div>
        </div>
    </div>
</div>
