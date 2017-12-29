<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="operationLogManager.aspx.cs" Inherits="UserCenterSystem.operationLogManager" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link type="text/css" href="css/page.css" rel="stylesheet" />
    <script src="Scripts/jquery-1.8.3.min.js"></script>
    <script src="Scripts/My97DatePicker/WdatePicker.js"></script>
    <style type="text/css">
        .dateinput {
            border: 1px solid #ccc;
            border-radius: 3px;
            box-shadow: inset 0px 1px 1px rgba(0,0,0,0.075);
        }

        .height28 {
            height: 28px;
        }

        .height30 {
            height: 30px;
        }
    </style>
    <script type="text/javascript">
        //将日期存到隐藏域
        function InintDate() {
            $("#starDateHidden").val($("#starDate").val());
            $("#endDateHidden").val($("#endDate").val());
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="con_wrap">
            <div style="text-align: center; width: 100%">
                <h3>操作日志管理</h3>
            </div>
            <div>
                <table style="width: 100%;">
                    <tr>
                        <td>
                            <div id="container">
                                <span>人员姓名</span>
                                <asp:TextBox ID="PersonName" runat="server"></asp:TextBox>
                                &nbsp;
                                    <span>模块名称</span>
                                <asp:DropDownList ID="Modelnode" runat="server" AutoPostBack="false" class="height30 dateinput" Width="80px">
                                    <asp:ListItem Value="0" Selected="True">-请选择-</asp:ListItem>
                                    <asp:ListItem Value="用户登录模块">用户登录模块</asp:ListItem>
                                    <asp:ListItem Value="教师信息管理">教师信息管理</asp:ListItem>
                                    <asp:ListItem Value="学生信息管理">学生信息管理</asp:ListItem>
                                    <%--<asp:ListItem Value="家长信息管理">家长信息管理</asp:ListItem>--%>
                                    <asp:ListItem Value="组织机构管理">组织机构管理</asp:ListItem>
                                    <asp:ListItem Value="年级管理">年级管理</asp:ListItem>
                                    <asp:ListItem Value="班级管理">班级管理</asp:ListItem>
                                    <asp:ListItem Value="学科信息">学科信息</asp:ListItem>
                                    <asp:ListItem Value="学科管理">学科管理</asp:ListItem>
                                    <%--<asp:ListItem Value="角色管理">角色管理</asp:ListItem>--%>
                                    <%--<asp:ListItem Value="学年学期管理">学年学期管理</asp:ListItem>--%>
                                    <%--<asp:ListItem Value="教研组管理">教研组管理</asp:ListItem>--%>
                                    <asp:ListItem Value="系统账号管理">系统账号管理</asp:ListItem>
                                    <%--<asp:ListItem Value="接口信息管理">接口信息管理</asp:ListItem>--%>
                                    <%--<asp:ListItem Value="接口权限管理">接口权限管理</asp:ListItem>--%>
                                    <asp:ListItem Value="修改密码管理">修改密码管理</asp:ListItem>
                                    <asp:ListItem Value="操作日志管理">操作日志管理</asp:ListItem>

                                </asp:DropDownList>
                                &nbsp;&nbsp;
                           <span>起始时间</span>
                                <input class="Wdate dateinput height28" type="text" id="starDate" runat="server" onfocus="WdatePicker()" />
                                <asp:HiddenField ID="starDateHidden" runat="server" />
                                <span>结束时间</span>
                                <input class="Wdate dateinput height28" type="text" id="endDate" runat="server" onfocus="WdatePicker()" />
                                <asp:HiddenField ID="endDateHidden" runat="server" />
                                <asp:Button ID="bt_Search" runat="server" Text="查询" Width="60px" BackColor="Wheat" OnClientClick="InintDate()" OnClick="bt_Search_Click" Style="margin-left: 10px;" />
                                <asp:Button ID="btnExport" runat="server" Text="导出" OnClick="btnExport_Click" Style="width: 60px; margin-left: 10px; background-color: Wheat;" OnClientClick="Javascript:return confirm('确定要导出吗？');" />
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div class="con_list" style="margin-left: 0px;">
                                <asp:ListView ID="lvLog" runat="server" OnPagePropertiesChanging="lvDPLog_PagePropertiesChanging">
                                    <EmptyDataTemplate>
                                        <table runat="server" style="border-collapse: collapse; border: 1px #F3F3F3 solid">
                                            <tr>
                                                <td style="text-align: center">暂无记录            
                                                </td>
                                            </tr>
                                        </table>
                                    </EmptyDataTemplate>
                                    <ItemTemplate>
                                        <tr style="text-align: center">

                                            <td style="width: 20%">
                                                <%# Eval("RYXM") %>
                                            </td>
                                            <td style="width: 25%">
                                                <%# Eval("MKMC") %>
                                            </td>
                                            <td style="width: 35%">
                                                <%# Eval("CZXX") %>
                                            </td>
                                            <td>
                                                <%# DateTime.Parse(Eval("CZSJ").ToString()).ToString("yyyy-MM-dd HH:mm:ss") %>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                    <AlternatingItemTemplate>
                                        <tr class="alt-row">
                                            <td style="width: 20%">
                                                <%# Eval("RYXM") %>
                                            </td>
                                            <td style="width: 25%">
                                                <%# Eval("MKMC") %>
                                            </td>
                                            <td style="width: 35%">
                                                <%# Eval("CZXX") %>
                                            </td>
                                            <td>
                                                <%# DateTime.Parse(Eval("CZSJ").ToString()).ToString("yyyy-MM-dd HH:mm:ss") %>
                                            </td>
                                        </tr>
                                    </AlternatingItemTemplate>
                                    <LayoutTemplate>
                                        <table id="itemPlaceholderContainer" runat="server" border="1" style="width: 100%;">
                                            <tr runat="server" class="b_txt alt-row">
                                                <th runat="server">人员姓名</th>
                                                <th runat="server">模块名称</th>
                                                <th runat="server">操作信息</th>
                                                <th runat="server">操作时间</th>
                                            </tr>
                                            <tr id="itemPlaceholder" runat="server">
                                            </tr>
                                        </table>
                                        </table>
                                    </LayoutTemplate>
                                </asp:ListView>
                                <asp:HiddenField ID="hiddenStrxxzzjgh" runat="server" />
                            </div>
                        </td>
                    </tr>
                </table>
                <table style="width: 100%;">
                    <tr style="line-height: 40px; color: #333333; text-align: center;">
                        <td>
                            <div class="pagination">
                                <asp:DataPager ID="DPLog" runat="server" PageSize="15" PagedControlID="lvLog">
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
        </div>
    </form>
</body>
</html>
