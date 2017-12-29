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
        /// ���ݲ���id��ѯ����������˾
        /// </summary>
        /// <param name="id">����Id</param>
        /// <returns></returns>
        public CompanyDepartment GetCompanyByDepartment(string id)
        {
            //��ѯ��ǰ�ڵ���Ϣ
            CompanyDepartment companyDepartment = new CompanyDepartmentDao().Get(id);

            //��ѯ���в���
            CompanyDepartment entity = new CompanyDepartment();
            entity.Type = CompanyOrDepartment.Department;
            List<CompanyDepartment> departmentList = new CompanyDepartmentDao().Find(entity);

            //��øò��ŵĶ�������
            CompanyDepartment topEntity = GetTopDepartmentNode(departmentList, companyDepartment.ParentId);

            if (topEntity == null)
            {
                topEntity = companyDepartment;
            }

            //��ѯ���й�˾
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
        /// ���ݽڵ�ID�ݹ��ѯ���ŵĶ������ڵ�
        /// </summary>
        /// <param name="departmentList">���в���list</param>
        /// <param name="parentId">���ڵ�ID</param>
        private CompanyDepartment GetTopDepartmentNode(List<CompanyDepartment> departmentList, string parentId)
        {
            CompanyDepartment companyDepartment = null;
            for (int i = 0; departmentList != null && i < departmentList.Count; i++)
            {
                CompanyDepartment entity = departmentList[i];

                if (entity.Id.Equals(parentId))
                {
                    companyDepartment = GetTopDepartmentNode(departmentList, entity.ParentId.ToString());

                    //����ϼ��ڵ�Ϊ�գ���ǰ�ڵ�Ϊ�����ڵ�
                    if (companyDepartment == null)
                        companyDepartment = entity;
                    break;
                }
            }

            return companyDepartment;
        }

        /// <summary>
        /// ����id��ѯ�����¼��ڵ�
        /// </summary>
        /// <param name="id">�ڵ�Id</param>
        /// <returns></returns>
        public List<CompanyDepartment> FindChildById(string id)
        {
            //��ѯ���� �и��ڵ�Ľڵ�
            List<CompanyDepartment> childList = new CompanyDepartmentManager().Find("-1",false);

            List<CompanyDepartment> list = new List<CompanyDepartment>();

            GetChildNodes(list, childList, id);

            return list;
        }

        /// <summary>
        /// ����id��ѯ�¼��ڵ㣨ֻ���ص�ǰ�ڵ��µ��ӽڵ㣬���ݹ��ѯ�¼���վ�㣩
        /// </summary>
        /// <param name="id">�ڵ�Id</param>
        /// <returns></returns>
        public List<CompanyDepartment> FindNextLevelChildById(string id)
        {
            //��ѯ���� �и��ڵ�Ľڵ�
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
        /// ���ݸ��ڵ�ID�ݹ��ѯ�ֽڵ�
        /// </summary>
        /// <param name="list">�ֽڵ�list</param>
        /// <param name="childList">�����и��ڵ��list</param>
        /// <param name="parentId">���ڵ�ID</param>
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
        /// ���ݸ��ڵ�����Ͳ�ѯ����
        /// </summary>
        /// <param name="parentId">���ڵ�ID</param>
        /// <param name="type">���ͣ���˾���߲���</param>
        /// <param name="searchDeleteData">�Ƿ��ѯ�Ѿ�ɾ��������</param>
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
