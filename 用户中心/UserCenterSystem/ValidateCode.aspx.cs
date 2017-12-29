using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UserCenterSystem
{
    public partial class ValidateCode : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string checkCode = CreateRandomCode(4);
                Session["CheckCode"] = checkCode;

                CreateImage(checkCode);
            }
        }
        /// <summary>
        /// 生成随机码
        /// </summary>
        /// <param name="codeCount">个数</param>
        /// <returns></returns>
        private string CreateRandomCode(int codeCount)
        {
            string allChar = "2,3,4,5,6,7,8,9,A,B,C,D,E,F,G,H,J,K,L,M,N,P,Q,R,S,T,U,W,X,Y,Z";
            string[] allCharArray = allChar.Split(',');
            string randomCode = "";
            int temp = -1; 
            Random rand = new Random();
            for (int i = 0; i < codeCount; i++)
            {
                if (temp != -1)
                {
                    rand = new Random(i * temp * ((int)DateTime.Now.Ticks));
                }
                int t = rand.Next(31);
                if (temp == t)
                {
                    return CreateRandomCode(codeCount);
                }
                temp = t;
                randomCode += allCharArray[t];
            }
            return randomCode;
        }
        /// <summary>
        /// 生成图片
        /// </summary>
        /// <param name="checkCode"></param>
        private void CreateImage(string checkCode)
        {
            int iwidth = (int)(checkCode.Length * 17.5);
            //创建画布，定义大小
            System.Drawing.Bitmap image = new System.Drawing.Bitmap(iwidth, 25);
            //用画布创建画板
            Graphics g = Graphics.FromImage(image);
            //定义文本格式，字体、字号和字形特性
            Font f = new System.Drawing.Font("Comic Sans MS", 14, System.Drawing.FontStyle.Italic);
            //定义画刷的颜色（文字的颜色）
            Brush b = new System.Drawing.SolidBrush(Color.Black);
            //g.FillRectangle(new System.Drawing.SolidBrush(Color.Blue),0,0,image.Width, image.Height); 
            //清除画布底色，换上指定的颜色
            g.Clear(Color.WhiteSmoke);
            //用定义好的文字、文本格式、画刷创建图片，定义开始坐标x，y
            g.DrawString(checkCode, f, b, 4, -2);
            //定义直线的颜色和长度
            Pen blackPen = new Pen(Color.AliceBlue, 0);
            Random rand = new Random();
            for (int i = 0; i < 2; i++)
            {
                int y = rand.Next(image.Height);
                int x = rand.Next(image.Height);
                //在画板上绘制线
                g.DrawLine(blackPen, 0, x, image.Width, y);
            }
            //创建内存流
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            //将画布以指定的格式保存到流中
            image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            //清除缓冲区流中的所有内容输出
            Response.ClearContent();
            //制定内容类型
            Response.ContentType = "image/Jpeg";
            //转换成字节数组，写入流
            Response.BinaryWrite(ms.ToArray());
            //清除Graphics、Bitmap对象
            g.Dispose();
            image.Dispose();

            //g.FillRectangle(new System.Drawing.SolidBrush(Color.Blue),0,0,image.Width, image.Height); 
            // g.Clear(Color.Blue);
        }


    }
}