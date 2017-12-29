using System;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using YHSD.VocationalEducation.Portal.Code.Entity;
using YHSD.VocationalEducation.Portal.Code.Common;
namespace YHSD.VocationalEducation.Portal.Code.Dao
{
    public class UserInfoDao
    {
        public void Add(UserInfo entity)
        {
            if (string.IsNullOrEmpty(entity.SystemStr))
                entity.SystemStr = CommonUtil.GetChildWebUrl();
            string[] strs=entity.SystemStr.Split(',');
            foreach (var item in strs)
            {
                //如果在系统已经存在,则直接将IsDelete改为0
                string sql = @"
                    DECLARE @isExits INT

                    SELECT @isExits=COUNT(*) FROM [UserInfo] WHERE Code=@Code
                    IF(@isExits>0)
                    BEGIN

                    UPDATE [UserInfo] SET Name=@Name,Sex=@Sex,Birthday=@Birthday,Mobile=@Mobile,Telephone=@Telephone,MSN=@MSN,QQ=@QQ,Email=@Email,ZipCode=@ZipCode,CardID=@CardID,EmergencyContact=@EmergencyContact,EmergencyTel=@EmergencyTel,IsDelete=@IsDelete WHERE Code=@Code

                    END
                    ELSE
                    BEGIN

                    INSERT INTO UserInfo (Id,Code,Name,DomainAccount,Sex,Birthday,EmployedDate,WorkDate,Nationality,Party,Degree,HouseHold,Mobile,Telephone,MSN,QQ,Email,EmergencyContact,EmergencyTel,Address,ZipCode,Photo,ImmediateSupervisor,IsDelete,NativePlace,Health,CardID,Professional,StaffLevel,LevelClass,ProbationEndDate,LaborContractStartDate,LaborContractEndDate,LaborContractType,Specialty,PhotoOne,PhotoTwo) VALUES(@Id,@Code,@Name,@DomainAccount,@Sex,@Birthday,@EmployedDate,@WorkDate,@Nationality,@Party,@Degree,@HouseHold,@Mobile,@Telephone,@MSN,@QQ,@Email,@EmergencyContact,@EmergencyTel,@Address,@ZipCode,@Photo,@ImmediateSupervisor,@IsDelete,@NativePlace,@Health,@CardID,@Professional,@StaffLevel,@LevelClass,@ProbationEndDate,@LaborContractStartDate,@LaborContractEndDate,@LaborContractType,@Specialty,@PhotoOne,@PhotoTwo)

                    END
                    ";

                //string sql = "INSERT INTO UserInfo (Id,Code,Name,DomainAccount,Sex,Birthday,EmployedDate,WorkDate,Nationality,Party,Degree,HouseHold,Mobile,Telephone,MSN,QQ,Email,EmergencyContact,EmergencyTel,Address,ZipCode,Photo,ImmediateSupervisor,IsDelete,NativePlace,Health,CardID,Professional,StaffLevel,LevelClass,ProbationEndDate,LaborContractStartDate,LaborContractEndDate,LaborContractType,Specialty,PhotoOne,PhotoTwo) "
                //+ " VALUES(@Id,@Code,@Name,@DomainAccount,@Sex,@Birthday,@EmployedDate,@WorkDate,@Nationality,@Party,@Degree,@HouseHold,@Mobile,@Telephone,@MSN,@QQ,@Email,@EmergencyContact,@EmergencyTel,@Address,@ZipCode,@Photo,@ImmediateSupervisor,@IsDelete,@NativePlace,@Health,@CardID,@Professional,@StaffLevel,@LevelClass,@ProbationEndDate,@LaborContractStartDate,@LaborContractEndDate,@LaborContractType,@Specialty,@PhotoOne,@PhotoTwo)";
                SqlConnection conn = ConnectionManager.GetConnection(item);
                SqlCommand cmd = new SqlCommand(sql, conn);
                if (!String.IsNullOrEmpty(entity.Id))
                    cmd.Parameters.Add("Id", SqlDbType.NVarChar).Value = entity.Id;
                else
                    cmd.Parameters.Add("Id", SqlDbType.NVarChar).Value = DBNull.Value;
                if (!String.IsNullOrEmpty(entity.Code))
                    cmd.Parameters.Add("Code", SqlDbType.NVarChar).Value = entity.Code;
                else
                    cmd.Parameters.Add("Code", SqlDbType.NVarChar).Value = DBNull.Value;
                if (!String.IsNullOrEmpty(entity.Name))
                    cmd.Parameters.Add("Name", SqlDbType.NVarChar).Value = entity.Name;
                else
                    cmd.Parameters.Add("Name", SqlDbType.NVarChar).Value = DBNull.Value;
                if (!String.IsNullOrEmpty(entity.DomainAccount))
                    cmd.Parameters.Add("DomainAccount", SqlDbType.NVarChar).Value = entity.DomainAccount;
                else
                    cmd.Parameters.Add("DomainAccount", SqlDbType.NVarChar).Value = DBNull.Value;
                if (!String.IsNullOrEmpty(entity.Sex))
                    cmd.Parameters.Add("Sex", SqlDbType.NVarChar).Value = entity.Sex;
                else
                    cmd.Parameters.Add("Sex", SqlDbType.NVarChar).Value = DBNull.Value;
                if (!String.IsNullOrEmpty(entity.Birthday))
                    cmd.Parameters.Add("Birthday", SqlDbType.NVarChar).Value = entity.Birthday;
                else
                    cmd.Parameters.Add("Birthday", SqlDbType.NVarChar).Value = DBNull.Value;
                if (!String.IsNullOrEmpty(entity.EmployedDate))
                    cmd.Parameters.Add("EmployedDate", SqlDbType.NVarChar).Value = entity.EmployedDate;
                else
                    cmd.Parameters.Add("EmployedDate", SqlDbType.NVarChar).Value = DBNull.Value;
                if (!String.IsNullOrEmpty(entity.WorkDate))
                    cmd.Parameters.Add("WorkDate", SqlDbType.NVarChar).Value = entity.WorkDate;
                else
                    cmd.Parameters.Add("WorkDate", SqlDbType.NVarChar).Value = DBNull.Value;
                if (!String.IsNullOrEmpty(entity.Nationality))
                    cmd.Parameters.Add("Nationality", SqlDbType.NVarChar).Value = entity.Nationality;
                else
                    cmd.Parameters.Add("Nationality", SqlDbType.NVarChar).Value = DBNull.Value;
                if (!String.IsNullOrEmpty(entity.Party))
                    cmd.Parameters.Add("Party", SqlDbType.NVarChar).Value = entity.Party;
                else
                    cmd.Parameters.Add("Party", SqlDbType.NVarChar).Value = DBNull.Value;
                if (!String.IsNullOrEmpty(entity.Degree))
                    cmd.Parameters.Add("Degree", SqlDbType.NVarChar).Value = entity.Degree;
                else
                    cmd.Parameters.Add("Degree", SqlDbType.NVarChar).Value = DBNull.Value;
                if (!String.IsNullOrEmpty(entity.HouseHold))
                    cmd.Parameters.Add("HouseHold", SqlDbType.NVarChar).Value = entity.HouseHold;
                else
                    cmd.Parameters.Add("HouseHold", SqlDbType.NVarChar).Value = DBNull.Value;
                if (!String.IsNullOrEmpty(entity.Mobile))
                    cmd.Parameters.Add("Mobile", SqlDbType.NVarChar).Value = entity.Mobile;
                else
                    cmd.Parameters.Add("Mobile", SqlDbType.NVarChar).Value = DBNull.Value;
                if (!String.IsNullOrEmpty(entity.Telephone))
                    cmd.Parameters.Add("Telephone", SqlDbType.NVarChar).Value = entity.Telephone;
                else
                    cmd.Parameters.Add("Telephone", SqlDbType.NVarChar).Value = DBNull.Value;
                if (!String.IsNullOrEmpty(entity.MSN))
                    cmd.Parameters.Add("MSN", SqlDbType.NVarChar).Value = entity.MSN;
                else
                    cmd.Parameters.Add("MSN", SqlDbType.NVarChar).Value = DBNull.Value;
                if (!String.IsNullOrEmpty(entity.QQ))
                    cmd.Parameters.Add("QQ", SqlDbType.NVarChar).Value = entity.QQ;
                else
                    cmd.Parameters.Add("QQ", SqlDbType.NVarChar).Value = DBNull.Value;
                if (!String.IsNullOrEmpty(entity.Email))
                    cmd.Parameters.Add("Email", SqlDbType.NVarChar).Value = entity.Email;
                else
                    cmd.Parameters.Add("Email", SqlDbType.NVarChar).Value = DBNull.Value;
                if (!String.IsNullOrEmpty(entity.EmergencyContact))
                    cmd.Parameters.Add("EmergencyContact", SqlDbType.NVarChar).Value = entity.EmergencyContact;
                else
                    cmd.Parameters.Add("EmergencyContact", SqlDbType.NVarChar).Value = DBNull.Value;
                if (!String.IsNullOrEmpty(entity.EmergencyTel))
                    cmd.Parameters.Add("EmergencyTel", SqlDbType.NVarChar).Value = entity.EmergencyTel;
                else
                    cmd.Parameters.Add("EmergencyTel", SqlDbType.NVarChar).Value = DBNull.Value;
                if (!String.IsNullOrEmpty(entity.Address))
                    cmd.Parameters.Add("Address", SqlDbType.NVarChar).Value = entity.Address;
                else
                    cmd.Parameters.Add("Address", SqlDbType.NVarChar).Value = DBNull.Value;
                if (!String.IsNullOrEmpty(entity.ZipCode))
                    cmd.Parameters.Add("ZipCode", SqlDbType.NVarChar).Value = entity.ZipCode;
                else
                    cmd.Parameters.Add("ZipCode", SqlDbType.NVarChar).Value = DBNull.Value;
                if (!String.IsNullOrEmpty(entity.Photo))
                    cmd.Parameters.Add("Photo", SqlDbType.NVarChar).Value = entity.Photo;
                else
                    cmd.Parameters.Add("Photo", SqlDbType.NVarChar).Value = DBNull.Value;
                if (!String.IsNullOrEmpty(entity.ImmediateSupervisor))
                    cmd.Parameters.Add("ImmediateSupervisor", SqlDbType.NVarChar).Value = entity.ImmediateSupervisor;
                else
                    cmd.Parameters.Add("ImmediateSupervisor", SqlDbType.NVarChar).Value = DBNull.Value;
                if (!String.IsNullOrEmpty(entity.IsDelete))
                    cmd.Parameters.Add("IsDelete", SqlDbType.NVarChar).Value = entity.IsDelete;
                else
                    cmd.Parameters.Add("IsDelete", SqlDbType.NVarChar).Value = DBNull.Value;
                if (!String.IsNullOrEmpty(entity.NativePlace))
                    cmd.Parameters.Add("NativePlace", SqlDbType.NVarChar).Value = entity.NativePlace;
                else
                    cmd.Parameters.Add("NativePlace", SqlDbType.NVarChar).Value = DBNull.Value;
                if (!String.IsNullOrEmpty(entity.Health))
                    cmd.Parameters.Add("Health", SqlDbType.NVarChar).Value = entity.Health;
                else
                    cmd.Parameters.Add("Health", SqlDbType.NVarChar).Value = DBNull.Value;
                if (!String.IsNullOrEmpty(entity.CardID))
                    cmd.Parameters.Add("CardID", SqlDbType.NVarChar).Value = entity.CardID;
                else
                    cmd.Parameters.Add("CardID", SqlDbType.NVarChar).Value = DBNull.Value;
                if (!String.IsNullOrEmpty(entity.Professional))
                    cmd.Parameters.Add("Professional", SqlDbType.NVarChar).Value = entity.Professional;
                else
                    cmd.Parameters.Add("Professional", SqlDbType.NVarChar).Value = DBNull.Value;
                if (!String.IsNullOrEmpty(entity.StaffLevel))
                    cmd.Parameters.Add("StaffLevel", SqlDbType.NVarChar).Value = entity.StaffLevel;
                else
                    cmd.Parameters.Add("StaffLevel", SqlDbType.NVarChar).Value = DBNull.Value;
                if (!String.IsNullOrEmpty(entity.LevelClass))
                    cmd.Parameters.Add("LevelClass", SqlDbType.NVarChar).Value = entity.LevelClass;
                else
                    cmd.Parameters.Add("LevelClass", SqlDbType.NVarChar).Value = DBNull.Value;
                if (!String.IsNullOrEmpty(entity.ProbationEndDate))
                    cmd.Parameters.Add("ProbationEndDate", SqlDbType.NVarChar).Value = entity.ProbationEndDate;
                else
                    cmd.Parameters.Add("ProbationEndDate", SqlDbType.NVarChar).Value = DBNull.Value;
                if (!String.IsNullOrEmpty(entity.LaborContractStartDate))
                    cmd.Parameters.Add("LaborContractStartDate", SqlDbType.NVarChar).Value = entity.LaborContractStartDate;
                else
                    cmd.Parameters.Add("LaborContractStartDate", SqlDbType.NVarChar).Value = DBNull.Value;
                if (!String.IsNullOrEmpty(entity.LaborContractEndDate))
                    cmd.Parameters.Add("LaborContractEndDate", SqlDbType.NVarChar).Value = entity.LaborContractEndDate;
                else
                    cmd.Parameters.Add("LaborContractEndDate", SqlDbType.NVarChar).Value = DBNull.Value;
                if (!String.IsNullOrEmpty(entity.LaborContractType))
                    cmd.Parameters.Add("LaborContractType", SqlDbType.NVarChar).Value = entity.LaborContractType;
                else
                    cmd.Parameters.Add("LaborContractType", SqlDbType.NVarChar).Value = DBNull.Value;
                if (!String.IsNullOrEmpty(entity.Specialty))
                    cmd.Parameters.Add("Specialty", SqlDbType.NVarChar).Value = entity.Specialty;
                else
                    cmd.Parameters.Add("Specialty", SqlDbType.NVarChar).Value = DBNull.Value;
                if (!String.IsNullOrEmpty(entity.PhotoOne))
                    cmd.Parameters.Add("PhotoOne", SqlDbType.NVarChar).Value = entity.PhotoOne;
                else
                    cmd.Parameters.Add("PhotoOne", SqlDbType.NVarChar).Value = DBNull.Value;
                if (!String.IsNullOrEmpty(entity.PhotoTwo))
                    cmd.Parameters.Add("PhotoTwo", SqlDbType.NVarChar).Value = entity.PhotoTwo;
                else
                    cmd.Parameters.Add("PhotoTwo", SqlDbType.NVarChar).Value = DBNull.Value;
                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        public UserInfo Get(String id)
        {
            UserInfo entity = new UserInfo();
            string sql = "select Id,Code,Name,(select Name from CompanyDepartment where id=(select DeptId from UserDept where UserId=UserInfo.Id)) as 'DeptName',DomainAccount,Sex,Birthday,EmployedDate,WorkDate,Nationality,Party,Degree,HouseHold,Mobile,Telephone,MSN,QQ,Email,EmergencyContact,EmergencyTel,Address,ZipCode,Photo,ImmediateSupervisor,IsDelete,NativePlace,Health,CardID,Professional,StaffLevel,LevelClass,ProbationEndDate,LaborContractStartDate,LaborContractEndDate,LaborContractType,Specialty,PhotoOne,PhotoTwo,(select PositionName from Position where Id=(select PositionId from UserPosition where UserId=UserInfo.Id)) as Role from UserInfo where Id = @Id and IsDelete=0";
            SqlConnection conn = ConnectionManager.GetConnection();
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.Add("Id", SqlDbType.NVarChar).Value = id;
            SqlDataReader rd = null;
            try
            {
                conn.Open();
                rd = cmd.ExecuteReader(CommandBehavior.SingleRow);
                while (rd.Read())
                {
                    entity.Id = MySqlDataReader.GetString(rd, "Id");
                    entity.Code = MySqlDataReader.GetString(rd, "Code");
                    entity.Name = MySqlDataReader.GetString(rd, "Name");
                    entity.DomainAccount = MySqlDataReader.GetString(rd, "DomainAccount");
                    entity.Sex = MySqlDataReader.GetString(rd, "Sex");
                    entity.Birthday = MySqlDataReader.GetString(rd, "Birthday");
                    entity.EmployedDate = MySqlDataReader.GetString(rd, "EmployedDate");
                    entity.WorkDate = MySqlDataReader.GetString(rd, "WorkDate");
                    entity.Nationality = MySqlDataReader.GetString(rd, "Nationality");
                    entity.Party = MySqlDataReader.GetString(rd, "Party");
                    entity.Degree = MySqlDataReader.GetString(rd, "Degree");
                    entity.HouseHold = MySqlDataReader.GetString(rd, "HouseHold");
                    entity.Mobile = MySqlDataReader.GetString(rd, "Mobile");
                    entity.Telephone = MySqlDataReader.GetString(rd, "Telephone");
                    entity.MSN = MySqlDataReader.GetString(rd, "MSN");
                    entity.QQ = MySqlDataReader.GetString(rd, "QQ");
                    entity.Email = MySqlDataReader.GetString(rd, "Email");
                    entity.EmergencyContact = MySqlDataReader.GetString(rd, "EmergencyContact");
                    entity.EmergencyTel = MySqlDataReader.GetString(rd, "EmergencyTel");
                    entity.Address = MySqlDataReader.GetString(rd, "Address");
                    entity.ZipCode = MySqlDataReader.GetString(rd, "ZipCode");
                    entity.Photo = MySqlDataReader.GetString(rd, "Photo");
                    entity.ImmediateSupervisor = MySqlDataReader.GetString(rd, "ImmediateSupervisor");
                    entity.IsDelete = MySqlDataReader.GetString(rd, "IsDelete");
                    entity.NativePlace = MySqlDataReader.GetString(rd, "NativePlace");
                    entity.Health = MySqlDataReader.GetString(rd, "Health");
                    entity.CardID = MySqlDataReader.GetString(rd, "CardID");
                    entity.Professional = MySqlDataReader.GetString(rd, "Professional");
                    entity.StaffLevel = MySqlDataReader.GetString(rd, "StaffLevel");
                    entity.LevelClass = MySqlDataReader.GetString(rd, "LevelClass");
                    entity.ProbationEndDate = MySqlDataReader.GetString(rd, "ProbationEndDate");
                    entity.LaborContractStartDate = MySqlDataReader.GetString(rd, "LaborContractStartDate");
                    entity.LaborContractEndDate = MySqlDataReader.GetString(rd, "LaborContractEndDate");
                    entity.LaborContractType = MySqlDataReader.GetString(rd, "LaborContractType");
                    entity.Specialty = MySqlDataReader.GetString(rd, "Specialty");
                    entity.PhotoOne = MySqlDataReader.GetString(rd, "PhotoOne");
                    entity.PhotoTwo = MySqlDataReader.GetString(rd, "PhotoTwo");
                    entity.Role = MySqlDataReader.GetString(rd, "Role");
                    entity.DeptName = MySqlDataReader.GetString(rd, "DeptName");
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (rd != null) rd.Close();
                conn.Close();
            }

            return entity;
        }
        public UserInfo GetByCode(String Code)
        {
            UserInfo entity = new UserInfo();
            string sql = "select Id,Code,Name,DomainAccount,Sex,Birthday,EmployedDate,WorkDate,Nationality,Party,Degree,HouseHold,Mobile,Telephone,MSN,QQ,Email,EmergencyContact,EmergencyTel,Address,ZipCode,Photo,ImmediateSupervisor,IsDelete,NativePlace,Health,CardID,Professional,StaffLevel,LevelClass,ProbationEndDate,LaborContractStartDate,LaborContractEndDate,LaborContractType,Specialty,PhotoOne,PhotoTwo from UserInfo where Code = @Code and IsDelete=0";
            SqlConnection conn = ConnectionManager.GetConnection();
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.Add("Code", SqlDbType.NVarChar).Value = Code;
            SqlDataReader rd = null;
            try
            {
                conn.Open();
                rd = cmd.ExecuteReader(CommandBehavior.SingleRow);
                while (rd.Read())
                {
                    entity.Id = MySqlDataReader.GetString(rd, "Id");
                    entity.Code = MySqlDataReader.GetString(rd, "Code");
                    entity.Name = MySqlDataReader.GetString(rd, "Name");
                    entity.DomainAccount = MySqlDataReader.GetString(rd, "DomainAccount");
                    entity.Sex = MySqlDataReader.GetString(rd, "Sex");
                    entity.Birthday = MySqlDataReader.GetString(rd, "Birthday");
                    entity.EmployedDate = MySqlDataReader.GetString(rd, "EmployedDate");
                    entity.WorkDate = MySqlDataReader.GetString(rd, "WorkDate");
                    entity.Nationality = MySqlDataReader.GetString(rd, "Nationality");
                    entity.Party = MySqlDataReader.GetString(rd, "Party");
                    entity.Degree = MySqlDataReader.GetString(rd, "Degree");
                    entity.HouseHold = MySqlDataReader.GetString(rd, "HouseHold");
                    entity.Mobile = MySqlDataReader.GetString(rd, "Mobile");
                    entity.Telephone = MySqlDataReader.GetString(rd, "Telephone");
                    entity.MSN = MySqlDataReader.GetString(rd, "MSN");
                    entity.QQ = MySqlDataReader.GetString(rd, "QQ");
                    entity.Email = MySqlDataReader.GetString(rd, "Email");
                    entity.EmergencyContact = MySqlDataReader.GetString(rd, "EmergencyContact");
                    entity.EmergencyTel = MySqlDataReader.GetString(rd, "EmergencyTel");
                    entity.Address = MySqlDataReader.GetString(rd, "Address");
                    entity.ZipCode = MySqlDataReader.GetString(rd, "ZipCode");
                    entity.Photo = MySqlDataReader.GetString(rd, "Photo");
                    entity.ImmediateSupervisor = MySqlDataReader.GetString(rd, "ImmediateSupervisor");
                    entity.IsDelete = MySqlDataReader.GetString(rd, "IsDelete");
                    entity.NativePlace = MySqlDataReader.GetString(rd, "NativePlace");
                    entity.Health = MySqlDataReader.GetString(rd, "Health");
                    entity.CardID = MySqlDataReader.GetString(rd, "CardID");
                    entity.Professional = MySqlDataReader.GetString(rd, "Professional");
                    entity.StaffLevel = MySqlDataReader.GetString(rd, "StaffLevel");
                    entity.LevelClass = MySqlDataReader.GetString(rd, "LevelClass");
                    entity.ProbationEndDate = MySqlDataReader.GetString(rd, "ProbationEndDate");
                    entity.LaborContractStartDate = MySqlDataReader.GetString(rd, "LaborContractStartDate");
                    entity.LaborContractEndDate = MySqlDataReader.GetString(rd, "LaborContractEndDate");
                    entity.LaborContractType = MySqlDataReader.GetString(rd, "LaborContractType");
                    entity.Specialty = MySqlDataReader.GetString(rd, "Specialty");
                    entity.PhotoOne = MySqlDataReader.GetString(rd, "PhotoOne");
                    entity.PhotoTwo = MySqlDataReader.GetString(rd, "PhotoTwo");
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (rd != null) rd.Close();
                conn.Close();
            }

            return entity;
        }
        public UserInfo GetCode(String Code)
        {
            UserInfo entity = new UserInfo();
            string sql = @"select Id,Code,Name,DomainAccount,Sex,Birthday,EmployedDate,WorkDate,Nationality,Party,Degree,HouseHold,Mobile,Telephone,MSN,QQ,Email,EmergencyContact,EmergencyTel,Address,ZipCode,Photo,ImmediateSupervisor,IsDelete,NativePlace,Health,CardID,Professional,StaffLevel,LevelClass,ProbationEndDate,LaborContractStartDate,LaborContractEndDate,LaborContractType,Specialty,PhotoOne,PhotoTwo,(SELECT Description FROM dbo.Position WHERE Id=(SELECT TOP 1 PositionId FROM dbo.UserPosition WHERE UserId=dbo.UserInfo.Id)) Role from UserInfo where IsDelete=0 AND Code = @Code";
            SqlConnection conn = ConnectionManager.GetConnection();
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.Add("Code", SqlDbType.NVarChar).Value = Code;
            SqlDataReader rd = null;
            try
            {
                conn.Open();
                rd = cmd.ExecuteReader(CommandBehavior.SingleRow);
                while (rd.Read())
                {
                    entity.Id = MySqlDataReader.GetString(rd, "Id");
                    entity.Code = MySqlDataReader.GetString(rd, "Code");
                    entity.Name = MySqlDataReader.GetString(rd, "Name");
                    entity.DomainAccount = MySqlDataReader.GetString(rd, "DomainAccount");
                    entity.Sex = MySqlDataReader.GetString(rd, "Sex");
                    entity.Birthday = MySqlDataReader.GetString(rd, "Birthday");
                    entity.EmployedDate = MySqlDataReader.GetString(rd, "EmployedDate");
                    entity.WorkDate = MySqlDataReader.GetString(rd, "WorkDate");
                    entity.Nationality = MySqlDataReader.GetString(rd, "Nationality");
                    entity.Party = MySqlDataReader.GetString(rd, "Party");
                    entity.Degree = MySqlDataReader.GetString(rd, "Degree");
                    entity.HouseHold = MySqlDataReader.GetString(rd, "HouseHold");
                    entity.Mobile = MySqlDataReader.GetString(rd, "Mobile");
                    entity.Telephone = MySqlDataReader.GetString(rd, "Telephone");
                    entity.MSN = MySqlDataReader.GetString(rd, "MSN");
                    entity.QQ = MySqlDataReader.GetString(rd, "QQ");
                    entity.Email = MySqlDataReader.GetString(rd, "Email");
                    entity.EmergencyContact = MySqlDataReader.GetString(rd, "EmergencyContact");
                    entity.EmergencyTel = MySqlDataReader.GetString(rd, "EmergencyTel");
                    entity.Address = MySqlDataReader.GetString(rd, "Address");
                    entity.ZipCode = MySqlDataReader.GetString(rd, "ZipCode");
                    entity.Photo = MySqlDataReader.GetString(rd, "Photo");
                    entity.ImmediateSupervisor = MySqlDataReader.GetString(rd, "ImmediateSupervisor");
                    entity.IsDelete = MySqlDataReader.GetString(rd, "IsDelete");
                    entity.NativePlace = MySqlDataReader.GetString(rd, "NativePlace");
                    entity.Health = MySqlDataReader.GetString(rd, "Health");
                    entity.CardID = MySqlDataReader.GetString(rd, "CardID");
                    entity.Professional = MySqlDataReader.GetString(rd, "Professional");
                    entity.StaffLevel = MySqlDataReader.GetString(rd, "StaffLevel");
                    entity.LevelClass = MySqlDataReader.GetString(rd, "LevelClass");
                    entity.ProbationEndDate = MySqlDataReader.GetString(rd, "ProbationEndDate");
                    entity.LaborContractStartDate = MySqlDataReader.GetString(rd, "LaborContractStartDate");
                    entity.LaborContractEndDate = MySqlDataReader.GetString(rd, "LaborContractEndDate");
                    entity.LaborContractType = MySqlDataReader.GetString(rd, "LaborContractType");
                    entity.Specialty = MySqlDataReader.GetString(rd, "Specialty");
                    entity.PhotoOne = MySqlDataReader.GetString(rd, "PhotoOne");
                    entity.PhotoTwo = MySqlDataReader.GetString(rd, "PhotoTwo");
                    entity.Role = MySqlDataReader.GetString(rd, "Role");
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (rd != null) rd.Close();
                conn.Close();
            }

            return entity;
        }
        public void Update(UserInfo entity)
        {
            if (string.IsNullOrEmpty(entity.SystemStr))
                entity.SystemStr = CommonUtil.GetChildWebUrl();
            string[] strs=entity.SystemStr.Split(',');
            foreach (var item in strs)
            {
                string sql = "UPDATE  UserInfo SET Id =@Id,Code =@Code,Name =@Name,DomainAccount =@DomainAccount,Sex =@Sex,Birthday =@Birthday,EmployedDate =@EmployedDate,WorkDate =@WorkDate,Nationality =@Nationality,Party =@Party,Degree =@Degree,HouseHold =@HouseHold,Mobile =@Mobile,Telephone =@Telephone,MSN =@MSN,QQ =@QQ,Email =@Email,EmergencyContact =@EmergencyContact,EmergencyTel =@EmergencyTel,Address =@Address,ZipCode =@ZipCode,Photo =@Photo,ImmediateSupervisor =@ImmediateSupervisor,NativePlace =@NativePlace,Health =@Health,CardID =@CardID,Professional =@Professional,StaffLevel =@StaffLevel,LevelClass =@LevelClass,ProbationEndDate =@ProbationEndDate,LaborContractStartDate =@LaborContractStartDate,LaborContractEndDate =@LaborContractEndDate,LaborContractType =@LaborContractType,Specialty =@Specialty,PhotoOne =@PhotoOne,PhotoTwo =@PhotoTwo where Id = @Id";
                SqlConnection conn = ConnectionManager.GetConnection(item);
                SqlCommand cmd = new SqlCommand(sql, conn);
                if (!String.IsNullOrEmpty(entity.Id))
                    cmd.Parameters.Add("Id", SqlDbType.NVarChar).Value = entity.Id;
                else
                    cmd.Parameters.Add("Id", SqlDbType.NVarChar).Value = DBNull.Value;
                if (!String.IsNullOrEmpty(entity.Code))
                    cmd.Parameters.Add("Code", SqlDbType.NVarChar).Value = entity.Code;
                else
                    cmd.Parameters.Add("Code", SqlDbType.NVarChar).Value = DBNull.Value;
                if (!String.IsNullOrEmpty(entity.Name))
                    cmd.Parameters.Add("Name", SqlDbType.NVarChar).Value = entity.Name;
                else
                    cmd.Parameters.Add("Name", SqlDbType.NVarChar).Value = DBNull.Value;
                if (!String.IsNullOrEmpty(entity.DomainAccount))
                    cmd.Parameters.Add("DomainAccount", SqlDbType.NVarChar).Value = entity.DomainAccount;
                else
                    cmd.Parameters.Add("DomainAccount", SqlDbType.NVarChar).Value = DBNull.Value;
                if (!String.IsNullOrEmpty(entity.Sex))
                    cmd.Parameters.Add("Sex", SqlDbType.NVarChar).Value = entity.Sex;
                else
                    cmd.Parameters.Add("Sex", SqlDbType.NVarChar).Value = DBNull.Value;
                if (!String.IsNullOrEmpty(entity.Birthday))
                    cmd.Parameters.Add("Birthday", SqlDbType.NVarChar).Value = entity.Birthday;
                else
                    cmd.Parameters.Add("Birthday", SqlDbType.NVarChar).Value = DBNull.Value;
                if (!String.IsNullOrEmpty(entity.EmployedDate))
                    cmd.Parameters.Add("EmployedDate", SqlDbType.NVarChar).Value = entity.EmployedDate;
                else
                    cmd.Parameters.Add("EmployedDate", SqlDbType.NVarChar).Value = DBNull.Value;
                if (!String.IsNullOrEmpty(entity.WorkDate))
                    cmd.Parameters.Add("WorkDate", SqlDbType.NVarChar).Value = entity.WorkDate;
                else
                    cmd.Parameters.Add("WorkDate", SqlDbType.NVarChar).Value = DBNull.Value;
                if (!String.IsNullOrEmpty(entity.Nationality))
                    cmd.Parameters.Add("Nationality", SqlDbType.NVarChar).Value = entity.Nationality;
                else
                    cmd.Parameters.Add("Nationality", SqlDbType.NVarChar).Value = DBNull.Value;
                if (!String.IsNullOrEmpty(entity.Party))
                    cmd.Parameters.Add("Party", SqlDbType.NVarChar).Value = entity.Party;
                else
                    cmd.Parameters.Add("Party", SqlDbType.NVarChar).Value = DBNull.Value;
                if (!String.IsNullOrEmpty(entity.Degree))
                    cmd.Parameters.Add("Degree", SqlDbType.NVarChar).Value = entity.Degree;
                else
                    cmd.Parameters.Add("Degree", SqlDbType.NVarChar).Value = DBNull.Value;
                if (!String.IsNullOrEmpty(entity.HouseHold))
                    cmd.Parameters.Add("HouseHold", SqlDbType.NVarChar).Value = entity.HouseHold;
                else
                    cmd.Parameters.Add("HouseHold", SqlDbType.NVarChar).Value = DBNull.Value;
                if (!String.IsNullOrEmpty(entity.Mobile))
                    cmd.Parameters.Add("Mobile", SqlDbType.NVarChar).Value = entity.Mobile;
                else
                    cmd.Parameters.Add("Mobile", SqlDbType.NVarChar).Value = DBNull.Value;
                if (!String.IsNullOrEmpty(entity.Telephone))
                    cmd.Parameters.Add("Telephone", SqlDbType.NVarChar).Value = entity.Telephone;
                else
                    cmd.Parameters.Add("Telephone", SqlDbType.NVarChar).Value = DBNull.Value;
                if (!String.IsNullOrEmpty(entity.MSN))
                    cmd.Parameters.Add("MSN", SqlDbType.NVarChar).Value = entity.MSN;
                else
                    cmd.Parameters.Add("MSN", SqlDbType.NVarChar).Value = DBNull.Value;
                if (!String.IsNullOrEmpty(entity.QQ))
                    cmd.Parameters.Add("QQ", SqlDbType.NVarChar).Value = entity.QQ;
                else
                    cmd.Parameters.Add("QQ", SqlDbType.NVarChar).Value = DBNull.Value;
                if (!String.IsNullOrEmpty(entity.Email))
                    cmd.Parameters.Add("Email", SqlDbType.NVarChar).Value = entity.Email;
                else
                    cmd.Parameters.Add("Email", SqlDbType.NVarChar).Value = DBNull.Value;
                if (!String.IsNullOrEmpty(entity.EmergencyContact))
                    cmd.Parameters.Add("EmergencyContact", SqlDbType.NVarChar).Value = entity.EmergencyContact;
                else
                    cmd.Parameters.Add("EmergencyContact", SqlDbType.NVarChar).Value = DBNull.Value;
                if (!String.IsNullOrEmpty(entity.EmergencyTel))
                    cmd.Parameters.Add("EmergencyTel", SqlDbType.NVarChar).Value = entity.EmergencyTel;
                else
                    cmd.Parameters.Add("EmergencyTel", SqlDbType.NVarChar).Value = DBNull.Value;
                if (!String.IsNullOrEmpty(entity.Address))
                    cmd.Parameters.Add("Address", SqlDbType.NVarChar).Value = entity.Address;
                else
                    cmd.Parameters.Add("Address", SqlDbType.NVarChar).Value = DBNull.Value;
                if (!String.IsNullOrEmpty(entity.ZipCode))
                    cmd.Parameters.Add("ZipCode", SqlDbType.NVarChar).Value = entity.ZipCode;
                else
                    cmd.Parameters.Add("ZipCode", SqlDbType.NVarChar).Value = DBNull.Value;
                if (!String.IsNullOrEmpty(entity.Photo))
                    cmd.Parameters.Add("Photo", SqlDbType.NVarChar).Value = entity.Photo;
                else
                    cmd.Parameters.Add("Photo", SqlDbType.NVarChar).Value = DBNull.Value;
                if (!String.IsNullOrEmpty(entity.ImmediateSupervisor))
                    cmd.Parameters.Add("ImmediateSupervisor", SqlDbType.NVarChar).Value = entity.ImmediateSupervisor;
                else
                    cmd.Parameters.Add("ImmediateSupervisor", SqlDbType.NVarChar).Value = DBNull.Value;
                if (!String.IsNullOrEmpty(entity.NativePlace))
                    cmd.Parameters.Add("NativePlace", SqlDbType.NVarChar).Value = entity.NativePlace;
                else
                    cmd.Parameters.Add("NativePlace", SqlDbType.NVarChar).Value = DBNull.Value;
                if (!String.IsNullOrEmpty(entity.Health))
                    cmd.Parameters.Add("Health", SqlDbType.NVarChar).Value = entity.Health;
                else
                    cmd.Parameters.Add("Health", SqlDbType.NVarChar).Value = DBNull.Value;
                if (!String.IsNullOrEmpty(entity.CardID))
                    cmd.Parameters.Add("CardID", SqlDbType.NVarChar).Value = entity.CardID;
                else
                    cmd.Parameters.Add("CardID", SqlDbType.NVarChar).Value = DBNull.Value;
                if (!String.IsNullOrEmpty(entity.Professional))
                    cmd.Parameters.Add("Professional", SqlDbType.NVarChar).Value = entity.Professional;
                else
                    cmd.Parameters.Add("Professional", SqlDbType.NVarChar).Value = DBNull.Value;
                if (!String.IsNullOrEmpty(entity.StaffLevel))
                    cmd.Parameters.Add("StaffLevel", SqlDbType.NVarChar).Value = entity.StaffLevel;
                else
                    cmd.Parameters.Add("StaffLevel", SqlDbType.NVarChar).Value = DBNull.Value;
                if (!String.IsNullOrEmpty(entity.LevelClass))
                    cmd.Parameters.Add("LevelClass", SqlDbType.NVarChar).Value = entity.LevelClass;
                else
                    cmd.Parameters.Add("LevelClass", SqlDbType.NVarChar).Value = DBNull.Value;
                if (!String.IsNullOrEmpty(entity.ProbationEndDate))
                    cmd.Parameters.Add("ProbationEndDate", SqlDbType.NVarChar).Value = entity.ProbationEndDate;
                else
                    cmd.Parameters.Add("ProbationEndDate", SqlDbType.NVarChar).Value = DBNull.Value;
                if (!String.IsNullOrEmpty(entity.LaborContractStartDate))
                    cmd.Parameters.Add("LaborContractStartDate", SqlDbType.NVarChar).Value = entity.LaborContractStartDate;
                else
                    cmd.Parameters.Add("LaborContractStartDate", SqlDbType.NVarChar).Value = DBNull.Value;
                if (!String.IsNullOrEmpty(entity.LaborContractEndDate))
                    cmd.Parameters.Add("LaborContractEndDate", SqlDbType.NVarChar).Value = entity.LaborContractEndDate;
                else
                    cmd.Parameters.Add("LaborContractEndDate", SqlDbType.NVarChar).Value = DBNull.Value;
                if (!String.IsNullOrEmpty(entity.LaborContractType))
                    cmd.Parameters.Add("LaborContractType", SqlDbType.NVarChar).Value = entity.LaborContractType;
                else
                    cmd.Parameters.Add("LaborContractType", SqlDbType.NVarChar).Value = DBNull.Value;
                if (!String.IsNullOrEmpty(entity.Specialty))
                    cmd.Parameters.Add("Specialty", SqlDbType.NVarChar).Value = entity.Specialty;
                else
                    cmd.Parameters.Add("Specialty", SqlDbType.NVarChar).Value = DBNull.Value;
                if (!String.IsNullOrEmpty(entity.PhotoOne))
                    cmd.Parameters.Add("PhotoOne", SqlDbType.NVarChar).Value = entity.PhotoOne;
                else
                    cmd.Parameters.Add("PhotoOne", SqlDbType.NVarChar).Value = DBNull.Value;
                if (!String.IsNullOrEmpty(entity.PhotoTwo))
                    cmd.Parameters.Add("PhotoTwo", SqlDbType.NVarChar).Value = entity.PhotoTwo;
                else
                    cmd.Parameters.Add("PhotoTwo", SqlDbType.NVarChar).Value = DBNull.Value;
                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    conn.Close();
                }
            }

        }

        public int FindNum(UserInfo entity)
        {
            int num = 0;
            StringBuilder sql = new StringBuilder("select count(*)  from UserInfo ui INNER JOIN dbo.[UserPosition] up ON ui.Id=up.UserId INNER JOIN dbo.Position p ON p.Id = up.PositionId LEFT JOIN dbo.ClassUser cu ON ui.id=cu.UId LEFT JOIN dbo.ClassInfo ci ON cu.CId=ci.ID  WHERE 1=1 and ui.isDelete=0 ");//查询学生
            if (entity != null && !string.IsNullOrEmpty(entity.Name))
            {
                sql.AppendFormat(" and ui.Name like '%'+@Name+'%'");
            }
            if (entity != null && !string.IsNullOrEmpty(entity.Role))
            {
                sql.AppendFormat(" and p.Description = @PositionName");
            }
            if (entity != null && !string.IsNullOrEmpty(entity.ClassID))
            {
                sql.AppendFormat(" and ci.ID = @ClassID");
            }
            SqlConnection conn = ConnectionManager.GetConnection();
            SqlCommand cmd = new SqlCommand(sql.ToString(), conn);
            if (entity != null && !string.IsNullOrEmpty(entity.Name))
            {
                cmd.Parameters.Add("@Name", SqlDbType.NVarChar).Value = entity.Name;
            }
            if (entity != null && !string.IsNullOrEmpty(entity.Role))
            {
                cmd.Parameters.Add("@PositionName", SqlDbType.NVarChar).Value = entity.Role;
            }
            if (entity != null && !string.IsNullOrEmpty(entity.ClassID))
            {
                cmd.Parameters.Add("@ClassID", SqlDbType.NVarChar).Value = entity.ClassID;
            }
            SqlDataReader rd = null;
            try
            {
                conn.Open();
                rd = cmd.ExecuteReader(CommandBehavior.SingleRow);
                if (rd.Read())
                {
                    num = rd.GetInt32(0);
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (rd != null) rd.Close();
                conn.Close();
            }

            return num;
        }
        public int FindNotInRoleNum(UserInfo entity)
        {
            int num = 0;

            StringBuilder sql = new StringBuilder(@"SELECT count(1)
                FROM    UserInfo ui
                        LEFT JOIN dbo.UserDept ud ON ud.UserId = ui.Id
                WHERE   1 = 1
                        AND ui.IsDelete = 0  and ui.Id NOT IN (SELECT UserId FROM dbo.UserPosition) ");
            if (entity != null && !string.IsNullOrEmpty(entity.Name))
            {
                sql.AppendFormat(" and ui.Name like '%'+@Name+'%'");
            }
            if (entity != null && !string.IsNullOrEmpty(entity.DeptID))
            {
                sql.AppendFormat(" and ud.DeptID = @DeptID");
            }
            
            SqlConnection conn = ConnectionManager.GetConnection();
            SqlCommand cmd = new SqlCommand(sql.ToString(), conn);
            if (entity != null && !string.IsNullOrEmpty(entity.Name))
            {
                cmd.Parameters.Add("@Name", SqlDbType.NVarChar).Value = entity.Name;
            }
            if (entity != null && !string.IsNullOrEmpty(entity.DeptID))
            {
                cmd.Parameters.Add("@DeptID", SqlDbType.NVarChar).Value = entity.DeptID;
            }
            
            SqlDataReader rd = null;
            try
            {
                conn.Open();
                rd = cmd.ExecuteReader(CommandBehavior.SingleRow);
                if (rd.Read())
                {
                    num = rd.GetInt32(0);
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (rd != null) rd.Close();
                conn.Close();
            }

            return num;
        }
        public int FindNotInNum(UserInfo entity)
        {
            int num = 0;

            //select count(*)  from UserInfo ui INNER JOIN dbo.[UserPosition] up ON ui.Id=up.UserId INNER JOIN dbo.Position p ON p.Id = up.PositionId where 1=1 and ui.isDelete=0 AND p.PositionName='学生'
            //StringBuilder sql = new StringBuilder("select count(*)  from UserInfo where 1=1 and isDelete=0 ");
            //StringBuilder sql = new StringBuilder("select count(*)  from UserInfo ui INNER JOIN dbo.[UserPosition] up ON ui.Id=up.UserId INNER JOIN dbo.Position p ON p.Id = up.PositionId where 1=1 and ui.isDelete=0 ");//查询学生
            //StringBuilder sql = new StringBuilder("select count(*)  from UserInfo ui INNER JOIN dbo.[UserPosition] up ON ui.Id=up.UserId INNER JOIN dbo.Position p ON p.Id = up.PositionId LEFT JOIN (SELECT distinct uid,cid from dbo.ClassUser) cu ON ui.id=cu.UId LEFT JOIN dbo.ClassInfo ci ON cu.CId=ci.ID  LEFT JOIN dbo.UserDept ud ON ud.UserId=ui.Id WHERE 1=1 and ui.isDelete=0 ");//查询学生
            StringBuilder sql = new StringBuilder(@"SELECT count(1)
                FROM    UserInfo ui
                        LEFT JOIN dbo.[UserPosition] up ON ui.Id = up.UserId
                        LEFT JOIN dbo.Position p ON p.Id = up.PositionId
                        LEFT JOIN dbo.UserDept ud ON ud.UserId = ui.Id
                WHERE   1 = 1
                        AND ui.IsDelete = 0 ");
            if (entity != null && !string.IsNullOrEmpty(entity.Name))
            {
                sql.AppendFormat(" and ui.Name like '%'+@Name+'%'");
            }
            if (entity != null && !string.IsNullOrEmpty(entity.CardID))
            {
                sql.AppendFormat(" and ui.CardID like '%'+@CardID+'%'");
            }
            if (entity != null && !string.IsNullOrEmpty(entity.Role))
            {
                sql.AppendFormat(" and p.Description = @PositionName");
            }
            if (entity != null && !string.IsNullOrEmpty(entity.ClassID))
            {
                sql.AppendFormat(@" and ui.Id IN (
			                        SELECT  uid
			                        FROM    classuser
			                        WHERE   cid = @ClassID
                                )");
            }
            if (entity != null && !string.IsNullOrEmpty(entity.NotInClassID))
            {
                sql.AppendFormat(@" and ui.Id NOT IN (
			                        SELECT DISTINCT uid
			                        FROM    classuser
                                )");
            }
            if (entity != null && !string.IsNullOrEmpty(entity.DeptID))
            {
                sql.Insert(0, string.Format(@"
                    WITH   CTE
                             AS ( SELECT   ID
                                  FROM     CompanyDepartment
                                  WHERE    Id = @DeptID
                                  UNION ALL
                                  SELECT   dbo.CompanyDepartment.ID
                                  FROM     CompanyDepartment
                                           INNER JOIN CTE ON CompanyDepartment.ParentId = CTE.ID
                                )"));
                sql.AppendFormat("AND ud.DeptId IN ( SELECT ID FROM CTE )");

            }
            //if (entity != null && !string.IsNullOrEmpty(entity.DeptID))
            //{
            //    sql.AppendFormat(" and ud.DeptID = @DeptID");
            //}
            SqlConnection conn = ConnectionManager.GetConnection();
            SqlCommand cmd = new SqlCommand(sql.ToString(), conn);
            if (entity != null && !string.IsNullOrEmpty(entity.Name))
            {
                cmd.Parameters.Add("@Name", SqlDbType.NVarChar).Value = entity.Name;
            }
            if (entity != null && !string.IsNullOrEmpty(entity.CardID))
            {
                cmd.Parameters.Add("@CardID", SqlDbType.NVarChar).Value = entity.CardID;
            }
            if (entity != null && !string.IsNullOrEmpty(entity.Role))
            {
                cmd.Parameters.Add("@PositionName", SqlDbType.NVarChar).Value = entity.Role;
            }
            if (entity != null && !string.IsNullOrEmpty(entity.ClassID))
            {
                cmd.Parameters.Add("@ClassID", SqlDbType.NVarChar).Value = entity.ClassID;
            }
            if (entity != null && !string.IsNullOrEmpty(entity.NotInClassID))
            {
                cmd.Parameters.Add("@NotInClassID", SqlDbType.NVarChar).Value = entity.NotInClassID;
            }
            if (entity != null && !string.IsNullOrEmpty(entity.DeptID))
            {
                cmd.Parameters.Add("@DeptID", SqlDbType.NVarChar).Value = entity.DeptID;
            }
            SqlDataReader rd = null;
            try
            {
                conn.Open();
                rd = cmd.ExecuteReader(CommandBehavior.SingleRow);
                if (rd.Read())
                {
                    num = rd.GetInt32(0);
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (rd != null) rd.Close();
                conn.Close();
            }

            return num;
        }

        /// <summary>
        /// 此方法用于查询与班级关联的学生,ID为关联ID,不适于查询学员信息
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="firstResult"></param>
        /// <param name="maxResults"></param>
        /// <returns></returns>
        public List<UserInfo> Find(UserInfo entity, int firstResult, int maxResults)
        {
            List<UserInfo> list = new List<UserInfo>();
            StringBuilder sql = new StringBuilder("select ui.Id,cu.Id CUID,ci.ID CIID,Code,ui.Name,DomainAccount,Sex,Birthday,EmployedDate,WorkDate,Nationality,Party,Degree,HouseHold,Mobile,Telephone,MSN,QQ,Email,EmergencyContact,EmergencyTel,Address,ZipCode,Photo,ImmediateSupervisor,NativePlace,Health,CardID,Professional,StaffLevel,LevelClass,ProbationEndDate,LaborContractStartDate,LaborContractEndDate,LaborContractType,Specialty,PhotoOne,PhotoTwo from UserInfo ui INNER JOIN dbo.[UserPosition] up ON ui.Id=up.UserId INNER JOIN dbo.Position p ON p.Id = up.PositionId Left JOIN dbo.ClassUser cu ON cu.UId = ui.Id LEFT JOIN dbo.ClassInfo ci ON ci.ID = cu.CId where 1=1 and ui.IsDelete=0 ");
            if (entity != null && !string.IsNullOrEmpty(entity.Name))
            {
                sql.AppendFormat(" and ui.Name like '%'+@Name+'%'");
            }
            if (entity != null && !string.IsNullOrEmpty(entity.Role))
            {
                sql.AppendFormat(" and p.Description = @PositionName");
            }
            if (entity != null && !string.IsNullOrEmpty(entity.ClassID))
            {
                sql.AppendFormat(" and ci.ID = @ClassID");
            }
            SqlConnection conn = ConnectionManager.GetConnection();
            SqlCommand cmd = new SqlCommand(sql.ToString(), conn);
            if (entity != null && !string.IsNullOrEmpty(entity.Name))
            {
                cmd.Parameters.Add("@Name", SqlDbType.NVarChar).Value = entity.Name;
            }
            if (entity != null && !string.IsNullOrEmpty(entity.Role))
            {
                cmd.Parameters.Add("@PositionName", SqlDbType.NVarChar).Value = entity.Role;
            }
            if (entity != null && !string.IsNullOrEmpty(entity.ClassID))
            {
                cmd.Parameters.Add("@ClassID", SqlDbType.NVarChar).Value = entity.ClassID;
            }
            SqlDataReader rd = null;
            try
            {
                conn.Open();
                int i = 0;
                rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    if (i >= firstResult + maxResults && firstResult > 0)
                    {
                        break;
                    }
                    if ((i >= firstResult && i < (firstResult + maxResults)) || firstResult < 0)
                    {
                        UserInfo userInfo = new UserInfo();
                        userInfo.Id = MySqlDataReader.GetString(rd, "Id");
                        userInfo.CUID = MySqlDataReader.GetString(rd, "CUID");
                        userInfo.Code = MySqlDataReader.GetString(rd, "Code");
                        userInfo.Name = MySqlDataReader.GetString(rd, "Name");
                        userInfo.DomainAccount = MySqlDataReader.GetString(rd, "DomainAccount");
                        userInfo.Sex = MySqlDataReader.GetString(rd, "Sex");
                        userInfo.Birthday = MySqlDataReader.GetString(rd, "Birthday");
                        userInfo.EmployedDate = MySqlDataReader.GetString(rd, "EmployedDate");
                        userInfo.WorkDate = MySqlDataReader.GetString(rd, "WorkDate");
                        userInfo.Nationality = MySqlDataReader.GetString(rd, "Nationality");
                        userInfo.Party = MySqlDataReader.GetString(rd, "Party");
                        userInfo.Degree = MySqlDataReader.GetString(rd, "Degree");
                        userInfo.HouseHold = MySqlDataReader.GetString(rd, "HouseHold");
                        userInfo.Mobile = MySqlDataReader.GetString(rd, "Mobile");
                        userInfo.Telephone = MySqlDataReader.GetString(rd, "Telephone");
                        userInfo.MSN = MySqlDataReader.GetString(rd, "MSN");
                        userInfo.QQ = MySqlDataReader.GetString(rd, "QQ");
                        userInfo.Email = MySqlDataReader.GetString(rd, "Email");
                        userInfo.EmergencyContact = MySqlDataReader.GetString(rd, "EmergencyContact");
                        userInfo.EmergencyTel = MySqlDataReader.GetString(rd, "EmergencyTel");
                        userInfo.Address = MySqlDataReader.GetString(rd, "Address");
                        userInfo.ZipCode = MySqlDataReader.GetString(rd, "ZipCode");
                        userInfo.Photo = MySqlDataReader.GetString(rd, "Photo");
                        userInfo.ImmediateSupervisor = MySqlDataReader.GetString(rd, "ImmediateSupervisor");
                        userInfo.NativePlace = MySqlDataReader.GetString(rd, "NativePlace");
                        userInfo.Health = MySqlDataReader.GetString(rd, "Health");
                        userInfo.CardID = MySqlDataReader.GetString(rd, "CardID");
                        userInfo.Professional = MySqlDataReader.GetString(rd, "Professional");
                        userInfo.StaffLevel = MySqlDataReader.GetString(rd, "StaffLevel");
                        userInfo.LevelClass = MySqlDataReader.GetString(rd, "LevelClass");
                        userInfo.ProbationEndDate = MySqlDataReader.GetString(rd, "ProbationEndDate");
                        userInfo.LaborContractStartDate = MySqlDataReader.GetString(rd, "LaborContractStartDate");
                        userInfo.LaborContractEndDate = MySqlDataReader.GetString(rd, "LaborContractEndDate");
                        userInfo.LaborContractType = MySqlDataReader.GetString(rd, "LaborContractType");
                        userInfo.Specialty = MySqlDataReader.GetString(rd, "Specialty");
                        userInfo.PhotoOne = MySqlDataReader.GetString(rd, "PhotoOne");
                        userInfo.PhotoTwo = MySqlDataReader.GetString(rd, "PhotoTwo");
                        userInfo.ClassID=MySqlDataReader.GetString(rd,"CIID");
                        if (!string.IsNullOrEmpty(userInfo.ClassID))
                        {
                            if (Convert.ToInt32(ConnectionManager.GetSingle("select count(*) from Chapter where CurriculumID in (select CurriculumID from ClassCurriculum where ClassID='" + userInfo.ClassID + "' and IsDelete=0)  and IsDelete=0")) > 0)
                            {
                                userInfo.Percentage = ConnectionManager.GetSingle("select (select count(*) from Chapter where CurriculumID in (select CurriculumID from ClassCurriculum where ClassID='" + userInfo.ClassID + "'and IsDelete=0) and IsDelete=0 and id in(select TableID from ClickDetail where Userid='" + userInfo.Id + "' and TableName='Chapter' ))*100/(select count(*) from Chapter where CurriculumID in (select CurriculumID from ClassCurriculum where ClassID='" + userInfo.ClassID + "' and IsDelete=0) and IsDelete=0)").ToString();
                            }
                            else
                            {
                                userInfo.Percentage = "尚未分配课程";
                            }
                        }
                        list.Add(userInfo);
                    }
                    i++;
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (rd != null) rd.Close();
                conn.Close();
            }

            return list;
        }
        public List<UserInfo> FindNotInClass(UserInfo entity, int firstResult, int maxResults)
        {
            List<UserInfo> list = new List<UserInfo>();
            StringBuilder sql = new StringBuilder(@"
                SELECT  ui.Id ,
                        Code ,
                        ui.Name ,
                        DomainAccount ,
                        Sex ,
                        Birthday ,
                        EmployedDate ,
                        WorkDate ,
                        Nationality ,
                        Party ,
                        Degree ,
                        HouseHold ,
                        Mobile ,
                        Telephone ,
                        MSN ,
                        QQ ,
                        Email ,
                        EmergencyContact ,
                        EmergencyTel ,
                        Address ,
                        ZipCode ,
                        Photo ,
                        ImmediateSupervisor ,
                        NativePlace ,
                        Health ,
                        CardID ,
                        Professional ,
                        StaffLevel ,
                        LevelClass ,
                        ProbationEndDate ,
                        LaborContractStartDate ,
                        LaborContractEndDate ,
                        LaborContractType ,
                        Specialty ,
                        PhotoOne ,
                        PhotoTwo
                FROM    UserInfo ui
                        LEFT JOIN dbo.[UserPosition] up ON ui.Id = up.UserId
                        LEFT JOIN dbo.Position p ON p.Id = up.PositionId
                        LEFT JOIN dbo.UserDept ud ON ud.UserId = ui.Id
                WHERE   1 = 1
                        AND ui.IsDelete = 0 ");
            if (entity != null && !string.IsNullOrEmpty(entity.Name))
            {
                sql.AppendFormat(" and ui.Name like '%'+@Name+'%'");
            }
            if (entity != null && !string.IsNullOrEmpty(entity.CardID))
            {
                sql.AppendFormat(" and ui.CardID like '%'+@CardID+'%'");
            }
            if (entity != null && !string.IsNullOrEmpty(entity.Role))
            {
                sql.AppendFormat(" and p.Description = @PositionName");
            }
            if (entity != null && !string.IsNullOrEmpty(entity.ClassID))
            {
                sql.AppendFormat(@" and ui.Id IN (
			                        SELECT  uid
			                        FROM    classuser
			                        WHERE   cid = @ClassID
                                )");
            }
            if (entity != null && !string.IsNullOrEmpty(entity.NotInClassID))
            {
                //sql.AppendFormat(@" and ui.Id NOT IN (
                //  SELECT  uid
                //  FROM    classuser
                //  WHERE   cid = @NotInClassID
                //)");
                //改为查询没有班级的学员,而不是查询不在某个班级的学员--即一个学生同时只能在一个班级
                sql.AppendFormat(@" and ui.Id NOT IN (
			                        SELECT DISTINCT uid
			                        FROM    classuser
                                )");
            }
            if (entity != null && !string.IsNullOrEmpty(entity.DeptID))
            {
                sql.Insert(0, string.Format(@"
                    WITH   CTE
                             AS ( SELECT   ID
                                  FROM     CompanyDepartment
                                  WHERE    Id = @DeptID
                                  UNION ALL
                                  SELECT   dbo.CompanyDepartment.ID
                                  FROM     CompanyDepartment
                                           INNER JOIN CTE ON CompanyDepartment.ParentId = CTE.ID
                                )"));
                sql.AppendFormat("AND ud.DeptId IN ( SELECT ID FROM CTE )");

            }
            SqlConnection conn = ConnectionManager.GetConnection();
            SqlCommand cmd = new SqlCommand(sql.ToString(), conn);
            if (entity != null && !string.IsNullOrEmpty(entity.Name))
            {
                cmd.Parameters.Add("@Name", SqlDbType.NVarChar).Value = entity.Name;
            }
            if (entity != null && !string.IsNullOrEmpty(entity.CardID))
            {
                cmd.Parameters.Add("@CardID", SqlDbType.NVarChar).Value = entity.CardID;
            }
            if (entity != null && !string.IsNullOrEmpty(entity.Role))
            {
                cmd.Parameters.Add("@PositionName", SqlDbType.NVarChar).Value = entity.Role;
            }
            if (entity != null && !string.IsNullOrEmpty(entity.ClassID))
            {
                cmd.Parameters.Add("@ClassID", SqlDbType.NVarChar).Value = entity.ClassID;
            }
            if (entity != null && !string.IsNullOrEmpty(entity.NotInClassID))
            {
                cmd.Parameters.Add("@NotInClassID", SqlDbType.NVarChar).Value = entity.NotInClassID;
            }
            if (entity != null && !string.IsNullOrEmpty(entity.DeptID))
            {
                cmd.Parameters.Add("@DeptID", SqlDbType.NVarChar).Value = entity.DeptID;
            }
            SqlDataReader rd = null;
            try
            {
                conn.Open();
                int i = 0;
                rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    if (i >= firstResult + maxResults && firstResult > 0)
                    {
                        break;
                    }
                    if ((i >= firstResult && i < (firstResult + maxResults)) || firstResult < 0)
                    {
                        UserInfo userInfo = new UserInfo();
                        userInfo.Id = MySqlDataReader.GetString(rd, "Id");
                        userInfo.Code = MySqlDataReader.GetString(rd, "Code");
                        userInfo.Name = MySqlDataReader.GetString(rd, "Name");
                        userInfo.DomainAccount = MySqlDataReader.GetString(rd, "DomainAccount");
                        userInfo.Sex = MySqlDataReader.GetString(rd, "Sex");
                        userInfo.Birthday = MySqlDataReader.GetString(rd, "Birthday");
                        userInfo.EmployedDate = MySqlDataReader.GetString(rd, "EmployedDate");
                        userInfo.WorkDate = MySqlDataReader.GetString(rd, "WorkDate");
                        userInfo.Nationality = MySqlDataReader.GetString(rd, "Nationality");
                        userInfo.Party = MySqlDataReader.GetString(rd, "Party");
                        userInfo.Degree = MySqlDataReader.GetString(rd, "Degree");
                        userInfo.HouseHold = MySqlDataReader.GetString(rd, "HouseHold");
                        userInfo.Mobile = MySqlDataReader.GetString(rd, "Mobile");
                        userInfo.Telephone = MySqlDataReader.GetString(rd, "Telephone");
                        userInfo.MSN = MySqlDataReader.GetString(rd, "MSN");
                        userInfo.QQ = MySqlDataReader.GetString(rd, "QQ");
                        userInfo.Email = MySqlDataReader.GetString(rd, "Email");
                        userInfo.EmergencyContact = MySqlDataReader.GetString(rd, "EmergencyContact");
                        userInfo.EmergencyTel = MySqlDataReader.GetString(rd, "EmergencyTel");
                        userInfo.Address = MySqlDataReader.GetString(rd, "Address");
                        userInfo.ZipCode = MySqlDataReader.GetString(rd, "ZipCode");
                        userInfo.Photo = MySqlDataReader.GetString(rd, "Photo");
                        userInfo.ImmediateSupervisor = MySqlDataReader.GetString(rd, "ImmediateSupervisor");
                        userInfo.NativePlace = MySqlDataReader.GetString(rd, "NativePlace");
                        userInfo.Health = MySqlDataReader.GetString(rd, "Health");
                        userInfo.CardID = MySqlDataReader.GetString(rd, "CardID");
                        userInfo.Professional = MySqlDataReader.GetString(rd, "Professional");
                        userInfo.StaffLevel = MySqlDataReader.GetString(rd, "StaffLevel");
                        userInfo.LevelClass = MySqlDataReader.GetString(rd, "LevelClass");
                        userInfo.ProbationEndDate = MySqlDataReader.GetString(rd, "ProbationEndDate");
                        userInfo.LaborContractStartDate = MySqlDataReader.GetString(rd, "LaborContractStartDate");
                        userInfo.LaborContractEndDate = MySqlDataReader.GetString(rd, "LaborContractEndDate");
                        userInfo.LaborContractType = MySqlDataReader.GetString(rd, "LaborContractType");
                        userInfo.Specialty = MySqlDataReader.GetString(rd, "Specialty");
                        userInfo.PhotoOne = MySqlDataReader.GetString(rd, "PhotoOne");
                        userInfo.PhotoTwo = MySqlDataReader.GetString(rd, "PhotoTwo");
                        list.Add(userInfo);
                    }
                    i++;
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (rd != null) rd.Close();
                conn.Close();
            }

            return list;
        }
        public List<UserInfo> FindNotInRole(UserInfo entity, int firstResult, int maxResults)
        {
            List<UserInfo> list = new List<UserInfo>();
            StringBuilder sql = new StringBuilder(@"SELECT ui.Id,Code,ui.Name,DomainAccount,Sex,Birthday,EmployedDate,WorkDate,Nationality,Party,Degree,HouseHold,Mobile,Telephone,MSN,QQ,Email,EmergencyContact,EmergencyTel,Address,ZipCode,Photo,ImmediateSupervisor,NativePlace,Health,CardID,Professional,StaffLevel,LevelClass,ProbationEndDate,LaborContractStartDate,LaborContractEndDate,LaborContractType,Specialty,PhotoOne,PhotoTwo
                FROM    UserInfo ui
                        LEFT JOIN dbo.UserDept ud ON ud.UserId = ui.Id
                WHERE   1 = 1
                        AND ui.IsDelete = 0 and ui.Id NOT IN (SELECT UserId FROM dbo.UserPosition) ");
            if (entity != null && !string.IsNullOrEmpty(entity.Name))
            {
                sql.AppendFormat(" and ui.Name like '%'+@Name+'%'");
            }
            if (entity != null && !string.IsNullOrEmpty(entity.DeptID))
            {
                sql.AppendFormat(" and ud.DeptID = @DeptID");
            }

            SqlConnection conn = ConnectionManager.GetConnection();
            SqlCommand cmd = new SqlCommand(sql.ToString(), conn);
            if (entity != null && !string.IsNullOrEmpty(entity.Name))
            {
                cmd.Parameters.Add("@Name", SqlDbType.NVarChar).Value = entity.Name;
            }
            if (entity != null && !string.IsNullOrEmpty(entity.DeptID))
            {
                cmd.Parameters.Add("@DeptID", SqlDbType.NVarChar).Value = entity.DeptID;
            }
            SqlDataReader rd = null;
            try
            {
                conn.Open();
                int i = 0;
                rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    if (i >= firstResult + maxResults && firstResult > 0)
                    {
                        break;
                    }
                    if ((i >= firstResult && i < (firstResult + maxResults)) || firstResult < 0)
                    {
                        UserInfo userInfo = new UserInfo();
                        userInfo.Id = MySqlDataReader.GetString(rd, "Id");
                        userInfo.Code = MySqlDataReader.GetString(rd, "Code");
                        userInfo.Name = MySqlDataReader.GetString(rd, "Name");
                        userInfo.DomainAccount = MySqlDataReader.GetString(rd, "DomainAccount");
                        userInfo.Sex = MySqlDataReader.GetString(rd, "Sex");
                        userInfo.Birthday = MySqlDataReader.GetString(rd, "Birthday");
                        userInfo.EmployedDate = MySqlDataReader.GetString(rd, "EmployedDate");
                        userInfo.WorkDate = MySqlDataReader.GetString(rd, "WorkDate");
                        userInfo.Nationality = MySqlDataReader.GetString(rd, "Nationality");
                        userInfo.Party = MySqlDataReader.GetString(rd, "Party");
                        userInfo.Degree = MySqlDataReader.GetString(rd, "Degree");
                        userInfo.HouseHold = MySqlDataReader.GetString(rd, "HouseHold");
                        userInfo.Mobile = MySqlDataReader.GetString(rd, "Mobile");
                        userInfo.Telephone = MySqlDataReader.GetString(rd, "Telephone");
                        userInfo.MSN = MySqlDataReader.GetString(rd, "MSN");
                        userInfo.QQ = MySqlDataReader.GetString(rd, "QQ");
                        userInfo.Email = MySqlDataReader.GetString(rd, "Email");
                        userInfo.EmergencyContact = MySqlDataReader.GetString(rd, "EmergencyContact");
                        userInfo.EmergencyTel = MySqlDataReader.GetString(rd, "EmergencyTel");
                        userInfo.Address = MySqlDataReader.GetString(rd, "Address");
                        userInfo.ZipCode = MySqlDataReader.GetString(rd, "ZipCode");
                        userInfo.Photo = MySqlDataReader.GetString(rd, "Photo");
                        userInfo.ImmediateSupervisor = MySqlDataReader.GetString(rd, "ImmediateSupervisor");
                        userInfo.NativePlace = MySqlDataReader.GetString(rd, "NativePlace");
                        userInfo.Health = MySqlDataReader.GetString(rd, "Health");
                        userInfo.CardID = MySqlDataReader.GetString(rd, "CardID");
                        userInfo.Professional = MySqlDataReader.GetString(rd, "Professional");
                        userInfo.StaffLevel = MySqlDataReader.GetString(rd, "StaffLevel");
                        userInfo.LevelClass = MySqlDataReader.GetString(rd, "LevelClass");
                        userInfo.ProbationEndDate = MySqlDataReader.GetString(rd, "ProbationEndDate");
                        userInfo.LaborContractStartDate = MySqlDataReader.GetString(rd, "LaborContractStartDate");
                        userInfo.LaborContractEndDate = MySqlDataReader.GetString(rd, "LaborContractEndDate");
                        userInfo.LaborContractType = MySqlDataReader.GetString(rd, "LaborContractType");
                        userInfo.Specialty = MySqlDataReader.GetString(rd, "Specialty");
                        userInfo.PhotoOne = MySqlDataReader.GetString(rd, "PhotoOne");
                        userInfo.PhotoTwo = MySqlDataReader.GetString(rd, "PhotoTwo");
                        list.Add(userInfo);
                    }
                    i++;
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (rd != null) rd.Close();
                conn.Close();
            }

            return list;
        }
        public List<UserInfo> FindDeptUser(UserInfo entity, int firstResult, int maxResults)
        {
            List<UserInfo> list = new List<UserInfo>();
            StringBuilder sql = new StringBuilder("select *,case UserInfo.Sex when '1' then '男' else '女' end as SexName ,(select Name from CompanyDepartment where id=(select DeptId from UserDept where UserId=UserInfo.Id)) as 'DeptName' from UserInfo where 1=1");
            if (entity != null && !string.IsNullOrEmpty(entity.Name))
            {
                sql.AppendFormat(" and Name like '%'+@Name+'%'");
            }
            if (entity != null && !string.IsNullOrEmpty(entity.DeptName))
            {
                sql.AppendFormat(" and id in (select UserId from UserDept where DeptID=@DeptName) ");
            }

            SqlConnection conn = ConnectionManager.GetConnection();
            SqlCommand cmd = new SqlCommand(sql.ToString(), conn);
            if (entity != null && !string.IsNullOrEmpty(entity.Name))
            {
                cmd.Parameters.Add("@Name", SqlDbType.NVarChar).Value = entity.Name;
            }
            if (entity != null && !string.IsNullOrEmpty(entity.DeptName))
            {
                cmd.Parameters.Add("@DeptName", SqlDbType.NVarChar).Value = entity.DeptName;
            }
            SqlDataReader rd = null;
            try
            {
                conn.Open();
                int i = 0;
                rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    if (i >= firstResult + maxResults && firstResult > 0)
                    {
                        break;
                    }
                    if ((i >= firstResult && i < (firstResult + maxResults)) || firstResult < 0)
                    {
                        UserInfo userInfo = new UserInfo();
                        userInfo.Id = MySqlDataReader.GetString(rd, "Id");
                        userInfo.Code = MySqlDataReader.GetString(rd, "Code");
                        userInfo.Name = MySqlDataReader.GetString(rd, "Name");
                        userInfo.DomainAccount = MySqlDataReader.GetString(rd, "DomainAccount");
                        userInfo.Sex = MySqlDataReader.GetString(rd, "Sex");
                        userInfo.Birthday = MySqlDataReader.GetString(rd, "Birthday");
                        userInfo.EmployedDate = MySqlDataReader.GetString(rd, "EmployedDate");
                        userInfo.WorkDate = MySqlDataReader.GetString(rd, "WorkDate");
                        userInfo.Nationality = MySqlDataReader.GetString(rd, "Nationality");
                        userInfo.Party = MySqlDataReader.GetString(rd, "Party");
                        userInfo.Degree = MySqlDataReader.GetString(rd, "Degree");
                        userInfo.HouseHold = MySqlDataReader.GetString(rd, "HouseHold");
                        userInfo.Mobile = MySqlDataReader.GetString(rd, "Mobile");
                        userInfo.Telephone = MySqlDataReader.GetString(rd, "Telephone");
                        userInfo.MSN = MySqlDataReader.GetString(rd, "MSN");
                        userInfo.QQ = MySqlDataReader.GetString(rd, "QQ");
                        userInfo.Email = MySqlDataReader.GetString(rd, "Email");
                        userInfo.EmergencyContact = MySqlDataReader.GetString(rd, "EmergencyContact");
                        userInfo.EmergencyTel = MySqlDataReader.GetString(rd, "EmergencyTel");
                        userInfo.Address = MySqlDataReader.GetString(rd, "Address");
                        userInfo.ZipCode = MySqlDataReader.GetString(rd, "ZipCode");
                        userInfo.Photo = MySqlDataReader.GetString(rd, "Photo");
                        userInfo.ImmediateSupervisor = MySqlDataReader.GetString(rd, "ImmediateSupervisor");
                        userInfo.NativePlace = MySqlDataReader.GetString(rd, "NativePlace");
                        userInfo.Health = MySqlDataReader.GetString(rd, "Health");
                        userInfo.CardID = MySqlDataReader.GetString(rd, "CardID");
                        userInfo.Professional = MySqlDataReader.GetString(rd, "Professional");
                        userInfo.StaffLevel = MySqlDataReader.GetString(rd, "StaffLevel");
                        userInfo.LevelClass = MySqlDataReader.GetString(rd, "LevelClass");
                        userInfo.ProbationEndDate = MySqlDataReader.GetString(rd, "ProbationEndDate");
                        userInfo.LaborContractStartDate = MySqlDataReader.GetString(rd, "LaborContractStartDate");
                        userInfo.LaborContractEndDate = MySqlDataReader.GetString(rd, "LaborContractEndDate");
                        userInfo.LaborContractType = MySqlDataReader.GetString(rd, "LaborContractType");
                        userInfo.Specialty = MySqlDataReader.GetString(rd, "Specialty");
                        userInfo.PhotoOne = MySqlDataReader.GetString(rd, "PhotoOne");
                        userInfo.PhotoTwo = MySqlDataReader.GetString(rd, "PhotoTwo");
                        userInfo.DeptName = MySqlDataReader.GetString(rd, "DeptName");
                        userInfo.SexName = MySqlDataReader.GetString(rd, "SexName");
                        list.Add(userInfo);
                    }
                    i++;
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (rd != null) rd.Close();
                conn.Close();
            }

            return list;
        }

        public List<UserInfo> Find(string SqlString)
        {
            List<UserInfo> list = new List<UserInfo>();
            SqlConnection conn = ConnectionManager.GetConnection();
            SqlCommand cmd = new SqlCommand(SqlString, conn);
            SqlDataReader rd = null;
            try
            {
                conn.Open();
                int i = 0;
                rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    UserInfo userInfo = new UserInfo();
                    userInfo.Id = MySqlDataReader.GetString(rd, "Id");
                    userInfo.Code = MySqlDataReader.GetString(rd, "Code");
                    userInfo.Name = MySqlDataReader.GetString(rd, "Name");
                    userInfo.DomainAccount = MySqlDataReader.GetString(rd, "DomainAccount");
                    userInfo.Sex = MySqlDataReader.GetString(rd, "Sex");
                    userInfo.Birthday = MySqlDataReader.GetString(rd, "Birthday");
                    userInfo.EmployedDate = MySqlDataReader.GetString(rd, "EmployedDate");
                    userInfo.WorkDate = MySqlDataReader.GetString(rd, "WorkDate");
                    userInfo.Nationality = MySqlDataReader.GetString(rd, "Nationality");
                    userInfo.Party = MySqlDataReader.GetString(rd, "Party");
                    userInfo.Degree = MySqlDataReader.GetString(rd, "Degree");
                    userInfo.HouseHold = MySqlDataReader.GetString(rd, "HouseHold");
                    userInfo.Mobile = MySqlDataReader.GetString(rd, "Mobile");
                    userInfo.Telephone = MySqlDataReader.GetString(rd, "Telephone");
                    userInfo.MSN = MySqlDataReader.GetString(rd, "MSN");
                    userInfo.QQ = MySqlDataReader.GetString(rd, "QQ");
                    userInfo.Email = MySqlDataReader.GetString(rd, "Email");
                    userInfo.EmergencyContact = MySqlDataReader.GetString(rd, "EmergencyContact");
                    userInfo.EmergencyTel = MySqlDataReader.GetString(rd, "EmergencyTel");
                    userInfo.Address = MySqlDataReader.GetString(rd, "Address");
                    userInfo.ZipCode = MySqlDataReader.GetString(rd, "ZipCode");
                    userInfo.Photo = MySqlDataReader.GetString(rd, "Photo");
                    userInfo.ImmediateSupervisor = MySqlDataReader.GetString(rd, "ImmediateSupervisor");
                    userInfo.NativePlace = MySqlDataReader.GetString(rd, "NativePlace");
                    userInfo.Health = MySqlDataReader.GetString(rd, "Health");
                    userInfo.CardID = MySqlDataReader.GetString(rd, "CardID");
                    userInfo.Professional = MySqlDataReader.GetString(rd, "Professional");
                    userInfo.StaffLevel = MySqlDataReader.GetString(rd, "StaffLevel");
                    userInfo.LevelClass = MySqlDataReader.GetString(rd, "LevelClass");
                    userInfo.ProbationEndDate = MySqlDataReader.GetString(rd, "ProbationEndDate");
                    userInfo.LaborContractStartDate = MySqlDataReader.GetString(rd, "LaborContractStartDate");
                    userInfo.LaborContractEndDate = MySqlDataReader.GetString(rd, "LaborContractEndDate");
                    userInfo.LaborContractType = MySqlDataReader.GetString(rd, "LaborContractType");
                    userInfo.Specialty = MySqlDataReader.GetString(rd, "Specialty");
                    userInfo.PhotoOne = MySqlDataReader.GetString(rd, "PhotoOne");
                    userInfo.PhotoTwo = MySqlDataReader.GetString(rd, "PhotoTwo");
                    userInfo.Percentage = MySqlDataReader.GetString(rd, "Percentage");
                    userInfo.Worktage = MySqlDataReader.GetString(rd, "Worktage");
                    userInfo.Hometage = MySqlDataReader.GetString(rd, "Hometage");
                    userInfo.ClassName = MySqlDataReader.GetString(rd, "ClassName");
                    list.Add(userInfo);

                    i++;
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (rd != null) rd.Close();
                conn.Close();
            }

            return list;
        }
        public void Delete(string id)
        {
            string sql = "delete  UserInfo where Id = @Id";
            SqlConnection conn = ConnectionManager.GetConnection();
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.Add("Id", SqlDbType.NVarChar).Value = id;
            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }

        }

        public void DeleteByIds(string ids)
        {
            string sql = "delete  UserInfo where Id in(" + ids + ")";
            SqlConnection conn = ConnectionManager.GetConnection();
            SqlCommand cmd = new SqlCommand(sql, conn);
            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }

        }


    }
}
