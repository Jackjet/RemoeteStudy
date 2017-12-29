<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AcademicSemesterManage.aspx.cs" Inherits="UserCenterSystem.AcademicSemesterManage" EnableViewState="true" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link type="text/css" href="css/page.css" rel="stylesheet" />
    <link href="Scripts/wbox/wbox.css" rel="stylesheet" />
    <script src="Scripts/jquery-1.8.3.min.js"></script>
    <script src="Scripts/wbox.js"></script>
    <script src="Scripts/My97DatePicker/WdatePicker.js"></script>
    <title>学年学期信息</title>
    <script type="text/javascript">
        function funExcelImportStu() {
            var wBoxExcelImport = $("#TeacherDR").wBox({
                //iframeWH: {
                //    width: 500, height: 500
                //},
                //requestType: "iframe",
                title: "添加学年学期",
                target: "#panelAdd"
            });
            wBoxExcelImport.showBox();
        }
        //将日期存到隐藏域
        function InintDate() {
            $("#FSstarDateHiddenField").val($("#FSstarDate").val());
            $("#FSendDateHiddenField").val($("#FSendDate").val());
            $("#SSstarDateHiddenField").val($("#SSstarDate").val());
            $("#SSendDateHiddenField").val($("#SSendDate").val());


            if ($("#Yearddl ").val() == "0") {
                alert('请选择学年');
                return false;
            }
            else if ($.trim($("#FSstarDateHiddenField").val()).length == 0 ||
                 $.trim($("#FSendDateHiddenField").val()).length == 0 ||
                 $.trim($("#SSstarDateHiddenField").val()).length == 0 ||
                 $.trim($("#SSendDateHiddenField").val()).length == 0) {
                alert('第一学期或第二学期的日期不能为空');
                return false;
            }
        }
        $(function () {

            if ('<%=flag%>' == "第一学期") {
                $("#FSstarDate").val('<%=FSstarDate%>');
                $("#FSendDate").val('<%=FSendDate%>');
                //其他学期
                $("#SSstarDate").val('<%=SSstarDate%>');
                $("#SSendDate").val('<%=SSendDate%>');
                $("#SSstarDate").attr("disabled", "disabled").css("background-color", "#E5E5E5");//禁用第二学期开始
                $("#SSendDate").attr("disabled", "disabled").css("background-color", "#E5E5E5");//禁用第二学期结束
            }
            else if ('<%=flag%>' == "第二学期") {
                $("#SSstarDate").val('<%=FSstarDate%>');
                $("#SSendDate").val('<%=FSendDate%>');
                //其他学期
                $("#FSstarDate").val('<%=SSstarDate%>');
                $("#FSendDate").val('<%=SSendDate%>');
                $("#FSstarDate").attr("disabled", "disabled").css("background-color", "#E5E5E5");//禁用第一学期开始
                $("#FSendDate").attr("disabled", "disabled").css("background-color", "#E5E5E5");//禁用第一学期结束

            }
            else {
                $("#FSstarDate").val('<%=FSstarDate%>');
                $("#FSendDate").val('<%=FSendDate%>');
                $("#SSstarDate").val('<%=SSstarDate%>');
                $("#SSendDate").val('<%=SSendDate%>');
            }
        })
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:HiddenField ID="EidtHidden" runat="server" />
        <asp:HiddenField ID="KeyID" runat="server" />
        <asp:HiddenField ID="Semester" runat="server" />

        <asp:HiddenField ID="hidFlagNum" runat="server" />
        <div class="con_wrap">
            <div class="divHeader" style="text-align: center; width: 100%">
                <h3>学年学期信息</h3>
            </div>
            <div class="divMain">
                <asp:Panel ID="panelDisp" runat="server">
                    <asp:Button ID="btHandel" runat="server" Text="添加" BackColor="Wheat" OnClick="btHandel_Click" />
                    <%--年级展示--%>
                    <div class="con_list">
                        <asp:ListView ID="lvDisp" runat="server" DataKeyNames="StudysectionID" OnItemCommand="lvDisp_ItemCommand" Visible="false"
                            OnItemEditing="lvDisp_ItemEditing" OnPagePropertiesChanging="lvDisp_PagePropertiesChanging">
                            <EmptyDataTemplate>
                                <table runat="server" style="width: 100%;" border="1">
                                    <tr>
                                        <td>没有符合条件的信息</td>
                                    </tr>
                                </table>
                            </EmptyDataTemplate>
                            <ItemTemplate>
                                <tr style="text-align: center;">
                                    <td><%#Eval("Academic") %></td>
                                    <td><%#Eval("Semester") %></td>
                                    <td><%# DateTime.Parse(Eval("SStartDate").ToString()).ToString("yyyy-MM-dd") %></td>
                                    <td><%# DateTime.Parse(Eval("SEndDate").ToString()).ToString("yyyy-MM-dd")  %></td>
                                    <td>
                                        <asp:Button ID="lbEdit" runat="server" Text="修改" CommandName="edit" BackColor="Wheat"></asp:Button>
                                        <asp:Button ID="lbDel" runat="server" Text="删除" CommandName="del" BackColor="Wheat"></asp:Button>
                                        <asp:HiddenField ID="hfID" runat="server" Value='<%#Eval("StudysectionID") %>' />
                                        <asp:HiddenField ID="SemesterHiddenField" runat="server" Value='<%#Eval("Semester") %>' />

                                    </td>
                                </tr>
                            </ItemTemplate>
                            <AlternatingItemTemplate>
                                <tr class="alt-row">
                                    <td><%#Eval("Academic") %></td>
                                    <td><%#Eval("Semester") %></td>
                                    <td><%# DateTime.Parse(Eval("SStartDate").ToString()).ToString("yyyy-MM-dd") %></td>
                                    <td><%# DateTime.Parse(Eval("SEndDate").ToString()).ToString("yyyy-MM-dd")  %></td>
                                    <td>
                                        <asp:Button ID="lbEdit" runat="server" Text="修改" CommandName="edit" BackColor="Wheat"></asp:Button>
                                        <asp:Button ID="lbDel" runat="server" Text="删除" CommandName="del" BackColor="Wheat"></asp:Button>
                                        <asp:HiddenField ID="hfID" runat="server" Value='<%#Eval("StudysectionID") %>' />
                                        <asp:HiddenField ID="SemesterHiddenField" runat="server" Value='<%#Eval("Semester") %>' />
                                    </td>
                                </tr>
                            </AlternatingItemTemplate>
                            <LayoutTemplate>
                                <table style="width: 100%;">
                                    <tr>
                                        <td>
                                            <table id="itemPlaceholderContainer" runat="server" style="width: 100%;">
                                                <tr class="b_txt alt-row">
                                                    <th>学年</th>
                                                    <th>学期</th>
                                                    <th>起始时间</th>
                                                    <th>结束时间</th>
                                                    <th>操作</th>
                                                </tr>
                                                <tr id="itemPlaceholder" runat="server">
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </LayoutTemplate>
                        </asp:ListView>
                    </div>
                    <table style="width: 100%;">
                        <tr style="line-height: 40px; color: #333333; text-align: center;">
                            <td>
                                <div class="pagination">
                                    <asp:DataPager ID="DPTeacher" runat="server" PageSize="10" PagedControlID="lvDisp">
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
                            </td>
                        </tr>

                    </table>


                </asp:Panel>
                <asp:Panel ID="panelAdd" runat="server" Visible="false">
                    <div style="width: 525px; margin: 0 auto;">
                        <table style="width: 100%;">

                            <tr>
                                <td style="width: 150px;">学年：<span style="color: Red;">*</span></td>
                                <td>
                                    <asp:DropDownList ID="Yearddl" runat="server" Width="100px"></asp:DropDownList>
                                </td>
                            </tr>
                            <tr id="diyixueqi">

                                <td>第一学期起止日期：<span style="color: Red;">*</span></td>
                                <td>

                                    <input class="Wdate" type="text" id="FSstarDate" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd'})" />—
                                <input class="Wdate" type="text" id="FSendDate" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd'})" />

                                    <asp:HiddenField ID="FSstarDateHiddenField" runat="server" />
                                    <asp:HiddenField ID="FSendDateHiddenField" runat="server" />
                                </td>

                            </tr>
                            <tr id="dierxueqi">

                                <td>第二学期起止日期：<span style="color: Red;">*</span></td>
                                <td>
                                    <input class="Wdate" type="text" id="SSstarDate" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd'})" />—
                                <input class="Wdate" type="text" id="SSendDate" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd'})" />
                                    <asp:HiddenField ID="SSstarDateHiddenField" runat="server" />
                                    <asp:HiddenField ID="SSendDateHiddenField" runat="server" />
                                </td>

                            </tr>
                            <tr>
                                <td colspan="2">
                                    <div style="width: 190px; margin: 20px auto;">
                                        <asp:Button ID="btSave" class="btnsave mr40" runat="server" Text="保存" OnClick="btSave_Click" OnClientClick="return InintDate()" />&nbsp;&nbsp;
                            <asp:Button ID="btCancel" runat="server" Text="取消" OnClick="btCancel_Click" Style="width: 60px" />&nbsp;&nbsp;
                            <asp:Label ID="lbMessage" runat="server" ForeColor="Red" Font-Size="14px"></asp:Label>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </div>
                </asp:Panel>
                <asp:HiddenField ID="hfDelete" runat="server" Value="0" />
            </div>
            

            <div class="con_list" runat="server" id="dis2"  visible="false">
                        <asp:ListView ID="lvDisp2" runat="server" DataKeyNames="StudysectionID" Visible="false"
                            OnPagePropertiesChanging="lvDisp_PagePropertiesChanging">
                            <EmptyDataTemplate>
                                <table runat="server" style="width: 100%;" border="1">
                                    <tr>
                                        <td>没有符合条件的信息</td>
                                    </tr>
                                </table>
                            </EmptyDataTemplate>
                            <ItemTemplate>
                                <tr style="text-align: center;">
                                    <td><%#Eval("Academic") %></td>
                                    <td><%#Eval("Semester") %></td>
                                    <td><%# DateTime.Parse(Eval("SStartDate").ToString()).ToString("yyyy-MM-dd") %></td>
                                    <td><%# DateTime.Parse(Eval("SEndDate").ToString()).ToString("yyyy-MM-dd")  %></td>
                                </tr>
                            </ItemTemplate>
                            <AlternatingItemTemplate>
                                <tr class="alt-row">
                                    <td><%#Eval("Academic") %></td>
                                    <td><%#Eval("Semester") %></td>
                                    <td><%# DateTime.Parse(Eval("SStartDate").ToString()).ToString("yyyy-MM-dd") %></td>
                                    <td><%# DateTime.Parse(Eval("SEndDate").ToString()).ToString("yyyy-MM-dd")  %></td>
                                </tr>
                            </AlternatingItemTemplate>
                            <LayoutTemplate>
                                <table style="width: 100%;">
                                    <tr>
                                        <td>
                                            <table id="itemPlaceholderContainer" runat="server" border="1" style="width: 100%;">
                                                <tr class="b_txt alt-row">
                                                    <th>学年</th>
                                                    <th>学期</th>
                                                    <th>起始时间</th>
                                                    <th>结束时间</th>
                                                </tr>
                                                <tr id="itemPlaceholder" runat="server">
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </LayoutTemplate>
                        </asp:ListView>
                    </div>
                    <table style="width: 100%;">
                        <tr style="line-height: 40px; color: #333333; text-align: center;">
                            <td>
                                <div class="pagination" runat="server" id="fenye2">
                                    <asp:DataPager ID="DataPager1" runat="server" PageSize="10" PagedControlID="lvDisp2">
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
                            </td>
                        </tr>
                    </table>

        </div>
    </form>
</body>
</html>
