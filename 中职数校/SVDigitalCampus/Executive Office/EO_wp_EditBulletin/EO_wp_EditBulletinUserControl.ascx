<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EO_wp_EditBulletinUserControl.ascx.cs" Inherits="SVDigitalCampus.Executive_Office.EO_wp_EditBulletin.EO_wp_EditBulletinUserControl" %>
<link type="text/css" rel="stylesheet" href="../../../../_layouts/15/SVDigitalCampus/CSS/common.css" />
<link type="text/css" rel="stylesheet" href="../../../../_layouts/15/SVDigitalCampus/CSS/style.css" />
<link type="text/css" rel="stylesheet" href="../../../../_layouts/15/SVDigitalCampus/CSS/iconfont.css" />
<link type="text/css" rel="stylesheet" href="../../../../_layouts/15/SVDigitalCampus/CSS/animate.css" />
<link href="../../../../_layouts/15/SVDigitalCampus/Script/KindUeditor/themes/default/default.css" rel="stylesheet" />
<script src="../../../../_layouts/15/SVDigitalCampus/Script/jquery-1.8.2.min.js"></script>
<script src="../../../../_layouts/15/SVDigitalCampus/Script/KindUeditor/kindeditor-min.js"></script>
<script src="../../../../_layouts/15/SVDigitalCampus/Script/KindUeditor/plugins/code/prettify.js"></script>
<script src="../../../../_layouts/15/SVDigitalCampus/Script/KindUeditor/lang/zh_CN.js"></script>
<script type="text/javascript">
    function close() {
        $("#mask,#maskTop", parent.document).fadeOut(function () {
            $(this).remove();
        });
        parent.location.href = parent.location.href;
        parent.location.reload(true);
    }
</script>
 <div class="MenuDiv"> 
      <div class="t_btn">
          <input id="btnEdit" class="btn" onclick="Submit(this)" type="button" value="保存" />
       </div> 
            <table class="tbEdit">
                        <tr>
            <td class="mi">*标题</td>
            <td class="ku">
                <asp:TextBox ID="txtTitle" runat="server"></asp:TextBox><asp:HiddenField ID="BulletinID" runat="server" /></td>
        </tr>
        <tr>
            <td class="mi">*类别</td>
            <td class="ku">
                <asp:DropDownList ID="ddlType" runat="server"></asp:DropDownList></td>
        </tr>
        <tr>
            <td class="mi">*内容</td>
            <td class="ku">
                <asp:TextBox ID="txtContent" runat="server" TextMode="MultiLine"  CssClass="content"></asp:TextBox></td>
        </tr>
        <tr>
            <td class="mi">排序</td>
            <td class="ku">
                  <asp:TextBox ID="txtOrder" TextMode="Number" Text="0" runat="server"></asp:TextBox><%-- <asp:DropDownList ID="ddlOrder" runat="server"></asp:DropDownList>--%></td>
        </tr>
      <%--  <tr>
            <td class="mi">关键词</td>
            <td class="ku">
                <asp:TextBox ID="txtKeyWord" runat="server"></asp:TextBox></td>
        </tr>--%>
        <tr>
            <td class="mi">备注</td>
            <td class="ku">
                <asp:TextBox ID="txtRemark" runat="server"></asp:TextBox></td>
        </tr>
     <%--   <tr>
            <td class="mi">状态</td>
            <td class="ku">
                <asp:DropDownList ID="Status" runat="server">
                    <asp:ListItem Value="1">待阅</asp:ListItem>
                    <asp:ListItem Value="2">已阅</asp:ListItem>
                </asp:DropDownList></td>
        </tr>--%>
            <tr>
                    <td class="mi">*可阅人</td>
                    <td class="ku"> 
                        <asp:TreeView ID="tvOrganization" runat="server" ShowCheckBoxes="All" 
                            CssClass="treeView" ShowLines="true" ExpandDepth="0" PopulateNodesFromClient="false"
                            ShowExpandCollapse="true" EnableViewState="false">
                            <LevelStyles>
                                <asp:TreeNodeStyle Font-Underline="False" />
                                <asp:TreeNodeStyle Font-Underline="False" />
                                <asp:TreeNodeStyle Font-Underline="False" />
                                <asp:TreeNodeStyle Font-Underline="False" />
                            </LevelStyles>
                        </asp:TreeView></td>
                </tr>
    </table>    
