<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ES_wp_ExamPaperMarkUserControl.ascx.cs" Inherits="SVDigitalCampus.Examination_System.ES_wp_ExamPaperMark.ES_wp_ExamPaperMarkUserControl" %>
<link href="../../../../_layouts/15/SVDigitalCampus/CSS/common.css" rel="stylesheet" />
<link href="../../../../_layouts/15/SVDigitalCampus/CSS/style.css" rel="stylesheet" />
<link href="../../../../_layouts/15/SVDigitalCampus/CSS/iconfont.css" rel="stylesheet" />
<link href="../../../../_layouts/15/SVDigitalCampus/CSS/animate.css" rel="stylesheet" />
<script src="../../../../_layouts/15/SVDigitalCampus/Script/jquery-1.8.2.min.js"></script>
<script src="../../_layouts/15/SVDigitalCampus/Script/tz_slider.js"></script>

<div class="Examination">
    <div class="centercon_e">
        <div class="tit">
            <h2>
                <asp:Label ID="ExamTitle" runat="server" Text=""></asp:Label><asp:HiddenField ID="ExamID" runat="server" />
            </h2>
          <%--  <div class="tit_bottom">
                <span>考试科目：</span>--%>
                <%--   <span>考试时间：<asp:Label ID="ExamTime" runat="server" Text=""></asp:Label>分钟</span>
                <span>总分：<asp:Label ID="FullScore" runat="server" Text=""></asp:Label>分</span>--%>
            <%--</div>--%>
        </div>
        <div class="Exammessage">
            <div>
                <table class="S_analysis">
                    <tr>
                        <td rowspan="2">学生姓名：<asp:Label ID="StuName" runat="server" Text=""></asp:Label></td>
                        <td rowspan="2">
                            考试科目：<asp:Label ID="Subject" runat="server" Text=""></asp:Label><br />
                            班级： <asp:Label ID="ClassName" runat="server" Text=""></asp:Label>
                        </td>
                        <td>客观题得分：
                        </td>
                        <td>主观题得分：
                        </td>
                        <td>总得分：
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="ObjScore" runat="server" Text="0"></asp:Label></td>
                        <td>
                            <asp:Label ID="SubScore" runat="server" Text="0"></asp:Label></td>
                        <td>
                            <asp:Label ID="TotalScore" runat="server" Text="0"></asp:Label></td>
                    </tr>
                </table>
            </div>
        </div>
        <div>
            <asp:ListView ID="lv_ExamQType" runat="server">
                <ItemTemplate>
                    <div class="firsttit">
                        <h2><%#Eval("TypeName") %><span class="xiaozi">（<%#Eval("TypeCount") %>题，每题<%#Eval("Score") %>分<%#Eval("SubScore") %>）</span></h2>
                        <asp:HiddenField ID="TypeID" runat="server" Value='<%#Eval("TypeID") %>' />
                        <asp:HiddenField ID="Template" runat="server" Value='<%#Eval("OrderID") %>' />
                        <asp:HiddenField ID="QType" runat="server" Value='<%#Eval("QType") %>' />
                        <ul class="timu">
                            <li>
                                <asp:ListView ID="QuestionTypeOne" runat="server">
                                    <ItemTemplate>
                                        <div class="defen fl">
                                            <span class="defenname">得分</span><br />
                                            <span class="fenzhi">
                                                <input type="text" id='PGetScore' name='<%#"PGetScore"+Eval("ID") %>' onkeyup="WorkSocre(this)" class="fenzhiinput" /></span>
                                            <asp:HiddenField ID="qID" runat="server" Value='<%#Eval("ID") %>' />
                                        </div>
                                        <div class="ticon fl">
                                            <p class="biaoti">
                                                <%# Eval("Count") %> .<%#Eval("Question") %>
                                            </p>
                                            <p class="xuanxiangkuang">
                                                <span><%#Eval("YourAnswer") %></span>
                                            </p>
                                            <p class="xuanxiangkuang" style='display: <%#Eval("kuangIsShow")%>'>
                                                <span><%#Eval("Answer") %></span>
                                            </p>
                                            <p>
                                                <%#Eval("Analysis") %>
                                            </p>
                                        </div>
                                        <div class="clear"></div>
                                    </ItemTemplate>
                                </asp:ListView>
                            </li>
                        </ul>
                    </div>
                </ItemTemplate>
            </asp:ListView>
        </div>
      <div class="clear"></div>
        <div class="MarkAnalysisdiv"><span class="MarkAnalysis">批语：</span><asp:TextBox ID="MarkAnalysis"  runat="server" CssClass="markatext"></asp:TextBox></div>
      <div class="clear"></div>
    <div class="submit_btn">
        <asp:Button ID="btnOk" runat="server" Text="确定" CssClass="submitbtn" OnClick="btnOk_Click" />
    </div>
    </div>
</div>
<script type="text/javascript">
    function WorkSocre(obj)
    {
        if ($(obj).val().length > 0) {
            if (isNaN($(obj).val())) {
                alert("请正确输入！");
                $(obj).val($(obj).next().val());
                return;
            }
            if ($(obj).val().indexOf('.') >= 0 || $(obj).val() < 0) {
                alert("请输入正数！");
                $(obj).val($(obj).next().val());
                return;
            }
        } else {
            alert("请输入数字!");
            $(obj).val($(obj).next().val());
            return;
        }
        var subscores = "0";
        $('[id^=PGetScore]').each(function () {
            if ($(this).val().trim()!=""){
                subscores = (parseFloat(subscores) + parseFloat($(this).val())).toString();
            }
        });
        $('#<%=SubScore.ClientID%>').text(subscores);
        var objsocres = $('#<%=ObjScore.ClientID%>').text();
        var totalscores = parseFloat(objsocres) + parseFloat(subscores);
        $('#<%=TotalScore.ClientID%>').text(totalscores);
    }
</script>