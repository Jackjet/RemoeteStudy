<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CaurseAddUserControl.ascx.cs" Inherits="SVDigitalCampus.School_Courses.CaurseAdd.CaurseAddUserControl" %>
<script src="../../../../_layouts/15/SVDigitalCampus/Script/jquery-1.8.2.min.js"></script>
<link href="../../../../_layouts/15/SVDigitalCampus/CSS/common.css" rel="stylesheet" />
<link type="text/css" rel="stylesheet" href="../../../../_layouts/15/SVDigitalCampus/CSS/style.css" />
<link type="text/css" rel="stylesheet" href="../../../../_layouts/15/SVDigitalCampus/CSS/iconfont.css" />
<link type="text/css" rel="stylesheet" href="../../../../_layouts/15/SVDigitalCampus/CSS/animate.css" />
<script src="../../../../_layouts/15/SVDigitalCampus/Script/Enter/tz_slider.js"></script>
<script src="../../../../_layouts/15/SVDigitalCampus/Script/DatePicker/WdatePicker.js"></script>
<style type="text/css">
    .tip {
        color: red;
        display: block;
        position: absolute;
        font-size: 12px;
        line-height: 16px;
    }

    .img {
        border: 1px solid #dcdcde;
        width: 160px;
        height: 120px;
    }
