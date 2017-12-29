using BLL;
using Common;
using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
namespace UserCenterSystem
{
    public partial class TeacherImport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// 【Button】【导入】
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnImport_Click(object sender, EventArgs e)
        {
            //1秒延时，让用户看到正在上传的提示
            System.Threading.Thread.Sleep(1000);
            if (fuImport.HasFile)
            {
                string FileName = fuImport.FileName;
                string strExtName = FileName.Substring(FileName.LastIndexOf(".") + 1).ToUpper();
                string XXZZJGH = "";
                DataTable tableTemp = null;
                string strToFile = "";
                // 显示在页面上的提示信息			
                string wrongSum = "";
                if (strExtName == "XLS" || strExtName == "XLSX")
                {
                    try
                    {
                        #region 第一 上传excel
                        // 保存的路径
                        string strToFilePath = "Upload/Teacher/";
                        // 物理完整路径                    
                        string strToFileFullPath = MapPath(strToFilePath);
                        // 检查是否有该路径  没有就创建
                        if (!Directory.Exists(strToFileFullPath))
                        {
                            Directory.CreateDirectory(strToFileFullPath);
                        }
                        string fileName = "Teacher" + DateTime.Now.ToString("yyyyMMddHHmmssfffffff") + ".xls";
                        // 将要保存的完整文件名                
                        strToFile = strToFileFullPath + fileName;
                        fuImport.SaveAs(strToFile);
                        #endregion
                        // 获取Excel的內容
                        tableTemp = ExcelCommon.GetTableFromExcel(strToFile, Common.UCSKey.TeacherSheetName);
                        if (tableTemp != null)
                        {
                            #region 第三：循环验证 必填项、合法性、是否重复
                            //获取Excel 到DateTable中的行数
                            if (tableTemp.Rows.Count == 0)
                            {
                                // File.Delete(fileName);  
                                tipInfo("导入信息内容不为空，请核实!");
                                return;
                            }
                            else
                            {
                                //记录当前行的索引，for 班号、年级、 姓名、性别、出生日期、学生类别、出生地、国别、政治面貌、身体状况、入学年月、是否是流动人口 为空提示

                                int currentRow = 1;
                                foreach (DataRow tr in tableTemp.Rows)
                                {
                                    //当前行的索引				
                                    currentRow = currentRow + 1;
                                    bool RowNull = true;//空行
                                    for (int i = 0; i < 8; i++)//一共有8列数据
                                    {
                                        if (!string.IsNullOrEmpty(tr[i].ToString()))
                                        {
                                            //如果有值就更改RowNull为false
                                            RowNull = false;
                                        }
                                    }
                                    if (RowNull)
                                    {
                                        //如果8列数据都为空，则下一循环
                                        continue;
                                    }
                                    //验证姓名不为空
                                    if (tr[0].ToString() == "")
                                    {
                                        wrongSum += "第[" + currentRow + "]行,第[1]列(姓名)不能为空，请核实！<br/>";
                                    }

                                    //身份证号
                                    if (tr[1].ToString().Trim() == "")
                                    {
                                        wrongSum += "第[" + currentRow + "]行,第[2]列(证件号)不能为空，请核实！<br/>";
                                    }
                                    else if (!DAL.ValidatorHelper.CheckIDCard(tr[1].ToString().Trim()))
                                    {
                                        //记录错误的身份证号	
                                        wrongSum += "第[" + currentRow + "]行,第[2]列(证件号)输入有误，请核实！<br/>";
                                    }
                                        
                                    //验证性别
                                    if (tr[2].ToString() == "")
                                    {
                                        wrongSum += "第[" + currentRow + "]行,第[3]列(性别)不能为空，请核实！<br/>";
                                    }
                                    else if (tr[2].ToString() != "男" && tr[2].ToString() != "女")
                                    {
                                        wrongSum += "第[" + currentRow + "]行,第[3]列(性别)输入有误，请核实！<br/>";
                                    }
                                        
                                    #region MyRegion
                                    
                                    //if (!(tr[1].ToString() == "" && tr[2].ToString() == "" && tr[3].ToString() == ""))
                                    //{
                                    //    //验证姓名不为空
                                    //    if (tr[1].ToString() == "")
                                    //        wrongSum += "第[" + currentRow + "]行,第[2]列(姓名)不能为空，请核实！<br/>";
                                    //    //验证性别
                                    //    if (tr[3].ToString() != "")
                                    //    {
                                    //        if (tr[3].ToString() != "男" && tr[3].ToString() != "女")
                                    //            wrongSum += "第[" + currentRow + "]行,第[4]列(性别)输入有误，请核实！<br/>";
                                    //    }
                                    //    //身份证号
                                    //    if (tr[2].ToString().Trim() == "")
                                    //        wrongSum += "第[" + currentRow + "]行,第[3]列(证件号)不能为空，请核实！<br/>";
                                    //    else if (!DAL.ValidatorHelper.CheckIDCard(tr[2].ToString().Trim()))
                                    //        //记录错误的身份证号	
                                    //        wrongSum += "第[" + currentRow + "]行,第[3]列(证件号)输入有误，请核实！<br/>";
                                    //    //工资等级
                                    //    if (tr[17].ToString().Trim().Length > 10)
                                    //        wrongSum += "第[" + currentRow + "]行,第[19]列(工资等级)，字符长度不能超过10位<br/>";
                                    //    //教师资格证类别
                                    //    if (tr[24].ToString().Trim().Length > 10)
                                    //        wrongSum += "第[" + currentRow + "]行,第[25]列(教师资格证类别)，字符长度不能超过10位<br/>";
                                    //    //出生年月
                                    //    if (string.IsNullOrWhiteSpace(tr[4].ToString().Trim()))
                                    //        tr[4] = DBNull.Value;
                                    //    else if (!IsDate(tr[4].ToString().Trim()))
                                    //    {
                                    //        if (tr[4].ToString().Trim().Length <= 4)
                                    //            wrongSum += "第[" + currentRow + "]行,第[5]列(出生年月)，必须为日期格式，如：2015/1/1<br/>";
                                    //        else
                                    //            tr[4] = HandlerLogic.HandleDate(tr[4].ToString().Trim());
                                    //    }
                                    //    if (string.IsNullOrWhiteSpace(tr[7].ToString().Trim()))
                                    //        tr[7] = DBNull.Value;
                                    //    else if (!IsDate(tr[7].ToString().Trim()))
                                    //    {
                                    //        if (tr[7].ToString().Trim().Length <= 4)
                                    //            wrongSum += "第[" + currentRow + "]行,第[8]列(参加工作时间)，必须为日期格式，如：2015/1/1<br/>";
                                    //        else
                                    //            tr[7] = HandlerLogic.HandleDate(tr[7].ToString().Trim());
                                    //    }
                                    //    if (string.IsNullOrWhiteSpace(tr[9].ToString().Trim()))
                                    //        tr[9] = DBNull.Value;
                                    //    else if (!IsDate(tr[9].ToString().Trim()))
                                    //    {
                                    //        if (tr[9].ToString().Trim().Length <= 4)
                                    //            wrongSum += "第[" + currentRow + "]行,第[10]列(毕业时间)，必须为日期格式，如：2015/1/1<br/>";
                                    //        else
                                    //            tr[9] = HandlerLogic.HandleDate(tr[9].ToString().Trim());
                                    //    }
                                    //    if (string.IsNullOrWhiteSpace(tr[15].ToString().Trim()))
                                    //        tr[15] = DBNull.Value;
                                    //    else if (!IsDate(tr[15].ToString().Trim()))
                                    //    {
                                    //        if (tr[15].ToString().Trim().Length <= 4)
                                    //            wrongSum += "第[" + currentRow + "]行,第[16]列(评定时间)，必须为日期格式，如：2015/1/1<br/>";
                                    //        else
                                    //            tr[15] = HandlerLogic.HandleDate(tr[15].ToString().Trim());
                                    //    }
                                    //    if (string.IsNullOrWhiteSpace(tr[27].ToString().Trim()))
                                    //        tr[27] = DBNull.Value;
                                    //    else if (!IsDate(tr[27].ToString().Trim()))
                                    //    {
                                    //        if (tr[27].ToString().Trim().Length <= 4)
                                    //            wrongSum += "第[" + currentRow + "]行,第[28]列(毕业时间)，必须为日期格式，如：2015/1/1<br/>";
                                    //        else
                                    //            tr[27] = HandlerLogic.HandleDate(tr[27].ToString().Trim());
                                    //    }
                                    //    if (string.IsNullOrWhiteSpace(tr[31].ToString().Trim()))
                                    //        tr[31] = DBNull.Value;
                                    //    else if (!IsDate(tr[31].ToString().Trim()))
                                    //    {
                                    //        if (tr[31].ToString().Trim().Length <= 4)
                                    //            wrongSum += "第[" + currentRow + "]行,第[32]列(毕业时间)，必须为日期格式，如：2015/1/1<br/>";
                                    //        else
                                    //            tr[31] = HandlerLogic.HandleDate(tr[31].ToString().Trim());
                                    //    }
                                    //    if (string.IsNullOrWhiteSpace(tr[34].ToString().Trim()))
                                    //        tr[34] = DBNull.Value;
                                    //    else if (!IsDate(tr[34].ToString().Trim()))
                                    //    {
                                    //        if (tr[34].ToString().Trim().Length <= 4)
                                    //            wrongSum += "第[" + currentRow + "]行,第[35]列(结业时间)，必须为日期格式，如：2015/1/1<br/>";
                                    //        else
                                    //            tr[34] = HandlerLogic.HandleDate(tr[34].ToString().Trim());
                                    //    }
                                    //}

                                    #endregion
                                }
                            }
                            //记入操作日志
                            Base_LogBLL.WriteLog(LogConstants.jsxxgl, ActionConstants.dr);
                            #endregion
                            #region 第四 身份证号验证
                            // 获取重复的考号和身份证号，输入提示信息
                            DataRow tr1;
                            DataRow tr2;

                            for (int i = 0; i < tableTemp.Rows.Count; i++)
                            {
                                tr2 = tableTemp.Rows[i];
                                if (tr2[2].ToString() != "")
                                {
                                    for (int j = i + 1; j < tableTemp.Rows.Count; j++)
                                    {
                                        tr1 = tableTemp.Rows[j];
                                        string aa = tr2["身份证号码"].ToString();
                                        string bb = tr1["身份证号码"].ToString();

                                        if (tr2["身份证号码"].ToString() != "" && tr1["身份证号码"].ToString() != "")
                                        {
                                            if (tr2["身份证号码"].ToString() == tr1["身份证号码"].ToString())
                                            {
                                                wrongSum += "第[" + (j + 2).ToString() + "]行身份证号码与第[" + (i + 2).ToString() + "]重复，请核实！<br/>";
                                            }
                                        }
                                    }
                                }
                            }

                            //输入页面到页面DIV中的提示信息			
                            if (!string.IsNullOrEmpty(wrongSum))
                            {
                                // 刪除服务器上的文件
                                File.Delete(strToFile);
                                tipInfo(wrongSum);
                                return;
                            }
                            #endregion
                            #region   第五 循环存储DB
                            int sum = 0;
                            if (tableTemp != null)
                            {
                                Base_TeacherBLL TeaBLL = new Base_TeacherBLL();
                                foreach (DataRow dr in tableTemp.Rows)
                                {
                                    bool RowNull = true;//空行
                                    for (int i = 0; i < 8; i++)//一共有8列数据
                                    {
                                        if (!string.IsNullOrEmpty(dr[i].ToString()))
                                        {
                                            //如果有值就更改RowNull为false
                                            RowNull = false;
                                        }
                                    }
                                    if (RowNull)
                                    {
                                        //如果8列数据都为空，则下一循环
                                        continue;
                                    }
                                    Base_Teacher Tea = new Base_Teacher();
                                    Tea.XM = dr[0].ToString().Trim();//姓名
                                    Tea.SFZJH = dr[1].ToString().Trim();//证件号
                                    Tea.XBM = dr[2].ToString().Trim();//性别

                                    Tea.MZM = dr[3].ToString().Trim();//民族
                                    Tea.JG = dr[4].ToString().Trim();//籍贯
                                    Tea.XZZ = dr[5].ToString().Trim();//现住址
                                    Tea.LXDH = dr[6].ToString().Trim();//联系电话
                                    Tea.JTDH = dr[7].ToString().Trim();//家庭电话
                                    
                                    if (TeaBLL.InsertExcel(Tea))
                                    {
                                        sum++;
                                    }
                                    #region MyRegion
                                    
                                    //if (!string.IsNullOrWhiteSpace(dr[0].ToString()) && dr[0].ToString() != "学校组织机构号")
                                    //{
                                    //    string ErrMessage = "";
                                    //    try
                                    //    {
                                    //        //Base_Teacher Tea = new Base_Teacher();
                                    //        //Base_TeacherSpouse Tsp = new Base_TeacherSpouse();  //配偶情况
                                    //        //Base_StudyCareer SC = new Base_StudyCareer();       //现学历
                                    //        //Base_StudyCareer YSC = new Base_StudyCareer();      //原学历
                                    //        //Base_StudyCareer XJXSC = new Base_StudyCareer();    //先进修学历
                                    //        //Base_StudyCareer YJSSC = new Base_StudyCareer();    //研究生学习班
                                    //        //Base_DepartmentBLL deptBll = new Base_DepartmentBLL();
                                    //        //if (string.IsNullOrWhiteSpace(XXZZJGH))
                                    //        //{
                                    //        //    XXZZJGH = deptBll.SelectXXZZJGHByJGMC(dr[0].ToString());
                                    //        //}
                                    //        if (!string.IsNullOrWhiteSpace(XXZZJGH))
                                    //        {
                                    //            Tea.XXZZJGH = XXZZJGH;
                                    //            Tea.XM = dr[1].ToString();
                                    //            Tea.SFZJH = dr[2].ToString().Trim();
                                    //            Tea.XBM = dr[3].ToString();
                                    //            if (dr[4] != DBNull.Value)
                                    //            {
                                    //                Tea.CSRQ = DataString(dr[4].ToString());
                                    //                Tea.AGE = CalculateAge(DateTime.Now, Tea.CSRQ);
                                    //            }
                                    //            else
                                    //            {
                                    //                Tea.CSRQ = new DateTime(1000, 1, 1);
                                    //            }

                                    //            Tea.MZM = dr[5].ToString();
                                    //            Tea.ZZMMM = dr[6].ToString();
                                    //            if (dr[7] != DBNull.Value)
                                    //            {
                                    //                Tea.CJGZSJ = DataString(dr[7].ToString());
                                    //            }
                                    //            else
                                    //            {
                                    //                Tea.CJGZSJ = new DateTime(1000, 1, 1);
                                    //            }

                                    //            ////判断数据是否存在
                                    //            //if (dr[9] != DBNull.Value && dr[10] != DBNull.Value && dr[11] != DBNull.Value)
                                    //            //{
                                    //            //    SC.XXZZRQ = DataString(dr[9].ToString());// Convert.ToDateTime(dr[9]);
                                    //            //    SC.XXDW = dr[10].ToString();
                                    //            //    SC.SXZYMC = dr[11].ToString();
                                    //            //    SC.SFZJH = Tea.SFZJH;
                                    //            //    SC.XLLX = Convert.ToInt16(UCSKey.XXLX.XXL).ToString();
                                    //            //    SC.CC = dr[8].ToString();
                                    //            //}
                                    //            //else
                                    //            //{
                                    //            //    SC.XXZZRQ = new DateTime(1000, 1, 1);
                                    //            //}
                                    //            Tea.XL = dr[12].ToString();
                                    //            Tea.ZC = dr[13].ToString();
                                    //            Tea.JB = dr[14].ToString();
                                    //            if (dr[15] != DBNull.Value)
                                    //            {
                                    //                Tea.PDSJ = DataString(dr[15].ToString());//Convert.ToDateTime(dr[15]);
                                    //            }
                                    //            else
                                    //            {
                                    //                Tea.PDSJ = new DateTime(1000, 1, 1);
                                    //            }

                                    //            Tea.GZXL = dr[16].ToString();
                                    //            Tea.DJ = dr[17].ToString();
                                    //            Tea.LB = dr[18].ToString();
                                    //            Tea.ZYRKXD = dr[19].ToString();
                                    //            Tea.XJTGW = dr[20].ToString();
                                    //            Tea.JKHJZ = dr[21].ToString();
                                    //            Tea.ZZKSH = dr[22].ToString();
                                    //            Tea.GGLB = dr[23].ToString();
                                    //            Tea.JSZKZLB = dr[24].ToString();
                                    //            Tea.SF = dr[25].ToString();

                                    //            //if (dr[27] != DBNull.Value && dr[28] != DBNull.Value && dr[29] != DBNull.Value)
                                    //            //{
                                    //            //    XJXSC.XXZZRQ = DataString(dr[27].ToString());// Convert.ToDateTime(dr[27]);
                                    //            //    XJXSC.XXDW = dr[28].ToString();
                                    //            //    XJXSC.SXZYMC = dr[29].ToString();
                                    //            //    XJXSC.SFZJH = Tea.SFZJH;
                                    //            //    XJXSC.XLLX = Convert.ToInt16(UCSKey.XXLX.YXL).ToString();
                                    //            //    XJXSC.CC = dr[26].ToString();

                                    //            //}

                                    //            //if (dr[31] != DBNull.Value && dr[32] != DBNull.Value && dr[33] != DBNull.Value)
                                    //            //{
                                    //            //    YSC.XXZZRQ = DataString(dr[31].ToString());// Convert.ToDateTime(dr[31]);
                                    //            //    YSC.CC = dr[30].ToString();
                                    //            //    YSC.XXDW = dr[32].ToString();
                                    //            //    YSC.SXZYMC = dr[33].ToString();
                                    //            //    YSC.SFZJH = Tea.SFZJH;
                                    //            //    YSC.XLLX = Convert.ToInt16(UCSKey.XXLX.XJXXL).ToString();
                                    //            //}

                                    //            //if (dr[34] != DBNull.Value && dr[35] != DBNull.Value && dr[36] != DBNull.Value)
                                    //            //{
                                    //            //    YJSSC.XXZZRQ = DataString(dr[34].ToString()); Convert.ToDateTime(dr[34]);
                                    //            //    YJSSC.XXDW = dr[35].ToString();
                                    //            //    YJSSC.SXZYMC = dr[36].ToString();
                                    //            //    YJSSC.SFZJH = Tea.SFZJH;
                                    //            //    YJSSC.XLLX = Convert.ToInt16(UCSKey.XXLX.YJSKC).ToString();
                                    //            //}

                                    //            Tea.JG = dr[37].ToString();
                                    //            Tea.XZZ = dr[38].ToString();
                                    //            Tea.LXDH = dr[39].ToString();
                                    //            Tea.JTDH = dr[40].ToString();

                                    //            ////如果有配偶信息则插入
                                    //            //if (!string.IsNullOrWhiteSpace(dr[37].ToString()))
                                    //            //{
                                    //            //    Tsp.POXM = dr[41].ToString();
                                    //            //    Tsp.POGZDW = dr[42].ToString();
                                    //            //    Tsp.SFZJH = Tea.SFZJH;
                                    //            //}
                                    //            if (tableTemp.Columns.Contains("备注"))//如果包含组织机构号的话
                                    //            {
                                    //                Tea.BZ = dr[43].ToString();
                                    //            }
                                    //            Tea.YHZT = "禁用";
                                    //            if (tableTemp.Columns.Contains("组织机构号"))//如果包含组织机构号的话
                                    //            {
                                    //                if (!string.IsNullOrWhiteSpace(dr[44].ToString()))//组织机构号
                                    //                {
                                    //                    Tea.ZZJGH = dr[44].ToString();
                                    //                }
                                    //            }
                                    //            if (tableTemp.Columns.Contains("任课年级"))//如果包含组织机构号的话
                                    //            {
                                    //                if (!string.IsNullOrWhiteSpace(dr[45].ToString()))//班主任  年级
                                    //                {
                                    //                    Tea.NJ = dr[46].ToString();
                                    //                }
                                    //            }
                                    //            if (tableTemp.Columns.Contains("任课班级"))//如果包含组织机构号的话
                                    //            {
                                    //                if (!string.IsNullOrWhiteSpace(dr[46].ToString()))//班主任  班级
                                    //                {
                                    //                    Tea.BH = dr[47].ToString();
                                    //                }
                                    //            }
                                    //            if (Tea != null && !string.IsNullOrWhiteSpace(Tea.SFZJH))
                                    //            {
                                    //                //Insert(Tea, SC, YSC, XJXSC, YJSSC, Tsp);
                                    //                sum++;
                                    //            }
                                    //        }
                                    //        else
                                    //        {
                                    //            tipInfo("学校名称错误");
                                    //            return;
                                    //        }
                                    //    }
                                    //    catch (Exception ex)
                                    //    {
                                    //        tipInfo("导入时发生异常，导入失败！先确认选择的是否是教师信息模板，然后对比模板看是否缺少必要的列.");
                                    //        LogCommon.writeLogUserCenter(ErrMessage + ex.Message, ex.StackTrace);
                                    //    }
                                    //}

                                    #endregion
                                }
                                if (sum > 0)
                                {
                                    // Bind(""); 
                                    tipInfo("成功导入" + sum + "条数据！", true);
                                    return;
                                }
                                else
                                {
                                    // 刪除服务器上的文件
                                    File.Delete(strToFile);
                                    tipInfo("导入失败，excel文件内容存在无效信息！");
                                    return;
                                }
                            }
                            else
                            {
                                // 刪除服务器上的文件
                                File.Delete(strToFile);
                                tipInfo("Excel文件中sheet命名不正确！");
                                return;
                            }
                            #endregion
                        }
                        else
                        {
                            // 刪除服务器上的文件
                            File.Delete(strToFile);
                            tipInfo("模板选择有误，请确认是否是" + Common.UCSKey.TeacherSheetName + "模板，如果是，请修改Sheet名称为【" + Common.UCSKey.TeacherSheetName + "】，然后再导入。");
                            return;
                        }
                    }
                    catch (Exception ex)
                    {
                        // 刪除服务器上的文件
                        File.Delete(strToFile);
                        tipInfo("导入时发生异常，导入失败！先确认选择的是否是教师信息模板，然后对比模板看是否缺少必要的列.");
                        LogCommon.writeLogUserCenter(ex.Message, ex.StackTrace);
                    }
                }
                else
                {
                    tipInfo("只能导入Excel格式的文件，[格式提示：xls或xlsx]");
                    return;
                }
            }
            else
            { tipInfo("请添加要上传的文件"); return; }
        }
        public void tipInfo(string info)
        {
            Div1.InnerHtml = "";
            // showError.Visible = true;
            Div1.Style.Add("color", "red");
            Div1.InnerHtml = info;
        }
        public void tipInfo(string info, bool istrue)
        {
            Div1.InnerHtml = "";
            // showError.Visible = true;
            Div1.Style.Add("color", "green");
            Div1.InnerHtml = info;
        }
        /// <summary>
        /// 判断是否是日期格式
        /// </summary>
        /// <param name="strDate"></param>
        /// <returns></returns>
        public bool IsDate(string strDate)
        {
            try
            {
                DateTime.Parse(strDate);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public DateTime DataString(string Datatime)
        {
            DateTime time = new DateTime(1000, 1, 1);
            System.Text.RegularExpressions.Regex reg = new System.Text.RegularExpressions.Regex(@"^[0-9]*$");
            if (reg.IsMatch(Datatime))
            {
                time = Convert.ToDateTime(Datatime.Substring(0, 4) + "-" + Datatime.Substring(4, 2));
            }
            else
            {
                time = Convert.ToDateTime(Datatime);
            }
            return time;
        }
        public void Insert(Base_Teacher Tea, Base_StudyCareer XSC, Base_StudyCareer YSC, Base_StudyCareer XJXSC, Base_StudyCareer YJSSC, Base_TeacherSpouse TS)
        {
            Base_TeacherBLL TeaBLL = new Base_TeacherBLL();
            Base_StudyCareerBLL StuCaBLL = new Base_StudyCareerBLL();
            Base_TeacherSpouseBLL TeaSpBLL = new Base_TeacherSpouseBLL();

            //验证信息是否已经存在
            //if (TeaBLL.CheckUserISExist(Tea.SFZJH))
            //{
            TeaBLL.InsertExcel(Tea);
            //}
            //else
            //{
            //    TeaBLL.Update(Tea);
            // }
            //if (TeaSpBLL.CheckUserISExist(TS.SFZJH))
            //{
            TeaSpBLL.Insert(TS);
            //}
            //else
            //{
            //    TeaSpBLL.Update(TS);
            //}
            if (XSC != null && !string.IsNullOrWhiteSpace(XSC.SFZJH))
            {
                StuCaBLL.Insert(XSC);
            }
            if (YSC != null && !string.IsNullOrWhiteSpace(YSC.SFZJH))
            {
                StuCaBLL.Insert(YSC);
            }
            if (XJXSC != null && !string.IsNullOrWhiteSpace(XJXSC.SFZJH))
            {
                StuCaBLL.Insert(XJXSC);
            }
            if (YJSSC != null && !string.IsNullOrWhiteSpace(YJSSC.SFZJH))
            {
                StuCaBLL.Insert(YJSSC);
            }
        }
        /// <summary>
        /// 根据出生日期计算年龄
        /// </summary>
        /// <param name="NowTime"></param>
        /// <param name="BirthDate"></param>
        /// <returns></returns>
        public int CalculateAge(DateTime NowTime, DateTime BirthDate)
        {
            DateTime Age = new DateTime((NowTime - BirthDate).Ticks);
            return Age.Year;
        }
        /// <summary>
        /// 下载模板
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void downmoban_Click(object sender, EventArgs e)
        {
            //记入操作日志
            Base_LogBLL.WriteLog(LogConstants.jsxxgl, ActionConstants.xzmb);
            string fileName = "教师信息模板.xls";//客户端保存的文件名
            string filePath = Server.MapPath("Template/老师信息模板.xls");//路径

            FileInfo fileInfo = new FileInfo(filePath);
            Response.Clear();
            Response.ClearContent();
            Response.ClearHeaders();
            Response.AddHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(fileName, Encoding.UTF8).ToString());
            Response.AddHeader("Content-Length", fileInfo.Length.ToString());
            Response.AddHeader("Content-Transfer-Encoding", "binary");
            Response.ContentType = "application/octet-stream";
            Response.ContentEncoding = Encoding.Default;//System.Text.Encoding.GetEncoding("GBK");
            Response.WriteFile(fileInfo.FullName);
            Response.Flush();
            Response.End();
        }


    }
}