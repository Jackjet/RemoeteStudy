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

namespace UserCenterSystem
{
    public partial class TeacherInfo : BaseInfo
    {
        /// <summary>
        /// 存储URL传参的教师身份证号
        /// </summary>

        protected void Page_Load(object sender, EventArgs e)
        {
            //Page.MaintainScrollPositionOnPostBack = true;
            //if (!IsPostBack)
            //{
                

            //}
        }
    //    /// <summary>
    //    /// 【Function】绑定学校
    //    /// </summary>
    //    public void BindScholl()
    //    {
    //        Base_Teacher teacher = Session[UCSKey.SESSION_LoginInfo] as Base_Teacher;
    //        Base_DepartmentBLL deptBll = new Base_DepartmentBLL();
    //        //DataTable dt = deptBll.SelectXJByLoginName(teacher);
    //        DataTable dt = deptBll.SelectDeptDtByLSJGH("0");
    //        ddlXX.DataSource = dt;
    //        ddlXX.DataTextField = "JGMC";
    //        ddlXX.DataValueField = "XXZZJGH";
    //        ddlXX.DataBind();
    //    }
    //    /// <summary>
    //    /// 【Function】绑定学科
    //    /// </summary>
    //    private void BindGrade()
    //    {
    //        try
    //        {
    //            Base_GradeBLL gradeBll = new Base_GradeBLL();
    //            string strJGH = string.Empty;
    //            strJGH = ddlXX.SelectedValue;
    //            //DataTable grade = gradeBll.SelectGradeByJGH(strJGH);//年级+学科
    //            DataTable grade = new DataTable();
    //            grade = gradeBll.SelectAllGradeInfo();
    //            //lvStu.DataSource = grade;
    //            //lvStu.DataBind();

    //            //绑定年级
    //            Drp_Grade.Items.Clear();
    //            Drp_Grade.DataTextField = "NJMC";
    //            Drp_Grade.DataValueField = "NJ";
    //            Drp_Grade.DataSource = grade;
    //            Drp_Grade.DataBind();
    //        }
    //        catch (Exception ex)
    //        {
    //            LogCommon.writeLogUserCenter(ex.Message, ex.StackTrace);
    //        }
    //    }
    //    /// <summary>
    //    /// 绑定班级
    //    /// </summary>
    //    private void BindClass()
    //    {
    //        try
    //        {
    //            Base_ClassBLL ClassBll = new Base_ClassBLL();
    //            DataTable dt = new DataTable();
    //            dt = ClassBll.GetClassByGradeID(Drp_Grade.SelectedValue);
    //            Drp_Class.Items.Clear();
    //            Drp_Class.DataTextField = "BJ";
    //            Drp_Class.DataValueField = "BJBH";
    //            Drp_Class.DataSource = dt;
    //            Drp_Class.DataBind();
    //        }
    //        catch (Exception ex)
    //        {
    //            LogCommon.writeLogUserCenter(ex.Message, ex.StackTrace);
    //        }
    //    }
    //    /// <summary>
    //    /// 教师信息
    //    /// </summary>
    //    /// <param name="IdCard">身份证号</param>
    //    public void BindTeacherInfo(string IdCard)
    //    {
    //        try
    //        {
    //            Base_StudyCareerBLL stuCaBLL = new Base_StudyCareerBLL();
    //            Base_TeacherBLL TeaBLL = new Base_TeacherBLL();
    //            Base_TeacherSpouseBLL TeaSPBLL = new Base_TeacherSpouseBLL();

    //            Base_Teacher Tea = new Base_Teacher();
    //            Base_StudyCareer stucaXXL = new Base_StudyCareer();
    //            Base_StudyCareer stucaYXL = new Base_StudyCareer();
    //            Base_StudyCareer stucaJXXL = new Base_StudyCareer();
    //            Base_StudyCareer stucaYJS = new Base_StudyCareer();
    //            Base_TeacherSpouse TeaS = new Base_TeacherSpouse();

    //            Tea = TeaBLL.GetTeacherBySFZJH(IdCard);
    //            if (Tea == null)
    //            {
    //                //BindGrade();
    //                //ListItem li = new ListItem("--请选择--", "");
    //                //ddlZZ.Items.Insert(0, li);
    //                //Drp_Grade.Items.Insert(0, li);
    //                //Drp_Class.Items.Insert(0, li);
    //                //SubjectSelected("");
    //            }
    //            //教师数据
    //            else if (Tea != null)
    //            {
    //                //BindGrade();
    //                FillTeacher(Tea);//加载教师信息
    //                if (!string.IsNullOrWhiteSpace(Tea.GradeID))
    //                    SubjectSelected(Tea.GradeID);
    //                else
    //                    SubjectSelected("");


    //                Drp_Grade.SelectedValue = Tea.NJ;
    //                Drp_Class.SelectedValue = Tea.BH;
    //                ddlXX.SelectedValue = Tea.XXZZJGH;

