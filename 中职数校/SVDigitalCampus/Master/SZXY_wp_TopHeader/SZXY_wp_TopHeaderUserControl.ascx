<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SZXY_wp_TopHeaderUserControl.ascx.cs" Inherits="SVDigitalCampus.Master.SZXY_wp_TopHeader.SZXY_wp_TopHeaderUserControl" %>
<script src="../../../../_layouts/15/SVDigitalCampus/Script/jquery-1.8.2.min.js"></script>
<style type="text/css">
    .userphoto{width:30px;height:30px;}
</style>
		    <div class="center_top">
			    <div class="left_logo fl">
				     <img src="/_layouts/15/SVDigitalCampus/Image/logo.png"/>
				</div>
				<div class="message_right fr">
				   <div class="xiaoxi fl">
				    <%--<div class="youxiang fl"><a href="#"><i class="iconfont">&#xe92e;</i></a></div>
					<div class="shezhi fl"><a href="#"><i class="iconfont">&#xe621;</i></a></div>--%>
				   </div>	
				     <div class="email fl">	  
	             	     <ul>
	             	     	<li>
				            <dl class="slidedown">
				                <dt class=""><a href="#"><i class="photo userphoto"><img src='<%=UserPhoto %>' style="width:30px;height:30px;"/></i><em><%=SPContext.Current.Web.CurrentUser.Name %></em></a></dt>
				                <dd>
				                 <div class="slidecon" style="display: none;">
				                        <ul class="s_con">
				                            <li><a href="javascript:void(0);" onclick="loginout();">退出系统</a></li>
				                        </ul>
				                    </div>
				                </dd>
				            </dl>
				           </li>
	             	     </ul>  		
				     </div>
				</div>
			</div>
			<div class="clear"></div>
<script src="../../../../_layouts/15/SVDigitalCampus/Script/jquery-1.8.2.min.js"></script>
<script type="text/javascript">

    function loginout() {
        <%HttpContext.Current.Session["Student"] = null;%>
        window.location.href = '<%=loginurl%>';
    }
</script>