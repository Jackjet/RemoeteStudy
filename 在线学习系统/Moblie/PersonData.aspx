<%@ Page Title="" Language="C#" MasterPageFile="~/Moblie.Master" AutoEventWireup="true" CodeBehind="PersonData.aspx.cs" Inherits="Moblie.PersonData" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="js/layer/skin/layer.css" rel="stylesheet" />
    <link href="js/layer/skin/layer.ext.css" rel="stylesheet" />
    <script src="js/jquery.min.js"></script>
    <script src="js/layer.js"></script>
    <script src="js/OpenLayer.js"></script>

    <style>
        .kaiKeDate {
            font-size: 12px;
            font-family: "微软雅黑","宋体";
            color: #999;
        }
    </style>
    <script>

        function EditUser(id) {
            OL_ShowLayerNo(2, "编辑个人信息", 770, 400, "http://61.50.119.70:1122/sites/StudentEducation/_layouts/15/YHSD.VocationalEducation.Portal/UpdateUser.aspx?id=" + id, function (returnVal) {
            });

        }
        function UpdatePwd() {
            OL_ShowLayerNo(2, "修改密码", 770, 600, "http://61.50.119.70:101/authorize_ChangePassWord.aspx?flag=del&_1448723986133", function (returnVal) {
            });
        }
        function PwdReset() {
            alert("请联系管理员重置密码！");
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">

    <div class="student_main">
        <div class="f_l">

            <div class="all_classes last_classes">
            </div>
            <div class="class_library">
                <div class="library_title">
                    <div class="title">个人信息</div>
                </div>
                <label style="text-align: center;" id="GrxxLabel" runat="server" />


            </div>
        </div>
        <div class="right_part f_r">
        </div>
        <div class="clear"></div>
    </div>
</asp:Content>