</div>
<script>
    <%-- var Contenteditor;
    KindEditor.ready(function (K) {
        Contenteditor = K.create('#<%=txtContent.ClientID%>', {
            uploadJson: '<%=layoutstr%>' + 'CommDataHandler.aspx?action=Upload_json',
            minWidth: '600px',
            width: '600px',
            minHeight:'200px',
            height:'200px',
            items: [
						'fontname', 'fontsize', '|', 'forecolor', 'hilitecolor', 'bold', 'italic', 'underline',
						'removeformat', '|', 'justifyleft', 'justifycenter', 'justifyright', 'insertorderedlist',
						'insertunorderedlist', '|', 'emoticons', 'image', 'link'],
            afterBlur: function () { this.sync(); }
        });
    });--%>
    function Submit(obj) {
        var Organization = GetCheckedNodes();
        var txtTitle = $('#<%=txtTitle.ClientID%>').val();
        var BulletinID = $('#<%=BulletinID.ClientID%>').val();
        var txtContent = $('#<%=txtContent.ClientID%>').val();
        var txtOrder = $('#<%=txtOrder.ClientID%>').val();
        var txtRemark = $('#<%=txtRemark.ClientID%>').val();
        var ddlType = $('#<%=ddlType.ClientID%>').val();
        if (checkNull()) {
            obj.disabled = true;
            jQuery.ajax({
                url: '<%=layoutstr%>' + "/SVDigitalCampus/BulletinHandler/BulletinHander.aspx?action=EditBulletin&" + Math.random(),   // 提交的页面
                type: "POST",                   // 设置请求类型为"POST"，默认为"GET"
                data: {
                    "Title": txtTitle,
                    "BulletinID": BulletinID,
                    "Content": txtContent,
                    "Order": txtOrder,
                    "Remark": txtRemark,
                    "Type": ddlType,
                    "Organization": Organization
                },
                beforeSend: function ()          // 设置表单提交前方法
                {
                    //alert("准备提交数据");


                },
                error: function (request) {      // 设置表单提交出错
                    //alert("表单提交出错，请稍候再试");
                    //rebool = false;
                },
                success: function (data) {
                    var ss = data.split("|");
                    if (ss[0] == "1") {
                        alert("编辑成功！");
                        window.parent.location.href = window.parent.location.href;
                        window.parent.location.reload(true);
                    }
                    else {
                        alert(ss[1]);
                    }
                }
            });
        } else {
            alert("必填项不能为空！");
        }
    
    
    }
    function checkNull() {
        var result = true;
        var Organization = GetCheckedNodes();
        var txtTitle = $('#<%=txtTitle.ClientID%>').val();
           var BulletinID = $('#<%=BulletinID.ClientID%>').val();
        var txtContent = $('#<%=txtContent.ClientID%>').val();
        var ddlType = $('#<%=ddlType.ClientID%>').val();
        if (BulletinID.trim() == "" || txtTitle.trim() == "" || txtContent.trim() == "" || ddlType.trim() == "" || Organization.trim() == "" || Status.trim() == "") {
            result = false;
        }
        return result;
    }
    function GetCheckedNodes() {
        var getAllNodes = "";
        var IDvalue = "";
        var tree = document.getElementById('<%=tvOrganization.ClientID%>').getElementsByTagName("Input");
        for (var i = 0; i < tree.length; i++) {
            if (tree[i].type == "checkbox" && tree[i].checked) {
                if (tree[i].getAttribute("title", 2) != "") {
                    getAllNodes = getAllNodes + tree[i].nextSibling.innerHTML + ",";
                    IDvalue = IDvalue + tree[i].nextSibling.title + ";";
                }
            }
        }
        return IDvalue;
    }
</script>