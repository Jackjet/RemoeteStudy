using System;
using System.Collections.Generic;
using YHSD.VocationalEducation.Portal.Code.Entity;
using YHSD.VocationalEducation.Portal.Code.Dao;
using YHSD.VocationalEducation.Portal.Code.Common;


namespace YHSD.VocationalEducation.Portal.Code.Manager
{
    public class CompanyDepartmentManager
    {
        public void Add(CompanyDepartment entity)
        {
            new CompanyDepartmentDao().Add(entity);
        }

        public CompanyDepartment Get(String id)
        {
            return new CompanyDepartmentDao().Get(id);
        }

        public void Update(CompanyDepartment entity)
        {
            new CompanyDepartmentDao().Update(entity);
        }

        public int FindNum(CompanyDepartment entity)
        {
            return new CompanyDepartmentDao().FindNum(entity);
        }

        public List<CompanyDepartment> Find(CompanyDepartment entity, int firstResult, int maxResults)
        {
            return new CompanyDepartmentDao().Find(entity, firstResult, maxResults);
        }
        public List<CompanyDepartment> FindByCode(CompanyDepartment entity)
        {
            return new CompanyDepartmentDao().FindByCode(entity);
        }

        public List<CompanyDepartment> Find(CompanyDepartment entity)
        {
            return new CompanyDepartmentDao().Find(entity);
        }

        /// <summary>
        /// 根据部门id查询部门所属公司
        /// </summary>
        /// <param name="id">部门Id</param>
        /// <returns></returns>
        public CompanyDepartment GetCompanyByDepartment(string id)
        {
            //查询当前节点信息
            CompanyDepartment companyDepartment = new CompanyDepartmentDao().Get(id);

            //查询所有部门
            CompanyDepartment entity = new CompanyDepartment();
            entity.Type = CompanyOrDepartment.Department;
            List<CompanyDepartment> departmentList = new CompanyDepartmentDao().Find(entity);

            //获得该部门的顶级部门
            CompanyDepartment topEntity = GetTopDepartmentNode(departmentList, companyDepartment.ParentId);

            if (topEntity == null)
            {
                topEntity = companyDepartment;
            }

            //查询所有公司
            entity = new CompanyDepartment();
            entity.Type = CompanyOrDepartment.Company;
            List<CompanyDepartment> companyList = new CompanyDepartmentDao().Find(entity);

            CompanyDepartment companyEntity = null;
            foreach(CompanyDepartment tempEntity in companyList)
            {
                if (tempEntity.Id.Equals(topEntity.ParentId))
                {
                    companyEntity = tempEntity;
                    break;
                }
            }

            return companyEntity;
        }

        /// <summary>
        /// 根据节点ID递归查询部门的顶级父节点
        /// </summary>
        /// <param name="departmentList">所有部门list</param>
        /// <param name="parentId">父节点ID</param>
        private CompanyDepartment GetTopDepartmentNode(List<CompanyDepartment> departmentList, string parentId)
        {
            CompanyDepartment companyDepartment = null;
            for (int i = 0; departmentList != null && i < departmentList.Count; i++)
            {
                CompanyDepartment entity = departmentList[i];

                if (entity.Id.Equals(parentId))
                {
                    companyDepartment = GetTopDepartmentNode(departmentList, entity.ParentId.ToString());

                    //如果上级节点为空，则当前节点为顶级节点
                    if (companyDepartment == null)
                        companyDepartment = entity;
                    break;
                }
            }

            return companyDepartment;
        }

        /// <summary>
        /// 根据id查询所有下级节点
        /// </summary>
        /// <param name="id">节点Id</param>
        /// <returns></returns>
        public List<CompanyDepartment> FindChildById(string id)
        {
            //查询所有 有父节点的节点
            List<CompanyDepartment> childList = new CompanyDepartmentManager().Find("-1",false);

            List<CompanyDepartment> list = new List<CompanyDepartment>();

            GetChildNodes(list, childList, id);

            return list;
        }

        /// <summary>
        /// 根据id查询下级节点（只返回当前节点下的子节点，不递归查询下级子站点）
        /// </summary>
        /// <param name="id">节点Id</param>
        /// <returns></returns>
        public List<CompanyDepartment> FindNextLevelChildById(string id)
        {
            //查询所有 有父节点的节点
            List<CompanyDepartment> childList = new CompanyDepartmentManager().Find("-1", false);

            List<CompanyDepartment> list = new List<CompanyDepartment>();

            for (int i = 0; childList != null && i < childList.Count; i++)
            {
                CompanyDepartment entity = childList[i];

                if (entity.ParentId.Equals(id))
                {
                    list.Add(entity);
                }
            }

            return list;
        }

        /// <summary>
        /// 根据父节点ID递归查询字节点
        /// </summary>
        /// <param name="list">字节点list</param>
        /// <param name="childList">所有有父节点的list</param>
        /// <param name="parentId">父节点ID</param>
        private void GetChildNodes(List<CompanyDepartment> list, List<CompanyDepartment> childList, string parentId)
        {
            for (int i = 0; childList != null && i < childList.Count; i++)
            {
                CompanyDepartment entity = childList[i];

                if (!entity.ParentId.Equals(parentId))
                {
                    continue;
                }

                GetChildNodes(list, childList, entity.Id.ToString());

                list.Add(entity);
            }
        }

        public void Delete(string id)
        {
            //new CompanyDepartmentDao().Delete(id);

            CompanyDepartmentDao companyDepartmentDao = new CompanyDepartmentDao();
            CompanyDepartment companyDepartment = companyDepartmentDao.Get(id);
            companyDepartment.IsDelete = PublicEnum.Yes;

            companyDepartmentDao.Update(companyDepartment);
        }

        public void DeleteByIds(string ids)
        {
            new CompanyDepartmentDao().DeleteByIds(ids);
        }

        /// <summary>
        /// 根据父节点和类型查询数据
        /// </summary>
        /// <param name="parentId">父节点ID</param>
        /// <param name="type">类型：公司或者部门</param>
        /// <param name="searchDeleteData">是否查询已经删除的数据</param>
        /// <returns></returns>
        public List<CompanyDepartment> Find(string parentId, Boolean searchDeleteData)
        {
            return new CompanyDepartmentDao().Find(parentId,searchDeleteData);
        }

        public void ClearDataExceptRootRecord()
        {
            new CompanyDepartmentDao().ClearDataExceptRootRecord();
        }
    }
}
