using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Common.SchoolUserService;
using System.Data;
using Microsoft.SharePoint;

namespace Common
{
    //教研组
    //教研组信息通过WebService从用户中心读取
    public class JYZ
    {
        /// <summary>
        /// 根据用户账号获取用户所在教研组
        /// 字段说明：JYZID-教研组ID、JYZMC-教研组名称、LSJGH-隶属机构号、JGMC-机构名称
        /// </summary>
        /// <param name="loginName">用户账号（无域，如admin）</param>
        /// <returns>返回DataTable</returns>
        public DataTable GetJYZByYHZH(string loginName)
        {
            UserPhoto userInfo = new UserPhoto();
            DataSet ds = userInfo.ReserchTeamL(loginName);//获取当前用户所在的教研组
            return ds.Tables.Count > 0 ? ds.Tables[0] : null;
        }

        /// <summary>
        /// 根据教研组ID获取该教研组成员
        /// 字段说明：JYZID-教研组ID、SFZJH-身份证件号、YHZH-用户账号、XM-姓名、JYZMC-教研组名称、LSJGH-隶属机构号、DZXX、JYZID1
        /// </summary>
        /// <param name="jyzID">教研组ID</param>
        /// <returns></returns>
        public DataTable GetMembersByJYZID(string jyzID)
        {
            UserPhoto userInfo = new UserPhoto();
            return userInfo.ReserchTeamUsers(jyzID);
        }

        /// <summary>
        /// 获取当前用户是教研组组长的教研组
        /// 字段说明：JYZID-教研组ID、JYZMC-教研组名称
        /// </summary>
        /// <param name="loginName">用户账号（无域，如admin）</param>
        /// <returns></returns>
        public DataTable GetLeadJYZ(string loginName)
        {
            DataTable dt = GetJYZByYHZH(loginName);
            DataTable newTeam = new DataTable();
            if (dt != null)
            {
                newTeam.Columns.Add("JYZID");
                newTeam.Columns.Add("JYZMC");
                //-----------------获取当前用户是教研组组长的所在教研组ID--------------
                using (SPSite site = new SPSite(SPContext.Current.Web.Url))
                {
                    using (SPWeb web = site.RootWeb)
                    {
                        //网站集根网站
                        SPList list = web.Lists.TryGetList("研修组");
                        if (list != null)
                        {
                            SPQuery query = new SPQuery();
                            query.Query = "<Where>"
                                + "<Eq><FieldRef Name='LeaderName'/><Value Type='Text'>" + loginName + "</Value></Eq>"
                                + "</Where>";
                            SPListItemCollection items = list.GetItems(query);
                            foreach (SPListItem item in items)
                            {
                                if (item["Title"] != null)
                                {
                                    newTeam = GetJYZ(item["Title"].ToString(), dt, newTeam);
                                }
                            }
                        }
                    }
                }
            }
            return newTeam;
        }

        private DataTable GetJYZ(string jyzID, DataTable dt, DataTable newTeam)
        {
            DataView dv = dt.DefaultView;
            dv.RowFilter = "JYZID='" + jyzID + "'";
            DataTable dvDt = dv.ToTable();
            DataRowCollection rows = dvDt.Rows;
            foreach (DataRow row in rows)
            {
                DataRow newDr = newTeam.NewRow();
                newDr["JYZID"] = row["JYZID"];
                newDr["JYZMC"] = row["JYZMC"];
                newTeam.Rows.Add(newDr);
            }
            return newTeam;
        }

        /// <summary>
        /// 返回当前用户是教研组组长的所有组成员
        /// 字段说明：JYZID-教研组ID、JYZMC-教研组名称、YHZH-教师账号、XM-教师姓名
        /// </summary>
        /// <param name="loginName">用户账号（无域，如admin）</param>
        /// <returns></returns>
        public DataTable GetLeadJYZMembers(string loginName)
        {
            DataTable membersDt = new DataTable();
            membersDt.Columns.Add("YHZH");//教师账号
            membersDt.Columns.Add("XM");//教师姓名
            membersDt.Columns.Add("JYZMC");//教研组名称
            membersDt.Columns.Add("JYZID");//教研组ID
            DataTable tempDt = new DataTable();
            DataRowCollection tempRows;
            DataTable leadJYZ = GetLeadJYZ(loginName);
            DataRowCollection rows = leadJYZ.Rows;
            foreach (DataRow row in rows)
            {
                if (row["JYZMC"] != null && row["JYZID"] != null)
                {
                    tempDt = GetMembersByJYZID(row["JYZID"].ToString());//根据教研组ID获取教研组所有成员
                    tempRows = tempDt.Rows;
                    foreach (DataRow tempRow in tempRows)
                    {
                        DataRow newRow = membersDt.NewRow();
                        newRow["YHZH"] = tempRow["YHZH"].ToString();//教师账号
                        newRow["XM"] = tempRow["XM"].ToString();//教师名称
                        newRow["JYZMC"] = row["JYZMC"].ToString();//教研组名称
                        newRow["JYZID"] = row["JYZID"].ToString();//教研组ID
                        membersDt.Rows.Add(newRow);
                    }
                }
            }
            return membersDt;
        }
    }
}
