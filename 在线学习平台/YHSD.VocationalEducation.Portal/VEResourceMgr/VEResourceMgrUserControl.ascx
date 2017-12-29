<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="VEResourceMgrUserControl.ascx.cs" Inherits="YHSD.VocationalEducation.Portal.VEResourceMgr.VEResourceMgrUserControl" %>

<link href="res/css/gridview.css" rel="stylesheet" />
<style type="text/css">
    .workDiv {
        margin-bottom: 10px;
    }
</style>
<script type="text/javascript">
    function ShowDialog() {
        window.showModalDialog("ResourceDialogPage.aspx", null, "dialogWidth=500px;dialogHeight:400px;");
    }
</script>
<div id="nav" class="workDiv">
    <div>
        <asp:TreeView runat="server" ID="tvResource" OnSelectedNodeChanged="tvResource_SelectedNodeChanged">
            <SelectedNodeStyle BackColor="#3333FF" ForeColor="White" />
        </asp:TreeView>
    </div>
</div>
<div id="topToolbar" class="workDiv">
    <span>资源名称：</span><asp:TextBox runat="server" ID="txtSourceName"></asp:TextBox>
    <span>主讲人：</span><asp:TextBox runat="server" ID="txtSeries"></asp:TextBox>
    <asp:Button runat="server" Text="查询" ID="btnQuery" />
</div>
<div id="main" class="workDiv">
    <%-- <asp:GridView ID="gv" runat="server"></asp:GridView>--%>
    <cc1:centergrid id="dgList" runat="server" autogeneratecolumns="False" allowcustompaging="True" allowpaging="True" settingdefaultcss="True" width="100%" onpageindexchanged="dgList_PageIndexChanged">
                <Columns>
                    <cc1:IndexColumn HeaderText="序号">
                    </cc1:IndexColumn>
                    <asp:BoundColumn DataField="ID" HeaderText="ID" Visible="False"></asp:BoundColumn>
                    <asp:TemplateColumn HeaderText="资源名称">
                        <ItemTemplate>
                            <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# DataBinder.Eval(Container, "DataItem.ID", "SupplierInfoDetails.aspx?Id={0}") %>' Text='<%# Eval("Name") %>'></asp:HyperLink>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:BoundColumn HeaderText="系列名称" DataField="SeriesName"></asp:BoundColumn>
                    <asp:BoundColumn HeaderText="主讲人" DataField="SpeechMaker"></asp:BoundColumn>
                    <asp:BoundColumn HeaderText="责任人" DataField="PersonLiable"></asp:BoundColumn>
                    <asp:BoundColumn HeaderText="拍摄时间" DataField="ScreenTime"></asp:BoundColumn>
                    <asp:BoundColumn HeaderText="格式" DataField="Format"></asp:BoundColumn>
                    <asp:BoundColumn HeaderText="时长" DataField="Duration"></asp:BoundColumn>
                    <asp:TemplateColumn HeaderText="操作">
                        <ItemTemplate>
                            <asp:LinkButton ID="btnDel" runat="server" CausesValidation="False" CommandName="Delete" OnClientClick="return confirm('您确定要删除吗？');">删除</asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                </Columns>
                <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
            </cc1:centergrid>
</div>
<div id="buttomToolbar">
    <input type="button" value="上传" id="btnAdd" onclick="ShowDialog()" />
</div>
