<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DriveTypeUserControl.ascx.cs" Inherits="SVDigitalCampus.Repository.DriveType.DriveTypeUserControl" %>
<script src="../../../../_layouts/15/SVDigitalCampus/Script/jquery-1.8.2.min.js"></script>

<link href="../../../../_layouts/15/SVDigitalCampus/CSS/common.css" rel="stylesheet" />
<link type="text/css" rel="stylesheet" href="../../../../_layouts/15/SVDigitalCampus/CSS/style.css" />
<link type="text/css" rel="stylesheet" href="../../../../_layouts/15/SVDigitalCampus/CSS/iconfont.css" />
<link type="text/css" rel="stylesheet" href="../../../../_layouts/15/SVDigitalCampus/CSS/animate.css" />
<script src="../../../../_layouts/15/SVDigitalCampus/Script/Enter/tz_slider.js"></script>
<script type="text/javascript">
    $(function () {
        BindType();
    });
    //绑定文件类型
    function BindType() {
        var FirstUrl = window.location.href;
        FirstUrl = FirstUrl.substring(0, FirstUrl.indexOf("SitePages"))
        $("#type").empty();

        $.ajax({
            type: "Post",
            url: FirstUrl + "/_layouts/15/SVDigitalCampus/hander/PersonDrive.aspx",
            data: { CMD: "FullType" },
            dataType: "text",
            success: function (returnVal) {
                returnVal = $.parseJSON(returnVal);
                $(returnVal).each(function () {
                    var tr = " <tr><td class=\"le\"><span class=\"m\"><input type=\"text\" id=\"Title" + this.ID + "\" style=\"padding: 0 10px; border-radius: 3px;height: 28px; width:60px;\" value=\"" + this.Title
                    + "\"/>&nbsp;&nbsp;<input id=\"Attr" + this.ID + "\" type=\"text\" style=\"padding: 0 10px; border-radius: 3px;height: 28px; width:400px;\" value=\""
                    + this.Attr + "\"/></span><a href=\"#\"><i class=\"iconfont\" onclick=\"EditType('" + this.ID + "')\">&#xe631;</i></a><a href=\"#\"><i class=\"iconfont\" onclick=\"DelType('" + this.ID + "')\">&#xe61e;</i></a></td></tr>";

                    $("#type").append(tr);
                });
            },
            error: function (errMsg) {
                alert('数据加载失败！');
            }
        });
    }
    //删除文档类型
    function DelType(id) {
        if (confirm("确认删除该分类吗")) {

            var FirstUrl = window.location.href;
            FirstUrl = FirstUrl.substring(0, FirstUrl.indexOf("SitePages"))

            $.ajax({
                type: "Post",
                url: FirstUrl + "/_layouts/15/SVDigitalCampus/hander/PersonDrive.aspx",
                data: { CMD: "DelType", DelTypeID: id},
                dataType: "text",
                success: function (returnVal) {
                    if (returnVal == "1") {
                        alert("删除成功");
                        BindType();
                        parent.GetFileType();
                    }
                    else { alert("删除失败") }
                },
                error: function (errMsg) {
                    alert('删除失败！');
                }
            });

        }
    }
    //新增文件类型
    function AddType() {
        var FirstUrl = window.location.href;
        FirstUrl = FirstUrl.substring(0, FirstUrl.indexOf("SitePages"))
        var TypeTitle = $("#Title").val();
        var Attr = $("#Attr").val();
        if (TypeTitle != "" && Attr != "") {

            $.ajax({
                type: "Post",
                url: FirstUrl + "/_layouts/15/SVDigitalCampus/hander/PersonDrive.aspx",
                data: { CMD: "AddType", AddTitle: TypeTitle, AddAttr: Attr },
                dataType: "text",
                success: function (returnVal) {
                    if (returnVal == "1") {
                        alert("添加成功");
                        BindType();
                        parent.GetFileType();
                    }
                    else { alert("添加失败") }
                },
                error: function (errMsg) {
                    alert('添加失败！');
                }
            });
        }
        else {
            alert("数据填写不完整");
        }
    }
    //修改文件后缀
    function EditType(id) {
        var FirstUrl = window.location.href;
        FirstUrl = FirstUrl.substring(0, FirstUrl.indexOf("SitePages"))
        var TypeAttr = $("#Attr" + id).val();
        var TypeTitle = $("#Title" + id).val();
        $.ajax({
            type: "Post",
            url: FirstUrl + "/_layouts/15/SVDigitalCampus/hander/PersonDrive.aspx",
            data: { CMD: "EditType", TypeAttr: TypeAttr, TypeTitle: TypeTitle, TypeAttrID: id },
            dataType: "text",
            success: function (returnVal) {
                if (returnVal == "1") {
                    alert("修改成功");
                    BindType();
                    parent.GetFileType();
                }
                else { alert("修改失败") }
            },
            error: function (errMsg) {
                alert('修改失败！');
            }
        });
    }
</script>
<div class="yy_dialog" style="top: 0px; left: 0px;">
    <div class="t_content">
        <!---选项卡-->
        <div class="yy_tab">
            <div class="content">
                <div class="internship">
                    <div class="t_message">
                        <div class="message_con">
                            <div class="add_input">
                                <input type="text" class="add_sear" value="" id="Title" placeholder="请输入类别名称">
                                <input type="text" class="add_sear" value="" id="Attr" placeholder="请输入后缀名称">
                                <input type="button" class="b_add" onclick="AddType()" value="添加">
                            </div>
                            <table class="add_drivetype" id="type">
                            </table>
                        </div>

                    </div>

                </div>
            </div>
        </div>

    </div>
</div>
