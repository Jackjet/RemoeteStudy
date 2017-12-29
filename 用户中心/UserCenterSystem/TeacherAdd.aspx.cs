using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Model;
using BLL;
using Common;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using DAL;

namespace UserCenterSystem
{
    public partial class TeacherAdd : BaseInfo
    {
        /// <summary>
        /// 存储URL传参的教师身份证号
        /// </summary>
        public string IdCard
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(Request.QueryString["IdCard"]))
                {
                    return Request.QueryString["IdCard"].ToString();
                }
                return "";
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.MaintainScrollPositionOnPostBack = true;
            if (!IsPostBack)
            {
                
                //Base_Teacher teacher = Session[UCSKey.SESSION_LoginInfo] as Base_Teacher;  //获取用户信息
                //if (teacher != null)
                //{
                //    if (teacher.XM == "超级管理员")
                //    {
                //        //ddlXX.Enabled = true;
                //        //txtSFZJH.Enabled = true;
                //    }

                //    BindScholl();//绑定学校下拉列表
                //    BindGrade();//绑定专业下拉列表
                //    BindClass();//绑定班级下拉列表

                //    if (!string.IsNullOrWhiteSpace(Request.QueryString["XXZZJGH"]))
                //    {
                //        //BindDDL(ddlXX, Request.QueryString["XXZZJGH"].ToString());
                //    }
                //    else if (!string.IsNullOrWhiteSpace(Request.QueryString["IdCard"]))
                //    {
                //        //Request.QueryString["IdCard"].ToString();
                //    }
                //    else
                //    {
                //        //ddlXX.Enabled = true;
                //        //txtSFZJH.Enabled = true;
                //    }

                //    StudentOldIdCard.Value = IdCard;//记录教师ID

                //    BindTeacherInfo(IdCard);//加载教师信息

                //    //BindDedpt(ddlXX.SelectedValue.ToString());
                //}
                //BindScholl();//绑定学校下拉列表
                //BindGrade();//绑定专业下拉列表
                //BindClass();//绑定班级下拉列表
                StudentOldIdCard.Value = IdCard;//记录教师ID

                BindTeacherInfo(IdCard);//加载教师信息
            }
        }
        /// <summary>
        /// 【Function】绑定学校
        /// </summary>
        public void BindScholl()
        {
            //Base_Teacher teacher = Session[UCSKey.SESSION_LoginInfo] as Base_Teacher;
            //Base_DepartmentBLL deptBll = new Base_DepartmentBLL();
            ////DataTable dt = deptBll.SelectXJByLoginName(teacher);
            //DataTable dt = deptBll.SelectDeptDtByLSJGH("0");
            //ddlXX.DataSource = dt;
            //ddlXX.DataTextField = "JGMC";
            //ddlXX.DataValueField = "XXZZJGH";
            //ddlXX.DataBind();
        }
        /// <summary>
        /// 【Function】绑定学科
        /// </summary>
        private void BindGrade()
        {
            try
            {
                //Base_GradeBLL gradeBll = new Base_GradeBLL();
                //string strJGH = string.Empty;
                //strJGH = ddlXX.SelectedValue;
                ////DataTable grade = gradeBll.SelectGradeByJGH(strJGH);//年级+学科
                //DataTable grade = new DataTable();
                //grade = gradeBll.SelectAllGradeInfo();
                ////lvStu.DataSource = grade;
                ////lvStu.DataBind();

                ////绑定年级
                //Drp_Grade.Items.Clear();
                //Drp_Grade.DataTextField = "NJMC";
                //Drp_Grade.DataValueField = "NJ";
                //Drp_Grade.DataSource = grade;
                //Drp_Grade.DataBind();
            }
            catch (Exception ex)
            {
                LogCommon.writeLogUserCenter(ex.Message, ex.StackTrace);
            }
        }
        /// <summary>
        /// 绑定班级
        /// </summary>
        private void BindClass()
        {
            try
            {
                //Base_ClassBLL ClassBll = new Base_ClassBLL();
                //DataTable dt = new DataTable();
                //dt = ClassBll.GetClassByGradeID(Drp_Grade.SelectedValue);
                //Drp_Class.Items.Clear();
                //Drp_Class.DataTextField = "BJ";
                //Drp_Class.DataValueField = "BJBH";
                //Drp_Class.DataSource = dt;
                //Drp_Class.DataBind();
            }
            catch (Exception ex)
            {
                LogCommon.writeLogUserCenter(ex.Message, ex.StackTrace);
            }
        }
        /// <summary>
        /// 教师信息
        /// </summary>
        /// <param name="IdCard">身份证号</param>
        public void BindTeacherInfo(string IdCard)
        {
            try
            {
                //Base_StudyCareerBLL stuCaBLL = new Base_StudyCareerBLL();
                Base_TeacherBLL TeaBLL = new Base_TeacherBLL();
                //Base_TeacherSpouseBLL TeaSPBLL = new Base_TeacherSpouseBLL();

                Base_Teacher Tea = new Base_Teacher();
                //Base_StudyCareer stucaXXL = new Base_StudyCareer();
                //Base_StudyCareer stucaYXL = new Base_StudyCareer();
                //Base_StudyCareer stucaJXXL = new Base_StudyCareer();
                //Base_StudyCareer stucaYJS = new Base_StudyCareer();
                //Base_TeacherSpouse TeaS = new Base_TeacherSpouse();

                Tea = TeaBLL.GetTeacherBySFZJH(IdCard);
                if (Tea == null)
                {
                    //BindGrade();
                    //ListItem li = new ListItem("--请选择--", "");
                    //ddlZZ.Items.Insert(0, li);
                    //Drp_Grade.Items.Insert(0, li);
                    //Drp_Class.Items.Insert(0, li);
                    //SubjectSelected("");
                }
                //教师数据
                else if (Tea != null)
                {
                    //BindGrade();
                    FillTeacher(Tea);//加载教师信息
                    //if (!string.IsNullOrWhiteSpace(Tea.GradeID))
                    //    SubjectSelected(Tea.GradeID);
                    //else
                    //    SubjectSelected("");


                    //Drp_Grade.SelectedValue = Tea.NJ;
                    //Drp_Class.SelectedValue = Tea.BH;
                    //ddlXX.SelectedValue = Tea.XXZZJGH;

                    //TeaS = TeaSPBLL.GetTeacherSpouseBySFZJH(IdCard);
                    //配偶数据
                    //if (TeaS != null)
                    //{
                    //    txtPOXM.Text = TeaS.POXM;
                    //    txtPOGZDW.Text = TeaS.POGZDW;
                    //}
                    //stucaXXL = stuCaBLL.GetStudyCareerBySFZJH(IdCard, Convert.ToInt16(UCSKey.XXLX.XXL));
                    //现学历
                    //if (stucaXXL != null)
                    //{
                    //    txtXXLCC.Text = stucaXXL.CC;
                    //    txtXXLZY.Text = stucaXXL.SXZYMC;
                    //    txtXXLBYYX.Text = stucaXXL.XXDW;
                    //    txtXXLBYSJ.Text = ProcessDate(stucaXXL.XXZZRQ);
                    //}
                    //stucaYXL = stuCaBLL.GetStudyCareerBySFZJH(IdCard, Convert.ToInt16(UCSKey.XXLX.YXL));
                    //原学历
                    //if (stucaYXL != null)
                    //{
                    //    txtYXLCC.Text = stucaYXL.CC;
                    //    txtYXLZY.Text = stucaYXL.SXZYMC;
                    //    txtYXLBYYX.Text = stucaYXL.XXDW;
                    //    txtYXLBYSJ.Text = ProcessDate(stucaYXL.XXZZRQ);
                    //}
                    //stucaJXXL = stuCaBLL.GetStudyCareerBySFZJH(IdCard, Convert.ToInt16(UCSKey.XXLX.XJXXL));
                    //现进修学历
                    //if (stucaJXXL != null)
                    //{
                    //    txtXJXCC.Text = stucaJXXL.CC;
                    //    txtXJXZY.Text = stucaJXXL.SXZYMC;
                    //    txtXJXBYYX.Text = stucaJXXL.XXDW;
                    //    txtXJXBYSJ.Text = ProcessDate(stucaJXXL.XXZZRQ);
                    //}
                    //stucaYJS = stuCaBLL.GetStudyCareerBySFZJH(IdCard, Convert.ToInt16(UCSKey.XXLX.YJSKC));
                    //研究生课程
                    //if (stucaYJS != null)
                    //{
                    //    txtYJSBCC.Text = stucaYJS.CC;
                    //    txtYJSBZY.Text = stucaYJS.SXZYMC;
                    //    txtYJSBBYYX.Text = stucaYJS.XXDW;
                    //    txtYJSBBYSJ.Text = ProcessDate(stucaYJS.XXZZRQ);
                    //}
                    //所负责班级
                    //Drp_Grade.SelectedIndex = Drp_Grade.Items.IndexOf(Drp_Grade.Items.FindByValue(Tea.NJ));
                    //Drp_Class.SelectedIndex = Drp_Class.Items.IndexOf(Drp_Class.Items.FindByValue(Tea.BH));
                    
                }
                else
                {
                    //ListItem li = new ListItem("--请选择--", "");
                    //ddlZZ.Items.Insert(0, li);
                    //Drp_Grade.Items.Insert(0, li);
                    //Drp_Class.Items.Insert(0, li);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        /// <summary>
        /// 【Function】绑定组织机构
        /// </summary>
        public void BindDedpt(string XXZZJGH)
        {
            //Base_DepartmentBLL deptBll = new Base_DepartmentBLL();
            //DataTable dt = deptBll.GetDeptBLL(XXZZJGH);
            //ddlZZ.DataSource = dt;
            //ddlZZ.DataTextField = "JGMC";
            //ddlZZ.DataValueField = "XXZZJGH";
            //ddlZZ.DataBind();
        }

        
        /// <summary>
        /// 绑定下拉框值
        /// </summary>
        public void BindDDL(DropDownList ddl, string SelectValue)
        {
            //if (ddl.Items.FindByValue(SelectValue) != null)
            //{
            //    ListItem li = new ListItem("--请选择--", "");
            //    ddl.Items.Insert(0, li);
            //    ddl.ClearSelection();
            //    ddl.Items.FindByValue(SelectValue).Selected = true;
            //}
            //else
            //{
            //    ListItem li = new ListItem("--请选择--", "");
            //    ddl.Items.Insert(0, li);
            //}
        }
        /// <summary>
        /// 绑定下拉框值
        /// </summary>
        public void BindXXZZJGH(DropDownList ddl, string SelectValue)
        {
            if (ddl.Items.FindByValue(SelectValue) != null)
            {
                ddl.ClearSelection();
                ddl.Items.FindByValue(SelectValue).Selected = true;
            }
        }
        ///// <summary>
        ///// 处理日期
        ///// </summary>
        //public string ProcessDate(DateTime? DT)
        //{
        //    if (DT.Year > 1900)
        //    {
        //        return DT.ToShortDateString();
        //    }
        //    return "";
        //}

        /// <summary>
        /// 填充教师相关信息
        /// </summary>
        private void FillTeacher(Base_Teacher Tea)
        {
            try
            {
                //BindXXZZJGH(ddlXX, Tea.XXZZJGH);
                //BindDDL(ddlZZ, Tea.ZZJGH);
                //BindDDL(Drp_Grade, Tea.NJ);

                Base_ClassBLL BC = new Base_ClassBLL();
                //DataSet ds = BC.GetClassBLL(Drp_Grade.SelectedValue.ToString(), ddlXX.SelectedValue.ToString());
                //if (ds != null && ds.Tables[0].Rows.Count > 0)
                //{
                //    Drp_Class.Items.Clear();
                //    Drp_Class.DataTextField = "BJ";
                //    Drp_Class.DataValueField = "BJBH";
                //    Drp_Class.DataSource = ds.Tables[0];
                //    Drp_Class.DataBind();
                //}
                //BindDDL(Drp_Class, Tea.BH);

                //------------------------------
                txtXM.Text = Tea.XM;//姓名
                txtSFZJH.Text = Tea.SFZJH;//身份证件号
                Hid_SFZJH.Value = Tea.SFZJH;//Hid身份证号  2014-12-26
                rblXB.SelectedValue = Tea.XBM;//性别码
                txtCSNY.Text = string.Format("{0:yyyy\\-MM\\-dd }", Tea.CSRQ);//出生日期
                txtMZ.Text = Tea.MZM;//民族码
                if (!string.IsNullOrEmpty(Tea.ZZMMM))
                {
                    ddZZMM.Items.FindByValue(Tea.ZZMMM).Selected = true;//政治面貌码
                }
                //txtCJGZSJ.Text = ProcessDate(Tea.CJGZSJ);
                //txtXL.Text = Tea.XL;//系列
                //txtZC.Text = Tea.ZC;//职称
                //txtJB.Text = Tea.JB;//级别
                //txtPDSJ.Text = ProcessDate(Tea.PDSJ);//评定时间
                //txtdl.Text = Tea.GZXL;//工资序列
                //txtDJ.Text = Tea.DJ;//等级
                //txtLB.Text = Tea.LB;//类别
                //txtZYRKXD.Text = Tea.ZYRKXD;//主要任课学段
                //txtXJTGW.Text = Tea.XJTGW;//现具体岗位
                //txtJKJZ.Text = Tea.JKHJZ;//兼课或兼职
                //txtZZKSS.Text = Tea.ZZKSH;//周总课时数
                //ddlGGLB.SelectedValue = Tea.GGLB;//骨干类别
                txtJSZGZ.Text = Tea.JSZKZLB;//教师资格证类别
                txtSF.Text = Tea.SF;//身份
                this.txtBZ.Text = Tea.BZ;//备注
                //Tea.YHZT = "禁用";//用户状态
                txtJG.Text = Tea.JG;//机构
                txtXZZ.Text = Tea.XZZ;//现住址
                txtSJH.Text = Tea.LXDH;//联系电话
                txtJTDH.Text = Tea.JTDH;//家庭电话
                //txtYWXM.Text = Tea.YWXM;//英文姓名
                //txtXMPY.Text = Tea.XMPY;//姓名拼音
                //txtCYM.Text = Tea.CYM;//曾用名
                if (!string.IsNullOrEmpty(Tea.SFZJLXM))
                {
                    ddSFZJLX.Items.FindByValue(Tea.SFZJLXM).Selected = true;//身份证码
                }
                //txtCSDM.Text = Tea.CSDM;//出生地码
                //if (!string.IsNullOrEmpty(Tea.HYZKM))
                //{
                //    rbHYZKM.Items.FindByValue(Tea.HYZKM).Selected = true;//婚姻状况码
                //}
                if (!string.IsNullOrEmpty(Tea.JKZKM))
                {
                    rblJKZKM.Items.FindByValue(Tea.JKZKM).Selected = true;//健康状况码
                }
                //txtGJDQM.Text = Tea.GJDQM;//国籍/地区码
                //txtGATQWM.Text = Tea.GATQWM;//港澳台侨外码
                //  rblJKZKM.SelectedValue = Tea.JKZKM;//健康状况码
                if (!string.IsNullOrEmpty(Tea.XXM))
                {
                    ddXXM.Items.FindByValue(Tea.XXM).Selected = true;//血型码
                }
                //txtXYZJM.Text = Tea.XYZJM;//信仰宗教码
                //  txtZP.Text=Tea.ZP;//照片
                txtSFZJYXQ.Text = Tea.SFZJYXQ;//身份证件有效期
                //txtJGH.Text = Tea.JGH;//机构号
                txtJTZZ.Text = Tea.JTZZ;//家庭住址
                txtHKSZD.Text = Tea.HKSZD;//户口所在地
                if (!string.IsNullOrEmpty(Tea.HKXZM))
                {
                    rbHKXZM.Items.FindByValue(Tea.HKXZM).Selected = true;//户口性质码
                }
                //txtCJGZSJ.Text = ProcessDate(Tea.CJGZSJ);//参加工作年月
                txtLXNY.Text = string.Format("{0:yyyy\\-MM\\-dd }", Tea.LXNY);//来校年月
                txtCJNY.Text = string.Format("{0:yyyy\\-MM\\-dd }", Tea.CJNY);//从教年月           
                txtBZLBM.Text = Tea.BZLBM;//编制类别码
                txtDABH.Text = Tea.DABH;//档案编号
                txtDAWB.Text = Tea.DAWB;//档案文本
                txtXZZ.Text = Tea.TXDZ;//通信地址
                txtYZBM.Text = Tea.YZBM;//邮政编码
                txtDZXX.Text = Tea.DZXX;//电子信箱
                txtZYDZ.Text = Tea.ZYDZ;//主页地址
                txtTC.Text = Tea.TC;//特长
                //if (!string.IsNullOrWhiteSpace(Tea.SubjectID))
                //{
                //    string[] subItems = Tea.SubjectID.Split(',');
                //    //遍历items

                //    //foreach (string item in subItems)
                //    //{
                //    //    ////如果值相等，则选中该项
                //    //    //foreach (ListItem listItem in cblSubject.Items)
                //    //    //{
                //    //    //    if (item == listItem.Value)
                //    //    //        listItem.Selected = true;
                //    //    //    else
                //    //    //        continue;
                //    //    //}
                //    //}
                //}
                //if (!string.IsNullOrWhiteSpace(Tea.GradeID))
                //{
                //    string[] GradeItems = Tea.GradeID.Split(',');
                //    //遍历items
                //    //foreach (string item in GradeItems)
                //    //{
                //    //    ////如果值相等，则选中该项
                //    //    // foreach (ListItem listItem in cblGrade.Items)
                //    //    // {
                //    //    //     if (item == listItem.Value)
                //    //    //         listItem.Selected = true;
                //    //    //     else
                //    //    //         continue;
                //    //    // }
                //    //}
                //}

                //  fuZP. = Tea.ZP;
                //绑定照片
                if (!string.IsNullOrWhiteSpace(Tea.ZP))
                {
                    string aa = Server.MapPath(Tea.ZP);
                    //string  aa = System.Web.UI.Control.ResolveUrl(Tea.ZP);
                    imghead.ImageUrl = Tea.ZP;
                    //    imghead.ImageUrl = aa;
                    imghead.Width = 100;
                    imghead.Height = 140;
                    //imgZP.ImageUrl = Server.MapPath(Tea.ZP);
                    //imgZP.Width = 100;
                    //imgZP.Height = 100;
                }
            }
            catch (Exception ex)
            {
                LogCommon.writeLogUserCenter(ex.Message, ex.StackTrace);
            }
        }
        private bool isDateTime(string time)
        {
            try
            {
                Convert.ToDateTime(time);
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        /// <summary>
        /// 【Button】【（保存）添加/修改】
        /// </summary>
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (IsValid)
            {

                try
                {
                    //Base_StudyCareerBLL stuCaBLL = new Base_StudyCareerBLL();
                    Base_TeacherBLL TeaBLL = new Base_TeacherBLL();
                    //Base_ParentBLL ParBLL = new Base_ParentBLL();
                    //Base_TeacherSpouseBLL TeaSPBLL = new Base_TeacherSpouseBLL();
                    Base_Teacher Tea = new Base_Teacher();

                    //if (StudentOldIdCard.Value != "" && txtSFZJH.Text != StudentOldIdCard.Value)
                    //{
                    //    Tea.SFZJH = StudentOldIdCard.Value;
                    //    TeaBLL.DeleteTeacherBLL(Tea);
                    //}

                    Tea=GetModel(txtSFZJH.Text);
                    //Tea.XXZZJGH = ddlXX.SelectedValue;//学校组织机构号
                    Tea.XM = txtXM.Text.Trim();//姓名
                    //Tea.SFZJH = txtSFZJH.Text.Trim();//身份证件号
                    Tea.XBM = rblXB.SelectedValue;//性别码
                    //if (!string.IsNullOrWhiteSpace(txtCSNY.Text))
                    //{
                    //    Tea.CSRQ = Convert.ToDateTime(txtCSNY.Text.Trim());
                    //    Tea.AGE = CalculateAge(DateTime.Now, Tea.CSRQ);
                    //}
                    if (isDateTime(txtCSNY.Text))
                    {
                        Tea.CSRQ = Convert.ToDateTime(txtCSNY.Text.Trim());
                        Tea.AGE = CalculateAge(DateTime.Now, Convert.ToDateTime(Tea.CSRQ.ToString()));
                    }
                    else
                    {
                        Tea.CSRQ = null;
                    }

                    Tea.MZM = txtMZ.Text.Trim();//民族码
                    Tea.ZZMMM = ddZZMM.SelectedItem.Value;//txtZZMM.Text.Trim();//政治面貌码
                    //if (!string.IsNullOrWhiteSpace(txtCJGZSJ.Text))
                    //{
                    //    Tea.CJGZSJ = Convert.ToDateTime(txtCJGZSJ.Text.Trim());
                    //}
                    // Tea.CJGZSJ = System.DateTime.Now;

                    //Tea.XL = txtXL.Text.Trim();//系列
                    //Tea.ZC = txtZC.Text.Trim();//职称
                    //Tea.JB = txtJB.Text.Trim();//级别
                    //if (!string.IsNullOrWhiteSpace(txtPDSJ.Text))
                    //{
                    //    Tea.PDSJ = Convert.ToDateTime(txtPDSJ.Text.Trim());//评定时间
                    //}
                    // Tea.PDSJ = System.DateTime.Now;

                    //Tea.GZXL = txtdl.Text.Trim();//工资序列
                    //Tea.DJ = txtDJ.Text.Trim();//等级
                    //Tea.LB = txtLB.Text.Trim();//类别
                    //Tea.ZYRKXD = txtZYRKXD.Text.Trim();//主要任课学段
                    //Tea.XJTGW = txtXJTGW.Text.Trim();//现具体岗位
                    //Tea.JKHJZ = txtJKJZ.Text.Trim();//兼课或兼职
                    //Tea.ZZKSH = txtZZKSS.Text.Trim();//周总课时数
                    //Tea.GGLB = ddlGGLB.SelectedValue;//骨干类别
                    Tea.JSZKZLB = txtJSZGZ.Text.Trim();//教师资格证类别
                    Tea.SF = txtSF.Text.Trim();//身份
                    Tea.BZ = this.txtBZ.Text.Trim();//备注
                    //Tea.YHZT = "禁用";//用户状态
                    Tea.JG = txtJG.Text.Trim();//机构
                    Tea.XZZ = txtXZZ.Text.Trim();//现住址
                    Tea.LXDH = txtSJH.Text.Trim();//联系电话
                    Tea.JTDH = txtJTDH.Text.Trim();//家庭电话
                    //Tea.YWXM = txtYWXM.Text.Trim();//英文姓名
                    //Tea.XMPY = txtXMPY.Text.Trim();//姓名拼音
                    //Tea.CYM = txtCYM.Text.Trim();//曾用名
                    Tea.SFZJLXM = ddSFZJLX.SelectedItem.Value;//身份证件类型码
                    //Tea.CSDM = txtCSDM.Text.Trim();//出生地码
                    //Tea.HYZKM = rbHYZKM.SelectedItem.Value;//婚姻状况码
                    //Tea.GJDQM = txtGJDQM.Text.Trim();//国籍/地区码
                    //Tea.GATQWM = txtGATQWM.Text.Trim();//港澳台侨外码
                    //  Tea.JKZKM = txtJKZKM.Text.Trim();//健康状况码
                    Tea.JKZKM = rblJKZKM.SelectedValue;
                    Tea.XXM = ddXXM.SelectedItem.Value;//血型码
                    //Tea.XYZJM = txtXYZJM.Text.Trim();//信仰宗教码
                    //     Tea.ZP = txtZP.Text.Trim();//照片
                    Tea.SFZJYXQ = txtSFZJYXQ.Text.Trim();//身份证件有效期
                    //Tea.JGH = txtJGH.Text.Trim();//机构号
                    Tea.JTZZ = txtJTZZ.Text.Trim();//家庭住址
                    Tea.HKSZD = txtHKSZD.Text.Trim();//户口所在地
                    Tea.HKXZM = rbHKXZM.SelectedItem.Value;//户口性质码
                    //if (!string.IsNullOrEmpty(txtCJGZSJ.Text.Trim()))
                    //{
                    //    Tea.GZNY = Convert.ToDateTime(txtCJGZSJ.Text.Trim());//参加工作年月
                    //}

                    if (isDateTime(txtLXNY.Text))
                    {
                        Tea.LXNY = Convert.ToDateTime(txtLXNY.Text.Trim());//来校年月
                    }
                    else
                    {
                        Tea.LXNY = null;
                    }
                    if (isDateTime(txtCJNY.Text))
                    {
                        Tea.CJNY = Convert.ToDateTime(txtCJNY.Text.Trim());//来校年月
                    }
                    else
                    {
                        Tea.CJNY = null;
                    }
                    //添加教师担任的年级绑定;
                    //StringBuilder sbGrade = new StringBuilder();

                    Tea.BZLBM = txtBZLBM.Text.Trim();//编制类别码
                    Tea.DABH = txtDABH.Text.Trim();//档案编号
                    Tea.DAWB = txtDAWB.Text.Trim();//档案文本
                    Tea.TXDZ = txtXZZ.Text.Trim();//通信地址
                    Tea.YZBM = txtYZBM.Text.Trim();//邮政编码
                    Tea.DZXX = txtDZXX.Text.Trim();//电子信箱
                    Tea.ZYDZ = txtZYDZ.Text.Trim();//主页地址
                    Tea.TC = txtTC.Text.Trim();//特长

                    //上传图片
                    string zpPath = string.Empty;
                    if (!string.IsNullOrEmpty(fuZP.FileName))
                    {
                        if (UploadImage(fuZP, out zpPath))
                        {
                            Tea.ZP = zpPath;
                        }
                        else
                        {
                            lbl_pic.Text = "请上传正确的图片!";
                            return;
                        }
                    }
                    string sbSubject = "";


                    bool istrue = Update(Tea);
                    if (istrue){
                        //AlertAndRedirect("保存成功！", "/TeacherList.aspx?XXZZJGH=" + ddlXX.SelectedValue);
                        alert("保存成功");
                    }

                    else
                    {
                        //AlertAndRedirect("保存失败！", "/TeacherList.aspx?XXZZJGH=" + ddlXX.SelectedValue);
                        alert("保存成功");
                    }
                    BindTeacherInfo(IdCard);
                    //记入操作日志
                    Base_LogBLL.WriteLog(LogConstants.jsxxgl, ActionConstants.add);
                }
                catch (Exception ex)
                {
                    AlertAndRedirect("保存成功！", "");
                    LogCommon.writeLogUserCenter(ex.Message, ex.StackTrace);
                }
            }
        }
        /// <summary>
        /// 弹出信息,并跳转指定页面。
        /// </summary>
        public static void AlertAndRedirect(string message, string toURL)
        {
            try
            {
                string js = "<script language=javascript>alert('{0}');window.location.replace('{1}')</script>";
                HttpContext.Current.Response.Write(string.Format(js, message, toURL));
            }
            catch (Exception ex)
            {
                LogCommon.writeLogUserCenter(ex.Message, ex.StackTrace);
            }
            finally
            {
                HttpContext.Current.Response.End();
            }
        }
        protected void alert(string strMessage)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "hdesd", "<script language='javascript'> alert('" + strMessage + "'); </script>");
        }
        /// <summary>
        /// 根据出生日期计算年龄
        /// </summary>
        public int CalculateAge(DateTime NowTime, DateTime BirthDate)
        {
            DateTime Age = new DateTime((NowTime - BirthDate).Ticks);
            return Age.Year;
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("/TeacherList.aspx");
        }
        public bool UploadImage(FileUpload file1, out string result)
        {
            try
            {
                bool fileOk = false;
                string path = HttpContext.Current.Server.MapPath("~/Upload/");
                string name = file1.FileName;       //获取文件名
                string type = System.IO.Path.GetExtension(file1.FileName).ToLower();    //获取文件类型
                string ipath = Server.MapPath("Upload") + "\\" + name;    //获取文件路径
                string wpath = "Upload\\" + name;        //[color=red]设置文件保存相对路径(这里的路径起始就是我们存放图片的文件夹名)[/color]
                result = wpath;
                if (file1.HasFile)
                {
                    //取得文件的扩展名,并转换成小写
                    string fileExtension = System.IO.Path.GetExtension(file1.FileName).ToLower();
                    //限定只能上传jpg和gif图片
                    string[] allowExtension = { ".jpg", ".gif", ".bmp", ".png" };
                    //对上传的文件的类型进行一个个匹对
                    if (type == ".jpg" || type == ".gif" || type == ".bmp" || type == ".png")
                        fileOk = true;
                    //对上传文件的大小进行检测，限定文件最大不超过8M
                    if (file1.PostedFile.ContentLength > 8192000)
                        fileOk = false;
                    //最后的结果
                    if (fileOk)
                    {
                        try
                        {
                            file1.PostedFile.SaveAs(ipath);
                            //lable1.Text = "上传成功";
                            fileOk = true;
                        }
                        catch (Exception ex)
                        {
                            Common.LogCommon.writeLogUserCenter(ex.Message, ex.StackTrace);
                            fileOk = false;
                        }
                    }
                    else
                        fileOk = false;
                }
                return fileOk;
            }
            catch (Exception ex)
            {
                LogCommon.writeLogUserCenter(ex.Message, ex.StackTrace);
                result = "";
                return false;
            }
        }

        /// <summary>
        /// 教师科目绑定
        /// </summary>
        /// <param name="TeacherSubject">教师科目</param>
        public void SubjectSelected(string TeacherSubject)
        {
            try
            {

                int num = 0;
                string[] GreadsSubject = TeacherSubject.Split('|');
                bool BoolCheck = false;
                bool BoolGread = false;

                Base_TeacherBLL BT = new Base_TeacherBLL();
                Base_SchoolSubject SS = new Base_SchoolSubject();


                string strJGH = string.Empty;
                //SS.SchoolID = ddlXX.SelectedValue;  //学校组织号


                //foreach (ListViewDataItem lvdi in this.lvStu.Items)    //便利ListView
                //{
                //    CheckBoxList cblXK = lvdi.FindControl("cblXK") as CheckBoxList;
                //    Label LBGread = lvdi.FindControl("Lb_Gread") as Label;
                //    Label LBSubject = lvdi.FindControl("Lb_Subject") as Label;
                //    if (num == 0)
                //        num++;
                //    else
                //        LBSubject.Text = "";
                //    if (cblXK != null)
                //    {
                //        HiddenField hfGread = lvdi.FindControl("hfGread") as HiddenField;   //年级
                //        if (hfGread != null)
                //        {
                //            SS.GradeID = hfGread.Value.ToString();         // 年级
                //            DataTable list = BT.SelectGreadSubject(SS);    //科目+ ID
                //            if (list.Rows.Count > 0 && list != null)
                //            {
                //                string str1 = list.Rows[0]["SubjectID"].ToString();
                //                string str2 = list.Rows[0]["SubjectName"].ToString();
                //                string[] arrStr1 = str1.Split(',');
                //                string[] arrStr2 = str2.Split(',');
                //                string newStr = string.Empty;
                //                for (int i = 0; i < arrStr1.Length; i++)   //遍历  科目
                //                {
                //                    cblXK.Items.Add(new ListItem(arrStr2[i], arrStr1[i]));
                //                }
                //                #region  选中 科目
                //                if (TeacherSubject != "")   //教师  是否有 学科
                //                {
                //                    for (int i = 0; i < cblXK.Items.Count; i++) //循环所有科目
                //                    {
                //                        for (int j = 0; j < GreadsSubject.Length; j++)   //循环年级  1:1,2.3
                //                        {
                //                            string[] Gread_Subject = GreadsSubject[j].Split(':');
                //                            for (int k = 0; k < Gread_Subject.Length; k++)  //   年级    /   科目
                //                            {
                //                                if (Gread_Subject[k] == hfGread.Value.ToString()) //【年级存在】
                //                                {
                //                                    k++;
                //                                    string[] Subject = Gread_Subject[k].Split(',');
                //                                    for (int M = 0; M < Subject.Length; M++)
                //                                    {
                //                                        if (cblXK.Items[i].Value == Subject[M])  //【科目存在】
                //                                        {
                //                                            BoolCheck = true;
                //                                            break;
                //                                        }
                //                                        else
                //                                            BoolCheck = false;
                //                                    }
                //                                    if (BoolCheck == true)
                //                                    {
                //                                        BoolGread = true;
                //                                        break;
                //                                    }
                //                                }
                //                                else
                //                                    BoolCheck = false;
                //                            }
                //                            if (BoolGread == true)
                //                                break;
                //                        }
                //                        if (BoolCheck)
                //                        {
                //                            cblXK.Items[i].Selected = true;
                //                            BoolGread = false;
                //                            BoolCheck = false;
                //                        }
                //                    }
                //                }
                //                #endregion
                //            }
                //        }
                //    }
                //}
            }
            catch (Exception ex)
            {
                Common.LogCommon.writeLogUserCenter(ex.Message, ex.StackTrace);
            }
        }

        /// <summary>
        /// 【Change】【学校 下拉框】
        /// </summary>
        protected void ddlXX_SelectedIndexChanged(object sender, EventArgs e)
        {
            //年级、班级
            //Drp_Grade.Items.Clear();
            //Drp_Class.Items.Clear();
            //BindGrade();
            ListItem li = new ListItem("--请选择--", "");
            //Drp_Grade.Items.Insert(0, li);
            //Drp_Class.Items.Insert(0, li);

            //学科
            SubjectSelected("");


            //现具体岗位
            //txtLB.Text = "";
            //txtZYRKXD.Text = "";
            //txtXJTGW.Text = "";
            //txtJKJZ.Text = "";
            //txtZZKSS.Text = "";
            //ddlGGLB.SelectedIndex = 0;

            //组织结构
            //  ddlZZ.SelectedIndex = 0;
        }

        /// <summary>
        /// 【Change】【年级下拉框】
        /// </summary>
        protected void Drp_Grade_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Base_ClassBLL BC = new Base_ClassBLL();
            //DataSet ds = BC.GetClassBLL(Drp_Grade.SelectedValue.ToString(), ddlXX.SelectedValue.ToString());
            //if (ds != null && ds.Tables[0].Rows.Count > 0)
            //{
            //    Drp_Class.DataTextField = "BJ";
            //    Drp_Class.DataValueField = "BJBH";
            //    Drp_Class.DataSource = ds.Tables[0];
            //    Drp_Class.DataBind();
            //}
            BindClass();
        }
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Model.Base_Teacher GetModel(string SFZJH)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 SFZJH,YHZH,XXZZJGH,ZZJGH,YHZT,GH,XM,YWXM,XMPY,CYM,XBM,CSRQ,CSDM,JG,MZM,GJDQM,SFZJLXM,HYZKM,GATQWM,ZZMMM,JKZKM,XYZJM,XXM,ZP,SFZJYXQ,JTZZ,XZZ,HKSZD,HKXZM,XLM,GZNY,LXNY,CJNY,BZLBM,DABH,DAWB,TXDZ,LXDH,YZBM,DZXX,ZYDZ,TC,GWZYM,ZYRKXD,CJGZSJ,XL,ZC,JB,PDSJ,GZXL,DJ,LB,XGWGZSJ,XJTGW,JKHJZ,ZZKSH,GGLB,JSZKZLB,SF,BZ,ZJDLSJ,DLIP,DLBSM,XGSJ,AGE,JTDH,JGH,GradeID,SubjectID,NJ,BH from Base_Teacher ");
            strSql.Append(" where SFZJH=@SFZJH ");
            SqlParameter[] parameters = {
					new SqlParameter("@SFZJH", SqlDbType.NVarChar,20)			};
            parameters[0].Value = SFZJH;

            Model.Base_Teacher model = new Model.Base_Teacher();
            DataSet ds = SqlHelper.ExecuteDataset( CommandType.Text,strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                return DataRowToModel(ds.Tables[0].Rows[0]);
            }
            else
            {
                return null;
            }
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Model.Base_Teacher DataRowToModel(DataRow row)
        {
            Model.Base_Teacher model = new Model.Base_Teacher();
            if (row != null)
            {
                if (row["SFZJH"] != null)
                {
                    model.SFZJH = row["SFZJH"].ToString();
                }
                if (row["YHZH"] != null)
                {
                    model.YHZH = row["YHZH"].ToString();
                }
                if (row["XXZZJGH"] != null)
                {
                    model.XXZZJGH = row["XXZZJGH"].ToString();
                }
                if (row["ZZJGH"] != null)
                {
                    model.ZZJGH = row["ZZJGH"].ToString();
                }
                if (row["YHZT"] != null)
                {
                    model.YHZT = row["YHZT"].ToString();
                }
                if (row["GH"] != null)
                {
                    model.GH = row["GH"].ToString();
                }
                if (row["XM"] != null)
                {
                    model.XM = row["XM"].ToString();
                }
                if (row["YWXM"] != null)
                {
                    model.YWXM = row["YWXM"].ToString();
                }
                if (row["XMPY"] != null)
                {
                    model.XMPY = row["XMPY"].ToString();
                }
                if (row["CYM"] != null)
                {
                    model.CYM = row["CYM"].ToString();
                }
                if (row["XBM"] != null)
                {
                    model.XBM = row["XBM"].ToString();
                }
                if (row["CSRQ"] != null && row["CSRQ"].ToString() != "")
                {
                    model.CSRQ = DateTime.Parse(row["CSRQ"].ToString());
                }
                if (row["CSDM"] != null)
                {
                    model.CSDM = row["CSDM"].ToString();
                }
                if (row["JG"] != null)
                {
                    model.JG = row["JG"].ToString();
                }
                if (row["MZM"] != null)
                {
                    model.MZM = row["MZM"].ToString();
                }
                if (row["GJDQM"] != null)
                {
                    model.GJDQM = row["GJDQM"].ToString();
                }
                if (row["SFZJLXM"] != null)
                {
                    model.SFZJLXM = row["SFZJLXM"].ToString();
                }
                if (row["HYZKM"] != null)
                {
                    model.HYZKM = row["HYZKM"].ToString();
                }
                if (row["GATQWM"] != null)
                {
                    model.GATQWM = row["GATQWM"].ToString();
                }
                if (row["ZZMMM"] != null)
                {
                    model.ZZMMM = row["ZZMMM"].ToString();
                }
                if (row["JKZKM"] != null)
                {
                    model.JKZKM = row["JKZKM"].ToString();
                }
                if (row["XYZJM"] != null)
                {
                    model.XYZJM = row["XYZJM"].ToString();
                }
                if (row["XXM"] != null)
                {
                    model.XXM = row["XXM"].ToString();
                }
                if (row["ZP"] != null)
                {
                    model.ZP = row["ZP"].ToString();
                }
                if (row["SFZJYXQ"] != null)
                {
                    model.SFZJYXQ = row["SFZJYXQ"].ToString();
                }
                if (row["JTZZ"] != null)
                {
                    model.JTZZ = row["JTZZ"].ToString();
                }
                if (row["XZZ"] != null)
                {
                    model.XZZ = row["XZZ"].ToString();
                }
                if (row["HKSZD"] != null)
                {
                    model.HKSZD = row["HKSZD"].ToString();
                }
                if (row["HKXZM"] != null)
                {
                    model.HKXZM = row["HKXZM"].ToString();
                }
                if (row["XLM"] != null)
                {
                    model.XLM = row["XLM"].ToString();
                }
                if (row["GZNY"] != null && row["GZNY"].ToString() != "")
                {
                    model.GZNY = DateTime.Parse(row["GZNY"].ToString());
                }
                if (row["LXNY"] != null && row["LXNY"].ToString() != "")
                {
                    model.LXNY = DateTime.Parse(row["LXNY"].ToString());
                }
                if (row["CJNY"] != null && row["CJNY"].ToString() != "")
                {
                    model.CJNY = DateTime.Parse(row["CJNY"].ToString());
                }
                if (row["BZLBM"] != null)
                {
                    model.BZLBM = row["BZLBM"].ToString();
                }
                if (row["DABH"] != null)
                {
                    model.DABH = row["DABH"].ToString();
                }
                if (row["DAWB"] != null)
                {
                    model.DAWB = row["DAWB"].ToString();
                }
                if (row["TXDZ"] != null)
                {
                    model.TXDZ = row["TXDZ"].ToString();
                }
                if (row["LXDH"] != null)
                {
                    model.LXDH = row["LXDH"].ToString();
                }
                if (row["YZBM"] != null)
                {
                    model.YZBM = row["YZBM"].ToString();
                }
                if (row["DZXX"] != null)
                {
                    model.DZXX = row["DZXX"].ToString();
                }
                if (row["ZYDZ"] != null)
                {
                    model.ZYDZ = row["ZYDZ"].ToString();
                }
                if (row["TC"] != null)
                {
                    model.TC = row["TC"].ToString();
                }
                if (row["GWZYM"] != null)
                {
                    model.GWZYM = row["GWZYM"].ToString();
                }
                if (row["ZYRKXD"] != null)
                {
                    model.ZYRKXD = row["ZYRKXD"].ToString();
                }
                if (row["CJGZSJ"] != null && row["CJGZSJ"].ToString() != "")
                {
                    model.CJGZSJ = DateTime.Parse(row["CJGZSJ"].ToString());
                }
                if (row["XL"] != null)
                {
                    model.XL = row["XL"].ToString();
                }
                if (row["ZC"] != null)
                {
                    model.ZC = row["ZC"].ToString();
                }
                if (row["JB"] != null)
                {
                    model.JB = row["JB"].ToString();
                }
                if (row["PDSJ"] != null && row["PDSJ"].ToString() != "")
                {
                    model.PDSJ = DateTime.Parse(row["PDSJ"].ToString());
                }
                if (row["GZXL"] != null)
                {
                    model.GZXL = row["GZXL"].ToString();
                }
                if (row["DJ"] != null)
                {
                    model.DJ = row["DJ"].ToString();
                }
                if (row["LB"] != null)
                {
                    model.LB = row["LB"].ToString();
                }
                if (row["XGWGZSJ"] != null && row["XGWGZSJ"].ToString() != "")
                {
                    model.XGWGZSJ = DateTime.Parse(row["XGWGZSJ"].ToString());
                }
                if (row["XJTGW"] != null)
                {
                    model.XJTGW = row["XJTGW"].ToString();
                }
                if (row["JKHJZ"] != null)
                {
                    model.JKHJZ = row["JKHJZ"].ToString();
                }
                if (row["ZZKSH"] != null)
                {
                    model.ZZKSH = row["ZZKSH"].ToString();
                }
                if (row["GGLB"] != null)
                {
                    model.GGLB = row["GGLB"].ToString();
                }
                if (row["JSZKZLB"] != null)
                {
                    model.JSZKZLB = row["JSZKZLB"].ToString();
                }
                if (row["SF"] != null)
                {
                    model.SF = row["SF"].ToString();
                }
                if (row["BZ"] != null)
                {
                    model.BZ = row["BZ"].ToString();
                }
                if (row["ZJDLSJ"] != null && row["ZJDLSJ"].ToString() != "")
                {
                    model.ZJDLSJ = DateTime.Parse(row["ZJDLSJ"].ToString());
                }
                if (row["DLIP"] != null)
                {
                    model.DLIP = row["DLIP"].ToString();
                }
                if (row["DLBSM"] != null)
                {
                    model.DLBSM = row["DLBSM"].ToString();
                }
                if (row["XGSJ"] != null && row["XGSJ"].ToString() != "")
                {
                    model.XGSJ = DateTime.Parse(row["XGSJ"].ToString());
                }
                if (row["AGE"] != null && row["AGE"].ToString() != "")
                {
                    model.AGE = int.Parse(row["AGE"].ToString());
                }
                if (row["JTDH"] != null)
                {
                    model.JTDH = row["JTDH"].ToString();
                }
                if (row["JGH"] != null)
                {
                    model.JGH = row["JGH"].ToString();
                }
                if (row["GradeID"] != null)
                {
                    model.GradeID = row["GradeID"].ToString();
                }
                if (row["SubjectID"] != null)
                {
                    model.SubjectID = row["SubjectID"].ToString();
                }
                if (row["NJ"] != null)
                {
                    model.NJ = row["NJ"].ToString();
                }
                if (row["BH"] != null)
                {
                    model.BH = row["BH"].ToString();
                }
            }
            return model;
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Model.Base_Teacher model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Base_Teacher set ");
            strSql.Append("SFZJH=@SFZJH,");
            strSql.Append("YHZH=@YHZH,");
            strSql.Append("XXZZJGH=@XXZZJGH,");
            strSql.Append("ZZJGH=@ZZJGH,");
            strSql.Append("YHZT=@YHZT,");
            strSql.Append("GH=@GH,");
            strSql.Append("XM=@XM,");
            strSql.Append("YWXM=@YWXM,");
            strSql.Append("XMPY=@XMPY,");
            strSql.Append("CYM=@CYM,");
            strSql.Append("XBM=@XBM,");
            strSql.Append("CSRQ=@CSRQ,");
            strSql.Append("CSDM=@CSDM,");
            strSql.Append("JG=@JG,");
            strSql.Append("MZM=@MZM,");
            strSql.Append("GJDQM=@GJDQM,");
            strSql.Append("SFZJLXM=@SFZJLXM,");
            strSql.Append("HYZKM=@HYZKM,");
            strSql.Append("GATQWM=@GATQWM,");
            strSql.Append("ZZMMM=@ZZMMM,");
            strSql.Append("JKZKM=@JKZKM,");
            strSql.Append("XYZJM=@XYZJM,");
            strSql.Append("XXM=@XXM,");
            strSql.Append("ZP=@ZP,");
            strSql.Append("SFZJYXQ=@SFZJYXQ,");
            strSql.Append("JTZZ=@JTZZ,");
            strSql.Append("XZZ=@XZZ,");
            strSql.Append("HKSZD=@HKSZD,");
            strSql.Append("HKXZM=@HKXZM,");
            strSql.Append("XLM=@XLM,");
            strSql.Append("GZNY=@GZNY,");
            strSql.Append("LXNY=@LXNY,");
            strSql.Append("CJNY=@CJNY,");
            strSql.Append("BZLBM=@BZLBM,");
            strSql.Append("DABH=@DABH,");
            strSql.Append("DAWB=@DAWB,");
            strSql.Append("TXDZ=@TXDZ,");
            strSql.Append("LXDH=@LXDH,");
            strSql.Append("YZBM=@YZBM,");
            strSql.Append("DZXX=@DZXX,");
            strSql.Append("ZYDZ=@ZYDZ,");
            strSql.Append("TC=@TC,");
            strSql.Append("GWZYM=@GWZYM,");
            strSql.Append("ZYRKXD=@ZYRKXD,");
            strSql.Append("CJGZSJ=@CJGZSJ,");
            strSql.Append("XL=@XL,");
            strSql.Append("ZC=@ZC,");
            strSql.Append("JB=@JB,");
            strSql.Append("PDSJ=@PDSJ,");
            strSql.Append("GZXL=@GZXL,");
            strSql.Append("DJ=@DJ,");
            strSql.Append("LB=@LB,");
            strSql.Append("XGWGZSJ=@XGWGZSJ,");
            strSql.Append("XJTGW=@XJTGW,");
            strSql.Append("JKHJZ=@JKHJZ,");
            strSql.Append("ZZKSH=@ZZKSH,");
            strSql.Append("GGLB=@GGLB,");
            strSql.Append("JSZKZLB=@JSZKZLB,");
            strSql.Append("SF=@SF,");
            strSql.Append("BZ=@BZ,");
            strSql.Append("ZJDLSJ=@ZJDLSJ,");
            strSql.Append("DLIP=@DLIP,");
            strSql.Append("DLBSM=@DLBSM,");
            strSql.Append("XGSJ=@XGSJ,");
            strSql.Append("AGE=@AGE,");
            strSql.Append("JTDH=@JTDH,");
            strSql.Append("JGH=@JGH,");
            strSql.Append("GradeID=@GradeID,");
            strSql.Append("SubjectID=@SubjectID,");
            strSql.Append("NJ=@NJ,");
            strSql.Append("BH=@BH");
            strSql.Append(" where SFZJH=@SFZJH ");
            SqlParameter[] parameters = {
					new SqlParameter("@SFZJH", SqlDbType.NVarChar,20),
					new SqlParameter("@YHZH", SqlDbType.NVarChar,20),
					new SqlParameter("@XXZZJGH", SqlDbType.NVarChar,36),
					new SqlParameter("@ZZJGH", SqlDbType.NVarChar,36),
					new SqlParameter("@YHZT", SqlDbType.NVarChar,2),
					new SqlParameter("@GH", SqlDbType.NVarChar,20),
					new SqlParameter("@XM", SqlDbType.NVarChar,36),
					new SqlParameter("@YWXM", SqlDbType.NVarChar,60),
					new SqlParameter("@XMPY", SqlDbType.NVarChar,60),
					new SqlParameter("@CYM", SqlDbType.NVarChar,36),
					new SqlParameter("@XBM", SqlDbType.NVarChar,1),
					new SqlParameter("@CSRQ", SqlDbType.DateTime2),
					new SqlParameter("@CSDM", SqlDbType.NVarChar,6),
					new SqlParameter("@JG", SqlDbType.NVarChar,20),
					new SqlParameter("@MZM", SqlDbType.NVarChar,20),
					new SqlParameter("@GJDQM", SqlDbType.NVarChar,50),
					new SqlParameter("@SFZJLXM", SqlDbType.NVarChar,10),
					new SqlParameter("@HYZKM", SqlDbType.NVarChar,10),
					new SqlParameter("@GATQWM", SqlDbType.NVarChar,20),
					new SqlParameter("@ZZMMM", SqlDbType.NVarChar,20),
					new SqlParameter("@JKZKM", SqlDbType.NVarChar,10),
					new SqlParameter("@XYZJM", SqlDbType.NVarChar,10),
					new SqlParameter("@XXM", SqlDbType.NVarChar,10),
					new SqlParameter("@ZP", SqlDbType.NVarChar,50),
					new SqlParameter("@SFZJYXQ", SqlDbType.NVarChar,50),
					new SqlParameter("@JTZZ", SqlDbType.NVarChar,50),
					new SqlParameter("@XZZ", SqlDbType.NVarChar,50),
					new SqlParameter("@HKSZD", SqlDbType.NVarChar,50),
					new SqlParameter("@HKXZM", SqlDbType.NVarChar,10),
					new SqlParameter("@XLM", SqlDbType.NVarChar,20),
					new SqlParameter("@GZNY", SqlDbType.DateTime2),
					new SqlParameter("@LXNY", SqlDbType.DateTime2),
					new SqlParameter("@CJNY", SqlDbType.DateTime2),
					new SqlParameter("@BZLBM", SqlDbType.NVarChar,10),
					new SqlParameter("@DABH", SqlDbType.NVarChar,10),
					new SqlParameter("@DAWB", SqlDbType.Text),
					new SqlParameter("@TXDZ", SqlDbType.NVarChar,180),
					new SqlParameter("@LXDH", SqlDbType.NVarChar,20),
					new SqlParameter("@YZBM", SqlDbType.NVarChar,10),
					new SqlParameter("@DZXX", SqlDbType.NVarChar,50),
					new SqlParameter("@ZYDZ", SqlDbType.NVarChar,-1),
					new SqlParameter("@TC", SqlDbType.NVarChar,-1),
					new SqlParameter("@GWZYM", SqlDbType.NVarChar,50),
					new SqlParameter("@ZYRKXD", SqlDbType.NVarChar,20),
					new SqlParameter("@CJGZSJ", SqlDbType.DateTime2),
					new SqlParameter("@XL", SqlDbType.NVarChar,10),
					new SqlParameter("@ZC", SqlDbType.NVarChar,20),
					new SqlParameter("@JB", SqlDbType.NVarChar,10),
					new SqlParameter("@PDSJ", SqlDbType.DateTime2),
					new SqlParameter("@GZXL", SqlDbType.NVarChar,10),
					new SqlParameter("@DJ", SqlDbType.NVarChar,10),
					new SqlParameter("@LB", SqlDbType.NVarChar,10),
					new SqlParameter("@XGWGZSJ", SqlDbType.DateTime2),
					new SqlParameter("@XJTGW", SqlDbType.NVarChar,20),
					new SqlParameter("@JKHJZ", SqlDbType.NVarChar,30),
					new SqlParameter("@ZZKSH", SqlDbType.NVarChar,10),
					new SqlParameter("@GGLB", SqlDbType.NVarChar,10),
					new SqlParameter("@JSZKZLB", SqlDbType.NVarChar,20),
					new SqlParameter("@SF", SqlDbType.NVarChar,10),
					new SqlParameter("@BZ", SqlDbType.NVarChar,60),
					new SqlParameter("@ZJDLSJ", SqlDbType.DateTime2),
					new SqlParameter("@DLIP", SqlDbType.NVarChar,50),
					new SqlParameter("@DLBSM", SqlDbType.NVarChar,50),
					new SqlParameter("@XGSJ", SqlDbType.DateTime2),
					new SqlParameter("@AGE", SqlDbType.Int,4),
					new SqlParameter("@JTDH", SqlDbType.NVarChar,20),
					new SqlParameter("@JGH", SqlDbType.NVarChar,50),
					new SqlParameter("@GradeID", SqlDbType.Char,100),
					new SqlParameter("@SubjectID", SqlDbType.Char,100),
					new SqlParameter("@NJ", SqlDbType.NVarChar,36),
					new SqlParameter("@BH", SqlDbType.NVarChar,36)};
            parameters[0].Value = model.SFZJH;
            parameters[1].Value = model.YHZH;
            parameters[2].Value = model.XXZZJGH;
            parameters[3].Value = model.ZZJGH;
            parameters[4].Value = model.YHZT;
            parameters[5].Value = model.GH;
            parameters[6].Value = model.XM;
            parameters[7].Value = model.YWXM;
            parameters[8].Value = model.XMPY;
            parameters[9].Value = model.CYM;
            parameters[10].Value = model.XBM;
            parameters[11].Value = model.CSRQ;
            parameters[12].Value = model.CSDM;
            parameters[13].Value = model.JG;
            parameters[14].Value = model.MZM;
            parameters[15].Value = model.GJDQM;
            parameters[16].Value = model.SFZJLXM;
            parameters[17].Value = model.HYZKM;
            parameters[18].Value = model.GATQWM;
            parameters[19].Value = model.ZZMMM;
            parameters[20].Value = model.JKZKM;
            parameters[21].Value = model.XYZJM;
            parameters[22].Value = model.XXM;
            parameters[23].Value = model.ZP;
            parameters[24].Value = model.SFZJYXQ;
            parameters[25].Value = model.JTZZ;
            parameters[26].Value = model.XZZ;
            parameters[27].Value = model.HKSZD;
            parameters[28].Value = model.HKXZM;
            parameters[29].Value = model.XLM;
            parameters[30].Value = model.GZNY;
            parameters[31].Value = model.LXNY;
            parameters[32].Value = model.CJNY;
            parameters[33].Value = model.BZLBM;
            parameters[34].Value = model.DABH;
            parameters[35].Value = model.DAWB;
            parameters[36].Value = model.TXDZ;
            parameters[37].Value = model.LXDH;
            parameters[38].Value = model.YZBM;
            parameters[39].Value = model.DZXX;
            parameters[40].Value = model.ZYDZ;
            parameters[41].Value = model.TC;
            parameters[42].Value = model.GWZYM;
            parameters[43].Value = model.ZYRKXD;
            parameters[44].Value = model.CJGZSJ;
            parameters[45].Value = model.XL;
            parameters[46].Value = model.ZC;
            parameters[47].Value = model.JB;
            parameters[48].Value = model.PDSJ;
            parameters[49].Value = model.GZXL;
            parameters[50].Value = model.DJ;
            parameters[51].Value = model.LB;
            parameters[52].Value = model.XGWGZSJ;
            parameters[53].Value = model.XJTGW;
            parameters[54].Value = model.JKHJZ;
            parameters[55].Value = model.ZZKSH;
            parameters[56].Value = model.GGLB;
            parameters[57].Value = model.JSZKZLB;
            parameters[58].Value = model.SF;
            parameters[59].Value = model.BZ;
            parameters[60].Value = model.ZJDLSJ;
            parameters[61].Value = model.DLIP;
            parameters[62].Value = model.DLBSM;
            parameters[63].Value = model.XGSJ;
            parameters[64].Value = model.AGE;
            parameters[65].Value = model.JTDH;
            parameters[66].Value = model.JGH;
            parameters[67].Value = model.GradeID;
            parameters[68].Value = model.SubjectID;
            parameters[69].Value = model.NJ;
            parameters[70].Value = model.BH;


            int rows = SqlHelper.ExecuteNonQuery(CommandType.Text,strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}