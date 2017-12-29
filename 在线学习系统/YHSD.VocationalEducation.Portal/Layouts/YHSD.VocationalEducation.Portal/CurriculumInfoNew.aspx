<%@ Assembly Name="YHSD.VocationalEducation.Portal, Version=1.0.0.0, Culture=neutral, PublicKeyToken=2e18f1308c96fd22" %>
<%@ Import Namespace="Microsoft.SharePoint.ApplicationPages" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CurriculumInfoNew.aspx.cs" Inherits="YHSD.VocationalEducation.Portal.Layouts.YHSD.VocationalEducation.Portal.CurriculumInfoNew" DynamicMasterPageFile="~masterurl/default.master" %>

<asp:Content ID="PageHead" ContentPlaceHolderID="PlaceHolderAdditionalPageHead" runat="server">
    <script src="js/DatePicker/WdatePicker.js"></script>
    
    <link href="css/edit.css" rel="stylesheet" />

    <script>
        function onSave() {
            if (document.getElementById("<%=this.txtTitle.ClientID %>").value.replace(/(^\s*)|(\s*$)/g, "") == "") {
                LayerTips("课程名称必须填写！", "<%=this.txtTitle.ClientID %>");
                    return false;
                }

                if (document.getElementById("<%=this.txtTitle.ClientID %>").value.length > 50) {
                LayerTips("课程名称不能超过50个字符！", "<%=this.txtTitle.ClientID %>");
                   return false;
               }
               if (document.getElementById("<%=this.txtDescription.ClientID %>").value.replace(/(^\s*)|(\s*$)/g, "") == "") {
                LayerTips("课程描述必须填写！", "<%=this.txtDescription.ClientID %>");
                return false;
            }
            if (document.getElementById("<%=this.txtDescription.ClientID %>").value.length > 200) {
                LayerTips("课程描述不能超过200个字符！", "<%=this.txtDescription.ClientID %>");
                return false;
            }
            if (document.getElementById("<%=this.HDCurriculumInfoType.ClientID %>").value.replace(/(^\s*)|(\s*$)/g, "") == "") {
                LayerTips("请选择课程分类！", "tdid");
                return false;
            }

        }
        <%--        function onSelectType() {

            var url = "/_layouts/15/YHSD.VocationalEducation.Portal/CurriculumInfoSelect.aspx";
            
            OL_ShowLayer(2, "选择课程分类", 420, 500, url, function (returnVal) {
                if (returnVal)
                {
                    document.getElementById("<%=this.HDCurriculumInfoType.ClientID %>").value = returnVal.Id;
                    document.getElementById("<%=this.ParentNameId.ClientID %>").innerText = returnVal.Name;
                }
            });
        }--%>

        function onSelectCurriculum() {

            var url = "<%=YHSD.VocationalEducation.Portal.Code.Common.CommonUtil.GetChildWebUrl()%>/_layouts/15/YHSD.VocationalEducation.Portal/SelectCurriculum.aspx";
            if (document.getElementById("<%=this.HDID.ClientID %>").value != "") {
                url = url + "?Id=" + document.getElementById("<%=this.HDID.ClientID %>").value;
            }
            OL_ShowLayerNo(2, "选择相关课程", 700, 500, url, function (returnVal) {
                if (returnVal) {
                    document.getElementById("<%=this.HDSelectCurriculum.ClientID %>").value = returnVal.Id;
                    document.getElementById("<%=this.XiangGuanName.ClientID %>").innerText = returnVal.Name;
                }
            });
        }
    </script>
</asp:Content>

