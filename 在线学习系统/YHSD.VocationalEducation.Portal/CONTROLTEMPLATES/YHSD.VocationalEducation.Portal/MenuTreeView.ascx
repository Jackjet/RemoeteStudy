<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MenuTreeView.ascx.cs" Inherits="YHSD.VocationalEducation.Portal.CONTROLTEMPLATES.YHSD.VocationalEducation.Portal.MenuTreeView" %>
<link href="/_layouts/15/YHSD.VocationalEducation.Portal/css/style.css" rel="stylesheet" />
 
<style type="text/css">.validatebox-text{cursor:pointer !important;}</style>

      <script type="text/javascript">
          $(document).ready(function () {
              $.parser.parse($("#<%=ComBox.ClientID%>"));
              $("#TextComBox").combobox({
                  panelHeight: "auto",
                  onSelect: function (param) {
                      location.href = param.value;
                  },
                  onLoadSuccess: function () {

                      $('#LogoImg').attr("src", "/_layouts/15/YHSD.VocationalEducation.Portal/images<%=YHSD.VocationalEducation.Portal.Code.Common.CommonUtil.GetChildWebUrl()%>.png");

                  }
              });

              $("div ul ul").hide();
              var href = window.location.href;
              $("span[href='" + href + "']").each(function () {
                  $("li").removeClass("Menu_DianJi");
                  $("span").removeClass("Menu_DianJi");
                  var aNode = $(this);
                  //将上级ul展开
                  aNode.parents("ul").css("display", "block");
                  aNode.addClass("Menu_DianJi");
                  //找到当前a节点的所有ul兄弟字节点
                  var lis = aNode.parent().next("ul");
                  //让子节点显示或隐藏
                  lis.toggle("show");
              });

          });
          function targetUrl(url, o) {


              if (url == window.location.href || url == "" || url == "#") {
                  $("span").removeClass("Menu_DianJi");
                  $("li").removeClass("Menu_DianJi");
                  $(o).addClass("Menu_DianJi");
                  $(o).next("ul").toggle("show");
                  return;
              }

                  window.location = url;
          }

        </script>

  <label id="treeMenu" runat="server" visible="false"></label>
 <div style="position: absolute;top:0px;">
                            <img id="LogoImg" style="height:50px;" src="/_layouts/15/YHSD.VocationalEducation.Portal/images/ContinuationEducation.png" />
                        </div>
<div style="top: 15px; margin-left: 690px; position: absolute; display:none;">
    <label id="ComBox" runat="server"></label>
</div>