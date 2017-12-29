<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Import Namespace="Microsoft.SharePoint.ApplicationPages" %>
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SystemSwitch.aspx.cs" Inherits="YHSD.VocationalEducation.Portal.Layouts.SystemSwitch"%>

<!DOCTYPE html>
<html>
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <script type="text/javascript" src="YHSD.VocationalEducation.Portal/js/layer/jquery.min.js"></script>
    <script type="text/javascript" src="YHSD.VocationalEducation.Portal/js/layer/layer.js"></script>
    <script type="text/javascript" src="YHSD.VocationalEducation.Portal/js/json.js"></script>
    <script type="text/javascript" src="YHSD.VocationalEducation.Portal/js/layer/OpenLayer.js"></script>
    <script type="text/javascript" src="YHSD.VocationalEducation.Portal/js/FormatUtil.js"></script>
    <title></title>
    <style type="text/css">
        html, body {
            width: 100%;
            height: 100%;
            overflow: hidden;
            margin: 0px;
            background-image: url("YHSD.VocationalEducation.Portal/images/bg.gif");
            background-size: 100% 100%;
            /*filter: progid:DXImageTransform.Microsoft.AlphaImageLoader ( enabled=bEnabled , sizingMethod='scale' , src='images/bg.gif' );*/
        }

        .SystemPartyMember {
            background-image: url("YHSD.VocationalEducation.Portal/images/03.gif");
        }

        .SystemTeacher {
            background-image: url("YHSD.VocationalEducation.Portal/images/02.gif");
        }

        .SystemStudent {
            background-image: url("YHSD.VocationalEducation.Portal/images/01.gif");
        }

        .SystemSwitch {
            min-width: 330px;
            max-width: 990px;
            height: 402px;
            margin: 0 auto;
        }

            .SystemSwitch div[class^='System'] {
                margin-right: 15px;
                float: left;
                width: 302px;
                height: 403px;
                cursor: pointer;
                background-size: 302px 403px;
                text-align: center;
                font-size: 40px;
                display: none;
            }

        .Left_Log {
            background-image: url("YHSD.VocationalEducation.Portal/images/logo_pic.png");
            background-repeat: no-repeat;
            height: 25px;
        }

        .TopDiv {
            padding-top: 15px;
            padding-bottom: 10px;
            padding-left: 15px;
            background-color: #49b700;
        }
    </style>
    <script type="text/javascript">
        $(function () {
            AjaxRequest("YHSD.VocationalEducation.Portal/Handler/SystemSwitchHandler.aspx", { CMD: "GetPower" }, function (data) {
                var loadIndex = layer.load(2);
                var index = 0;
                var url;
                for (var i in data) {
                    var item = $.parseJSON(data[i]);
                    switch (i) {
                        case "HadStudentSystemPower":
                            if (item.Had) {
                                url = item.Url;
                                $("#SystemStudent").prop("url", item.Url);
                                $("#SystemStudent").show();
                                index += 1;
                            }
                            break;
                        case "HadTeacherSystemPower":
                            if (item.Had) {
                                url = item.Url;
                                $("#SystemTeacher").prop("url", item.Url);
                                $("#SystemTeacher").show();
                                index += 1;
                            }
                            break;
                        case "HadPartyMemberSystemPower":
                            if (item.Had) {
                                url = item.Url;
                                $("#SystemPartyMember").prop("url", item.Url);
                                $("#SystemPartyMember").show();
                                index += 1;
                            }
                            break;
                        default:
                            break;
                    }
                }
                layer.close(loadIndex);
                if (index == 1)
                    location.href = url;
                else if (index == 0)
                    LayerAlert("您没有权限访问任何系统!");
            });
            $("#SystemSwitch div[id^='System']").click(function () {
                var url = $(this).prop("url");
                if (url)
                    location.href = url;
            })
        })
    </script>
</head>
<body>
    <div class="TopDiv">
        <div class="Left_Log">
        </div>
    </div>
    <div style="top: 50%; width: 100%; margin-top: -200px; position: absolute;">
        <div style="text-align: center;">
            <div style="display: inline-block;">
                <div class="SystemSwitch" id="SystemSwitch">
                    <div class="SystemStudent DisableSystem" id="SystemStudent">
                    </div>
                    <div class="SystemTeacher DisableSystem" id="SystemTeacher">
                    </div>
                    <div class="SystemPartyMember DisableSystem" id="SystemPartyMember">
                    </div>
                </div>
            </div>
        </div>
    </div>
</body>
</html>
