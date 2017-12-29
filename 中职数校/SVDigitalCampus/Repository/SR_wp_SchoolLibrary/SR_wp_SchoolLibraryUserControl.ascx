<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SR_wp_SchoolLibraryUserControl.ascx.cs" Inherits="SVDigitalCampus.Repository.SR_wp_SchoolLibrary.SR_wp_SchoolLibraryUserControl" %>
<link href="../../../../_layouts/15/SVDigitalCampus/CSS/common.css" rel="stylesheet" />
<link href="../../../../_layouts/15/SVDigitalCampus/CSS/style.css" rel="stylesheet" />
<link href="../../../../_layouts/15/SVDigitalCampus/CSS/iconfont.css" rel="stylesheet" />
<link href="../../../../_layouts/15/SVDigitalCampus/CSS/animate.css" rel="stylesheet" />
<script src="../../../../_layouts/15/SVDigitalCampus/Script/jquery-1.8.2.min.js"></script>
<link href="../../../../_layouts/15/SVDigitalCampus/Script/Enter/jquery-webox.css" rel="stylesheet" />
<script src="../../../../_layouts/15/SVDigitalCampus/Script/Enter/jquery-webox.js"></script>

<script src="../../../../_layouts/15/SVDigitalCampus/Script/Enter/tz_slider.js"></script>
<script src="../../../../_layouts/15/SVDigitalCampus/Script/Enter/uploadFile.js"></script>

<script src="../../../../_layouts/15/SVDigitalCampus/Script/Enter/popwin.js"></script>

<link href="../../../../_layouts/15/SVDigitalCampus/test/type.css" rel="stylesheet" />
<link href="../../../../_layouts/15/SVDigitalCampus/test/Pager.css" rel="stylesheet" />
<script src="../../../../_layouts/15/SVDigitalCampus/test/Pager.js"></script>
<script src="../../../../_layouts/15/SVDigitalCampus/test/json.js"></script>
<script src="../../../../_layouts/15/SVDigitalCampus/test/VocationalEducation.js"></script>
<script src="../../../../_layouts/15/SVDigitalCampus/test/FormatUtil.js"></script>
<script src="../../../../_layouts/15/SVDigitalCampus/Script/Drive/SchoolLibrary.js"></script>
<style type="text/css">
    .hid {
        display: none;
    }

    .fileup {
        border-radius: 3px;
        background-color: #0DA6EC;
        color: white;
        border: 0px;
    }

    .shenhe {
        display: block;
        padding: 0px 4px;
        background: #5493d7;
        border-radius: 3px;
        color: #fff;
        float: left;
        margin: 0 4px;
        height: 20px;
        line-height: 20px;
    }

    .selected {
        display: inline-block;
        background: #abd0f5;
        color: #fff;
    }

    .star {
        padding: 0;
        margin: 0;
        list-style: none;
        float: left;
    }

    .star li {
        float: left;
        height: 20px;
        width: 20px;
        margin-right: 4px;
    }
    .star li.on {
        color:#f60;
    }
    .star li.off {
        color:#ccc;
    }
</style>


<!--校本资源库-->