    //                TeaS = TeaSPBLL.GetTeacherSpouseBySFZJH(IdCard);
    //                //配偶数据
    //                //if (TeaS != null)
    //                //{
    //                //    txtPOXM.Text = TeaS.POXM;
    //                //    txtPOGZDW.Text = TeaS.POGZDW;
    //                //}
    //                stucaXXL = stuCaBLL.GetStudyCareerBySFZJH(IdCard, Convert.ToInt16(UCSKey.XXLX.XXL));
    //                //现学历
    //                //if (stucaXXL != null)
    //                //{
    //                //    txtXXLCC.Text = stucaXXL.CC;
    //                //    txtXXLZY.Text = stucaXXL.SXZYMC;
    //                //    txtXXLBYYX.Text = stucaXXL.XXDW;
    //                //    txtXXLBYSJ.Text = ProcessDate(stucaXXL.XXZZRQ);
    //                //}
    //                stucaYXL = stuCaBLL.GetStudyCareerBySFZJH(IdCard, Convert.ToInt16(UCSKey.XXLX.YXL));
    //                //原学历
    //                //if (stucaYXL != null)
    //                //{
    //                //    txtYXLCC.Text = stucaYXL.CC;
    //                //    txtYXLZY.Text = stucaYXL.SXZYMC;
    //                //    txtYXLBYYX.Text = stucaYXL.XXDW;
    //                //    txtYXLBYSJ.Text = ProcessDate(stucaYXL.XXZZRQ);
    //                //}
    //                stucaJXXL = stuCaBLL.GetStudyCareerBySFZJH(IdCard, Convert.ToInt16(UCSKey.XXLX.XJXXL));
    //                //现进修学历
    //                //if (stucaJXXL != null)
    //                //{
    //                //    txtXJXCC.Text = stucaJXXL.CC;
    //                //    txtXJXZY.Text = stucaJXXL.SXZYMC;
    //                //    txtXJXBYYX.Text = stucaJXXL.XXDW;
    //                //    txtXJXBYSJ.Text = ProcessDate(stucaJXXL.XXZZRQ);
    //                //}
    //                stucaYJS = stuCaBLL.GetStudyCareerBySFZJH(IdCard, Convert.ToInt16(UCSKey.XXLX.YJSKC));
    //                //研究生课程
    //                //if (stucaYJS != null)
    //                //{
    //                //    txtYJSBCC.Text = stucaYJS.CC;
    //                //    txtYJSBZY.Text = stucaYJS.SXZYMC;
    //                //    txtYJSBBYYX.Text = stucaYJS.XXDW;
    //                //    txtYJSBBYSJ.Text = ProcessDate(stucaYJS.XXZZRQ);
    //                //}
    //                //所负责班级
    //                //Drp_Grade.SelectedIndex = Drp_Grade.Items.IndexOf(Drp_Grade.Items.FindByValue(Tea.NJ));
    //                //Drp_Class.SelectedIndex = Drp_Class.Items.IndexOf(Drp_Class.Items.FindByValue(Tea.BH));
                    
    //            }
    //            else
    //            {
    //                //ListItem li = new ListItem("--请选择--", "");
    //                //ddlZZ.Items.Insert(0, li);
    //                //Drp_Grade.Items.Insert(0, li);
    //                //Drp_Class.Items.Insert(0, li);
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            throw ex;
    //        }

    //    }

    //    /// <summary>
    //    /// 【Function】绑定组织机构
    //    /// </summary>
    //    public void BindDedpt(string XXZZJGH)
    //    {
    //        Base_DepartmentBLL deptBll = new Base_DepartmentBLL();
    //        DataTable dt = deptBll.GetDeptBLL(XXZZJGH);
    //        ddlZZ.DataSource = dt;
    //        ddlZZ.DataTextField = "JGMC";
    //        ddlZZ.DataValueField = "XXZZJGH";
    //        ddlZZ.DataBind();
    //    }

        
    //    /// <summary>
    //    /// 绑定下拉框值
    //    /// </summary>
    //    public void BindDDL(DropDownList ddl, string SelectValue)
    //    {
    //        //if (ddl.Items.FindByValue(SelectValue) != null)
    //        //{
    //        //    ListItem li = new ListItem("--请选择--", "");
    //        //    ddl.Items.Insert(0, li);
    //        //    ddl.ClearSelection();
    //        //    ddl.Items.FindByValue(SelectValue).Selected = true;
    //        //}
    //        //else
    //        //{
    //        //    ListItem li = new ListItem("--请选择--", "");
    //        //    ddl.Items.Insert(0, li);
    //        //}
    //    }
    //    /// <summary>
    //    /// 绑定下拉框值
    //    /// </summary>
    //    public void BindXXZZJGH(DropDownList ddl, string SelectValue)
    //    {
    //        if (ddl.Items.FindByValue(SelectValue) != null)
    //        {
    //            ddl.ClearSelection();
    //            ddl.Items.FindByValue(SelectValue).Selected = true;
    //        }
    //    }
    //    /// <summary>
    //    /// 处理日期
    //    /// </summary>
    //    public string ProcessDate(DateTime DT)
    //    {
    //        if (DT.Year > 1900)
    //        {
    //            return DT.ToShortDateString();
    //        }
    //        return "";
    //    }

    //    /// <summary>
    //    /// 填充教师相关信息
    //    /// </summary>
    //    private void FillTeacher(Base_Teacher Tea)
    //    {
    //        try
    //        {
    //            //BindXXZZJGH(ddlXX, Tea.XXZZJGH);
    //            //BindDDL(ddlZZ, Tea.ZZJGH);
    //            //BindDDL(Drp_Grade, Tea.NJ);

