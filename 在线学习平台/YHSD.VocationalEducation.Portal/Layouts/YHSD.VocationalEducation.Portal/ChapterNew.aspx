<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Import Namespace="Microsoft.SharePoint.ApplicationPages" %>
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ChapterNew.aspx.cs" Inherits="YHSD.VocationalEducation.Portal.Layouts.YHSD.VocationalEducation.Portal.ChapterNew" DynamicMasterPageFile="~masterurl/default.master" %>

<asp:Content ID="PageHead" ContentPlaceHolderID="PlaceHolderAdditionalPageHead" runat="server">
    <link href="css/edit.css" rel="stylesheet" />
        <script>
            $(pageInit);
            function pageInit() {
                $('#<%=this.WorkDescription.ClientID %>').xheditor({ tools: 'Cut,Copy,Paste,Fontface,FontSize,Bold,Italic,Underline,Strikethrough,FontColor,BackColor,Align,Img,Emot,Source,Preview,Print,about', forcePtag: "true", upImgUrl: "<%=YHSD.VocationalEducation.Portal.Code.Common.CommonUtil.GetChildWebUrl()%>/_layouts/15/YHSD.VocationalEducation.Portal/ChapterNew.aspx", upImgExt: "jpg,jpeg,gif,png" });
            }
            function onSelectPlay() {

                var url = "<%=YHSD.VocationalEducation.Portal.Code.Common.CommonUtil.GetChildWebUrl()%>/_layouts/15/YHSD.VocationalEducation.Portal/SelectResou.aspx";
                OL_ShowLayerNo(2, "选择资源", 700, 500, url, function (returnVal) {
                    if (returnVal) {
                        document.getElementById("<%=this.HDResoureID.ClientID %>").value = returnVal.Id;
                    document.getElementById("<%=this.ResoureName.ClientID %>").innerText = returnVal.Name;
                }
               });
<%--                var organization = window.showModalDialog(url, window, 'dialogWidth:600px;dialogHeight:400px;edge:raised;scroll:auto;status:no;');

                if (organization) {
                    document.getElementById("<%=this.HDResoureID.ClientID %>").value = organization.Id;
                    document.getElementById("<%=this.ResoureName.ClientID %>").innerText = organization.Name;
                    return false;
                }--%>

            }
    </script>
</asp:Content>

<asp:Content ID="Main" ContentPlaceHolderID="PlaceHolderMain" runat="server">
          <asp:HiddenField ID="HDResoureID" runat="server" />
          <asp:HiddenField ID="HDAttachmentObject" runat="server" />   
    <table class="tableEdit MendEdit">
             <tr>
            <th>章节序号&nbsp;<span style="color:Red">*</span></th>
            <td><asp:Label runat="server" ID="LabelSerialNumber"/></td>
        </tr>
        <tr>
            <th>章节名称&nbsp;<span style="color:Red">*</span></th>
            <td><asp:TextBox ID="txtTitle" runat="server" CssClass="inputPart" Width="466px"></asp:TextBox></td>
        </tr>
             <tr>
            <th>章节课件&nbsp;</th>
            <td id="tdWorkDescription">
        <script src="js/uploadify/jquery.uploadify.min.js"></script>
                        <link href="js/uploadify/uploadify.css" rel="stylesheet" />
                    <div >   
<style>
.uploadify-queue {

    width: 300px;
}

</style> 
         
                   <input type="file" name="uploadify" id="uploadify"  />



                         <script type="text/javascript" language="javascript">
                             $(function () {

                                 $("#uploadify").uploadify({
                                     'auto': true,                      //是否自动上传
                                     'swf': 'js/uploadify/uploadify.swf',      //上传swf控件,不可更改
                                     'uploader': 'ChapterNew.aspx',            //上传处理页面,可以aspx
                                     'formData': { HDImg: "" }, //参数
                                     //'fileTypeDesc': '',
                                     //'fileTypeExts': '*.jpg;*.jpeg;*.png',   //文件类型限制,默认不受限制
                                     'buttonText': '选择课件',//按钮文字
                                     // 'cancelimg': 'uploadify/uploadify-cancel.png',
                                     'width': 100,
                                     'height': 26,
                                     //最大文件数量'uploadLimit':
                                     'multi': false,//单选            
                                     'fileSizeLimit': '120MB',//最大文档限制
                                     'queueSizeLimit': 10,  //队列限制
                                     'removeCompleted': true, //上传完成自动清空
                                     'removeTimeout': 1, //清空时间间隔
                                     'overrideEvents': ['onDialogClose', 'onUploadSuccess', 'onUploadError',

                                     'onSelectError'],
                                     'onUploadSuccess': function (file, data, response) {
                                         $('#' + file.id).find('.data').html('         章节课件上传完成');
                                         if (data != null) {
                                             document.getElementById("<%=this.HDAttachmentObject.ClientID %>").value = document.getElementById("<%=this.HDAttachmentObject.ClientID %>").value + data+",";
                                             var str = $.parseJSON(data);
                                             if (str != null) {
                                                 $("#tdWorkDescription").prepend("<p style='margin-top:10px;margin-bottom:10px;'><a href='" + str.FilePhysicalPath + "'><span >" + str.FileName + "</span></a></p>");
                                             }
                                         }
                                     },
                                     'onSelectError': uploadify_onSelectError,
                                     'onUploadError': uploadify_onUploadError


                                 });
                             });

                        </script></div>
            </td>
        </tr>
          <tr>
            <th>章节视频&nbsp;<span style="color:Red">*</span></th>
            <td><span id="ResoureName"  runat="server"  ></span>
               <input type="button"  class="button_s" value="选择"  onclick="onSelectPlay()">
            </td>
        </tr>

         <tr>
            <th>作业描述</th>
            <td >
    <script src="xhEditor/xheditor-1.2.2.min.js"></script>
    <script src="xhEditor/xheditor_lang/zh-cn.js"></script>
<style>
.xhe_default .xheIframeArea {
    height: 100%;
    width: 480px;
}
.xhe_default td.xheTool {

    width: 480px;
}

</style>
                <textarea runat="server" id="WorkDescription" name="WorkDescription"  style="width: 480px; height:300px;"></textarea>
            

            </td>
        </tr>
    </table>
        <table width="100%">
        <tr>
            <td align="center">
                <asp:Button ID="BTSave" runat="server" Text="确定"   CssClass="button_s"  OnClick="BTSave_Click"  />
                &nbsp;&nbsp;<input type="button" value="取消" class="button_n" onclick="history.go(-1);"  />
            </td>
        </tr>
    </table>
</asp:Content>

<asp:Content ID="PageTitle" ContentPlaceHolderID="PlaceHolderPageTitle" runat="server">

</asp:Content>

<asp:Content ID="PageTitleInTitleArea" ContentPlaceHolderID="PlaceHolderPageTitleInTitleArea" runat="server" >
</asp:Content>