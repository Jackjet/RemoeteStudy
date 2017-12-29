<%@ Assembly Name="YHSD.VocationalEducation.Portal, Version=1.0.0.0, Culture=neutral, PublicKeyToken=2e18f1308c96fd22" %>
<%@ Import Namespace="Microsoft.SharePoint.ApplicationPages" %>
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CompanyEdit.aspx.cs" Inherits="YHSD.VocationalEducation.Portal.Layouts.YHSD.VocationalEducation.Portal.CompanyEdit"%>

<html>
<head id="Head1" runat="server">
    <meta http-equiv="expires" content="0"/>
	<meta http-equiv="pragma" content="no-cache"/>
	<base target="_self" />
    <title>编辑乡镇信息</title>
	<link href="css/edit.css" rel="stylesheet" />
    <link href="css/type.css" rel="stylesheet" />
        <script src="js/Jquery.easyui/jquery.min.js"></script>
  <script src="js/Jquery.easyui/jquery.easyui.min.js"></script>
        <script src="js/layer/layer.js"></script>
    <script src="js/layer/OpenLayer.js"></script>
    <link href="js/Jquery.easyui/themes/default/easyui.css" rel="stylesheet" />
</head>
<body>
<form id="Form" runat="server">
    <script type="text/javascript">
        $(function () {
            $('#ddlCompany').combotree({
                url: '<%=YHSD.VocationalEducation.Portal.Code.Common.CommonUtil.GetChildWebUrl()%>/_layouts/15/YHSD.VocationalEducation.Portal/CompanyTree.aspx',
                valueField: 'id',
                textField: 'text',
                onSelect: function (node) {
                    document.getElementById("<%=this.HDParentID.ClientID %>").value = node.id;
                },
                onLoadSuccess: function () {
                    var deptid = document.getElementById("<%=this.HDParentID.ClientID %>").value;
                      if (deptid != null && deptid != "") {
                          $('#ddlCompany').combotree("setValue", deptid);
                      }

                  }
            })
        });
          function onSave() {
              if (document.getElementById("<%=this.Name.ClientID %>").value.trim() == "") {
                  LayerTips("名称必须填写！","<%=this.Name.ClientID %>");
                return false;
            }

            if (document.getElementById("<%=this.Name.ClientID %>").value.length > 100) {
                LayerTips("名称不能超过100个字符！", "<%=this.Name.ClientID %>");
                return false;
            }


            if (document.getElementById("<%=this.Description.ClientID %>").value.length > 100) {
                LayerTips("描述不能超过100个字符！","<%=this.Description.ClientID %>");
                return false;
            }
            var sequenceValue = document.getElementById("<%=this.Sequence.ClientID %>").value;

            if (sequenceValue.trim() == "") {
                LayerTips("显示顺序必须填写！", "<%=this.Sequence.ClientID %>");
                return false;
            }
            if (isNaN(sequenceValue)) {
                LayerTips("显示顺序必须为数字！", "<%=this.Sequence.ClientID %>");
                return false;
            }
            var SortNum = parseInt(sequenceValue);
            if (!(SortNum > 0 && SortNum < 999)) {
                LayerTips("显示顺序内容必须是数字且大于0小于999！", "<%=this.Sequence.ClientID %>");
                return false;
            }  

              if (document.getElementById("<%=this.HDID.ClientID %>").value!="" && document.getElementById("<%=this.HDID.ClientID %>").value == document.getElementById("<%=this.HDParentID.ClientID %>").value) {
                LayerTips("不能选择当前公司为上级公司！", "divCompany");
            return false;
        }
        }

        function SelectParent() {
            var id = document.getElementById("<%=this.HDID.ClientID %>").value;
            var organization = window.showModalDialog("<%=YHSD.VocationalEducation.Portal.Code.Common.CommonUtil.GetChildWebUrl()%>/_layouts/15/YHSD.VocationalEducation.Portal/ComPanySelect.aspx?type=0&id=" + id, window, 'dialogWidth:420px;dialogHeight:550px;edge:raised;scroll:auto;status:no;');

            if (organization) {
                document.getElementById("<%=this.HDParentID.ClientID %>").value = organization.Id;
                document.getElementById("ParentNameId").innerText = organization.Name; 
            }

        }
    </script>
    <asp:HiddenField ID="HDID" runat="server" />
    <asp:HiddenField ID="HDParentID" runat="server" />
    
    <table class="tableEdit MendEdit">

		<tr>
            <th>所属上级</th>
            <td><div id="divCompany"><select id="ddlCompany" class="easyui-combotree" style="width:200px;"
        data-options="url:'CompanyTree.aspx',required:false"></select></div></td>
        </tr>
        <tr>
            <th>名称&nbsp;<span style="color:Red">*</span></th>
            <td><asp:TextBox ID="Name" runat="server" CssClass="inputPart"></asp:TextBox></td>
        </tr>
        <tr>
            <th>简称&nbsp;</th>
            <td><asp:TextBox ID="DisplayName" runat="server" CssClass="inputPart"></asp:TextBox></td>
        </tr>
	        <tr>
            <th>编码</th>
            <td><asp:TextBox ID="Code" runat="server" CssClass="inputPart"></asp:TextBox></td>
        </tr>

         <tr>
            <th>显示顺序&nbsp;<span style="color:Red">*</span></th>
            <td><asp:TextBox ID="Sequence" runat="server" CssClass="inputPart"></asp:TextBox></td>
        </tr>
        <tr>
            <th>描述</th>
            <td><asp:TextBox ID="Description" runat="server" TextMode="MultiLine" Rows="6" CssClass="inputPart" 
height="50px"></asp:TextBox></td>
        </tr>
    </table>
    <table width="100%" class="MendEdit">
        <tr>
            <td align="center">
                <asp:Button ID="BTSave" runat="server" Text="确定"  OnClientClick="return onSave()" CssClass="ms-
ButtonHeightWidth button_s"  onclick="BTCompanySave_Click" />
                &nbsp;&nbsp;<input class="button_n" type="button" value="取消" onClick="OL_CloseLayerIframe();"  
class="ms-ButtonHeightWidth"/>
            </td>
        </tr>
    </table>
</form>
</body>
</html>

