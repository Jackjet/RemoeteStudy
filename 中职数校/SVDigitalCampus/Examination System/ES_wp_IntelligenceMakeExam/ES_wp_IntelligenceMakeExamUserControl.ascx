<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ES_wp_IntelligenceMakeExamUserControl.ascx.cs" Inherits="SVDigitalCampus.Examination_System.ES_wp_IntelligenceMakeExam.ES_wp_IntelligenceMakeExamUserControl" %>
<link type="text/css" rel="stylesheet" href="../../../../_layouts/15/SVDigitalCampus/CSS/common.css" />
<link type="text/css" rel="stylesheet" href="../../../../_layouts/15/SVDigitalCampus/CSS/style.css" />
<link type="text/css" rel="stylesheet" href="../../../../_layouts/15/SVDigitalCampus/CSS/iconfont.css" />
<link type="text/css" rel="stylesheet" href="../../../../_layouts/15/SVDigitalCampus/CSS/animate.css" />
<script src="../../../../_layouts/15/SVDigitalCampus/Script/jquery-1.8.2.min.js"></script>
<script type="text/javascript" src="../../../../_layouts/15/SVDigitalCampus/Script/tz_slider.js"></script>
<script src="../../../../_layouts/15/SVDigitalCampus/Script/popwin.js"></script>
<script type="text/javascript">
    function showimenu(obj) {
        $(".i-menu").next().hide();
        var dis = $(obj).next().css("display");
        //alert(dis);
        if (dis == "block") {
            $(obj).next().hide();
        }
        else {
            $(obj).next().show();
        }

    }
    function showicomenu(obj) {
        $(".ici-menu").next().hide();
        var dis = $(obj).next().css("display");
        //alert(dis);
        if (dis == "none") {
            $(obj).next().show();
        }
        else {
            $(obj).next().hide();
        }
    }
