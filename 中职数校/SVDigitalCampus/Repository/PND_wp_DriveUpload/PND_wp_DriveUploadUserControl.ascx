<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PND_wp_DriveUploadUserControl.ascx.cs" Inherits="SVDigitalCampus.Repository.PND_wp_DriveUpload.PND_wp_DriveUploadUserControl" %>
<link href="../../../../_layouts/15/SVDigitalCampus/CSS/common.css" rel="stylesheet" />
<link href="../../../../_layouts/15/SVDigitalCampus/CSS/style.css" rel="stylesheet" />
<link href="../../../../_layouts/15/SVDigitalCampus/uploadify/uploadify.css" rel="stylesheet" />
<script src="../../../../_layouts/15/SVDigitalCampus/Script/jquery-1.8.2.min.js"></script>
<script src="../../../../_layouts/15/SVDigitalCampus/uploadify/jquery.uploadify-3.1.min.js"></script>
<script src="../../../../_layouts/15/SVDigitalCampus/Script/Enter/Upload.js"></script>

<div class="yy_dialog" style="top: 0px; left: 0px;">
    <div class="t_content">
        <!---选项卡-->
        <div class="yy_tab">
            <div class="content">
                <div class="internship">
                    <div class="t_message">
                        <div class="message_con">
                            <div class="Upload">
                                <div class="list_add">

                                    <div class="add_jia">
                                        <table class="jia_c">
                                            <tr>
                                                <td id="att1">
                                                    <input type="file" name="uploadify" id="uploadify" />
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                    <div class="fileup_w">
                                        <a href="javascript:$('#uploadify').uploadify('upload', '*')" class="fileup">开始上传</a>
                                        <a href="javascript:jQuery('#uploadify').uploadify('cancel')" class="fileup">取消上传</a>
                                    </div>
                                    <div class="T_list">
                                        <div id="fileQueue" style="width: 100%; height: auto; border: 2px solid #5493d7;"></div>
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

