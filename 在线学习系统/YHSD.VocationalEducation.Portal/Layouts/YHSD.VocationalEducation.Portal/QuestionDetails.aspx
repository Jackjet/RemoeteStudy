<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Import Namespace="Microsoft.SharePoint.ApplicationPages" %>
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="QuestionDetails.aspx.cs" Inherits="YHSD.VocationalEducation.Portal.Layouts.YHSD.VocationalEducation.Portal.QuestionDetails" DynamicMasterPageFile="~masterurl/default.master" %>

<asp:Content ID="PageHead" ContentPlaceHolderID="PlaceHolderAdditionalPageHead" runat="server">
    <script src="js/layer/jquery.min.js"></script>
    <script src="js/layer/layer.js"></script>
    <script src="js/layer/OpenLayer.js"></script>
    <script src="js/Tabs.js"></script>
    <link href="css/type.css" rel="stylesheet" />
    <link href="css/Tabs.css" rel="stylesheet" />
    <script type="text/javascript">
        var id = "<%=Request["id"]%>";
        var type = "<%=Request["type"]%>";
        function LoadMulti(){
            //$("#txtChooseTitle").val();
        }
        $(function () {
            switch (type) {
                case "多选题":
                    LoadMulti();
                    break;
                default:
                    layer.msg("异常命令!");
                    break;
            }
        });
    </script>
</asp:Content>

<asp:Content ID="Main" ContentPlaceHolderID="PlaceHolderMain" runat="server">
    
    <div id="menu">
        <!--tag标题-->
        <ul id="nav">
            <li><a href="#" class="selected">单选题</a></li>
            <li><a href="#" class="">多选题</a></li>
            <li><a href="#" class="">判断题</a></li>
            <li><a href="#" class="">排序题</a></li>
            <li><a href="#" class="">简答题</a></li>
        </ul>
        <!--二级菜单-->
        <div id="menu_con">
            <!--单选-->
            <div class="tag" style="display:block">
                <div>
                    <table>
                        <tr>
                            <td>标题:</td>
                            <td>
                                <textarea rows="20" cols="50" id="txtChooseTitle" class="input_part" style="height: 50px; width: 350px; overflow-x: hidden"></textarea>
                            </td>
                        </tr>
                        <tr>
                            <td>分类:</td>
                            <td><input type="hidden" id="CfId"/><span id="CfName">未选择</span><input type="button" value="选择" id="btnSltCf" class="button_s"/></td>
                        </tr>
                        <tr>
                            <td>选项:</td>
                            <td>
                                <div id="optionList"></div>
                                <input type="button" value="添加选项" id="btnAddOption" class="button_s" />
                            </td>
                        </tr>
                        <tr>
                            <td>正确答案</td>
                            <td><div id="answerList"></div></td>
                        </tr>
                    </table>
                </div>
                <div>
                    <input type="button" value="保存" id="btnSaveMulti" class="button_s" /><input type="button" value="返回" onclick="javascript: window.history.back();" class="button_s" />
                </div>
            </div>
            <div class="tag" style="display:none">
                选择题(多选)
            </div>
            <div class="tag" style="display:none">
                判断题
            </div>
            <div class="tag" style="display:none">
                排序题
            </div>
            <div class="tag" style="display:none">
                简答题
            </div>
        </div>
    </div>
</asp:Content>

<asp:Content ID="PageTitle" ContentPlaceHolderID="PlaceHolderPageTitle" runat="server">
问题详细
</asp:Content>

<asp:Content ID="PageTitleInTitleArea" ContentPlaceHolderID="PlaceHolderPageTitleInTitleArea" runat="server" >
问题详细
</asp:Content>
