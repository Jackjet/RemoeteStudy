<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CouseEditUserControl.ascx.cs" Inherits="SVDigitalCampus.School_Courses.CouseEdit.CouseEditUserControl" %>
<script src="../../_layouts/15/SVDigitalCampus/Script/JSBase.js"></script>
<script src="../../../../_layouts/15/SVDigitalCampus/Script/jquery-1.8.2.min.js"></script>
<link href="../../../../_layouts/15/SVDigitalCampus/CSS/common.css" rel="stylesheet" />
<link type="text/css" rel="stylesheet" href="../../../../_layouts/15/SVDigitalCampus/CSS/style.css" />
<link type="text/css" rel="stylesheet" href="../../../../_layouts/15/SVDigitalCampus/CSS/iconfont.css" />
<link type="text/css" rel="stylesheet" href="../../../../_layouts/15/SVDigitalCampus/CSS/animate.css" />
<script src="../../../../_layouts/15/SVDigitalCampus/Script/Enter/tz_slider.js"></script>

<style type="text/css">
    .tip {
        color: red;
        display: block;
        position: absolute;
        left: 220px;
    }

    .img {
        border: 1px solid #dcdcde;
        width: 160px;
        height: 120px;
    }
</style>
<script type="text/javascript">
    function ShowImage(url) {
        //展示图片        
        document.getElementById("<%=img_Pic.ClientID%>").src = url;
    }
    $(function () {
        BindBase();
        bindMajor();
        BindBeginTime();
        bindSubject();
    });
    function bindMajor() {
        var FirstUrl = window.location.href;
        FirstUrl = FirstUrl.substring(0, FirstUrl.indexOf("SitePages"));

        $.ajax({
            url: FirstUrl + "/_layouts/15/SVDigitalCampus/hander/Major.aspx",
            data: { Func: "GetMajor", MID: $("[id$='hdMajor']").val() },
            type: "POST",
            success: function (data) {
                $("select[id$='ddMajor']").children().remove();
                $("select[id$='ddMajor']").append("<option value=\"0\" selected=\"selected\">请选择专业</option>" + data);
            }
        });
    }
    function bindSubject() {
        var FirstUrl = window.location.href;
        FirstUrl = FirstUrl.substring(0, FirstUrl.indexOf("SitePages"));

        var Majorid = $("select[id$='ddMajor']").val();
        if (Majorid == "0") {
            Majorid = $("[id$='hdMajor']").val();
        }
        if (Majorid != "0") {
            jQuery.ajax({
                url: FirstUrl + "/_layouts/15/SVDigitalCampus/hander/Major.aspx",
                data: { Func: "GetSubJect", Majorid: Majorid, SID: $("[id$='hdSubJect']").val() },
                type: "POST",
                success: function (data) {
                    $("select[id$='ddSubJect']").children().remove();
                    $("select[id$='ddSubJect']").append("<option value=\"0\">请选择专业</option>" + data);
                }
            });
        }
    }
    function BindBeginTime() {
        var FirstUrl = window.location.href;
        FirstUrl = FirstUrl.substring(0, FirstUrl.indexOf("SitePages"));

        jQuery.ajax({
            url: FirstUrl + "/_layouts/15/SVDigitalCampus/hander/Major.aspx",
            data: { Func: "GetStudysection", BID: $("[id$='hdETime']").val() },
            type: "POST",
            success: function (data) {
                $("select[id$='BeginTime']").children().remove();
                $("select[id$='BeginTime']").append("<option  value=\"0\">开学学期</option>" + data);

                $("select[id$='EndTime']").children().remove();
                $("select[id$='EndTime']").append("<option  value=\"0\">结课学期</option>" + data);
            }
        });
    }
    function BindBeginTime() {
        var FirstUrl = window.location.href;
        FirstUrl = FirstUrl.substring(0, FirstUrl.indexOf("SitePages"));

        jQuery.ajax({
            url: FirstUrl + "/_layouts/15/SVDigitalCampus/hander/Major.aspx",
            data: { Func: "GetStudysection", EID: $("[id$='hdETime']").val() },
            type: "POST",
            success: function (data) {
                $("select[id$='BeginTime']").children().remove();
                $("select[id$='BeginTime']").append("<option  value=\"0\">开学学期</option>" + data);

                $("select[id$='EndTime']").children().remove();
                $("select[id$='EndTime']").append("<option  value=\"0\">结课学期</option>" + data);
            }
        });
    }
    function BindBase() {
        var FirstUrl = window.location.href;
        FirstUrl = FirstUrl.substring(0, FirstUrl.indexOf("SitePages"));
        var CourseID = GetRequest().CourseID;

        $.ajax({
            url: FirstUrl + "/_layouts/15/SVDigitalCampus/hander/Caurse.aspx",
            data: { Func: "GetBaseID", CourseID: CourseID },
            type: "POST",
            async: false,
            success: function (returnVal) {
                returnVal = $.parseJSON(returnVal);
                if (returnVal != null) {
                    $($.parseJSON(returnVal.Data)).each(function () {
                        $("[id$='hdMajor']").val(this.MajorID);
                        $("[id$='hdSubJect']").val(this.SubjectID);
                        $("[id$='hdBTime']").val(this.BeginTerm);
                        $("[id$='hdETime']").val(this.EndTerm);

                    });
                }
            }
        });
    }
    function CheckBase() {
        if ($("select[id$='BeginTime']").val() != "") {

            var BeginTime = $("select[id$='BeginTime']").val();
            $("[id$='hdBTime']").val(BeginTime);
        }
        if ($("[id$='EndTime']").val() != "") {

            var EndTime = $("select[id$='EndTime']").val();
            $("[id$='hdETime']").val(EndTime);
        }
        if ($("select[id$='ddMajor']").val() != "") {
            var selectvalue2 = $("select[id$='ddMajor']").val();
            $("[id$='hdMajor']").val(selectvalue2);
        }
        if ($("select[id$='ddSubJect']").val() != "") {
            var selectvalue2 = $("select[id$='ddSubJect']").val();

            $("[id$='hdSubJect']").val(selectvalue2);
        }
    }
    function GetRequest() {
        var url = location.search; //获取url中"?"符后的字串

        var theRequest = new Object();

        if (url.indexOf("?") != -1) {

            var str = url.substr(1);

            strs = str.split("&");

            for (var i = 0; i < strs.length; i++) {

                theRequest[strs[i].split("=")[0]] = (strs[i].split("=")[1]);

            }

        }
        return theRequest;
    }
