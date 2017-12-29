<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="R_wp_LibraryUserControl.ascx.cs" Inherits="SVDigitalCampus.Repository.R_wp_Library.R_wp_LibraryUserControl" %>

<link href="../../../../_layouts/15/SVDigitalCampus/CSS/common.css" rel="stylesheet" />
<link href="../../../../_layouts/15/SVDigitalCampus/CSS/style.css" rel="stylesheet" />
<link href="../../../../_layouts/15/SVDigitalCampus/CSS/iconfont.css" rel="stylesheet" />
<link href="../../../../_layouts/15/SVDigitalCampus/CSS/animate.css" rel="stylesheet" />
<script src="../../../../_layouts/15/SVDigitalCampus/Script/jquery-1.8.2.min.js"></script>
<link href="../../../../_layouts/15/SVDigitalCampus/Script/Enter/jquery-webox.css" rel="stylesheet" />
<script src="../../../../_layouts/15/SVDigitalCampus/Script/Enter/jquery-webox.js"></script>

<script src="../../../../_layouts/15/SVDigitalCampus/Script/Enter/tz_slider.js"></script>
<script src="../../../../_layouts/15/SVDigitalCampus/Script/Enter/uploadFile.js"></script>
<script src="../../../../_layouts/15/SVDigitalCampus/Script/JSBase.js"></script>
<script src="../../../../_layouts/15/SVDigitalCampus/Script/Enter/popwin.js"></script>

