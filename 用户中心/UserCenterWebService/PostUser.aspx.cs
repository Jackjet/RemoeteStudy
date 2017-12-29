using ADManager;
using BLL;
using Microsoft.SharePoint;
using System;
using System.Collections.Generic;
using System.Data;
using System.DirectoryServices;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Xml;
using WebServiceUser;

namespace Me
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        #region## 类型
        /// <summary>
        /// 类型
        /// </summary>
        public enum TypeEnum : int
        {
            /// <summary>
            /// 组织单位
            /// </summary>
            OU = 1,

            /// <summary>
            /// 用户
            /// </summary>
            USER = 2
        }
        #endregion

        #region## Ad域信息实体
        /// <summary>
        /// Ad域信息实体
        /// </summary>
        public class AdModel
        {
            public AdModel(string id, string name, int typeId, string parentId, string cn, string dispalyname, string description,string pwdLastSet,DateTime whenChanged,DateTime whenCreated)
            {
                Id = id;
                Name = name;
                TypeId = typeId;
                ParentId = parentId;
                Cn = cn;
                Dispalyname = dispalyname;
                Description = description;

                PwdLastSet = pwdLastSet;
                WhenChanged = whenChanged;
                WhenCreated = whenCreated;
            }

            public string Id { get; set; }

            public string Name { get; set; }

            public int TypeId { get; set; }

            public string ParentId { get; set; }

            public string Cn { get; set; }

            public string Dispalyname { get; set; }

            public string Description { get; set; }

            public string PwdLastSet { get; set; }

            public DateTime WhenChanged { get; set; }

            public DateTime WhenCreated { get; set; }

        }
        #endregion

        private List<AdModel> list = new List<AdModel>();

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 【Button】【同步用户】
        /// </summary>
        protected void Btn_Post_Click(object sender, EventArgs e)
        {
            PostUser pu = new PostUser();
            SPSite site = new SPSite(TextBox2.Text);
            SPWeb web = site.RootWeb;
            SPSite sps = web.Site;

            pu.FPostUsers(TextBox3.Text, "5151", sps.ID, TextBox1.Text);
        }

        /// <summary>
        /// 【Button】【查询用户权限】
        /// </summary>
        protected void Btn_SeachSharePoint_Click(object sender, EventArgs e)
        {
            PostUser pu = new PostUser();
            DataTable dt = pu.dtGroupUser(TextBox4.Text);

            List_InBox.DataSource = dt;
            List_InBox.DataBind();
        }

        /// <summary>
        /// 【Button】【删除按钮】
        /// </summary>
        protected void Btn_DeleteSharepoint_Click(object sender, EventArgs e)
        {
            for (int i = 0; i <= List_InBox.Items.Count - 1; i++)
            {
                CheckBox cbox = (CheckBox)List_InBox.Items[i].FindControl("Ckb_SelNew");
                HtmlInputHidden HtmlInputID = (HtmlInputHidden)List_InBox.Items[i].FindControl("txtID");  

                if (cbox.Checked == true)
                {
                    SPSecurity.RunWithElevatedPrivileges(delegate()
                    {
                        SPSite site = new SPSite(TextBox2.Text);
                        {
                            SPWeb web = site.RootWeb;
                            web.AllowUnsafeUpdates = true;
                            web.Users.Remove(HtmlInputID.Value);
                        }
                    });
                }
            }
        }

        /// <summary>
        /// 查询AD
        /// </summary>
        protected void Btn_SeachAD_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();

            dt.Columns.Add("UserNO");           //登录名
            dt.Columns.Add("cn");               //用户名
            dt.Columns.Add("dispalyname");       //组名
            dt.Columns.Add("description");      //用户权限
            //时间
            dt.Columns.Add("pwdLastSet");    
            dt.Columns.Add("whenChanged");     
            dt.Columns.Add("whenCreated");




            if (ValidationInput())
            {
                DirectoryEntry domain;
                DirectoryEntry rootOU;

                if (IsConnected(txtDomainName.Text.Trim(), txtUserName.Text.Trim(), txtPwd.Text.Trim(),Txt_IP.Text.Trim(), out domain))
                {
                    if (IsExistOU(domain, out rootOU, Txt_Department.Text.Trim()))
                    {
                        domain=rootOU;
                        if (IsExistOU(domain, out rootOU, Txt_UserDepartment.Text.Trim()))
                        {
                            SyncAll(rootOU);      //同步所有  


                            for (int i = 0; i < list.Count; i++)
                            {
                                DataRow DtRow = dt.NewRow();
                                DtRow["UserNO"] = list[i].Name;
                                DtRow["cn"] = list[i].Cn;
                                DtRow["dispalyname"] = list[i].Dispalyname;
                                DtRow["description"] = list[i].Description;
                                DtRow["pwdLastSet"] = list[i].PwdLastSet;
                                DtRow["whenChanged"] = list[i].WhenChanged;
                                DtRow["whenCreated"] = list[i].WhenCreated;

                               // DtRow["description"] = "UPDATE DigtalCampus..Base_Teacher SET YHZH='" + list[i].Name + "',YHZT='启用' WHERE SFZJH = '" + list[i].Description + "'";
                                dt.Rows.Add(DtRow);
                            }
                            DataView DTV = dt.DefaultView;
                            DTV.Sort = "whenCreated desc";


                            Lit_User.DataSource = DTV;
                            Lit_User.DataBind();
                        }
                        else
                        {
                            // MessageBox.Show("域中不存在此组织结构!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }
                }
                else
                {
                  //  MessageBox.Show("不能连接到域,请确认输入是否正确!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
        }

        /// <summary>
        /// 删除AD
        /// </summary>
        protected void Btn_ADDelete_Click(object sender, EventArgs e)
        {
            for (int i = 0; i <= Lit_User.Items.Count - 1; i++)
            {
                CheckBox cbox = (CheckBox)Lit_User.Items[i].FindControl("Ckb_Sel");
                HtmlInputHidden HtmlInputID = (HtmlInputHidden)Lit_User.Items[i].FindControl("txtID");  

                if (cbox.Checked == true)
                {
                    ADWebService ad = new ADWebService();
                    ad.DeleteUser2(HtmlInputID.Value);
                }
            }
        }


        /// <summary>
        /// 取用户所对应的用户组
        /// </summary>
        /// <param name="userName">用户名称</param>
        /// <param name="domain">域</param>
        /// <param name="adusername">登陆用户</param>
        /// <param name="adpassword">登陆密码</param>
        /// <returns></returns>
        public static List<string> GetADGroups(string userName, string domain, string adusername, string adpassword)
        {
            List<string> groups = new List<string>();
            try
            {
                var entry = new DirectoryEntry(string.Format("LDAP://{0}", domain), adusername, adpassword);
                entry.RefreshCache();

                DirectorySearcher search = new DirectorySearcher(entry);
                search.PropertiesToLoad.Add("memberof");
                search.Filter = string.Format("sAMAccountName={0}", userName);
                SearchResult result = search.FindOne();

                if (result != null)
                {
                    ResultPropertyValueCollection c = result.Properties["memberof"];
                    foreach (var a in c)
                    {
                        string temp = a.ToString();
                        Match match = Regex.Match(temp, @"CN=\s*(?<g>\w*)\s*.");
                        groups.Add(match.Groups["g"].Value);
                    }
                }
            }
            catch
            {

            }
            return groups;
        }


        #region## 同步
        /// <summary>
        /// 功能:同步
        /// 创建人:Wilson
        /// 创建时间:2012-12-15
        /// </summary>
        /// <param name="entryOU"></param>
        public void SyncAll(DirectoryEntry entryOU)
        {
            /*
             * 参考：http://msdn.microsoft.com/zh-cn/library/system.directoryservices.directorysearcher.filter(v=vs.80).aspx
             * 
             * -----------------其它------------------------------             
             * 机算机：       (objectCategory=computer)
             * 组：           (objectCategory=group)
             * 联系人：       (objectCategory=contact)
             * 共享文件夹：   (objectCategory=volume)
             * 打印机         (objectCategory=printQueue)
             * ---------------------------------------------------
             */
            DirectorySearcher mySearcher = new DirectorySearcher(entryOU, "(objectclass=organizationalUnit)"); //查询组织单位                 

            DirectoryEntry root = mySearcher.SearchRoot;   //查找根OU

            SyncRootOU(root);

            StringBuilder sb = new StringBuilder();

            sb.Append("\r\nID\t帐号\t类型\t父ID\r\n");

            foreach (var item in list)
            {
                sb.AppendFormat("{0}\t{1}\t{2}\t{3}\r\n", item.Id, item.Name, item.TypeId, item.ParentId);
            }

           // LogRecord.WriteLog(sb.ToString());

           // MessageBox.Show("同步成功", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);

           // Application.Exit();
        }
        #endregion

        #region## 同步根组织单位
        /// <summary>
        /// 功能: 同步根组织单位
        /// 创建人:Wilson
        /// 创建时间:2012-12-15
        /// </summary>
        /// <param name="entry"></param>
        private void SyncRootOU(DirectoryEntry entry)
        {
            if (entry.Properties.Contains("ou") && entry.Properties.Contains("objectGUID"))
            {
                string rootOuName = entry.Properties["ou"][0].ToString();

                byte[] bGUID = entry.Properties["objectGUID"][0] as byte[];

                string id = BitConverter.ToString(bGUID);

                list.Add(new AdModel(id, rootOuName, (int)TypeEnum.OU, "0", "", "", "", "", DateTime.MinValue, DateTime.MinValue));

                SyncSubOU(entry, id);
            }
        }
        #endregion

        #region## 同步下属组织单位及下属用户
        /// <summary>
        /// 功能: 同步下属组织单位及下属用户
        /// 创建人:Wilson
        /// 创建时间:2012-12-15
        /// </summary>
        /// <param name="entry"></param>
        /// <param name="parentId"></param>
        private void SyncSubOU(DirectoryEntry entry, string parentId)
        {
            foreach (DirectoryEntry subEntry in entry.Children)
            {
                string entrySchemaClsName = subEntry.SchemaClassName;

                string[] arr = subEntry.Name.Split('=');
                string categoryStr = arr[0];
                string nameStr = arr[1];
                string id = string.Empty;

                if (subEntry.Properties.Contains("objectGUID"))   //SID
                {
                    byte[] bGUID = subEntry.Properties["objectGUID"][0] as byte[];

                    id = BitConverter.ToString(bGUID);
                }

                bool isExist = list.Exists(d => d.Id == id);

                switch (entrySchemaClsName)
                {
                    case "organizationalUnit":

                        if (!isExist)
                        {
                            list.Add(new AdModel(id, nameStr, (int)TypeEnum.OU, parentId, "", "", "", "", DateTime.MinValue, DateTime.MinValue));
                        }

                        SyncSubOU(subEntry, id);
                        break;
                    case "user":
                        string accountName = string.Empty;
                        string cn = string.Empty;
                        string dispalyname = string.Empty;
                        string description = string.Empty;
                        string pwdLastSet= string.Empty;
                        DateTime whenChanged= DateTime.MinValue;
                        DateTime whenCreated = DateTime.MinValue;

                        if (subEntry.Properties.Contains("samaccountName"))
                        {
                            accountName = subEntry.Properties["samaccountName"].Value.ToString();
                        }
                        if (subEntry.Properties.Contains("cn"))
                        {
                            cn = subEntry.Properties["cn"][0].ToString();
                        }
                        if (subEntry.Properties.Contains("displayName"))
                        {
                            dispalyname = subEntry.Properties["displayName"].Value.ToString();

                        }
                        if (subEntry.Properties.Contains("description"))
                        {
                            description = subEntry.Properties["description"].Value.ToString();
                        }
                        if (subEntry.Properties.Contains("pwdLastSet"))
                        {
                            pwdLastSet = subEntry.Properties["pwdLastSet"].Value.ToString();
                        }
                        if (subEntry.Properties.Contains("whenChanged"))
                        {
                            whenChanged = (DateTime)subEntry.Properties["whenChanged"].Value;
                        }
                        if (subEntry.Properties.Contains("whenCreated"))
                        {
                            whenCreated = Convert.ToDateTime(subEntry.Properties["whenCreated"].Value);
                        }

                        if (!isExist)
                        {
                            list.Add(new AdModel(id, accountName, (int)TypeEnum.USER, parentId, cn, dispalyname, description, pwdLastSet,whenChanged, whenCreated));
                        }
                        break;
                }
            }
        }
        #endregion

        #region## 是否连接到域
        /// <summary>
        /// 功能：是否连接到域
        /// 作者：Wilson
        /// 时间：2012-12-15
        /// http://msdn.microsoft.com/zh-cn/library/system.directoryservices.directoryentry.path(v=vs.90).aspx
        /// </summary>
        /// <param name="domainName">域名或IP</param>
        /// <param name="userName">用户名</param>
        /// <param name="userPwd">密码</param>
        /// <param name="entry">域</param>
        /// <returns></returns>
        private bool IsConnected(string domainName, string userName, string userPwd,string sADIp, out DirectoryEntry domain)
        {
            domain = new DirectoryEntry();
            try
            {
                //LDAP://192.168.0.82/DC=SHAREPOINT2013
                //LDAP://192.168.137.100/DC=SP2013,DC=com
                domain.Path = string.Format("LDAP://" + sADIp + "/{0}", GetLDAPDomain(domainName));

            

                domain.Username = userName;
                domain.Password = userPwd;
                domain.AuthenticationType = AuthenticationTypes.Secure;
                domain.RefreshCache();

                //string sPath = "LDAP://192.168.0.82/DC=SharePoint2013,DC=com";// string.Format("LDAP://" + sADIp + "/DC={0},DC=com", domainName);
                //string sADUser = userName;
                //string sADPassword = userPwd;
                //domain = new DirectoryEntry(sPath, sADUser, sADPassword, AuthenticationTypes.Secure);


                return true;
            }
            catch (Exception ex)
            {
                Common.LogCommon.writeLogWebService(DateTime.Now + "  " + ex.Message, ex.StackTrace);
                return false;
            }
        }
        #endregion

        #region## 域地址拼接
        /// <summary>   
        /// This will read in the ADServer Value from the Web.config and will Return it   
        /// as an LDAP Path   
        /// e.g.. DC=testing, DC=co, DC=nz.   
        /// This is required when Creating Directory Entry other than the Root.   
        /// </summary>   
        /// <returns></returns>   
        private string GetLDAPDomain(string sDomain)
        {
            StringBuilder LDAPDomain = new StringBuilder();
            string[] LDAPDC = sDomain.Split('.');
            for (int i = 0; i < LDAPDC.GetUpperBound(0) + 1; i++)
            {
                LDAPDomain.Append("DC=" + LDAPDC[i]);
                if (i < LDAPDC.GetUpperBound(0))
                {
                    LDAPDomain.Append(",");
                }
            }
            return LDAPDomain.ToString();
        }
        #endregion

        #region## 域中是否存在组织单位
        /// <summary>
        /// 功能：域中是否存在组织单位
        /// 作者：Wilson
        /// 时间：2012-12-15
        /// </summary>
        /// <param name="entry"></param>
        /// <param name="ou"></param>
        /// <returns></returns>
        private bool IsExistOU(DirectoryEntry entry, out DirectoryEntry ou, string txtRootOU)
        {
            ou = new DirectoryEntry();
            try
            {
                ou = entry.Children.Find("OU=" + txtRootOU);

                return (ou != null);
            }
            catch (Exception ex)
            {
               // LogRecord.WriteLog("[IsExistOU方法]错误信息：" + ex.Message);
                return false;
            }
        }
        #endregion

        #region## 窗体加载
        /// <summary>
        /// 窗体加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmAD_Load(object sender, EventArgs e)
        {
            txtDomainName.Text = "SHAREPOINT2013";
            txtUserName.Text = "administrator";
            txtPwd.Text = "123";
           // txtRootOU.Text = "acompany";
        }
        #endregion

        #region## 验证输入
        /// <summary>
        /// 功能：验证输入
        /// 作者：Wilson
        /// 时间：2012-12-15
        /// </summary>
        /// <returns></returns>
        private bool ValidationInput()
        {
            if (txtDomainName.Text.Trim().Length == 0)
            {
               // MessageBox.Show("请输入域名!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDomainName.Focus();
                return false;
            }

            if (txtUserName.Text.Trim().Length == 0)
            {
               // MessageBox.Show("请输入用户名!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtUserName.Focus();
                return false;
            }

            if (txtPwd.Text.Trim().Length == 0)
            {
                //MessageBox.Show("请输入密码!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPwd.Focus();
                return false;
            }

           // if (txtRootOU.Text.Trim().Length == 0)
           // {
               // MessageBox.Show("请输入根组织单位!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
               // txtRootOU.Focus();
            //    return false;
           // }
            return true;
        }
        #endregion
    }
}