</style>
<script type="text/javascript">
    $(function () {
        bindMajor();
        BindBeginTime();
    });
    function bindMajor(){
        var FirstUrl = window.location.href;
        FirstUrl = FirstUrl.substring(0, FirstUrl.indexOf("SitePages"));
        
        $.ajax({
            url: FirstUrl + "/_layouts/15/SVDigitalCampus/hander/Major.aspx",
            data: { Func: "GetMajor"},
            type: "POST",
            success: function (data) {
                $("select[id$='ddMajor']").children().remove();
                $("select[id$='ddMajor']").append("<option selected=\"selected\" value=\"0\">请选择专业</option>" + data);
                if ($("select[id$='ddMajor'] option:selected").val() != null && $("select[id$='ddMajor'] option:selected").val() != 0) {
                }
            }
        });
    }
    function bindSubject() {

        var FirstUrl = window.location.href;
        FirstUrl = FirstUrl.substring(0, FirstUrl.indexOf("SitePages"));

        var Majorid = $("select[id$='ddMajor']").val();
        $("[id$='hdMajor']").val(Majorid);

        if (Majorid != "0") {
            jQuery.ajax({
                url: FirstUrl + "/_layouts/15/SVDigitalCampus/hander/Major.aspx",
                data: { Func: "GetSubJect", Majorid:Majorid},
                type: "POST",
                success: function (data) {
                    $("select[id$='ddSubJect']").children().remove();
                    $("select[id$='ddSubJect']").append("<option selected=\"selected\" value=\"0\">请选择专业</option>" + data);
                    if ($("select[id$='ddSubJect'] option:selected").val() != null && $("select[id$='ddSubJect'] option:selected").val() != 0) {

                    }
                }
            });
        }
    }
    function BindBeginTime(){
        var FirstUrl = window.location.href;
        FirstUrl = FirstUrl.substring(0, FirstUrl.indexOf("SitePages"));

        jQuery.ajax({
            url: FirstUrl + "/_layouts/15/SVDigitalCampus/hander/Major.aspx",
            data: { Func: "GetStudysection"},
            type: "POST",
            success: function (data) {
                $("select[id$='BeginTime']").children().remove();
                $("select[id$='BeginTime']").append("<option selected=\"selected\" value=\"0\">开学学期</option>" + data);
                
                $("select[id$='EndTime']").children().remove();
                $("select[id$='EndTime']").append("<option selected=\"selected\" value=\"0\">结课学期</option>" + data);
            }
        });
    }
   
    function CheckBase() {       
        if (!isTrue()) {
            return;
        }
    }

    function isTrue() 
    {
        var flag = true;
        if ($("[id$='txtName']").val() == "") {
            $("[id$='txtName']").parent().find("i").html("&#xe648;");
            $("[id$='txtName']").parent().find("i").removeClass();
            $("[id$='txtName']").parent().find("i").addClass("iconfont tishi fault_t");
            flag = false;
        }
        if ($("[id$='Count']").val() == "") {
            $("[id$='Count']").parent().find("i").html("&#xe648;");
            $("[id$='Count']").parent().find("i").removeClass();
            $("[id$='Count']").parent().find("i").addClass("iconfont tishi fault_t");
            flag = false;
        }
       
        if ($("select[id$='BeginTime']").val() == "") {
           
            $("[id$='BeginTime']").parent().find("i").html("&#xe648;");
            $("[id$='BeginTime']").parent().find("i").removeClass();
            $("[id$='BeginTime']").parent().find("i").addClass("iconfont tishi fault_t");
            flag = false;
        }
        else{
            var BeginTime = $("select[id$='BeginTime']").val();
            $("[id$='hdBTime']").val(BeginTime);
        }
        if ($("[id$='EndTime']").val() == "") {
            $("[id$='EndTime']").parent().find("i").html("&#xe648;");
            $("[id$='EndTime']").parent().find("i").removeClass();
            $("[id$='EndTime']").parent().find("i").addClass("iconfont tishi fault_t");
            flag = false;
        }
        else{ 
            var EndTime = $("select[id$='EndTime']").val();
            $("[id$='hdETime']").val(EndTime);
        }
        var select1 = document.all.<%= ddMajor.ClientID %>;
        var selectvalue = select1.options[select1.selectedIndex].value; 
        if (selectvalue=="-1") {
            $("[id$='ddMajor']").parent().find("i").html("&#xe648;");
            $("[id$='ddMajor']").parent().find("i").removeClass();
            $("[id$='ddMajor']").parent().find("i").addClass("iconfont tishi fault_t");
            flag = false;

        }
        
        var select2 = document.all.<%= ddSubJect.ClientID %>;
        var selectvalue2 = select2.options[select2.selectedIndex].value; 
        if (selectvalue2=="-1") {
            $("[id$='ddSubJect']").parent().find("i").html("&#xe648;");
            $("[id$='ddSubJect']").parent().find("i").removeClass();
            $("[id$='ddSubJect']").parent().find("i").addClass("iconfont tishi fault_t");
            flag = false;

        }
        else{ 
            $("[id$='hdSubJect']").val(selectvalue2);
        }

        $(".m_top").find("i").each(function (result) {
            //$("#Editdiv .m_top").find("i").each(function (result) {
            if ($(this).attr("class") == "iconfont tishi fault_t") {
                flag = false;
            }
        });
        return flag;
    }
    //function CheckDate() {
    //    $(".yy_tabheader").find("li[id=task]").addClass("selected").siblings().removeClass("selected");
    //    $(".Caursetc").eq(2).show().siblings().hide();
    //}
   
    function confirmRepeat(e) {
        var FirstUrl = window.location.href;
        FirstUrl = FirstUrl.substring(0, FirstUrl.indexOf("SitePages"))
        var txt = $(e).val();
        if (txt) {
            var postData = { Func: "CheckTitle", Title: txt };
            $.ajax({
                type: "Post",
                url: FirstUrl + "/_layouts/15/SVDigitalCampus/hander/Caurse.aspx",
                data: postData,
                dataType: "text",
                success: function (returnVal) {
                    if (returnVal == 0) {
                        $(e).parent().find("i").html("&#xe654;");
                        $(e).parent().find("i").removeClass();
                        $(e).parent().find("i").addClass("iconfont tishi true_t");
                        $(e).parent().find("span").html("");
                    }
                    else {
                        $(e).parent().find("i").html("&#xe648;");
                        $(e).parent().find("i").removeClass();
                        $(e).parent().find("i").addClass("iconfont tishi fault_t");
                        $(e).parent().find("span").html("课程名称重复");
                    }
                },
                error: function (errMsg) {
                    alert('数据加载失败！');
                }
            });
        }
        else {
            $(e).parent().find("i").html("&#xe648;");
            $(e).parent().find("i").removeClass();
            $(e).parent().find("i").addClass("iconfont tishi fault_t");
        }
    }
    function confirmNull(e) {
        var txt = $(e).val();
        if (txt) {
            $(e).parent().find("i").html("&#xe654;");
            $(e).parent().find("i").removeClass();
            $(e).parent().find("i").addClass("iconfont tishi true_t");
        }
        else {
            $(e).parent().find("i").html("&#xe648;");
            $(e).parent().find("i").removeClass();
            $(e).parent().find("i").addClass("iconfont tishi fault_t");
        }
    }
    function ShowImage(url) { //展示图片        
        document.getElementById("<%=img_Pic.ClientID%>").src = url;
    }


 <%--   function ShowImage(url) { //展示图片        
        var prevImg = document.getElementById("<%=img_Pic.ClientID%>");
        if (file.files && file.files[0]) {
            var reader = new FileReader();
            reader.onload = function (evt) {
                prevImg.src = evt.target.result;
            }
            reader.readAsDataURL(file.files[0]);
        }
        else {
            prevImg.style.display = "none";
            var divImg = document.getElementById('divImg');
            divImg.style.display = "block";
            divImg.filters.item("DXImageTransform.Microsoft.AlphaImageLoader").src = file.value;
        }
    }--%>
    function AddWork(){
        popWin.showWin("600", "520", "添加课程作业", '<%=rootUrl%>' + "/SitePages/TaskAdd.aspx", "no");
    }
