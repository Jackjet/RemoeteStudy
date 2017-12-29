using Microsoft.SharePoint;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
  public   class ErrorCatch
    {   
      public void SaveError(Exception ex,string login,string remark)
      {
      
          SPWeb web = SPContext.Current.Web;
          SPList list = web.Lists.TryGetList("ErrorList");
          SPListItem addItem = list.Items.Add();

          addItem["ErrorLoginName"] = login;
          addItem["ErrorMessage"] = ex.Message.ToString();//异常消息描述
          addItem["ErrorFunction"] = ex.TargetSite.ToString();//方法名
          addItem["ErrorPlace"] = ex.StackTrace.ToString();//位置
          
          addItem.Update();
     
  
      
      }
    }
}