<div class="School_library">
    <input id="hMajor" type="hidden" />
    <input id="hSubject" type="hidden" runat="server" />
    <input id="hContent" type="hidden" runat="server" />
    <input id="Hidden2" type="hidden" />
    <input id="Hidden3" type="hidden" />
    <input id="HFoldUrl" type="hidden" runat="server" />
    <input id="HStatus" type="hidden" value="" runat="server" />
    <input id="HAdmin" type="hidden" value="" runat="server" />

    <!--页面名称-->
    <h1 class="Page_name">校本资源库</h1>

    <div class="Whole_display_area">
        <div class="S_conditions">

            <div id="selectList" class="screenBox screenBackground">
                <dl class="listIndex dlHeight" attr="terminal_brand_s">
                    <dt>专业</dt>
                    <dd id="Major"></dd>
                </dl>
                <dl class="listIndex" attr="terminal_brand_s">
                    <dt>学科</dt>
                    <dd id="SubJect"></dd>
                </dl>
            </div>
        </div>
        <div class="clear"></div>
        <div class="Schoolcon_wrap">
            <div class="left_navcon fl">
                <h1>课程</h1>
                <h3 class="tit" style="background: #F6F6F6" id="quanbu" onclick="navTopClick()">
                    <i class="icon" style="background: #0da6ea; border: 1px solid #0da6ea"></i>全部
                    <i class="iconfont" style="cursor: pointer; display: none" id="toptianjia" onclick="AddMenuTop('')">&#xe630;</i></h3>
                <div class="select-box" id="Tree">
                </div>
            </div>
            <div class="right_dcon fr">
                <!--操作区域-->
                <div class="Operation_area">
                    <div class="left_choice fl">
                        <ul>
                            <li class="Sear">
                                <input type="text" placeholder=" 请输入关键字" class="search" name="search" id="txtName" /><i class="iconfont" id="btnQuery" onclick="btnQuery()">&#xe62d;</i>
                            </li>
                        </ul>
                    </div>
                    <div class="right_add fr" id="opertion" style="display: none">
                        <a href="#" class="add" onclick="uploader()"><i class="iconfont">&#xe65a;</i>上传文件</a>
                        <a href="#" class="add" onclick="javascript: appendAfterRow('tab', '1');" id="xjwjj" style="display: none"><i class="iconfont">&#xe601;</i>新建文件夹</a>
                        <div class="Batch_operation fl" id="plcz" style="display: none">
                            <a href="#" class="add"><i class="iconfont">&#xe602;</i>批量操作</a>
                            <ul class="B_con" style="display: none;">
                                <li><a href="#" onclick="Move('')">移动到</a></li>
                                <li><a href="#" onclick="Move()">复制到</a></li>
                                <li><a href="#" onclick="DelMore()">删除</a></li>
                                <li><a href="#" onclick="Down('')">下载</a></li>
                            </ul>

                        </div>
                        <a href="#" class="add" onclick="shaixuan()"><i class="iconfont">&#xe630;</i>筛选</a>

                    </div>
                </div>
                <div class="clear"></div>
                <div class="File_type" style="display: none;">
                    <dl class="F_type" id="F_type">
                    </dl>
                    <div class="clear"></div>
                    <dl class="F_time">
                        <dt>上传时间：</dt>
                        <dd class="selected"><a href="#" onclick="serchTime('',this)">不限</a></dd>
                        <dd><a href="#" onclick="serchTime('一周内',this)">一周内</a></dd>
                        <dd><a href="#" onclick="serchTime('一月内',this)">一月内</a></dd>
                        <dd><a href="#" onclick="serchTime('半年内',this)">半年内</a></dd>
                    </dl>
                </div>
                <div class="clear"></div>
                <!--展示区域-->
                <div class="Display_form">
                    <div class="Resources_tab">
                        <div class="Resources_tabheader">
                            <ul class="resourses">
                                <li id="all" class="selected"><a href="#" onclick="TabSel('0')"><span class="zong_tit"><span class="add_li">全部</span></span></a></li>
                                <li><a href="#" onclick="TabSel('1')"><span class="zong_tit"><span class="add_li">教案</span></span></a></li>
                                <li><a href="#" onclick="TabSel('2')"><span class="zong_tit"><span class="add_li">课件</span></span></a></li>
                                <li><a href="#" onclick="TabSel('3')"><span class="zong_tit"><span class="add_li">习题</span></span></a></li>
                                <li><a href="#" id="daishenhe" onclick="TabSel('-1')" style="display: none"><span class="zong_tit"><span class="add_li">审核</span></span></a></li>
                            </ul>
                        </div>
                        <div class="navgition" style="width: 100%">全部文件</div>

                        <div class="content">
                            <div class="tc">
                                <div class="Order_form">
                                    <div class="Food_order">
                                        <table class="O_form" id="tab">
                                            <thead>
                                                <tr class="O_trth">
                                                    <!--表头tr名称-->
                                                    <th class="Check">
                                                        <input type="checkbox" class="Check_box" id="checkAll" /></th>
                                                    <th class="name">文件名称 </th>
                                                    <th class="File_size">文件大小 </th>
                                                    <th class="Change_date">修改日期 </th>
                                                    <th class="Operations" id="Operations" style="display: none">操作 </th>
                                                </tr>
                                            </thead>
                                            <tbody></tbody>

                                        </table>
                                        <div id="pageDiv" class="pageDiv">
                                        </div>
                                    </div>
                                </div>
                            </div>

                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

