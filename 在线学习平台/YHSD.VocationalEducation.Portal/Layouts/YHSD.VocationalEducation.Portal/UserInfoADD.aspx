﻿<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Import Namespace="Microsoft.SharePoint.ApplicationPages" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserInfoADD.aspx.cs" Inherits="YHSD.VocationalEducation.Portal.Layouts.YHSD.VocationalEducation.Portal.UserInfoADD" %>

<html>
<head id="Head1" runat="server">
    <meta http-equiv="expires" content="0" />
    <meta http-equiv="pragma" content="no-cache" />
    <base target="_self" />
    <title>增加人员</title>
    <link href="css/edit.css" rel="stylesheet" />
    <link href="css/type.css" rel="stylesheet" />
    <script src="js/Jquery.easyui/jquery.min.js"></script>
    <script src="js/Jquery.easyui/jquery.easyui.min.js"></script>
    <script src="js/DatePicker/WdatePicker.js"></script>
    <script src="js/uploadify/jquery.uploadify.min.js"></script>
    <link href="js/uploadify/uploadify.css" rel="stylesheet" />
    <script src="js/layer/layer.js"></script>
    <script src="js/layer/OpenLayer.js"></script>
    <link href="js/Jquery.easyui/themes/default/easyui.css" rel="stylesheet" />
    <script>
        function onSave() {
            if (document.getElementById("<%=this.DomainAccount.ClientID %>").value.replace(/(^\s*)|(\s*$)/g, "") == "") {
                layer.tips("域账户必须填写！", "#" + "<%=this.DomainAccount.ClientID %>");
                return false;
            }

            if (document.getElementById("<%=this.DomainAccount.ClientID %>").value.length > 100) {
                layer.tips("域账户不能超过100个字符！", "#" + "<%=this.DomainAccount.ClientID %>");
                return false;
            }
            if (document.getElementById("<%=this.Name.ClientID %>").value.replace(/(^\s*)|(\s*$)/g, "") == "") {
                layer.tips("姓名必须填写！", "#" + "<%=this.Name.ClientID %>");
                return false;
            }
            if (document.getElementById("<%=this.Name.ClientID %>").value.length > 100) {
                layer.tips("姓名不能超过100个字符！", "#" + "<%=this.Name.ClientID %>");
                return false;
            }
            if (document.getElementById("<%=this.HidDeptID.ClientID %>").value == "") {
                layer.tips("必须选择所属乡镇！", "#divCompany input");
                return false;
            }
            if (document.getElementById("<%=this.Mobile.ClientID %>").value.length > 50) {
                layer.tips("手机号码不能超过50个字符！", "#" + "<%=this.Mobile.ClientID %>");
                return false;
            }
            if (document.getElementById("<%=this.Telephone.ClientID %>").value.length > 50) {
                layer.tips("电话不能超过50个字符！", "#" + "<%=this.Telephone.ClientID %>");
                return false;
            }
            if (document.getElementById("<%=this.MSN.ClientID %>").value.length > 50) {
                layer.tips("MSN不能超过50个字符！", "#" + "<%=this.MSN.ClientID %>");
                return false;
            }
            if (document.getElementById("<%=this.QQ.ClientID %>").value.length > 50) {
                layer.tips("QQ不能超过50个字符！", "#" + "<%=this.QQ.ClientID %>");
                return false;
            }
            if (document.getElementById("<%=this.Email.ClientID %>").value.length > 50) {
                layer.tips("电子邮件不能超过50个字符！", "#" + "<%=this.Email.ClientID %>");
                return false;
            }
            if (document.getElementById("<%=this.ZipCode.ClientID %>").value.length > 50) {
                layer.tips("邮编不能超过50个字符！", "#" + "<%=this.ZipCode.ClientID %>");
                return false;
            }
            if (document.getElementById("<%=this.EmergencyContact.ClientID %>").value.length > 50) {
                layer.tips("紧急联系人不能超过50个字符！", "#" + "<%=this.EmergencyContact.ClientID %>");
                return false;
            }
            if (document.getElementById("<%=this.CardID.ClientID %>").value.length > 18) {
                layer.tips("身份证号码不能超过18个字符！", "#" + "<%=this.CardID.ClientID %>");
                 return false;
             }
            if (document.getElementById("<%=this.EmergencyTel.ClientID %>").value.length > 50) {
                layer.tips("紧急联系人电话不能超过50个字符！", "#" + "<%=this.EmergencyTel.ClientID %>");
                  return false;
            }
            if ($("#cbsSytem input:checked").length == 0) {
                layer.tips("请至少选择一个要添加的系统！", "#" + "<%=this.cbsSytem.ClientID %>");
                return false;
            }
          }
          function CloseQuXiao() {
              var val = $("#UserImg").attr("src");
              if (val != 'images/NoTouXiang.png') {
                  LayerConfirm("已上传照片！确认退出吗??", function () {




                      var postData = { UserPhoto: val };
                      $.ajax({
                          type: "POST",
                          url: "UserInfoADD.aspx",
                          data: postData,
                          dataType: "text",
                          success: function (returnVal) {
                              if (returnVal == "ok") {
                                  OL_CloseLayerIframe();
                              }
                          },
                          error: function (errMsg) {
                              OL_CloseLayerIframe();
                          }
                      })

                  });
                  return;
              }
              else {
                  OL_CloseLayerIframe();
              }
          }
          $(function () {
              $('#ddlCompany').combotree({
                  onSelect: function (node) {
                      document.getElementById("<%=this.HidDeptID.ClientID %>").value = node.id;
                }
            })
            $('#ddljiaose').combotree({
                onSelect: function (node) {
                    document.getElementById("<%=this.HidPostionID.ClientID %>").value = node.id;
                }
            })
        });
    </script>
