using System.IO;
using System.Web;
using System.Web.SessionState;

namespace MemberLogin_Sample
{
    /// <summary>
    /// Summary description for MemberLogin
    /// </summary>
    public class MemberLogin : IHttpHandler,IRequiresSessionState
    {
        readonly MemberService _memberService = new MemberService();

        public void ProcessRequest(HttpContext context)
        {
            //取得使用者輸入密碼
            var password = context.Request["password"];
            //取得使用者輸入帳號
            var userName = context.Request["userName"];

            //驗證使用者是否登入
            if (_memberService.IsUserLogin(userName, password))
            {
                //如果登入在Session
                context.Session["IsLogin"] = true;
                //登入成功導轉到會員列表頁面
                context.Response.Redirect("~/MemberInfo.ashx");
            }
            else
            {
                //File.ReadAllText讀取loginPage的樣板檔案
                //context.Server.MapPath讀取網站跟目錄下檔案路徑
               var loginPageHtml = File.ReadAllText(context.Server.MapPath("~/loginPage.html"))
                   .Replace("<LoginFail/>", "登入失敗!");
                context.Response.Write(loginPageHtml);
                context.Response.ContentType = "text/html";
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}