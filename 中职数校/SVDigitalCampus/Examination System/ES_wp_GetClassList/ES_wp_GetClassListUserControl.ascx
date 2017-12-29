<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ES_wp_GetClassListUserControl.ascx.cs" Inherits="SVDigitalCampus.Examination_System.ES_wp_GetClassList.ES_wp_GetClassListUserControl" %>
   <script type="text/javascript">
       var api = frameElement.api, W = api.opener;
        <%if(SelectMore) 
        {%>
       api.button({
           id: 'valueOk',
           name: '确定',
           callback: ok
       });
       var IDvalue = "";

       function ok() {
           W.ShowValueModel(init1(), IDvalue);
           api.close();
       }
       function init1() {
           var getAllNodes = "";
           var getID = "";
           var tree = document.getElementById("tvClass").getElementsByTagName("Input");
           for (var i = 0; i < tree.length; i++) {
               if (tree[i].type == "checkbox" && tree[i].checked) {
                   if (tree[i].getAttribute("title", 2) != "") {
                       getAllNodes = getAllNodes + tree[i].nextSibling.innerHTML + ",";
                       IDvalue = IDvalue + tree[i].nextSibling.title + ",";
                   }
               }
           }

           return getAllNodes;
       }
       <%} %>
 
    </script>
  <div class="tableBoxNewDiv">
            <table border="0" class="tableBoxNew" cellpadding="0" cellspacing="0">
                <tr>
                    <th class="w3" scope="col">
                        班级列表
                    </th>
                </tr>
                <tr>
                    <td class="newtd">
                        <asp:TreeView ID="tvClass" onclick="OnTreeNodeChecked()" runat="server" ShowCheckBoxes="None"
                            CssClass="treeView" ShowLines="true" ExpandDepth="0" PopulateNodesFromClient="false"
                            ShowExpandCollapse="true" EnableViewState="false">
                            <LevelStyles>
                                <asp:TreeNodeStyle Font-Underline="False" />
                                <asp:TreeNodeStyle Font-Underline="False" />
                                <asp:TreeNodeStyle Font-Underline="False" />
                                <asp:TreeNodeStyle Font-Underline="False" />
                            </LevelStyles>
                        </asp:TreeView>
                    </td>
                </tr>
                <%
                    if (IsReset)
                    {
                %>
                <tr>
                    <td>
                        <input id="btnReset" type="button" onclick="javascript: window.parent.location.reload(true);" value="清空" />
                    </td>
                </tr>
                <%} %>
            </table>
        </div>