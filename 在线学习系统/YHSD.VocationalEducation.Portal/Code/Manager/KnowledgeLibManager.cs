using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YHSD.VocationalEducation.Portal.Code.Dao;
using YHSD.VocationalEducation.Portal.Code.Entity;

namespace YHSD.VocationalEducation.Portal.Code.Manager
{
    public class KnowledgeLibManager
    {
        public List<KnowledgeLib> FindKnowledgeLibSearch(KnowledgeLib entity)
        {
            return new KnowledgeLibDao().FindKnowledgeLibSearch(entity);
        }
        public KnowledgeLib GetKnowledgeById(string knowid)
        {
            return new KnowledgeLibDao().GetKnowledgeById(knowid);
        }
        public void Add(KnowledgeLib entity)
        {
            new KnowledgeLibDao().Add(entity);
        }
        public void Update(KnowledgeLib entity)
        {
            new KnowledgeLibDao().Update(entity);
        }
        public void Delete(string id)
        {
            new KnowledgeLibDao().Delete(id);
        }
    }
}