    //            Base_ClassBLL BC = new Base_ClassBLL();
    //            //DataSet ds = BC.GetClassBLL(Drp_Grade.SelectedValue.ToString(), ddlXX.SelectedValue.ToString());
    //            //if (ds != null && ds.Tables[0].Rows.Count > 0)
    //            //{
    //            //    Drp_Class.Items.Clear();
    //            //    Drp_Class.DataTextField = "BJ";
    //            //    Drp_Class.DataValueField = "BJBH";
    //            //    Drp_Class.DataSource = ds.Tables[0];
    //            //    Drp_Class.DataBind();
    //            //}
    //            //BindDDL(Drp_Class, Tea.BH);

    //            //------------------------------
    //            txtXM.Text = Tea.XM;//姓名
    //            txtSFZJH.Text = Tea.SFZJH;//身份证件号
    //            Hid_SFZJH.Value = Tea.SFZJH;//Hid身份证号  2014-12-26
    //            rblXB.SelectedValue = Tea.XBM;//性别码
    //            txtCSNY.Text = ProcessDate(Tea.CSRQ);//出生日期
    //            txtMZ.Text = Tea.MZM;//民族码
    //            if (!string.IsNullOrEmpty(Tea.ZZMMM))
    //            {
    //                ddZZMM.Items.FindByValue(Tea.ZZMMM).Selected = true;//政治面貌码
    //            }
    //            //txtCJGZSJ.Text = ProcessDate(Tea.CJGZSJ);
    //            //txtXL.Text = Tea.XL;//系列
    //            //txtZC.Text = Tea.ZC;//职称
    //            //txtJB.Text = Tea.JB;//级别
    //            //txtPDSJ.Text = ProcessDate(Tea.PDSJ);//评定时间
    //            //txtdl.Text = Tea.GZXL;//工资序列
    //            //txtDJ.Text = Tea.DJ;//等级
    //            //txtLB.Text = Tea.LB;//类别
    //            //txtZYRKXD.Text = Tea.ZYRKXD;//主要任课学段
    //            //txtXJTGW.Text = Tea.XJTGW;//现具体岗位
    //            //txtJKJZ.Text = Tea.JKHJZ;//兼课或兼职
    //            //txtZZKSS.Text = Tea.ZZKSH;//周总课时数
    //            //ddlGGLB.SelectedValue = Tea.GGLB;//骨干类别
    //            txtJSZGZ.Text = Tea.JSZKZLB;//教师资格证类别
    //            txtSF.Text = Tea.SF;//身份
    //            this.txtBZ.Text = Tea.BZ;//备注
    //            //Tea.YHZT = "禁用";//用户状态
    //            txtJG.Text = Tea.JG;//机构
    //            txtXZZ.Text = Tea.XZZ;//现住址
    //            txtSJH.Text = Tea.LXDH;//联系电话
    //            txtJTDH.Text = Tea.JTDH;//家庭电话
    //            //txtYWXM.Text = Tea.YWXM;//英文姓名
    //            //txtXMPY.Text = Tea.XMPY;//姓名拼音
    //            //txtCYM.Text = Tea.CYM;//曾用名
    //            if (!string.IsNullOrEmpty(Tea.SFZJLXM))
    //            {
    //                ddSFZJLX.Items.FindByValue(Tea.SFZJLXM).Selected = true;//身份证码
    //            }
    //            //txtCSDM.Text = Tea.CSDM;//出生地码
    //            //if (!string.IsNullOrEmpty(Tea.HYZKM))
    //            //{
    //            //    rbHYZKM.Items.FindByValue(Tea.HYZKM).Selected = true;//婚姻状况码
    //            //}
    //            if (!string.IsNullOrEmpty(Tea.JKZKM))
    //            {
    //                rblJKZKM.Items.FindByValue(Tea.JKZKM).Selected = true;//健康状况码
    //            }
    //            //txtGJDQM.Text = Tea.GJDQM;//国籍/地区码
    //            //txtGATQWM.Text = Tea.GATQWM;//港澳台侨外码
    //            //  rblJKZKM.SelectedValue = Tea.JKZKM;//健康状况码
    //            if (!string.IsNullOrEmpty(Tea.XXM))
    //            {
    //                ddXXM.Items.FindByValue(Tea.XXM).Selected = true;//血型码
    //            }
    //            //txtXYZJM.Text = Tea.XYZJM;//信仰宗教码
    //            //  txtZP.Text=Tea.ZP;//照片
    //            txtSFZJYXQ.Text = Tea.SFZJYXQ;//身份证件有效期
    //            //txtJGH.Text = Tea.JGH;//机构号
    //            txtJTZZ.Text = Tea.JTZZ;//家庭住址
    //            txtHKSZD.Text = Tea.HKSZD;//户口所在地
    //            if (!string.IsNullOrEmpty(Tea.HKXZM))
    //            {
    //                rbHKXZM.Items.FindByValue(Tea.HKXZM).Selected = true;//户口性质码
    //            }
    //            //txtCJGZSJ.Text = ProcessDate(Tea.CJGZSJ);//参加工作年月
    //            txtLXNY.Text = ProcessDate(Tea.LXNY);//来校年月
    //            txtCJNY.Text = ProcessDate(Tea.CJNY);//从教年月           
    //            txtBZLBM.Text = Tea.BZLBM;//编制类别码
    //            txtDABH.Text = Tea.DABH;//档案编号
    //            txtDAWB.Text = Tea.DAWB;//档案文本
    //            txtXZZ.Text = Tea.TXDZ;//通信地址
    //            txtYZBM.Text = Tea.YZBM;//邮政编码
    //            txtDZXX.Text = Tea.DZXX;//电子信箱
    //            txtZYDZ.Text = Tea.ZYDZ;//主页地址
    //            txtTC.Text = Tea.TC;//特长
    //            if (!string.IsNullOrWhiteSpace(Tea.SubjectID))
    //            {
    //                string[] subItems = Tea.SubjectID.Split(',');
    //                //遍历items

