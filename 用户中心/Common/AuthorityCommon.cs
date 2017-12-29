using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;

namespace Common
{
    /// <summary>
    /// 权限管理公共类
    /// </summary>
   public static class AuthorityCommon
    {
       /// <summary>
       /// 判断用户是否是超级管理员
       /// </summary>
       /// <param name="Session"></param>
       /// <returns></returns>
       public static bool IsSuperAdmin(Base_Teacher teacher)
       {
           if (teacher.XXZZJGH == HandlerLogic.GetAdminViewName())
           {
               return true;
           }
           return false;   
       }
    }
}
