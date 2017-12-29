<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TG_wp_TeacherGrowUserControl.ascx.cs" Inherits="Sinp_TeacherWP.WebParts.TG.TG_wp_TeacherGrow.TG_wp_TeacherGrowUserControl" %>
<link href="/_layouts/15/Style/iconfont.css" rel="stylesheet" />
<link href="/_layouts/15/Style/layout.css" rel="stylesheet" />
<link href="/_layouts/15/Style/common.css" rel="stylesheet" />
<script src="/_layouts/15/Script/jquery-1.8.0.js"></script>
<script src="/_layouts/15/Script/uploadFile.js"></script>
<script src="/_layouts/15/Script/uploadfile1.js"></script>
<script src="/_layouts/15/Script/My97DatePicker/WdatePicker.js"></script>
<script type="text/javascript">
    function RemovePlan(fileName, trId) {
        $("#" + trId).hide();
        var nowfile = $("input[id$=Hid_fileName]").val()
        $("input[id$=Hid_fileName]").val(nowfile + '#' + fileName);
    }
    function RemoveThesis(fileName, trId) {
        $("#" + trId).hide();
        var nowfile = $("input[id$=Hid_fileName1]").val()
        $("input[id$=Hid_fileName1]").val(nowfile + '#' + fileName);
    }
    function RemoveGuidance(fileName, trId) {
        $("#" + trId).hide();
        var nowfile = $("input[id$=Hid_fileName2]").val()
        $("input[id$=Hid_fileName2]").val(nowfile + '#' + fileName);
    }
    function RemovePrize(fileName, trId) {
        $("#" + trId).hide();
        var nowfile = $("input[id$=Hid_fileName3]").val()
        $("input[id$=Hid_fileName3]").val(nowfile + '#' + fileName);
    }
