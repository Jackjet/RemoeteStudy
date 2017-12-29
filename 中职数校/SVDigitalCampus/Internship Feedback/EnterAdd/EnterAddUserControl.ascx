<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EnterAddUserControl.ascx.cs" Inherits="SVDigitalCampus.Internship_Feedback.EnterAdd.EnterAddUserControl" %>
<script src="../../../../_layouts/15/SVDigitalCampus/Script/jquery-1.8.2.min.js"></script>
<link href="../../../../_layouts/15/SVDigitalCampus/CSS/common.css" rel="stylesheet" />
<link type="text/css" rel="stylesheet" href="../../../../_layouts/15/SVDigitalCampus/CSS/style.css" />
<link type="text/css" rel="stylesheet" href="../../../../_layouts/15/SVDigitalCampus/CSS/iconfont.css" />
<link type="text/css" rel="stylesheet" href="../../../../_layouts/15/SVDigitalCampus/CSS/animate.css" />
<script src="../../../../_layouts/15/SVDigitalCampus/Script/Enter/tz_slider.js"></script>
<style type="text/css">
    .tip {
        color: red;
        display: block;
        position: absolute;
        font-size: 12px;
        line-height: 16px;
    }
</style>
<script type="text/javascript">
    function add(j) {
        var txtAttr = "bigcol";
        var textid = txtAttr + '' + j;
        var text = document.getElementById(textid).value;
        if (text.length > 0) {
            document.getElementById("bt" + j).style.display = "none";
            document.getElementById("del" + j).style.display = "inline";
            j++;
            // $("#addinfo").append("<input type='text' name='bigcol' id=" + 'bigcol' + j + " value=''  class='hu' placeholder=' 请输入实习岗位' />&nbsp;<input id=" + 'bt' + j + " type='button' value='+' onclick='add(" + j + ")' /><input type='button' id=" + 'del' + j + " value='-' class='del'  style='display:none'/><br/>");
            $("#addinfo").append("<input type='text' name='bigcol' id=" + 'bigcol' + j + " value=''  class='hu' placeholder=' 请输入实习岗位' />&nbsp;<a href='#' id=" + 'bt' + j + "  onclick='add(" + j + ")'><i class='iconfont tishi jia_t'>&#xe635;</i></a><a href='#' id=" + 'del' + j + " class='del'  style='display:none' onclick='del(" + j + ")'><i class='iconfont tishi jian_t'>&#xe634;</i></a><br/>");
        }
        else {
            alert("文本框有空值");
        }
    }
    function del(id) {
        $("#bigcol" + id).remove();
        $("#bt" + id).remove();
        $("#del" + id).remove();

    }
    $(document).ready(function () {
        //删除大列
        $('.del').live('click', function () {
            $(this).parent().remove();
        });

    });

</script>

<div class="yy_dialog" id="Adddiv" style="top: 0px; left: 0px;">
    <%--<div class="t_title">
        <h3 class="t_h3">添加企业信息</h3>
        <a href="#" class="t_close" onclick="closeDiv('Adddiv')">X</a>
    </div>--%>
    <%--<div id="Add_head" style="z-index: 1001; position: absolute;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</div>--%>

    <div class="t_content">
        <!---选项卡-->
        <div class="yy_tab">
            <div class="yy_tabheader">
                <ul>
                    <li id="qy" class="selected"><a href="#"><span class="zong_tit"><span class="num_1">1</span><span class="add_li">添加企业信息</span></span></a></li>
                    <li id="gw"><a href="#"><span class="zong_tit"><span class="num_2">2</span><span class="add_li">添加实习岗位</span></span></a></li>
                    <li id="comple"><a href="#"><span class="zong_tit"><span class="iconfont num_3">&#xe652;</span><span class="add_li">完成</span></span></a></li>
                </ul>
            </div>

            <div class="content">
                <div class="tc" style="display: block;">
                    <div class="t_message">
                        <div class="message_con">

                            <table class="m_top">
                                <tr>
                                    <td class="mi"><span class="m">企业名称：</span></td>
                                    <td class="ku">
                                        <input id="txtName" runat="server" class="hu" placeholder=" 请输入企业名称" onchange="confirmRepeat(this,'EnterName','企业名称重复')" />
                                        <i class=""></i>
                                        <span class="tip"></span>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="mi"><span class="m">企业账号：</span></td>
                                    <td class="ku">
                                        <input type="text" id="UserID" runat="server" class="hu" placeholder=" 请输入企业账号" onchange="confirmRepeat(this,'UserID','企业账号重复')">
                                        <i class=""></i>
                                        <span class="tip"></span>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="mi"><span class="m">密码：</span></td>
                                    <td class="ku">
                                        <input type="password" placeholder=" 请输入密码" id="UserPwd" runat="server" onchange="confirmE(this)">
                                        <i class=""></i></td>
                                </tr>
                            </table>
                            <table class="c_line">
                            </table>
                            <table class="m_bottom">
                                <tr>
                                    <td class="mi"><span class="m">负责人：</span></td>
                                    <td class="ku">
                                        <input type="text" class="hu" placeholder=" 请输入负责人名称" id="RelationName" runat="server" onchange="confirmE(this)">
                                        <i class=""></i></td>
                                </tr>
                                <tr>
                                    <td class="mi"><span class="m">电话：</span></td>
                                    <td class="ku">
                                        <input id="txtPhone" runat="server" placeholder=" 请输入电话" onchange="phone(this)" />
                                        <i class=""></i>
                                        <span class="tip"></span>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="mi"><span class="m">电子邮箱：</span></td>
                                    <td class="ku">
                                        <input id="tbEmail" runat="server" onchange="Email(this)" placeholder=" 请输入邮箱" />
                                        <i class=""></i>
                                        <span class="tip"></span>
                                    </td>
                                </tr>
                            </table>
                        </div>

                    </div>
                    <div class="t_btn">
                        <input type="button" value="下一步" onclick="yzqy()" />
                    </div>
                </div>
                <div class="tc" id="mc" style="display: none;">
                    <div class="t_message">
                        <div class="message_con">

                            <table class="m_top">
                                <tr>
                                    <td class="mi"><span class="m">实习岗位：</span></td>
                                    <td class="ku">
                                        <div id="addinfo" style="float: left;">
                                            <input type='text' name="bigcol" id="bigcol0" value='' class="hu" placeholder=" 请输入实习岗位" />
                                            <a href="#" id="bt0" onclick="add(0)"><i class="iconfont tishi jia_t">&#xe635;</i></a>
                                            <a href="#" id="del0" class="del" style="display: none" onclick="del(0)"><i class="iconfont tishi jian_t">&#xe634;</i></a>
                                            <br />
                                        </div>
                                        <input id="names" type="hidden" runat="server" value="" name="names" />
                                </tr>
                            </table>
                        </div>
                    </div>
                    <div class="t_btn">
                        <asp:Button ID="Add" runat="server" Text="下一步" OnClientClick="GetAddInput();" OnClick="Add_Click" />
                    </div>
                </div>

                <div class="tc" style="display: none;" id="wc" runat="server">
                    <p class="finish">
                        <span class="fl icon_f"><i class="iconfont finish_t">&#xe654;</i></span>
                        <span class="fr info_f">企业信息添加成功</span>
                    </p>
                </div>
            </div>
        </div>

    </div>
</div>
