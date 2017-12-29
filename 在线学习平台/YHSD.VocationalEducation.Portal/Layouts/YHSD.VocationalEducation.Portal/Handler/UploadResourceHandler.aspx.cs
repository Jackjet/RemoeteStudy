using System;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using YHSD.VocationalEducation.Portal.Code.Entity;
using YHSD.VocationalEducation.Portal.Code.Manager;
using YHSD.VocationalEducation.Portal.Code.Common;
using System.IO;
using System.Web;

namespace YHSD.VocationalEducation.Portal.Layouts.YHSD.VocationalEducation.Portal.Handler
{
    public partial class UploadResourceHandler : BaseHandler
    {
        private const string ImgExtension = ".jpg";
        protected void Page_Load(object sender, EventArgs e)
        {
            string cmd = Request.Form["CMD"];
            if (string.IsNullOrEmpty(cmd))
                return;
            switch (cmd)
            {
                case "Upload":
                    Upload();
                    break;
                case "CatchImg":
                    CatchImg();
                    break;
                case "AddResource":
                    AddResource();
                    break;
                case "EditResource":
                    EditResource();
                    break;
                case "GetName":
                    string id = Request.Form["id"];
                    ResourceClassificationManager rm = new ResourceClassificationManager();
                    Response.Write(CommonUtil.Serialize(new { Success = true, Name = rm.Get(id).Name }));
                    break;
                case "GetModel":
                    GetModel();
                    break;
                case "DelAttachment":
                    DelAttachment();
                    break;
                default:
                    CommonUtil.UndefinedCMDException(Request.Form["CMD"]);
                    break;
            }

        }
        void DelAttachment()
        {
            string id = Request.Form["DelID"];
            AttachmentInfoManager aim = new AttachmentInfoManager();
            aim.Delete(id);
            base.Success();
        }
        void GetModel()
        {
            string id = Request.Form["EditID"];
            ResourceManager rm = new ResourceManager();
            Resource model = rm.Get(id);
            AttachmentInfoManager rim = new AttachmentInfoManager();
            AttachmentInfo AttachmentInfoModel = new AttachmentInfo();
            if (!model.AttachmentID.Equals(string.Empty))
                AttachmentInfoModel=rim.Get(model.AttachmentID);
            Response.Write(CommonUtil.Serialize(new { Success = true, Model = CommonUtil.Serialize(model), AttachmentInfoModel = CommonUtil.Serialize(AttachmentInfoModel) }));
        }
        void EditResource()
        {
            if (string.IsNullOrEmpty(Request.Form["Entity"]))
            {
                base.SystemError("未接收到任何数据!");
                return;
            }
            Resource entity = CommonUtil.DeSerialize<Resource>(Request.Form["Entity"]);
            Resource oldEntity = CommonUtil.DeSerialize<Resource>(Request.Form["OldEntity"]);
            AttachmentInfo attachment = CommonUtil.DeSerialize<AttachmentInfo>(Request.Form["AttachmentModel"]);
            if (Request.Form["IsNewAttach"].Equals(Boolean.TrueString,StringComparison.CurrentCultureIgnoreCase))
            {
                #region 将临时文件夹中的视频与图片移动到指定分类的文件夹
                string classificationPath = new ResourceClassificationManager().GetPathByID(entity.ClassificationID);// 分类路径
                string tempPath = string.Format("{0}{1}", CommonUtil.GetChildWebUrl(), ConnectionManager.SPTempFolder);
                string movePath = string.Format("{0}{1}", CommonUtil.GetChildWebUrl(), ConnectionManager.SPClassificationName);
                //step1 移动视频
                string oldVideoUrl = string.Format("{0}/{1}", tempPath, attachment.StoreName);
                string newVideoUrl = string.Format("{0}{1}/{2}", movePath, classificationPath, attachment.StoreName);
                CommonUtil.MoveFuJian(oldVideoUrl, newVideoUrl);

                //step2 移动图片
                string oldImgUrl = string.Format("{0}/{1}{2}", tempPath, attachment.ID, ImgExtension);
                string newImgUrl = string.Format("{0}{1}/{2}{3}", movePath, classificationPath, attachment.ID, ImgExtension);
                CommonUtil.MoveFuJian(oldImgUrl, newImgUrl);
                entity.PhotoUrl = string.Format("{0}", newImgUrl);
                attachment.SPUrl = newVideoUrl;
                #endregion
                new AttachmentInfoManager().Add(attachment);//保存到附件表
            }
            else if (!oldEntity.ClassificationID.Equals(entity.ClassificationID))//如果当前资源不是新上传的并且分类发生改变
            {
                #region 将原分类的视频与图片移动到新分类的的文件夹下
                string oldCf = oldEntity.ClassificationID;
                string newCf = entity.ClassificationID;

                string newClassificationPath = new ResourceClassificationManager().GetPathByID(entity.ClassificationID);// 新路径路径
                string oldClassificationPath = new ResourceClassificationManager().GetPathByID(oldEntity.ClassificationID);// 旧路径
                string movePath = string.Format("{0}{1}", CommonUtil.GetChildWebUrl(), ConnectionManager.SPClassificationName);

                //step1 Move video
                string oldVideoUrl = string.Format("{0}{1}/{2}", movePath, oldClassificationPath,attachment.StoreName);
                string newVideoUrl = string.Format("{0}{1}/{2}", movePath, newClassificationPath, attachment.StoreName);
                CommonUtil.MoveFuJian(oldVideoUrl, newVideoUrl);

                //step2 Move picture
                string oldImgUrl = string.Format("{0}{1}/{2}{3}", movePath, oldClassificationPath, entity.AttachmentID, ImgExtension);
                string newImgUrl = string.Format("{0}{1}/{2}{3}", movePath, newClassificationPath, entity.AttachmentID, ImgExtension);
                CommonUtil.MoveFuJian(oldImgUrl, newImgUrl);
                entity.PhotoUrl = newImgUrl;
                #endregion
            }
            new ResourceManager().Update(entity);
            base.Success();
        }

