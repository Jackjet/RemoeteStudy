using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YHSD.VocationalEducation.Portal.Code.Dao;
using YHSD.VocationalEducation.Portal.Code.Entity;

namespace YHSD.VocationalEducation.Portal.Code.Manager
{
    public class CertificateInfoManager
    {
        public List<CertificateInfo> FindCertificateSearch(CertificateInfo entity)
        {
            return new CertificateInfoDao().FindCertificateSearch(entity);
        }

        public void Add(CertificateInfo entity)
        {
            new CertificateInfoDao().Add(entity);
        }
        public CertificateInfo GetCertificateById(string cerid)
        {
            return new CertificateInfoDao().GetCertificateById(cerid);
        }
        public void Update(CertificateInfo entity)
        {
            new CertificateInfoDao().Update(entity);
        }
        public void Delete(string id)
        {
            new CertificateInfoDao().Delete(id);
        }
        public DataTable BindUserInfoDrop() {
            return new CertificateInfoDao().BindUserInfoDrop();
        }
        public DataTable BindCurriculumDrop() {
            return new CertificateInfoDao().BindCurriculumDrop();
        } 
    }
}
