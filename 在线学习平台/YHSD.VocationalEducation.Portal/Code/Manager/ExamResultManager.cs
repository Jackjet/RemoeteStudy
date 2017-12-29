using System;
using System.Collections.Generic;
using YHSD.VocationalEducation.Portal.Code.Entity;
using YHSD.VocationalEducation.Portal.Code.Dao;
using YHSD.VocationalEducation.Portal.Code.Common;
using System.Data;
using System.Linq;
using System.Text;
namespace YHSD.VocationalEducation.Portal.Code.Manager
{
    public class ExamResultManager
    {
        public void Add(ExamResult entity)
        {
            new ExamResultDao().Add(entity);
        }

        public ExamResult Get(String id)
        {
            return new ExamResultDao().Get(id);
        }

        public void Update(ExamResult entity)
        {
            new ExamResultDao().Update(entity);
        }

        public int FindNum(ExamResult entity)
        {
            return new ExamResultDao().FindNum(entity);
        }
        public int FindMakingListNum(ExamResult entity)
        {
            return new ExamResultDao().FindMakingListNum(entity);
        }

        public List<ExamResult> Find(ExamResult entity, int firstResult, int maxResults)
        {
            List<ExamResult> ls=new ExamResultDao().Find(entity, firstResult, maxResults);
            ls.ForEach(delegate(ExamResult item)
            {
                item.CreateTime = CommonUtil.getDate(item.CreateTime);
                item.MarkingTime = CommonUtil.getDate(item.MarkingTime);
            });
            return ls;
        }
        public List<ExamResult> FindMakingList(ExamResult entity, int firstResult, int maxResults)
        {
            List<ExamResult> ls = new ExamResultDao().FindMakingList(entity, firstResult, maxResults);
            ls.ForEach(delegate(ExamResult item)
            {
                item.CreateTime = CommonUtil.getDate(item.CreateTime);
                item.MarkingTime = CommonUtil.getDate(item.MarkingTime);
            });
            return ls;
        }
        public List<ExamResult> FindTeacher(String UserID)
        {
            return new ExamResultDao().FindTeacher(UserID);
        }
        public int FindStuPaperScoreNum(PaperScore entity)
        {
            StringBuilder sbs = new StringBuilder(@"with CteClassInfos as(select ui.*,ci.Name CName,ci.ID CID from UserInfo ui left join ClassUser cu on ui.Id=cu.UId left join ClassInfo ci on cu.CId=ci.ID)
                select count(*) from ( select (select Name from UserInfo where Id=UserID)UserName,UserID,(select CName from CteClassInfos where Id=UserID)Class,(select Title from Papers where id=PaperID)PaperName,PaperID,max(score) MaxScore,avg(score) AvgScore,count(*) ExamCount from ExamResult where IsMarking=1 group by PaperID,UserID) tab where 1=1 ");
            if (entity != null && !string.IsNullOrEmpty(entity.PaperName))
            {
                sbs.AppendFormat(" and PaperName like '%" + entity.PaperName + "%'");
            }
            if (entity != null && !string.IsNullOrEmpty(entity.UserName))
            {
                sbs.AppendFormat(" and UserName like '%" + entity.UserName + "%'");
            }
            if (entity != null && !string.IsNullOrEmpty(entity.Class))
            {
                sbs.AppendFormat(" and Class like '%{0}%'", entity.Class);
            }
            int count;
            object obj = ConnectionManager.GetSingle(sbs.ToString());
            if (obj == null)
                return 0;
            if (int.TryParse(obj.ToString(), out count))
                return count;
            return 0;
        }
        public List<PaperScore> FindStuPaperScore(PaperScore entity, int firstResult, int maxResults)
        {
            StringBuilder sbs = new StringBuilder(@"with CteClassInfos as(select ui.*,ci.Name CName,ci.ID CID from UserInfo ui left join ClassUser cu on ui.Id=cu.UId left join ClassInfo ci on cu.CId=ci.ID)
                select * from ( select (select Name from UserInfo where Id=UserID)UserName,UserID,(select CName from CteClassInfos where Id=UserID)Class,(select Title from Papers where id=PaperID)PaperName,PaperID,max(score) MaxScore,avg(score) AvgScore,count(*) ExamCount from ExamResult where IsMarking=1 group by PaperID,UserID) tab where 1=1 ");
            if (entity != null && !string.IsNullOrEmpty(entity.PaperName))
            {
                sbs.AppendFormat(" and PaperName like '%{0}%' ", entity.PaperName);
            }
            if (entity != null && !string.IsNullOrEmpty(entity.UserName))
            {
                sbs.AppendFormat(" and UserName like '%{0}%' ", entity.UserName);
            }
            if (entity != null && !string.IsNullOrEmpty(entity.Class))
            {
                sbs.AppendFormat(" and Class like '%{0}%' ", entity.Class);
            }
            sbs.Append(" order by (select CreateTime from Papers where ID=PaperID) desc,MaxScore desc");
            DataTable dt = ConnectionManager.Query(sbs.ToString()).Tables[0];
            var ls = from item in dt.AsEnumerable()
                     select new PaperScore
                     {
                         Class = CommonUtil.GetString(item["Class"]),
                         UserName = CommonUtil.GetString(item["UserName"]),
                         UserID = CommonUtil.GetString(item["UserID"]),
                         PaperName = CommonUtil.GetString(item["PaperName"]),
                         PaperID = CommonUtil.GetString(item["PaperID"]),
                         MaxScore = CommonUtil.GetString(item["MaxScore"]),
                         AvgScore = CommonUtil.GetString(item["AvgScore"]),
                         ExamCount = CommonUtil.GetString(item["ExamCount"])
                     };
            if (firstResult < 0)
                return ls.ToList();
            return ls.Skip(firstResult).Take(maxResults).ToList();
        }

        public int FindClassPaperScoreNum(PaperScore entity)
        {
            StringBuilder sbs = new StringBuilder(@"with CteClassInfo as
                (select ui.*,ci.Name CName,ci.ID CID from UserInfo ui left join ClassUser cu on ui.Id=cu.UId left join ClassInfo ci on cu.CId=ci.ID)
                select count(*) from (select (select Name from ClassInfo where id=cci.CID) Class,cci.CID,(select Title from Papers where id=PaperID)PaperName,PaperID,max(score) MaxScore,avg(score) AvgScore from ExamResult er left join CteClassInfo cci on er.UserID=cci.Id where IsMarking=1 group by PaperID,cci.CID) tab where 1=1");
            if (entity != null && !string.IsNullOrEmpty(entity.PaperName))
            {
                sbs.AppendFormat(" and PaperName like '%" + entity.PaperName + "%'");
            }
            if (entity != null && !string.IsNullOrEmpty(entity.Class))
            {
                sbs.AppendFormat(" and Class like '%" + entity.Class + "%'");
            }
            int count;
            object obj = ConnectionManager.GetSingle(sbs.ToString());
            if (obj == null)
                return 0;
            if (int.TryParse(obj.ToString(), out count))
                return count;
            return 0;
        }

        public List<PaperScore> FindClassPaperScore(PaperScore entity, int firstResult, int maxResults)
        {
            //            StringBuilder sbs = new StringBuilder(@"with CteClassInfo as
            //                (select ui.*,ci.Name CName,ci.ID CID from UserInfo ui left join ClassUser cu on ui.Id=cu.UId left join ClassInfo ci on cu.CId=ci.ID)
            //                select *,dbo.GetPassPercentByCID(CID) PassPercent from (select (select Name from ClassInfo where id=cci.CID) Class,cci.CID,(select Title from Papers where id=PaperID)PaperName,PaperID,max(score) MaxScore,avg(score) AvgScore from ExamResult er left join CteClassInfo cci on er.UserID=cci.Code where IsMarking=1 group by PaperID,cci.CID) tab where 1=1");
            StringBuilder sbs = new StringBuilder(@"
                    WITH    CteClassInfo
                          AS ( SELECT   ui.* ,
                                        ci.Name CName ,
                                        ci.ID CID
                               FROM     UserInfo ui
                                        LEFT JOIN ClassUser cu ON ui.Id = cu.UId
                                        LEFT JOIN ClassInfo ci ON cu.CId = ci.ID
                             )
                    SELECT  * ,
                            ( SELECT    ROUND(AVG(MaxScore), 2)
                              FROM      ( SELECT    CID ,
                                                    PaperID ,
                                                    UserID ,
                                                    MAX(Score) MaxScore
                                          FROM      ( SELECT    ( dbo.getCIDByCode(UserID) ) CID ,
                                                                *
                                                      FROM      examResult
                                                      WHERE     isdelete = 0
                                                    ) tab1
                                          WHERE     cid = tab.CID
                                                    AND PaperID = tab.PaperID
                                          GROUP BY  CID ,
                                                    PaperID ,
                                                    UserID
                                        ) temp
                              WHERE     1 = 1
                            ) AvgScore ,
                            ( SELECT    MAX(MaxScore)
                              FROM      ( SELECT    CID ,
                                                    PaperID ,
                                                    UserID ,
                                                    MAX(Score) MaxScore
                                          FROM      ( SELECT    ( dbo.getCIDByCode(UserID) ) CID ,
                                                                *
                                                      FROM      examResult
                                                      WHERE     isdelete = 0
                                                    ) tab1
                                          WHERE     cid = tab.CID
                                                    AND PaperID = tab.PaperID
                                          GROUP BY  CID ,
                                                    PaperID ,
                                                    UserID
                                        ) temp
                              WHERE     1 = 1
                            ) MaxScore ,
                            dbo.GetPassPercentByCID(tab.CID,tab.PaperID) PassPercent
                    FROM    ( SELECT    ( SELECT    Name
                                          FROM      ClassInfo
                                          WHERE     id = cci.CID
                                        ) Class ,
                                        cci.CID ,
                                        ( SELECT    Title
                                          FROM      Papers
                                          WHERE     id = PaperID
                                        ) PaperName ,
                                        PaperID
                              FROM      ExamResult er
                                        LEFT JOIN CteClassInfo cci ON er.UserID = cci.Id
                              WHERE     IsMarking = 1
                              GROUP BY  PaperID ,
                                        cci.CID
                            ) tab
                    WHERE   1 = 1 ");
            if (entity != null && !string.IsNullOrEmpty(entity.PaperName))
            {
                sbs.AppendFormat(" and PaperName like '%" + entity.PaperName + "%'");
            }
            if (entity != null && !string.IsNullOrEmpty(entity.Class))
            {
                sbs.AppendFormat(" and Class like '%" + entity.Class + "%'");
            }

            sbs.Append(" order by PassPercent DESC,(select CreateTime from Papers where ID=PaperID) desc,MaxScore desc");
            DataTable dt = ConnectionManager.Query(sbs.ToString()).Tables[0];
            var ls = from item in dt.AsEnumerable()
                     select new PaperScore
                     {
                         Class = CommonUtil.GetString(item["Class"]),
                         PaperName = CommonUtil.GetString(item["PaperName"]),
                         PaperID = CommonUtil.GetString(item["PaperID"]),
                         MaxScore = CommonUtil.GetString(item["MaxScore"]),
                         AvgScore = CommonUtil.GetString(item["AvgScore"]),
                         PassPercent = CommonUtil.GetString(item["PassPercent"]),
                     };
            if (firstResult < 0)
                return ls.ToList();
            return ls.Skip(firstResult).Take(maxResults).ToList();
        }
        public int FindStatisticsQuestionNum(PaperScore entity)
        {
            StringBuilder sbs = new StringBuilder(@"
                        SELECT  COUNT(*)
                        FROM    ( SELECT    Title ,
                                            QuestionType ,
											dbo.ErrorPercent(StoreID) ErrorPercent,
                                            dbo.GetUserNameByCode(CreateUser) CreateUser
                                  FROM      dbo.QuestionStore
                                  WHERE     IsDelete = 0
                                ) tab WHERE 1=1 AND ErrorPercent<>'0.0%'  ");
            if (entity != null && !string.IsNullOrEmpty(entity.Title))
            {
                sbs.AppendFormat(" and Title like '%" + entity.Title + "%'");
            }
            if (entity != null && !string.IsNullOrEmpty(entity.CreateUser))
            {
                sbs.AppendFormat(" and CreateUser like '%" + entity.CreateUser + "%'");
            }
            if (entity != null && !string.IsNullOrEmpty(entity.QuestionType))
            {
                sbs.AppendFormat(" and QuestionType like '%" + entity.QuestionType + "%'");
            }
            int count;
            object obj = ConnectionManager.GetSingle(sbs.ToString());
            if (obj == null)
                return 0;
            if (int.TryParse(obj.ToString(), out count))
                return count;
            return 0;
        }
        public List<PaperScore> FindStatisticsQuestion(PaperScore entity, int firstResult, int maxResults)
        {
            StringBuilder sbs = new StringBuilder(@"
                SELECT * FROM (
                    SELECT  dbo.ErrorPercent(StoreID) ErrorPercent ,
                            ( SELECT    COUNT(*)
                              FROM      dbo.ExamAnswer
                              WHERE     QuestionID IN ( SELECT  ID
                                                        FROM    dbo.ExamQuestionStore
                                                        WHERE   OldStoreID = QuestionStore.StoreID )
                            ) ShowCount ,
                            id ,
                            dbo.QuestionStore.QuestionType ,
                            dbo.QuestionStore.Title ,
                            CreateTime ,
                            dbo.getUserNameByCode(CreateUser) CreateUser
                    FROM    dbo.QuestionStore
                    WHERE   IsDelete = 0) tab where 1=1 AND ErrorPercent<>'0.0%' ");
            if (entity != null && !string.IsNullOrEmpty(entity.Title))
            {
                sbs.AppendFormat(" and Title like '%" + entity.Title + "%'");
            }
            if (entity != null && !string.IsNullOrEmpty(entity.CreateUser))
            {
                sbs.AppendFormat(" and CreateUser like '%" + entity.CreateUser + "%'");
            }
            if (entity != null && !string.IsNullOrEmpty(entity.QuestionType))
            {
                sbs.AppendFormat(" and QuestionType like '%" + entity.QuestionType + "%'");
            }
            sbs.Append(" ORDER BY len(ErrorPercent)DESC,ErrorPercent DESC,ShowCount DESC ");
            DataTable dt = ConnectionManager.Query(sbs.ToString()).Tables[0];
            var ls = from item in dt.AsEnumerable()
                     select new PaperScore
                     {
                         Title = CommonUtil.GetString(item["Title"]),
                         ShowCount = CommonUtil.GetString(item["ShowCount"]),
                         ErrorPercent = CommonUtil.GetString(item["ErrorPercent"]),
                         CreateUser = CommonUtil.GetString(item["CreateUser"]),
                         CreateTime = CommonUtil.getDate(item["CreateTime"]),
                         QuestionType = CommonUtil.GetString(item["QuestionType"])
                     };
            if (firstResult < 0)
                return ls.ToList();
            return ls.Skip(firstResult).Take(maxResults).ToList();
        }

        public void Delete(string id)
        {
            new ExamResultDao().Delete(id);
        }

        public void DeleteByIds(string ids)
        {
            new ExamResultDao().DeleteByIds(ids);
        }


    }
}