<link href="../../../../_layouts/15/SVDigitalCampus/test/type.css" rel="stylesheet" />
<link href="../../../../_layouts/15/SVDigitalCampus/test/Pager.css" rel="stylesheet" />
<script src="../../../../_layouts/15/SVDigitalCampus/test/Pager.js"></script>
<script src="../../../../_layouts/15/SVDigitalCampus/test/Pager.js"></script>
<script src="../../../../_layouts/15/SVDigitalCampus/test/json.js"></script>
<script src="../../../../_layouts/15/SVDigitalCampus/test/VocationalEducation.js"></script>
<script src="../../../../_layouts/15/SVDigitalCampus/test/FormatUtil.js"></script>
<script src="../../../../_layouts/15/SVDigitalCampus/Script/Drive/Library.js"></script>
<style type="text/css">
    .hid {
        display: none;
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

    .select {
        display: inline-block;
        background: #5493D7;
    }

        .select a {
            background: #5493D7;
            color: #fff;
        }
</style>

<div class="School_library">
    <!--页面名称-->
    <h1 class="Page_name">资源库</h1>
    <!--整个展示区域-->
    <div class="Whole_display_area">


        <div>
            <!--操作区域-->
            <div class="Operation_area">
                <div class="left_choice fl">
                    <ul>
                        <li class="Sear">
                            <input type="text" placeholder=" 请输入关键字" class="search" name="search" /><i class="iconfont">&#xe62d;</i>
                        </li>
                    </ul>
                </div>
                <div class="right_add fr" id="opertion">
                    <a href="#" class="add" onclick="uploader()"><i class="iconfont">&#xe65a;</i>上传文件</a>
                    <a href="#" class="add" onclick="javascript: appendAfterRow('tab', '1')"><i class="iconfont">&#xe601;</i>新建文件夹</a>
                    <div class="Batch_operation fl">
                        <a href="#" class="add"><i class="iconfont">&#xe602;</i>批量操作</a>
                        <ul class="B_con" style="display: none;">
                            <li><a href="#" onclick="Move('')">移动到</a></li>
                            <li><a href="#" onclick="Move()">复制到</a></li>
                            <li><a href="#" onclick="DelMore()">删除</a></li>
                            <li><a href="#" onclick="Down('')">下载</a></li>
                        </ul>
                    </div>
                </div>
            </div>
            <div class="clear"></div>
            <input id="Hidden2" type="hidden" />
            <input id="Hidden3" type="hidden" />
            <input id="HFoldUrl" type="hidden" runat="server" />
            <input id="HStatus" type="hidden" value="" />

            <div class="File_type" style="display: block;">
                <dl class="F_type" id="F_type">
                </dl>
                <div class="clear"></div>
                <dl class="F_time">
                    <dt>上传时间：</dt>
                    <dd class="select"><a href="#" onclick="serchTime('',this)">不限</a></dd>
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
                            <li class="selected">
                                <a href="#" onclick="TabSel('')"><span class="zong_tit"><span class="add_li">全部</span><span class="num_1"></span></span>                                </a>
                            </li>
                            <li id="daishenhe" style="display: none">
                                <a href="#" onclick="TabSel('0')"><span class="zong_tit"><span class="add_li">待审核</span><span class="num_1"></span></span></a>
                            </li>
                            <li>
                                <a href="#" onclick="TabSel('2')"><span class="zong_tit"><span class="add_li">审核未通过</span><span class="num_1"></span></span></a>

                            </li>
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
                                                    <input type="checkbox" class="Check_box" name="Check_box" id="checkAll" /></th>
                                                <th class="name">文件名称 </th>
                                                <th class="File_size">文件大小 </th>
                                                <th class="Change_date">修改日期 </th>
                                                <th class="Operations">操作 </th>
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
<div id="Filediv" class="blk" style="display: none; position: absolute; z-index: 1000; background: #67a3d9;">
    <div class="head">
        <div class="head-right"></div>
    </div>
    <div class="main" style="width: 600px; background-color: white; border: 1px solid #374760">
        <div style="height: 40px; background: #5493D7; line-height: 40px; font-size: 12px;">
            <span style="float: left; color: white; margin-left: 20px;">文件上传</span>
            <span style="float: right; margin-right: 20px;">
                <input type="button" value="X" onclick="closeDiv('Filediv')" style="background: #5493D7; border-color: #5493D7; height: 20px; line-height: 20px;" />
            </span>
        </div>
        <div id="File_head" style="z-index: 1001; position: absolute;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</div>
        <div style="margin: 20px; border: 1px solid #374760">
            <table style="width: 100%; margin: 10px; border: 0">
                <tr>
                    <td colspan="4">
                        <table style="border: 1px solid gray; width: 97%;">
                            <tr style="height: 40px;">
                                <td id="att1" style="text-align: center; font-size: 30px; font-weight: bolder;">
                                    <input type="button" value="+" onclick="fileupload0.click()" id="BtFile" style="height: 100%; width: 95%; font-size: 25px; border: 0; background-color: white;" />
                                    <input type="file" name="fileupload0" id="fileupload0" style="display: none" onchange="AddFile()" multiple="multiple" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr style="height: 20px;">
                    <td></td>
                </tr>
                <tr>
                    <td class="lftd">上传队列</td>

                </tr>
                <tr style="height: 20px;">
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>
                        <input id="Hidden1" type="hidden" runat="server" />

                        <table border="0" cellpadding="0" cellspacing="0" id="idAttachmentsTable" style="width: 97%; border: 1px solid gray;">
                            <tr style="height: 30px; border-bottom: 1px solid gray;">
                                <td>文件名</td>
                                <td>大小</td>
                                <td>操作</td>
                                <td style="width: 50%"></td>
                            </tr>
                        </table>
                    </td>
                </tr>

                <tr style="height: 20px;">
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>
                        <asp:Button ID="FielAdd" runat="server" Text="上传文件" OnClick="FielAdd_Click" /></td>
                </tr>
            </table>


        </div>
    </div>

</div>
<div id="Messagediv" class="blk" style="display: none; position: absolute; z-index: 1000; background: #67a3d9;">
    <div class="heaMessaged">
        <div class="head-right"></div>
    </div>
    <div class="main" style="width: 300px; background-color: white; border: 1px solid #374760">
        <div style="height: 40px; background: #5493D7; line-height: 50px; font-size: 15px;">
            <span style="float: left; color: white; margin-left: 20px; line-height: 30px;">拒绝意见</span>
            <span style="float: right; margin-right: 20px; line-height: 30px; color: white;">
                <input type="button" value="X" onclick="closeDiv('Messagediv')" style="border-color: #5493D7; background-color: #5493D7; height: 30px" />
            </span>
        </div>
        <div id="Message_head" style="z-index: 1001; position: absolute;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</div>
        <div style="margin: 20px;">
            <table style="width: 100%; margin: 10px; height:100px;">
                <tr style="border-bottom: 0">
                    <td class="lftd">拒绝原因：<input id="hdS" type="hidden" /></td>
                    <td>
                        <input type="text" id="Message" class="search" /></td>
                </tr>
                <tr>
                    <td colspan="2" style="text-align: center;">
                        <input type="button" value="确定" onclick="Check('', '2')" style="width: 100px; border: none; border-radius: 3px; height: 38px; font-size: 14px; background: #96cc66; cursor: pointer; text-align: center; padding: 10px 0; color: white; background: #87c352; outline: none;" />
                </tr>
            </table>

        </div>
    </div>
</div>
