using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL
{
  public  class RandomHelper
    {
        /// <summary>
        /// 加密随机数生成器，生成随机种子
        /// </summary>
        /// <returns></returns>
        private static int Chaos_GetRandomSeed()
        {
            byte[] bytes = new byte[4];
            System.Security.Cryptography.RNGCryptoServiceProvider rng = new System.Security.Cryptography.RNGCryptoServiceProvider();
            rng.GetBytes(bytes);
            return BitConverter.ToInt32(bytes, 0);
        }

      public static string  GetRandomForStuSfzjh()
      {

          //用消息框输出十个随机数
          string strMSG = "";
          for (int i = 0; i < 15; i++)
          {
              //每次生成随机数的时候都使用机密随机数生成器来生成种子，
              //这样即使在很短的时间内也可以保证生成的随机数不同
              Random rdm = new Random(Chaos_GetRandomSeed());

              //获取一个10到30之间的随机数
              int iRand = rdm.Next(10, 300);
              strMSG += iRand.ToString();
          }
          strMSG = strMSG.Substring(0, 15);
          return "YQ" + strMSG;
      }
    }
}