        #region Upload
        public void Upload()
        {
            if (Request.Files != null && Request.Files.Count > 0)
            {
                string cfId = Request.Form["ClassificationID"];
                Guid guid = Guid.NewGuid();//文件名称(不包含后缀)
                string odlName = Request.Files[0].FileName;//文件原始名
                string strExt = odlName.Substring(odlName.LastIndexOf('.')).ToLower();//后缀名
                string fileName = string.Format("{0}{1}", guid.ToString(), strExt);//GUID+后缀
                string fileFullPath = string.Format("{0}{1}", ConnectionManager.LocalSavePath, fileName);//物理磁盘的全路径

                try
                {
                    string spurl = string.Empty;
                    bool toLocal = UploadToLocal(fileFullPath);
                    if (!toLocal)
                    {
                        base.BusinessError("保存本地副本失败！");
                        return;
                    }
                    bool toSP = UploadToSPTempFolder(fileName, cfId, out spurl);//上传文件到文档库和本地并获取上传结果
                    if (!toSP)
                    {
                        base.BusinessError("上传到SharePoint失败！");
                        return;
                    }
                    ////此处将数据添加到附件表
                    //AttachmentInfoManager rm = new AttachmentInfoManager();
                    //rm.Add(new AttachmentInfo() { ID = guid.ToString(), FileName = odlName, StoreName = fileName, SPUrl = spurl, FileExtension = strExt });
                    AttachmentInfo model = new AttachmentInfo() { ID = guid.ToString(), FileName = odlName, StoreName = fileName, SPUrl = spurl, FileExtension = strExt };
                    Response.Write(CommonUtil.Serialize(
                        new { Success = true, FileName = fileName, Path = fileFullPath, AttachmentID = guid.ToString(), AttachmentModel = CommonUtil.Serialize(model) }
                    ));
                }
                catch (Exception ex)
                {
                    base.SystemError(ex);
                }
            }
        }
        /// <summary>
        /// 保存文件到本地
        /// </summary>
        public bool UploadToLocal(string fileFullPath)
        {
            HttpPostedFile file = Request.Files[0];//上传的文件
            file.SaveAs(fileFullPath);//保存文件到本地
            return true;
        }
        /// <summary>
        /// 上传到临时文件夹
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="cfId"></param>
        /// <param name="_spurl"></param>
        /// <returns></returns>
        public bool UploadToSPTempFolder(string fileName, string cfId, out string _spurl)
        {
            ResourceClassificationManager manager = new ResourceClassificationManager();
            string SPPath = string.Format("{0}{1}", CommonUtil.GetChildWebUrl(), ConnectionManager.SPTempFolder);
            _spurl = string.Format("{0}/{1}", SPPath, fileName);
            return CommonUtil.CreateFileByPath(SPPath, fileName, Request.Files[0].InputStream);
            #region OldCode
            //string cfId = Request.Form["ClassificationID"];
            //ResourceClassificationManager manager = new ResourceClassificationManager();
            //string SPPath = string.Format("{0}{1}", ConnectionManager.SPClassificationName, manager.GetPathByID(cfId));//根据分类ID 获取分类的路径(/Resource/教育资源/BootStrap培训)
            //_spurl = string.Format("{0}/{1}", SPPath, fileName);
            //return CommonUtil.CreateFileByPath(SPPath, fileName, Request.Files[0].InputStream);
            #endregion
        }
        #endregion

