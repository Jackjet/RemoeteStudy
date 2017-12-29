using System;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using YHSD.VocationalEducation.Portal.Code.Common;
using YHSD.VocationalEducation.Portal.Code.Entity;
using YHSD.VocationalEducation.Portal.Code.Manager;
using System.Web;
using System.Data;
using System.IO;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.HSSF.UserModel;
using System.Text;
using Centerland.ADUtility;
using System.Web.UI.WebControls;

namespace YHSD.VocationalEducation.Portal.Layouts.YHSD.VocationalEducation.Portal
{
    public partial class ImportUser : LayoutsPageBase
    {
        private IWorkbook workbook = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.HttpMethod == "POST" && Request.Files["FileData"] != null)
                {
                    DataImportUser();
                }
                else
                {
                    cbsSytem.Items.Add(new System.Web.UI.WebControls.ListItem { Text = PublicEnum.SystemStudentWebName, Value = PublicEnum.SystemStudentUrl });
                    cbsSytem.Items.Add(new System.Web.UI.WebControls.ListItem { Text = PublicEnum.SystemTeacherWebName, Value = PublicEnum.SystemTeacherUrl });
                    cbsSytem.Items.Add(new System.Web.UI.WebControls.ListItem { Text = PublicEnum.SystemPartyMemberWebName, Value = PublicEnum.SystemPartyMemberUrl });
                    cbsSytem.SelectedValue = CommonUtil.GetChildWebUrl();//默认添加当前站点
                    foreach (ListItem li in cbsSytem.Items)
                    {
                        li.Attributes.Add("alt", li.Value);
                    }
                }
            }

        }
        public void DataImportUser()
        {
            HttpPostedFile file = Request.Files["FileData"];
            DataTable TableUser = ImportExcelFile(file.InputStream, file.FileName, true);
            UserInfo user = new UserInfo();
            UserInfoManager userManager = new UserInfoManager();
            SPWeb web = SPContext.Current.Site.RootWeb;
            bool oldValue = web.AllowUnsafeUpdates;
            web.AllowUnsafeUpdates = true;
            for (int i = 0; i < TableUser.Rows.Count; i++)
            {
                user.Id = Guid.NewGuid().ToString();
                user.Code = TableUser.Rows[i][0].ToString().Trim();
                user.Name=TableUser.Rows[i][1].ToString().Trim();
                if (string.IsNullOrEmpty(user.Code) || string.IsNullOrEmpty(user.Name))
                {
                    continue;
                }
                user.DomainAccount = ConnectionManager.ADName + "\\" + user.Code;
                user.Sex=TableUser.Rows[i][2].ToString()=="男"?"1":"0";
                user.Birthday = TableUser.Rows[i][3].ToString();
                user.Mobile = TableUser.Rows[i][4].ToString();
                user.Telephone = TableUser.Rows[i][5].ToString();
                user.MSN = TableUser.Rows[i][6].ToString(); 
                user.QQ = TableUser.Rows[i][7].ToString(); ;
                user.Email = TableUser.Rows[i][8].ToString(); 
                user.ZipCode = TableUser.Rows[i][9].ToString(); 
                user.EmergencyContact = TableUser.Rows[i][10].ToString();
                user.EmergencyTel = TableUser.Rows[i][11].ToString();
                user.CardID = TableUser.Rows[i][12].ToString(); 
                user.IsDelete = "0";
                //StringBuilder sbs = new StringBuilder();
                //foreach (System.Web.UI.WebControls.ListItem item in cbsSytem.Items)
                //{
                //    if (item.Selected)
                //    {
                //        sbs.AppendFormat("{0},", item.Value);
                //    }
                //}
                
                //user.SystemStr = sbs.ToString().TrimEnd(',');
                if (!string.IsNullOrEmpty(Request["HDCheckValue"]))
                {
                    user.SystemStr = Request["HDCheckValue"].ToString();
                }
                //添加到系统中
                userManager.Add(user);
                //同步到AD域中
                if (CommonUtil.SynchronousADUser(user) == "")
                {
                    try
                    {
                        web.EnsureUser(user.DomainAccount);
                    }
                    catch (Exception)
                    {
                    }
                }
             
            }
            web.AllowUnsafeUpdates = oldValue;
            Response.Write(TableUser.Rows.Count);
            Response.End();
           // Page.ClientScript.RegisterStartupScript(typeof(string), "OK", "<script>OL_CloserLayerAlert('成功导入["+TableUser.Rows.Count+"]条人员数据!');</script>");
        }
        public DataTable ImportExcelFile(Stream file, string filename, bool isFirstRowColumn)
        {
            ISheet sheet = null;
            DataTable data = new DataTable();
            int startRow = 0;

            try
            {
                //using (FileStream file = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                //{
                if (filename.IndexOf(".xlsx") > 0) // 2007版本
                    workbook = new XSSFWorkbook(file);
                else if (filename.IndexOf(".xls") > 0) // 2003版本
                    workbook = new HSSFWorkbook(file);
                //}


                sheet = workbook.GetSheetAt(0);

                if (sheet != null)
                {
                    IRow firstRow = sheet.GetRow(0);
                    int cellCount = firstRow.LastCellNum; //一行最后一个cell的编号 即总的列数

                    if (isFirstRowColumn)
                    {
                        for (int i = firstRow.FirstCellNum; i < cellCount; ++i)
                        {
                            ICell cell = firstRow.GetCell(i);
                            if (cell != null)
                            {
                                string cellValue = cell.StringCellValue;
                                if (cellValue != null)
                                {
                                    DataColumn column = new DataColumn(cellValue);
                                    data.Columns.Add(column);
                                }
                            }
                        }
                        startRow = sheet.FirstRowNum + 1;
                    }
                    else
                    {
                        startRow = sheet.FirstRowNum;
                    }
                    //最后一列的标号
                    int rowCount = sheet.LastRowNum;
                    for (int i = startRow; i <= rowCount; ++i)
                    {
                        IRow row = sheet.GetRow(i);
                        if (row == null) continue; //没有数据的行默认是null　　　　　　　

                        DataRow dataRow = data.NewRow();
                        for (int j = row.FirstCellNum; j < cellCount; ++j)
                        {
                            if (row.GetCell(j) != null) //同理，没有数据的单元格都默认是null
                                dataRow[j] = row.GetCell(j).ToString();
                        }
                        data.Rows.Add(dataRow);
                    }
                }

                return data;

            }
            catch (Exception e)
            {
                throw e;
            }



        }
    }
}