</script>
<div id="Editdiv" class="yy_dialog" style="display: block; top: 0; left: 0">
    <input id="hdMajor" type="hidden" runat="server" />
    <input id="hdBTime" type="hidden" runat="server" />
    <input id="hdETime" type="hidden" runat="server" />
    <input id="hdSubJect" type="hidden" runat="server" />

    <div class="t_content">
        <div class="yy_tab">
            <div class="content">
                <div class="Caursetc">
                    <div class="t_message">
                        <div class="message_con">

                            <table class="m_top">
                                <tr>
                                    <td class="mi"><span class="m">课程名称：</span></td>
                                    <td class="kuc">
                                        <input id="txtName" runat="server" class="hu" placeholder=" 请输入课程名称" onchange="confirmRepeat(this)" />
                                        <i class=""></i>
                                        <span class="tip"></span>
                                    </td>
                                    <td rowspan="3" colspan="2" style="width: 300px; height: 100px; margin: auto; text-align: center; vertical-align: bottom;">
                                        <asp:Image ID="img_Pic" CssClass="img" runat="server" />
                                        <input type="file" id="fimg_Asso" name="fimg_Asso" runat="server" onchange="ShowImage(this.value);" style="width: 120px; display: none;" />
                                        <a href="#" onclick="$('#<%=fimg_Asso.ClientID%>').click();" style="display: block; color: blue;">更换图片</a>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="mi"><span class="m">主讲老师：</span></td>
                                    <td class="kuc">
                                        <asp:DropDownList ID="ddTeacher" runat="server" Width="220px">
                                        </asp:DropDownList>
                                        <i class=""></i>
                                        <span class="tip"></span>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="mi"><span class="m">人数上限：</span></td>
                                    <td class="kuc">
                                        <input type="text" placeholder=" 请输入人数上限" id="Count" runat="server" onchange="confirmNull(this)" onkeyup="this.value=this.value.replace(/\D/g,'')" onafterpaste="this.value=this.value.replace(/\D/g,'')">
                                        <i class=""></i></td>
                                </tr>
                                <%--  <tr>
                                    <td class="mi"><span class="m">开课学年：</span></td>
                                    <td class="kuc">
                                        <input type="text" placeholder=" 请输入开课学年" id="Year" runat="server" onchange="confirmNull(this)" />
                                        <i class=""></i></td>

                                    <td class="mi"><span class="m">开课学期：</span></td>
                                    <td>
                                        <asp:DropDownList ID="ddDate" runat="server" Width="220px">
                                            <asp:ListItem Value="1">第一学期</asp:ListItem>
                                            <asp:ListItem Value="2">第二学期</asp:ListItem>
                                            <asp:ListItem Value="3">第三学期</asp:ListItem>
                                            <asp:ListItem Value="4">第四学期</asp:ListItem>
                                        </asp:DropDownList>
                                        <i class=""></i>
                                        <span class="tip"></span>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="mi"><span class="m">开课时间：</span></td>
                                    <td class="kuc">
                                        <input type="text" readonly="readonly" runat="server" id="BeginTime" onclick="WdatePicker()" onchange="confirmNull(this)" />
                                        <i class=""></i>
                                        <span class="tip"></span>
                                    </td>

                                    <td class="mi"><span class="m">结课时间：</span></td>
                                    <td class="kuc">
                                        <input type="text" readonly="readonly" runat="server" id="EndTime" onclick="WdatePicker()" onchange="confirmNull(this)" />
                                        <i class=""></i>
                                        <span class="tip"></span>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="mi"><span class="m">所属专业：</span></td>
                                    <td class="kuc">
                                        <asp:DropDownList ID="ddMajor" runat="server" Width="220px" AutoPostBack="True" DataTextField="NJMC" DataValueField="NJ"></asp:DropDownList>
                                        <i class=""></i>
                                        <span class="tip"></span>
                                    </td>

                                    <td class="mi"><span class="m">所属学科：</span></td>
                                    <td class="kuc">
                                        <asp:DropDownList ID="ddSubJect" runat="server" Width="220px" AutoPostBack="True" DataTextField="Title" DataValueField="ID"></asp:DropDownList>
                                        <i class=""></i>
                                        <span class="tip"></span>
                                    </td>
                                </tr>--%>
                                <tr>
                                    <td class="mi"><span class="m">开课时间：</span></td>
                                    <td class="kuc">
                                        <select name="education" id="BeginTime" runat="server" style="width: 220px;">
                                        </select>
                                        <i class=""></i>
                                        <span class="tip"></span>
                                    </td>

                                    <td class="mi"><span class="m">结课时间：</span></td>
                                    <td class="kuc">
                                        <select name="education" id="EndTime" runat="server" style="width: 220px;">
                                        </select>
                                        <i class=""></i>
                                        <span class="tip"></span>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="mi"><span class="m">所属专业：</span></td>
                                    <td class="kuc">
                                        <select name="ddMajor" id="ddMajor" runat="server" style="width: 220px;" onchange="bindSubject()">
                                        </select>
                                        <i class=""></i>
                                        <span class="tip"></span>
                                    </td>

                                    <td class="mi"><span class="m">所属学科：</span></td>
                                    <td class="kuc">
                                        <select name="ddSubJect" id="ddSubJect" runat="server" style="width: 220px;"></select>
                                        <i class=""></i>
                                        <span class="tip"></span>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="mi"><span class="m">课程简介：</span></td>
                                    <td colspan="3">
                                        <textarea id="Introduc" cols="20" rows="10" style="width: 550px; height: 80px;" runat="server"></textarea>

                                        <%--<input type="text" placeholder=" 请输入课程简介" id="Introduc" runat="server" style="width: 550px;">--%>
                                        <i class=""></i></td>
                                </tr>
                                <tr>
                                    <td class="mi"><span class="m">课程通知：</span></td>
                                    <td colspan="3">
                                        <textarea id="Annonce" cols="20" rows="10" style="width: 550px; height: 80px;" runat="server"></textarea>

                                        <%--<input type="text" placeholder=" 请输入课程简介" id="Introduc" runat="server" style="width: 550px;">--%>
                                        <i class=""></i></td>
                                </tr>
                            </table>
                        </div>

                    </div>
                    <div class="t_btn">
                        <asp:Button ID="btOk" runat="server" Text="保存" OnClientClick="CheckBase()" OnClick="btOk_Click" />
                    </div>
                </div>

            </div>
        </div>

    </div>

</div>
