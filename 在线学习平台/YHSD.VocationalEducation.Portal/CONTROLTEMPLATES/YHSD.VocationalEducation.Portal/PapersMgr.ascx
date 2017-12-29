<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PapersMgr.ascx.cs" Inherits="YHSD.VocationalEducation.Portal.CONTROLTEMPLATES.YHSD.VocationalEducation.Portal.PapersMgr" %>
<script type="text/javascript">

    function PreView(paperId) {

        var layerIndex = OL_ShowLayer(2, "试卷预览", 860, 660, "<%=YHSD.VocationalEducation.Portal.Code.Common.CommonUtil.GetChildWebUrl()%>/_layouts/15/YHSD.VocationalEducation.Portal/PaperPreView.aspx?PaperID=" + paperId, function () {

        });
        layer.full(layerIndex);
    }

    function BindDataToTab(bindData) {
        $("#tab tbody").empty();
        $(bindData).each(function (k) {
            var tr = "<tr class='" + GetTrClass(k) + "'>\
                        <td class='td_tit'>" + GetTrNum(k, pageIndex) + "</td>\
                        <td class='td_tit_left' style='width:250px;'>" + this.Title + "</td>\
                        <td class='td_tit_right'>" + this.TotalScore + "</td>\
                        <td class='td_tit_right'>" + this.QuestionCount + "</td>\
                        <td class='td_tit'>" + this.CreateTime + "</td>\
                        <td class='td_tit'>\
                            <a title='预览' onclick='PreView(\"" + this.ID + "\")'>\
                                <img class='ViewImg' />\
                            </a>\
                            <a title='编辑试卷' onclick='EditPaper(\"" + this.ID + "\")'>\
                                <img class='EditImg' />\
                            </a>\
                            <a title='删除试卷' onclick='DeletePaper(\"" + this.ID + "\")'>\
                                <img class='DelImg' />\
                            </a>\
                        </td>\
                      </tr>";
            $("#tab tbody").append(tr);
        });
    }

    function AddPaper() {
        var url = "<%=YHSD.VocationalEducation.Portal.Code.Common.CommonUtil.GetChildWebUrl()%>/_layouts/15/YHSD.VocationalEducation.Portal/FormPapers.aspx";
        location.href = url;
    }

    function EditPaper(id) {
        var url = "<%=YHSD.VocationalEducation.Portal.Code.Common.CommonUtil.GetChildWebUrl()%>/_layouts/15/YHSD.VocationalEducation.Portal/FormPapers.aspx?EditID=" + id;
        location.href = url;
    }

    function DeletePaper(id) {
        LayerConfirm("确定删除此试卷?", function () {
            AjaxRequest("<%=YHSD.VocationalEducation.Portal.Code.Common.CommonUtil.GetChildWebUrl()%>/_layouts/15/YHSD.VocationalEducation.Portal/Handler/CommonHandler.aspx", { CMD: "DelModel", TypeName: "Papers", DelId: id }, function () {
                ShowData(1);
                layer.closeAll();
            });
        });
    }

    function ShowData(index) {
        var postData = { CMD: "FullTab", TypeName: "Papers", PageSize: pageSize, PageIndex: index };
        var title = $("#txtTitle").val();
        var model = {};
        if (title != "") {
            model.Title = title;
        }
        if (PropertyCount(model) > 0) {
            postData.ConditionModel = JSON.stringify(model);
        }
        AjaxRequest("<%=YHSD.VocationalEducation.Portal.Code.Common.CommonUtil.GetChildWebUrl()%>/_layouts/15/YHSD.VocationalEducation.Portal/Handler/CommonHandler.aspx", postData, function (returnVal) {
            PagerRefresh(index, returnVal.PageCount);
            BindDataToTab($.parseJSON(returnVal.Data));
        })
    }

    $(function () {
        LoadPageControl(ShowData, "pageDiv");
        ShowData(1);
        $("#txtTitle").keypress(function () {
            EnterEvent("btnQuery");
        });
        $("#btnQuery").click(function () {
            ShowData(1);
        });
    })
</script>
<div class="TopToolbar">
    <span>试卷名称&nbsp</span><input type="text" class="input_part" style="width: 150px;" id="txtTitle" />
    <input type="button" value="查询" id="btnQuery" class="button_s" />
</div>
<div class="main_list_table">
    <table id="tab" class="list_table" cellspacing="0" cellpadding="0" border="0">
        <thead>
            <tr class="tab_th top_th">
                <th class="th_tit">序号</th>
                <th class="th_tit_left">试卷名称</th>
                <th class="th_tit_right">总分</th>
                <th class="th_tit_right">总题量</th>
                <th class="th_tit">出卷日期</th>
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
<div id="buttomToolbar" class="main_button_part">
    <input type="button" value="出卷" id="btnAdd" onclick="AddPaper()" class="button_s" />
</div>
