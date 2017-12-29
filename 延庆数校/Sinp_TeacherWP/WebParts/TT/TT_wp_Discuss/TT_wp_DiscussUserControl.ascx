<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TT_wp_DiscussUserControl.ascx.cs" Inherits="Sinp_TeacherWP.WebParts.TT.TT_wp_Discuss.TT_wp_DiscussUserControl" %>
<link href="/_layouts/15/Style/animate.css" rel="stylesheet" />
<link href="/_layouts/15/Style/sg/sg.css" rel="stylesheet" />
<link href="/_layouts/15/Style/discuss.css" rel="stylesheet" />
<link href="/_layouts/15/Style/layout.css" rel="stylesheet" />
<link href="/_layouts/15/Style/style.css" rel="stylesheet" />
<script src="/_layouts/15/Script/jquery-1.8.0.js"></script>
<script src="/_layouts/15/Script/uploadFile.js"></script>
<script type="text/javascript">
    function setValue() {

        var nowfile = $("#dv_content").text();
        $("input[id$=Hid_content]").val(nowfile);
    }
    function setSecValue(event)
    {
        var preval = $(Event).prev().text();
        $(Event).next().val(preval);
    }
</script>
<div class="PE_wrap">
<div class="top_TPcon">
    <div class="arttit">
        <asp:Literal ID="Lit_Title" runat="server"></asp:Literal>
    </div>
    <div class="artsectit">作者：<asp:Literal ID="Lit_CreateUser" runat="server"></asp:Literal>&nbsp;&nbsp;点击数：<asp:Literal ID="Lit_ClickCount" runat="server"></asp:Literal>&nbsp;&nbsp;更新时间：<asp:Literal ID="Lit_UpdateTime" runat="server"></asp:Literal></div>
    <div class="artcontent">
        <asp:Literal ID="Lit_Content" runat="server"></asp:Literal>
        <asp:Literal ID="Lit_Attrchment" runat="server"></asp:Literal>
    </div>