</div>

<div id="Filediv" class="blk" style="display: none; position: absolute; z-index: 1000; background: #67a3d9;">

    <div class="main" style="width: 600px; background-color: white; border: 1px solid #0DA6EC; box-shadow: 0px 2px 14px #bbb;">
        <div style="height: 40px; background: #0DA6EC; line-height: 40px; font-size: 16px;">
            <span style="float: left; color: white; margin-left: 20px;">文件上传</span>
            <span style="float: right; margin-right: 20px;">
                <input type="button" value="X" onclick="closeDiv('Filediv')" style="background: #0DA6EC; border-color: #0DA6EC; height: 30px; line-height: 30px; font-size: 14px; color: white" />

            </span>
        </div>
        <div id="File_head" style="z-index: 1001; position: absolute;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</div>
        <div class="Upload">
            <div class="list_add">
                <div class="add_jia">
                    <table class="jia_c">
                        <tr>
                            <td id="att1">
                                <input type="button" value="+" onclick="fileupload0.click()" id="BtFile" />
                                <input type="file" name="fileupload0" id="fileupload0" style="display: none" onchange="AddFile()" multiple="multiple" />                                                               
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="T_list">
                    <input id="Hidden1" type="hidden" runat="server" />
                    <table border="0" cellpadding="0" cellspacing="0" id="idAttachmentsTable" class="W_form">
                        <tr class="trth">
                            <th>文件名</th>
                            <%--<td>大小</td>--%>
                            <th>操作</th>
                        </tr>
                    </table>
                </div>
            </div>
            <div class="fileup_w">
                <asp:Button ID="FielAdd" runat="server" Text="上传文件" OnClick="FielAdd_Click" CssClass="fileup" />
            </div>
        </div>
    </div>

</div>
<div id="Messagediv" class="blk" style="display: none; position: absolute; z-index: 1000; background: #67a3d9;">
    <div class="heaMessaged">
        <div class="head-right"></div>
    </div>
    <div class="main" style="width: 300px; background-color: white; border: 1px solid #374760">
        <div style="height: 40px; background: #5493D7; line-height: 50px; font-size: 15px;">
            <span style="float: left; color: white; margin-left: 20px;">拒绝意见</span>
            <span style="float: right; margin-right: 20px;">
                <input type="button" value="X" onclick="closeDiv('Messagediv')" style="border-color: #5493D7; background-color: #5493D7; height: 30px" />
            </span>
        </div>
        <div id="Message_head" style="z-index: 1001; position: absolute;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</div>
        <div style="margin: 20px; border: 1px solid #374760">
            <table style="width: 100%; margin: 10px;">
                <tr style="border-bottom: 0">
                    <td class="lftd">拒绝原因：<input id="hdS" type="hidden" /></td>
                    <td>
                        <input type="text" id="Message" class="search" /></td></tr><tr>
                    <td colspan="2">
                        <input type="button" value="确定" onclick="shenhe()" class="b_add" />
                </tr>
            </table>

        </div>
    </div>
</div>