</script>

<div class="yy_dialog" id="Adddiv" style="top: 0px; left: 0px;">
    <input id="hdMajor" type="hidden" runat="server" />
    <input id="hdBTime" type="hidden" runat="server" />
    <input id="hdETime" type="hidden" runat="server" />
    <input id="hdSubJect" type="hidden" runat="server" />
    <div class="t_content">
        <!---选项卡-->
        <div class="yy_tab">

            <div class="content">
                <div class="Caursetc" style="display: block;">
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
                                        <a href="#" onclick="$('#<%=fimg_Asso.ClientID%>').click();" style="display: block; color: blue;">选择图片</a>
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

                                <tr>
                                    <td class="mi"><span class="m">开课时间：</span></td>
                                    <td class="kuc">
                                        <select name="education" id="BeginTime" runat="server" style="width: 220px;">
                                        </select>
                                        <%--<input type="text" readonly="readonly" runat="server" id="BeginTime" onclick="WdatePicker()" onchange="confirmNull(this)" />--%>
                                        <i class=""></i>
                                        <span class="tip"></span>
                                    </td>

                                    <td class="mi"><span class="m">结课时间：</span></td>
                                    <td class="kuc">
                                        <select name="education" id="EndTime" runat="server" style="width: 220px;">
                                        </select>
                                        <%--<input type="text" readonly="readonly" runat="server" id="EndTime" onclick="WdatePicker()" onchange="confirmNull(this)" />--%>
                                        <i class=""></i>
                                        <span class="tip"></span>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="mi"><span class="m">所属专业：</span></td>
                                    <td class="kuc">
                                        <select name="ddMajor" id="ddMajor" runat="server" style="width: 220px;" onchange="bindSubject()">
                                        </select>
                                        <%--<asp:DropDownList ID="ddMajor" runat="server" Width="220px" AutoPostBack="True" DataTextField="NJMC" DataValueField="NJ" OnSelectedIndexChanged="ddMajor_SelectedIndexChanged"></asp:DropDownList>--%>
                                        <i class=""></i>
                                        <span class="tip"></span>
                                    </td>

                                    <td class="mi"><span class="m">所属学科：</span></td>
                                    <td class="kuc">
                                        <select name="ddSubJect" id="ddSubJect" runat="server" style="width: 220px;"></select>
                                        <%--<asp:DropDownList ID="ddSubJect" runat="server" Width="220px" DataTextField="Title" DataValueField="ID"></asp:DropDownList>--%>
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
                                        <i class=""></i></td>
                                </tr>
                            </table>
                        </div>

                    </div>
                    <div class="t_btn">
                        <asp:Button ID="btOk" runat="server" Text="提交" OnClientClick="return CheckBase()" OnClick="btOk_Click" />
                    </div>
                </div>

            </div>
        </div>

    </div>
</div>