</script>
<div class="IntelligenceMakeQ">
    <!--页面名称-->
    <h1 class="Page_name">挑选试题</h1>
    <!--整个展示区域-->
    <div class="Whole_display_area">
       <%-- <div class="Operation_area">
            <div class="left_choice fl">
                <ul>
                    <li class="">
                        <div class="qiehuan fl">
                            <span class="qijin">
                                <asp:LinkButton ID="btnMessage" OnClick="btnMessage_Click" runat="server" CssClass="Disable">试卷信息</asp:LinkButton><asp:LinkButton ID="btnAdd" runat="server" CssClass="Enable">新建试卷</asp:LinkButton></span>
                        </div>
                    </li>
                </ul>
            </div>
            <div class="clear"></div>
        </div>--%>
        <div class="S_conditions">
            <div id="selectList" class="screenBox screenBackground">
                <dl class="listIndex dlHeight" attr="terminal_brand_s">
                    <dt>专业</dt>
                    <dd>
                        <asp:ListView ID="lvMajor" runat="server" ItemPlaceholderID="itemPlaceholder" OnItemCommand="lvMajor_ItemCommand">
                            <LayoutTemplate>
                                <asp:PlaceHolder ID="itemPlaceholder" runat="server"></asp:PlaceHolder>
                            </LayoutTemplate>
                            <ItemTemplate>
                                <label>
                                    <asp:LinkButton ID="majoritem" runat="server" CommandArgument='<%#Eval("ID") %>' CommandName="showSubject" CssClass='<%#Eval("class") %>'><%#Eval("Title") %></asp:LinkButton>
                                </label>
                            </ItemTemplate>
                        </asp:ListView>
                        <span class="more">展开</span>
                    </dd>
                </dl>
                <dl class="listIndex" attr="terminal_brand_s" id="subjectdl" runat="server">
                    <dt>学科</dt>
                    <dd>
                        <asp:ListView ID="lvSubject" runat="server" ItemPlaceholderID="subitemPlaceHolder" OnItemCommand="lvSubject_ItemCommand">
                            <LayoutTemplate>
                                <asp:PlaceHolder ID="subitemPlaceHolder" runat="server"></asp:PlaceHolder>
                            </LayoutTemplate>
                            <ItemTemplate>
                                <label>
                                    <asp:LinkButton ID="SubjectItem" runat="server" CommandArgument='<%#Eval("ID") %>' CommandName="SubjectSearch" CssClass='<%#Eval("class") %>'><%#Eval("Title") %></asp:LinkButton></label>
                            </ItemTemplate>
                        </asp:ListView>
                    </dd>
                </dl>
            </div>
        </div>
        <div class="clear"></div>
        <!--操作区域-->
        <div class="Operation_area markover">
            <div class="left_choice fl">
                <ul>
                    <li class="">
                        <div class="qiehuan fl">
                            <span class="qijin">
                                <asp:LinkButton ID="btnManual" runat="server" OnClick="btnManual_Click" CssClass="Disable">手动组卷</asp:LinkButton><asp:LinkButton ID="btnNoopsyche" runat="server" CssClass="Enable">智能组卷</asp:LinkButton></span>
                        </div>
                    </li>
                </ul>
            </div>
        </div>
        <div class="clear"></div>
        <div class="Schoolcon_wrap">
            <div class="left_navcon fl">
                <h1>课程</h1>
                <h3 class="tit">
                    <asp:LinkButton ID="lbChapterAll" OnClick="lbChapterAll_Click" runat="server"><i class="icon"></i>全部<span id="CharptCount" runat="server" class="fr tongji">(0)</span></asp:LinkButton></h3>
                <div class="select-box">
                    <asp:ListView ID="lvChapter" runat="server" OnItemCommand="lvChapter_ItemCommand">
                        <ItemTemplate>
                            <div class="item">
                                <div class="i-menu cf" id='<%#"imenu"+Eval("ID") %>'>
                                    <asp:LinkButton ID="lbChapter" runat="server" CommandArgument='<%#Eval("ID") %>' CommandName="showPart">
                                        <span class="fl tubiao"><i class="iconfont">&#xe606;</i></span>
                                        <span class="tit fl"><%#Eval("Title") %>
                                            <span class="fr tongji">(<%#Eval("Count") %>)</span>
                                        </span>
                                    </asp:LinkButton>
                                </div>
                                <div class="i-con">
                                    <asp:ListView ID="lvPart" runat="server" OnItemCommand="lvPart_ItemCommand">
                                        <ItemTemplate>
                                            <div class="ic-item">
                                                <div class="ici-menu cf" id='<%#"part"+Eval("ID") %>'>
                                                    <asp:LinkButton ID="lvPart" runat="server" CommandArgument='<%#Eval("ID") %>' CommandName="showKlPoint">
                                                         <span class="fl icon"></span>
                                                    <span class="tit fl"><%#Eval("Title") %>
                                                        <span class="fr tongji">(<%#Eval("Count") %>)</span>
                                                    </span>
                                                    </asp:LinkButton>
                                                </div>
                                                <div class="ici-con cf" style='display: <%#Eval("IsShow")%>;'>
                                                    <asp:ListView ID="lvKlpoint" runat="server" OnItemCommand="lvKlpoint_ItemCommand">
                                                        <ItemTemplate>
                                                            <div class="icic-item">
                                                                <span class="icon fl"></span>
                                                                <span class="fl">
                                                                    <asp:LinkButton ID="lbKlpoint" runat="server" CommandName="Getklpoint" CommandArgument='<%#Eval("ID") %>'><%#Eval("Title") %></asp:LinkButton></span>
                                                            </div>
                                                            <div class="clear"></div>
                                                        </ItemTemplate>
                                                    </asp:ListView>
                                                    <div class="clear"></div>
                                                </div>
                                            </div>
                                        </ItemTemplate>
                                    </asp:ListView>
                                    <div class="clear"></div>
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:ListView>
                </div>
            </div>
            <div class="right_dcon fr">
                <div class="clear"></div>
                <div class="formlist">
                    <h2 class="form_tit"><span class="tit">题型数量设置</span></h2>
                    <div class="conlist">
                        <h1 class="tip">
                            <span class="Select fl">
                                <span class="wenzi">题目难易程度</span>
                                <asp:DropDownList ID="Difficulty" class="option" runat="server" AutoPostBack="True"  OnSelectedIndexChanged="Difficulty_SelectedIndexChanged" >
                                    <asp:ListItem Value="0">所有</asp:ListItem>
                                    <asp:ListItem Value="1">简单</asp:ListItem>
                                    <asp:ListItem Value="2">一般</asp:ListItem>
                                    <asp:ListItem Value="3">困难</asp:ListItem>
                                </asp:DropDownList>
                            </span>
                            <span class="tips fr">题型总数量：已经设置<em class="num_t"><span runat="server" id="Number">0</span></em>道题</span>
                        </h1>
                        <div class="clear"></div>
                        <div class="concon">
                            <ul class="listcon">
                                <asp:ListView ID="lvQType" runat="server" GroupItemCount="2" ItemPlaceholderID="qtypegroupItem">
                                    <GroupTemplate>
                                        <asp:PlaceHolder ID="qtypegroupItem" runat="server"></asp:PlaceHolder>
                                    </GroupTemplate>
                                    <ItemTemplate>
                                        <li><%#Eval("Title") %>：   <span class="d_jiajian">
                          <asp:LinkButton ID="lbjian" class="d_jian" runat="server" OnClick='<%# "ReduceNum(this,"+ Eval("ID")+","+Eval("QType")+");return false;"%>'>-</asp:LinkButton>
                                                        <em>
                                                            <input id="txtNumber" type="text" onkeyup="this.value=this.value.replace(/\D/g,'1')" onafterpaste="this.value=this.value.replace(/\D/g,'1')" onblur='<%# "CheckNumber(this,"+ Eval("ID")+","+Eval("QType")+");" %>' value='<%#Eval("Number") %>' runat="server" class="num" />
                                                            <input type="hidden" id="oldnum" value='<%#Eval("Number") %>'/> <input type="hidden" id="maxnum" value='<%#Eval("TypeQCount") %>'/>
                                                        </em>
                                                        <asp:LinkButton ID="lbjia" class="d_jia" runat="server" OnClick='<%# "AddNum(this,"+ Eval("ID")+","+Eval("QType")+");return false;"%>'>+</asp:LinkButton>
                                 </span>题(共<%#Eval("TypeQCount") %>题)     
                                        </li>
                                    </ItemTemplate>
                                </asp:ListView>
                            </ul>
                        </div>
                    </div>
                </div>
                <%-- <div class="ExamQ">
                    <h2>难度系数设置</h2>
                    <span>调节总难度：<span id="difficulty" runat="server" class="red">中等</span></span>
                    <span>简单</span>
                    <div class="bfbDifficulty">
                        <img class="barimg" src="../../../../_layouts/15/SVDigitalCampus/Image/1.png">
                    </div>
                    <span>困难</span>
                    <span class="gray">合理区间（0.25-0.75）</span>
                </div>--%>
                <div class="end">
                <asp:Button ID="MakeExam" runat="server" CssClass="add fr" UseSubmitBehavior="false" OnClientClick="this.value='开始组卷';this.disabled=true;"  OnClick="MakeExam_Click" Text="开始组卷" />
                </div>
            </div>
        </div>
    </div>
</div>
<script type="text/javascript">
    //$(function () {
    //    $(".bfbDifficulty img").animate({ width: "50px" });
    //    var bfb = 50 * 100 / 140;
    //    $("span[id$='difficulty']").html("中等(" + bfb / 100 + ")");
    //    $(".bfbDifficulty").click(function (e) {
    //        x = $(this).offset();
    //        var ep = e.pageX - x.left;
    //        $(".bfbDifficulty img").animate({ width: ep + "px" });
    //        bfb = ep * 100 / 140;
    //        if (parseFloat(bfb) > 0 && parseFloat(bfb) <= 33) {
    //            $("span[id$='difficulty']").html("简单(" + bfb / 100 + ")");
    //        }
    //        else if (parseFloat(bfb) > 33 && parseFloat(bfb) <= 66)
    //        { $("span[id$='difficulty']").html("中等(" + bfb / 100 + ")"); }
    //        else if (parseFloat(bfb) > 66 && parseFloat(bfb) < 100)
    //        { $("span[id$='difficulty']").html("较难(" + bfb / 100 + ")"); }
    //    })
    //});
    function CheckNumber(obj, id, type) {
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
            alert("请输入数量!");
            $(obj).val($(obj).next().val());
            return;
        }
        if ($(obj).val() == $(obj).next().val()) {
            return;
        }
        if (parseInt( $(obj).val()) > parseInt($(obj).next().next().val())) {
            alert("请输入不超过总题数的数字!");
            $(obj).val($(obj).next().val());
            return;
        }
        var diff = $('#<%=Difficulty.ClientID%>').val();
        var num = $(obj).val();
        var oldnum = $(obj).next().val();
        if (id != null && id != "" && type != null && type != "" && num != null && num != "") {
            jQuery.ajax({
                url: '<%=layoutstr%>' + "/SVDigitalCampus/ExamSystemHander/ExamSystemDataHander.aspx?action=ChangeNum&" + Math.random(),   // 提交的页面
                type: "POST",                   // 设置请求类型为"POST"，默认为"GET"
                data: { "id": id, "type": type, "oldnum": oldnum, "num": num, "diff": diff },
                beforeSend: function ()          // 设置表单提交前方法
                {
                    //alert("准备提交数据");


                },
                error: function (request) {      // 设置表单提交出错
                    //alert("表单提交出错，请稍候再试");
                    //rebool = false;
                },
                success: function (result) {
                    var ss = result.split("|");
                    if (ss[0] == "1") {
                        //修改显示（数量/金额）
                        if (parseInt($('#<%=Number.ClientID%>').html()) + parseInt(ss[1]) >= 0) {
                            $('#<%=Number.ClientID%>').html(parseInt($('#<%=Number.ClientID%>').html()) + parseInt(ss[1]));
                        }
                        $(obj).val(parseInt($(obj).next().val()) + parseInt(ss[1]));
                        $(obj).next().val(parseInt($(obj).next().val()) + parseInt(ss[1]));
                    } else {
                        $(obj).val($(obj).next().val());
                    }
                }

            });
        }
    }
    function AddNum(obj, Id, typeid) {
                    var prevobj = $(obj).prev().find("[id$='txtNumber']");
                    var diff = $('#<%=Difficulty.ClientID%>').val();
        jQuery.ajax({
            url: '<%=layoutstr%>' + "/SVDigitalCampus/ExamSystemHander/ExamSystemDataHander.aspx?action=AddNum&" + Math.random(),   // 提交的页面
            type: "POST",                   // 设置请求类型为"POST"，默认为"GET"
            data: { "id": Id, "type": typeid, "diff": diff },
            beforeSend: function ()          // 设置表单提交前方法
            {
                //alert("准备提交数据");


            },
            error: function (request) {      // 设置表单提交出错
                //alert("表单提交出错，请稍候再试");
                //rebool = false;
            },
            success: function (result) {
                var ss = result.split("|");
                if (ss[0] == "1") {
                    //修改显示（数量/金额）
                    var nownum = parseInt($(prevobj).val()) + parseInt(ss[1]);
                    $(prevobj).val(nownum);

                    $('#<%=Number.ClientID%>').html(parseInt($('#<%=Number.ClientID%>').html()) + parseInt(ss[1]));
                    $(prevobj).next().val(nownum);

                } else { $(prevobj).val($(prevobj).next().val()); }
            }

        });
    }
    function ReduceNum(obj, Id, typeid) {
        var nextvobj = $(obj).next().find("[id$='txtNumber']");
        var newcount = parseInt($(nextvobj).val()) + parseInt(-1);
        var diff = $('#<%=Difficulty.ClientID%>').val();
        if (parseInt(newcount) >= 0) {
            jQuery.ajax({
                url: '<%=layoutstr%>' + "/SVDigitalCampus/ExamSystemHander/ExamSystemDataHander.aspx?action=ReduceNum&" + Math.random(),   // 提交的页面
                type: "POST",                   // 设置请求类型为"POST"，默认为"GET"
                data: { "id": Id, "type": typeid, "diff": diff },
                beforeSend: function ()          // 设置表单提交前方法
                {
                    //alert("准备提交数据");


                },
                error: function (request) {      // 设置表单提交出错
                    //alert("表单提交出错，请稍候再试");
                    //rebool = false;
                },
                success: function (result) {
                    var ss = result.split("|");
                    if (ss[0] == "1") {
                        //修改显示（数量）
                        $(nextvobj).val(parseInt($(nextvobj).val()) + parseInt(-1));

                        $('#<%=Number.ClientID%>').html(parseInt($('#<%=Number.ClientID%>').html()) + parseInt(-1));
                        $(nextvobj).next().val($(nextvobj).val());
                    } else { $(nextvobj).val($(nextvobj).next().val()); }
                }

            });
        }
    }
</script>