</div>
<!--timeline start-->
<div class="timeline">
    <div class="t_send" style='display:<%=IsPast?"none":"block"%>'>
        <div class="t_header">
            <asp:Image runat="server" ID="Img_CurrentPic" Width="50" Height="50" />
            <span class="name">
                <asp:Literal ID="Lit_CurrentName" runat="server"></asp:Literal></span>
        </div>
        <div class="t_icon"></div>
        <div class="t_box">
            <p class="t_title">记录内容：</p>
            <div id="dv_content" class="t_msg" contenteditable="true"></div>
            <asp:HiddenField ID="Hid_content" runat="server" />
            <div class="t_face">
                <%--<a href="#" class="t_face_btn">上传附件</a>--%>
                <div style="width: 96%; margin: 10px auto auto auto;">
                    <asp:Label ID="lbAttach" runat="server">
                    </asp:Label>
                    <asp:HiddenField ID="Hid_fileName" runat="server" />
                    <table id="idAttachmentsTable" style="display: block;">
                        <asp:Literal ID="Lit_Bind" runat="server"></asp:Literal>
                    </table>
                    <table style="width: 100%; display: block;">
                        <tr>
                            <td id="att1">&nbsp;
                                            <input id="fileupload0" name="fileupload0" style="width: 80%;" type="file" />

                            </td>
                            <td>
                                <input onclick="AddFile()" style="width: 70px;" type="button" value="上传" /></td>
                        </tr>
                    </table>
                </div>
                <asp:LinkButton OnClick="LB_Publish_Click" CssClass="t_send_btn" ID="LB_Publish" runat="server" OnClientClick="setValue();">发表</asp:LinkButton>
                <%--<a href="#" class="t_send_btn">发 表</a>--%>
            </div>
        </div>
    </div>


    <asp:ListView ID="LV_FirstReply" runat="server" OnItemCommand="LV_FirstReply_ItemCommand" OnItemDataBound="LV_FirstReply_ItemDataBound" OnPagePropertiesChanging="LV_FirstReply_PagePropertiesChanging">        
        <LayoutTemplate>
            <div id="itemPlaceholder" runat="server"></div>
        </LayoutTemplate>
        <ItemTemplate>
            <div class="t_all">
                <div class='t_list animated bounceIn'>
                    <div class='l_header'>
                        <img src='<%#Eval("PictUrl") %>' alt='' width='50' height='50' /><span class="name"><%#Eval("ReplyUser") %></span>
                    </div>
                    <div class='l_icon'></div>
                    <div class='l_msg'>
                        <div class="firstcon">
                            <div class="l_msgcon"><%#Eval("Content") %></div>
                            <div class="co_con">
                                <div class="co_left fl">
                                    <a href="#">附件：<%#Eval("Attachment") %></a></div>
                                <div class="co_right fr">
                                    <span class="Reply">回复(<asp:Literal ID="Lit_Count" runat="server"></asp:Literal>)</span>	|
                                <%--<span class="Comment"><a href="#">评论(<i class="con_num">2</i>)</a></span>|--%>
                                <span class="Praise">
                                    <asp:LinkButton ID="LB_Zan" runat="server" CommandName="Zan" CommandArgument='<%# Eval("ID") %>'><img src="/_layouts/15/TeacherImages/zan.png" width="18" /><i class="con_num"><%#Eval("PraiseCount") %></i></asp:LinkButton>
                                    </span>|
                                <span class="date"><%#Eval("Created") %></span>
                                </div>
                            </div>
                        </div>
                        <div class="clear"></div>
                        <div class="secondcon" style="display: none;">
                            <div class='s_icon'></div>
                            <div class="clear"></div>
                            <div class="s_con">
                                <div class="Publish" style='display:<%=IsPast?"none":"block"%>'>
                                    <asp:HiddenField ID="Hid_ItemId" runat="server" Value='<%# Eval("ID") %>' />
                                              <div><asp:TextBox ID="TB_SecondVal" runat="server" Width="80%"></asp:TextBox></div>
                                    <asp:LinkButton CssClass="t_send_btn fr" ID="LB_Second" OnClientClick="setSecValue(this);" runat="server" CommandName="Publish" CommandArgument='<%# Eval("ID") %>'>回复</asp:LinkButton>
                                    <%--<asp:HiddenField ID="Hid_SecondVal" runat="server" />--%>
                                </div>
                                <div class="clear"></div>

                                <asp:ListView ID="LV_SecondReply" runat="server">

                                    <LayoutTemplate>
                                        <div id="itemPlaceholder" runat="server"></div>
                                    </LayoutTemplate>
                                    <ItemTemplate>
                                        <div class="third">
                                            <div class="tcon_left fl">
                                                <img src='<%#Eval("PictUrl") %>' alt='' width='40' height='40' /><span class="name"><%#Eval("ReplyUser") %></span>
                                            </div>
                                            <div class="tcon_right fr">
                                                <p class="third_con"><%#Eval("Content") %></p>
                                                <p class="third_c">
                                                    <span class="times fl"><%#Eval("Created") %></span>
                                                </p>
                                            </div>
                                        </div>
                                    </ItemTemplate>
                                </asp:ListView>
                            </div>
                        </div>

                    </div>
                </div>
            </div>

        </ItemTemplate>
    </asp:ListView>
    <div class="paging">
            <asp:DataPager ID="DPProject" runat="server" PageSize="15" PagedControlID="LV_FirstReply">
                <Fields>
                    <asp:NextPreviousPagerField
                        ButtonType="Link" ShowNextPageButton="False" ShowPreviousPageButton="true"
                        ShowFirstPageButton="true" FirstPageText="首页" PreviousPageText="上一页" />

                    <asp:NumericPagerField CurrentPageLabelCssClass="number now" NumericButtonCssClass="number" />

                    <asp:NextPreviousPagerField
                        ButtonType="Link" ShowPreviousPageButton="False" ShowNextPageButton="true"
                        ShowLastPageButton="true" LastPageText="末页" NextPageText="下一页" />

                    <asp:TemplatePagerField>
                        <PagerTemplate>
                            <span class="page">| <%# Container.StartRowIndex / Container.PageSize + 1%> / 
                            <%# (Container.TotalRowCount % Container.MaximumRows) > 0 ? Convert.ToInt16(Container.TotalRowCount / Container.MaximumRows) + 1 : Container.TotalRowCount / Container.MaximumRows%>  页
                            (共<%# Container.TotalRowCount %>项)
                            </span>
                        </PagerTemplate>
                    </asp:TemplatePagerField>
                </Fields>
            </asp:DataPager>
        </div>









    <div class="clear"></div>

</div>
</div>
<script src="/_layouts/15/Script/jquery-1.8.0.js"></script>

<script src="/_layouts/15/Script/tz_slider.js"></script>
<script src="/_layouts/15/Script/tz_util.js"></script>
<script>
    $(function () {
        $(".Reply").click(function () {
            $(this).parent().parent().parent().next().next().slideToggle();
        });
    })
</script>
