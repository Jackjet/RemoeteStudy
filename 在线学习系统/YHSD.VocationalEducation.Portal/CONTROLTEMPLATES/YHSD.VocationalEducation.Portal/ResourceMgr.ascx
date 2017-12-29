<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ResourceMgr.ascx.cs" Inherits="YHSD.VocationalEducation.Portal.CONTROLTEMPLATES.YHSD.VocationalEducation.Portal.ResourceMgr" %>

<script type="text/javascript">
    function AddResource() {
        var url = "<%=YHSD.VocationalEducation.Portal.Code.Common.CommonUtil.GetChildWebUrl()%>/_layouts/15/YHSD.VocationalEducation.Portal/UploadResource.aspx";
        location.href = url;
    }
    function EditResource(editId) {
        var url = "<%=YHSD.VocationalEducation.Portal.Code.Common.CommonUtil.GetChildWebUrl()%>/_layouts/15/YHSD.VocationalEducation.Portal/UploadResource.aspx?id=" + editId;
        location.href = url;
    }
    function DelResource(delId) {
        LayerConfirm("确定删除?", function () {
            var postData = { CMD: "DelResource", DelID: delId };
            AjaxRequest("<%=YHSD.VocationalEducation.Portal.Code.Common.CommonUtil.GetChildWebUrl()%>/_layouts/15/YHSD.VocationalEducation.Portal/Handler/ResourceMgrHandler.aspx", postData, function (returnVal) {
                ShowData(1);
                layer.closeAll();
            });
        })
    }
    function BindDataToTab(bindData) {
        $("#tab tbody").empty();
        $(bindData).each(function (k) {
            var tr = "<tr class='" + GetTrClass(k) + "'>\
                        <td class='td_tit'>" + GetTrNum(k, pageIndex) + "</td>\
                        <td class='td_tit_left'>" + this.Name + "</td>\
                        <td class='td_tit_left'>" + this.SeriesName + "</td>\
                        <td class='td_tit_left'>" + this.SpeechMaker + "</td>\
                        <td class='td_tit_left'>" + this.PersonLiable + "</td>\
                        <td class='td_tit'>" + this.ScreenTime + "</td>\
                        <td class='td_tit_left'>" + this.Format + "</td>\
                        <td class='td_tit_right'>" + this.Duration + "</td>\
                        <td class='td_tit'>\
                            <a title='编辑资源' onclick='EditResource(\"" + this.ID + "\")'><img class='EditImg' /></a>\
                            <a title='删除资源' onclick='DelResource(\"" + this.ID + "\")'><img class='DelImg' /></a>\
                        </td>\
                      </tr>";
            $("#tab tbody").append(tr);
        });
    }
    function ShowData(index) {
        var postData = { CMD: "FullTab", PageSize: pageSize, PageIndex: index };
        var speechMaker = $("#txtSpeechMaker").val();
        var name = $("#txtName").val();
        var classificationName = $("#txtClassification").val();
        var model = {};
        if (speechMaker != "") {
            model.SpeechMaker = speechMaker;
        }
        if (name != "") {
            model.Name = name;
        }
        if (classificationName != "") {
            model.RName = classificationName;
        }
        if (PropertyCount(model) > 0) {
            postData.ConditionModel = JSON.stringify(model);
        }
        AjaxRequest("<%=YHSD.VocationalEducation.Portal.Code.Common.CommonUtil.GetChildWebUrl()%>/_layouts/15/YHSD.VocationalEducation.Portal/Handler/ResourceMgrHandler.aspx", postData, function (returnVal) {
            PagerRefresh(index, returnVal.PageCount);
            BindDataToTab($.parseJSON(returnVal.Data));
        })
    }

    $(function () {
        LoadPageControl(ShowData, "pageDiv");
        ShowData(1);

        $("#txtName,#txtClassification,#txtSpeechMaker").keypress(function () {
            EnterEvent("btnQuery");
        })

        $("#btnQuery").click(function () {
            ShowData(1);
        })
    })
</script>
<div class="TopToolbar">
    <span>资源名称</span>&nbsp<input type="text" class="input_part" style="width:150px;" id="txtName" />&nbsp&nbsp
    <span>资源分类</span>&nbsp<input type="text" class="input_part" style="width:150px;" id="txtClassification" />&nbsp&nbsp
    <span>主讲人</span>&nbsp<input type="text" class="input_part" style="width:150px;" id="txtSpeechMaker" />&nbsp&nbsp
    <input type="button" value="查询" id="btnQuery" class="button_s"/>
</div>
        <div class="main_list_table">
            <table id="tab" class="list_table" cellspacing="0" cellpadding="0" border="0">
                <thead>
                    <tr class="tab_th top_th">
                        <th class="th_tit">序号</th>
                        <th class="th_tit_left">资源名称</th>
                        <th class="th_tit_left">分类</th>
                        <th class="th_tit_left">主讲人</th>
                        <th class="th_tit_left">责任者</th>
                        <th class="th_tit">拍摄时间</th>
                        <th class="th_tit_left">格式</th>
                        <th class="th_tit_right">时长</th>
                        <th class="th_tit">操作</th>
                    </tr>
                </thead>
                <tbody>
                </tbody>
            </table>
        </div>
<div style="text-align:center;"> 
    <div id="containerdiv" style="overflow: hidden; display: inline-block;">
        <div id="pageDiv" class="pageDiv">
        </div>
    </div>
</div>
<div class="main_button_part">
    <input type="button" value="上传" id="btnAdd" onclick="AddResource()"  class="button_s"/>
</div>

