<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TitleUserControl.ascx.cs" Inherits="SVDigitalCampus.Master.Title.TitleUserControl" %>
<ul>
    <li>
        <dl class="slidedown">
            <dt class=""><a href="#"><i class="photo"><img src='<%=UserPhoto %>' style="width:30px;height:30px;"/></i><em><%=User %></em></a></dt>
            <dd>
                <div class="slidecon" style="display: none;">
                    <ul class="s_con">
                        <li><a href="#">个人设置</a></li>
                        <li><a href="#">消息提醒</a></li>
                         <li><a href="javascript:void(0);" onclick="loginout();">退出系统</a></li>
                    </ul>
                </div>
            </dd>
        </dl>
    </li>
</ul>
<script src="../../../../_layouts/15/SVDigitalCampus/Script/jquery-1.8.2.min.js"></script>
<script type="text/javascript">

    function loginout() {
          <%HttpContext.Current.Session["Student"] = null;%>
        window.location.href = '<%=loginurl%>';
    }
</script>