    //                //foreach (string item in subItems)
    //                //{
    //                //    ////如果值相等，则选中该项
    //                //    //foreach (ListItem listItem in cblSubject.Items)
    //                //    //{
    //                //    //    if (item == listItem.Value)
    //                //    //        listItem.Selected = true;
    //                //    //    else
    //                //    //        continue;
    //                //    //}
    //                //}
    //            }
    //            if (!string.IsNullOrWhiteSpace(Tea.GradeID))
    //            {
    //                string[] GradeItems = Tea.GradeID.Split(',');
    //                //遍历items
    //                //foreach (string item in GradeItems)
    //                //{
    //                //    ////如果值相等，则选中该项
    //                //    // foreach (ListItem listItem in cblGrade.Items)
    //                //    // {
    //                //    //     if (item == listItem.Value)
    //                //    //         listItem.Selected = true;
    //                //    //     else
    //                //    //         continue;
    //                //    // }
    //                //}
    //            }

    //            //  fuZP. = Tea.ZP;
    //            //绑定照片
    //            if (!string.IsNullOrWhiteSpace(Tea.ZP))
    //            {
    //                string aa = Server.MapPath(Tea.ZP);
    //                //string  aa = System.Web.UI.Control.ResolveUrl(Tea.ZP);
    //                imghead.ImageUrl = Tea.ZP;
    //                //    imghead.ImageUrl = aa;
    //                imghead.Width = 100;
    //                imghead.Height = 140;
    //                //imgZP.ImageUrl = Server.MapPath(Tea.ZP);
    //                //imgZP.Width = 100;
    //                //imgZP.Height = 100;
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            LogCommon.writeLogUserCenter(ex.Message, ex.StackTrace);
    //        }
    //    }

    //    /// <summary>
    //    /// 【Button】【（保存）添加/修改】
    //    /// </summary>
    //    protected void btnAdd_Click(object sender, EventArgs e)
    //    {
    //        if (IsValid)
    //        {

    //            try
    //            {
    //                Base_StudyCareerBLL stuCaBLL = new Base_StudyCareerBLL();
    //                Base_TeacherBLL TeaBLL = new Base_TeacherBLL();
    //                Base_ParentBLL ParBLL = new Base_ParentBLL();
    //                Base_TeacherSpouseBLL TeaSPBLL = new Base_TeacherSpouseBLL();
    //                Base_Teacher Tea = new Base_Teacher();

    //                if (StudentOldIdCard.Value != "" && txtSFZJH.Text != StudentOldIdCard.Value)
    //                {
    //                    Tea.SFZJH = StudentOldIdCard.Value;
    //                    TeaBLL.DeleteTeacherBLL(Tea);
    //                }


    //                Tea.XXZZJGH = ddlXX.SelectedValue;//学校组织机构号
    //                Tea.XM = txtXM.Text.Trim();//姓名
    //                Tea.SFZJH = txtSFZJH.Text.Trim();//身份证件号
    //                Tea.XBM = rblXB.SelectedValue;//性别码
    //                if (!string.IsNullOrWhiteSpace(txtCSNY.Text))
    //                {
    //                    Tea.CSRQ = Convert.ToDateTime(txtCSNY.Text.Trim());
    //                    Tea.AGE = CalculateAge(DateTime.Now, Tea.CSRQ);
    //                }


    //                Tea.MZM = txtMZ.Text.Trim();//民族码
    //                Tea.ZZMMM = ddZZMM.SelectedItem.Value;//txtZZMM.Text.Trim();//政治面貌码
    //                //if (!string.IsNullOrWhiteSpace(txtCJGZSJ.Text))
    //                //{
    //                //    Tea.CJGZSJ = Convert.ToDateTime(txtCJGZSJ.Text.Trim());
    //                //}
    //                // Tea.CJGZSJ = System.DateTime.Now;

    //                //Tea.XL = txtXL.Text.Trim();//系列
    //                //Tea.ZC = txtZC.Text.Trim();//职称
    //                //Tea.JB = txtJB.Text.Trim();//级别
    //                //if (!string.IsNullOrWhiteSpace(txtPDSJ.Text))
    //                //{
    //                //    Tea.PDSJ = Convert.ToDateTime(txtPDSJ.Text.Trim());//评定时间
    //                //}
    //                // Tea.PDSJ = System.DateTime.Now;

