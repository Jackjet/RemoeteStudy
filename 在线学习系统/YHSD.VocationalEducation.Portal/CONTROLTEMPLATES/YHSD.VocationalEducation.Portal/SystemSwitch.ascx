<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SystemSwitch.ascx.cs" Inherits="YHSD.VocationalEducation.Portal.CONTROLTEMPLATES.YHSD.VocationalEducation.Portal.SystemSwitch" %>

<style type="text/css">
    .SystemPartyMember {
    }

    .SystemTeacher {
    }

    .SystemStudent {
    }

    .SystemSwitch {
        width: auto;
        height: 402px;
    }

        .SystemSwitch div[class^='System'] {
            margin-left: 15px;
            float: left;
            width: 300px;
            height: 400px;
            cursor: pointer;
            border: 1px solid silver;
            background-image: url("res/Img/toolBg.gif");
            background-repeat: repeat-x;
            background-size: 300px 400px;
            text-align: center;
            font-size: 40px;
        }

    .DisableSystem {
        background-color: #000;
        opacity: 0.5;
        filter: progid:DXImageTransform.Microsoft.Alpha(opacity=50);
        cursor: default !important;
    }

    .DisableSystem {
        background-color: #000;
        opacity: 0.5;
        filter: progid:DXImageTransform.Microsoft.Alpha(opacity=50);
        cursor: default !important;
    }
</style>
<div class="SystemSwitch">
    <div class="SystemStudent">
        <span>
            <br />
            <br />
            <br />
            职教中心
        </span>
    </div>
    <div class="SystemPartyMember">
        <span>
            <br />
            <br />
            <br />
            党员学习
        </span>
    </div>
    <div class="SystemTeacher DisableSystem">
        <span>
            <br />
            <br />
            <br />
            继续教育
        </span>
    </div>
</div>