<asp:Content ID="Main" ContentPlaceHolderID="PlaceHolderMain" runat="server">
    <asp:HiddenField ID="HDCurriculumInfoType" runat="server" />
    <asp:HiddenField ID="HDSelectCurriculum" runat="server" />
    <asp:HiddenField ID="HDCurriculumImage" runat="server" />
    <asp:HiddenField ID="HDID" runat="server" />
    <table class="tableEdit">
        <tr>
            <th>课程名称&nbsp;<span style="color: Red">*</span></th>
            <td>
                <asp:TextBox ID="txtTitle" Width="400px" runat="server" CssClass="inputPart"></asp:TextBox></td>
        </tr>
        <tr>
            <th>课程描述&nbsp;<span style="color: Red">*</span></th>
            <td>
                <asp:TextBox ID="txtDescription" runat="server" Width="400px" Height="80px" TextMode="MultiLine" Rows="6" CssClass="inputPart"></asp:TextBox></td>
        </tr>
        <tr>
            <th>课程分类&nbsp;<span style="color: Red">*</span></th>
            <td id="tdid">
                <script>
                    $(function () {
                        $('#cc').combotree({
                            onSelect: function (node) {
                                document.getElementById("<%=this.HDCurriculumInfoType.ClientID %>").value = node.id;
                            },
                            onLoadSuccess: function () {
                                var Postionid = document.getElementById("<%=this.HDCurriculumInfoType.ClientID %>").value;
                if (Postionid != null && Postionid != "") {
                    $('#cc').combotree("setValue", Postionid);
                }

            }
                        })
                    });
                </script>
                <select id="cc" class="easyui-combotree" style="width: 200px; height: 29px;"
                    data-options="url:'<%=YHSD.VocationalEducation.Portal.Code.Common.CommonUtil.GetChildWebUrl()%>/_layouts/15/YHSD.VocationalEducation.Portal/CurriculumInfoTree.aspx',required:false">
                </select>
            </td>
        </tr>
        <tr>
            <th>课程首图片<span style="color: Red"></span></th>
            <td>
                <script src="js/uploadify/jquery.uploadify.min.js"></script>
                <link href="js/uploadify/uploadify.css" rel="stylesheet" />
                <img id="KeChenImg" runat="server" src="/_layouts/15/YHSD.VocationalEducation.Portal/images/NoImage.png" width="170" height="100" />
                <div>
                    <style>
                        .uploadify-queue {
                            width: 300px;
                        }
                    </style>

                    <input type="file" name="uploadify" id="uploadify" />



                    <script type="text/javascript" language="javascript">
                        $(function () {

                            $("#uploadify").uploadify({
                                'auto': true,                      //是否自动上传
                                'swf': 'js/uploadify/uploadify.swf',      //上传swf控件,不可更改
                                'uploader': 'CurriculumInfoNew.aspx',            //上传处理页面,可以aspx
                                'formData': { HDImg: "" }, //参数
                                //'fileTypeDesc': '',
                                'fileTypeExts': '*.jpg;*.jpeg;*.png',   //文件类型限制,默认不受限制
                                'buttonText': '选择图片',//按钮文字
                                // 'cancelimg': 'uploadify/uploadify-cancel.png',
                                'width': 100,
                                'height': 26,
                                //最大文件数量'uploadLimit':
                                'multi': false,//单选            
                                'fileSizeLimit': '20MB',//最大文档限制
                                'queueSizeLimit': 1,  //队列限制
                                'removeCompleted': true, //上传完成自动清空
                                'removeTimeout': 0, //清空时间间隔
                                'overrideEvents': ['onDialogClose', 'onUploadSuccess', 'onUploadError',

                                'onSelectError'],
                                'onUploadSuccess': function (file, data, response) {
                                    $('#' + file.id).find('.data').html('         课程图片上传完成');
                                    //$("#uploadify").uploadify("settings", "formData", { 'HDImg': data }); //动态设置参数
                                    $("#<%=this.KeChenImg.ClientID %>").attr("src", data);
                                         $("#<%=this.HDCurriculumImage.ClientID %>").val(data);
                                     },
                                     'onSelectError': uploadify_onSelectError,
                                     'onUploadError': uploadify_onUploadError


                                 });
                             });

                    </script>
                </div>
                <%--                     <label class="LabelImg"  id="LabelImgUrl" runat="server" visible="false"></label>
                <asp:FileUpload ID="FildImgUrl" runat="server" CssClass="inputPart" />--%>
            </td>
        </tr>
        <tr>
            <th>是否公开&nbsp;<span style="color: Red"></span></th>
            <td>
                <asp:RadioButton ID="radYes" runat="server" GroupName="Open" Text="是" /><asp:RadioButton ID="radNo" runat="server" GroupName="Open" Text="否" Checked="true" />
            </td>
        </tr>
        <tr>
            <th>是否收费&nbsp;<span style="color: Red"></span></th>
            <td>
                <asp:RadioButton ID="ShouFei" runat="server" GroupName="Open1" Text="是" /><asp:RadioButton ID="BuShouFei" runat="server" GroupName="Open1" Text="否" Checked="true" />

            </td>

        </tr>
         <tr>
            <th>授课方式&nbsp;<span style="color: Red"></span></th>
            <td>
                <asp:RadioButton ID="XianShang" runat="server" GroupName="Open2" Text="线上" /><asp:RadioButton ID="XianXia" runat="server" GroupName="Open2" Text="线下" Checked="true" />

            </td>

        </tr>
        <tr>
            <th>收费价格:</th>
            <td>

                <asp:TextBox ID="Class_Price" class="input_part" runat="server"></asp:TextBox></td>
        </tr>
        <tr>
            <th>时长:</th>
            <td><asp:TextBox ID="TB_Times" class="input_part" runat="server"></asp:TextBox></td>
        </tr>
        <tr>
            <th>开始时间:</th>
            <td>
                <asp:TextBox ID="TB_StartTime" style="cursor:pointer;" onclick="WdatePicker({ isShowClear: true, readOnly: true, dateFmt: 'yyyy-MM-dd', autoPickDate: true });" runat="server"></asp:TextBox></td>
        </tr>
        <tr>
            <th>截至时间:</th>
            <td>
                <asp:TextBox ID="TB_EndTime" style="cursor:pointer;" onclick="WdatePicker({ isShowClear: true, readOnly: true, dateFmt: 'yyyy-MM-dd HH:mm:ss', autoPickDate: true });" runat="server"></asp:TextBox></td>
        </tr>
        <tr>
            <th>相关课程&nbsp;</th>
            <td><span id="XiangGuanName" runat="server"></span>
                <input class="button_s" type="button" value="选择" onclick="onSelectCurriculum()">
            </td>
        </tr>
    </table>
    <table width="100%">
        <tr>
            <td align="center">
                <!--<button class="button_s">确定</button>-->
                <asp:Button ID="BTSave" runat="server" Text="确定" OnClientClick="return onSave()" CssClass="button_s" OnClick="BTCompanySave_Click" />
                &nbsp;&nbsp;
                <!--<button class="button_n" onclick="history.go(-1);">取消</button>-->
                <input type="button" value="取消" onclick="history.go(-1);" class="button_n" />
            </td>
        </tr>
    </table>

</asp:Content>

<asp:Content ID="PageTitle" ContentPlaceHolderID="PlaceHolderPageTitle" runat="server">
</asp:Content>

<asp:Content ID="PageTitleInTitleArea" ContentPlaceHolderID="PlaceHolderPageTitleInTitleArea" runat="server">
</asp:Content>