    //                //Tea.GZXL = txtdl.Text.Trim();//工资序列
    //                //Tea.DJ = txtDJ.Text.Trim();//等级
    //                //Tea.LB = txtLB.Text.Trim();//类别
    //                //Tea.ZYRKXD = txtZYRKXD.Text.Trim();//主要任课学段
    //                //Tea.XJTGW = txtXJTGW.Text.Trim();//现具体岗位
    //                //Tea.JKHJZ = txtJKJZ.Text.Trim();//兼课或兼职
    //                //Tea.ZZKSH = txtZZKSS.Text.Trim();//周总课时数
    //                //Tea.GGLB = ddlGGLB.SelectedValue;//骨干类别
    //                Tea.JSZKZLB = txtJSZGZ.Text.Trim();//教师资格证类别
    //                Tea.SF = txtSF.Text.Trim();//身份
    //                Tea.BZ = this.txtBZ.Text.Trim();//备注
    //                Tea.YHZT = "禁用";//用户状态
    //                Tea.JG = txtJG.Text.Trim();//机构
    //                Tea.XZZ = txtXZZ.Text.Trim();//现住址
    //                Tea.LXDH = txtSJH.Text.Trim();//联系电话
    //                Tea.JTDH = txtJTDH.Text.Trim();//家庭电话
    //                //Tea.YWXM = txtYWXM.Text.Trim();//英文姓名
    //                //Tea.XMPY = txtXMPY.Text.Trim();//姓名拼音
    //                //Tea.CYM = txtCYM.Text.Trim();//曾用名
    //                //Tea.SFZJLXM = ddSFZJLX.SelectedItem.Value;//身份证件类型码
    //                //Tea.CSDM = txtCSDM.Text.Trim();//出生地码
    //                //Tea.HYZKM = rbHYZKM.SelectedItem.Value;//婚姻状况码
    //                //Tea.GJDQM = txtGJDQM.Text.Trim();//国籍/地区码
    //                //Tea.GATQWM = txtGATQWM.Text.Trim();//港澳台侨外码
    //                //  Tea.JKZKM = txtJKZKM.Text.Trim();//健康状况码
    //                Tea.JKZKM = rblJKZKM.SelectedValue;
    //                Tea.XXM = ddXXM.SelectedItem.Value;//血型码
    //                //Tea.XYZJM = txtXYZJM.Text.Trim();//信仰宗教码
    //                //     Tea.ZP = txtZP.Text.Trim();//照片
    //                Tea.SFZJYXQ = txtSFZJYXQ.Text.Trim();//身份证件有效期
    //                //Tea.JGH = txtJGH.Text.Trim();//机构号
    //                Tea.JTZZ = txtJTZZ.Text.Trim();//家庭住址
    //                Tea.HKSZD = txtHKSZD.Text.Trim();//户口所在地
    //                Tea.HKXZM = rbHKXZM.SelectedItem.Value;//户口性质码
    //                //if (!string.IsNullOrEmpty(txtCJGZSJ.Text.Trim()))
    //                //{
    //                //    Tea.GZNY = Convert.ToDateTime(txtCJGZSJ.Text.Trim());//参加工作年月
    //                //}
    //                if (!string.IsNullOrEmpty(txtLXNY.Text.Trim()))
    //                {
    //                    Tea.LXNY = Convert.ToDateTime(txtLXNY.Text.Trim());//来校年月
    //                }
    //                if (!string.IsNullOrEmpty(txtCJNY.Text.Trim()))
    //                {
    //                    Tea.CJNY = Convert.ToDateTime(txtCJNY.Text.Trim());//来校年月
    //                }
    //                //添加教师担任的年级绑定;
    //                StringBuilder sbGrade = new StringBuilder();

    //                Tea.BZLBM = txtBZLBM.Text.Trim();//编制类别码
    //                Tea.DABH = txtDABH.Text.Trim();//档案编号
    //                Tea.DAWB = txtDAWB.Text.Trim();//档案文本
    //                Tea.TXDZ = txtXZZ.Text.Trim();//通信地址
    //                Tea.YZBM = txtYZBM.Text.Trim();//邮政编码
    //                Tea.DZXX = txtDZXX.Text.Trim();//电子信箱
    //                Tea.ZYDZ = txtZYDZ.Text.Trim();//主页地址
    //                Tea.TC = txtTC.Text.Trim();//特长

    //                //现学历
    //                //if (!string.IsNullOrWhiteSpace(txtXXLCC.Text) &&
    //                //    !string.IsNullOrWhiteSpace(txtXXLBYSJ.Text) &&
    //                //    !string.IsNullOrWhiteSpace(txtXXLBYYX.Text.Trim()) &&
    //                //    !string.IsNullOrWhiteSpace(txtXXLZY.Text.Trim()))
    //                //{
    //                //if(true){
    //                //    Base_StudyCareer stucaXXL = new Base_StudyCareer();
    //                //    //stucaXXL.CC = txtXXLCC.Text.Trim();
    //                //    stucaXXL.SFZJH = txtSFZJH.Text.Trim();
    //                //    //stucaXXL.SXZYMC = txtXXLZY.Text.Trim();
    //                //    //stucaXXL.XXDW = txtXXLBYYX.Text.Trim();
    //                //    //if (!string.IsNullOrWhiteSpace(txtXXLBYSJ.Text))
    //                //    //{
    //                //    //    stucaXXL.XXZZRQ = Convert.ToDateTime(txtXXLBYSJ.Text.Trim());
    //                //    //}
    //                //    stucaXXL.XLLX = Convert.ToInt16(UCSKey.XXLX.XXL).ToString();
    //                //    stuCaBLL.Insert(stucaXXL);
    //                //}
    //                //原学历

