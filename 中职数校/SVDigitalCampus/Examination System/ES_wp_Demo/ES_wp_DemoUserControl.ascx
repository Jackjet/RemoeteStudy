<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ES_wp_DemoUserControl.ascx.cs" Inherits="SVDigitalCampus.考试系统.ES_wp_Demo.ES_wp_DemoUserControl" %>

<script src="../../../../_layouts/15/SVDigitalCampus/Script/jquery-1.8.2.min.js"></script>
<style type="text/css">
    .hide {
        display: none;
    }

    .progress {
        z-index: 2000;
    }

    .mask {
        position: fixed;
        top: 0;
        right: 0;
        bottom: 0;
        left: 0;
        z-index: 1000;
        background-color: #000000;
    }
</style>
<script type="text/javascript">

    $(function () {

        $.ajax2 = function (options) {

            var img = $("#progressImgage");

            var mask = $("#maskOfProgressImage");

            var complete = options.complete;

            options.complete = function (httpRequest, status) {

                img.hide();

                mask.hide();

                if (complete) {

                    complete(httpRequest, status);

                }

            };

            options.async = true;

            img.show().css({

                "position": "fixed",

                "top": "50%",

                "left": "50%",

                "margin-top": function () { return -1 * img.height() / 2; },

                "margin-left": function () { return -1 * img.width() / 2; }

            });

            mask.show().css("opacity", "0.1");

            $.ajax(options);

        };

    });
    function load() {

        $.ajax2({

            url: "http://192.168.1.123:6206/sites/OrderMealSystem/_layouts/15/SVDigitalCampus/ExamSystemHander/ExamSystemDataHander.aspx?action=LoadExamQXml&" + Math.random(),

            success: function (result) {

                $("#result").html(result);

            }

        });
    }
   
</script>
<a ID="load" onclick="load();" href="#" >load</a>
<img id="progressImgage" class="progress hide" alt="" src="http://192.168.1.123:6206/sites/OrderMealSystem/_layouts/15/SVDigitalCampus/Image/ajax-loader.gif" />

<div id="maskOfProgressImage" class="mask hide"></div>