</script>
<asp:HiddenField ID="HF_TabFlag" Value="first" runat="server" />
<div class="yy_tab">
    <div class="yy_tabheader">
        <ul class="tab_tit">
            <li class="selected"><a href="#Personal_information" class="first">个人信息</a></li>
            <li><a href="#Term_plan" class="second">学期计划</a></li>
            <li><a href="#Training_information" class="three">培训信息</a></li>
            <li><a href="#Winning_information" class="four">获奖信息</a></li>
            <li><a href="#Open_class" class="five">公开课</a></li>
            <li><a href="#Guiding_performance" class="six">指导业绩</a></li>
            <li><a href="#Monograph" class="seven">论文专著</a></li>
        </ul>
    </div>
    <div class="content">
        <div class="tc" style="display: none;" id="first">
            <div class="Personal_info">
                
                <asp:Image ID="Img_TeacherInfo" ImageUrl="/_layouts/15/TeacherImages/teacher.jpg" runat="server" /><a onclick="showload()" id="asitechage" href="#">更换网站头像</a><a onclick="showupload()" id="achage" href="#">更换照片</a>
            </div>
            <div id="dvupload">
                <asp:FileUpload ID="zpUpload" runat="server" />
                <asp:Button ID="Btn_ChangePic" runat="server" Text="上传照片" CssClass="btn_save" OnClick="Btn_ChangePic_Click" />
                <input type="button" value="取消" id="uploadCancel" onclick="hideupload()" />
            </div>
            <div id="dvload">
                <asp:FileUpload ID="zpload" runat="server" />
                <asp:Button ID="Btn_ChangeLittlePic" runat="server" Text="上传网站头像" CssClass="btn_save" OnClick="Btn_ChangeLittlePic_Click"  />
                <input type="button" value="取消" id="loadCancel" onclick="hideload()" />
            </div>
            <asp:Panel ID="Pan_AddInfo" runat="server">
                <div class="formdv">
                    <table class="tabContent" style="display: block;">
                        <tr>
                            <th>个人格言：</th>
                            <td>
                                <asp:TextBox ID="TB_Maxim" runat="server"></asp:TextBox>
                                <span style="color: red;">*
                            <asp:RequiredFieldValidator ID="RFV_Maxim" runat="server" ControlToValidate="TB_Maxim"
                                ErrorMessage="必填" ValidationGroup="InfoSubmit" SetFocusOnError="True" Display="Dynamic"></asp:RequiredFieldValidator></span>
                            </td>
                        </tr>


                        <tr class="areatr">
                            <th>自我评价：</th>
                            <td>
                                <asp:TextBox ID="TB_Evaluate" TextMode="MultiLine" Height="82px" runat="server"></asp:TextBox>
                                <span style="color: red;">*
                            <asp:RequiredFieldValidator ID="RFV_Evaluate" runat="server" ControlToValidate="TB_Evaluate"
                                ErrorMessage="必填" ValidationGroup="InfoSubmit" SetFocusOnError="True" Display="Dynamic"></asp:RequiredFieldValidator></span>
                            </td>
                        </tr>
                        <tr class="areatr">
                            <th>个人感悟：</th>
                            <td>
                                <asp:TextBox ID="TB_Gnosis" TextMode="MultiLine" Height="82px" runat="server"></asp:TextBox>
                                <span style="color: red;">*
                            <asp:RequiredFieldValidator ID="RFV_Gnosis" runat="server" ControlToValidate="TB_Gnosis"
                                ErrorMessage="必填" ValidationGroup="InfoSubmit" SetFocusOnError="True" Display="Dynamic"></asp:RequiredFieldValidator></span>
                            </td>
                        </tr>
                        <tr class="areatr">
                            <th>自我反思：</th>
                            <td>
                                <asp:TextBox ID="TB_ReThink" TextMode="MultiLine" Height="82px" runat="server"></asp:TextBox>
                                <span style="color: red;">*
                            <asp:RequiredFieldValidator ID="RFV_ReThink" runat="server" ControlToValidate="TB_ReThink"
                                ErrorMessage="必填" ValidationGroup="InfoSubmit" SetFocusOnError="True" Display="Dynamic"></asp:RequiredFieldValidator></span>
                            </td>
                        </tr>

                        <tr class="btntr">
                            <th></th>
                            <td>
                                <asp:Button ID="Btn_InfoSave" OnClick="Btn_InfoSave_Click" CssClass="save" runat="server" ValidationGroup="InfoSubmit" Text="保存" />
                                <asp:Button ID="Btn_InfoCancel" OnClick="Btn_InfoCancel_Click" CssClass="cancel" runat="server" Text="取消" />

                            </td>
                        </tr>
                    </table>
                </div>
            </asp:Panel>

            <asp:Panel ID="Pan_ShowInfo" runat="server">
                <div class="formdv">
                    <table class="tabContent" style="display: block;">
                        <tr>
                            <td>个人格言：</td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Literal ID="Lit_Maxim" runat="server"></asp:Literal>
                            </td>
                        </tr>
                        <tr>
                            <td>自我评价：</td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Literal ID="Lit_Evaluate" runat="server"></asp:Literal>
                            </td>
                        </tr>
                        <tr>
                            <td>个人感悟：</td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Literal ID="Lit_Gnosis" runat="server"></asp:Literal>
                            </td>
                        </tr>
                        <tr>
                            <td>自我反思：</td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Literal ID="Lit_ReThink" runat="server"></asp:Literal>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: center;">
                                <asp:HiddenField ID="Hid_InfoId" runat="server" />
                                <asp:Button ID="Btn_EditInfo" OnClick="Btn_EditInfo_Click" CssClass="save" runat="server" Text="编辑个人信息" />
                            </td>
                        </tr>
                    </table>
                </div>
            </asp:Panel>
        </div>
        <!--学期计划-->
        <div class="tc" id="second" style="display: none;">
            <asp:Panel ID="Pan_AddTerm" runat="server">
                <div class="dvaddTerm">
                    <input type="button" value="添加学期计划" onclick="showterm()" />
                </div>
                <div class="formdv">
                    <table class="tabContent" id="addterm" style="display: none;">
                        <tr>
                            <th>计划名称：</th>
                            <td>
                                <asp:TextBox ID="TB_PlanTitle" runat="server"></asp:TextBox>
                                <span style="color: red;">*
                            <asp:RequiredFieldValidator ID="RFV_PlanTitle" runat="server" ControlToValidate="TB_PlanTitle"
                                ErrorMessage="必填" ValidationGroup="PlanSubmit" SetFocusOnError="True" Display="Dynamic"></asp:RequiredFieldValidator></span>
                            </td>
                        </tr>
                        <tr>
                            <th>计划类型：</th>
                            <td>

                                <asp:DropDownList ID="DDL_PlanType" runat="server"></asp:DropDownList>

                                <span style="color: red;">*
                            <asp:RequiredFieldValidator ID="RFV_PlanType" runat="server" ControlToValidate="DDL_PlanType"
                                ErrorMessage="必填" ValidationGroup="PlanSubmit" SetFocusOnError="True" Display="Dynamic"></asp:RequiredFieldValidator></span>

                            </td>
                        </tr>
                        <tr class="areatr">
                            <th>计划内容：</th>
                            <td>
                                <asp:TextBox ID="TB_PlanContent" TextMode="MultiLine" Height="82px" runat="server"></asp:TextBox>
                                <span style="color: red;">*
                            <asp:RequiredFieldValidator ID="RFV_PlanContent" runat="server" ControlToValidate="TB_PlanContent"
                                ErrorMessage="必填" ValidationGroup="PlanSubmit" SetFocusOnError="True" Display="Dynamic"></asp:RequiredFieldValidator></span>
                            </td>
                        </tr>
                        <tr>
                            <th>附件：</th>
                            <td>
                                <asp:Label ID="lbAttach" runat="server">
                                </asp:Label>
                                <asp:HiddenField ID="Hid_fileName" runat="server" />
                                <table id="idAttachmentsTable0" style="display: block;">
                                    <asp:Literal ID="Lit_Bind" runat="server"></asp:Literal>
                                </table>
                                <table style="width: 100%; display: block;">
                                    <tr>
                                        <td id="att0">&nbsp;
                                            <input id="fileupload0" name="fileupload0" style="width: 80%;" type="file" />
                                            <%--<asp:FileUpload ID="FU_TermFile" runat="server" />--%>
                                        </td>
                                        <td>
                                            <input type="button" onclick="UploadFile('fileupload', 'idAttachmentsTable0', 'att0')" style="width: 70px;" value="上传" /></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr class="btntr">
                            <th></th>
                            <td>
                                <asp:Button ID="Btn_PlanSave" OnClick="Btn_PlanSave_Click" CssClass="save" runat="server" ValidationGroup="PlanSubmit" Text="保存" />
                                <asp:Button ID="Btn_PlanCancel" OnClick="Btn_PlanCancel_Click" CssClass="cancel" runat="server" Text="取消" />

                            </td>
                        </tr>
                    </table>
                </div>
            </asp:Panel>
            <asp:Panel ID="Pan_ShowTerm" runat="server">
                <div class="showterm">
                    <div class="plantitle">
                        <span class="title">
                         <asp:Literal ID="Lit_Title" runat="server"></asp:Literal>
                        </span>                         
                        <span class="zt fr"><asp:Image ID="Img_Status" ImageUrl="/_layouts/15/TeacherImages/wait.png" runat="server" CssClass="ZT_img" /></span>
                    </div>
                    <table>
                        <tr>
                            <td></td>
                            <td><span>
                                <asp:Literal ID="Lit_LearnYear" runat="server"></asp:Literal></span><span><asp:Literal ID="Lit_PlanType" runat="server"></asp:Literal></span></td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                <p>
                                    <asp:Literal ID="Lit_PlanContent" runat="server"></asp:Literal>
                                </p>
                            </td>
                        </tr>
                        <tr>
                            <td class="fujian">附件：
                                <table style="display: block;">
                                    <asp:Literal ID="Lit_Attachment" runat="server"></asp:Literal>
                                </table>
                            </td>
                            <td>
                                <asp:HiddenField ID="Hid_Id" runat="server" />
                            </td>
                            <td class="fr a_editor"><a href="#"><i class="iconfont">&#xe60a;</i><asp:LinkButton CommandName="Del" ID="LB_PlanEdit" runat="server" OnClick="LB_PlanEdit_Click">编辑</asp:LinkButton></a></td>
                        </tr>
                        <tr>
                            <asp:Literal ID="Lit_AuditOpinion" runat="server"></asp:Literal>
                        </tr>
                    </table>

                </div>
            </asp:Panel>
            <asp:Panel ID="Pan_WorkLog" runat="server">
                <div class="formdv">

                    <div class="topadd"><a href="#"><i class="iconfont">&#xe623;</i>添加工作日志</a></div>

                    <table class="tabContent">
                        <tr>
                            <th>日志名称：</th>
                            <td>
                                <asp:TextBox ID="TB_LogTitle" runat="server"></asp:TextBox>
                                <span style="color: red;">*
                            <asp:RequiredFieldValidator ID="RFV_LogTitle" runat="server" ControlToValidate="TB_LogTitle"
                                ErrorMessage="必填" ValidationGroup="LogSubmit" SetFocusOnError="True" Display="Dynamic"></asp:RequiredFieldValidator></span>
                            </td>
                        </tr>
                        <tr>
                            <th>日志类型：</th>
                            <td>

                                <asp:DropDownList ID="DDL_LogType" runat="server"></asp:DropDownList>

                                <span style="color: red;">*
                            <asp:RequiredFieldValidator ID="RFV_LogType" runat="server" ControlToValidate="DDL_LogType"
                                ErrorMessage="必填" ValidationGroup="LogSubmit" SetFocusOnError="True" Display="Dynamic"></asp:RequiredFieldValidator></span>

                            </td>
                        </tr>

                        <tr class="areatr">
                            <th>日志内容：</th>
                            <td>
                                <asp:TextBox ID="TB_LogContent" TextMode="MultiLine" Height="82px" runat="server"></asp:TextBox>
                                <span style="color: red;">*
                            <asp:RequiredFieldValidator ID="RFV_LogContent" runat="server" ControlToValidate="TB_LogContent"
                                ErrorMessage="必填" ValidationGroup="LogSubmit" SetFocusOnError="True" Display="Dynamic"></asp:RequiredFieldValidator></span>
                            </td>
                        </tr>

                        <tr class="btntr">
                            <th></th>
                            <td>
                                <asp:Button ID="Btn_LogSave" OnClick="Btn_LogSave_Click" CssClass="save" runat="server" ValidationGroup="LogSubmit" Text="保存" />
                                <asp:Button ID="Btn_LogCancel" OnClick="Btn_LogCancel_Click" CssClass="cancel" runat="server" Text="取消" />

                            </td>
                        </tr>
                    </table>
                </div>

                <div class="content">
                    <div class="container">
                        <ul class="list">
                            <asp:ListView ID="LV_Log" runat="server" OnPagePropertiesChanging="LV_Log_PagePropertiesChanging" OnItemCommand="LV_Log_ItemCommand" OnItemEditing="LV_Log_ItemEditing">
                                <EmptyDataTemplate>
                                    <table class="emptydate">
                                        <tr>
                                            <td>
                                                <img src="/_layouts/15/TeacherImages/expression.png" /><p>没有找到相关内容</p>
                                            </td>
                                        </tr>
                                    </table>
                                </EmptyDataTemplate>
                                <LayoutTemplate>
                                    <li id="itemPlaceholder" runat="server"></li>
                                </LayoutTemplate>
                                <ItemTemplate>
                                    <li class="li_list">
                                        <div class="top_remarks">
                                            <div class="left_con fl"><span class="times"><%# Eval("LogType") %><span class="con_details"></span></div>
                                            <div class="right_edit fr"><i class="iconfont">&#xe60a;</i><asp:LinkButton OnClientClick="shouwtab();" CommandName="Edit" CommandArgument='<%# Eval("ID") %>' ID="LB_Edit" runat="server">编辑</asp:LinkButton>|<i class="iconfont">&#xe62f;</i><asp:LinkButton CommandName="Del" CommandArgument='<%# Eval("ID") %>' ID="LB_Del" runat="server" OnClientClick="return confirm('你确定要删除吗？')">删除</asp:LinkButton></div>
                                        </div>
                                        <div class="con_text">
                                            <h2><%# Eval("Title") %><%--<i class="iconfont">&#xe60f;</i>--%></h2>
                                            <div class="con_con">
                                                <p>
                                                    <%# Eval("LogContent") %>
                                                </p>
                                                <%--<div class="attachment"><a href="">附件：<%# Eval("Attachment") %></a></div>--%>
                                            </div>
                                        </div>
                                        <div class="boxmore"><a href="#"><span class="J-more">更多</span></a> </div>
                                    </li>
                                </ItemTemplate>
                            </asp:ListView>
                        </ul>
                        <div class="paging">
                            <asp:DataPager ID="DP_Log" runat="server" PageSize="5" PagedControlID="LV_Log">
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
                            <%# (Container.TotalRowCount % Container.MaximumRows) > 0 ? Convert.ToInt32(Container.TotalRowCount / Container.MaximumRows) + 1 : Container.TotalRowCount / Container.MaximumRows%>  页
                            (共<%# Container.TotalRowCount %>项)
                                            </span>
                                        </PagerTemplate>
                                    </asp:TemplatePagerField>
                                </Fields>
                            </asp:DataPager>
                        </div>

                    </div>
                </div>
            </asp:Panel>

        </div>
        <!--培训信息-->
        <div class="tc" style="display: none;" id="three">

            <div class="formdv">

                <div class="topadd"><a href="#"><i class="iconfont">&#xe623;</i>添加培训信息</a></div>

                <table class="tabContent">
                    <tr>
                        <th>培训主题：</th>
                        <td>
                            <asp:TextBox ID="TB_Title" runat="server"></asp:TextBox>
                            <span style="color: red;">*
                            <asp:RequiredFieldValidator ID="RFV_Title" runat="server" ControlToValidate="TB_Title"
                                ErrorMessage="必填" ValidationGroup="submit" SetFocusOnError="True" Display="Dynamic"></asp:RequiredFieldValidator></span>
                        </td>
                    </tr>
                    <tr>
                        <th>培训地点：</th>
                        <td>
                            <asp:TextBox ID="TB_TrainPlace" runat="server"></asp:TextBox>
                            <span style="color: red;">*
                            <asp:RequiredFieldValidator ID="RFV_TrainPlace" runat="server" ControlToValidate="TB_TrainPlace"
                                ErrorMessage="必填" ValidationGroup="submit" SetFocusOnError="True" Display="Dynamic"></asp:RequiredFieldValidator></span>
                        </td>
                    </tr>
                    <tr>
                        <th>开始时间：</th>
                        <td>
                            <asp:TextBox ID="TB_StartTime" CssClass="Wdate" onclick="javascript: WdatePicker()" runat="server"></asp:TextBox>
                            <span style="color: red;">*<asp:RequiredFieldValidator ID="RFV_StartTime" runat="server" ControlToValidate="TB_StartTime"
                                ErrorMessage="必填" ValidationGroup="submit" SetFocusOnError="True" Display="Dynamic"></asp:RequiredFieldValidator></span>
                        </td>
                    </tr>
                    <tr>
                        <th>结束时间：</th>
                        <td>
                            <asp:TextBox ID="TB_EndTime" CssClass="Wdate" onclick="javascript: WdatePicker()" runat="server"></asp:TextBox>
                            <span style="color: red;">*<asp:RequiredFieldValidator ID="RFV_EndTime" runat="server" ControlToValidate="TB_EndTime"
                                ErrorMessage="必填" ValidationGroup="submit" SetFocusOnError="True" Display="Dynamic"></asp:RequiredFieldValidator>
                            </span>
                        </td>
                    </tr>
                    <tr class="areatr">
                        <th>培训内容：</th>
                        <td>
                            <asp:TextBox ID="TB_TrainingContent" TextMode="MultiLine" Height="82px" runat="server"></asp:TextBox>
                            <span style="color: red;">*
                            <asp:RequiredFieldValidator ID="RFV_TrainingContent" runat="server" ControlToValidate="TB_TrainingContent"
                                ErrorMessage="必填" ValidationGroup="submit" SetFocusOnError="True" Display="Dynamic"></asp:RequiredFieldValidator></span>
                        </td>
                    </tr>

                    <tr class="btntr">
                        <th></th>
                        <td>
                            <asp:Button ID="Btn_TrainSave" OnClick="Btn_TrainSave_Click" CssClass="save" runat="server" ValidationGroup="submit" Text="保存" />
                            <asp:Button ID="Btn_TrainCancel" CssClass="cancel" runat="server" OnClick="Btn_TrainCancel_Click" Text="取消" />

                        </td>
                    </tr>
                </table>
            </div>

            <div class="content">
                <div class="container">
                    <ul class="list">
                        <asp:ListView ID="LV_Training" runat="server" OnPagePropertiesChanging="LV_Training_PagePropertiesChanging" OnItemCommand="LV_Training_ItemCommand" OnItemEditing="LV_Training_ItemEditing">
                            <EmptyDataTemplate>
                                <table class="emptydate">
                                    <tr>
                                        <td>
                                            <img src="/_layouts/15/TeacherImages/expression.png" /><p>没有找到相关内容</p>
                                        </td>
                                    </tr>
                                </table>
                            </EmptyDataTemplate>
                            <LayoutTemplate>
                                <li id="itemPlaceholder" runat="server"></li>
                            </LayoutTemplate>
                            <ItemTemplate>
                                <li class="li_list">
                                    <div class="top_remarks">
                                        <div class="left_con fl"><span class="times"><%# Eval("StartTime") %>-<%# Eval("EndTime") %></span><span class="con_details"><em><%# Eval("LearnYear") %></em>|<em><%# Eval("TrainPlace") %></em></span></div>
                                        <div class="right_edit fr"><i class="iconfont">&#xe60a;</i><asp:LinkButton OnClientClick="shouwtab();" CommandName="Edit" CommandArgument='<%# Eval("ID") %>' ID="LB_Edit" runat="server">编辑</asp:LinkButton>|<i class="iconfont">&#xe62f;</i><asp:LinkButton CommandName="Del" CommandArgument='<%# Eval("ID") %>' ID="LB_Del" runat="server" OnClientClick="return confirm('你确定要删除吗？')">删除</asp:LinkButton></div>
                                    </div>
                                    <div class="con_text">
                                        <h2><%# Eval("Title") %><%--<i class="iconfont">&#xe60f;</i>--%></h2>
                                        <div class="con_con">
                                            <p>
                                                <%# Eval("TrainingContent") %>
                                            </p>
                                            <%--<div class="attachment"><a href="">附件：<%# Eval("Attachment") %></a></div>--%>
                                        </div>
                                    </div>
                                    <div class="boxmore"><a href="#"><span class="J-more">更多</span></a> </div>
                                </li>
                            </ItemTemplate>
                        </asp:ListView>
                    </ul>
                    <div class="paging">
                        <asp:DataPager ID="DPTeacher" runat="server" PageSize="5" PagedControlID="LV_Training">
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
                            <%# (Container.TotalRowCount % Container.MaximumRows) > 0 ? Convert.ToInt32(Container.TotalRowCount / Container.MaximumRows) + 1 : Container.TotalRowCount / Container.MaximumRows%>  页
                            (共<%# Container.TotalRowCount %>项)
                                        </span>
                                    </PagerTemplate>
                                </asp:TemplatePagerField>
                            </Fields>
                        </asp:DataPager>
                    </div>

                </div>
            </div>
        </div>
        <!--获奖信息-->
        <div class="tc" style="display: none;" id="four">
            <div class="formdv">

                <div class="topadd"><a href="#"><i class="iconfont">&#xe623;</i>添加获奖信息</a></div>

                <table class="tabContent">
                    <tr>
                        <th>获奖名称：</th>
                        <td>
                            <asp:TextBox ID="TB_PrizeTitle" runat="server"></asp:TextBox>
                            <span style="color: red;">*
                            <asp:RequiredFieldValidator ID="RFV_PrizeTitle" runat="server" ControlToValidate="TB_PrizeTitle"
                                ErrorMessage="必填" ValidationGroup="PrizeSubmit" SetFocusOnError="True" Display="Dynamic"></asp:RequiredFieldValidator></span>
                        </td>
                    </tr>
                    <tr>
                        <th>获奖日期：</th>
                        <td>
                            <asp:TextBox ID="TB_PrizeDate" CssClass="Wdate" onclick="javascript: WdatePicker()" runat="server"></asp:TextBox>
                            <span style="color: red;">*<asp:RequiredFieldValidator ID="RFV_PrizeDate" runat="server" ControlToValidate="TB_PrizeDate"
                                ErrorMessage="必填" ValidationGroup="PrizeSubmit" SetFocusOnError="True" Display="Dynamic"></asp:RequiredFieldValidator></span>
                        </td>
                    </tr>
                    <tr>
                        <th>获奖类型：</th>
                        <td>
                            <asp:DropDownList ID="DDL_PrizeType" runat="server"></asp:DropDownList>

                            <span style="color: red;">*
                            <asp:RequiredFieldValidator ID="RFV_PrizeType" runat="server" ControlToValidate="DDL_PrizeType"
                                ErrorMessage="必填" ValidationGroup="PrizeSubmit" SetFocusOnError="True" Display="Dynamic"></asp:RequiredFieldValidator></span>
                        </td>
                    </tr>

                    <tr>
                        <th>获奖级别：</th>
                        <td>
                            <asp:DropDownList ID="DDL_PrizeLevel" runat="server"></asp:DropDownList>

                            <span style="color: red;">*
                            <asp:RequiredFieldValidator ID="RFV_PrizeLevel" runat="server" ControlToValidate="DDL_PrizeLevel"
                                ErrorMessage="必填" ValidationGroup="PrizeSubmit" SetFocusOnError="True" Display="Dynamic"></asp:RequiredFieldValidator></span>
                        </td>
                    </tr>
                    <tr>
                        <th>获奖等级：</th>
                        <td>
                            <asp:DropDownList ID="DDL_PrizeGrade" runat="server"></asp:DropDownList>

                            <span style="color: red;">*
                            <asp:RequiredFieldValidator ID="RFV_PrizeGrade" runat="server" ControlToValidate="DDL_PrizeGrade"
                                ErrorMessage="必填" ValidationGroup="PrizeSubmit" SetFocusOnError="True" Display="Dynamic"></asp:RequiredFieldValidator></span>
                        </td>
                    </tr>
                    <tr>
                        <th>颁奖单位：</th>
                        <td>
                            <asp:TextBox ID="TB_RewardsBureau" runat="server"></asp:TextBox>
                            <span style="color: red;">*
                            <asp:RequiredFieldValidator ID="RFV_RewardsBureau" runat="server" ControlToValidate="TB_RewardsBureau"
                                ErrorMessage="必填" ValidationGroup="PrizeSubmit" SetFocusOnError="True" Display="Dynamic"></asp:RequiredFieldValidator></span>
                        </td>
                    </tr>
                    <tr class="areatr">
                        <th>获奖感言：</th>
                        <td>
                            <asp:TextBox ID="TB_PrizeThankful" TextMode="MultiLine" Height="82px" runat="server"></asp:TextBox>

                        </td>
                    </tr>                    
                    <tr>
                        <th>附件：</th>
                        <td>
                            <asp:Label ID="lbAttach3" runat="server">
                            </asp:Label>
                            <asp:HiddenField ID="Hid_fileName3" runat="server" />
                            <table id="idAttachmentsTable3" style="display: block;">
                                <asp:Literal ID="Lit_Bind3" runat="server"></asp:Literal>
                            </table>
                            <table style="width: 100%; display: block;">
                                <tr>
                                    <td id="att3">&nbsp;
                                            <input id="filePrize0" name="filePrize0" style="width: 80%;" type="file" />
                                        <%--<asp:FileUpload ID="FU_TermFile" runat="server" />--%>
                                    </td>
                                    <td>
                                        <input type="button" onclick="UploadFile('filePrize', 'idAttachmentsTable3', 'att3')" style="width: 70px;" value="上传" /></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr class="btntr">
                        <th></th>
                        <td>
                            <asp:Button ID="Btn_PrizeSave" OnClick="Btn_PrizeSave_Click" CssClass="save" runat="server" ValidationGroup="PrizeSubmit" Text="保存" />
                            <asp:Button ID="Btn_PrizeCancel" CssClass="cancel" runat="server" OnClick="Btn_PrizeCancel_Click" Text="取消" />

                        </td>
                    </tr>
                </table>
            </div>

            <div class="content">
                <div class="container">
                    <ul class="list">
                        <asp:ListView ID="LV_Prize" runat="server" OnPagePropertiesChanging="LV_Prize_PagePropertiesChanging" OnItemCommand="LV_Prize_ItemCommand" OnItemEditing="LV_Prize_ItemEditing">
                            <EmptyDataTemplate>
                                <table class="emptydate">
                                    <tr>
                                        <td>
                                            <img src="/_layouts/15/TeacherImages/expression.png" /><p>没有找到相关内容</p>
                                        </td>
                                    </tr>
                                </table>
                            </EmptyDataTemplate>
                            <LayoutTemplate>
                                <li id="itemPlaceholder" runat="server"></li>
                            </LayoutTemplate>
                            <ItemTemplate>
                                <li class="li_list">
                                    <div class="top_remarks">
                                        <div class="left_con fl"><span class="times"><%# Eval("PrizeDate") %></span><span class="con_details"><em><%# Eval("PrizeGrade") %></em>|<em><%# Eval("PrizeLevel") %></em>|<%--<em><%# Eval("PrizeType") %></em>|--%><em><%# Eval("RewardsBureau") %></em></span></div>
                                        <div class="right_edit fr"><i class="iconfont">&#xe60a;</i><asp:LinkButton OnClientClick="shouwtab();" CommandName="Edit" CommandArgument='<%# Eval("ID") %>' ID="LB_Edit" runat="server">编辑</asp:LinkButton>|<i class="iconfont">&#xe62f;</i><asp:LinkButton CommandName="Del" CommandArgument='<%# Eval("ID") %>' ID="LB_Del" runat="server" OnClientClick="return confirm('你确定要删除吗？')">删除</asp:LinkButton></div>
                                    </div>
                                    <div class="con_text">
                                        <h2><%# Eval("Title") %><%--<i class="iconfont">&#xe60f;</i>--%></h2>
                                        <div class="con_con">
                                            <p>
                                                <%# Eval("PrizeThankful") %>
                                            </p>
                                            <div class="attachment">附件：<%# Eval("Attachment") %></div>
                                            <%--<div class="attachment"><a href="">附件：<%# Eval("Attachment") %></a></div>--%>
                                        </div>
                                    </div>
                                    <div class="boxmore"><a href="#"><span class="J-more">更多</span></a> </div>
                                </li>
                            </ItemTemplate>
                        </asp:ListView>
                    </ul>
                    <div class="paging">
                        <asp:DataPager ID="DP_Prize" runat="server" PageSize="5" PagedControlID="LV_Prize">
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
                            <%# (Container.TotalRowCount % Container.MaximumRows) > 0 ? Convert.ToInt32(Container.TotalRowCount / Container.MaximumRows) + 1 : Container.TotalRowCount / Container.MaximumRows%>  页
                            (共<%# Container.TotalRowCount %>项)
                                        </span>
                                    </PagerTemplate>
                                </asp:TemplatePagerField>
                            </Fields>
                        </asp:DataPager>
                    </div>

                </div>
            </div>
        </div>
        <!--添加公开课-->
        <div class="tc" style="display: none;" id="five">
            <div class="formdv">

                <div class="topadd"><a href="#"><i class="iconfont">&#xe623;</i>添加公开课</a></div>

                <table class="tabContent">
                    <tr>
                        <th>公开课名称：</th>
                        <td>
                            <asp:TextBox ID="TB_ClassTitle" runat="server"></asp:TextBox>
                            <span style="color: red;">*
                            <asp:RequiredFieldValidator ID="RFV_ClassTitle" runat="server" ControlToValidate="TB_ClassTitle"
                                ErrorMessage="必填" ValidationGroup="ClassSubmit" SetFocusOnError="True" Display="Dynamic"></asp:RequiredFieldValidator></span>
                        </td>
                    </tr>
                    <tr>
                        <th>公开课类型：</th>
                        <td>
                            <asp:TextBox ID="TB_ClassType" runat="server"></asp:TextBox>

                            <span style="color: red;">*
                            <asp:RequiredFieldValidator ID="RFV_ClassType" runat="server" ControlToValidate="TB_ClassType"
                                ErrorMessage="必填" ValidationGroup="ClassSubmit" SetFocusOnError="True" Display="Dynamic"></asp:RequiredFieldValidator></span>
                        </td>

                    </tr>
                    <tr>
                        <th>上课时间：</th>
                        <td>
                            <asp:TextBox ID="TB_ClassTime" CssClass="Wdate" onclick="javascript: WdatePicker()" runat="server"></asp:TextBox>
                            <span style="color: red;">*<asp:RequiredFieldValidator ID="RFV_ClassTime" runat="server" ControlToValidate="TB_ClassTime"
                                ErrorMessage="必填" ValidationGroup="ClassSubmit" SetFocusOnError="True" Display="Dynamic"></asp:RequiredFieldValidator></span>
                        </td>
                    </tr>
                    <tr>
                        <th>公开课级别：</th>
                        <td>
                            <asp:DropDownList ID="DDL_ClassLevel" runat="server"></asp:DropDownList>

                            <span style="color: red;">*
                            <asp:RequiredFieldValidator ID="RFV_ClassLevel" runat="server" ControlToValidate="DDL_ClassLevel"
                                ErrorMessage="必填" ValidationGroup="ClassSubmit" SetFocusOnError="True" Display="Dynamic"></asp:RequiredFieldValidator></span>
                        </td>
                    </tr>
                    <tr>
                        <th>公开课地点：</th>
                        <td>
                            <asp:TextBox ID="TB_ClassAddress" runat="server"></asp:TextBox>
                            <span style="color: red;">*
                            <asp:RequiredFieldValidator ID="RFV_ClassAddress" runat="server" ControlToValidate="TB_ClassAddress"
                                ErrorMessage="必填" ValidationGroup="ClassSubmit" SetFocusOnError="True" Display="Dynamic"></asp:RequiredFieldValidator></span>
                        </td>
                    </tr>
                    <tr>
                        <th>公开课人数：</th>
                        <td>
                            <asp:TextBox ID="TB_StudentNum" runat="server"></asp:TextBox>
                            <span style="color: red;">*
                            <asp:RequiredFieldValidator ID="RFV_StudentNum" runat="server" ControlToValidate="TB_StudentNum"
                                ErrorMessage="必填" ValidationGroup="ClassSubmit" SetFocusOnError="True" Display="Dynamic"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="REV_TB_StudentNum" runat="server" ErrorMessage="请输入有效数字"
                                    Display="Dynamic" ControlToValidate="TB_StudentNum" ValidationExpression="^[1-9][0-9]{0,3}$"
                                    ValidationGroup="ClassSubmit"></asp:RegularExpressionValidator></span>
                        </td>
                    </tr>
                    <tr class="areatr">
                        <th>公开课内容：</th>
                        <td>
                            <asp:TextBox ID="TB_ClassContent" TextMode="MultiLine" Height="82px" runat="server"></asp:TextBox>

                        </td>
                    </tr>
                    <tr>
                        <th>附件：</th>
                        <td>
                            <asp:Label ID="lbAttach4" runat="server">
                            </asp:Label>
                            <asp:HiddenField ID="Hid_fileName4" runat="server" />
                            <table id="idAttachmentsTable4" style="display: block;">
                                <asp:Literal ID="Lit_Bind4" runat="server"></asp:Literal>
                            </table>
                            <table style="width: 100%; display: block;">
                                <tr>
                                    <td id="att4">&nbsp;
                                            <input id="fileClass0" name="fileClass0" style="width: 80%;" type="file" />
                                        <%--<asp:FileUpload ID="FU_TermFile" runat="server" />--%>
                                    </td>
                                    <td>
                                        <input type="button" onclick="UploadFile('fileClass', 'idAttachmentsTable4', 'att4')" style="width: 70px;" value="上传" /></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr class="btntr">
                        <th></th>
                        <td>
                            <asp:Button ID="Btn_ClassSave" OnClick="Btn_ClassSave_Click" CssClass="save" runat="server" ValidationGroup="ClassSubmit" Text="保存" />
                            <asp:Button ID="Btn_ClassCancel" CssClass="cancel" runat="server" OnClick="Btn_ClassCancel_Click" Text="取消" />

                        </td>
                    </tr>
                </table>
            </div>

            <div class="content">
                <div class="container">
                    <ul class="list">
                        <asp:ListView ID="LV_OpenClass" runat="server" OnPagePropertiesChanging="LV_OpenClass_PagePropertiesChanging" OnItemCommand="LV_OpenClass_ItemCommand" OnItemEditing="LV_OpenClass_ItemEditing">
                            <EmptyDataTemplate>
                                <table class="emptydate">
                                    <tr>
                                        <td>
                                            <img src="/_layouts/15/TeacherImages/expression.png" /><p>没有找到相关内容</p>
                                        </td>
                                    </tr>
                                </table>
                            </EmptyDataTemplate>
                            <LayoutTemplate>
                                <li id="itemPlaceholder" runat="server"></li>
                            </LayoutTemplate>
                            <ItemTemplate>
                                <li class="li_list">
                                    <div class="top_remarks">
                                        <div class="left_con fl"><span class="times"><%# Eval("ClassTime") %></span><span class="con_details"><em><%# Eval("ClassType") %></em>|<em><%# Eval("ClassLevel") %></em>|<em><%# Eval("ClassAddress") %></em>|<em><%# Eval("StudentNum") %></em></span></div>
                                        <div class="right_edit fr"><i class="iconfont">&#xe60a;</i><asp:LinkButton OnClientClick="shouwtab();" CommandName="Edit" CommandArgument='<%# Eval("ID") %>' ID="LB_Edit" runat="server">编辑</asp:LinkButton>|<i class="iconfont">&#xe62f;</i><asp:LinkButton CommandName="Del" CommandArgument='<%# Eval("ID") %>' ID="LB_Del" runat="server" OnClientClick="return confirm('你确定要删除吗？')">删除</asp:LinkButton></div>
                                    </div>
                                    <div class="con_text">
                                        <h2><%# Eval("Title") %><%--<i class="iconfont">&#xe60f;</i>--%></h2>
                                        <div class="con_con">
                                            <p>
                                                <%# Eval("ClassContent") %>
                                            </p>
                                            <div class="attachment">附件：<%# Eval("Attachment") %></div>
                                            <div class="attachment">审核意见：<%# Eval("AuditOpinion") %></div>
                                            <%--<div class="attachment"><a href="">附件：<%# Eval("Attachment") %></a></div>--%>
                                        </div>
                                    </div>
                                    <div class="boxmore"><a href="#"><span class="J-more">更多</span></a> </div>
                                </li>
                            </ItemTemplate>
                        </asp:ListView>
                    </ul>
                    <div class="paging">
                        <asp:DataPager ID="DP_Class" runat="server" PageSize="5" PagedControlID="LV_OpenClass">
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
                            <%# (Container.TotalRowCount % Container.MaximumRows) > 0 ? Convert.ToInt32(Container.TotalRowCount / Container.MaximumRows) + 1 : Container.TotalRowCount / Container.MaximumRows%>  页
                            (共<%# Container.TotalRowCount %>项)
                                        </span>
                                    </PagerTemplate>
                                </asp:TemplatePagerField>
                            </Fields>
                        </asp:DataPager>
                    </div>

                </div>
            </div>
        </div>
        <!--指导业绩-->
        <div class="tc" style="display: none;" id="six">
            <div class="formdv">

                <div class="topadd"><a href="#"><i class="iconfont">&#xe623;</i>添加指导业绩</a></div>

                <table class="tabContent">
                    <tr>
                        <th>业绩名称：</th>
                        <td>
                            <asp:TextBox ID="TB_GuidanceTitle" runat="server"></asp:TextBox>
                            <span style="color: red;">*
                            <asp:RequiredFieldValidator ID="RFV_GuidanceTitle" runat="server" ControlToValidate="TB_GuidanceTitle"
                                ErrorMessage="必填" ValidationGroup="TGuidanceSubmit" SetFocusOnError="True" Display="Dynamic"></asp:RequiredFieldValidator></span>
                        </td>
                    </tr>
                    <tr>
                        <th>被指导人：</th>
                        <td>
                            <asp:TextBox ID="TB_tbPersons" runat="server"></asp:TextBox>

                            <span style="color: red;">*
                            <asp:RequiredFieldValidator ID="RFV_tbPersons" runat="server" ControlToValidate="TB_tbPersons"
                                ErrorMessage="必填" ValidationGroup="TGuidanceSubmit" SetFocusOnError="True" Display="Dynamic"></asp:RequiredFieldValidator></span>
                        </td>
                    </tr>
                    <tr>
                        <th>开始时间：</th>
                        <td>
                            <asp:TextBox ID="TB_GuidanceStartTime" CssClass="Wdate" onclick="javascript: WdatePicker()" runat="server"></asp:TextBox>
                            <span style="color: red;">*<asp:RequiredFieldValidator ID="RFV_GuidanceStartTime" runat="server" ControlToValidate="TB_GuidanceStartTime"
                                ErrorMessage="必填" ValidationGroup="TGuidanceSubmit" SetFocusOnError="True" Display="Dynamic"></asp:RequiredFieldValidator></span>
                        </td>
                    </tr>
                    <tr>
                        <th>结束时间：</th>
                        <td>
                            <asp:TextBox ID="TB_GuidanceEndTime" CssClass="Wdate" onclick="javascript: WdatePicker()" runat="server"></asp:TextBox>
                            <span style="color: red;">*<asp:RequiredFieldValidator ID="RFV_GuidanceEndTime" runat="server" ControlToValidate="TB_GuidanceEndTime"
                                ErrorMessage="必填" ValidationGroup="TGuidanceSubmit" SetFocusOnError="True" Display="Dynamic"></asp:RequiredFieldValidator></span>
                        </td>
                    </tr>
                    <tr class="areatr">
                        <th>指导内容：</th>
                        <td>
                            <asp:TextBox ID="TB_GuidContent" TextMode="MultiLine" Height="82px" runat="server"></asp:TextBox>

                        </td>
                    </tr>
                    <tr>
                        <th>附件：</th>
                        <td>
                            <asp:Label ID="lbAttach2" runat="server">
                            </asp:Label>
                            <asp:HiddenField ID="Hid_fileName2" runat="server" />
                            <table id="idAttachmentsTable2" style="display: block;">
                                <asp:Literal ID="Lit_Bind2" runat="server"></asp:Literal>
                            </table>
                            <table style="width: 100%; display: block;">
                                <tr>
                                    <td id="att2">&nbsp;
                                            <input id="fileGuidance0" name="fileGuidance0" style="width: 80%;" type="file" />
                                        <%--<asp:FileUpload ID="FU_TermFile" runat="server" />--%>
                                    </td>
                                    <td>
                                        <input type="button" onclick="UploadFile('fileGuidance', 'idAttachmentsTable2', 'att2')" style="width: 70px;" value="上传" /></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr class="btntr">
                        <th></th>
                        <td>
                            <asp:Button ID="Btn_GuidanceSave" OnClick="Btn_GuidanceSave_Click" CssClass="save" runat="server" ValidationGroup="TGuidanceSubmit" Text="保存" />
                            <asp:Button ID="Btn_GuidanceCancel" CssClass="cancel" runat="server" Text="取消" OnClick="Btn_GuidanceCancel_Click" />

                        </td>
                    </tr>
                </table>
            </div>

            <div class="content">
                <div class="container">
                    <ul class="list">
                        <asp:ListView ID="LV_Guidance" runat="server" OnPagePropertiesChanging="LV_Guidance_PagePropertiesChanging" OnItemCommand="LV_Guidance_ItemCommand" OnItemEditing="LV_Guidance_ItemEditing">
                            <EmptyDataTemplate>
                                <table class="emptydate">
                                    <tr>
                                        <td>
                                            <img src="/_layouts/15/TeacherImages/expression.png" /><p>没有找到相关内容</p>
                                        </td>
                                    </tr>
                                </table>
                            </EmptyDataTemplate>
                            <LayoutTemplate>
                                <li id="itemPlaceholder" runat="server"></li>
                            </LayoutTemplate>
                            <ItemTemplate>
                                <li class="li_list">
                                    <div class="top_remarks">
                                        <div class="left_con fl"><span class="times"><%# Eval("StartTime") %>-<%# Eval("EndTime") %></span><span class="con_details"><em><%# Eval("tbPersons") %></em>|</span></div>
                                        <div class="right_edit fr"><i class="iconfont">&#xe60a;</i><asp:LinkButton OnClientClick="shouwtab();" CommandName="Edit" CommandArgument='<%# Eval("ID") %>' ID="LB_Edit" runat="server">编辑</asp:LinkButton>|<i class="iconfont">&#xe62f;</i><asp:LinkButton CommandName="Del" CommandArgument='<%# Eval("ID") %>' ID="LB_Del" runat="server" OnClientClick="return confirm('你确定要删除吗？')">删除</asp:LinkButton></div>
                                    </div>
                                    <div class="con_text">
                                        <h2><%# Eval("Title") %><%--<i class="iconfont">&#xe60f;</i>--%></h2>
                                        <div class="con_con">
                                            <p>
                                                <%# Eval("GuidContent") %>
                                            </p>
                                            <div class="attachment">附件：<%# Eval("Attachment") %></div>
                                            <%--<div class="attachment"><a href="">附件：<%# Eval("Attachment") %></a></div>--%>
                                        </div>
                                    </div>
                                    <div class="boxmore"><a href="#"><span class="J-more">更多</span></a> </div>
                                </li>
                            </ItemTemplate>
                        </asp:ListView>
                    </ul>
                    <div class="paging">
                        <asp:DataPager ID="DP_Guidance" runat="server" PageSize="5" PagedControlID="LV_Guidance">
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
                            <%# (Container.TotalRowCount % Container.MaximumRows) > 0 ? Convert.ToInt32(Container.TotalRowCount / Container.MaximumRows) + 1 : Container.TotalRowCount / Container.MaximumRows%>  页
                            (共<%# Container.TotalRowCount %>项)
                                        </span>
                                    </PagerTemplate>
                                </asp:TemplatePagerField>
                            </Fields>
                        </asp:DataPager>
                    </div>

                </div>
            </div>
        </div>
        <!--论文专著-->
        <div class="tc" style="display: none;" id="seven">
            <div class="formdv">

                <div class="topadd"><a href="#"><i class="iconfont">&#xe623;</i>添加论文专著</a></div>

                <table class="tabContent">
                    <tr>
                        <th>论文名称：</th>
                        <td>
                            <asp:TextBox ID="TB_ThesisTitle" runat="server"></asp:TextBox>
                            <span style="color: red;">*
                            <asp:RequiredFieldValidator ID="RFV_ThesisTitle" runat="server" ControlToValidate="TB_ThesisTitle"
                                ErrorMessage="必填" ValidationGroup="ThesisSubmit" SetFocusOnError="True" Display="Dynamic"></asp:RequiredFieldValidator></span>
                        </td>
                    </tr>
                    <tr>
                        <th>著作人：</th>
                        <td>
                            <asp:TextBox ID="TB_ThesisAuthor" runat="server"></asp:TextBox>
                            <span style="color: red;">*
                            <asp:RequiredFieldValidator ID="RFV_ThesisAuthor" runat="server" ControlToValidate="TB_ThesisAuthor"
                                ErrorMessage="必填" ValidationGroup="ThesisSubmit" SetFocusOnError="True" Display="Dynamic"></asp:RequiredFieldValidator></span>
                        </td>
                    </tr>
                    <tr>
                        <th>出版日期：</th>
                        <td>
                            <asp:TextBox ID="TB_ThesisStartTime" CssClass="Wdate" onclick="javascript: WdatePicker()" runat="server"></asp:TextBox>
                            <span style="color: red;">*<asp:RequiredFieldValidator ID="RFV_ThesisStartTime" runat="server" ControlToValidate="TB_ThesisStartTime"
                                ErrorMessage="必填" ValidationGroup="ThesisSubmit" SetFocusOnError="True" Display="Dynamic"></asp:RequiredFieldValidator></span>
                        </td>
                    </tr>
                    <tr>
                        <th>获奖日期：</th>
                        <td>
                            <asp:TextBox ID="TB_ThesisEndTime" CssClass="Wdate" onclick="javascript: WdatePicker()" runat="server"></asp:TextBox>
                            <span style="color: red;">*<asp:RequiredFieldValidator ID="RFV_ThesisEndTime" runat="server" ControlToValidate="TB_ThesisEndTime"
                                ErrorMessage="必填" ValidationGroup="ThesisSubmit" SetFocusOnError="True" Display="Dynamic"></asp:RequiredFieldValidator></span>
                        </td>
                    </tr>
                    <tr>
                        <th>获奖级别：</th>
                        <td>
                            <asp:DropDownList ID="DDL_ThesisLevel" runat="server"></asp:DropDownList>

                            <span style="color: red;">*
                            <asp:RequiredFieldValidator ID="RFV_ThesisLevel" runat="server" ControlToValidate="DDL_ThesisLevel"
                                ErrorMessage="必填" ValidationGroup="ThesisSubmit" SetFocusOnError="True" Display="Dynamic"></asp:RequiredFieldValidator></span>
                        </td>
                    </tr>
                    <tr>
                        <th>获奖等级：</th>
                        <td>
                            <asp:DropDownList ID="DDL_ThesisGrade" runat="server"></asp:DropDownList>

                            <span style="color: red;">*
                            <asp:RequiredFieldValidator ID="RFV_ThesisGrade" runat="server" ControlToValidate="DDL_ThesisGrade"
                                ErrorMessage="必填" ValidationGroup="ThesisSubmit" SetFocusOnError="True" Display="Dynamic"></asp:RequiredFieldValidator></span>
                        </td>
                    </tr>
                    <tr>
                        <th>附件：</th>
                        <td>
                            <asp:Label ID="lbAttach1" runat="server">
                            </asp:Label>
                            <asp:HiddenField ID="Hid_fileName1" runat="server" />
                            <table id="idAttachmentsTable1" style="display: block;">
                                <asp:Literal ID="Lit_Bind1" runat="server"></asp:Literal>
                            </table>
                            <table style="width: 100%; display: block;">
                                <tr>
                                    <td id="att1">&nbsp;
                                            <input id="fileup0" name="fileup0" style="width: 80%;" type="file" />
                                        <%--<asp:FileUpload ID="FU_TermFile" runat="server" />--%>
                                    </td>
                                    <td>
                                        <input type="button" onclick="UploadFile('fileup', 'idAttachmentsTable1', 'att1')" style="width: 70px;" value="上传" /></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr class="btntr">
                        <th></th>
                        <td>
                            <asp:Button ID="Btn_ThesisSave" CssClass="save" runat="server" OnClick="Btn_ThesisSave_Click" ValidationGroup="ThesisSubmit" Text="保存" />
                            <asp:Button ID="Btn_ThesisCancel" CssClass="cancel" runat="server" OnClick="Btn_ThesisCancel_Click" Text="取消" />

                        </td>
                    </tr>
                </table>
            </div>

            <div class="content">
                <div class="container">
                    <ul class="list">
                        <asp:ListView ID="LV_Thesis" runat="server" OnPagePropertiesChanging="LV_Thesis_PagePropertiesChanging" OnItemCommand="LV_Thesis_ItemCommand" OnItemEditing="LV_Thesis_ItemEditing">
                            <EmptyDataTemplate>
                                <table class="emptydate">
                                    <tr>
                                        <td>
                                            <img src="/_layouts/15/TeacherImages/expression.png" /><p>没有找到相关内容</p>
                                        </td>
                                    </tr>
                                </table>
                            </EmptyDataTemplate>
                            <LayoutTemplate>
                                <li id="itemPlaceholder" runat="server"></li>
                            </LayoutTemplate>
                            <ItemTemplate>
                                <li class="li_list">
                                    <div class="top_remarks">
                                        <div class="left_con fl"><span class="times">出版日期：<%# Eval("StartTime") %></span><span class="con_details"><em><%# Eval("PrizeLevel") %></em>|<em>获奖日期：<%# Eval("EndTime") %></em>|</span></div>
                                        <div class="right_edit fr"><i class="iconfont">&#xe60a;</i><asp:LinkButton OnClientClick="shouwtab();" CommandName="Edit" CommandArgument='<%# Eval("ID") %>' ID="LB_Edit" runat="server">编辑</asp:LinkButton>|<i class="iconfont">&#xe62f;</i><asp:LinkButton CommandName="Del" CommandArgument='<%# Eval("ID") %>' ID="LB_Del" runat="server" OnClientClick="return confirm('你确定要删除吗？')">删除</asp:LinkButton></div>
                                    </div>
                                    <div class="con_text">
                                        <h2><%# Eval("Title") %><%--<i class="iconfont">&#xe60f;</i>--%></h2>
                                        <div class="con_con">
                                            <div class="attachment">附件：<%# Eval("Attachment") %></div>
                                        </div>
                                    </div>
                                    <div class="boxmore"><a href="#"><span class="J-more">更多</span></a> </div>
                                </li>
                            </ItemTemplate>
                        </asp:ListView>
                    </ul>
                    <div class="paging">
                        <asp:DataPager ID="DP_Thesis" runat="server" PageSize="5" PagedControlID="LV_Thesis">
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
                            <%# (Container.TotalRowCount % Container.MaximumRows) > 0 ? Convert.ToInt32(Container.TotalRowCount / Container.MaximumRows) + 1 : Container.TotalRowCount / Container.MaximumRows%>  页
                            (共<%# Container.TotalRowCount %>项)
                                        </span>
                                    </PagerTemplate>
                                </asp:TemplatePagerField>
                            </Fields>
                        </asp:DataPager>
                    </div>

                </div>
            </div>
        </div>
    </div>
</div>
<script type="text/javascript">
    $(function () {
        var intvalue = $("Input[id*='HF_TabFlag']").val();
        var loadindex = $("." + intvalue).parent().index();
        $("." + intvalue).parent().addClass("selected").siblings().removeClass("selected");
        $("." + intvalue).parents(".yy_tab").find(".tc").eq(loadindex).show().siblings().hide();

        //var hidthree = $("Input[id*='HidThree']").val();
        //$("#tabThree").css("display", hidthree);

        $(".yy_tab .yy_tabheader").find("a").click(function () {
            var index = $(this).parent().index();
            $(this).parent().addClass("selected").siblings().removeClass("selected");
            $(this).parents(".yy_tab").find(".tc").eq(index).show().siblings().hide();

            $("Input[id*='HF_TabFlag']").val($(this).attr("class"));
        });
        $(".topadd").find("a").click(function () {
            $(this).parent().next().slideToggle();
            //$(".tabContent").show();
        });
        $(".J-more").click(function () {
            $(this).html($(this).html() == "更多" ? "收起" : "更多");
            $(this).parents('.boxmore').siblings('.con_text').find('.con_con').toggle();
        });

    });
    function hidetab() {
        $(".tabContent").hide();
    }
    function showtab() {
        $(".tabContent").show();
    }
    function showterm() {
        $(".dvaddTerm").hide();
        $("#addterm").show();
    }
    function showupload() {
        $("#dvupload").show();
    }
    function hideupload() {
        $("#dvupload").hide();
    }

    function showload() {
        $("#dvload").show();
    }
    function hideload() {
        $("#dvload").hide();
    }
</script>