    //                //if (!string.IsNullOrWhiteSpace(txtYXLBYSJ.Text) &&
    //                //    !string.IsNullOrWhiteSpace(txtYXLBYYX.Text) &&
    //                //    !string.IsNullOrWhiteSpace(txtYXLCC.Text.Trim()) &&
    //                //    !string.IsNullOrWhiteSpace(txtYXLZY.Text.Trim()))
    //                //{
    //                //if(true){
    //                //    Base_StudyCareer stucaXXL = new Base_StudyCareer();
    //                //    //stucaXXL.CC = txtYXLCC.Text.Trim();
    //                //    stucaXXL.SFZJH = txtSFZJH.Text.Trim();
    //                //    //stucaXXL.SXZYMC = txtYXLZY.Text.Trim();
    //                //    //stucaXXL.XXDW = txtYXLBYYX.Text.Trim();
    //                //    //if (!string.IsNullOrWhiteSpace(txtYXLBYSJ.Text))
    //                //    //{
    //                //    //    stucaXXL.XXZZRQ = Convert.ToDateTime(txtYXLBYSJ.Text.Trim());
    //                //    //}
    //                //    stucaXXL.XLLX = Convert.ToInt16(UCSKey.XXLX.YXL).ToString();
    //                //    stuCaBLL.Insert(stucaXXL);
    //                //}
    //                //现进修学历
    //                //if (!string.IsNullOrWhiteSpace(txtXJXCC.Text) &&
    //                //    !string.IsNullOrWhiteSpace(txtXJXBYSJ.Text) &&
    //                //    !string.IsNullOrWhiteSpace(txtXJXBYYX.Text.Trim()) &&
    //                //    !string.IsNullOrWhiteSpace(txtXJXZY.Text.Trim()))
    //                //{
    //                //if(true){
    //                //    Base_StudyCareer stucaXXL = new Base_StudyCareer();
    //                //    //stucaXXL.CC = txtXJXCC.Text.Trim();
    //                //    stucaXXL.SFZJH = txtSFZJH.Text.Trim();
    //                //    stucaXXL.SXZYMC = txtXJXZY.Text.Trim();
    //                //    stucaXXL.XXDW = txtXJXBYYX.Text.Trim();
    //                //    if (!string.IsNullOrWhiteSpace(txtXJXBYSJ.Text))
    //                //    {
    //                //        stucaXXL.XXZZRQ = Convert.ToDateTime(txtXJXBYSJ.Text.Trim());
    //                //    }
    //                //    stucaXXL.XLLX = Convert.ToInt16(UCSKey.XXLX.XJXXL).ToString();
    //                //    stuCaBLL.Insert(stucaXXL);
    //                //}
    //                //参加研究生课程班学习

    //                //if (!string.IsNullOrWhiteSpace(txtYJSBCC.Text) &&
    //                //    !string.IsNullOrWhiteSpace(txtYJSBBYSJ.Text) &&
    //                //    !string.IsNullOrWhiteSpace(txtYJSBBYYX.Text.Trim()) &&
    //                //    !string.IsNullOrWhiteSpace(txtYJSBZY.Text.Trim()))
    //                //{
    //                //if(true){
    //                //    Base_StudyCareer stucaXXL = new Base_StudyCareer();
    //                //    stucaXXL.CC = txtYJSBCC.Text.Trim();
    //                //    stucaXXL.SFZJH = txtSFZJH.Text.Trim();
    //                //    stucaXXL.SXZYMC = txtYJSBZY.Text.Trim();
    //                //    stucaXXL.XXDW = txtYJSBBYYX.Text.Trim();
    //                //    if (!string.IsNullOrWhiteSpace(txtYJSBBYSJ.Text))
    //                //    {
    //                //        stucaXXL.XXZZRQ = Convert.ToDateTime(txtYJSBBYSJ.Text.Trim());
    //                //    }
    //                //    stucaXXL.XLLX = Convert.ToInt16(UCSKey.XXLX.YJSKC).ToString();
    //                //    stuCaBLL.Insert(stucaXXL);
    //                //}
    //                //配偶情况
    //                //if (!string.IsNullOrWhiteSpace(txtPOXM.Text) && !string.IsNullOrWhiteSpace(txtPOGZDW.Text))
    //                //{
    //                //    Base_TeacherSpouse TeaS = new Base_TeacherSpouse();
    //                //    TeaS.POXM = txtPOXM.Text.Trim();
    //                //    TeaS.POGZDW = txtPOGZDW.Text.Trim();
    //                //    TeaS.SFZJH = txtSFZJH.Text.Trim();
    //                //    TeaSPBLL.Insert(TeaS);
    //                //}

    //                //上传图片
    //                string zpPath = string.Empty;
    //                if (!string.IsNullOrEmpty(fuZP.FileName))
    //                {
    //                    if (UploadImage(fuZP, out zpPath))
    //                    {
    //                        Tea.ZP = zpPath;
    //                    }
    //                    else
    //                    {
    //                        lbl_pic.Text = "请上传正确的图片!";
    //                        return;
    //                    }
    //                }
    //                string sbSubject = "";