</head>
<body>
    <form id="Form" runat="server">
        <input type="hidden" id="HidUserImg" runat="server" />
        <asp:HiddenField ID="HidDeptID" runat="server" />
        <asp:HiddenField ID="HidPostionID" runat="server" />
        <table class="tableEdit MendEdit" style="width: 100%;">
            <tr>
                <th>域账号&nbsp;<span style="color: Red">*</span></th>
                <td>
                    <asp:TextBox ID="DomainAccount" runat="server" class="inputpart" Width="200px"></asp:TextBox></td>
                <th>姓名&nbsp;<span style="color: Red">*</span></th>
                <td>
                    <asp:TextBox ID="Name" runat="server" class="inputpart"
                        Width="200px"></asp:TextBox></td>
            </tr>
            <tr>

                <th>性别</th>
                <td>
                    <asp:RadioButton ID="radMale" runat="server" GroupName="Sex" Text="男" Checked="true" /><asp:RadioButton
                        ID="radFemale" runat="server" GroupName="Sex" Text="女" /></td>
                <th>出生日期</th>
                <td>
                    <asp:TextBox ID="Birthday" ReadOnly="true" runat="server" class="inputpart" Width="200px"
                        onclick="WdatePicker({ isShowClear: true, readOnly: true });"></asp:TextBox></td>
            </tr>

            <tr>
                <th>所属乡镇<span style="color: Red">*</span></th>
                <td>
                    <div id="divCompany">
                        <select id="ddlCompany" class="easyui-combotree" style="width: 200px; height: 29px;"
                            data-options="url:'CompanyTree.aspx',required:false">
                        </select>
                    </div>
                </td>
                <th>角色</th>
                <td>
                    <select id="ddljiaose" class="easyui-combotree" style="width: 200px; height: 29px;"
                        data-options="url:'PositionTree.aspx',required:false">
                    </select></td>
            </tr>
            <tr>
                <th>手机</th>
                <td>
                    <asp:TextBox ID="Mobile" runat="server" class="inputpart" Width="200px"></asp:TextBox></td>
                <th>电话</th>
                <td>
                    <asp:TextBox ID="Telephone" runat="server" class="inputpart" Width="200px"></asp:TextBox></td>
            </tr>
            <tr>
                <th>MSN</th>
                <td>
                    <asp:TextBox ID="MSN" runat="server" class="inputpart" Width="200px"></asp:TextBox></td>
                <th>QQ</th>
                <td>
                    <asp:TextBox ID="QQ" runat="server" class="inputpart" Width="200px"></asp:TextBox></td>
            </tr>
            <tr>
                <th>电子邮件</th>
                <td>
                    <asp:TextBox ID="Email" runat="server" class="inputpart" Width="200px"></asp:TextBox></td>
                <th>邮编</th>
                <td>
                    <asp:TextBox ID="ZipCode" runat="server" class="inputpart" Width="200px"></asp:TextBox></td>
            </tr>
            <tr>
                <th>紧急联系人</th>
                <td>
                    <asp:TextBox ID="EmergencyContact" runat="server" class="inputpart" Width="200px"></asp:TextBox></td>
                <th>紧急联系人电话</th>
                <td>
                    <asp:TextBox ID="EmergencyTel" runat="server" class="inputpart" Width="200px"></asp:TextBox></td>
            </tr>
               <tr>
            <th>身份证号码</th>
            <td colspan="3"><asp:TextBox ID="CardID" runat="server"  class="inputpart" Width="200px"></asp:TextBox></td>
           
             </tr>
            <tr>
                <th>添加到以下系统</th>
                <td colspan="3">
                    <asp:CheckBoxList runat="server" ID="cbsSytem" RepeatDirection="Horizontal">
                    </asp:CheckBoxList>
                </td>
            </tr>
            <tr>
                <th>照片</th>
                <td>
                    <img id="UserImg" src="images/NoTouXiang.png" width="53" height="53" /></td>
            </tr>
        </table>
        <div style="margin-left: 180px; margin-top: -50px">
            <input type="file" name="uploadify"
                id="uploadify" />
            <style type="text/css">
                #cbsSytem td {
                    font-size: 13px;
                    font-family: "微软雅黑","宋体";
                }

                .uploadify-queue-item {
                    margin-top: -50px;
                    max-width: 250px;
                    margin-left: 110px;
                }
            </style>
            <script type="text/javascript" language="javascript">
                             $(function () {

                           $("#uploadify").uploadify({
                               'auto': true,                      //是否自动上传
                               'swf': 'js/uploadify/uploadify.swf',      //上传swf控件,不可更改
                               'uploader': 'UserInfoADD.aspx',            //上传处理页面,可以aspx
                               // 'formData': { HDUserImg:"" }, //参数
                               //'fileTypeDesc': '',
                               'fileTypeExts': '*.jpg;*.jpeg;*.png',   //文件类型限制,默认不受限制
                               'buttonText': '选择照片',//按钮文字
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
                                   $('#' + file.id).find('.data').html('         照片上传完成');
                                   $("#UserImg").attr("src", data);
                                   $("#TDbtn").css("display", "block"); //将按钮显示
                                   $("#uploadify").uploadify("settings", "formData", { 'HDUserImg': data }); //动态设置参数
                                   $("#HidUserImg").val(data);
                               },
                               'onUploadStart': function (file) {
                                   $("#TDbtn").css("display", "none"); //将按钮隐藏
                               },
                               'onSelectError': uploadify_onSelectError,
                               'onUploadError': uploadify_onUploadError


                           });
                       });

                        </script>
        </div>
        <table width="100%" class="MendEdit">
            <tr>
                <td align="center" id="TDbtn">
                    <asp:Button ID="BTSave" runat="server" Text="确定" CssClass="button_s" OnClientClick="return onSave()" OnClick="BTSave_Click" />
                    &nbsp;&nbsp;<input class="button_n" type="button" value="取消" onclick="CloseQuXiao()" class="ms-ButtonHeightWidth" />
                </td>
            </tr>
        </table>
    </form>
</body>

</html>
