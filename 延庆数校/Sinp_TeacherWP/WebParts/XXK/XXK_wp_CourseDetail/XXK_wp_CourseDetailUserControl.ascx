<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="XXK_wp_CourseDetailUserControl.ascx.cs" Inherits="Sinp_TeacherWP.WebParts.XXK.XXK_wp_CourseDetail.XXK_wp_CourseDetailUserControl" %>
<script src="../../../_layouts/15/Script/jquery-1.8.2.min.js"></script>
<link href="../../../_layouts/15/Style/common.css" rel="stylesheet" />
<link href="../../../_layouts/15/Style/st_index.css" rel="stylesheet" />

<script type="text/javascript">
    $(document).ready(function () {
        /* 滑动/展开 */
        var abb = $(".kc_zuoye").find("dd ul li ol");
        var acc = $(".kc_zuoye").find("dd ul li div i");
        $(".list_kctitle").click(function () {
            $(this).next(abb).slideToggle();
            $(this).find("i").html($(this).find("i").html() == "+" ? "-" : "+");
        });

    });


</script>

<dl class="my_kc">
    <dt class="my_kc_xxk">
        <ul>
            <li><a href="">选修课信息</a></li>
            <li class="active"><a href="">我的选修课</a></li>
        </ul>
    </dt>
    <dd class="my_kc_xq">
        <div class="music_kc">
             <span class="zt fr">
                <asp:Image ID="Img_Status" ImageUrl="/_layouts/15/TeacherImages/wait.png" runat="server" CssClass="ZT_img" /></span>
            <img src="images/zs28.jpg" alt="">
            <div class="music_nr">
                <h2>音乐选修课</h2>
                <div>
                    <span>课程类别：专业选修课</span>
                    <span>上课场地：107场地</span>
                    <span>选课人数上线：50</span>
                    <span>硬件要求：音响、多媒体</span>
                </div>
                <p>在高等学校中学习某一专业的学生可以有选择地修习的课程。有些选修课是为介绍先进科学技术和最新科学成果；有些选修课是为扩大学生知识面（如中国语言文学专业的学生选修通史，化学专业的学生选修生物学，会计专业的学生选修法学概论等）；还有些选修课是为满足学生的兴趣爱好，发展他们某一方面的才能(如专业的学生选修文学、音乐、绘画、戏剧等课程)。选修课可分为限制性选修课与非限制性选修课。限制性选修课也称指定选修课，指学生须在某一学科门类的领域或一组课程中选修；如有的专业教学计划规定高年级学生须在某一专门组或选修组中选修若干门课程...</p>
                <div>附件：<a href="">课程评价标准.doc</a></div>
            </div>
        </div>
        <dl class="bm_student">
            <dt>
                <span class="active">热门活动</span>
                <p>查看全部报名人员</p>
            </dt>
            <dd>
                <img src="images/zs26.jpg" alt="">
                <img src="images/zs22.jpg" alt="">
                <img src="images/zs11.jpg" alt="">
                <img src="images/zs28.jpg" alt="">
                <img src="images/zs20.jpg" alt="">
                <img src="images/zs21.jpg" alt="">
                <img src="images/zs26.jpg" alt="">
                <img src="images/zs22.jpg" alt="">
                <img src="images/zs11.jpg" alt="">
                <img src="images/zs28.jpg" alt="">
                <img src="images/zs20.jpg" alt="">
                <img src="images/zs22.jpg" alt="">
                <img src="images/zs11.jpg" alt="">
                <img src="images/zs28.jpg" alt="">
                <img src="images/zs20.jpg" alt="">
                <img src="images/zs21.jpg" alt="">
            </dd>
        </dl>
        <div class="kc_ls">
            <dl class="kc_zuoye">
                <dt>
                    <span class="active">课程作业</span>
                </dt>
                <dd>
                    <ul>
                        <li>
                            <div class="list_kctitle">
                                <p>1、课程介绍</p>
                                <i>+</i>
                            </div>
                            <ol>
                                <li><a href="">王艳艳-课程介绍熟悉.doc</a><div>09-22</div>
                                    <span><a href="">评价作业</a></span></li>
                                <li><a href="">理三-课程介绍熟悉.doc</a><div>09-22</div>
                                    <span><a href="">评价作业</a></span></li>
                                <li><a href="">晓笑笑-课程介绍熟悉.doc</a><div>09-22</div>
                                    <span><a href="">评价作业</a></span></li>
                                <li><a href="">王艳艳-课程介绍熟悉.doc</a><div>09-22</div>
                                    <span><a href="">评价作业</a></span></li>
                                <li><a href="">理三-课程介绍熟悉.doc</a><div>09-22</div>
                                    <span><a href="">评价作业</a></span></li>
                                <li><a href="">晓笑笑-课程介绍熟悉.doc</a><div>09-22</div>
                                    <span><a href="">评价作业</a></span></li>
                            </ol>
                        </li>
                        <li>
                            <div class="list_kctitle">
                                <p>2、代数人称代词</p>
                                <i>+</i>
                            </div>
                            <ol>
                                <li><a href="">晓笑笑-课程介绍熟悉.doc</a><div>09-22</div>
                                    <span><a href="">评价作业</a></span></li>
                                <li><a href="">王艳艳-课程介绍熟悉.doc</a><div>09-22</div>
                                    <span><a href="">评价作业</a></span></li>
                                <li><a href="">理三-课程介绍熟悉.doc</a><div>09-22</div>
                                    <span><a href="">评价作业</a></span></li>
                            </ol>
                        </li>
                        <li>
                            <div class="list_kctitle">
                                <p>3、名次的阴阳性、名义句子、人称代词词尾</p>
                                <i>+</i>
                            </div>
                            <ol>
                                <li><a href="">晓笑笑-课程介绍熟悉.doc</a><div>09-22</div>
                                    <span><a href="">评价作业</a></span></li>
                                <li><a href="">王艳艳-课程介绍熟悉.doc</a><div>09-22</div>
                                    <span><a href="">评价作业</a></span></li>
                                <li><a href="">理三-课程介绍熟悉.doc</a><div>09-22</div>
                                    <span><a href="">评价作业</a></span></li>
                                <li><a href="">王艳艳-课程介绍熟悉.doc</a><div>09-22</div>
                                    <span><a href="">评价作业</a></span></li>
                                <li><a href="">理三-课程介绍熟悉.doc</a><div>09-22</div>
                                    <span><a href="">评价作业</a></span></li>
                            </ol>
                        </li>
                        <li>
                            <div class="list_kctitle">
                                <p>4、根词和动词的过去式用法</p>
                                <i>+</i>
                            </div>
                        </li>
                        <li>
                            <div class="list_kctitle">
                                <p>5、疑问句，人称代词的复数形式，完整是复数形式</p>
                                <i>+</i>
                            </div>
                        </li>
                        <li>
                            <div class="list_kctitle">
                                <p>6、代数人称代词</p>
                                <i>+</i>
                            </div>
                        </li>
                        <li>
                            <div class="list_kctitle">
                                <p>7、名次的阴阳性、名义句子、人称代词词尾</p>
                                <i>+</i>
                            </div>
                        </li>
                        <li>
                            <div class="list_kctitle">
                                <p>8、根词和动词的过去式用法</p>
                                <i>+</i>
                            </div>
                        </li>
                        <li>
                            <div>
                                <p>9、疑问句，人称代词的复数形式，完整是复数形式</p>
                                <i>+</i>
                            </div>
                        </li>
                    </ul>
                </dd>
            </dl>
            <dl class="js_ziyuan">
                <dl class="js_jianjie">
                    <dt>
                        <span class="active">教师简介</span>
                    </dt>
                    <dd>
                        <img src="images/zs11.jpg" alt="">
                        <p>王小舒，共产党员，曾毕业于东北师范大学，小脚高级职称。在三尺讲台上，辛勤耕耘经济学院本科生建立，本着户外旅行以及对同学们课外生活的丰富，曾获省市级优秀教师...</p>
                    </dd>
                </dl>
                <dl class="js_jianjie">
                    <dt>
                        <span class="active">课程资源</span>
                    </dt>
                    <dd>
                        <ul>
                            <li>
                                <a href=""><i class="iconfont">&#xe60d;</i>理三-课程介绍熟悉.doc</a>
                                <div>09-22</div>
                            </li>
                            <li>
                                <a href=""><i class="iconfont">&#xe60e;</i>中华传统文学修养是一门提高学生对传统文化认知的课程.pdf</a>
                                <div>09-22</div>
                            </li>
                            <li>
                                <a href=""><i class="iconfont">&#xe60d;</i>辛勤耕耘经济学院本科生建立.doc</a>
                                <div>09-22</div>
                            </li>
                            <li>
                                <a href=""><i class="iconfont">&#xe60e;</i>户外旅行以及对同学们课外生活的丰富.pdf</a>
                                <div>09-22</div>
                            </li>
                        </ul>
                    </dd>
                </dl>
            </dl>
        </div>
    </dd>
</dl>