    //                foreach (ListViewDataItem lvdi in this.lvStu.Items)    //便利ListView
    //                {
    //                    CheckBoxList cblXK = lvdi.FindControl("cblXK") as CheckBoxList;  //CheckBoxList
    //                    if (cblXK != null)
    //                    {
    //                        HiddenField hfGread = lvdi.FindControl("hfGread") as HiddenField;   //年级
    //                        if (hfGread != null)
    //                        {
    //                            for (int i = 0; i < cblXK.Items.Count; i++)  //循环 每一行  ListView 中CheckBoxList 中  CheckBox
    //                            {
    //                                if (cblXK.Items[i].Selected)
    //                                {
    //                                    sbSubject = sbSubject + cblXK.Items[i].Value + ",";
    //                                }
    //                            }
    //                            if (sbSubject != "")
    //                            {
    //                                Tea.GradeID = Tea.GradeID + hfGread.Value.ToString() + ":" + sbSubject.Substring(0, sbSubject.Length - 1) + "|";  //年级学科
    //                                sbSubject = "";
    //                            }
    //                        }
    //                    }
    //                }

    //                Tea.ZZJGH = ddlZZ.SelectedValue.ToString(); //组织机构
    //                Tea.NJ = Drp_Grade.SelectedValue.ToString();  //年级
    //                Tea.BH = Drp_Class.SelectedValue.ToString();  //班级
    //                bool istrue = TeaBLL.InsertAll(Tea);
    //                if (istrue)
    //                    AlertAndRedirect("保存成功！", "/TeacherList.aspx?XXZZJGH=" + ddlXX.SelectedValue);
    //                else
    //                    AlertAndRedirect("保存失败！", "/TeacherList.aspx?XXZZJGH=" + ddlXX.SelectedValue);
    //                //记入操作日志
    //                Base_LogBLL.WriteLog(LogConstants.jsxxgl, ActionConstants.add);
    //            }
    //            catch (Exception ex)
    //            {
    //                LogCommon.writeLogUserCenter(ex.Message, ex.StackTrace);
    //            }
    //        }
    //    }
    //    /// <summary>
    //    /// 弹出信息,并跳转指定页面。
    //    /// </summary>
    //    public static void AlertAndRedirect(string message, string toURL)
    //    {
    //        try
    //        {
    //            string js = "<script language=javascript>alert('{0}');window.location.replace('{1}')</script>";
    //            HttpContext.Current.Response.Write(string.Format(js, message, toURL));
    //        }
    //        catch (Exception ex)
    //        {
    //            LogCommon.writeLogUserCenter(ex.Message, ex.StackTrace);
    //        }
    //        finally
    //        {
    //            HttpContext.Current.Response.End();
    //        }
    //    }
    //    protected void alert(string strMessage)
    //    {
    //        ClientScript.RegisterStartupScript(this.GetType(), "hdesd", "<script language='javascript'> alert('" + strMessage + "'); </script>");
    //    }
    //    /// <summary>
    //    /// 根据出生日期计算年龄
    //    /// </summary>
    //    public int CalculateAge(DateTime NowTime, DateTime BirthDate)
    //    {
    //        DateTime Age = new DateTime((NowTime - BirthDate).Ticks);
    //        return Age.Year;
    //    }

    //    protected void btnBack_Click(object sender, EventArgs e)
    //    {
    //        Response.Redirect("/TeacherList.aspx");
    //    }
    //    public bool UploadImage(FileUpload file1, out string result)
    //    {
    //        try
    //        {
    //            bool fileOk = false;
    //            string path = HttpContext.Current.Server.MapPath("~/Upload/");
    //            string name = file1.FileName;       //获取文件名
    //            string type = System.IO.Path.GetExtension(file1.FileName).ToLower();    //获取文件类型
    //            string ipath = Server.MapPath("Upload") + "\\" + name;    //获取文件路径
    //            string wpath = "Upload\\" + name;        //[color=red]设置文件保存相对路径(这里的路径起始就是我们存放图片的文件夹名)[/color]
    //            result = wpath;
    //            if (file1.HasFile)
    //            {
    //                //取得文件的扩展名,并转换成小写
    //                string fileExtension = System.IO.Path.GetExtension(file1.FileName).ToLower();
    //                //限定只能上传jpg和gif图片
    //                string[] allowExtension = { ".jpg", ".gif", ".bmp", ".png" };
    //                //对上传的文件的类型进行一个个匹对
    //                if (type == ".jpg" || type == ".gif" || type == ".bmp" || type == ".png")
    //                    fileOk = true;
    //                //对上传文件的大小进行检测，限定文件最大不超过8M
    //                if (file1.PostedFile.ContentLength > 8192000)
    //                    fileOk = false;
    //                //最后的结果
    //                if (fileOk)
    //                {
    //                    try
    //                    {
    //                        file1.PostedFile.SaveAs(ipath);
    //                        //lable1.Text = "上传成功";
    //                        fileOk = true;
    //                    }
    //                    catch (Exception ex)
    //                    {
    //                        Common.LogCommon.writeLogUserCenter(ex.Message, ex.StackTrace);
    //                        fileOk = false;
    //                    }
    //                }
    //                else
    //                    fileOk = false;
    //            }
    //            return fileOk;
    //        }
    //        catch (Exception ex)
    //        {
    //            LogCommon.writeLogUserCenter(ex.Message, ex.StackTrace);
    //            result = "";
    //            return false;
    //        }
    //    }

    //    /// <summary>
    //    /// 教师科目绑定
    //    /// </summary>
    //    /// <param name="TeacherSubject">教师科目</param>
    //    public void SubjectSelected(string TeacherSubject)
    //    {
    //        try
    //        {

    //            int num = 0;
    //            string[] GreadsSubject = TeacherSubject.Split('|');
    //            bool BoolCheck = false;
    //            bool BoolGread = false;