        #region CatchImg
        public void CatchImg()
        {
            try
            {
                string FilePath = Request.Form["FilePath"];
                string FileName = Request.Form["FileName"];
                string ClassificationID = Request.Form["ClassificationID"];
                string imgPath = CommonUtil.CatchImg(FilePath, ConnectionManager.FfmPegPath, ConnectionManager.FfmPegImgSize, 1);
                if (string.IsNullOrEmpty(imgPath))
                {
                    base.BusinessError("图片抓取失败!");
                    return;
                }

                byte[] bytes = FileUtil.LoadFile(imgPath);//加载本地图片

                if (bytes == null)
                {
                    base.BusinessError("读取本地文件失败!");
                    return;
                }
                //if (string.IsNullOrEmpty(ClassificationID))
                //{
                //    base.BusinessError("分类获取失败!");
                //    return;
                //}

                //ResourceClassificationManager manager = new ResourceClassificationManager();
                //string SPPath = string.Format("{0}{1}", ConnectionManager.SPClassificationName, manager.GetPathByID(ClassificationID));
                string SPPath =string.Format("{0}{1}",CommonUtil.GetChildWebUrl(),ConnectionManager.SPTempFolder);
                FileName = System.IO.Path.ChangeExtension(FileName, ImgExtension);

                string spImgUrl = CommonUtil.CreateFileByPath(SPPath, FileName, bytes);//图片上传到文档库
                if (string.IsNullOrEmpty(spImgUrl))
                {
                    base.BusinessError("上传截图到文档库失败!");
                    return;
                }

                Response.Write(CommonUtil.Serialize(new { Success = true, ImgUrl = spImgUrl }));
            }
            catch (Exception ex)
            {
                base.SystemError(ex);
            }
        }
        #endregion

        #region AddResource
        private void AddResource()
        {

            if (string.IsNullOrEmpty(Request.Form["Entity"]))
            {
                base.SystemError("未接收到任何数据!");
                return;
            }
            Resource entity = CommonUtil.DeSerialize<Resource>(Request.Form["Entity"]);
            AttachmentInfo model = CommonUtil.DeSerialize<AttachmentInfo>(Request.Form["AttachmentModel"]);

            string classificationPath = new ResourceClassificationManager().GetPathByID(entity.ClassificationID);// 分类路径

            #region 将视频与图片移动到新文件夹
            string tempPath = string.Format("{0}{1}", CommonUtil.GetChildWebUrl(), ConnectionManager.SPTempFolder);
            string movePath = string.Format("{0}{1}", CommonUtil.GetChildWebUrl(), ConnectionManager.SPClassificationName);
            //step1 移动视频
            string oldVideoUrl = string.Format("{0}/{1}", tempPath, model.StoreName);
            string newVideoUrl = string.Format("{0}{1}/{2}", movePath, classificationPath, model.StoreName);
            CommonUtil.MoveFuJian(oldVideoUrl, newVideoUrl);

            //step2 移动图片
            string oldImgUrl = string.Format("{0}/{1}{2}", tempPath, model.ID, ImgExtension);
            string newImgUrl = string.Format("{0}{1}/{2}{3}", movePath, classificationPath, model.ID, ImgExtension);
            CommonUtil.MoveFuJian(oldImgUrl, newImgUrl);
            entity.PhotoUrl = string.Format("{0}", newImgUrl);
            model.SPUrl = newVideoUrl;
            #endregion



            AttachmentInfoManager aim = new AttachmentInfoManager();
            aim.Add(model);//保存到附件表

            ResourceManager rm = new ResourceManager();
            rm.Add(entity);//保存到资源表
            base.Success();
        }
        #endregion
    }
}
