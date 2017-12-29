<%@ Assembly Name="YHSD.VocationalEducation.Portal, Version=1.0.0.0, Culture=neutral, PublicKeyToken=2e18f1308c96fd22" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MenuList.ascx.cs" Inherits="YHSD.VocationalEducation.Portal.CONTROLTEMPLATES.YHSD.VocationalEducation.Portal.MenuList" %>
  <script>
           var parentid;
            var Name;
            $(document).ready(function () {
               // $("div ul ul").hide();
                $(".current_part").click(function () {
                    $("div ul li").removeClass("current_part");
                    $(this).addClass("current_part");
                })
                $(".Menu_List").click(function () {
                    $("div ul li").removeClass("Menu_DianJi");
                    var aNode = $(this);
                    // aNode.parent().text();
                    aNode.addClass("Menu_DianJi");
                    //找到当前a节点的所有ul兄弟字节点
                    var lis = aNode.next("ul");
                    //让子节点显示或隐藏
                    lis.toggle("show");
                });
                $(".nav_list").click(function () {
                    $(".nav_list").removeClass("current_list");
                    $(this).addClass("current_list");
                })
            });
            function AddMenu() {
                if (parentid == null || parentid == "undefined") {
                    parentid = "Root";
                }
                var url="<%=YHSD.VocationalEducation.Portal.Code.Common.CommonUtil.GetChildWebUrl()%>/_layouts/15/YHSD.VocationalEducation.Portal/MendEdit.aspx?parentId=" + parentid;
                //openDialog(url, 420, 300, closeCallback);
                OL_ShowLayerNo(2, "新建菜单", 420, 300, url, function (returnVal) {
                });
                return false;

            }
            function UpdateMenu() {
                if (parentid == null || parentid == "undefined" || parentid == "Root") {
                    LayerAlert("请选择要编辑的节点");
                    return false;
                }
                var url = "<%=YHSD.VocationalEducation.Portal.Code.Common.CommonUtil.GetChildWebUrl()%>/_layouts/15/YHSD.VocationalEducation.Portal/MendEdit.aspx?id=" + parentid;
                //openDialog(url, 420, 300, closeCallback);
                OL_ShowLayerNo(2, "编辑菜单", 420, 300, url, function (returnVal) {
                });
                return false;
            }
            function GetID(id) {
                parentid = id;

            }
            function DeleteMenu() {
                if (parentid != null && parentid != "undefined" && parentid != "Root")
                {
                    document.getElementById("<%=this.DeleteID.ClientID %>").value = parentid;

                    LayerConfirmDelete("确定删除此菜单吗?", function () {
                        var postData = { DeleteID: parentid };
                        $.ajax({
                            type: "Post",
                            url: "<%=YHSD.VocationalEducation.Portal.Code.Common.CommonUtil.GetChildWebUrl()%>/_layouts/15/YHSD.VocationalEducation.Portal/MendEdit.aspx",
                            data: postData,
                            dataType: "text",
                            success: function (returnVal) {
                                if (returnVal != "" && returnVal != "undefined")
                                {
                                    if (returnVal == "ok") {
                                        LayerAlert("删除成功!!!", function () {
                                            window.location.href = window.location.href;
                                        });
                                    }
                                    else {

                                        LayerAlert(returnVal);
                                    }
                                }
                            }
                        });
                    });
                 }
                else {
                       LayerAlert("请选择要删除的菜单!!");
                      }

           }


        </script>

<asp:HiddenField ID="DeleteID" runat="server" />
      <label style="color:black;" id="treeMenu" runat="server" visible="false"></label>
<span style="display:inline-block;width:100%;text-align:left;margin-top:20px;">
    <asp:Button ID="BTAdd" CssClass="button_s" runat="server" Text="新建" OnClientClick="return AddMenu()" />
    <asp:Button ID="BTEdit" CssClass="button_s" runat="server" Text="编辑"  OnClientClick="return UpdateMenu()" />
<%--    <asp:Button ID="BTDelete" CssClass="button_n" OnClick="BTDelete_Click" runat="server" Text="删除"  OnClientClick="return DeleteMenu()"  />--%>
     <input type="button" value="删除" style="display:none;" class="button_n" onclick="DeleteMenu()" />
</span>