    //            Base_TeacherBLL BT = new Base_TeacherBLL();
    //            Base_SchoolSubject SS = new Base_SchoolSubject();


    //            string strJGH = string.Empty;
    //            SS.SchoolID = ddlXX.SelectedValue;  //学校组织号


    //            foreach (ListViewDataItem lvdi in this.lvStu.Items)    //便利ListView
    //            {
    //                CheckBoxList cblXK = lvdi.FindControl("cblXK") as CheckBoxList;
    //                Label LBGread = lvdi.FindControl("Lb_Gread") as Label;
    //                Label LBSubject = lvdi.FindControl("Lb_Subject") as Label;
    //                if (num == 0)
    //                    num++;
    //                else
    //                    LBSubject.Text = "";
    //                if (cblXK != null)
    //                {
    //                    HiddenField hfGread = lvdi.FindControl("hfGread") as HiddenField;   //年级
    //                    if (hfGread != null)
    //                    {
    //                        SS.GradeID = hfGread.Value.ToString();         // 年级
    //                        DataTable list = BT.SelectGreadSubject(SS);    //科目+ ID
    //                        if (list.Rows.Count > 0 && list != null)
    //                        {
    //                            string str1 = list.Rows[0]["SubjectID"].ToString();
    //                            string str2 = list.Rows[0]["SubjectName"].ToString();
    //                            string[] arrStr1 = str1.Split(',');
    //                            string[] arrStr2 = str2.Split(',');
    //                            string newStr = string.Empty;
    //                            for (int i = 0; i < arrStr1.Length; i++)   //遍历  科目
    //                            {
    //                                cblXK.Items.Add(new ListItem(arrStr2[i], arrStr1[i]));
    //                            }
    //                            #region  选中 科目
    //                            if (TeacherSubject != "")   //教师  是否有 学科
    //                            {
    //                                for (int i = 0; i < cblXK.Items.Count; i++) //循环所有科目
    //                                {
    //                                    for (int j = 0; j < GreadsSubject.Length; j++)   //循环年级  1:1,2.3
    //                                    {
    //                                        string[] Gread_Subject = GreadsSubject[j].Split(':');
    //                                        for (int k = 0; k < Gread_Subject.Length; k++)  //   年级    /   科目
    //                                        {
    //                                            if (Gread_Subject[k] == hfGread.Value.ToString()) //【年级存在】
    //                                            {
    //                                                k++;
    //                                                string[] Subject = Gread_Subject[k].Split(',');
    //                                                for (int M = 0; M < Subject.Length; M++)
    //                                                {
    //                                                    if (cblXK.Items[i].Value == Subject[M])  //【科目存在】
    //                                                    {
    //                                                        BoolCheck = true;
    //                                                        break;
    //                                                    }
    //                                                    else
    //                                                        BoolCheck = false;
    //                                                }
    //                                                if (BoolCheck == true)
    //                                                {
    //                                                    BoolGread = true;
    //                                                    break;
    //                                                }
    //                                            }
    //                                            else
    //                                                BoolCheck = false;
    //                                        }
    //                                        if (BoolGread == true)
    //                                            break;
    //                                    }
    //                                    if (BoolCheck)
    //                                    {
    //                                        cblXK.Items[i].Selected = true;
    //                                        BoolGread = false;
    //                                        BoolCheck = false;
    //                                    }
    //                                }
    //                            }
    //                            #endregion
    //                        }
    //                    }
    //                }
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            Common.LogCommon.writeLogUserCenter(ex.Message, ex.StackTrace);
    //        }
    //    }

    //    /// <summary>
    //    /// 【Change】【学校 下拉框】
    //    /// </summary>
    //    protected void ddlXX_SelectedIndexChanged(object sender, EventArgs e)
    //    {
    //        //年级、班级
    //        //Drp_Grade.Items.Clear();
    //        //Drp_Class.Items.Clear();
    //        //BindGrade();
    //        ListItem li = new ListItem("--请选择--", "");
    //        //Drp_Grade.Items.Insert(0, li);
    //        //Drp_Class.Items.Insert(0, li);

    //        //学科
    //        SubjectSelected("");


    //        //现具体岗位
    //        //txtLB.Text = "";
    //        //txtZYRKXD.Text = "";
    //        //txtXJTGW.Text = "";
    //        //txtJKJZ.Text = "";
    //        //txtZZKSS.Text = "";
    //        //ddlGGLB.SelectedIndex = 0;

    //        //组织结构
    //        //  ddlZZ.SelectedIndex = 0;
    //    }

    //    /// <summary>
    //    /// 【Change】【年级下拉框】
    //    /// </summary>
    //    protected void Drp_Grade_SelectedIndexChanged(object sender, EventArgs e)
    //    {
    //        //Base_ClassBLL BC = new Base_ClassBLL();
    //        //DataSet ds = BC.GetClassBLL(Drp_Grade.SelectedValue.ToString(), ddlXX.SelectedValue.ToString());
    //        //if (ds != null && ds.Tables[0].Rows.Count > 0)
    //        //{
    //        //    Drp_Class.DataTextField = "BJ";
    //        //    Drp_Class.DataValueField = "BJBH";
    //        //    Drp_Class.DataSource = ds.Tables[0];
    //        //    Drp_Class.DataBind();
    //        //}
    //        BindClass();
    //    }
    }
}